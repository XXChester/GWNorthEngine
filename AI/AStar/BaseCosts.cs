using System;
using System.Collections.Generic;
using System.Text;

namespace GWNorthEngine.AI.AStar {
	/// <summary>
	/// Base governing implementation of the cost module for determining tiles costs
	/// </summary>
	public abstract class BaseCosts {
		#region Class properties
		/// <summary>
		/// Cost of a standard Walkable tile
		/// </summary>
		public abstract int StandardCost { get; }
		/// <summary>
		/// Cost of a Variable Terrain Low Cost tile
		/// </summary>
		public abstract int VariableTerrainLowCost { get; }
		/// <summary>
		/// Cost of a Variable Terrain Medium Cost tile
		/// </summary>
		public abstract int VariableTerrainMediumCost { get; }
		/// <summary>
		/// Cost of a Variable Terrain High Cost tile
		/// </summary>
		public abstract int VariableTerrainHighCost { get; }
		/// <summary>
		/// Gets or sets the Diagonal Multiplier used for determining the cost of diagonal moves
		/// </summary>
		public virtual float DiagonalMultiplier { get; set; }
		#endregion Class properties

		#region Class constructor
		/// <summary>
		/// Builsd the basic cost class
		/// </summary>
		/// <param name="diagonalMultiplier">Multiplier used for diagonal movement determination</param>
		public BaseCosts(float diagonalMultiplier) {
			this.DiagonalMultiplier = diagonalMultiplier;
		}
		#endregion Class constructor

		#region Support methods
		/// <summary>
		/// Determines the diagonal cost associated with a tile
		/// </summary>
		/// <param name="currentPositionsCost">Value if the tile were a straight move not a diagonal move</param>
		/// <returns>Diagonal cost of the tile</returns>
		public virtual int getDiagonalCost(int currentPositionsCost) {
			return ((int)((currentPositionsCost * this.DiagonalMultiplier)));
		}

		/// <summary>
		/// Determines the cost of a tile based on its type
		/// </summary>
		/// <param name="space"></param>
		/// <returns></returns>
		public virtual int getCost(BasePathFinder.TypeOfSpace space) {
			int cost = StandardCost;
			if (space == BasePathFinder.TypeOfSpace.VariableTerrainLowCost) {
				cost = VariableTerrainLowCost;
			} else if (space == BasePathFinder.TypeOfSpace.VariableTerrainMediumCost) {
				cost = VariableTerrainMediumCost;
			} else if (space == BasePathFinder.TypeOfSpace.VariableTerrainHighCost) {
				cost = VariableTerrainHighCost;
			}
			return cost;
		}
		#endregion Support methods
	}
}
