using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
namespace GWNorthEngine.Utils {
	/// <summary>
	/// Helper methods for screen and object transitions
	/// </summary>
	public static class TransitionUtils {
		/// <summary>
		/// Fades out a colour over time
		/// </summary>
		/// <param name="colour">The constant colour of the object is rendered in</param>
		/// <param name="TOTAL_TIME_TO_TRANSITION">Constant value of the total transition time</param>
		/// <param name="elapsedTransitionTime">Time that has elapsed sense the transition started</param>
		/// <returns>Newly created colour to assign to the object for its next rendering pass</returns>
		public static Color fadeOut(Color colour, float TOTAL_TIME_TO_TRANSITION, float elapsedTransitionTime) {
			float alpha = 1f - (elapsedTransitionTime / TOTAL_TIME_TO_TRANSITION);
			return Color.Lerp(Color.Transparent, colour, alpha);
		}

		/// <summary>
		/// Fades in a colour over time
		/// </summary>
		/// <param name="colour">The constant colour of the object is rendered in</param>
		/// <param name="TOTAL_TIME_TO_TRANSITION">Constant value of the total transition time</param>
		/// <param name="elapsedTransitionTime">Time that has elapsed sense the transition started</param>
		/// <returns>Newly created colour to assign to the object for its next rendering pass</returns>
		public static Color fadeIn(Color colour, float TOTAL_TIME_TO_TRANSITION, float elapsedTransitionTime) {
			float alpha = 1f - (elapsedTransitionTime / TOTAL_TIME_TO_TRANSITION);
			return Color.Lerp(colour, Color.Transparent, alpha);
		}
	}
}
