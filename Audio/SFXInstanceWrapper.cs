using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Audio;
namespace GWNorthEngine.Audio {
	internal class SFXInstanceWrapper {
		#region Class properties
		public SoundEffectInstance Instance { get; set; }
		public string Name { get; set; }
		#endregion Class properties

		#region Constructor
		internal SFXInstanceWrapper(SoundEffectInstance sfxInstance, string name) {
			this.Instance = sfxInstance;
			this.Name = name;
		}
		#endregion Constructor

		#region Destructor
		public void dispose() {
			if (!this.Instance.IsDisposed) {
				if (this.Instance.State != SoundState.Stopped) {
					this.Instance.Stop();
				}
				this.Instance.Dispose();
			}
		}
		#endregion Destructor
	}
}
