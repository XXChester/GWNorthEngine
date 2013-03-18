using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace GWNorthEngine.GUI.Params {
	/// <summary>
	/// Models the data required to build a textbox
	/// </summary>
	public class TextBoxParams : BaseGUIElementParams {
		#region Class properties
		/// <summary>
		/// Text to set in the Textbox
		/// </summary>
		public string Text { get; set; }
		/// <summary>
		/// SpriteFont to use for the Textbox
		/// </summary>
		public SpriteFont Font { get; set; }
		/// <summary>
		/// Maximum length of the text
		/// </summary>
		public int MaxLength { get; set; }
		/// <summary>
		/// Size per character
		/// </summary>
		public float SizePerCharacter { get; set; }
		/// <summary>
		/// Scale of the text
		/// </summary>
		public Vector2 TextScale { get; set; }
		/// <summary>
		/// Colour of the text
		/// </summary>
		public Color TextColour { get; set; }
		#endregion Class properties

		#region Constructor
		/// <summary>
		/// Constructs a default TextBoxParms object with the following values
		/// MaxLength:				20
		/// SizePerCharacter:		11f
		/// TextScale:				1f,1f
		/// </summary>
		public TextBoxParams() {
			this.MaxLength = 20;
			this.SizePerCharacter = 11f;
			this.Text = "";
			this.TextScale = new Vector2(1f);
		}
		#endregion Constructor
	}
}
