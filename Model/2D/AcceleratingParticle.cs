using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

using GWNorthEngine.Engine;
using GWNorthEngine.Engine.Params;
using GWNorthEngine.Model;
using GWNorthEngine.Model.Params;
using GWNorthEngine.Logic;
using GWNorthEngine.Logic.Params;
using GWNorthEngine.Input;
using GWNorthEngine.Utils;
using GWNorthEngine.Scripting;

namespace GWNorthEngine.Model {
	/// <summary>
	/// Models a particle that accelerates as its life progresses
	/// </summary>
	public class AcceleratingParticle : BaseParticle2D {
		#region Constructor
		/// <summary>
		/// Constructs an Accelerating particle
		/// </summary>
		/// <param name="parms">BaseParticle2DParms object</param>
		public AcceleratingParticle(BaseParticle2DParams parms) : base(parms) {
		}
		#endregion Constructor

		#region Support methods
		/// <summary>
		/// Updates the particles position, gaining speed as its life progresses
		/// </summary>
		/// <param name="elapsed">Elapsed time</param>
		public override void update(float elapsed) {
			base.update(elapsed);
			float reverseRelativeAge = 1 - (this.timeAlive / this.timeToLive);
			Vector2 relativeAcceleration = (base.acceleration * reverseRelativeAge) * direction;
			Vector2 speed = (relativeAcceleration / 1000) * elapsed;

			this.position += speed;
		}
		#endregion Support methods
	}
}
