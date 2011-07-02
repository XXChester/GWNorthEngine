using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GWNorthEngine.Audio.Params {
	/// <summary>
	/// Object containing the data required to build the BaseSoundEngine
	/// </summary>
	public abstract class BaseSoundEngineParams {
		#region Class properties
		/// <summary>
		/// Gets or sets the volume to play sound effects at
		/// </summary>
		public float Volume { get; set; }
		/// <summary>
		/// Gets or sets whether audio is to be played
		/// </summary>
		public bool Muted { get; set; }
		#endregion Class properties

		#region Constructor
		/// <summary>
		/// Builds the default settings for setting up a BaseSoundEngine. The default settings are listed below
		/// Volume:		100%(1f)
		/// Muted:	false
		/// </summary>
		public BaseSoundEngineParams() {
			this.Volume = 1f;
			this.Muted = false;
		}
		#endregion Constructor
	}
}
