using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GWNorthEngine.Logic.Params {
	/// <summary>
	/// Models the data required to build a KeyFrameAnimationManager object
	/// </summary>
	public class KeyFrameAnimationManagerParams : BaseAnimationManagerParams {
		#region Class variables
		private KeyFrameData keyFrameData;
		#endregion Class variables

		#region Class properties
		/// <summary>
		/// Gets or sets the KeyFrameData object and also the bases frame rate
		/// </summary>
		public KeyFrameData KeyFrameData {
			get { return this.keyFrameData; }
			set {
				this.keyFrameData = value;
				base.FrameRate = this.keyFrameData.FrameRate;
			}
		}
		#endregion Class properties
	}
}
