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
using GWNorthEngine.Model.Params;
using GWNorthEngine.Model.Effects;
using GWNorthEngine.Utils;
using GWNorthEngine.Logic;

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
		/// Rendering rectangle
		/// </summary>
		protected Rectangle renderingRectangle;
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
		/// <summary>
		/// Gets or sets the Origin in which the sprite is rendered around
		/// </summary>
		public Vector2 Origin { get { return this.origin; } set { this.origin = value; } }
		/// <summary>
		/// Gets or sets the SpriteEffects to rendering the object under
		/// </summary>
		public SpriteEffects SpriteEffect { get { return this.spriteEffect; } set { this.spriteEffect = value; } }
		/// <summary>
		/// Gets or sets the Rectangle used for rendering the texture
		/// </summary>
		public Rectangle RenderingRectangle { get { return this.renderingRectangle; } set { this.renderingRectangle = value; } }
		/// <summary>
		/// Effects applied to this object
		/// </summary>
		public List<BaseEffect> Effects { get; set; }
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
			this.renderingRectangle = parms.RenderingRectangle;
		}
		#endregion Constructor

		#region Initialization

		#endregion Initialization

		#region Support method
		/// <summary>
		/// Sets the rendering rectangle based on the texture's bounds
		/// </summary>
		/// <param name="texture">Texture to base the Rendering Rectangle on</param>
		protected void setRenderingRectByTexture(Texture2D texture) {
			// if no rendering rectangle was defined we should try to base it off of the texture
			if (texture != null && Rectangle.Empty.Equals(this.renderingRectangle)) {
				this.renderingRectangle = texture.Bounds;
			}
		}

		/// <summary>
		/// Adds an effect to the list of effects
		/// </summary>
		/// <param name="effect">Effect object to add</param>
		public void addEffect(BaseEffect effect) {
			addEffect(effect, this);
		}

		/// <summary>
		/// Adds an effect to the list of effects
		/// </summary>
		/// <param name="effect">Effect object to add</param>
		/// <param name="reference">Object that the effect applies to</param>
		public void addEffect(BaseEffect effect, Base2DSpriteDrawable reference) {
			if (this.Effects == null) {
				this.Effects = new List<BaseEffect>();
			}
			effect.Reference = reference;
			this.Effects.Add(effect);
		}

		/// <summary>
		/// Updates the effects applied to a BaseStatic2DSpriteDrawable
		/// </summary>
		/// <param name="elapsed">Time elapsed sense the last call</param>
		public virtual void updateEffects(float elapsed) {
			if (this.Effects != null) {
				foreach (BaseEffect effect in this.Effects) {
					effect.update(elapsed);
				}
			}
		}

		/// <summary>
		/// Runs the Effects applied to the object
		/// </summary>
		/// <param name="elapsed">Time elapsed sense the last call</param>
		public virtual void update(float elapsed) {
			updateEffects(elapsed);
		}

		/// <summary>
		/// Abstract method that forces the children objects to implement render
		/// </summary>
		/// <param name="spriteBatch">SpriteBatch object</param>
		public void render(SpriteBatch spriteBatch) {
			render(spriteBatch, Vector2.Zero);
		}

		/// <summary>
		/// Abstract method that forces the children objects to implement render
		/// </summary>
		/// <param name="spriteBatch">SpriteBatch object</param>
		/// <param name="positionOffset">Offset to render the object at</param>
		public abstract void render(SpriteBatch spriteBatch, Vector2 positionOffset);
		#endregion Support method

		#region Destructor
		
		#endregion Destructor
	}
}
