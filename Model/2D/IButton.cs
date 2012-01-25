using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GWNorthEngine.Model {
	/// <summary>
	///  Button interface defining what every button must implement
	/// </summary>
	public interface IButton {
		/// <summary>
		/// Gets or sets the ID of the button
		/// </summary>
		int ID { get; set; }

		/// <summary>
		/// abstract rendering of the base button
		/// </summary>
		/// <param name="spriteBatch">SpriteBatch object used to render the sprite</param>
		void render(SpriteBatch spriteBatch);

		/// <summary>
		/// Processes the actors movment
		/// </summary>
		/// <param name="actorsPosition">Actors current position</param>
		void processActorsMovement(Vector2 actorsPosition);

		/// <summary>
		/// Determines if the actor is over the button
		/// </summary>
		/// <param name="actorsPosition">Actors current position</param>
		/// <returns>boolean based on whether the actor is over the button</returns>
		bool isActorOver(Vector2 actorsPosition);

		/// <summary>
		/// Releases the portions of the button that cause memory leaks
		/// </summary>
		void dispose();
	}
}
