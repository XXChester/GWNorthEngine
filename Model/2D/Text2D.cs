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
	public class Text2D : Base2DSpriteDrawable {
		#region Class variables
		/// <summary>
		/// SpriteFont object
		/// </summary>
		protected SpriteFont font;
		/// <summary>
		/// Text written to the screen
		/// </summary>
		protected string writtenText;
		#endregion Class variables

		#region Class propeties
		/// <summary>
		/// Gets or sets text to be written to the screen
		/// </summary>
		public string WrittenText { get { return this.writtenText; } set { this.writtenText = value; } }
		#endregion Class properties

		#region Constructor
		/// <summary>
		/// Builds an object that can write text to the screen
		/// </summary>
		/// <param name="parms">TextParams object</param>
		public Text2D(Text2DParams parms)
			: base(parms) {
			this.font = parms.Font;
			this.writtenText = parms.WrittenText;
		}
		#endregion Constructor

		#region Support methods
		/// <summary>
		/// Updates the text
		/// </summary>
		/// <param name="elapsed">time elapsed sense the last method call</param>
		public override void update(float elapsed) {
			// TODO: could write a fade in and out routine here later
		}

		/// <summary>
		/// Renders the sprite to the screen
		/// </summary>
		/// <param name="spriteBatch">SpriteBatch object used to render the sprite</param>
		/// <param name="positionOffset">Offset to render the object at</param>
		public override void render(SpriteBatch spriteBatch, Vector2 positionOffset) {
			if (this.writtenText != null) {
				spriteBatch.DrawString(this.font, this.writtenText, Vector2.Add(base.position, positionOffset), base.lightColour,
					base.rotation, base.origin, base.scale, base.spriteEffect, base.layer);
			}
		}
		#endregion Support methods

		#region Destructor

		#endregion Destructor
	}
}
