		using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

using GWNorthEngine.Logic.Params;
namespace GWNorthEngine.Logic {
	/// <summary>
	/// Models the data required for a clampable(confined to a certain space) camera to be used in 2D space
	/// </summary>
	public class ClampableCamera2D : Camera2D {
		#region Class variables
		private Vector2 maxClamp;
		private Vector2 minClamp;
		#endregion Class variables

		#region Constructor
		/// <summary>
		/// Builds a ClampableCamera2D object based on the ClampableCamera2DParams object passed in
		/// </summary>
		/// <param name="parms">ClampableCamera2DParams object containing the data required to build the camera</param>
		public ClampableCamera2D(ClampableCamera2DParams parms)
			:base(parms) {
			this.maxClamp = parms.MaxClamp;
			this.minClamp = parms.MinClamp;
		}
		#endregion Constructor

		#region Support methods
		/// <summary>
		/// Updates the cameras position based on the amount passed in but taking into account the cameras clamping restrictions
		/// </summary>
		/// <param name="moveBy">Amount to move the camera by</param>
		/// <param name="fromPosition">Position the clamping is to be based on</param>
		/// <param name="divisionFactor">Amount the position should be divided by to account for clamping</param>
		public void update(Vector2 moveBy, Vector2 fromPosition, float divisionFactor) {
			float x = fromPosition.X / divisionFactor;
			float y = fromPosition.Y / divisionFactor;

			// horizontal clamping
			if (x <= this.minClamp.X || x > this.maxClamp.X) {
				moveBy.X = 0f;
			}

			// vertical clamping
			if (y <= this.minClamp.Y || y > this.maxClamp.Y) {
				moveBy.Y = 0f;
			}
			update(moveBy);
		}
		#endregion Support methods
	}
}
