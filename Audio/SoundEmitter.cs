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
using GWNorthEngine.Audio;
using GWNorthEngine.Audio.Params;
using GWNorthEngine.Model;
using GWNorthEngine.Model.Params;
using GWNorthEngine.Input;

namespace GWNorthEngine.Audio {
	/// <summary>
	/// Models a SoundEmitter that plays sounds dynamic to a position
	/// </summary>
	public class SoundEmitter {
		#region Class variables
		private float pan;
		private float pitch;
		private SoundEffectInstance sfxInstance;
		private SFXEngine sfxEngine;
		#endregion Class variables

		#region Class properties
		/// <summary>
		/// Position of the Sound Emitter
		/// </summary>
		public Vector2 Position { get; set; }
		/// <summary>
		/// Emission radius
		/// </summary>
		public float EmittRadius { get; set; }
		#endregion Class properties

		#region Constructor
		/// <summary>
		/// Creates a SoundEmitter object based on the params
		/// </summary>
		/// <param name="parms">SoundEmitterParams object</param>
		public SoundEmitter(SoundEmitterParams parms) {
			this.pan = parms.Pan;
			this.pitch = parms.Pitch;
			this.EmittRadius = parms.EmittRadius;
			this.Position = parms.Position;
			this.sfxEngine = parms.SFXEngine;
		}
		#endregion Constructor

		#region Support methods
		private void determineEmission(Vector2 listenersPosition, out float volume, out float pan) {
			// determine the values based on the listeners position & radius
			Vector2 space = new Vector2(MathHelper.Max(this.Position.X, listenersPosition.X) -
				MathHelper.Min(this.Position.X, listenersPosition.X), MathHelper.Max(this.Position.Y, listenersPosition.Y) - 
				MathHelper.Min(this.Position.Y, listenersPosition.Y));
			float difference = (space.X + space.Y) / 2f;
			float delta = (difference / this.EmittRadius);
			float invDelta = 1 - delta;
			if (difference > this.EmittRadius) {
				volume = 0f;
			} else {
				volume = invDelta;
			}

			pan = MathHelper.Clamp(((this.Position.X - listenersPosition.X) / this.EmittRadius), -1f, 1f);

			//Console.WriteLine("Delta: " + delta + "\tInverseDelta: " + invDelta);
			//Console.WriteLine("Vol: " + volume + "\t\tPan: " + pan);
		}

		/// <summary>
		/// Plays a sound effect in relation to a listening position
		/// </summary>
		/// <param name="sfx">SoundEffect to play</param>
		/// <param name="listenersPosition">Position the listener is currently at</param>
		/// <param name="pitch">Pitch of the sound effect</param>
		/// <param name="loop">Whether we are looping the sound effect or not</param>
		public void playSoundEffect(SoundEffect sfx, Vector2 listenersPosition, float pitch = 0f, bool loop = false) {
			float volume, pan;
			determineEmission(listenersPosition, out volume, out pan);
			this.sfxInstance = this.sfxEngine.playSoundEffect(sfx, volume, pan, pitch, loop);
		}

		/// <summary>
		/// Updates the emitter based on the listeners position
		/// </summary>
		/// <param name="listenersPosition"></param>
		public void update(Vector2 listenersPosition) {
			update(new Vector2[] { listenersPosition });
		}

		/// <summary>
		/// Updates the emitter based on the listeners positions
		/// </summary>
		/// <param name="listenersPositions">Vector2[] of the positions that we need to take into account for listening</param>
		public void update(Vector2[] listenersPositions) {
			if (this.sfxInstance != null) {
				float masterVol = 0f;
				float masterPan = 0f;
				float volume;
				float pan;
				foreach (Vector2 listenersPosition in listenersPositions) {
					determineEmission(listenersPosition, out volume, out pan);
					masterVol += volume;
					masterPan += pan;
				}
				this.sfxInstance.Pan = masterPan / listenersPositions.Length;
				this.sfxInstance.Volume = masterVol / listenersPositions.Length;
			}
		}
		#endregion Supoprt methods
	}
}
