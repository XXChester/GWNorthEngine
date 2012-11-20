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
	public class FadeEffect : BaseEffect {
		/// <summary>
		/// Fade State
		/// </summary>
		public enum FadeState {
			/// <summary>
			/// Fade In
			/// </summary>
			In,
			/// <summary>
			/// Fade out
			/// </summary>
			Out
		}
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
		}
		#endregion Constructor

		#region Support methods
		/// <summary>
		/// Runs the effect
		/// </summary>
		public override void update(float elapsed) {
			this.ElapsedTransitionTime += elapsed;
			if (this.State == FadeState.In) {
				float alpha = 1f - (this.ElapsedTransitionTime/ this.TotalTransitionTime);
				base.Reference.LightColour = Color.Lerp(this.OriginalColour, Color.Transparent, alpha);
			} else if (this.State == FadeState.Out) {
				float alpha = 1f - (this.ElapsedTransitionTime / this.TotalTransitionTime);
				base.Reference.LightColour = Color.Lerp(Color.Transparent, this.OriginalColour, alpha);
			}
		}
		#endregion Support methods
	}
}
