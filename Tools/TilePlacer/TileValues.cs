using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GWNorthEngine.Tools.TilePlacer {
	/// <summary>
	/// Constant values for the TilePlacer tool
	/// </summary>
	public enum TileValues {
		/// <summary>
		/// Unknown tile value meaning the tile was left empty
		/// </summary>
		Unknown = -1,
		/// <summary>
		/// Background tile
		/// </summary>
		BackGround = 0,
		/// <summary>
		/// Tile that you cannot pass through
		/// </summary>
		NoMovements = 1,
		/// <summary>
		/// Tiles that you stand on
		/// </summary>
		SpawnPoint,
		/// <summary>
		/// Tiles that you can climb
		/// </summary>
		Climbers,
		/// <summary>
		/// Moveable piece on the board
		/// </summary>
		Walkable,
		/// <summary>
		/// Variable terrain with a higher cost than standard with an extra cost that is low
		/// </summary>
		VariableTerrainLowCost,
		/// <summary>
		/// Variable terrain with a higher cost than standard with an extra cost that is medium
		/// </summary>
		VariableTerrainMediumCost,
		/// <summary>
		/// Variable terrain with a higher cost than standard with an extra cost that is high
		/// </summary>
		VariableTerrainHighCost
	}
}
