using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using GWNorthEngine.Model.Effects;

namespace GWNorthEngine.Model.Params {
	/// <summary>
	/// Params object containing the data required to build a TexturedEffectButton object
	/// </summary>
	public class TexturedEffectButtonParams : StaticDrawable2DParams {
		#region Class properties
		/// <summary>
		/// Buttons ID used for determining if a button was clicked
		/// </summary>
		public int ID { get; set; }
		/// <summary>
		/// Effects to be applied to the button on hover
		/// </summary>
		public List<BaseEffect> Effects { get; set; }
		/// <summary>
		/// Delegate that resets the button when the mouse is not over it
		/// </summary>
		public resetDrawableDelegate ResetDelegate { get; set; }
		/// <summary>
		/// Area of the rectable to pick
		/// </summary>
		public Rectangle PickableArea { get; set; }
		#endregion Class properties
	}
}
