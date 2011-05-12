using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GWNorthEngine.Model {
	/// <summary>
	/// Animation manager class which handles the animation steps for anything animated
	/// </summary>
	public class AnimationManager {
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
		#region Class variables
		private AnimationState animationState;
		/// <summary>
		/// Frame rate at which the object is animated
		/// </summary>
		protected float frameRate;
		/// <summary>
		/// Time elapsed sense we last animated the object
		/// </summary>
		protected float totalElapsed;
		/// <summary>
		/// Current frame we are at in the animation line
		/// </summary>
		protected int currentFrame;
		#endregion Class variables

		#region Class propeties
		/// <summary>
		/// Gets or sets the animation state for the animation manager
		/// </summary>
		public AnimationState State { get { return this.animationState; } set { this.animationState = value; } }
		/// <summary>
		/// Gets or sets the total elapsed time
		/// </summary>
		public float TotalElapsed { get { return this.totalElapsed; } set { this.totalElapsed = value; } }
		/// <summary>
		/// Gets or sets the frame rate in which the sprite animates
		/// </summary>
		public float FrameRate { get { return this.frameRate; } set { this.frameRate = value; } }
		/// <summary>
		/// Gets the current frame of the object
		/// </summary>
		public int CurrentFrame { get { return this.currentFrame; } }
		#endregion Class properties

		#region Constructor
		/// <summary>
		/// Builds an AnimationManager based on the animation state
		/// </summary>
		/// <param name="animationState">Start animation state of the object</param>
		/// <param name="frameRate">Starting frame rate of the object</param>
		/// <param name="maxFrameCount">Used in reverse playing scenarios; it sets the first frame to the end</param>
		public AnimationManager(AnimationState animationState, float frameRate, int maxFrameCount) {
			this.animationState = animationState;
			this.frameRate = frameRate;
			resetAnimation(maxFrameCount);
		}
		#endregion Constructor

		#region Support methods
		/// <summary>
		/// Handles incrementing the animation steps
		/// </summary>
		private void setNextFrame(int maxFrameCount) {
			if (this.animationState == AnimationState.PlayForward || this.animationState == AnimationState.PlayForwardOnce) {
				if (this.currentFrame == maxFrameCount) {
					// for animations only playing once we pause the animation once we are at our max frame
					if (this.animationState == AnimationState.PlayForwardOnce) {
						this.animationState = AnimationState.Paused;
					} else {
						this.currentFrame = 0;
					}
				} else {
					this.currentFrame++;
				}
			} else if (this.animationState == AnimationState.PlayReversed) {
				if (this.currentFrame == 0) {
					this.currentFrame = maxFrameCount;
				} else {
					this.currentFrame--;
				}
			}
		}

		/// <summary>
		/// Resets the animation sequence that the AnimationManager is controlling
		/// </summary>
		/// <param name="maxFrameCount">Max frames used to reset reverse sprites</param>
		public void resetAnimation(int maxFrameCount) {
			this.totalElapsed = 0f;
			if (animationState == AnimationState.PlayReversed) {
				this.currentFrame = maxFrameCount;
			} else {
				this.currentFrame = 0;
			}
		}

		/// <summary>
		/// Handles whether it is time to update the animation step or not
		/// </summary>
		/// <param name="elapsed">Time elapsed sense the last call</param>
		/// <param name="maxFrameCount">Maximum frames of the object</param>
		public void update(float elapsed, int maxFrameCount) {
			if (this.animationState != AnimationState.Paused) {
				this.totalElapsed += elapsed;
				if (this.totalElapsed > this.frameRate) {
					setNextFrame(maxFrameCount);
					this.totalElapsed -= this.frameRate;
				}
			}
		}
		#endregion Support methods
	}
}
