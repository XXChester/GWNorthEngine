using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using GWNorthEngine.Engine.Params;
namespace GWNorthEngine.Engine {
	public abstract class BaseCamera {
		#region Class properties
		public Vector3 Position { get; set; }
		public Vector3 Target { get; set; }
		public Vector3 Up { get; set; }
		public Matrix ViewMatrix { get; set; }
		public Matrix ProjectionMatrix { get; set; }
		#endregion Class properties

		#region Constructor
		public BaseCamera(DefaultCameraParams parms) {
			this.Position = parms.Position;
			this.Target = parms.Target;
			this.Up = parms.Up;
			this.ViewMatrix = parms.ViewMatrix;
			this.ProjectionMatrix = parms.ProjectionMatrix;
		}
		#endregion Constructor

		#region Support methods
		protected virtual void updateViewMatrix() {
			this.ViewMatrix = Matrix.CreateLookAt(this.Position, this.Target, this.Up);
		}

		public abstract void update();
		#endregion Support methods
	}
}
