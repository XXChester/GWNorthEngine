using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

using GWNorthEngine.Logic;
using GWNorthEngine.Model.Effects.Params;

namespace GWNorthEngine.Model.Effects {
	/// <summary>
	/// Models the data required for a scale over time effect
	/// </summary>
	public class ScaleOverTimeEffect : BaseEffect {
		#region Class variables
		#endregion Class variables

		#region Class properties
		/// <summary>
		/// Amount to scale by
		/// </summary>
		public Vector2 ScaleBy { get; set; }
		#endregion Class properties

		#region Constructor
		/// <summary>
		/// Constructs a ScaleOverTimeEffect
		/// </summary>
		/// <param name="parms">ScaleOverTimeEffectParams object</param>
		public ScaleOverTimeEffect(ScaleOverTimeEffectParams parms) : base(parms) {
			this.ScaleBy = parms.ScaleBy;
		}
		#endregion Consturctor

		#region Support methods
		/// <summary>
		/// Runs the effect
		/// </summary>
		public override void update(float elapsed) {
			this.Reference.Scale += (ScaleBy / 1000f) * elapsed;
		}
		#endregion Support methods
	}
}
