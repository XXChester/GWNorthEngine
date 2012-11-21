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
using GWNorthEngine.Engine.Params;
using GWNorthEngine.GUI.Params;
using GWNorthEngine.Model;
using GWNorthEngine.Model.Params;
using GWNorthEngine.Logic;
using GWNorthEngine.Logic.Params;
using GWNorthEngine.Input;
using GWNorthEngine.Utils;
using GWNorthEngine.Scripting;

namespace GWNorthEngine.GUI {
	/// <summary>
	/// Models a GUI Slider
	/// </summary>
	public class Slider : BaseGUIElement {
		#region Class variables
		private StaticDrawable2D bar;
		private StaticDrawable2D ball;
		private Text2D text;
		private BoundingBox bbox;
		private bool alreadyPicked;
		private float currentValue;
		private float initialX;
		private const float MAX_VALUE = 1f;
		private const float MIN_VALUE = 0f;
		#endregion Class variables

		#region Class propeties
		/// <summary>
		/// Value of the slider between 0 and 1
		/// </summary>
		public float CurrentValue { get { return this.currentValue; } set { this.currentValue = MathHelper.Clamp(value, MIN_VALUE, MAX_VALUE); } }
		#endregion Class properties

		#region Constructor
		/// <summary>
		/// Constructs a Slider object based on the params object
		/// </summary>
		/// <param name="parms">SliderParams object</param>
		public Slider(SliderParams parms) : base(parms) {
			this.CurrentValue = parms.CurrentValue;
			this.initialX = parms.Position.X;
			Texture2D texture = LoadingUtils.load<Texture2D>(parms.Content, Constants.GUI_FILE_NAME);

			StaticDrawable2DParams imgParms = new StaticDrawable2DParams {
				Position = parms.Position,
				LightColour = parms.BarColour,
				Scale = parms.Scale,
				RenderingRectangle = Constants.SLIDER_BAR,
				Texture = texture,
				Origin = new Vector2(Constants.SLIDER_BAR.Width / 2, Constants.SLIDER_BAR.Height / 2)
			};
			this.bar = new StaticDrawable2D(imgParms);

			imgParms.Scale = parms.BallScale;
			imgParms.LightColour = parms.BallColour;
			imgParms.RenderingRectangle = Constants.SLIDER_BALL;
			imgParms.Origin = new Vector2(Constants.SLIDER_BALL.Width / 2, Constants.SLIDER_BALL.Height / 2);
			this.ball = new StaticDrawable2D(imgParms);
			setPosition();

			Text2DParams textParms = new Text2DParams {
				Position = new Vector2(parms.Position.X + 100f, parms.Position.Y),
				LightColour = parms.LightColour,
				WrittenText = getValue(),
				Font = parms.Font,
				Origin = new Vector2(16f, 16f),
			};
			this.text = new Text2D(textParms);
		}
		#endregion Constructor

		#region Support methods
		private void getBBox() {
			// flip the origins since we rotated 90 degrees
			float halfOriginX = this.ball.Origin.Y / 2;
			float halfOriginY = this.ball.Origin.X / 2;

			float halfSize = (Constants.SLIDER_BALL.Height / 2);
			Vector3 min = new Vector3(this.ball.Position.X - halfOriginX, this.ball.Position.Y - halfOriginY - halfSize, 0f);
			Vector3 max = new Vector3(this.ball.Position.X + halfOriginX, this.ball.Position.Y + halfSize + halfOriginY, 0f);
			this.bbox = new BoundingBox(min, max);
		}

		private void setPosition() {
			// set the position based on the scale, position, current value
			// base on the Y values because we rotated the slider
			float sizeScaled = (Constants.SLIDER_BAR.Width * this.bar.Scale.X) / 2;
			float maxSize = initialX + sizeScaled;
			float minSize = initialX - sizeScaled;

			float delta = (maxSize - minSize) / 100;
			float wholeValue = this.currentValue * 100;
			float sizeToValueRelation = wholeValue * delta;
			this.ball.Position = new Vector2(minSize + sizeToValueRelation, this.ball.Position.Y);
			getBBox();
		}

		private string getValue() {
			string value = (CurrentValue * 100).ToString();
			if (value.Length > 3 && this.currentValue < .1) {
				value = value.Substring(0, 1);
			} else if (value.Length > 3 && this.currentValue < 1) {
				value = value.Substring(0, 2);
			}
			return value + "%";
		}

		/// <summary>
		/// Updates the slider
		/// </summary>
		/// <param name="elapsed">Time elapsed since the last call</param>
		public override void update(float elapsed) {
			if (this.alreadyPicked) {
				Vector2 delta = InputManager.getInstance().MousePosition - InputManager.getInstance().PreviousMousePosition;
				if (delta.X < 0) {
					this.CurrentValue -= .05f;
					setPosition();
				} else if (delta.X > 0) {
					this.CurrentValue += .05f;
					setPosition();
				}
			}
			
			if (InputManager.getInstance().isLeftButtonDown()) {
				if (PickingUtils.pickVector(InputManager.getInstance().MousePosition, this.bbox)) {
					this.alreadyPicked = true;
				}
			} else if (!InputManager.getInstance().isLeftButtonDown()) {
				this.alreadyPicked = false;
			}
			this.bar.update(elapsed);
			this.ball.update(elapsed);
			this.text.WrittenText = getValue();
			this.text.update(elapsed);
		}

		/// <summary>
		/// Renders the Slider
		/// </summary>
		/// <param name="spriteBatch">SpriteBatch object used for rendering</param>
		public override void render(SpriteBatch spriteBatch) {
			this.bar.render(spriteBatch);
			this.ball.render(spriteBatch);
			this.text.render(spriteBatch);
		}
		#endregion Support methods
	}
}
