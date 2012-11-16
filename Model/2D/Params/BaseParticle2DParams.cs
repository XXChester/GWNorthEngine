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
namespace GWNorthEngine.Model.Params {
	/// <summary>
	/// Object containing the data required to build a base 2D particle
	/// </summary>
	public class BaseParticle2DParams : Base2DSpriteDrawableParams {
		#region Class variables
		private float timeToLive;
		private Vector2 acceleration;
		private Vector2 direction;
		private Texture2D texture;
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
		/// Gets or sets the texture for the particle
		/// </summary>
		public Texture2D Texture { get { return this.texture; } set { this.texture = value; } }
		#endregion Class properties
	}
}
