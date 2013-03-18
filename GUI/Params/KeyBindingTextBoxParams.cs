using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace GWNorthEngine.GUI.Params {
	/// <summary>
	/// Models the data required to build a key binding textbox
	/// </summary>
	public class KeyBindingTextBoxParams : TextBoxParams {
		#region Properties
		/// <summary>
		/// Key that we are bound to
		/// </summary>
		public Keys BoundTo { get; set; }
		#endregion Properties

		#region Constructor
		/// <summary>
		/// Constructs a default KeyBindingTextBoxParams object with the following values
		/// MaxLength:				1
		/// </summary>
		public KeyBindingTextBoxParams() {
			this.MaxLength = 1;
			this.Text = "";
		}
		#endregion Constructor
	}
}
