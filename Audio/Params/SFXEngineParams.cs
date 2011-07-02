using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GWNorthEngine.Audio.Params {
	/// <summary>
	/// Object containing the required data to build a SFXEngine instance
	/// </summary>
	public class SFXEngineParams : BaseSoundEngineParams {
		#region Class properties
		/// <summary>
		/// Gets or sets the panning of the sound effects
		/// </summary>
		public float Pan { get; set; }
		/// <summary>
		/// Gets or sets the pitch of the sound effects
		/// </summary>
		public float Pitch { get; set; }
		#endregion Class properties

		#region Constructor
		/// <summary>
		/// Builds the default settings for setting up a SFXEngine. The default settings are listed below.
		/// Pan:		0f(Centered)
		/// Pitch:		0f(Unity)
		/// </summary>
		public SFXEngineParams() {
			this.Pan = 0f;
			this.Pitch = 0f;
		}
		#endregion Constructor
	}
}
