﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GWNorthEngine.Tools.TilePlacer {
	/// <summary>
	/// Models the data that makes up a layer of a full map
	/// </summary>
	public class Layer {
		#region Class properties
		/// <summary>
		/// Gets or sets the tiles used to draw the map
		/// </summary>
		public MapTile[,] Tiles { get; set; }
		#endregion Class properties

		#region Constructor
		/// <summary>
		/// Constructs a Layer based off the xml loaded via the MapLoader
		/// </summary>
		/// <param name="tiles">Array of MapTile objects loaded from the xml</param>
		public Layer(MapTile[,] tiles) {
			this.Tiles = tiles;
		}
		#endregion Constructor

		#region Destructor
		/// <summary>
		/// Cleans up the tiles
		/// </summary>
		public void dispose() {
			foreach (MapTile tile in this.Tiles) {
				if (tile != null) {
					tile.dispose();
				}
			}
		}
		#endregion Destructor
	}
}
