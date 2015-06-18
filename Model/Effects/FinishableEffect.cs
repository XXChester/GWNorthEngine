using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GWNorthEngine.Model.Effects {
	/// <summary>
	/// Effects that can and be removed after certain conditions
	/// </summary>
	public interface FinishableEffect {
		/// <summary>
		/// If an effect has run it's course, this flag will be true
		/// </summary>
		bool HasFinished { get; }
	}
}
