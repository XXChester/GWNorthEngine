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
		/// <summary>
		/// Reference object
		/// </summary>
		protected Base2DSpriteDrawable reference;
		#endregion Class variables

		#region Constructor
		/// <summary>
		/// Constructs a BaseEffect object based on the parms
		/// </summary>
		/// <param name="parms">BaseEffectParams object</param>
		public BaseEffect(BaseEffectParams parms) {
			this.reference = parms.Reference;
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
