using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using GWNorthEngine.Audio.Params;
namespace GWNorthEngine.Audio {
	/// <summary>
	/// Base implementation of a sound engine
	/// </summary>
	public abstract class BaseSoundEngine {
		#region Class variables
		private float volume;
		#endregion Class variables

		#region Class properties
		/// <summary>
		/// Gets or sets the volume to play the audio at
		/// </summary>
		public virtual float Volume {
			get { return this.volume; }
			set { this.volume = MathHelper.Clamp(value, 0f, 1f); }
		}
		/// <summary>
		/// Gets or sets whether to play audio or not
		/// </summary>
		public virtual bool Muted { get; set; }
		#endregion Class properties

		#region Constructor
		/// <summary>
		/// Constructs a SoundEngine object based on the data passed in via the params object
		/// </summary>
		/// <param name="parms"></param>
		public BaseSoundEngine(BaseSoundEngineParams parms) {
			this.Volume = parms.Volume;
			this.Muted = parms.Muted;
		}
		#endregion Constructor

		#region Support methods
		/// <summary>
		/// Monitors current Sound instances and flags them for cleanup if they have finished playing
		/// </summary>
		public abstract void update();
		#endregion Support methods

		#region Destructor
		/// <summary>
		/// Cleans up resources used by the SoundEngine
		/// </summary>
		public abstract void dispose();
		#endregion
	}
}
