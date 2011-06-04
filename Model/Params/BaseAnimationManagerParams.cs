using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GWNorthEngine.Model.Params {
	public class BaseAnimationManagerParams {
		#region Class properties
		public float FrameRate { get; set; }
		public int TotalFrameCount { get; set; }
		public AnimationManager.AnimationState AnimationState { get; set; }
		#endregion Class properties

		#region Constructor
		public BaseAnimationManagerParams() {
			this.AnimationState = AnimationManager.AnimationState.Paused;
		}
		#endregion Constructor
	}
}
