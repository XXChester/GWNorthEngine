using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GWNorthEngine.Utils {
	/// <summary>
	/// Helpful methods for performing operations on enums
	/// </summary>
	public class EnumUtils {
		/// <summary>
		/// Gets a Enum value based on the int provided
		/// </summary>
		/// <typeparam name="T">Type of enum to lookup</typeparam>
		/// <param name="number">Number we are looking the value up for</param>
		/// <returns>Enum representation of the number</returns>
		public static T numberToEnum<T>(int number) {
			return (T)Enum.ToObject(typeof(T), number);
		}

		/// <summary>
		/// Inverses the value of the passed in enum and returns the enum version of its result
		/// </summary>
		/// <typeparam name="T">Enum type</typeparam>
		/// <param name="value">Value to invert</param>
		/// <returns>Inverted version of the enum value passed in</returns>
		public static T inverseValue<T>(T value) {
			Object boxedVersion = value;// Cannot just cast a enum to Enum or int...we have to box it into an Object first
			int outNum = -(int)boxedVersion;
			return numberToEnum<T>(outNum);
		}
	}
}
