using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace GWNorthEngine.GUI.Params {
	/// <summary>
	/// Models the data required to build a CheckBox
	/// </summary>
	public class CheckBoxParams : BaseGUIElementParams {
		#region Class properties
		/// <summary>
		/// Text to appear next to the CheckBox
		/// </summary>
		public string Text { get; set; }
		/// <summary>
		/// Font to write the text in
		/// </summary>
		public SpriteFont Font { get; set; }
		/// <summary>
		/// Whether or not the initial state of the CheckBox is checked or unchecked
		/// </summary>
		public bool Checked { get; set; }
		#endregion Class properties
	}
}
