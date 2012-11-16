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
using GWNorthEngine.Engine;
using GWNorthEngine.Model.Params;
namespace GWNorthEngine.Model {
	/// <summary>
	/// Line class that simply draws a line to the screen
	/// </summary>
	public class Line2D : Base2DSpriteDrawable {
		#region Class variables
		/// <summary>
		/// Ending position of the Line
		/// </summary>
		protected Vector2 endPosition;
		/// <summary>
		/// Texture used to draw the line
		/// </summary>
		protected Texture2D texture;
		#endregion Class variables

		#region Class properties
		/// <summary>
		/// Gets or sets the starting position of the line
		/// </summary>
		public override Vector2 Position {
			get {
				return base.Position;
			}
			set {
				base.Position = value;
				recalculateRotation();
				recalculateScale();
			}
		}

		/// <summary>
		/// Gets or sets the end position of the line
		/// </summary>
		public Vector2 EndPosition {
			get {
				return this.endPosition;
			}
			set {
				this.endPosition = value;
				recalculateRotation();
				recalculateScale();
			}
		}
		#endregion Class properties

		#region Constructor
		/// <summary>
		/// Actual building of the line
		/// </summary>
		/// <param name="parms">Line2DParams object containing the data required to build the line</param>
		public Line2D(Line2DParams parms)
			:base(parms){
			base.position = parms.StartPosition;
			this.endPosition = parms.EndPosition;
			this.texture = parms.Texture;
			
			recalculateRotation();
			recalculateScale();
		}
		#endregion Constructor

		#region Initialization

		#endregion Initialization

		#region Support methods
		/// <summary>
		/// Recalculates the rotation based on the current starting and end positions of the line
		/// </summary>
		public void recalculateRotation() {
			Vector2 difference = this.endPosition - base.position;
			base.rotation = (float)(Math.Atan2(difference.Y, difference.X)) - MathHelper.PiOver2;
		}

		/// <summary>
		/// Recalculates the scale based on the current starting and end positions of the line
		/// </summary>
		public void recalculateScale() {
			Vector2 difference = this.endPosition - base.position;
			base.scale = new Vector2(1.0f, difference.Length() / this.texture.Height);
		}

		/// <summary>
		/// Renders the sprite to the screen
		/// </summary>
		/// <param name="spriteBatch">SpriteBatch object used to render the sprite</param>
		/// <param name="positionOffset">Offset to render the object at</param>
		public override void render(SpriteBatch spriteBatch, Vector2 positionOffset) {
			spriteBatch.Draw(this.texture, Vector2.Add(base.position, positionOffset), null, base.lightColour, base.rotation, 
				base.origin, base.scale, base.spriteEffect, base.layer);
		}
		#endregion Support methods

		#region Destructor
		/// <summary>
		/// Disposes the texture used to draw the line
		/// </summary>
		public void dispose() {
			if (this.texture != null) {
				this.texture.Dispose();
			}
		}
		#endregion Destructor
	}
}
