using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

using GWNorthEngine.Engine;
using GWNorthEngine.Logic;
using GWNorthEngine.Model.Effects;
using GWNorthEngine.Model.Params;

namespace GWNorthEngine.Model {
	/// <summary>
	/// Models the data required to build a ConstantSpeedParticle
	/// </summary>
	public class ConstantSpeedParticle : BaseParticle2D {
		#region Class variables

		#endregion Class variables

		#region Constructor
		/// <summary>
		/// Constructs a particle that moves in a constant speed
		/// </summary>
		/// <param name="parms"></param>
		public ConstantSpeedParticle(BaseParticle2DParams parms) : base(parms) { }
		#endregion Constructor

		#region Support methods
		/// <summary>
		/// Moves the particle in the constant direction
		/// </summary>
		/// <param name="elapsed">Time elapsed since the last frame</param>
		public override void update(float elapsed) {
			base.update(elapsed);

			Vector2 speed = ((base.direction * base.acceleration) / 1000f) * elapsed;
			base.position += speed;
		}
		#endregion Support methods
	}
}
