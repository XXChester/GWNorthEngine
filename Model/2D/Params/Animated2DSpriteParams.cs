using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GWNorthEngine.Model.Params {
	/// <summary>
	/// Object containing the data required to build a sprite
	/// </summary>
	public class Animated2DSpriteParams : Base2DSpriteDrawableParams {
		#region Class variables
		private ContentManager content;
		private string texturesName;
		private Texture2D texture2D;
		private float frameRate;
		private int totalFrameCount;
		private AnimationManager.AnimationState animationState;
		private Animated2DSprite.LoadingType loadingType;
		private int framesStartWidth;
		private int framesStartHeight;
		private int framesWidth;
		private int framesHeight;
		private int spaceBetweenFrames;
		#endregion Class variables

		#region Class propeties
		/// <summary>
		/// Gets or sets the ContentManager used when loading a sprites texture internally
		/// </summary>
		public ContentManager Content { get { return this.content; } set { this.content = value; } }
		/// <summary>
		/// Gets or sets the textures name to load for an internal loading sprite
		/// </summary>
		public string TexturesName { get { return this.texturesName; } set { this.texturesName = value; } }
		/// <summary>
		/// Gets or sets the texture for a sprite for externally loaded sprites
		/// </summary>
		public Texture2D Texture2D { get { return this.texture2D; } set { this.texture2D = value; } }
		/// <summary>
		/// Gets or sets the starting frame rate in which the sprite is to be animated at
		/// </summary>
		public float FrameRate { get { return this.frameRate; } set { this.frameRate = value; } }
		/// <summary>
		/// Gets or sets the total numebr of frames for the sprite
		/// </summary>
		public int TotalFrameCount { get { return this.totalFrameCount; } set { this.totalFrameCount = value; } }
		/// <summary>
		/// Gets or sets the starting animation state of the sprite
		/// </summary>
		public AnimationManager.AnimationState AnimationState { get { return this.animationState; } set { this.animationState = value; } }
		/// <summary>
		/// Gets or sets the loading type of the sprite
		/// </summary>
		public Animated2DSprite.LoadingType LoadingType { get { return this.loadingType; } set { this.loadingType = value; } }
		/// <summary>
		/// Gets or sets where in the sprite sheet the starting width is. ***NOT REQUIRED FOR INTERNALLY LOADED SPRITES***
		/// </summary>
		public int FramesStartWidth { get { return this.framesStartWidth; } set { this.framesStartWidth = value; } }
		/// <summary>
		/// Gets or sets where in the sprite sheet the starting height is. ***NOT REQUIRED FOR INTERNALLY LOADED SPRITES***
		/// </summary>
		public int FramesStartHeight { get { return this.framesStartHeight; } set { this.framesStartHeight = value; } }
		/// <summary>
		/// Gets or sets the sprites width for the frame loading. ***NOT REQUIRED FOR INTERNALLY LOADED SPRITES***
		/// </summary>
		public int FramesWidth { get { return this.framesWidth; } set { this.framesWidth = value; } }
		/// <summary>
		/// Gets or sets the sprites height for the frame loading. ***NOT REQUIRED FOR INTERNALLY LOADED SPRITES***
		/// </summary>
		public int FramesHeight { get { return this.framesHeight; } set { this.framesHeight = value; } }
		/// <summary>
		/// Gets or Sets the distance between the frames in the sprite sheet to load.  ***NOT REQUIRED FOR INTERNALLY LOADED SPRITES***
		/// </summary>
		public int SpaceBetweenFrames { get { return this.spaceBetweenFrames; } set { this.spaceBetweenFrames = value; } }
		#endregion Class properties

		#region Constructor
		/// <summary>
		/// Defines a new SpriteParams object with the defaults set. The defaults are listed below;
		/// AnimationState: AnimationState.Paused
		/// LoadingType:	LoadingType.WholeSheetReadFramesFromFile (Internal loaded sprite)
		/// </summary>
		public Animated2DSpriteParams() {
			// defaults
			this.animationState = AnimationManager.AnimationState.Paused;
			this.loadingType = Animated2DSprite.LoadingType.WholeSheetReadFramesFromFile;
		}
		#endregion Constructor
	}
}
