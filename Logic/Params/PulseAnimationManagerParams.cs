using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GWNorthEngine.Logic.Params {
	/// <summary>
	/// Models the data required to build a PulseAnimationManager object
	/// </summary>
	public class PulseAnimationManagerParams : BaseAnimationManagerParams {
		#region Class properties
		/// <summary>
		/// Gets or sets the PulseDirection
		/// </summary>
		public PulseDirection PulseDirection { get; set; }
		#endregion Class properties
	}
}
