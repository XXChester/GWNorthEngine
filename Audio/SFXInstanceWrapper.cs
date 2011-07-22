using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Audio;
namespace GWNorthEngine.Audio {
	internal class SFXInstanceWrapper : BaseWrapper {
		#region Class properties
		public SoundEffectInstance Instance { get; set; }
		#endregion Class properties

		#region Constructor
		internal SFXInstanceWrapper(SoundEffectInstance sfxInstance, string name) 
			:base(name) {
			this.Instance = sfxInstance;
		}
		#endregion Constructor

		#region Destructor
		public override void dispose() {
			if (!this.Instance.IsDisposed) {
				if (this.Instance.State != SoundState.Stopped) {
					this.Instance.Stop();
				}
				//Commented out dispose to fix a MemmoryAccessViolation bug in the XNA Framwork. Have to let the ContentManager handle the cleanup of sfx's
				//this.Instance.Dispose();
			}
		}
		#endregion Destructor
	}
}
