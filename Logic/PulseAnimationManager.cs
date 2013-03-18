using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GWNorthEngine.Logic.Params;

namespace GWNorthEngine.Logic {
	/// <summary>
	/// Models a pulse animation manager which plays the animation forward to the end, then plays reversed to the start, rinse and repeat
	/// </summary>
	public class PulseAnimationManager : AnimationManager {
		#region Class properties
		/// <summary>
		/// Gets or Sets the direction to pulse in
		/// </summary>
		public PulseDirection PulseDirection { get; set; }
		#endregion Class properties

		#region Constructor
		/// <summary>
		/// Constructs a PuleAnimationManager based on the params passed in
		/// </summary>
		/// <param name="parms">PulseAnmationManagerParams object</param>
		public PulseAnimationManager(PulseAnimationManagerParams parms)
			: base(parms) {
				this.PulseDirection = PulseDirection;
		}
		#endregion Constructor

		#region Support methods
		/// <summary>
		/// Handles incrementing the animation steps
		/// </summary>
		/// <param name="maxFrameCount">Max frames used to reset reverse sprites</param>
		protected override void setNextFrame(int maxFrameCount) {
			if (base.animationState == AnimationState.PulseForwardBack) {
				if (base.currentFrame == 0) {
					base.currentFrame++;
					this.PulseDirection = PulseDirection.Up;
				} else if (base.currentFrame == maxFrameCount) {
					this.PulseDirection = Logic.PulseDirection.Down;
					base.currentFrame--;
				} else {
					base.currentFrame--;
				}
			} else {
				base.setNextFrame(maxFrameCount);
			}
		}

		/// <summary>
		/// Resets the animation sequence that the AnimationManager is controlling and has an option to set the animation so that it will
		/// fire as soon the state is changed to a "play" state
		/// </summary>
		/// <param name="maxFrameCount">Max frames used to reset reverse sprites</param>
		/// <param name="cockAnimation">Determines if we are setting the animation up to fire right away as soon as a "Play" state is entered or whether
		/// we want it set to replay the whole wait time</param>
		public override void resetAnimation(int maxFrameCount, bool cockAnimation = false) {
			this.totalElapsed = 0f;
			if (this.animationState == AnimationState.PulseForwardBack) {
				if (this.PulseDirection == PulseDirection.Up) {
					base.currentFrame = 0;
				} else {
					base.currentFrame = maxFrameCount;
				}
			} else {
				base.resetAnimation(maxFrameCount, cockAnimation);
			}

			if (cockAnimation) {
				// if we are resetting and setting the animation up to be run, we do not want it to play right away
				base.animationState = AnimationState.Paused;
				base.totalElapsed = frameRate;
			}
		}
		#endregion Support methods
	}
}
