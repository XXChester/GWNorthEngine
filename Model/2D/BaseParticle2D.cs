using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using GWNorthEngine.Model.Params;
using GWNorthEngine.Utils;
namespace GWNorthEngine.Model {
	/// <summary>
	/// Models the basis of every particle
	/// </summary>
	public class BaseParticle2D : Base2DSpriteDrawable {
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
		/// Acceleration of the particle
		/// </summary>
		protected Vector2 acceleration;
		/// <summary>
		/// Direction the particle is headed in
		/// </summary>
		protected Vector2 direction;
		/// <summary>
		/// Texture of the particle
		/// </summary>
		protected Texture2D texture;
		/// <summary>
		/// Original position of the particle
		/// </summary>
		protected readonly Vector2 originalPosition;
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
		/// Gets or sets the acceleration of the particle
		/// </summary>
		public Vector2 Acceleration { get { return this.acceleration; } set { this.acceleration = value; } }
		/// <summary>
		/// Gets or sets the directional value of the particle
		/// </summary>
		public Vector2 Direction { get { return this.direction; } set { this.direction = value; } }
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
		public BaseParticle2D(BaseParticle2DParams parms)
			:base(parms){
			this.timeAlive = 0f;
			this.timeToLive = parms.TimeToLive;
			this.originalPosition = parms.Position;
			this.direction = parms.Direction;
			this.acceleration = parms.Acceleration;
			this.texture = parms.Texture;
			base.setRenderingRectByTexture(this.texture);
		}
		#endregion Constructor

		#region Initialization

		#endregion Initialization

		#region Support methods
		/// <summary>
		/// Fades the particle out over its life time to eventually invisible
		/// </summary>
		public virtual void fadeOutAsLifeProgresses() {
			this.lightColour = TransitionUtils.fadeOut(base.originalLightColour, this.timeToLive, this.timeAlive);
		}

		/// <summary>
		/// Updates the particle. Be default it just updates the particles life
		/// </summary>
		/// <param name="elapsed">Time since the last time the method was called</param>
		public override void update(float elapsed) {
			this.timeAlive += elapsed;
		}

		/// <summary>
		/// Renders the particle to screen
		/// </summary>
		/// <param name="spriteBatch">SpriteBatch object used to render the particle</param>
		public override void render(SpriteBatch spriteBatch) {
			spriteBatch.Draw(this.texture, base.position, base.renderingRectangle, base.lightColour, base.rotation, base.origin, base.scale, base.spriteEffect, base.layer);
		}
		#endregion Support methods
	}
}
