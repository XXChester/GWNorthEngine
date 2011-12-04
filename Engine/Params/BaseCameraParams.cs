using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
namespace GWNorthEngine.Engine.Params {
	public abstract class BaseCameraParams {
		#region Class properties
		public Vector3 Position { get; set; }
		public Vector3 Target { get; set; }
		public Vector3 Up { get; set; }
		public Matrix ViewMatrix { get; set; }
		public Matrix ProjectionMatrix { get; set; }
		#endregion Class properties
	}
}
