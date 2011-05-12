using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace GWNorthEngine.Scripting.Utils {
	/// <summary>
	/// Helpful methods for scripting
	/// </summary>
	internal class ScriptUtils {
		/// <summary>
		/// Retrieves the parameter information for a method used for displaying help
		/// </summary>
		/// <param name="paramInfos">ParameterInfo object array of the parameter for the methods</param>
		/// <param name="stringBuilder">StringBuilder object reference that we add to for output</param>
		public static  void getMethodParameterInfo(ParameterInfo[] paramInfos, ref StringBuilder stringBuilder) {
			ParameterInfo paramInfo;
			stringBuilder.Append("(");
			for (int i = 0; i < paramInfos.Length; i++) {
				paramInfo = paramInfos[i];
				if (paramInfo != null) {
					if (i != 0) {
						stringBuilder.Append(",");
					}
					// I prefer to output the parameters names which does 3 things.
					//1)I am more likely to recognize a parameter name than the fully qualified data type
					//2)Forces me to supply descriptive parameter names
					//3)Cleaner help method because there are no fully qualified data type strings
					stringBuilder.Append(paramInfo.Name);
				}
			}
			stringBuilder.Append(")");
		}

		/// <summary>
		/// Retrieves the parameter information for a property used for displaying help
		/// </summary>
		/// <param name="paramInfos">ParameterInfo object array of the parameter for the methods</param>
		/// <param name="stringBuilder">StringBuilder object reference that we add to for output</param>
		public static void getPropertyParameterInfo(ParameterInfo[] paramInfos, ref StringBuilder stringBuilder) {
			ParameterInfo paramInfo;
			for (int i = 0; i < paramInfos.Length; i++) {
				paramInfo = paramInfos[i];
				if (paramInfo != null) {
					stringBuilder.Append(" - DataType: " + paramInfo.ParameterType.Name);
				}
			}
		}

		/// <summary>
		/// Scrubs the excess crap off of a value such as the data type, and opening and closing braces
		/// </summary>
		/// <param name="value">Value to scrub</param>
		/// <param name="paramType">Type of the value</param>
		/// <returns>Clean parameter</returns>
		public static string scrubStringParameter(string value, Type paramType) {
			// clean up the value..it may contain the data type, brackets etc
			value = value.Replace(paramType.Name.ToString(), "").TrimStart('(').TrimEnd(')');
			return value;
		}
	}
}
