using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Reflection;
using GWNorthEngine.Scripting.Utils;
namespace GWNorthEngine.Scripting.Model {
	/// <summary>
	/// Models the data required to use an object via scripting
	/// </summary>
	internal class RegisteredObject {
		#region Class variables
		private string referenceName;
		private Object reference;
		private List<MethodInfo> methods;
		private List<PropertyInfo> properties;
		// helper methods
		private const string LIST_METHOD_NAMES = "listMethods";
		private const string LIST_PROPERTY_NAMES = "listProperties";
		private const string HELP = "help";
		#endregion Class variables

		#region Class properties
		/// <summary>
		/// Gets the reference name of the registered object
		/// </summary>
		public string ReferenceName { get { return this.referenceName; } }
		/// <summary>
		/// Gets the actual reference object of the registered object
		/// </summary>
		public Object Reference { get { return this.reference; } }
		#endregion Class properties

		#region Constructor
		/// <summary>
		/// Builds a new RegisteredObject
		/// </summary>
		/// <param name="reference">Object reference</param>
		/// <param name="referenceName">Reference name to access the object</param>
		/// <param name="methods">MethodInfo object array of methods you want accessible to the script, by default it is everything</param>
		/// <param name="properties">PropertyInfo object array of properties you want accessible to the script, by default it is everything</param>
		public RegisteredObject(ref Object reference, string referenceName, MethodInfo[] methods, PropertyInfo[] properties) {
			this.reference = reference;
			this.referenceName = referenceName;
			this.methods = methods.ToList<MethodInfo>();
			this.properties = properties.ToList<PropertyInfo>();
			// add our helper methods
			this.methods.Add(this.GetType().GetMethod(LIST_METHOD_NAMES));
			this.methods.Add(this.GetType().GetMethod(LIST_PROPERTY_NAMES));
			this.methods.Add(this.GetType().GetMethod(HELP));

			//Alpha sort
			this.methods.Sort(AlphaComparer.getInstance());
			this.properties.Sort(AlphaComparer.getInstance());
		}
		#endregion Constructor

		#region Support methods
		/// <summary>
		/// Runs a specific method, parses the input for its parameter etc
		/// </summary>
		/// <param name="input">Input to execute</param>
		/// <returns>True if a method was found to execute, otherwise false</returns>
		private bool runMethod(string input) {
			bool foundCommand = false;
			Object invokingObj;
			string command = getCommand(input);
			object[] parameters = null;
			// figure out if we have parameters
			// now that we have our method name it is time to run the method
			foreach (MethodInfo method in this.methods) {
				if (method.Name.ToLower() == command.ToLower()) {// Case insensitivity
					invokingObj = this.reference;
					if (method.Name == LIST_METHOD_NAMES || method.Name == LIST_PROPERTY_NAMES || method.Name == HELP) {
						//for our helper methods the object to invoker is actually this class so we need to change it
						invokingObj = this;
					}
					// determine if we have parameters
					if (input.Contains('(')) {
						parameters = getParameters(method, input);
						if (method.GetParameters().Length != parameters.Length) {
							// must be dealing with an overloaded function so move on to the next method
							continue;
						}
					}
					// invoke the method
					method.Invoke(invokingObj, parameters);
					foundCommand = true;
					break;
				}
			}
			return foundCommand;
		}

		/// <summary>
		/// Runs a property based on the input, parses the parameters etc
		/// </summary>
		/// <param name="input">Input to execute</param>
		/// <param name="result">outputs the return object of a getter</param>
		/// <returns>True if a property was found, otherwise false</returns>
		private bool runProperty(string input, out object result) {
			bool foundProperty = false;
			result = null;
			string command = getCommand(input);
			string value = null;
			// figure out if we are a get or set operation
			if (command.Contains('=')) {
				// we are a set so figure out our assignment value(s)
				string[] components = input.Split('=');
				command = components[0].Split(ScriptManager.COMMAND_IDENTIFIER)[1];
				command = command.Replace(" ", "");
				value = components[1].TrimStart(' ');// incase we did test = test we want to remove the leading space
			}
			// now that we have our property name and possible assignment value(s) we need to run the property
			foreach (PropertyInfo property in this.properties) {
				if (property.Name.ToLower() == command.ToLower().Replace(" ", "")) {// Case insensitivity and removing a possible leading space
					// if there is no equal sign we are a getting
					if (input.Contains('=')) {
						// run the property
						result = runProperty(property.GetAccessors(), ObjectTranslator.translate(property.PropertyType, value));
						foundProperty = true;
						break;
					} else {
						// run the property
						result = runProperty(property.GetAccessors());
						foundProperty = true;
						break;
					}
				}
			}
			return foundProperty;
		}

		/// <summary>
		/// Executes a specific properties methods
		/// </summary>
		/// <param name="methods">Methods avaialble</param>
		/// <param name="values">values to assign the property if we are doing a set</param>
		/// <returns>Get object if that is the property we are executing</returns>
		private object runProperty(MethodInfo[] methods, params object[] values) {
			object returnObj = null;
			foreach (MethodInfo method in methods) {
				// determines if this is a getter or setter that we are trying to execute
				if (values == null || values.Length < 1) {
					if (method.Name.StartsWith("get")) {
						// execute the property and return the value returned
						returnObj = method.Invoke(this.reference, null);
						break;
					}
				} else {
					if (method.Name.StartsWith("set")) {
						// execute the property
						method.Invoke(this.reference, values);
						break;
					}
				}
			}
			return returnObj;
		}

		/// <summary>
		/// Retrieves a command based on the input
		/// </summary>
		/// <param name="input">Input to parse</param>
		/// <returns>Command retrieved from the parsed input</returns>
		private string getCommand(string input) {
			string[] commands = input.Split(ScriptManager.COMMAND_IDENTIFIER);
			string command = commands[1];
			// figure out if we have parameters
			if (input.Contains('(')) {
				// clean up our command
				command = command.Substring(0, command.IndexOf('('));//remove the leading brace
			}
			return command;
		}

		/// <summary>
		/// Retrieves the object parameters for a method
		/// </summary>
		/// <param name="method">Method we are retrieving the parameters for</param>
		/// <param name="input">Console input to parse</param>
		/// <returns>object[] of the parameters</returns>
		private object[] getParameters(MethodInfo method, string input) {
			object[] parameters = null;
			// determine if we have parameters
			if (input.Contains('(')) {
				// we do so figure out what they are now
				List<string> parms = new List<string>();
				ParameterInfo[] paramInfos = method.GetParameters();
				if (paramInfos != null && paramInfos.Length >= 1) {
					// Thanks to WolfgangKluge at MSDN for this regex pattern (http://social.msdn.microsoft.com/Forums/en/regexp/thread/bfa96ae8-6df6-4308-811e-29fc25156c4d)
					string pattern = @"^(?<member>(?>[^(]+))\(
					(?:
						(?<parameter>
							(?:
								(?>[^,()""']+)|
								""(?>[^\\""]+|\\"")*""|
								@""(?>[^""]+|"""")*""|
								'(?:[^']|\\')'|
								\(
									(?:
										(?>[^()""']+)|
										""(?>[^\\""]+|\\"")*""|
										@""(?>[^""]+|"""")*""|
										'(?:[^']|\\')'|
										(?<nest>\()|
										(?<-nest>\))
									)*
									(?(nest)(?!))
								\)
							)+
						)
						\s*
						(?(?=,),\s*|(?=\)))
					)+
					\)
					";
					MatchCollection matchCollection = Regex.Matches(input, pattern, RegexOptions.IgnorePatternWhitespace);
					if (matchCollection == null || matchCollection.Count == 0) {
						throw new ScriptException("Malformed method call, ensure every opening brace has a closing brace");
					}
					GroupCollection groups = null;
					CaptureCollection captures = null;
					bool foundMethodStatement;
					foreach (Match match in matchCollection) {
						groups = match.Groups;
						if (groups != null) {
							foundMethodStatement = false;
							foreach (Group group in groups) {
								captures = group.Captures;
								if (captures != null) {
									foreach (Capture capture in captures) {
										if (foundMethodStatement) {
											parms.Add(capture.Value);
										} else {
											if (capture.Value.EndsWith(method.Name)) {
												foundMethodStatement = true;
											}
										}
									}
								}
							}
						}
					}
					parameters = new object[parms.Count];
					string value = null;
					for (int i = 0; i < parms.Count; i++) {
						Type type = paramInfos[i].ParameterType;
						value = parms[i];
						value = ScriptUtils.scrubStringParameter(value, type);
						parameters[i] = ObjectTranslator.translate(type, value);
					}
				}
			}
			return parameters;
		}

		/// <summary>
		/// Lists the script accessible methods
		/// </summary>
		public void listMethods() {
			StringBuilder stringBuilder = null;
			foreach (MethodInfo method in this.methods) {
				stringBuilder = new StringBuilder();
				stringBuilder.Append(method.Name);
				// retrieve the methods parameter information to add to the output
				ScriptUtils.getMethodParameterInfo(method.GetParameters(), ref stringBuilder);
				Console.WriteLine(stringBuilder.ToString());
			}
		}

		/// <summary>
		/// Lists the script accessible properties
		/// </summary>
		public void listProperties() {
			StringBuilder stringBuilder = null;
			foreach (PropertyInfo property in this.properties) {
				stringBuilder = new StringBuilder();
				stringBuilder.Append(property.Name);
				// retrieve the properties parameter information to add to the output
				if (property.GetSetMethod() != null) {
					ScriptUtils.getPropertyParameterInfo(property.GetSetMethod().GetParameters(), ref stringBuilder);
				}
				Console.WriteLine(stringBuilder.ToString());
			}
		}

		/// <summary>
		/// Displays help information for the registered object such as script accessible methods and properties
		/// </summary>
		public void help() {
			Console.WriteLine("\nMethods:");
			listMethods();
			Console.WriteLine("\nProperties:");
			listProperties();
		}

		/// <summary>
		/// Runs a command against the registered object
		/// </summary>
		/// <param name="command">Command to execute</param>
		/// <returns>Object based on a properties return type</returns>
		public object runCommand(string command) {
			object result = null;
			if (!runMethod(command)) {
				if (!runProperty(command, out result)) {
					// if we did not find a method or property throw an exception
					throw new ScriptException("Failed to find anything to match your input. Type \"OBJECT.help\" for a list of script accessible elements");
				}
			}
			return result;
		}
		#endregion Support methods
	}
}
