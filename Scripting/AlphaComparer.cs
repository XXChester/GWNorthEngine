using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using GWNorthEngine.Scripting.Model;
namespace GWNorthEngine.Scripting {
	internal class AlphaComparer : IComparer<object> {
		//singleton instance
		private static AlphaComparer instance = new AlphaComparer();

		public static AlphaComparer getInstance() {
			return instance;
		}

		public int Compare(object obj1, object obj2) {
			//NOTE: reflection types need to reference the getType().BaseType for some reason their type is RuntimeXXXInfo which you cannot typeof check
			string string1 = null;
			string string2 = null;
			if (obj1.GetType() == typeof(RegisteredObject)) {
				string1 = ((RegisteredObject)obj1).ReferenceName;
				string2 = ((RegisteredObject)obj2).ReferenceName;
			} else if (obj1.GetType() == typeof(string)) {
				string1 = (string)obj1;
				string2 = (string)obj2;
			} else if (obj1.GetType().BaseType == typeof(MethodInfo)) {
				string1 = ((MethodInfo)obj1).Name;
				string2 = ((MethodInfo)obj2).Name;
			} else if (obj1.GetType().BaseType == typeof(PropertyInfo)) {
				string1 = ((PropertyInfo)obj1).Name;
				string2 = ((PropertyInfo)obj2).Name;
			}

			return (string1.CompareTo(string2));
		}
	}
}
