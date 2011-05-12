using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using Microsoft.Xna.Framework;
using GWNorthEngine.Scripting.Model;
namespace GWNorthEngine.Scripting.Utils {
	/// <summary>
	/// Translates a string to the specified object
	/// </summary>
	internal class ObjectTranslator {
		/// <summary>
		/// Translates a specific value to the specified data type
		/// </summary>
		/// <param name="translationType">type to translate to</param>
		/// <param name="value">string version of the data</param>
		/// <returns>Object translated to</returns>
		public static object translate(Type translationType, string value) {
			object translatedObjects = null;
			if (translationType == typeof(string)) {
				// strings need to be converted to char arrays for some reason
				translatedObjects = value;
			} else {
				// This works for most types, it will look through its constructors etc to find the best suited to create the object
				List<object> parms = new List<object>();
				ConstructorInfo[] constructorInfos = translationType.GetConstructors();
				bool foundMethod;
				string[] componenets = value.Split(',');
				if (constructorInfos != null && constructorInfos.Length > 1) {
					MethodInfo[] constructionMethods = null;
					ParameterInfo[] constructorParamInfos = null;
					object[] constructorsParamTypeValues = null;
					foreach (ConstructorInfo constructorInfo in constructorInfos) {
						if (constructorInfo != null) {
							constructorParamInfos = constructorInfo.GetParameters();
							if (constructorParamInfos != null && constructorInfo.GetParameters().Length == componenets.Length) {// does this constructor match the amount of data we have
								constructorsParamTypeValues = new object[componenets.Length];
								for (int j = 0; j < constructorParamInfos.Length; j++) {
									constructionMethods = constructorParamInfos[j].ParameterType.GetMethods();
									foundMethod = false;
									foreach (MethodInfo method in constructionMethods) {
										if (method.Name == "Parse") {
											foreach (ParameterInfo paramInfo in method.GetParameters()) {
												if (paramInfo.ParameterType == typeof(string)) {
													constructorsParamTypeValues[j] =
														method.Invoke(paramInfo.ParameterType, new object[] { ScriptUtils.scrubStringParameter(componenets[j], translationType) });
													foundMethod = true;
													break;
												}
											}
											if (foundMethod) {
												break;
											}
										}
									}
									/* //Testing work with creating objects before finding the above solution, keep this here incase we need to go back to it
									constructorsParamTypeValues = translate(paramType, componenets[j].Replace(",", ""));
									Assembly assembly = paramType.Assembly;
									temp = assembly.CreateInstance(paramType.FullName);
									temp = Activator.CreateInstance(constructorsParamTypeValues[0].GetType(), constructorsParamTypeValues);*/
								}
								translatedObjects = constructorInfo.Invoke(constructorsParamTypeValues);
								break;
							}
						}
					}
				} else {
					// no constructors present for this type so try to directly parse it
					MethodInfo[] typesMethods = translationType.GetMethods();
					foundMethod = false;
					foreach (MethodInfo method in typesMethods) {
						if (method.Name == "Parse") {
							foreach (ParameterInfo paramInfo in method.GetParameters()) {
								if (paramInfo.ParameterType == typeof(string)) {
									translatedObjects =
										method.Invoke(paramInfo.ParameterType, new object[] { ScriptUtils.scrubStringParameter(componenets[0], translationType) });
									foundMethod = true;
									break;
								}
							}
							if (foundMethod) {
								break;
							}
						}
					}
				}
			}
			return translatedObjects;
		}
	}
}
