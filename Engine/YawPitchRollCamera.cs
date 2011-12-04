using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

using GWNorthEngine.Engine.Params;
namespace GWNorthEngine.Engine {
	public class YawPitchRollCamera : DefaultCamera {
		#region Class properties
		public Vector3 Rotation { get; set; }
		#endregion Class properties

		#region Constructor
		public YawPitchRollCamera(DefaultCameraParams parms)
			: base(parms) {

		}
		#endregion Constructor

		#region Support methods
		public virtual void updateRotationMatrix() {
			Matrix rollMatrix = Matrix.CreateFromAxisAngle(base.Target, this.Rotation.Z);
			base.Up = Vector3.Transform(base.Up, rollMatrix);
			base.Position = Vector3.Transform(base.Position, rollMatrix);

			Matrix yawMatrix = Matrix.CreateFromAxisAngle(base.Up, this.Rotation.Y);
			base.Target = Vector3.Transform(base.Target, yawMatrix);
			base.Position = Vector3.Transform(base.Position, yawMatrix);

			Matrix pitchMatrix = Matrix.CreateFromAxisAngle(base.Position, this.Rotation.X);
			base.Target = Vector3.Transform(base.Target, pitchMatrix);
			base.Up = Vector3.Transform(base.Up, pitchMatrix);

			this.Rotation = Vector3.Zero;
		}

		public override void update() {
			updateRotationMatrix();
			base.updateViewMatrix();
		}
		#endregion Support methods
	}
}
