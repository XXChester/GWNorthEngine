using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using GWNorthEngine.Model.Params;
using GWNorthEngine.Utils;
namespace GWNorthEngine.Model.Params {
	/// <summary>
	/// Object containing the data required to build a BaseParticle2DEmitter object
	/// </summary>
	public class BaseParticle2DEmitterParams {
		#region Class propeties
		/// <summary>
		/// Gets or sets the rate which a particle should be created at
		/// </summary>
		public float SpawnDelay { get; set; }
		/// <summary>
		/// Gets or sets the texture used for particle generation
		/// </summary>
		public Texture2D ParticleTexture { get; set; }
		#endregion Class properties
	}
}
