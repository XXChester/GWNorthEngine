using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GWNorthEngine.Tools.TilePlacer {
	/// <summary>
	/// Contains common constants for the TilePlacer tool
	/// </summary>
	public class Constants {
		/// <summary>
		/// File extension used by files for or created by the TilePlacer
		/// </summary>
		public const string FILE_EXTENSION = ".XML";
		/// <summary>
		/// Header that denotes the start of the map size section
		/// </summary>
		public const string XML_HEADER_SIZE = "Size";
		/// <summary>
		/// Height of the map
		/// </summary>
		public const string XML_HEIGHT = "Height";
		/// <summary>
		/// Width of the map
		/// </summary>
		public const string XML_WIDTH = "Width";
		/// <summary>
		/// Header that denotes the start of a layer
		/// </summary>
		public const string XML_HEADER_LAYER = "Layer";
		/// <summary>
		/// Header that denotes the start of a tile
		/// </summary>
		public const string XML_HEADER_TILE = "Tile";
		/// <summary>
		/// Name of the tile
		/// </summary>
		public const string XML_TILE_NAME = "Name";
		/// <summary>
		/// Filename of the tile
		/// </summary>
		public const string XML_TILE_FILE_NAME = "FileName";
		/// <summary>
		/// Value of the tile (TileValues int representation)
		/// </summary>
		public const string XML_TILE_VALUE = "Value";
	}
}
