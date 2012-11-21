using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace GWNorthEngine.GUI.Params {
	/// <summary>
	/// Models the basic information required to build any GUI element
	/// </summary>
	public abstract class BaseGUIElementParams {
		#region Class properties
		/// <summary>
		/// Colour to render the element in
		/// </summary>
		public Color LightColour { get; set; }
		/// <summary>
		/// ContentManager object
		/// </summary>
		public ContentManager Content { get; set; }
		/// <summary>
		/// Position the GUI element is to reside at
		/// </summary>
		public Vector2 Position { get; set; }
		/// <summary>
		/// Scale of the GUI element
		/// </summary>
		public Vector2 Scale { get; set; }
		#endregion Class properties

		#region Constructor
		/// <summary>
		/// Constructs a basic BaseGUIElementParams object
		/// </summary>
		public BaseGUIElementParams() {
			this.Scale = new Vector2(1f);
		}
		#endregion Constructor
	}
}
