using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace GWNorthEngine.Model.Params {
	/// <summary>
	/// Params object containing the data required to build a TexturedButton object
	/// </summary>
	public class TexturedButtonParams : BaseButtonParams {
		#region Class properties
		/// <summary>
		/// Gets or sets the regular texture for the button
		/// </summary>
		public Texture2D RegularTexture { get; set; }
		/// <summary>
		/// Gets or sets the buttons mouse over texture
		/// </summary>
		public Texture2D MouseOverTexture { get; set; }
		/// <summary>
		/// Gets or sets the Colour in which the button is to be rendered in
		/// </summary>
		public Color LightColour { get; set; }
		#endregion Class properties

		#region Constructor
		/// <summary>
		/// Initializes a button with the default values set below
		/// 
		/// LightColour:		Color.White;
		/// </summary>
		public TexturedButtonParams() {
			this.LightColour = Color.White;
		}
		#endregion Constructor
	}
}
