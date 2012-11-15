using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using GWNorthEngine.Model.Params;
namespace GWNorthEngine.Model {
	/// <summary>
	/// Text class for writing text to the screen
	/// </summary>
	public class StaticDrawable2D : Base2DSpriteDrawable {
		#region Class variables
		/// <summary>
		/// Texture2D object
		/// </summary>
		protected Texture2D texture;
		#endregion Class variables

		#region Class propeties
		/// <summary>
		/// Gets or sets the Texture of the static drawable object
		/// </summary>
		public Texture2D Texture { get { return this.texture; } set { this.texture = value; } }
		#endregion Class properties

		#region Constructor
		/// <summary>
		/// Builds an object that can draw a texture to the screen
		/// </summary>
		/// <param name="parms">StaticDrawable2DParams object</param>
		public StaticDrawable2D(StaticDrawable2DParams parms)
			: base(parms) {
			this.texture = parms.Texture;
			base.setRenderingRectByTexture(this.texture);
		}
		#endregion Constructor

		#region Support methods
		/// <summary>
		/// Renders the sprite to the screen
		/// </summary>
		/// <param name="spriteBatch">SpriteBatch object used to render the sprite</param>
		/// <param name="positionOffset">Offset to render the object at</param>
		public override void render(SpriteBatch spriteBatch, Vector2 positionOffset) {
			spriteBatch.Draw(this.texture, Vector2.Add(base.position, positionOffset), base.renderingRectangle, base.lightColour,
				base.rotation, base.origin, base.scale, base.spriteEffect, base.layer);
		}
		#endregion Support methods

		#region Destructor
		/// <summary>
		/// Disposes the texture that was being drawn
		/// </summary>
		public virtual void dispose() {
			if (this.texture != null) {
				this.texture.Dispose();
			}
		}
		#endregion Destructor
	}
}
