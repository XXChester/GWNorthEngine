using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GWNorthEngine.AI.AStar;
using GWNorthEngine.Tools.TilePlacer;
using GWNorthEngine.Utils;
namespace GWNorthEngine.Tools {
	/// <summary>
	/// Translation utility between tool values and other engine components values
	/// </summary>
	public static class Translator {
		/// <summary>
		/// Translates a TilePlacer.TileValue to an AI.AStar.PathFinder.TypeOfSpace value
		/// </summary>
		/// <param name="value">Tile placer value</param>
		/// <returns>AStar representation of the value passed in</returns>
		public static BasePathFinder.TypeOfSpace translateTileValueToAStarType(TileValues value) {
			return translateTileValueToAStarType((int)value);
		}

		/// <summary>
		/// Translates an int representation of a TilePlacer.TileValue to an AI.AStar.PathFinder.TypeOfSpace value
		/// </summary>
		/// <param name="value">Tile placer value</param>
		/// <returns>AStar representation of the value passed in</returns>
		public static BasePathFinder.TypeOfSpace translateTileValueToAStarType(int value) {
			BasePathFinder.TypeOfSpace aStarValue;
			TileValues tileValue = EnumUtils.numberToEnum<TileValues>(value);
			switch (tileValue) {
				case TileValues.NoMovements:
					aStarValue = BasePathFinder.TypeOfSpace.Unwalkable;
					break;
				case TileValues.VariableTerrainLowCost:
					aStarValue = BasePathFinder.TypeOfSpace.VariableTerrainLowCost;
					break;
				case TileValues.VariableTerrainMediumCost:
					aStarValue = BasePathFinder.TypeOfSpace.VariableTerrainMediumCost;
					break;
				case TileValues.VariableTerrainHighCost:
					aStarValue = BasePathFinder.TypeOfSpace.VariableTerrainHighCost;
					break;
				default:
					aStarValue = BasePathFinder.TypeOfSpace.Walkable;
					break;
			}
			return aStarValue;
		}
	}
}
