using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GWNorthEngine.Logic {
	/// <summary>
	/// Animation state of the object
	/// </summary>
	public enum AnimationState {
		/// <summary>
		/// Animation is paused
		/// </summary>
		Paused,
		/// <summary>
		/// Animation is playing forawrd
		/// </summary>
		PlayForward,
		/// <summary>
		/// Animation is playing in reverse
		/// </summary>
		PlayReversed,
		/// <summary>
		/// Animation plays through forward 1 time
		/// </summary>
		PlayForwardOnce
	}
}
