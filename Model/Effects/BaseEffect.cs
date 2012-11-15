using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GWNorthEngine.Model.Effects {
	/// <summary>
	/// Models the manditory fields for an effect
	/// </summary>
	public abstract class BaseEffect {
		/// <summary>
		/// Every effect must implement the update method
		/// </summary>
		public abstract void update();
	}
}
