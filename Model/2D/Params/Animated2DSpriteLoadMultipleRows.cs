using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework.Graphics;

namespace GWNorthEngine.Model.Params {
	/// <summary>
	/// Object used to load an Animated2DSprite object that is a multiple rows in a sprite sheet
	/// </summary>
	public class Animated2DSpriteLoadMultipleRows : BaseAnimated2DSpriteParams {
		#region Class properties
		/// <summary>
		/// Gets or sets the number of columns to load in a sprite sheet before moving to the next line
		/// </summary>
		public int MaxColumnsToARow { get { return base.maxColumnsToARow; } set { this.maxColumnsToARow = value; } }
		#endregion Class properties
	}
}
