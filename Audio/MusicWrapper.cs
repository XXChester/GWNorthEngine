using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Media;
namespace GWNorthEngine.Audio {
	internal class MusicWrapper : BaseWrapper {
		#region Class properties
		public Song Song { get; set; }
		#endregion Class properties

		#region Constructor
		public MusicWrapper(Song song)
			: base(song.Name) {
				this.Song = song;
		}
		#endregion Constructor

		#region Destructor
		public override void dispose() {
			if (this.Song != null) {
				if (!this.Song.IsDisposed) {
					this.Song.Dispose();
				}
			}
		}
		#endregion Destructor
	}
}
