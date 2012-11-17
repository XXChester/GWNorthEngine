using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

using GWNorthEngine.Engine;
using GWNorthEngine.Logic;
using GWNorthEngine.Model.Effects.Params;

namespace GWNorthEngine.Model.Effects {
	/// <summary>
	/// Models the data required for a pulse effect
	/// </summary>
	public class PulseEffect : BaseEffect {
		#region Class variables
		private PulseDirection pulseDirection;
		#endregion Class variables

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

		#region Constructor
		/// <summary>
		/// Constructs a PulseEffect object
		/// </summary>
		/// <param name="parms">PulseEffectParams object</param>
		public PulseEffect(PulseEffectParams parms) : base(parms) {
			this.ScaleBy = parms.ScaleBy;
			this.ScaleDownTo = parms.ScaleDownTo;
			this.ScaleUpTo = parms.ScaleUpTo;
		}
		#endregion Constructor

		#region Support methods
		/// <summary>
		/// Runs the effect
		/// </summary>
		public override void update(float elapsed) {
			if (this.pulseDirection == PulseDirection.Up) {
				this.reference.Scale += new Vector2((this.ScaleBy / 1000f) * elapsed);
				if (this.reference.Scale.X >= this.ScaleUpTo) {
					this.pulseDirection = PulseDirection.Down;
				}
			} else {
				this.reference.Scale -= new Vector2((this.ScaleBy / 1000f) * elapsed);
				if (this.reference.Scale.X <= this.ScaleDownTo) {
					this.pulseDirection = PulseDirection.Up;
				}
			}
		}
		#endregion Support methods
	}
}
