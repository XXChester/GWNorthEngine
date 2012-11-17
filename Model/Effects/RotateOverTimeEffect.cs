using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

using GWNorthEngine.Logic;
using GWNorthEngine.Model.Effects.Params;

namespace GWNorthEngine.Model.Effects {
	/// <summary>
	/// Models the data required for a rotate over time effect
	/// </summary>
	public class RotateOverTimeEffect : BaseEffect {
		#region Class variables
		#endregion Class variables

		#region Class properties
		/// <summary>
		/// Amount to rotate by
		/// </summary>
		public float RotateBy { get; set; }
		#endregion Class properties

		#region Constructor
		/// <summary>
		/// Constructs a ScaleOverTimeEffect
		/// </summary>
		/// <param name="parms">RotateOverTimeEffectParams object</param>
		public RotateOverTimeEffect(RotateOverTimeEffectParams parms)
			: base(parms) {
			this.RotateBy = parms.RotateBy;
		}
		#endregion Consturctor

		#region Support methods
		/// <summary>
		/// Runs the effect
		/// </summary>
		public override void update(float elapsed) {
			this.reference.Rotation += (RotateBy / 1000f) * elapsed;
		}
		#endregion Support methods
	}
}
