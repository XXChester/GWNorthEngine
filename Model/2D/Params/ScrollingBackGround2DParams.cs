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
using GWNorthEngine.Model;
namespace GWNorthEngine.Model.Params {
	/// <summary>
	/// Contains the data required to build a scrolling background
	/// </summary>
	public class ScrollingBackGround2DParams {
		#region Class propeties
		/// <summary>
		/// Gets or Sets Speed in which the scene should scroll at
		/// </summary>
		public Vector2 Speed { get; set; }
		/// <summary>
		/// Gets or sets the List of StaticDrawable2D background objects **NOTE**Positions are set in the scroller, just pass a basic list
		/// </summary>
		public List<StaticDrawable2D> BackGrounds { get; set; }
		/// <summary>
		/// Gets or sets the direction that the scene should scroll in
		/// </summary>
		public ScrollingBackGround2D.ScrollingDirection ScrollingDirection { get; set; }
		/// <summary>
		/// Gets or Sets the viewport the background is rendered in
		/// </summary>
		public Viewport ViewPort { get; set; }
		#endregion Class properties
	}
}
