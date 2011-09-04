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
		/// <summary>
		/// Determines how the sprite sheet is loaded
		/// </summary>
		public enum LoadingType {
			/// <summary>
			/// Loads the entire sprite sheet and determines the frames based on the Textures width / the total frames passed in
			/// </summary>
			WholeSheetReadFramesFromFile,
			/// <summary>
			/// Loads the sprite sheet based on the framesStartWidth, framesStartHeight, framesHeight, framesWidth, and the space between the frames
			/// </summary>
			CustomizedSheetDefineFrames
		}
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
		#endregion Class properties

		#region Constructor
		/// <summary>
		/// Builds a sprite based off of the parms specified
		/// </summary>
		/// <param name="parms">SpriteParams object containing all of the data to how this sprite is loaded, its position, colour, etc,etc</param>
		public Animated2DSprite(Animated2DSpriteParams parms)
			: base(parms) {
			if (parms.Texture2D == null && parms.TexturesName != null) {
				Texture2D texture2D = LoadingUtils.loadTexture2D(parms.Content, parms.TexturesName);
				parms.Texture2D = texture2D;
			}

			initSprite(parms.Position, parms.Origin, parms.Rotation, parms.Scale, parms.Layer, parms.Texture2D, parms.LightColour, parms.LoadingType, parms.FramesStartWidth, 
				parms.FramesStartHeight, parms.FramesWidth, parms.FramesHeight, parms.SpaceBetweenFrames, parms.AnimationParams);
		}
		#endregion Constructor

		#region Initialization
		/// <summary>
		/// Actual building of the sprite
		/// </summary>
		/// <param name="position">Starting position of the sprite</param>
		/// <param name="origin">Sprites orign; important for rotating the sprite</param>
		/// <param name="rotation">Rotation of  the sprite</param>
		/// <param name="scale">X,Y scale of the sprite</param>
		/// <param name="depth">Layer to draw the sprite at</param>
		/// <param name="texture">Texture2D use to render the sprite</param>
		/// <param name="lightColour">Colour to render to the sprite in</param>
		/// <param name="loadingType">Way to load the sprite</param>
		/// <param name="framesStartWidth">Starting x point of the sprite in the sprite sheet</param>
		/// <param name="framesStartHeight">Starting y point of the sprite in the sprite sheet</param>
		/// <param name="frameWidth">X size of a sprites single frame</param>
		/// <param name="frameHeight">Y size of a sprites single frame</param>
		/// <param name="spaceBetweenFrames">Space between the frames in the sprite sheet</param>
		/// <param name="animationParams">BaseAnimationManagerParams object containing the animation information for the sprite</param>
		private void initSprite(Vector2 position, Vector2 origin, float rotation, Vector2 scale, float depth, Texture2D texture, Color lightColour, Animated2DSprite.LoadingType loadingType, 
			int framesStartWidth, int framesStartHeight, int frameWidth, int frameHeight, int spaceBetweenFrames, BaseAnimationManagerParams animationParams) {
			this.position = position;
			this.origin = origin;
			this.rotation = rotation;
			this.scale = scale;
			this.layer = depth;
			this.texture = texture;
			this.lightColour = lightColour;
			this.animationManager = new AnimationManager(animationParams);

			//setup the frames
			this.frames = new Rectangle[animationParams.TotalFrameCount];
			if (loadingType == LoadingType.WholeSheetReadFramesFromFile) {
				frameWidth = this.texture.Width / animationParams.TotalFrameCount;
				frameHeight = this.texture.Height;
			}
			// load the frames
			int x = framesStartWidth;
			for (int i = 0; i < this.frames.Length; i++) {
				this.frames[i] = new Rectangle(x, framesStartHeight, frameWidth, frameHeight);
				x = (x + frameWidth + spaceBetweenFrames);
			}
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
		}

		/// <summary>
		/// Renders the sprite to the screen
		/// </summary>
		/// <param name="spriteBatch">SpriteBatch object used to render the sprite</param>
		public override void render(SpriteBatch spriteBatch) {
			render(spriteBatch, this.animationManager.CurrentFrame);
		}

		/// <summary>
		/// Renders the sprite to the screen at a specific frame
		/// </summary>
		/// <param name="spriteBatch">SpriteBatch object used to render the sprite</param>
		/// <param name="frame">Specific frame to render the sprite at</param>
		public virtual void render(SpriteBatch spriteBatch, int frame) {
			spriteBatch.Draw(this.texture, this.position, this.frames[frame], base.lightColour, base.rotation, base.origin, base.scale, base.spriteEffect, base.layer);
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
