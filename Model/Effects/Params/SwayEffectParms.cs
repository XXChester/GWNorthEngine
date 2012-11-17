using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

using GWNorthEngine.Logic;
using GWNorthEngine.Model.Effects;

namespace GWNorthEngine.Model.Effects.Params {
	/// <summary>
	/// Models the data required for a Sway effect
	/// </summary>
	public class SwayEffectParms : BaseEffectParams {
		#region Class properties
		/// <summary>
		/// Amount to sway by
		/// </summary>
		public Vector2 SwayBy { get; set; }
		/// <summary>
		/// Amount to sway up to
		/// </summary>
		public float SwayUpTo { get; set; }
		/// <summary>
		/// Amount to sway down to
		/// </summary>
		public float SwayDownTo { get; set; }
		/// <summary>
		/// Direction we want to start the effect in
		/// </summary>
		public PulseDirection Direction { get; set; }
		#endregion Class properties
	}
}
