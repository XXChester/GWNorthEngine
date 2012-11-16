using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using GWNorthEngine.Model.Params;
using GWNorthEngine.Utils;
namespace GWNorthEngine.Model {
	/// <summary>
	/// Models the basis of an automatic particle emitter
	/// </summary>
	public abstract class AutoParticle2DEmitter : BaseParticle2DEmitter {
		#region Class variables

		#endregion Class variables

		#region Class properties

		#endregion Class properties

		#region Constructor
		/// <summary>
		/// Builds the auto particle emitter
		/// </summary>
		/// <param name="parms">BaseParticle2DEmitterParams object containing the data required to build the particle emitter</param>
		public AutoParticle2DEmitter(BaseParticle2DEmitterParams parms) : base(parms) {
		}
		#endregion Constructor

		#region Support methods
		/// <summary>
		/// Updates the particle emitter and its particles, and creates new particles, also handles the removal of old particles
		/// </summary>
		/// <param name="elapsed"></param>
		public override void update(float elapsed) {
			base.update(elapsed);

			if (this.elapsedSpawnTime >= this.spawnDelay) {
				createParticle();
			}
		}
		#endregion Support methods
	}
}
