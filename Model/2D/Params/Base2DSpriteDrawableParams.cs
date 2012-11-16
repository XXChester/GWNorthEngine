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
using GWNorthEngine.Model.Effects;

namespace GWNorthEngine.Model.Params {
	/// <summary>
	/// Object containing the required data for the basis to build any SpriteBatch drawable object
	/// </summary>
	public class Base2DSpriteDrawableParams {
		#region Class variables
		private Vector2 position;
		private Vector2 origin;
		private Vector2 scale;
		private float rotation;
		private float layer;
		private Color lightColour;
		#endregion Class variables

		#region Class propeties
		/// <summary>
		/// Gets or sets the starting position of the sprite
		/// </summary>
		public Vector2 Position { get { return this.position; } set { this.position = value; } }
		/// <summary>
		/// Gets or sets the starting origin of the sprite
		/// </summary>
		public Vector2 Origin { get { return this.origin; } set { this.origin = value; } }
		/// <summary>
		/// Gets or Sets the startign scale of the sprite
		/// </summary>
		public Vector2 Scale { get { return this.scale; } set { this.scale = value; } }
		/// <summary>
		/// Gets or sets the starting rotation of the sprite
		/// </summary>
		public float Rotation { get { return this.rotation; } set { this.rotation = value; } }
		/// <summary>
		/// Gets or sets the starting layer to render the sprite at
		/// </summary>
		public float Layer { get { return this.layer; } set { this.layer = value; } }
		/// <summary>
		/// Gets or sets the starting colour in which to render the sprite in
		/// </summary>
		public Color LightColour { get { return this.lightColour; } set { this.lightColour = value; } }
		/// <summary>
		/// Gets or sets the SpriteEffect technique used for rendering the sprite
		/// </summary>
		public SpriteEffects SpriteEffect { get; set; }
		/// <summary>
		/// Gets or sets the Rectangle used to render portions of the Texture
		/// </summary>
		public Rectangle RenderingRectangle { get; set; }
		#endregion Class properties

		#region Constructor
		/// <summary>
		/// Defines a new Base2DSpriteDrawableParams object with the defaults set. The defaults are listed below;
		/// LightColour:	Colour.White
		/// Origin:			Vector2(0f,0f)
		/// Scale:			Vector2(1f,1f)
		/// Rotation:		0f
		/// Layer:			0f
		/// SpriteEffect:	None
		/// </summary>
		public Base2DSpriteDrawableParams() {
			// defaults
			this.lightColour = Color.White;
			this.origin = Vector2.Zero;
			this.scale = new Vector2(1f, 1f);
			this.rotation = 0f;
			this.layer = 0f;
			this.SpriteEffect = SpriteEffects.None;
		}
		#endregion Constructor
	}
}
