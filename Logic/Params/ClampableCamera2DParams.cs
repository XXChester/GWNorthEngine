using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

namespace GWNorthEngine.Logic.Params {
	/// <summary>
	/// Models the data required to build a ClampableCamera2D object
	/// </summary>
	public class ClampableCamera2DParams : Camera2DParams {
		#region Class properties
		/// <summary>
		/// Gets or sets the Maximum values to clamp the camera at
		/// </summary>
		public Vector2 MaxClamp { get; set; }
		/// <summary>
		/// Gets or sets the Minimum values to clamp the camera at
		/// </summary>
		public Vector2 MinClamp { get; set; }
		#endregion Class properties
	}
}
