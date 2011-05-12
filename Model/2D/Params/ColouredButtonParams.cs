using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
namespace GWNorthEngine.Model.Params {
	/// <summary>
	/// Params object containing the data required to build a coloured button
	/// </summary>
	public class ColouredButtonParams : BaseButtonParams {
		#region Class properties
		/// <summary>
		/// Gets or sets the lines texture
		/// </summary>
		public Texture2D LinesTexture { get; set; }
		/// <summary>
		/// Gets or sets the regular light colour
		/// </summary>
		public Color RegularColour { get; set; }
		/// <summary>
		/// Gets or sets the mouse over light colour
		/// </summary>
		public Color MouseOverColour { get; set; }
		/// <summary>
		/// Gets or sets the SpriteFont object
		/// </summary>
		public SpriteFont Font { get; set; }
		/// <summary>
		/// Gets or sets the text to be written inside the button
		/// </summary>
		public string Text { get; set; }
		/// <summary>
		/// Gets or sets the Texts position
		/// </summary>
		public Vector2 TextsPosition { get; set; }
		#endregion Class properties
	}
}
