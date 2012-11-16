using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

using GWNorthEngine.Engine;
using GWNorthEngine.Logic;

namespace GWNorthEngine.Model.Effects.Params {
	/// <summary>
	/// Models the data required for a pulse effect
	/// </summary>
	public class PulseEffectParams : BaseEffectParams {

		#region Class properties
		/// <summary>
		/// Amount to scale down to
		/// </summary>
		public float ScaleDownTo { get; set; }
		/// <summary>
		/// Amount to scale up to
		/// </summary>
		public float ScaleUpTo { get; set; }
		/// <summary>
		/// Amount to scale by
		/// </summary>
		public float ScaleBy { get; set; }
		#endregion Class properties
	}
}
