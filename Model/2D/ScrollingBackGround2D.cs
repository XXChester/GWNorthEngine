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
using GWNorthEngine.Model;
using GWNorthEngine.Model.Params;
using GWNorthEngine.Input;
using GWNorthEngine.Utils;
namespace GWNorthEngine.Model {
	/// <summary>
	/// Class that handles a scrolling background
	/// </summary>
	public class ScrollingBackGround2D {
		/// <summary>
		/// Directions the scene can scroll in
		/// </summary>
		public enum ScrollingDirection {
			/// <summary>
			/// Not scrolling
			/// </summary>
			None,
			/// <summary>
			/// Scrolls to the left
			/// </summary>
			Left,
			/// <summary>
			/// Scrolls to the right
			/// </summary>
			Right,
			/// <summary>
			/// Scrolls upwards
			/// </summary>
			Up,
			/// <summary>
			/// Scrolls downwards
			/// </summary>
			Down
		}

		#region Class variables
		/// <summary>
		/// List of StaticDrawable2D objects used as the backgrounds
		/// </summary>
		protected List<StaticDrawable2D> backGrounds;
		/// <summary>
		/// Viewport which the background is rendered in
		/// </summary>
		protected Viewport viewPort;
		#endregion Class variables

		#region Class propeties
		/// <summary>
		/// Gets or sets the speed which the scene is scrolling at
		/// </summary>
		public Vector2 Speed { get; set; }
		/// <summary>
		/// Gets or sets the direction the scene is scrolling in
		/// </summary>
		public ScrollingDirection ScrollDirection { get; set; }
		#endregion Class properties

		#region Constructor
		/// <summary>
		/// Constructs the scrolling background
		/// </summary>
		/// <param name="parms">ScrollingBackGround2dParams object containing the data required to build the scrolling background</param>
		public ScrollingBackGround2D(ScrollingBackGround2DParams parms) {
			this.Speed = parms.Speed;
			this.backGrounds = parms.BackGrounds;
			this.ScrollDirection = parms.ScrollingDirection;
			this.viewPort = parms.ViewPort;

			Vector2 previousPosition;
			float previousXSize = 0f;
			float previousYSize = 0f;
			this.backGrounds[0].Position = new Vector2(this.viewPort.X, this.viewPort.Y);
			this.backGrounds[0].RenderingRectangle = new Rectangle(0, 0, (int)(this.backGrounds[0].Texture.Width), (int)(this.backGrounds[0].Texture.Height));
			for (int i = 1; i < this.backGrounds.Count; i++) {
				previousPosition = this.backGrounds[i - 1].Position;
				if (this.ScrollDirection == ScrollingDirection.Left || this.ScrollDirection == ScrollingDirection.Right) {
					previousXSize = this.backGrounds[i - 1].Texture.Width * this.backGrounds[i - 1].Scale.X - 1;
				} else if (this.ScrollDirection == ScrollingDirection.Down || this.ScrollDirection == ScrollingDirection.Up) {
					previousYSize = this.backGrounds[i - 1].Texture.Height * this.backGrounds[i - 1].Scale.Y - 1;
				}
				this.backGrounds[i].Position = new Vector2(previousPosition.X + previousXSize,previousPosition.Y + previousYSize);
				this.backGrounds[i].RenderingRectangle = new Rectangle(0, 0, (int)(this.backGrounds[i].Texture.Width), (int)(this.backGrounds[i].Texture.Height));
			}
		}
		#endregion Constructor

		#region Support methods
		/// <summary>
		/// Updates the scrolling background and handles the wrapping
		/// </summary>
		/// <param name="elapsed">Elapsed time since the last update call</param>
		public virtual void update(float elapsed) {
			// check if any of the images have fallen off the screen, if they have re-order them
			StaticDrawable2D parent;
			int index;
			for (int i = 0; i < this.backGrounds.Count; i++) {
				if (this.backGrounds[i].Position.X < -this.viewPort.Width * this.backGrounds[i].Scale.X) {//left
					if (i == 0) {
						index = this.backGrounds.Count - 1;
					} else {
						index = i - 1;
					}
					parent = this.backGrounds[index];
					this.backGrounds[i].Position = new Vector2(parent.Position.X + parent.RenderingRectangle.Width * parent.Scale.X - 1, this.viewPort.Y);
				} else if (this.backGrounds[i].Position.X > this.viewPort.Width * this.backGrounds[i].Scale.X) {//right
					if (i == this.backGrounds.Count - 1) {
						index = 0;
					} else {
						index = i + 1;
					}
					parent = this.backGrounds[index];
					this.backGrounds[i].Position = new Vector2(parent.Position.X - parent.RenderingRectangle.Width * this.backGrounds[i].Scale.X + 1, this.viewPort.Y);
				} else if (this.backGrounds[i].Position.Y < -this.viewPort.Height * this.backGrounds[i].Scale.Y) {//Up
					if (i == 0) {
						index = this.backGrounds.Count - 1;
					} else {
						index = i - 1;
					}
					parent = this.backGrounds[index];
					this.backGrounds[i].Position = new Vector2(this.viewPort.X, parent.Position.Y + parent.RenderingRectangle.Height * parent.Scale.Y - 1);
				} else if (this.backGrounds[i].Position.Y > this.viewPort.Height * this.backGrounds[i].Scale.Y) {//down
					if (i == this.backGrounds.Count - 1) {
						index = 0;
					} else {
						index = i + 1;
					}
					parent = this.backGrounds[index];
					this.backGrounds[i].Position = new Vector2(this.viewPort.X, parent.Position.Y - parent.RenderingRectangle.Height * this.backGrounds[i].Scale.Y + 1);
				} 
			}

			float directionX = 0f;
			float directionY = 0f;
			if (this.ScrollDirection == ScrollingDirection.Left) {
				directionX = -1f;
			} else if (this.ScrollDirection == ScrollingDirection.Right) {
				directionX = 1f;
			} else if (this.ScrollDirection == ScrollingDirection.Up) {
				directionY = -1f;
			} else if (this.ScrollDirection == ScrollingDirection.Down) {
				directionY = 1f;
			}
			//update each backgrounds position
			for (int i = 0; i < this.backGrounds.Count; i++) {
				this.backGrounds[i].Position = new Vector2(
					this.backGrounds[i].Position.X + (directionX * this.Speed.X * (elapsed / 1000f)),
					this.backGrounds[i].Position.Y + (directionY * this.Speed.Y * (elapsed / 1000f)));
			}
		}

		/// <summary>
		/// Renders the scrolling background
		/// </summary>
		/// <param name="spriteBatch"></param>
		public virtual void render(SpriteBatch spriteBatch) {
			if (this.backGrounds != null) {
				for (int i = 0; i < this.backGrounds.Count; i++) {
					this.backGrounds[i].render(spriteBatch);
				}
			}
		}
		#endregion Support methods

		#region Destructor
		/// <summary>
		/// Disposes of the backgrounds used by the scene
		/// </summary>
		public virtual void dispose() {
			if (this.backGrounds != null) {
				for (int i = 0; i < this.backGrounds.Count; i++) {
					this.backGrounds[i].dispose();
				}
			}
		}
		#endregion Destructor
	}
}