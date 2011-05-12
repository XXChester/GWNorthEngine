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
namespace GWNorthEngine.Model.Params {
	/// <summary>
	/// Object containing the data required to build a 2D line
	/// </summary>
	public class Line2DParams : Base2DSpriteDrawableParams {
		#region Class variables
		private Vector2 startPosition;
		private Vector2 endPosition;
		private Texture2D texture;
		#endregion Class variables

		#region Class propeties
		/// <summary>
		/// Gets or sets the starting position of the line
		/// </summary>
		public Vector2 StartPosition { get { return this.startPosition; } set { this.startPosition = value; } }
		/// <summary>
		/// Gets or sets the ending position of the line
		/// </summary>
		public Vector2 EndPosition { get { return this.endPosition; } set { this.endPosition = value; } }
		/// <summary>
		/// Gets or sets the texture for the line
		/// </summary>
		public Texture2D Texture { get { return this.texture; } set { this.texture = value; } }
		#endregion Class properties
	}
}
