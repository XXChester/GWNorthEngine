using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

namespace GWNorthEngine.Model.Effects.Params {
	/// <summary>
	/// Models the data required to build a ColourLerpEffect
	/// </summary>
	public class ColourLerpEffectParams : BaseEffectParams {
		/// <summary>
		/// Color to lerp down to
		/// </summary>
		public Color LerpUpTo { get; set; }
		/// <summary>
		/// Color to lerp up to
		/// </summary>
		public Color LerpDownTo { get; set; }
		/// <summary>
		/// Amount to lerp by
		/// </summary>
		public float LerpBy { get; set; }
	}
}
