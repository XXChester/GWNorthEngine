using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

using GWNorthEngine.Logic;

namespace GWNorthEngine.Model.Effects.Params {
	/// <summary>
	/// Models the data required for a scale over time effect
	/// </summary>
	public class ScaleOverTimeEffectParams : BaseEffectParams {
		#region Class properties
		/// <summary>
		/// Amount to scale by
		/// </summary>
		public Vector2 ScaleBy { get; set; }
		#endregion Class properties
	}
}
