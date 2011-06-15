using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GWNorthEngine.Model.Params {
	/// <summary>
	/// Object containing the required data to build the AnimationManager object
	/// </summary>
	public class BaseAnimationManagerParams {
		#region Class properties
		/// <summary>
		/// Gets or sets the rate in which the object animates at
		/// </summary>
		public float FrameRate { get; set; }
		/// <summary>
		/// Gets or sets the total animation frame count
		/// </summary>
		public int TotalFrameCount { get; set; }
		/// <summary>
		/// Gets or sets the AnimationState of the object
		/// </summary>
		public AnimationManager.AnimationState AnimationState { get; set; }
		#endregion Class properties

		#region Constructor
		/// <summary>
		/// Builds the default settings for setting up the AnimationManager object. The default settings are below;
		/// AnimationState:		Paused
		/// </summary>
		public BaseAnimationManagerParams() {
			this.AnimationState = AnimationManager.AnimationState.Paused;
		}
		#endregion Constructor
	}
}
