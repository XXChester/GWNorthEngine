using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GWNorthEngine.GUI.Params {
	/// <summary>
	/// Models the data required to build a GUI Slider
	/// </summary>
	public class SliderParams : BaseGUIElementParams {
		#region Class properties
		/// <summary>
		/// Intitial value (between 0 and 1)
		/// </summary>
		public float CurrentValue { get; set; }
		/// <summary>
		/// Scale of the ball
		/// </summary>
		public Vector2 BallScale { get; set; }
		/// <summary>
		/// Font to write the % with
		/// </summary>
		public SpriteFont Font { get; set; }
		/// <summary>
		/// Colour ofthe Ball
		/// </summary>
		public Color BallColour { get; set; }
		/// <summary>
		/// Colour of the Bar
		/// </summary>
		public Color BarColour { get; set; }
		#endregion Class properties

		#region Constructor
		/// <summary>
		/// Constructs a default SliderParams object with the default values below;
		/// Scale(Bar):		Vector2(4f,1f)
		/// BallScale:		Vector2(1f)
		/// </summary>
		public SliderParams() {
			base.Scale = new Vector2(4f, 1f);
			this.BallScale = new Vector2(1f);
		}
		#endregion Constructor
	}
}
