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
	public class FadeEffectParams : BaseEffectParams {
		/// <summary>
		/// State of the effect
		/// </summary>
		public FadeEffect.FadeState State { get; set; }
		/// <summary>
		/// Total time the fade should take
		/// </summary>
		public float TotalTransitionTime { get; set; }
		/// <summary>
		/// Original colour to base the effect off of
		/// </summary>
		public Color OriginalColour { get; set; }
	}
}
