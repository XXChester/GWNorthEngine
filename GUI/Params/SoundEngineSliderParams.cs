using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
		/// CheckBoxParams object to use to build the CheckBox for muting
		/// </summary>
		public CheckBoxParams CheckBoxParams { get; set; }
	}
}
