using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using GWNorthEngine.Engine;

namespace GWNorthEngine.Model.Params {
	/// <summary>
	/// Object containing the data required to build a 3D model
	/// </summary>
	public class BaseModelParams {
		#region Class properties
		/// <summary>
		/// Gets or sets the core XNA Model used for rendering
		/// </summary>
		public Microsoft.Xna.Framework.Graphics.Model Model { get; set; }
		/// <summary>
		/// Gets or sets the position of the model
		/// </summary>
		public Vector3 Position { get; set; }
		/// <summary>
		/// Gets or sets the rotation of the model
		/// </summary>
		public Vector3 Rotation { get; set; }
		/// <summary>
		/// Gets or sets the scale of the model
		/// </summary>
		public Vector3 Scale { get; set; }
		/// <summary>
		/// Gets or sets whether the model is to be drawn
		/// </summary>
		public bool Visible { get; set; }
		#endregion Class properties

		#region Constructor
		/// <summary>
		/// Builds the default settings for the ModelParams. The default settings are below;
		/// Scale:		new Vector3(1f)
		/// </summary>
		public BaseModelParams() {
			this.Scale = new Vector3(1f);
			this.Visible = true;
		}
		#endregion Constructor
	}
}
