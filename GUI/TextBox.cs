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
	/// Models a TextBox
	/// </summary>
	public class TextBox : BaseGUIElement {
		#region Class variables
		private List<StaticDrawable2D> imgs;
		private StaticDrawable2D cursorIndicator;
		private bool showIndicator;
		private SpriteFont font;
		private BoundingBox bbox;
		protected Text2D text2D;
		protected string text;
		private float elapsedTimeSinzeIndicatorFlash;
		private readonly float SIZE_PER_CHARACTER;
		private readonly float START_BAR_X;
		private const float INDICATOR_TIME = 350f;
		protected readonly int MAX_LENGTH;
		protected readonly List<Keys> VALID_KEYS;
		#endregion Class variables

		#region Class propeties
		/// <summary>
		/// Text written inside of the box
		/// </summary>
		public string Text { get { return this.text; } set { this.text = value; if (this.text2D != null) { this.text2D.WrittenText = value; } } }
		/// <summary>
		/// Whether or not the textbox has focus
		/// </summary>
		public virtual bool HasFocus { get; set; }
		#endregion Class properties

		#region Constructor
		/// <summary>
		/// Constructs a Textbox based on the params object passed in
		/// </summary>
		/// <param name="parms">TextBoxParams object</param>
		public TextBox(TextBoxParams parms) : base(parms) {
			this.font = parms.Font;
			this.Text = parms.Text;
			this.SIZE_PER_CHARACTER = parms.SizePerCharacter;
			this.MAX_LENGTH = parms.MaxLength;
			this.VALID_KEYS = new List<Keys>();
			this.VALID_KEYS.AddRange(KeyLists.DIRECTIONAL_KEYS);
			this.VALID_KEYS.AddRange(KeyLists.LETTERS_KEYS);
			this.VALID_KEYS.AddRange(KeyLists.NUMBER_KEYS);
			this.VALID_KEYS.AddRange(KeyLists.NUMPAD_KEYS);

			Vector2 endsScale = new Vector2(1f, parms.Scale.Y);
			Vector2 midScale = new Vector2(parms.Scale.X, 1f);
			this.imgs = new List<StaticDrawable2D>();
			// Starting edge
			StaticDrawable2DParams imgParms = new StaticDrawable2DParams {
				Position = parms.Position,
				LightColour = parms.LightColour,
				RenderingRectangle = Constants.TXT_BOX_END,
				Scale = endsScale,
				Texture = LoadingUtils.load<Texture2D>(parms.Content, Constants.GUI_FILE_NAME)
			};
			this.imgs.Add(new StaticDrawable2D(imgParms));

			// Middle bars
			Vector2 start = new Vector2(imgParms.Position.X - 30f, imgParms.Position.Y);
			Vector2 startBar = Vector2.Add(start, new Vector2(Constants.TXT_BOX_END.Width, 0f));
			START_BAR_X = startBar.X;
			float length = parms.MaxLength * parms.SizePerCharacter + parms.Scale.X;
			imgParms.Position = startBar;
			imgParms.RenderingRectangle = Constants.TXT_BOX_MIDDLE;
			imgParms.Scale = new Vector2(length, parms.Scale.Y);
			this.imgs.Add(new StaticDrawable2D(imgParms));

			// Ending edge
			imgParms.Position = Vector2.Add(start, new Vector2(Constants.TXT_BOX_END.Width + length, 0f));
			imgParms.RenderingRectangle = Constants.TXT_BOX_END;
			imgParms.Scale = endsScale;
			this.imgs.Add(new StaticDrawable2D(imgParms));

			this.bbox = new BoundingBox(new Vector3(start.X, start.Y, 0f), new Vector3(imgParms.Position.X + Constants.TXT_BOX_MIDDLE.Width,
				imgParms.Position.Y + Constants.TXT_BOX_MIDDLE.Height, 0f));

			// Cursor indicator
			float x = START_BAR_X + text.Length * SIZE_PER_CHARACTER + 4f;
			imgParms.Position = new Vector2( x, imgParms.Position.Y + 6f);
			imgParms.Scale = new Vector2(1f, (parms.Scale.Y - .5f));
			imgParms.LightColour = parms.TextColour;
			this.cursorIndicator = new StaticDrawable2D(imgParms);
			updateIndicatorsPosition();

			Text2DParams txtParms = new Text2DParams {
				Position = new Vector2(parms.Position.X + 2f, parms.Position.Y),
				Font = parms.Font,
				Scale = parms.TextScale,
				WrittenText = this.Text,
				LightColour = parms.TextColour
			};
			this.text2D = new Text2D(txtParms);
		}
		#endregion Constructor

		#region Support methods
		private Vector2 updateIndicatorsPosition() {
			Vector2 position = this.cursorIndicator.Position;

			float x = START_BAR_X + this.text.Length * SIZE_PER_CHARACTER + 4f;
			position = new Vector2(x, position.Y);

			this.cursorIndicator.Position = position;
			return position;
		}

		/// <summary>
		/// Handles updating the text of the TextBox
		/// </summary>
		protected virtual void handleTextUpdate() {
			Keys[] pressedKeys = InputManager.getInstance().getNewKeys();
			if (pressedKeys != null && pressedKeys.Length > 0) {
				string newText = null;
				KeyValuePair<Keys, string> defaultKVP = default(KeyValuePair<Keys, string>);
				KeyValuePair<Keys, string> result = defaultKVP;
				foreach (Keys key in pressedKeys) {
					if (Keys.Back == key) {
						if (this.text.Length > 0) {
							this.text = this.text.Substring(0, this.text.Length - 1);
						}
					} else if (this.text.Length < MAX_LENGTH) {
						if (Keys.Space == key) {
							this.text += " ";
						} else if (VALID_KEYS.Contains(key)) {
							// do we have a translation?
							newText = KeyLists.translate(key);
							this.text += newText;
						}
					}
				}
				this.text2D.WrittenText = this.text;
			}
		}

		/// <summary>
		/// Checks if the textbox has gained focus
		/// </summary>
		protected virtual void checkFocus() {
			if (InputManager.getInstance().wasLeftButtonPressed()) {
				this.HasFocus = isActorOver(InputManager.getInstance().MousePosition);
			}
		}

		/// <summary>
		/// Determines if the actor is over the button
		/// </summary>
		/// <param name="actorsPosition">Actors current position</param>
		/// <returns>boolean based on whether the actor is over the button</returns>
		public bool isActorOver(Vector2 actorsPosition) {
			return PickingUtils.pickVector(actorsPosition, this.bbox);
		}

		/// <summary>
		/// Updates the TextBox
		/// </summary>
		/// <param name="elapsed">Time since the last frame</param>
		public override void update(float elapsed) {
			if (this.imgs != null) {
				foreach (StaticDrawable2D img in this.imgs) {
					img.update(elapsed);
				}
				this.text2D.update(elapsed);

				if (this.HasFocus) {
					handleTextUpdate();
					if (this.text.Length < MAX_LENGTH) {
						updateIndicatorsPosition();
						this.elapsedTimeSinzeIndicatorFlash += elapsed;
						if (this.elapsedTimeSinzeIndicatorFlash >= INDICATOR_TIME) {
							this.elapsedTimeSinzeIndicatorFlash = 0f;
							this.showIndicator = !this.showIndicator;
						}
					} else {
						this.showIndicator = false;
					}
				}

				checkFocus();
			}
		}

		/// <summary>
		/// Renders the TextBox
		/// </summary>
		/// <param name="spriteBatch">SpriteBatch object used to render the TextBox</param>
		public override void render(SpriteBatch spriteBatch) {
			if (this.imgs != null) {
				foreach (StaticDrawable2D img in this.imgs) {
					img.render(spriteBatch);
				}
				this.text2D.render(spriteBatch);
				if (HasFocus && showIndicator) {
					this.cursorIndicator.render(spriteBatch);
				}
			}
		}
		#endregion Support methods
	}
}
