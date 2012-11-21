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

using GWNorthEngine.Audio;
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
	/// Models the data of a SoundEngineSlider GUI element
	/// </summary>
	public class SoundEngineSlider : Slider {
		#region Class variables
		private BaseSoundEngine soundEngine;
		private CheckBox checkBox;
		#endregion Class variables

		#region Class propeties
		/// <summary>
		/// Changes the volume of the sound engine, value is clamped between 0 and 1
		/// </summary>
		public override float CurrentValue {
			get {
				return base.CurrentValue;
			}
			set {
				base.CurrentValue = value;
				if (this.soundEngine != null) {
					this.soundEngine.Volume = base.CurrentValue;
				}
			}
		}
		#endregion Class properties

		#region Constructor
		/// <summary>
		/// Constructs a SoundEngineSlider object based on the parms
		/// </summary>
		/// <param name="parms">SoundEngineSliderParams object</param>
		public SoundEngineSlider(SoundEngineSliderParams parms):base(parms) {
			this.soundEngine = parms.SoundEngine;
			this.checkBox = new CheckBox(parms.CheckBoxParams);
		}
		#endregion Constructor

		#region Support methods
		/// <summary>
		/// Updates the slider
		/// </summary>
		/// <param name="elapsed">Time elapsed since the last call</param>
		public override void update(float elapsed) {
			base.update(elapsed);
			this.checkBox.update(elapsed);
			this.soundEngine.Muted = this.checkBox.Checked;
		}

		/// <summary>
		/// Renders the Slider
		/// </summary>
		/// <param name="spriteBatch">SpriteBatch object used for rendering</param>
		public override void render(SpriteBatch spriteBatch) {
			base.render(spriteBatch);
			this.checkBox.render(spriteBatch);
		}
		#endregion Support methods
	}
}
