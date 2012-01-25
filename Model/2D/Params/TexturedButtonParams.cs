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
	public class TexturedButtonParams : StaticDrawable2DParams {
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
		/// Buttons ID used for determining if a button was clicked
		/// </summary>
		public int ID { get; set; }
		#endregion Class properties

		#region Constructor
		/// <summary>
		/// Initializes a button with the default values set below
		/// 
		/// LightColour:		Color.White;
		/// </summary>
		public TexturedButtonParams() {
			base.LightColour = Color.White;
		}
		#endregion Constructor
	}
}
