using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

using GWNorthEngine.Model.Effects;

namespace GWNorthEngine.Model.Effects.Params {
	/// <summary>
	/// Models the data required to build a fade effect
	/// </summary>
	public class PartialFadeEffectParams : FadeEffectParams {
		/// <summary>
		/// Amount to fade in our out
		/// </summary>
		public int AlphaAmount { get; set; }
	}
}
