using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using GWNorthEngine.Logic.Params;
namespace GWNorthEngine.Model.Params {
	/// <summary>
	/// Object containing the data required to build a sprite
	/// </summary>
	public abstract class BaseAnimated2DSpriteParams : Base2DSpriteDrawableParams {
		#region Class variables
		/// <summary>
		/// Where to start in the sprites width for loading
		/// </summary>
		protected int framesStartWidth;
		/// <summary>
		/// Where to start in the sprites height for loading
		/// </summary>
		protected int framesStartHeight;
		/// <summary>
		/// Space between the frames
		/// </summary>
		protected int spaceBetweenFrames;
		/// <summary>
		/// Number of columns to load in a sheet before moving to the next line
		/// </summary>
		protected int maxColumnsToARow;
		#endregion Class variables

		#region Class propeties
		/// <summary>
		/// Gets or sets the texture for a sprite for externally loaded sprites
		/// </summary>
		public Texture2D Texture2D { get; set; }
		/// <summary>
		/// Gets or sets the sprites width for the frame loading.
		/// </summary>
		public int FramesWidth { get; set; }
		/// <summary>
		/// Gets or sets the sprites height for the frame loading.
		/// </summary>
		public int FramesHeight { get; set; }
		/// <summary>
		/// Gets or sets the BaseAnimationManagerParams object for the sprite
		/// </summary>
		public BaseAnimationManagerParams AnimationParams { get; set; }
		#endregion Class properties

		#region Constructor
		/// <summary>
		/// Defines a new SpriteParams object with the defaults set. The defaults are listed below;
		/// AnimationState: AnimationState.Paused
		/// </summary>
		public BaseAnimated2DSpriteParams() {
			// defaults
			this.AnimationParams = new BaseAnimationManagerParams();
		}
		#endregion Constructor
	}
}
