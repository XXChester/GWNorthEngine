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
using GWNorthEngine.Model;
using GWNorthEngine.Model.Params;
using GWNorthEngine.Logic;
using GWNorthEngine.Logic.Params;
using GWNorthEngine.Input;
using GWNorthEngine.Utils;
using GWNorthEngine.Scripting;

using GWNorthEngine.GUI.Params;

namespace GWNorthEngine.GUI {
	/// <summary>
	/// Models a CheckBox
	/// </summary>
	public class CheckBox : BaseGUIElement {
		#region Class variables
		private StaticDrawable2D activeImg;
		private StaticDrawable2D uncheckedBoxImag;
		private StaticDrawable2D checkedBoxImag;
		private Text2D text;
		private BoundingBox bbox;
		#endregion Class variables

		#region Class propeties
		/// <summary>
		/// Whether or not the check box is checked or unchecked
		/// </summary>
		public bool Checked { get; set; }
		#endregion Class properties

		#region Constructor
		/// <summary>
		/// Constructs a CheckBox object based on the params
		/// </summary>
		/// <param name="parms">CheckBoxParams object</param>
		public CheckBox(CheckBoxParams parms) : base(parms) {
			this.Checked = parms.Checked;

			StaticDrawable2DParams imgParms = new StaticDrawable2DParams {
				Position = parms.Position,
				LightColour = parms.LightColour,
				Scale = parms.Scale,
				RenderingRectangle = Constants.CHK_BOX_UNCHECKED,
				Texture = LoadingUtils.load<Texture2D>(parms.Content, Constants.GUI_FILE_NAME)
			};
			this.uncheckedBoxImag = new StaticDrawable2D(imgParms);
			Vector3 max = new Vector3(parms.Position.X + Constants.CHK_BOX_CHECKED.Width, parms.Position.Y + Constants.CHK_BOX_CHECKED.Height, 0f);
			this.bbox = new BoundingBox(new Vector3(parms.Position, 0f), max);

			imgParms.RenderingRectangle = Constants.CHK_BOX_CHECKED;
			this.checkedBoxImag = new StaticDrawable2D(imgParms);

			Text2DParams textParms = new Text2DParams {
				Position = new Vector2(parms.Position.X + 40f, parms.Position.Y),
				LightColour = parms.LightColour,
				WrittenText = parms.Text,
				Scale = parms.Scale,
				Font = parms.Font
			};
			this.text = new Text2D(textParms);
		}
		#endregion Constructor

		#region Support methods
		/// <summary>
		/// Updates the Checkbox
		/// </summary>
		/// <param name="elapsed">Time elapsed since the last call</param>
		public override void update(float elapsed) {
			if (InputManager.getInstance().wasLeftButtonPressed()) {
				// if we were inside the checkbox
				if (PickingUtils.pickVector(InputManager.getInstance().MousePosition, this.bbox)) {
					this.Checked = !this.Checked;
				}
			}


			if (this.Checked) {
				this.activeImg = this.checkedBoxImag;
			} else {
				this.activeImg = this.uncheckedBoxImag;
			}

			this.activeImg.update(elapsed);
			this.text.update(elapsed);
		}

		/// <summary>
		/// Renders the CheckBox
		/// </summary>
		/// <param name="spriteBatch">SpriteBatch object used for rendering</param>
		public override void render(SpriteBatch spriteBatch) {
			this.activeImg.render(spriteBatch);
			this.text.render(spriteBatch);
		}
		#endregion Support methods
	}
}
