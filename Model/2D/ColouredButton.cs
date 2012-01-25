using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using GWNorthEngine.Model;
using GWNorthEngine.Model.Params;
using GWNorthEngine.Utils;
namespace GWNorthEngine.Model {
	/// <summary>
	/// Coloured button
	/// </summary>
	public class ColouredButton : IButton {
		#region Class variables
		private Color regularColour;
		private Color mouseOverColour;
		private Line2D[] lines;
		private Text2D text;
		private Rectangle renderingRectangle;
		#endregion Class variables

		#region Class propeties
		/// <summary>
		/// Gets or sets the Text2D object used by the button
		/// </summary>
		public Text2D Text { get { return this.text; } set { this.text = value; } }
		/// <summary>
		/// Gets or sets the Line2D object used by the button
		/// </summary>
		public Line2D[] Lines { get { return this.lines; } set { this.lines = value; } }
		/// <summary>
		/// Gets or sets the ID of the button
		/// </summary>
		public int ID { get; set; }
		#endregion Class properties

		#region Constructor
		/// <summary>
		/// Building of a coloured button
		/// </summary>
		/// <param name="parms"></param>
		public ColouredButton(ColouredButtonParams parms) {
			this.regularColour = parms.RegularColour;
			this.mouseOverColour = parms.MouseOverColour;
			this.renderingRectangle = new Rectangle(parms.StartX, parms.StartY, parms.Width, parms.Height);
			this.ID = parms.ID;
			
			// create our lines
			this.lines = new Line2D[4];
			Line2DParams lineParams = new Line2DParams();
			lineParams.Texture = parms.LinesTexture;
			lineParams.LightColour = this.regularColour;
			
			lineParams.StartPosition = new Vector2(parms.StartX, parms.StartY);
			lineParams.EndPosition = new Vector2(parms.StartX + parms.Width, parms.StartY);
			this.lines[0] = new Line2D(lineParams);

			lineParams.StartPosition = new Vector2(parms.StartX + parms.Width, parms.StartY);
			lineParams.EndPosition = new Vector2(parms.StartX + parms.Width, parms.StartY + parms.Height);
			this.lines[1] = new Line2D(lineParams);

			lineParams.StartPosition = new Vector2(parms.StartX + parms.Width, parms.StartY + parms.Height);
			lineParams.EndPosition = new Vector2(parms.StartX, parms.StartY + parms.Height);
			this.lines[2] = new Line2D(lineParams);

			lineParams.StartPosition = new Vector2(parms.StartX, parms.StartY + parms.Height);
			lineParams.EndPosition = new Vector2(parms.StartX, parms.StartY);
			this.lines[3] = new Line2D(lineParams);

			// create the text
			Text2DParams textParams = new Text2DParams();
			textParams.Font = parms.Font;
			textParams.LightColour = this.regularColour;
			textParams.Position = parms.TextsPosition;
			textParams.WrittenText = parms.Text;
			this.text = new Text2D(textParams);
		}
		#endregion Constructor

		#region Support methods
		/// <summary>
		/// Determines if the actor is over the button
		/// </summary>
		/// <param name="actorsPosition">Actors current position</param>
		/// <returns>boolean based on whether the actor is over the button</returns>
		public bool isActorOver(Vector2 actorsPosition) {
			return (PickingUtils.pickRectangle(actorsPosition, this.renderingRectangle));
		}

		/// <summary>
		/// Processes the movement of the actor (Mouse/XBox Controller etc)
		/// </summary>
		/// <param name="actorsPosition">Actors current position</param>
		public void processActorsMovement(Vector2 actorsPosition) {
			if (isActorOver(actorsPosition)) {
				foreach (Line2D line in this.lines) {
					line.LightColour = this.mouseOverColour;
				}
				this.text.LightColour = this.mouseOverColour;
			} else {
				foreach (Line2D line in this.lines) {
					line.LightColour = this.regularColour;
				}
				this.text.LightColour = this.regularColour;
			}
		}

		/// <summary>
		/// Updates both the Line and text's colour to the colour passed in
		/// </summary>
		/// <param name="newColour">Colour to assign to the Text2D and Lin2D objects</param>
		public void updateColours(Color newColour) {
			foreach (Line2D line in this.lines) {
				line.LightColour = newColour;
			}
			text.LightColour = newColour;
		}

		/// <summary>
		/// Rendering of the button
		/// </summary>
		/// <param name="spriteBatch">SpriteBatch object to render the button</param>
		public void render(SpriteBatch spriteBatch) {
			foreach (Line2D line in this.lines) {
				line.render(spriteBatch);
			}
			this.text.render(spriteBatch);
		}
		#endregion Support methods

		#region Destructor
		/// <summary>
		/// Releases the portions of the button that cause memory leaks
		/// </summary>
		public void dispose() {
			if (this.lines != null) {
				foreach (Line2D line in this.lines) {
					line.dispose();
				}
			}
		}
		#endregion Destructor
	}
}
