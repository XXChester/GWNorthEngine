using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

using GWNorthEngine.Audio;

namespace GWNorthEngine.GUI.Params {
	/// <summary>
	/// Models the data required to build a SoundEngine specific slider
	/// </summary>
	public class SoundEngineSliderParams : SliderParams {
		/// <summary>
		/// BaseSoundEngine that the slider is to control
		/// </summary>
		public BaseSoundEngine SoundEngine { get; set; }
		/// <summary>
		/// Text to display beside the CheckBox
		/// </summary>
		public string CheckBoxText { get; set; }
		/// <summary>
		/// Whether the check box is checked or unchecked
		/// </summary>
		public bool Checked { get; set; }
		/// <summary>
		/// Position of the CheckBox
		/// </summary>
		public Vector2 CheckBoxPosition { get; set; }
	}
}
