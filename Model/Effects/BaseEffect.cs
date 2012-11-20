using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GWNorthEngine.Model.Effects.Params;

namespace GWNorthEngine.Model.Effects {
	/// <summary>
	/// Models the manditory fields for an effect
	/// </summary>
	public abstract class BaseEffect {
		#region Class variables

		#endregion Class variables

		#region Class properties
		/// <summary>
		/// Reference object LightColour that the effect is to apply to
		/// </summary>
		public Base2DSpriteDrawable Reference { get; set; }
		#endregion Class properties

		#region Constructor
		/// <summary>
		/// Constructs a BaseEffect object based on the parms
		/// </summary>
		/// <param name="parms">BaseEffectParams object</param>
		public BaseEffect(BaseEffectParams parms) {
			
		}
		#endregion Constructor

		#region Support methods
		/// <summary>
		/// Every effect must implement the update method
		/// </summary>
		public abstract void update(float elapsed);
		#endregion Support methods
	}
}
