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
		StandOns = 2,
		/// <summary>
		/// Tiles that you can climb
		/// </summary>
		Climbers = 3
	}
}
