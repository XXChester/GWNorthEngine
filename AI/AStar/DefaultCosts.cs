using System;
using System.Collections.Generic;
using System.Text;

namespace GWNorthEngine.AI.AStar {
	/// <summary>
	/// Default Cost implementation for variable terrain
	/// </summary>
	public class DefaultCosts : BaseCosts {
		#region Class properties
		/// <summary>
		/// Cost of a standard Walkable tile (cost: 10)
		/// </summary>
		public override int StandardCost { get { return 10; } }
		/// <summary>
		/// Cost of a Variable Terrain Low Cost tile (cost: StandardCost * 2)
		/// </summary>
		public override int VariableTerrainLowCost { get { return StandardCost * 2; } }
		/// <summary>
		/// Cost of a Variable Terrain Medium Cost tile (cost: StandardCost * 3)
		/// </summary>
		public override int VariableTerrainMediumCost { get { return StandardCost * 3; } }
		/// <summary>
		/// Cost of a Variable Terrain High Cost tile (cost: StandardCost * 4)
		/// </summary>
		public override int VariableTerrainHighCost { get { return StandardCost * 4; } }
		#endregion Class properties

		#region Constructor
		/// <summary>
		/// Constructs the DefaultClass implementation with a diagonal multiplier of 1.5f
		/// </summary>
		public DefaultCosts() : base(1.5f) { }
		#endregion Constructor
	}
}
