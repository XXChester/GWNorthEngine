using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using GWNorthEngine.Utils;
using GWNorthEngine.Model.Params;
namespace GWNorthEngine.Model {
	/// <summary>
	/// Abstract button class containing the basis for every button
	/// </summary>
	public abstract class Button {
		#region Class variables
		/// <summary>
		/// Rectangle used to render the button
		/// </summary>
		protected Rectangle renderingRectangle;
		#endregion Class variables

		#region Class properties
		/// <summary>
		/// Gets or sets the ID of the button
		/// </summary>
		public int ID { get; set; }
		#endregion Class properties

		#region Constructor
		/// <summary>
		/// Builds the base of the button
		/// </summary>
		/// <param name="parms">BaseButtonParams object to create the button from</param>
		public Button(BaseButtonParams parms) {
			this.renderingRectangle = new Rectangle(parms.StartX, parms.StartY, parms.Width, parms.Height);
			this.ID = parms.ID;
		}
		#endregion Constructor

		#region Support methods
		/// <summary>
		/// abstract rendering of the base button
		/// </summary>
		/// <param name="spriteBatch">SpriteBatch object used to render the sprite</param>
		public abstract void render(SpriteBatch spriteBatch);

		/// <summary>
		/// Processes the actors movment
		/// </summary>
		/// <param name="actorsPosition">Actors current position</param>
		public abstract void processActorsMovement(Vector2 actorsPosition);

		/// <summary>
		/// Determines if the actor is over the button
		/// </summary>
		/// <param name="actorsPosition">Actors current position</param>
		/// <returns>boolean based on whether the actor is over the button</returns>
		public virtual bool isActorOver(Vector2 actorsPosition) {
			return (PickingUtils.pickRectangle(actorsPosition, this.renderingRectangle));
		}
		#endregion Support methods

		#region Destructor
		/// <summary>
		/// abstract disposal of the base button
		/// </summary>
		public abstract void dispose();
		#endregion Destructor
	}
}
