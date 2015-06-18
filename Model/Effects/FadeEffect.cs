using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

using GWNorthEngine.Model.Effects.Params;

namespace GWNorthEngine.Model.Effects {
	/// <summary>
	/// Models the data required for a fade effect
	/// </summary>
	public class FadeEffect : BaseEffect, FinishableEffect {
		/// <summary>
		/// Fade State
		/// </summary>
		public enum FadeState {
			/// <summary>
			/// Fade In
			/// </summary>
			In,
			/// <summary>
			/// Partial fade in
			/// </summary>
			PartialIn,
			/// <summary>
			/// Fade out
			/// </summary>
			Out,
			/// <summary>
			/// Partial fade out
			/// </summary>
			PartialOut
		}
		private int alphaAmount;

		#region Class properties
		/// <summary>
		/// State of the effect
		/// </summary>
		public FadeState State { get; set; }
		/// <summary>
		/// Total time the fade should take
		/// </summary>
		public float TotalTransitionTime { get; set; }
		/// <summary>
		/// Elapsed time the fade has taken
		/// </summary>
		public float ElapsedTransitionTime { get; set; }
		/// <summary>
		/// Original colour to base the effect off of
		/// </summary>
		public Color OriginalColour { get; set; }
		/// <summary>
		/// Colour we will partially fade out to
		/// </summary>
		public Color PartialFadeToColour { get; set; }
		/// <summary>
		/// If an effect has run it's course, this flag will be true
		/// </summary>
		public bool HasFinished { get {
			return ElapsedTransitionTime.CompareTo(TotalTransitionTime) == 1;	
		} }

		public int AlphaAmount {
			get { return this.alphaAmount; }
			set {
				this.alphaAmount = value;
				//this.PartialFadeToColour = new Color(OriginalColour.R, OriginalColour.G, OriginalColour.B, this.alphaAmount);
			}
		}
		#endregion Class properties

		#region Constructor
		/// <summary>
		/// Constructs a FadeEffect object
		/// </summary>
		/// <param name="parms">FadeEffectParms object</param>
		public FadeEffect(FadeEffectParams parms): base(parms) {
			this.State = parms.State;
			this.TotalTransitionTime = parms.TotalTransitionTime;
			this.ElapsedTransitionTime = 0f;
			this.OriginalColour = parms.OriginalColour;
			if (parms.GetType().Equals(typeof(PartialFadeEffectParams))) {
				PartialFadeEffectParams partialParams = (PartialFadeEffectParams)parms;
				this.AlphaAmount = partialParams.AlphaAmount;
			}
		}
		#endregion Constructor

		#region Support methods
		/// <summary>
		/// Runs the effect
		/// </summary>
		public override void update(float elapsed) {
			this.ElapsedTransitionTime += elapsed;
			float alpha = 1f - (this.ElapsedTransitionTime / this.TotalTransitionTime);
			if (this.State == FadeState.In) {
				base.Reference.LightColour = Color.Lerp(this.OriginalColour, Color.Transparent, alpha);
			} else if (this.State == FadeState.PartialIn) {
				if (base.Reference.LightColour.A < AlphaAmount) {
					this.PartialFadeToColour = Color.Lerp(this.OriginalColour, this.PartialFadeToColour, alpha);
					base.Reference.LightColour = this.PartialFadeToColour;
				}
			} else if (this.State == FadeState.Out) {
				base.Reference.LightColour = Color.Lerp(Color.Transparent, this.OriginalColour, alpha);
			} else if (this.State == FadeState.PartialOut) {
				if (base.Reference.LightColour.A > AlphaAmount) {
					this.PartialFadeToColour = Color.Lerp(Color.Transparent, this.OriginalColour, alpha);
					base.Reference.LightColour = this.PartialFadeToColour;
				}
			}
		}

		/// <summary>
		/// Resets the effect
		/// </summary>
		public virtual void reset() {
			this.ElapsedTransitionTime = 0f;
		}

		/// <summary>
		/// Changes the elapsed time to it's reversed version so that the alpha blending takes place from the current position instead of restarting
		/// </summary>
		public virtual void interruptFade() {
			this.ElapsedTransitionTime = this.TotalTransitionTime - this.ElapsedTransitionTime;
		}
		#endregion Support methods
	}
}
