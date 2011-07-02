using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Audio;
using GWNorthEngine.Audio.Params;
namespace GWNorthEngine.Audio {
	/// <summary>
	/// Sound effect engine, properly plays sound effects
	/// </summary>
	public class SFXEngine : BaseSoundEngine {
		#region Class variables
		private List<SFXInstanceWrapper> activeInstances;
		#endregion Class variables

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
		/// Builds a SFXEngine based on the parameter objects data
		/// </summary>
		/// <param name="parms">SFXEngineParams object containing the data required to build the SFXEngine</param>
		public SFXEngine(SFXEngineParams parms)
			: base(parms) {
			this.activeInstances = new List<SFXInstanceWrapper>();
			this.Pan = parms.Pan;
			this.Pitch = parms.Pitch;
		}
		#endregion Constructor

		#region Support methods
		/// <summary>
		/// Plays a sound effect with the generic data of the SFXEngine such as volume, etc
		/// </summary>
		/// <param name="sfx">SoundEffect to play</param>
		public void playSoundEffect(SoundEffect sfx) {
			playSoundEffect(sfx, base.Volume, this.Pan, this.Pitch);
		}

		/// <summary>
		/// Plays a sound effect with a specific volume
		/// </summary>
		/// <param name="sfx">SoundEffect to play</param>
		/// <param name="volume">Volume to play the sound effect at</param>
		public void playSoundEffect(SoundEffect sfx, float volume) {
			playSoundEffect(sfx, volume, this.Pan, this.Pitch);
		}

		/// <summary>
		/// Plays a sound effect at a specific volume, pan and pitch
		/// </summary>
		/// <param name="sfx">Sound effect to play</param>
		/// <param name="volume">Volume to play the sound effect at</param>
		/// <param name="pan">Pan to play the sound effect at</param>
		/// <param name="pitch">Pitch to play the sound effect at</param>
		public void playSoundEffect(SoundEffect sfx, float volume, float pan, float pitch) {
			// in certain scenarios some SFXs have to be played differently so we pass in vol, pan and pitch
			if (base.Enabled) {
				// create an instance of the SFX to play, do not actually play the sfx sense we may crash or exit in the middle of the audio and need to stop the audio
				// if we do not do this the client will receive an exception up to the UI because the audio will keep trying to play
				SoundEffectInstance instance = sfx.CreateInstance();
				instance.Volume = volume;
				instance.Pan = pan;
				instance.Pitch = pitch;
				instance.Play();
				this.activeInstances.Add(new SFXInstanceWrapper(instance, sfx.Name));
			}
		}

		/// <summary>
		/// Stops all sound effects that match the name passed in and are not already stopped
		/// </summary>
		/// <param name="name">Name of the sound effect we are trying to stop</param>
		public override void stop(string name) {
			if (this.activeInstances != null) {
				foreach (SFXInstanceWrapper wrapper in this.activeInstances) {
					if (name.Equals(wrapper.Name)) {
						if (wrapper.Instance.State != SoundState.Stopped) {
							wrapper.Instance.Stop();
						}
					}
				}
			}
		}

		/// <summary>
		/// Monitors current SoundEffectInstances and flags them for cleanup if they have finished playing
		/// </summary>
		public void update() {
			if (base.Enabled) {
				if (this.activeInstances != null) {
					List<int> instancesUpForRemoval = new List<int>();
					for (int i = 0; i < this.activeInstances.Count; i++) {
						if (this.activeInstances[i].Instance.State == SoundState.Stopped) {
							instancesUpForRemoval.Add(i);
						}
					}

					// remove flagged instances
					for (int i = instancesUpForRemoval.Count - 1; i >= 0; i--) {
						this.activeInstances[instancesUpForRemoval[i]].Instance.Dispose();
						this.activeInstances.RemoveAt(instancesUpForRemoval[i]);
					}
				}
			}
		}
		#endregion Support methods

		#region Destructor
		/// <summary>
		/// Cleans up the SFXEngines resources
		/// </summary>
		~SFXEngine() {
			dispose();
		}

		/// <summary>
		/// Cleans up the SFXEngines resources
		/// </summary>
		public override void dispose() {
			if (this.activeInstances != null) {
				lock (this.activeInstances) {
					foreach (SFXInstanceWrapper wrapper in this.activeInstances) {
						if (wrapper != null) {
							wrapper.dispose();
						}
					}
				}
			}
		}
		#endregion Destructor
	}
}
