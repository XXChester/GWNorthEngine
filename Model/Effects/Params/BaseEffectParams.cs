using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GWNorthEngine.Model.Effects.Params {
	/// <summary>
	/// Models the common data between effects
	/// </summary>
	public abstract class BaseEffectParams {
		/// <summary>
		/// Reference object LightColour that the effect is to apply to
		/// </summary>
		public Base2DSpriteDrawable Reference { get; set; }
	}
}
