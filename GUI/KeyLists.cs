using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework.Input;

namespace GWNorthEngine.GUI {
	/// <summary>
	/// Constant representations of List[Keys]
	/// </summary>
	public static class KeyLists {
		/// <summary>
		/// Number keys 0-9
		/// </summary>
		public static readonly List<Keys> NUMBER_KEYS = new List<Keys> {
				Keys.D1, Keys.D2,Keys.D3,Keys.D4,Keys.D5,Keys.D6,Keys.D7,Keys.D8,Keys.D9,Keys.D0,
			};

		/// <summary>
		/// Letters a-z
		/// </summary>
		public static readonly List<Keys> LETTERS_KEYS = new List<Keys> {
			Keys.Q,Keys.W,Keys.E,Keys.R,Keys.T,Keys.Y,Keys.U,Keys.I,Keys.O,Keys.P,
				Keys.A,Keys.S,Keys.D,Keys.F,Keys.G,Keys.H,Keys.J,Keys.K,Keys.L,
				Keys.Z,Keys.X,Keys.C,Keys.V,Keys.B,Keys.N,Keys.M,
		};

		/// <summary>
		/// Numpad keys Numpad0-Numpad9
		/// </summary>
		public static readonly List<Keys> NUMPAD_KEYS = new List<Keys> {
			Keys.NumPad1, Keys.NumPad2,Keys.NumPad3,Keys.NumPad4,Keys.NumPad5,Keys.NumPad6,Keys.NumPad7,Keys.NumPad8,Keys.NumPad9,Keys.NumPad0,
		};

		/// <summary>
		/// Directional Keys (left,right,up,donw)
		/// </summary>
		public static readonly List<Keys> DIRECTIONAL_KEYS = new List<Keys> {
			Keys.Left, Keys.Right, Keys.Up, Keys.Down
		};

		/// <summary>
		/// Translations for keys
		/// </summary>
		public static readonly List<KeyValuePair<Keys, string>> TRANSLATIONS = new List<KeyValuePair<Keys, string>>() {
			new KeyValuePair<Keys, string>(Keys.D1, "1"),
			new KeyValuePair<Keys, string>(Keys.D2, "2"),
			new KeyValuePair<Keys, string>(Keys.D3, "3"),
			new KeyValuePair<Keys, string>(Keys.D4, "4"),
			new KeyValuePair<Keys, string>(Keys.D5, "5"),
			new KeyValuePair<Keys, string>(Keys.D6, "6"),
			new KeyValuePair<Keys, string>(Keys.D7, "7"),
			new KeyValuePair<Keys, string>(Keys.D8, "8"),
			new KeyValuePair<Keys, string>(Keys.D9, "9"),
			new KeyValuePair<Keys, string>(Keys.D0, "0"),
		};

		/// <summary>
		/// Translates a Key to a user friendly version if there is one
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		public static string translate(Keys key) {
			string translation = key.ToString();
			KeyValuePair<Keys, string> defaultKVP = default(KeyValuePair<Keys, string>);
			KeyValuePair<Keys, string> result = TRANSLATIONS.SingleOrDefault(s => s.Key == key);
			if (!result.Equals(defaultKVP)) {
				translation = result.Value;
			}
			return translation;
		}
	}
}
