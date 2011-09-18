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
	/// Models the basis of every particle emitter
	/// </summary>
	public abstract class BaseParticle2DEmitter {
		#region Class variables
		/// <summary>
		/// Elapsed time since the last particle was spawned
		/// </summary>
		protected float elapsedSpawnTime;
		/// <summary>
		/// Spawn delay to create new particles
		/// </summary>
		protected readonly float spawnDelay;
		/// <summary>
		/// Texture the particles are using
		/// </summary>
		protected Texture2D particleTexture;
		/// <summary>
		/// Basic params object used to create the particle
		/// </summary>
		protected BaseParticle2DParams particleParams;
		/// <summary>
		/// List of emitted particles
		/// </summary>
		protected List<BaseParticle2D> particles;
		/// <summary>
		/// Random number generator
		/// </summary>
		protected readonly Random RANDOM;
		#endregion Class variables

		#region Constructor
		/// <summary>
		/// Builds the base particle emitter
		/// </summary>
		/// <param name="parms">BaseParticle2DEmitterParams object containing the data required to build the particle emitter</param>
		public BaseParticle2DEmitter(BaseParticle2DEmitterParams parms) {
			this.elapsedSpawnTime = parms.SpawnDelay;
			this.particleTexture = parms.ParticleTexture;
			this.spawnDelay = parms.SpawnDelay;
			this.RANDOM = new Random();
			this.particles = new List<BaseParticle2D>();
		}
		#endregion Constructor

		#region Support methods
		/// <summary>
		/// Default implementation of creating a particle...default implementation is setting the elapsedSpawnTime back to 0f;
		/// </summary>
		public virtual void createParticle() {
			this.elapsedSpawnTime = 0f;
		}

		/// <summary>
		/// Updates the particle emitter and its particles. Also handles the removal of old particles
		/// </summary>
		/// <param name="elapsed"></param>
		public virtual void update(float elapsed) {
			this.elapsedSpawnTime += elapsed;
			List<int> indexesUpForRemoval = new List<int>();
			BaseParticle2D particle = null;
			for (int i = 0; i < this.particles.Count; i++) {
				particle = this.particles[i];
				particle.update(elapsed);
				if (particle.TimeAlive >= particle.TimeToLive) {
					// mark the particle for removal to avoid concurrent access violations
					indexesUpForRemoval.Add(i);
				}
			}
			for (int i = indexesUpForRemoval.Count - 1; i >= 0; i--) {
				this.particles.RemoveAt(indexesUpForRemoval[i]);
			}
		}

		/// <summary>
		/// Renders the emitters active particles
		/// </summary>
		/// <param name="spriteBatch"></param>
		public virtual void render(SpriteBatch spriteBatch) {
			if (this.particles != null) {
				foreach (BaseParticle2D particle in this.particles) {
					particle.render(spriteBatch);
				}
			}
		}
		#endregion Support methods

		#region Destructor
		/// <summary>
		/// Disposes of the emitters texture used to generate the particles
		/// </summary>
		public virtual void dispose() {
			if (this.particleTexture != null) {
				this.particleTexture.Dispose();
			}
		}
		#endregion Destructor
	}
}
