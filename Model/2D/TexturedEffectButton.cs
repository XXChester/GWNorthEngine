using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using GWNorthEngine.Engine;
using GWNorthEngine.Model.Effects;
using GWNorthEngine.Model.Params;
using GWNorthEngine.Utils;
namespace GWNorthEngine.Model {
	/// <summary>
	/// Textured effect button
	/// </summary>
	public class TexturedEffectButton : StaticDrawable2D, IButton {
		#region Class variables
		private bool previousActorOver;
		private List<BaseEffect> effects;
		private resetDrawableDelegate reset;
		private Rectangle pickableArea;
		#endregion Class variables

		#region Class properties
		/// <summary>
		/// Gets or sets the ID of the button
		/// </summary>
		public int ID { get; set; }
		public Rectangle PickableArea { get { return this.pickableArea; } }
		#endregion Class properties

		#region Constructor
		/// <summary>
		/// Building of a textured button
		/// </summary>
		/// <param name="parms">TexturedButtonParams object containing the data required to build the TexturedButton</param>
		public TexturedEffectButton(TexturedEffectButtonParams parms)
			: base(parms) {
			base.setRenderingRectByTexture(base.texture);
			this.ID = parms.ID;
			this.effects = parms.Effects;
			this.reset = parms.ResetDelegate;
			this.pickableArea = parms.PickableArea;
		}
		#endregion Construct

		#region Support methods
		/// <summary>
		/// Determines if the actor is over the button
		/// </summary>
		/// <param name="actorsPosition">Actors current position</param>
		/// <returns>boolean based on whether the actor is over the button</returns>
		public bool isActorOver(Vector2 actorsPosition) {
			return PickingUtils.pickRectangle(actorsPosition, this.pickableArea);
		}

		/// <summary>
		/// Processes the movement of the actor (Mouse/XBox Controller etc)
		/// </summary>
		/// <param name="actorsPosition">Actors current position</param>
		public void processActorsMovement(Vector2 actorsPosition) {
			bool hovering = isActorOver(actorsPosition);
			if (this.previousActorOver && !hovering) {
				// remove effects
				if (this.effects != null) {
					foreach (BaseEffect effect in this.effects) {
						base.Effects.Remove(effect);
					}
				}
				this.reset(this);
			} else if (!this.previousActorOver && hovering) {
				// add effect
				if (this.effects != null) {
					foreach (BaseEffect effect in this.effects) {
						base.addEffect(effect);
					}
				}
			} 
			this.previousActorOver = hovering;
		}
		#endregion Support methods
	}
}