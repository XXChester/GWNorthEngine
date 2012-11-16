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
	/// Models the data required for a Color lerping effect
	/// </summary>
	public class ColorLerpEffect : BaseEffect {
		#region Class variables
		private float lerp;
		private PulseDirection pulseDirection;
		#endregion Class variables

		#region Class properties
		/// <summary>
		/// Color to lerp down to
		/// </summary>
		public Color LerpUpTo { get; set; }
		/// <summary>
		/// Color to lerp up to
		/// </summary>
		public Color LerpDownTo { get; set; }
		/// <summary>
		/// Amount to lerp by
		/// </summary>
		public float LerpBy { get; set; }
		#endregion Class properties

		#region Constructor
		/// <summary>
		/// Constructs a PulseEffect object
		/// </summary>
		/// <param name="parms">ColourLerpEffectParms object used to build the effect</param>
		public ColorLerpEffect(ColourLerpEffectParams parms) : base(parms) {
			this.LerpDownTo = parms.LerpDownTo;
			this.LerpUpTo = parms.LerpUpTo;
			this.LerpBy = parms.LerpBy;
		}
		#endregion Constructor

		#region Support methods
		/// <summary>
		/// Runs the effect
		/// </summary>
		public override void update(float elapsed) {
			if (this.pulseDirection == PulseDirection.Up) {
				this.lerp += this.LerpBy;
				if (this.lerp >= 1) {
					this.pulseDirection = PulseDirection.Down;
				}
			} else {
				this.lerp -= this.LerpBy;
				if (this.lerp <= 0) {
					this.pulseDirection = PulseDirection.Up;
				}
			}
			this.reference.LightColour = Color.Lerp(this.LerpUpTo, this.LerpDownTo, this.lerp);
		}
		#endregion Support methods
	}
}
