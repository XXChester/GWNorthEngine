using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework.Graphics;

namespace GWNorthEngine.Model.Params {
	public class Animated2DSpriteLoadSingleCustomRow : BaseAnimated2DSpriteParams {
		#region Class properties
		/// <summary>
		/// Gets or sets where in the sprite sheet the starting width is.
		/// </summary>
		public int FramesStartWidth { get { return base.framesStartWidth; } set { base.framesStartWidth = value; } }
		/// <summary>
		/// Gets or sets where in the sprite sheet the starting height is.
		/// </summary>
		public int FramesStartHeight { get { return base.framesStartHeight; } set { base.framesStartHeight = value; } }
		/// <summary>
		/// Gets or Sets the distance between the frames in the sprite sheet to load.
		/// </summary>
		public int SpaceBetweenFrames { get { return base.spaceBetweenFrames; } set { base.spaceBetweenFrames = value; } }
		#endregion Class properties

		#region Constructor

		#endregion Constructor
	}
}
