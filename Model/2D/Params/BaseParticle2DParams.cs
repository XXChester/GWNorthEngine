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
	/// Object containing the data required to build a base 2D particle
	/// </summary>
	public class BaseParticle2DParams {
		#region Class variables
		private float timeToLive;
		private float layer;
		private Vector2 acceleration;
		private Vector2 direction;
		private Vector2 origin;
		private Vector2 position;
		private Vector2 scale;
		private Texture2D texture;
		private Color lightColour;
		#endregion Class variables

		#region Class propeties
		/// <summary>
		/// Gets or sets the maximum time the particle can live for
		/// </summary>
		public float TimeToLive { get { return this.timeToLive; } set { this.timeToLive = value; } }
		/// <summary>
		/// Gets or sets the acceleration of the particle
		/// </summary>
		public Vector2 Acceleration { get { return this.acceleration; } set { this.acceleration = value; } }
		/// <summary>
		/// Gets or sets the directional value of the particle
		/// </summary>
		public Vector2 Direction { get { return this.direction; } set { this.direction = value; } }
		/// <summary>
		/// Gets or sets the starting origin of the particle
		/// </summary>
		public Vector2 Origin { get { return this.origin; } set { this.origin = value; } }
		/// <summary>
		/// Gets or sets the starting position of the sprite
		/// </summary>
		public Vector2 Position { get { return this.position; } set { this.position = value; } }
		/// <summary>
		/// Gets or Sets the startign scale of the sprite
		/// </summary>
		public Vector2 Scale { get { return this.scale; } set { this.scale = value; } }
		/// <summary>
		/// Gets or sets the starting layer to render the sprite at
		/// </summary>
		public float Layer { get { return this.layer; } set { this.layer = value; } }
		/// <summary>
		/// Gets or sets the starting colour in which to render the sprite in
		/// </summary>
		public Color LightColour { get { return this.lightColour; } set { this.lightColour = value; } }
		/// <summary>
		/// Gets or sets the texture for the particle
		/// </summary>
		public Texture2D Texture { get { return this.texture; } set { this.texture = value; } }
		#endregion Class properties

		#region Cosntructor
		/// <summary>
		/// Defines a new SpriteParams object with the defaults set. The defaults are listed below;
		/// LightColour:	Colour.White
		/// Scale:			Vector2(1f,1f)
		/// Layer:			0f
		/// </summary>
		public BaseParticle2DParams() {
			// defaults
			this.lightColour = Color.White;
			this.scale = new Vector2(1f, 1f);
			this.layer = 0f;
		}
		#endregion Constructor
	}
}
