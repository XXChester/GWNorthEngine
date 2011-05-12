using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using GWNorthEngine.Model.Params;
namespace GWNorthEngine.Model {
	/// <summary>
	/// Models the basis of every particle
	/// </summary>
	public class BaseParticle2D {
		#region Class variables
		/// <summary>
		/// Time the particle is alive
		/// </summary>
		protected float timeAlive;
		/// <summary>
		/// Time the particle dies at
		/// </summary>
		protected float timeToLive;
		/// <summary>
		/// Position of the particle
		/// </summary>
		protected Vector2 position;
		/// <summary>
		/// Scale of the particle
		/// </summary>
		protected Vector2 scale;
		/// <summary>
		/// Sprites origin used for rotation
		/// </summary>
		protected Vector2 origin;
		/// <summary>
		/// Original position of the particle
		/// </summary>
		protected Vector2 originalPosition;
		/// <summary>
		/// Acceleration of the particle
		/// </summary>
		protected Vector2 acceleration;
		/// <summary>
		/// Direction the particle is headed in
		/// </summary>
		protected Vector2 direction;
		/// <summary>
		/// Colour to render the particle in
		/// </summary>
		protected Color lightColour;
		/// <summary>
		/// Layer the particle is to be rendered at
		/// </summary>
		protected float layer;
		/// <summary>
		/// Texture of the particle
		/// </summary>
		protected Texture2D texture;
		#endregion Class variables

		#region Class propeties
		/// <summary>
		/// Gets or sets the time the particle is alive
		/// </summary>
		public float TimeAlive { get { return this.timeAlive; } set { this.timeAlive = value; } }
		/// <summary>
		/// Gets or sets the maximum time the particle can live for
		/// </summary>
		public float TimeToLive { get { return this.timeToLive; } set { this.timeToLive = value; } }
		/// <summary>
		/// Gets or sets the original position of the particle
		/// </summary>
		public Vector2 OriginalPosition { get { return this.originalPosition; } set { this.originalPosition = value; } }
		/// <summary>
		/// Gets or sets the acceleration of the particle
		/// </summary>
		public Vector2 Acceleration { get { return this.acceleration; } set { this.acceleration = value; } }
		/// <summary>
		/// Gets or sets the directional value of the particle
		/// </summary>
		public Vector2 Direction { get { return this.direction; } set { this.direction = value; } }
		/// <summary>
		/// Gets or sets the position of the sprite
		/// </summary>
		public virtual Vector2 Position { get { return this.position; } set { this.position = value; } }
		/// <summary>
		/// Gets or sets the scale of the sprite
		/// </summary>
		public virtual Vector2 Scale { get { return this.scale; } set { this.scale = value; } }
		/// <summary>
		/// Gets or sets the Colour in which the sprite is to be rendered in
		/// </summary>
		public virtual Color LightColour { get { return this.lightColour; } set { this.lightColour = value; } }
		/// <summary>
		/// Gets or sets the texture for the particle
		/// </summary>
		public Texture2D Texture { get { return this.texture; } set { this.texture = value; } }
		#endregion Class properties

		#region Constructor
		/// <summary>
		/// Builds the base particle
		/// </summary>
		/// <param name="parms">BaseParticle2DParams object containing the data required to build the particle</param>
		public BaseParticle2D(BaseParticle2DParams parms) {
			this.timeAlive = 0f;
			this.timeToLive = parms.TimeToLive;
			this.position = parms.Position;
			this.originalPosition = parms.Position;
			this.direction = parms.Direction;
			this.scale = parms.Scale;
			this.origin = parms.Origin;
			this.acceleration = parms.Acceleration;
			this.layer = parms.Layer;
			this.lightColour = parms.LightColour;
			this.texture = parms.Texture;
		}
		#endregion Constructor

		#region Initialization

		#endregion Initialization

		#region Support methods
		/// <summary>
		/// Fades the particle out over its life time to eventually invisible
		/// </summary>
		public virtual void fadeOutAsLifeProgresses() {
			float alpha = 1.0f - (this.timeAlive / this.timeToLive);
			this.lightColour = new Color(new Vector4(alpha, alpha, alpha, alpha));
		}

		/// <summary>
		/// Updates the particle. Be default it just updates the particles life
		/// </summary>
		/// <param name="elapsed">Time since the last time the method was called</param>
		public virtual void update(float elapsed) {
			this.timeAlive += elapsed;
			float relativeAge = this.timeAlive / this.timeToLive;
			this.position = .5f * this.acceleration * relativeAge * relativeAge + this.direction * relativeAge + this.originalPosition;
		}

		/// <summary>
		/// Renders the particle to screen
		/// </summary>
		/// <param name="spriteBatch">SpriteBatch object used to render the particle</param>
		public virtual void render(SpriteBatch spriteBatch) {
			spriteBatch.Draw(this.texture, this.position, null, this.lightColour, 0f, this.origin, this.scale, SpriteEffects.None, this.layer);
		}
		#endregion Support methods
	}
}
