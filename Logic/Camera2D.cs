using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

using GWNorthEngine.Engine;
using GWNorthEngine.Logic.Params;

namespace GWNorthEngine.Logic {
	/// <summary>
	/// Models the data required for a camera to be used in 2D space
	/// </summary>
	public class Camera2D {
		#region Class variables
		private Vector2 position;
		#endregion Class variables

		#region CLass properties
		/// <summary>
		/// Gets the position of the camera
		/// </summary>
		public Vector2 Position { get { return this.position; } }
		#endregion Class properties

		#region Constructor
		/// <summary>
		/// Builds a Camer2D object based on the Camera2DParams object passed in
		/// </summary>
		/// <param name="parms">Camera2DParams object containing the data required to build the camera</param>
		public Camera2D(Camera2DParams parms) {
			this.position = parms.StartingPosition;
		}
		#endregion Constructor

		#region Support methods
		/// <summary>
		/// Updates the cameras position by the amount pass in
		/// </summary>
		/// <param name="moveBy">Amount to move the camera by</param>
		public void update(Vector2 moveBy) {
			this.position -= moveBy;
		}
		#endregion Support methods
	}
}
