using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

namespace GWNorthEngine.GUI {
	/// <summary>
	/// Constants file for GUI elements
	/// </summary>
	public class Constants {
		/// <summary>
		/// Texture name that the GUI elements can be found in
		/// </summary>
		public static string GUI_FILE_NAME = "GUIElements";
		/// <summary>
		/// Space between GUI elements in the texture
		/// </summary>
		public static int SPACE_BETWEEN = 4;
		/// <summary>
		/// Location of the Unchecked check box
		/// </summary>
		public static Rectangle CHK_BOX_UNCHECKED = new Rectangle(SPACE_BETWEEN, SPACE_BETWEEN, 32, 32);
		/// <summary>
		/// Location of the Checked check box
		/// </summary>
		public static Rectangle CHK_BOX_CHECKED = new Rectangle(40, SPACE_BETWEEN, 32, 32);
		/// <summary>
		/// Location of the Slider's Ball
		/// </summary>
		public static Rectangle SLIDER_BALL = new Rectangle(75, SPACE_BETWEEN, 8, 32);
		/// <summary>
		/// Location of the Slider's Bar
		/// </summary>
		public static Rectangle SLIDER_BAR = new Rectangle(111, SPACE_BETWEEN, 32, 32);
		/// <summary>
		/// Location of the Textbox end bar
		/// </summary>
		public static Rectangle TXT_BOX_END = new Rectangle(147, SPACE_BETWEEN, 32, 32);
		/// <summary>
		/// Location of the Textox middle
		/// </summary>
		public static Rectangle TXT_BOX_MIDDLE = new Rectangle(184, SPACE_BETWEEN, 1, 32);
	}
}
