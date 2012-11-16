using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

using GWNorthEngine.Engine;
namespace GWNorthEngine.Logic.Params {
	/// <summary>
	/// Models the data required to build a Camera2D object
	/// </summary>
	public class Camera2DParams {
		#region Class properties
		/// <summary>
		/// Gets or sets the starting position of the camera
		/// </summary>
		public Vector2 StartingPosition { get; set; }
		#endregion Class properties
	}
}
