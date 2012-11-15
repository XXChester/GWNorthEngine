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

namespace GWNorthEngine.Audio.Params {
	/// <summary>
	/// Models the data required to create a Sound Emitter
	/// </summary>
	public class SoundEmitterParams {
		#region Class properties
		/// <summary>
		/// Position the emitter is to be located at
		/// </summary>
		public Vector2 Position { get; set; }
		/// <summary>
		/// Pan of the emitter
		/// </summary>
		public float Pan { get; set; }
		/// <summary>
		/// Pitch of the emitter
		/// </summary>
		public float Pitch { get; set; }
		/// <summary>
		/// Emission raidus
		/// </summary>
		public float EmittRadius { get; set; }
		/// <summary>
		/// Reference to the SFXEngine that will be used to emitt the sounds
		/// </summary>
		public SFXEngine SFXEngine { get; set; }
		#endregion Class properties

		#region Constructor
		/// <summary>
		/// Builds a default SoundEmitterParams object with the following values\n
		/// Pan:		0f(Centered)
		/// Pitch:		0f(Unity)
		/// </summary>
		public SoundEmitterParams() {
			this.Pan = 0f;
			this.Pitch = 0f;
		}
		#endregion Constructor
	}
}
