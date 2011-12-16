using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using GWNorthEngine.Model.Params;
using GWNorthEngine.Utils;
namespace GWNorthEngine.Model {
	/// <summary>
	/// Textured button
	/// </summary>
	public class TexturedButton : Button {
		#region Class variables
		private Texture2D regularTexture;
		private Texture2D mouseOverTexture;
		private Texture2D activeTexture;
		#endregion Class variables

		#region Class properties
		/// <summary>
		/// Gets or sets the Colour in which the button is to be rendered in
		/// </summary>
		public Color LightColour { get; set; }
		#endregion Class properties

		#region Constructor
		/// <summary>
		/// Building of a textured button
		/// </summary>
		/// <param name="parms">TexturedButtonParams object containing the data required to build the TexturedButton</param>
		public TexturedButton(TexturedButtonParams parms)
			: base(parms) {
			this.regularTexture = parms.RegularTexture;
			this.mouseOverTexture = parms.MouseOverTexture;
			this.activeTexture = this.regularTexture;
		}
		#endregion Construct

		#region Support methods
		/// <summary>
		/// Processes the movement of the actor (Mouse/XBox Controller etc)
		/// </summary>
		/// <param name="actorsPosition">Actors current position</param>
		public override void processActorsMovement(Vector2 actorsPosition) {
			this.activeTexture = this.regularTexture;
			if (base.isActorOver(actorsPosition)) {
				this.activeTexture = this.mouseOverTexture;
			}
		}

		/// <summary>
		/// Rendering of the button
		/// </summary>
		/// <param name="spriteBatch">SpriteBatch object to render the button</param>
		public override void render(SpriteBatch spriteBatch) {
			spriteBatch.Draw(this.activeTexture, base.renderingRectangle, this.LightColour);
		}
		#endregion Support methods

		#region Drestructor
		/// <summary>
		/// Releases the portions of the button that cause memory leaks
		/// </summary>
		public override void dispose() {
			if (this.regularTexture != null) {
				this.regularTexture.Dispose();
				this.regularTexture = null;
			}
			if (this.mouseOverTexture != null) {
				this.mouseOverTexture.Dispose();
				this.mouseOverTexture = null;
			}
			if (this.activeTexture != null) {
				this.activeTexture.Dispose();
				this.activeTexture = null;
			}
		}
		#endregion Destructor
	}
}