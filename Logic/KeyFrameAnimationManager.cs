using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GWNorthEngine.Logic.Params;

namespace GWNorthEngine.Logic {
	/// <summary>
	/// Animation manager class which handles the animation steps for anything animated between a defined set of frames
	/// </summary>
	public class KeyFrameAnimationManager : AnimationManager {
		#region Class variables
		private KeyFrameData keyFrameData;
		#endregion Class variables
		
		#region Class propeties
		/// <summary>
		/// Gets or sets the KeyFrameData object as well as sets the parents current frame, and frame rate
		/// </summary>
		public KeyFrameData KeyFrameData {
			get { return this.keyFrameData; }
			set {
				this.keyFrameData = value;
				base.frameRate = this.keyFrameData.FrameRate;
				if (this.keyFrameData.ResetAnimationOnSet) {
					resetAnimation();
				}
			}
		}
		#endregion Class properties

		#region Constructor
		/// <summary>
		/// Constructs a KeyFrameAnimationManager object based on the KeyFrameAnimationManagerParms object passed in
		/// </summary>
		/// <param name="parms"></param>
		public KeyFrameAnimationManager(KeyFrameAnimationManagerParams parms)
			: base(parms) {
			this.keyFrameData = parms.KeyFrameData;
			// we need to call reset because when the base did on its constructor we didn't have teh keyFrameData set yet
			resetAnimation();
		}
		#endregion Constructor

		#region Support methods
		/// <summary>
		/// Handles incrementing the animation steps within the KeyFrameData object
		/// </summary>
		protected override void setNextFrame(int maxFrameCount) {
			if (base.animationState == AnimationState.PlayForward || base.animationState == AnimationState.PlayForwardOnce) {
				if (base.currentFrame == this.keyFrameData.EndingKeyFrame) {
					// for animations only playing once we pause the animation once we are at our max frame
					if (base.animationState == AnimationState.PlayForwardOnce) {
						base.animationState = AnimationState.Paused;
					} else {
						base.currentFrame = this.keyFrameData.StartingKeyFrame;
					}
				} else {
					this.currentFrame++;
				}
			} else if (base.animationState == AnimationState.PlayReversed || this.animationState == AnimationState.PlayReversedOnce) {
				if (this.currentFrame == 0) {
					if (this.animationState == AnimationState.PlayReversedOnce) {
						this.animationState = AnimationState.Paused;
					} else {
						base.currentFrame = this.keyFrameData.EndingKeyFrame;
					}
				} else {
					base.currentFrame--;
				}
			}
		}

		/// <summary>
		/// Resets the animation sequence that the KeyFrameAnimationManager is controlling
		/// </summary>
		/// <param name="cockAnimation">Determines if we are setting the animation up to fire right away as soon as a "Play" state is
		/// entered or whether we want it set to replay the whole wait time</param>
		public void resetAnimation(bool cockAnimation = false) {
			int dummy = -1;
			resetAnimation(dummy, cockAnimation);
		}

		/// <summary>
		/// Resets the animation sequence that the KeyFrameAnimationManager is controlling and has an option to set the animation so 
		/// that it will fire as soon the state is changed to a "play" state
		/// </summary>
		/// <param name="maxFrameCount">NOT USED BUT required as we are overloading the base</param>
		/// <param name="cockAnimation">Determines if we are setting the animation up to fire right away as soon as a "Play" state is
		/// entered or whether we want it set to replay the whole wait time</param>
		public override void resetAnimation(int maxFrameCount, bool cockAnimation) {
			base.totalElapsed = 0f;
			if (base.animationState == AnimationState.PlayReversed || this.animationState == AnimationState.PlayReversedOnce) {
				base.currentFrame = this.keyFrameData.EndingKeyFrame;
			} else {
				base.currentFrame = this.keyFrameData.StartingKeyFrame;
			}

			if (cockAnimation) {
				base.State = AnimationState.Paused;
				base.totalElapsed = this.keyFrameData.FrameRate;
			}
		}
		#endregion Support methods
	}
}
