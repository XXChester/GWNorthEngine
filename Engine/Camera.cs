using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using GWNorthEngine.Engine.Params;
namespace GWNorthEngine.Engine {
	/// <summary>
	/// Models the data required for a camera to be used in 3D space
	/// </summary>
	public class Camera {
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

		#region Constructor
		/// <summary>
		/// Creates a Camera based on the data parameter object
		/// </summary>
		/// <param name="parms">CameraParams object</param>
		public Camera(CameraParams parms) {
			this.Position = parms.Position;
			this.Target = parms.Target;
			this.Up = parms.Up;
			this.ViewMatrix = parms.ViewMatrix;
			this.ProjectionMatrix = parms.ProjectionMatrix;
		}
		#endregion Constructor

		#region Support methods
		/// <summary>
		/// Updates the view matrix based on the cameras current position, target and up vector
		/// </summary>
		protected virtual void updateViewMatrix() {
			this.ViewMatrix = Matrix.CreateLookAt(this.Position, this.Target, this.Up);
		}

		/// <summary>
		/// Updates the rotation matrix and applies it to the cameras position, target and up vector (Yaw, Pitch and Roll)
		/// </summary>
		protected virtual void updateRotationMatrix() {
			Matrix rollMatrix = Matrix.CreateFromAxisAngle(this.Target, this.Rotation.Z);
			this.Up = Vector3.Transform(this.Up, rollMatrix);
			this.Position = Vector3.Transform(this.Position, rollMatrix);

			Matrix yawMatrix = Matrix.CreateFromAxisAngle(this.Up, this.Rotation.Y);
			this.Target = Vector3.Transform(this.Target, yawMatrix);
			this.Position = Vector3.Transform(this.Position, yawMatrix);

			Matrix pitchMatrix = Matrix.CreateFromAxisAngle(this.Position, this.Rotation.X);
			this.Target = Vector3.Transform(this.Target, pitchMatrix);
			this.Up = Vector3.Transform(this.Up, pitchMatrix);

			this.Rotation = Vector3.Zero;
		}

		/// <summary>
		/// Updates the camera
		/// </summary>
		public virtual void update() {
			updateRotationMatrix();
			updateViewMatrix();
		}
		#endregion Support methods
	}
}
