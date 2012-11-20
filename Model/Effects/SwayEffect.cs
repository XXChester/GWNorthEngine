using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

using GWNorthEngine.Logic;
using GWNorthEngine.Model.Effects.Params;

namespace GWNorthEngine.Model.Effects {
	/// <summary>
	/// Models the data required for a Sway effect
	/// </summary>
	public class SwayEffect : BaseEffect {
		#region Class variables
		private PulseDirection direction;
		private float currentSway;
		#endregion Class variables

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
		#endregion Class properties

		#region Constructor
		/// <summary>
		/// Constructs a SwayEffect
		/// </summary>
		/// <param name="parms">SwayEffectParams object</param>
		public SwayEffect(SwayEffectParms parms) : base(parms) {
			this.currentSway = 0f;
			this.SwayBy = parms.SwayBy;
			this.SwayUpTo = parms.SwayUpTo;
			this.SwayDownTo = parms.SwayDownTo;
			this.direction = parms.Direction;
		}
		#endregion Consturctor

		#region Support methods
		/// <summary>
		/// Runs the effect
		/// </summary>
		public override void update(float elapsed) {
			if (this.direction == PulseDirection.Up) {
				this.currentSway += ((SwayBy.X + SwayBy.Y) / 1000f) * elapsed;
				this.Reference.Position += (SwayBy / 1000f) * elapsed;
				if (this.currentSway >= this.SwayUpTo) {
					this.direction = PulseDirection.Down;
				}
			} else if (this.direction == PulseDirection.Down) {
				this.currentSway -= ((SwayBy.X + SwayBy.Y) / 1000f) * elapsed;
				this.Reference.Position -= (SwayBy / 1000f) * elapsed;
				if (this.currentSway <= this.SwayDownTo) {
					this.direction = PulseDirection.Up;
				}
			}
		}
		#endregion Support methods
	}
}
