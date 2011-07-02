using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Media;
using GWNorthEngine.Audio.Params;
namespace GWNorthEngine.Audio {
	/// <summary>
	/// Music engine, handles playing a play list
	/// </summary>
	public class MusicEngine : BaseSoundEngine {
		#region Class variables
		private List<MusicWrapper> playList;
		private int trackNumber;
		#endregion Class variables

		#region Class properties
		/// <summary>
		/// Gets the current track that is playing
		/// </summary>
		public Song currentTrack {
			get {
				return this.playList[this.trackNumber].Song;
			}
		}

		/// <summary>
		/// Gets or sets whether the engine is muted
		/// </summary>
		public override bool Muted { 
			set { 
				base.Muted = value;
				MediaPlayer.IsMuted = value;
			}
		}

		/// <summary>
		/// Gets or sets the volume of the engine
		/// </summary>
		public override float Volume {
			set {
				base.Volume = value;
				MediaPlayer.Volume = base.Volume;
			}
		}
		#endregion Class properties

		#region Constructor

		#endregion Constructor
		/// <summary>
		/// Builds a MusicEngine based on the data passed in via the parameter object
		/// </summary>
		/// <param name="parms">MusicEngineParams object containing the required data</param>
		public MusicEngine(MusicEngineParams parms)
			: base(parms) {
			this.playList = new List<MusicWrapper>();
			this.trackNumber = parms.StartTrack;
			add(parms.PlayList);

			MediaPlayer.Volume = base.Volume;
			MediaPlayer.IsMuted = base.Muted;
			if (parms.State == MediaState.Playing && this.playList.Count > 0) {
				play();
			} else {
				MediaPlayer.Stop();
			}
		}
		#region Support methods
		/// <summary>
		/// Adds a Song object to the play list
		/// </summary>
		/// <param name="song">Song object to add</param>
		public void add(Song song) {
			this.playList.Add(new MusicWrapper(song));
		}

		/// <summary>
		/// Adds a list of Song objects to the play list
		/// </summary>
		/// <param name="playList"></param>
		public void add(List<Song> playList) {
			if (playList != null) {
				foreach (Song song in playList) {
					add(song);
				}
			}
		}

		/// <summary>
		/// Plays the next track in the play list
		/// </summary>
		public void playNextTrack() {
			if (this.trackNumber == this.playList.Count - 1) {
				this.trackNumber = 0;
			} else {
				this.trackNumber++;
			}
			play();
		}

		/// <summary>
		/// Plays the previous track in the play list
		/// </summary>
		public void playPreviousTrack() {
			if (this.trackNumber == 0) {
				this.trackNumber = this.playList.Count - 1;
			} else {
				this.trackNumber--;
			}
			play();
		}

		/// <summary>
		/// Plays whichever track is up
		/// </summary>
		public void play() {
				stop();
				MediaPlayer.Play(this.playList[this.trackNumber].Song);
		}

		/// <summary>
		/// Stops the current track
		/// </summary>
		public void stop() {
			if (MediaPlayer.State != MediaState.Stopped) {
				MediaPlayer.Stop();
			}
		}

		/// <summary>
		/// Pauses the current track
		/// </summary>
		public void pause() {
			if (MediaPlayer.State != MediaState.Paused) {
				MediaPlayer.Pause();
			}
		}

		/// <summary>
		/// Unpauses the current track
		/// </summary>
		public void unpause() {
			if (MediaPlayer.State == MediaState.Paused) {
				MediaPlayer.Resume();
			}
		}
		#endregion Support methods

		#region Destructor
		/// <summary>
		/// Cleans up resources used by the music engine
		/// </summary>
		public override void dispose() {
			if (this.playList != null) {
				foreach (MusicWrapper wrapper in this.playList) {
					wrapper.dispose();
				}
			}
		}
		#endregion Destructor
	}
}
