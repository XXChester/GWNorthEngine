using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace GWNorthEngine.Engine {
	/// <summary>
	/// Frames per second class
	/// </summary>
	public class FrameRate {
		#region Class variables
		// singleton instance variable
		private static FrameRate instance = new FrameRate();
		private int lastTick;
		private int lastFrameRate;
		private int frameRate;
		#endregion Class variables

		#region Constructor

		#endregion Constructor

		#region Support methods
		/// <summary>
		/// Retrieves the singleton instance variable
		/// </summary>
		/// <returns></returns>
		public static FrameRate getInstance() {
			return instance;
		}

		/// <summary>
		/// Calculates and returns the frames per second
		/// </summary>
		/// <param name="gameTime">GameTime object</param>
		/// <returns>Current frames per second (FPS)</returns>
		public int calculateFrameRate(GameTime gameTime) {
			if (gameTime.TotalGameTime.TotalMilliseconds - this.lastTick >= 1000) {
				this.lastFrameRate = this.frameRate;
				this.frameRate = 0;
				this.lastTick = (int)gameTime.TotalGameTime.TotalMilliseconds;
			}
			this.frameRate++;
			return this.lastFrameRate;
		}
		#endregion Support methods
	}
}
