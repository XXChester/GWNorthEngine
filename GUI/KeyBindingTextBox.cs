using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

using GWNorthEngine.Engine;
using GWNorthEngine.Engine.Params;
using GWNorthEngine.Model;
using GWNorthEngine.Model.Params;
using GWNorthEngine.Logic;
using GWNorthEngine.Logic.Params;
using GWNorthEngine.Input;
using GWNorthEngine.Utils;
using GWNorthEngine.Scripting;
using GWNorthEngine.GUI.Params;


namespace GWNorthEngine.GUI {
	/// <summary>
	/// Models a KeyBindingTextBox
	/// </summary>
	public class KeyBindingTextBox : TextBox {
		#region Class variables
		private string previousText;
		#endregion Class variables

		#region Class propeties
		/// <summary>
		/// Override HasFocus
		/// </summary>
		public override bool HasFocus {
			get {
				return base.HasFocus;
			}
			set {
				base.HasFocus = value;
				if (value) {
					base.Text = "";
				}
			}
		}
		/// <summary>
		/// Key that we are bound to
		/// </summary>
		public Keys BoundTo { get; set; }
		#endregion Class properties

		#region Constructor
		/// <summary>
		/// Constructs a KeyBindingTextBox
		/// </summary>
		/// <param name="parms">KeyBindingTextBoxParams object containing the data required to construct the object</param>
		public KeyBindingTextBox(KeyBindingTextBoxParams parms)
			: base(parms) {
			this.previousText = parms.Text;
			this.BoundTo = parms.BoundTo;
		}
		#endregion Constructor

		#region Support methods
		/// <summary>
		/// handles the text update
		/// </summary>
		protected override void handleTextUpdate() {
			Keys[] pressedKeys = InputManager.getInstance().getNewKeys();
			if (pressedKeys != null && pressedKeys.Length > 0) {
				string newText = null;
				foreach (Keys key in pressedKeys) {
					if (Keys.Escape == key) {
						base.text = this.previousText;
						base.HasFocus = false;
						break;
					} else if (base.text.Length < base.MAX_LENGTH) {
						if (Keys.Enter != key) {
							// do we have a translation?
							newText = KeyLists.translate(key);
							this.BoundTo = key;
							this.previousText = newText;
							base.text += newText;
							base.HasFocus = false;
							break;
						}
					}
				}
				base.text2D.WrittenText = base.text;
			}
		}

		/// <summary>
		/// Checks if the textbox has gained focus
		/// </summary>
		protected override void checkFocus() {
			if (InputManager.getInstance().wasLeftButtonPressed()) {
				bool hadFocus = this.HasFocus;
				this.HasFocus = isActorOver(InputManager.getInstance().MousePosition);
				if (hadFocus && !this.HasFocus) {
					this.Text = this.previousText;
				}
			}
		}
		#endregion Support methods
	}
}
