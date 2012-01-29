using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using GWNorthEngine.Model.Params;
using GWNorthEngine.Utils;

namespace GWNorthEngine.Model {
	/// <summary>
	/// Sprite class that can be either animated or not
	/// </summary>
	public class Animated2DSprite : Base2DSpriteDrawable {
		#region Class variables
		/// <summary>
		/// Texture used to render the sprite
		/// </summary>
		protected Texture2D texture;
		/// <summary>
		/// Aninmation manager for the sprite
		/// </summary>
		protected AnimationManager animationManager;
		/// <summary>
		/// All the frames from the sprite sheet for the sprite
		/// </summary>
		protected Rectangle[] frames;
		#endregion Class variables

		#region Class propeties
		/// <summary>
		/// Gets or sets the animation manager for the sprite
		/// </summary>
		public AnimationManager AnimationManager { get { return this.animationManager; } set { this.animationManager = value; } }
		/// <summary>
		/// Gets or sets the texture of an animated sprite
		/// </summary>
		public Texture2D Texture { get { return this.texture; } set { this.texture = value; } }
		/// <summary>
		/// Gets or sets the animation frames
		/// </summary>
		public Rectangle[] Frames { get { return this.frames; } set { this.frames = value; } }
		#endregion Class properties

		#region Constructor
		/// <summary>
		/// Builds a sprite based off of the parms specified
		/// </summary>
		/// <param name="parms">SpriteParams object containing all of the data to how this sprite is loaded, its position, colour, etc,etc</param>
		public Animated2DSprite(BaseAnimated2DSpriteParams parms)
			: base(parms) {
			if (typeof(Animated2DSpriteLoadSingleCustomRow) == parms.GetType()) {
				Animated2DSpriteLoadSingleCustomRow realParms = (Animated2DSpriteLoadSingleCustomRow)parms;
				initSprite(realParms.Texture2D, realParms.AnimationParams, realParms.FramesWidth,
					realParms.FramesHeight, realParms.SpaceBetweenFrames,  realParms.FramesStartWidth, realParms.FramesStartHeight);
			} else if (typeof(Animated2DSpriteLoadSingleRowBasedOnTexture) == parms.GetType()) {
				parms.FramesWidth = parms.Texture2D.Width / parms.AnimationParams.TotalFrameCount;
				parms.FramesHeight = parms.Texture2D.Height;
				initSprite(parms.Texture2D, parms.AnimationParams, parms.FramesWidth, parms.FramesHeight);
			} else {
				initSprite(parms.Texture2D, parms.AnimationParams, parms.FramesWidth, parms.FramesHeight);
			}
		}
		#endregion Constructor

		#region Initialization
		/// <summary>
		/// Actual building of the sprite
		/// </summary>
		/// <param name="texture">Texture2D use to render the sprite</param>
		/// <param name="framesStartWidth">Starting x point of the sprite in the sprite sheet</param>
		/// <param name="framesStartHeight">Starting y point of the sprite in the sprite sheet</param>
		/// <param name="frameWidth">X size of a sprites single frame</param>
		/// <param name="frameHeight">Y size of a sprites single frame</param>
		/// <param name="spaceBetweenFrames">Space between the frames in the sprite sheet</param>
		/// <param name="animationParams">BaseAnimationManagerParams object containing the animation information for the sprite</param>
		private void initSprite(Texture2D texture, BaseAnimationManagerParams animationParams, int frameWidth, int frameHeight,
			int spaceBetweenFrames=0, int framesStartWidth=0, int framesStartHeight=0) {
			this.texture = texture;
			this.animationManager = new AnimationManager(animationParams);

			// load the frames
			this.frames = new Rectangle[animationParams.TotalFrameCount];
			int x = framesStartWidth;
			for (int i = 0; i < this.frames.Length; i++) {
				this.frames[i] = new Rectangle(x, framesStartHeight, frameWidth, frameHeight);
				x = (x + frameWidth + spaceBetweenFrames);
			}
			base.renderingRectangle = this.frames[this.animationManager.CurrentFrame];
		}
		#endregion Initialization

		#region Support methods
		/// <summary>
		/// Resets the sprite
		/// </summary>
		public void reset() {
			reset(false);
		}

		/// <summary>
		/// Resets the sprite with the option to make the animation sequence fire as soon as a "Play" state is entered
		/// </summary>
		/// <param name="cockAnimation">Option to make an animation fire as soon as a "Play" state is entered</param>
		public void reset(bool cockAnimation) {
			if (cockAnimation) {
				this.animationManager.resetAnimation(this.frames.Length - 1, true);
			} else {
				this.animationManager.resetAnimation(this.frames.Length - 1);
			}
		}

		/// <summary>
		/// Updates the sprite, by default only the animation
		/// </summary>
		/// <param name="elapsed"></param>
		public override void update(float elapsed) {
			this.animationManager.update(elapsed, this.frames.Length - 1);
			base.renderingRectangle = this.frames[this.animationManager.CurrentFrame];
		}

		/// <summary>
		/// Renders the sprite to the screen
		/// </summary>
		/// <param name="spriteBatch">SpriteBatch object used to render the sprite</param>
		public override void render(SpriteBatch spriteBatch) {
			spriteBatch.Draw(this.texture, base.position, base.renderingRectangle, base.lightColour, base.rotation, base.origin, base.scale, base.spriteEffect, base.layer);
		}
		#endregion Support methods

		#region Destructor
		/// <summary>
		/// Releases the portions of the sprite that cause memory leaks
		/// </summary>
		public virtual void dispose() {
			if (this.texture != null) {
				this.texture.Dispose();
			}
		}
		#endregion Destructor
	}
}
