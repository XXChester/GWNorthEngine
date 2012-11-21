using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

using GWNorthEngine.GUI.Params;

namespace GWNorthEngine.GUI {
	/// <summary>
	/// Models the data that every GUI element must have
	/// </summary>
	public abstract class BaseGUIElement {

		#region Class variables
		/// <summary>
		/// Colour to render the element in
		/// </summary>
		protected Color lightColour;
		/// <summary>
		/// ContentManager object
		/// </summary>
		protected ContentManager content;
		/// <summary>
		/// Position the element resides at
		/// </summary>
		protected Vector2 position;
		/// <summary>
		/// Scale of the element
		/// </summary>
		protected Vector2 scale;
		#endregion Class variables

		#region Constructor
		/// <summary>
		/// Constructs a BaseGUIElement object based on the params object
		/// </summary>
		/// <param name="parms">BaseGUIElementParams object</param>
		public BaseGUIElement(BaseGUIElementParams parms) {
			this.lightColour = parms.LightColour;
			this.position = parms.Position;
			this.content = parms.Content;
			this.scale = parms.Scale;
		}
		#endregion Constructor

		#region Support methods
		/// <summary>
		/// Updates the GUI element
		/// </summary>
		/// <param name="elapsed">Time elapsed since the last call</param>
		public abstract void update(float elapsed);

		/// <summary>
		/// Renders the GUI element
		/// </summary>
		/// <param name="spriteBatch">SpriteBatch object used for rendering</param>
		public abstract void render(SpriteBatch spriteBatch);
		#endregion Support methods
	}
}
