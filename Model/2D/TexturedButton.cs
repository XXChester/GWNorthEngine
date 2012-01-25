using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using GWNorthEngine.Model.Params;
using GWNorthEngine.Utils;
namespace GWNorthEngine.Model {
	/// <summary>
	/// Textured button
	/// </summary>
	public class TexturedButton : StaticDrawable2D, IButton {
		#region Class variables
		private Texture2D regularTexture;
		private Texture2D mouseOverTexture;
		#endregion Class variables

		#region Class properties
		/// <summary>
		/// Gets or sets the ID of the button
		/// </summary>
		public int ID { get; set; }
		#endregion Class properties

		#region Constructor
		/// <summary>
		/// Building of a textured button
		/// </summary>
		/// <param name="parms">TexturedButtonParams object containing the data required to build the TexturedButton</param>
		public TexturedButton(TexturedButtonParams parms)
			: base(parms) {
			base.texture = parms.RegularTexture;
			base.setRenderingRectByTexture(base.texture);
			this.regularTexture = parms.RegularTexture;
			this.mouseOverTexture = parms.MouseOverTexture;
			this.ID = parms.ID;
		}
		#endregion Construct

		#region Support methods
		/// <summary>
		/// Determines if the actor is over the button
		/// </summary>
		/// <param name="actorsPosition">Actors current position</param>
		/// <returns>boolean based on whether the actor is over the button</returns>
		public bool isActorOver(Vector2 actorsPosition) {
			int x1 = (int)(base.position.X - base.origin.X);
			int y1 = (int)(base.position.Y - base.origin.Y);
			int x2 = (int)(base.texture.Width);
			int y2 = (int)(base.texture.Height);
			return (PickingUtils.pickRectangle(actorsPosition, new Rectangle(x1, y1, x2, y2)));
		}

		/// <summary>
		/// Processes the movement of the actor (Mouse/XBox Controller etc)
		/// </summary>
		/// <param name="actorsPosition">Actors current position</param>
		public void processActorsMovement(Vector2 actorsPosition) {
			this.texture = this.regularTexture;
			if (isActorOver(actorsPosition)) {
				this.texture = this.mouseOverTexture;
			}
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
			base.dispose();
		}
		#endregion Destructor
	}
}