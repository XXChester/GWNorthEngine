using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Media;
namespace GWNorthEngine.Audio.Params {
	/// <summary>
	/// Object containing the required data to build a MusicEngine instance
	/// </summary>
	public class MusicEngineParams : BaseSoundEngineParams {
		#region Class properties
		/// <summary>
		/// Gets or sets the list of Song objects to play in the music engine
		/// </summary>
		public List<Song> PlayList { get; set; }
		/// <summary>
		/// Gets or sets the track to start playing in the music engine
		/// </summary>
		public int StartTrack { get; set; }
		/// <summary>
		/// Gets or sets the state of the music engine
		/// </summary>
		public MediaState State { get; set; }
		#endregion Class properties

		#region Constructor
		/// <summary>
		/// Builds the default settings for setting up a MusicEngine. The default settings are listed below.
		/// 
		/// </summary>
		public MusicEngineParams() {
			this.StartTrack = 0;
			this.State = MediaState.Playing;
		}
		#endregion Constructor
	}
}
