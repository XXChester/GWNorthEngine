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
		/// Translates an int representation of a TilePlacer.TileValue to an AI.AStar.PathFinder.TypeOfSpace value
		/// </summary>
		/// <param name="value">Tile placer value</param>
		/// <returns>AStar representation of the value passed in</returns>
		public static PathFinder.TypeOfSpace translateTileValueToAStarType(int value) {
			PathFinder.TypeOfSpace aStarValue;
			TileValues tileValue = EnumUtils.numberToEnum<TileValues>(value);
			switch (tileValue) {
				case TileValues.BackGround:
					aStarValue = PathFinder.TypeOfSpace.Unwalkable;
					break;
				case TileValues.NoMovements:
					aStarValue = PathFinder.TypeOfSpace.Unwalkable;
					break;
				default:
					aStarValue = PathFinder.TypeOfSpace.Walkable;
					break;
			}
			return aStarValue;
		}
	}
}
