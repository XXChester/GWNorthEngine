using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
namespace GWNorthEngine.Engine.Params {
	/// <summary>
	/// Object containing the data required to build a camera
	/// </summary>
	public class CameraParams {
		#region Class properties
		/// <summary>
		/// Gets or Sets the position of the Camera
		/// </summary>
		public Vector3 Position { get; set; }
		/// <summary>
		/// Gets or sets the cameras target or look at position
		/// </summary>
		public Vector3 Target { get; set; }
		/// <summary>
		/// Gets or sets the Up vector of the camera
		/// </summary>
		public Vector3 Up { get; set; }
		/// <summary>
		/// Gets or sets the rotation of the camera
		/// </summary>
		public Vector3 Rotation { get; set; }
		/// <summary>
		/// Gets or sets the view matrix of the camera
		/// </summary>
		public Matrix ViewMatrix { get; set; }
		/// <summary>
		/// Gets or sets the projection matrix of the camera
		/// </summary>
		public Matrix ProjectionMatrix { get; set; }
		#endregion Class properties
	}
}
