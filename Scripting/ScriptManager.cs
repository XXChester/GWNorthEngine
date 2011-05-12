using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using Microsoft.Xna.Framework;
using GWNorthEngine.Utils;
using GWNorthEngine.Scripting.Utils;
using GWNorthEngine.Scripting.Model;
namespace GWNorthEngine.Scripting {
	/// <summary>
	/// Singleton class that handles the recieving of input and disbatching it to the proper registered objects
	/// </summary>
	public class ScriptManager {
		// singleton instance variable
		private static ScriptManager instance = new ScriptManager();
		private List<RegisteredObject> registeredObjects = new List<RegisteredObject>();
		private RegisteredObject lockedOnObject;
		private bool lockOnEnabled;
		private string logFile;
		private const int LOCK_ON_PADDING = 16;
		private const string COMMAND_HELP = "help";
		private const string COMMAND_GET_VALUE = "getValue";
		private const string COMMAND_LOG = "log";
		private const string COMMAND_LOCK_ON = "lockOn";
		private readonly string[] HIGH_LEVEL_COMMANDS = new string[] { COMMAND_HELP, COMMAND_GET_VALUE, COMMAND_LOG, COMMAND_LOCK_ON };
		/// <summary>
		/// Character used to describe what identifies a registered objects method such as test.help
		/// </summary>
		internal const char COMMAND_IDENTIFIER = '.';

		/// <summary>
		///  File that the script console will log too undet the "Log" command
		/// </summary>
		public string LogFile { get { return this.logFile; } set { this.logFile = value; } }
		/// <summary>
		/// Gets whether or not locking on is enabled
		/// </summary>
		public bool LockOnEnabled { get { return this.lockOnEnabled; } }

		/// <summary>
		/// Default constructor which just alpha sorts the high level commands
		/// </summary>
		public ScriptManager() {
			Array.Sort(HIGH_LEVEL_COMMANDS, AlphaComparer.getInstance());
		}

		/// <summary>
		/// Retrieves the singleton instance variable
		/// </summary>
		/// <returns>Singleton of ScriptManager</returns>
		public static ScriptManager getInstance() {
			return instance;
		}

		/// <summary>
		/// Displays general help such as the command identifier, high level commands, and registered objects
		/// </summary>
		public void help() {
			Console.WriteLine("\nObject invoker is: \"" + COMMAND_IDENTIFIER + "\"");
			Console.WriteLine("Log file is: " + this.logFile);
			Console.WriteLine("Lock on to objects enabled: " + this.lockOnEnabled);
			Console.WriteLine("High level commands:");
			StringBuilder stringBuilder = null;
			MethodInfo method = null;
			foreach (string command in HIGH_LEVEL_COMMANDS) {
				stringBuilder = new StringBuilder(command);
				method = this.GetType().GetMethod(command);
				ScriptUtils.getMethodParameterInfo(method.GetParameters(), ref stringBuilder);
				Console.WriteLine(stringBuilder.ToString());
			}
			Console.WriteLine("Registered Objects: ");
			foreach (RegisteredObject registeredObject in this.registeredObjects) {
				Console.WriteLine(registeredObject.ReferenceName);
			}
		}

		/// <summary>
		/// Logs a message from the console to the specified log file
		/// </summary>
		/// <param name="message">Message to log to the log file</param>
		public void log(string message) {
			if (this.logFile != null) {
				StreamWriter writer = null;
				try {
					writer = new StreamWriter(Environment.CurrentDirectory + @"..\..\..\..\" + this.logFile, true);
					writer.WriteLine(message);
					Console.WriteLine("Wrote \"{0}\" to the log file", message);
				} catch (IOException ex) {
					throw new ScriptException("Failed to open/write the log file. " + ex.Message);
				} finally {
					if (writer != null) {
						writer.Close();
						writer.Dispose();
					}
				}
			} else {
				throw new ScriptException("No log file specified");
			}
		}

		/// <summary>
		/// Enables the ability to lock onto an object and move it around the screen with the mouse for map building purposes
		/// </summary>
		/// <param name="input">String representation of a boolean value</param>
		public void lockOn(string input) {
			if (input.ToLower() == "true") {
				this.lockOnEnabled = true;
			} else if (input.ToLower() == "false") {
				this.lockOnEnabled = false;
			} else {
				object output;
				if (findObject(input, out output)) {
					this.lockedOnObject = (RegisteredObject)output;
					this.lockOnEnabled = true;
					Console.WriteLine("Locked onto " + this.lockedOnObject.ReferenceName);
				} else {
					throw new ScriptException("Invalid input, please ensure it is a boolean expression or an objects reference...your input: " + input);
				}
			}
			Console.WriteLine("Lock on enabled: " + this.lockOnEnabled);
		}

		/// <summary>
		/// Moves the locked onto object around the screen based on the mouses position
		/// </summary>
		/// <param name="mousePosition">Vector2 of the mouse position</param>
		public void handleMouseMovement(Vector2 mousePosition) {
			if (this.lockedOnObject != null) {
				this.lockedOnObject.runCommand(this.lockedOnObject.ReferenceName + ".Position = " + "(" + mousePosition.X + "," + mousePosition.Y + ")");
			}
		}
		
		/// <summary>
		/// Handles the locking on and off of an object
		/// </summary>
		/// <param name="mousePosition">Vector2 of the mouse position used to find the object</param>
		public void handleMouseClick(Vector2 mousePosition) {
			if (this.lockedOnObject == null) {
				//find our registered object by the mouse position and padded a bit
				PropertyInfo property;
				MethodInfo method;
				Vector2 registeredPosition;
				Rectangle paddedInput = new Rectangle
					((int)mousePosition.X - LOCK_ON_PADDING, (int)mousePosition.Y - LOCK_ON_PADDING, (int)mousePosition.X + LOCK_ON_PADDING, (int)mousePosition.Y + LOCK_ON_PADDING);
				foreach (RegisteredObject registeredObject in this.registeredObjects) {
					property = registeredObject.Reference.GetType().GetProperty("Position");
					if (property != null) {
						method = property.GetGetMethod();
						if (method != null) {
							registeredPosition = (Vector2)method.Invoke(registeredObject.Reference, null);
							if (PickingUtils.pickRectangle(registeredPosition, paddedInput)) {
								this.lockedOnObject = registeredObject;
								Console.WriteLine("Locked onto " + this.lockedOnObject.ReferenceName);
								break;
							}
						}
					}
				}
			} else {
				Console.WriteLine("Releasing lock from " + this.lockedOnObject.ReferenceName);
				this.lockedOnObject = null;
			}
		}

		/// <summary>
		/// Outputs the toString() of an object
		/// </summary>
		/// <param name="value">Object to execute the toString against</param>
		public void getValue(object value) {
			Console.WriteLine(value.ToString());
		}

		/// <summary>
		/// Finds the registered object and the method/property to execute
		/// </summary>
		/// <param name="input">Input from the console</param>
		/// <param name="output">Outputs the resulting object if a method/property returns one</param>
		/// <returns>true if we found a registered object and a method within that object</returns>
		private bool findAndExecute(string input, out object output) {
			bool foundObject = false;
			output = null;
			if (findObject(input.Split(COMMAND_IDENTIFIER)[0], out output)) {
				foundObject = true;
				output = ((RegisteredObject)output).runCommand(input);
			}
			return foundObject;
		}

		/// <summary>
		/// Finds a registered object based on the reference name provided
		/// </summary>
		/// <param name="referenceName">Objects reference name to look up</param>
		/// <param name="output">Outputs the found RegisteredObject</param>
		/// <returns>true if we found a registered object, otherwise false</returns>
		private bool findObject(string referenceName, out object output) {
			bool foundObject = false;
			output = null;
			RegisteredObject registeredObject;
			for (int i = 0; i < this.registeredObjects.Count; i++) {
				registeredObject = this.registeredObjects[i];
				if (registeredObject != null && registeredObject.ReferenceName.ToLower() == referenceName.ToLower()) {
					output = registeredObject;
					foundObject = true;
					break;
				}
			}
			return foundObject;
		}

		/// <summary>
		/// Registers an object to be accessible via script from the console
		/// </summary>
		/// <param name="itemToRegister">Object to register available</param>
		/// <param name="referenceName">Name you want to use to access this object</param>
		public void registerObject(Object itemToRegister, string referenceName) {
			Type objectType = itemToRegister.GetType();
			this.registeredObjects.Add(new RegisteredObject(ref itemToRegister, referenceName, objectType.GetMethods(), objectType.GetProperties()));
			// alpha sort the registered objects
			this.registeredObjects.Sort(AlphaComparer.getInstance());
		}

		/// <summary>
		/// Handles the input received from the console window
		/// </summary>
		/// <param name="input">Input read in from the console window</param>
		public void handleInput(string input) {
			object output = null;
			try {
				if (input.ToLower() == COMMAND_HELP.ToLower()) {
					help();
				} else if (input.ToLower().StartsWith(COMMAND_LOG.ToLower())) {
					string message = input.Replace(COMMAND_LOG, "").TrimStart('(').TrimEnd(')');
					// check if we are doing a getValue within the log message
					if (input.ToLower().Contains(COMMAND_GET_VALUE.ToLower())) {
						// we need to get the value from getValue first
						string subInput = input.Substring(input.LastIndexOf('(')).Replace("(", "").Replace(")", "");//strip text leading up to the opening brace and the braces to get our sub input
						if (findAndExecute(subInput, out output) && output != null) {
							// rip out the get value parts so we can append the result to the rest of the message
							message = message.Substring(0, message.IndexOf("getValue(")) + output.ToString(); ;
						} else {
							throw new ScriptException("Failed to find value to look up via getValue(OBJECT.Property)");
						}
					}
					log(message);
				} else if (input.ToLower().StartsWith(COMMAND_GET_VALUE.ToLower())) {
					// we need to find the property in the argument so send that piece back in
					string subInput = input.Substring(input.IndexOf('(')).Replace("(", "").Replace(")", "");//strip text leading up to the opening brace and the braces to get our sub input
					if (findAndExecute(subInput, out output) && output != null) {
						getValue(output);
					} else {
						throw new ScriptException("Failed to find value to look up via getValue(OBJECT.Property)");
					}
				} else if (input.ToLower().StartsWith(COMMAND_LOCK_ON.ToLower())) {
					string value = input.ToUpper().Replace(COMMAND_LOCK_ON.ToUpper(), "").TrimStart('(').TrimEnd(')');
					lockOn(value);
				} else if (input.Contains(COMMAND_IDENTIFIER)) {
					if (!findAndExecute(input, out output)) {
						throw new ScriptException("Failed to find anything to match your input. Type \"help\" for a list of registered objects");
					}
				} else {
					throw new ScriptException("Failed to find anything to match your input. Type \"help\" for a list of registered objects");
				}
			} catch (ScriptException ex) {
				Console.WriteLine(ex.Message);
			} catch (Exception ex) {
				Console.WriteLine("Blanket exception catch, message: " + ex.Message);
			}
		}
	}
}
