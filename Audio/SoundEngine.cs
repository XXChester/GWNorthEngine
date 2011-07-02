using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Audio;
using GWNorthEngine.Audio.Params;
namespace GWNorthEngine.Audio {
	/// <summary>
	/// Base implementation of a sound engine
	/// </summary>
	public abstract class BaseSoundEngine {
		#region Class variables
		#endregion Class variables

		#region Class properties
		/// <summary>
		/// Gets or sets the volume to play the audio at
		/// </summary>
		public float Volume { get; set; }
		/// <summary>
		/// Gets or sets whether to play audio or not
		/// </summary>
		public bool Enabled { get; set; }
		#endregion Class properties

		#region Constructor
		/// <summary>
		/// Constructs a SoundEngine object based on the data passed in via the params object
		/// </summary>
		/// <param name="parms"></param>
		public BaseSoundEngine(BaseSoundEngineParams parms) {
			this.Volume = parms.Volume;
			this.Enabled = parms.Enabled;
		}
		#endregion Constructor

		#region Support methods
		/// <summary>
		/// Stops the specified audio from playing
		/// </summary>
		/// <param name="name">Name of the audio file to stop playing</param>
		public abstract void stop(string name);
		#endregion Support methods

		#region Destructor
		/// <summary>
		/// Cleans up resources used by the SoundEngine
		/// </summary>
		public abstract void dispose();
		#endregion
	}
}
