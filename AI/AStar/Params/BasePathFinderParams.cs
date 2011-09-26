using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GWNorthEngine.AI.AStar;
namespace GWNorthEngine.AI.AStar.Params {
	/// <summary>
	/// Contains the common data required to build a A* Pathfinder
	/// </summary>
	public abstract class BasePathFinderParams {
		#region Class properties
		/// <summary>
		/// Gets or sets the height of the AI representation of the board
		/// </summary>
		public int Height { get; set; }
		/// <summary>
		/// Gets or sets the width of the AI representation of the board
		/// </summary>
		public int Width { get; set; }
		/// <summary>
		/// Gets or sets whether corner cutting is allowed or not
		/// </summary>
		public bool AllowCornerCutting { get; set; }
		/// <summary>
		/// Gets or sets the Directional Restriction Type on the algorithm
		/// </summary>
		public BasePathFinder.RestrictionType DirectionRestrictionType { get; set; }
		/// <summary>
		/// Gets or sets the governing costs object used by the algorithm
		/// </summary>
		public BaseCosts Costs { get; set; }
		#endregion Class properties

		#region Constructor
		/// <summary>
		///  Builds the default settings for setting up a PathFinder. The default settings are listed below.
		///  AllowCornerCutting:				False
		///  DirectionRestrictionType:			Restricted
		///  Costs:								new DefaultCosts()
		/// </summary>
		public BasePathFinderParams() {
			this.DirectionRestrictionType = BasePathFinder.RestrictionType.Restricted;
			this.Costs = new DefaultCosts();
			this.AllowCornerCutting = false;
		}
		#endregion Constructor
	}
}
