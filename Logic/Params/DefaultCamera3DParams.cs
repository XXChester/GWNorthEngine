using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
namespace GWNorthEngine.Logic.Params {
	/// <summary>
	/// Object containing the data required to build a default camera
	/// </summary>
	public class DefaultCamera3DParams : Camera3DParams {
		#region Constructor
		/// <summary>
		/// Constructs a default camera setup with the following values
		/// Position:			Vector3(0f,0f,50f)
		/// Target:				Vector3.Zero
		/// UpVector:			Vector3.Up
		/// ViewMatrix:			Matrix.Identity
		/// ProjectionMatrix:	Matrix.createPerspectiveFOV(45f, 16:9, .5f, 5000f)
		/// </summary>
		public DefaultCamera3DParams() {
			base.Position = new Vector3(0f, 0f, 50f);
			base.Target = Vector3.Zero;
			base.Up = Vector3.Up;
			base.ViewMatrix = Matrix.Identity;
			float standardFieldOfView = 45f;
			float standardAspectRatio = 16f / 9f;
			float nearClippingPlane = 0.5f;
			float farClippingPlane = 5000f;
			base.ProjectionMatrix = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(standardFieldOfView), standardAspectRatio,
				nearClippingPlane, farClippingPlane);
		}
		#endregion Constructor
	}
}
