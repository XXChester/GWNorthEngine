using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using GWNorthEngine.Model.Params;
namespace GWNorthEngine.Model.Params {
	/// <summary>
	/// Models the data required to draw a static texture to the screen
	/// </summary>
	public class StaticDrawable2DParams : Base2DSpriteDrawableParams {
		#region Class propeties
		/// <summary>
		/// Gets or sets the texture to be drawn to the screen
		/// </summary>
		public Texture2D Texture { get; set; }
		#endregion Class properties
	}
}
