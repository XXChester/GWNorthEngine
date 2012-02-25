using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GWNorthEngine.Logic.Params;
namespace GWNorthEngine.Logic {
	/// <summary>
	/// Animation manager class which handles the animation steps for anything animated
	/// </summary>
	public class AnimationManager {
		#region Class variables
		/// <summary>
		/// Animation state for the animation sequence
		/// </summary>
		protected AnimationState animationState;
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
		/// Builds an AnimationManager based on the AnimationManagerParams object passed in
		/// </summary>
		/// <param name="parms">BaseAnimationManagerParams object containing the data required to load the Animation Manager object</param>
		public AnimationManager(BaseAnimationManagerParams parms) {
			this.animationState = parms.AnimationState;
			this.frameRate = parms.FrameRate;
			resetAnimation(parms.TotalFrameCount - 1);// comes in as an array size so - 1
		}
		#endregion Constructor

		#region Support methods
		/// <summary>
		/// Handles incrementing the animation steps
		/// </summary>
		protected virtual void setNextFrame(int maxFrameCount) {
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
			resetAnimation(maxFrameCount, false);
		}

		/// <summary>
		/// Resets the animation sequence that the AnimationManager is controlling and has an option to set the animation so that it will
		/// fire as soon the state is changed to a "play" state
		/// </summary>
		/// <param name="maxFrameCount">Max frames used to reset reverse sprites</param>
		/// <param name="cockAnimation">Determines if we are setting the animation up to fire right away as soon as a "Play" state is entered or whether
		/// we want it set to replay the whole wait time</param>
		public virtual void resetAnimation(int maxFrameCount, bool cockAnimation) {
			this.totalElapsed = 0f;
			if (this.animationState == AnimationState.PlayReversed) {
				this.currentFrame = maxFrameCount;
			} else {
				this.currentFrame = 0;
			}

			if (cockAnimation) {
				this.State = AnimationState.Paused;
				this.totalElapsed = frameRate;
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
