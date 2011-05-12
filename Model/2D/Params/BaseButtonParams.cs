using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
namespace GWNorthEngine.Model.Params {
	/// <summary>
	/// Base class modeling the common data required for a button
	/// </summary>
	public abstract class BaseButtonParams {
		#region Class propeties
		/// <summary>
		/// Gets or sets the starting x positon of the button
		/// </summary>
		public int StartX { get; set; }
		/// <summary>
		/// Gets or sets the starting y position of the button
		/// </summary>
		public int StartY { get; set; }
		/// <summary>
		/// Gets or sets the width of the button
		/// </summary>
		public int Width { get; set; }
		/// <summary>
		/// Gets or sets the height of the button
		/// </summary>
		public int Height { get; set; }
		/// <summary>
		/// Buttons ID used for determining if a button was clicked
		/// </summary>
		public int ID { get; set; }
		#endregion Class properties
	}
}
