using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

using GWNorthEngine.Engine;
using GWNorthEngine.Logic;

namespace GWNorthEngine.Model.Effects {
	/// <summary>
	/// Models the data required for a pulse effect
	/// </summary>
	public class PulseEffect : BaseEffect {
		#region Class variables
		private Vector2D scale;
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
		/// <summary>
		/// Reference to the objects scale that the effect is to apply to
		/// </summary>
		public Vector2D Scale { get { return this.scale; } }
		#endregion Class properties

		#region Constructor
		/// <summary>
		/// Constructs a PulseEffect object
		/// </summary>
		/// <param name="scale">Reference's scale that we are going to control</param>
		/// <param name="scaleUpTo">Value to scale up to</param>
		/// <param name="scaleDownTo">Value to sclae down to</param>
		/// <param name="scaleBy">Amount to scale by</param>
		public PulseEffect(Vector2D scale, float scaleUpTo, float scaleDownTo, float scaleBy) {
			this.scale = scale;
			this.ScaleBy = scaleBy;
			this.ScaleDownTo = scaleDownTo;
			this.ScaleUpTo = scaleUpTo;
		}
		#endregion Constructor

		#region Support methods
		/// <summary>
		/// Runs the effect
		/// </summary>
		public override void update() {
			if (this.pulseDirection == PulseDirection.Up) {
				this.scale += new Vector2D(this.ScaleBy);
				if (this.scale.X >= this.ScaleUpTo) {
					this.pulseDirection = PulseDirection.Down;
				}
			} else {
				this.scale -= new Vector2D(this.ScaleBy);
				if (this.scale.X <= this.ScaleDownTo) {
					this.pulseDirection = PulseDirection.Up;
				}
			}
		}
		#endregion Support methods
	}
}
