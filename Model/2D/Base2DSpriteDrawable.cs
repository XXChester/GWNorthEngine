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
using GWNorthEngine.Model.Params;
using GWNorthEngine.Utils;
namespace GWNorthEngine.Model {
	/// <summary>
	/// Models the basis of every SpriteBatch drawable object
	/// </summary>
	public abstract class Base2DSpriteDrawable {
		#region Class variables
		/// <summary>
		/// Position of the sprite
		/// </summary>
		protected Vector2 position;
		/// <summary>
		/// Sprites origin used for rotation
		/// </summary>
		protected Vector2 origin;
		/// <summary>
		/// Scale of the sprite
		/// </summary>
		protected Vector2 scale;
		/// <summary>
		/// Rotation of the sprite
		/// </summary>
		protected float rotation;
		/// <summary>
		/// Layer the sprite is to be rendered at
		/// </summary>
		protected float layer;
		/// <summary>
		/// Colour to render the sprite in
		/// </summary>
		protected Color lightColour;
		/// <summary>
		/// SpriteEffects to render the sprite with
		/// </summary>
		protected SpriteEffects spriteEffect;
		/// <summary>
		/// Original colour the sprite was rendered in
		/// </summary>
		protected readonly Color originalLightColour;
		#endregion Class variables

		#region Class propeties
		/// <summary>
		/// Gets or sets the rotational value for the sprite
		/// </summary>
		public virtual float Rotation { get { return this.rotation; } set { this.rotation = value; } }
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
		#endregion Class properties

		#region Constructor
		/// <summary>
		/// Builds a basic SpriteBatch drawable item
		/// </summary>
		/// <param name="parms">BaseSpriteDrawableParams object</param>
		public Base2DSpriteDrawable(Base2DSpriteDrawableParams parms) {
			this.rotation = parms.Rotation;
			this.layer = parms.Layer;
			this.position = parms.Position;
			this.origin = parms.Origin;
			this.scale = parms.Scale;
			this.lightColour = parms.LightColour;
			this.originalLightColour = parms.LightColour;
			this.spriteEffect = parms.SpriteEffect;
		}
		#endregion Constructor

		#region Initialization

		#endregion Initialization

		#region Support method
		/// <summary>
		/// Scales the object over time
		/// </summary>
		/// <param name="scaleBy"></param>
		public virtual void scaleAsLifeProgresses(Vector2 scaleBy) {
			this.scale += scaleBy;
		}

		/// <summary>
		/// Rotates the object over time
		/// </summary>
		/// <param name="rotationSpeed"></param>
		public virtual void rotateAsLifeProgresses(float rotationSpeed) {
			this.rotation += rotationSpeed;
		}

		/// <summary>
		/// Abstract method that forces the children objects to implement update
		/// </summary>
		/// <param name="elapsed">Time elapsed sense the last call</param>
		public abstract void update(float elapsed);

		/// <summary>
		/// Abstract method that forces the children objects to implement render
		/// </summary>
		/// <param name="spriteBatch">SpriteBatch object</param>
		public abstract void render(SpriteBatch spriteBatch);
		#endregion Support method

		#region Destructor
		
		#endregion Destructor
	}
}
