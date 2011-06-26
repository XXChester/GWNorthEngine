using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
namespace GWNorthEngine.Tools.TilePlacer {
	/// <summary>
	/// Models the data that is a direct result from loading a TilePlacer map
	/// </summary>
	public class LoadResult {
		#region Class properties
		/// <summary>
		/// Gets or sets the height of the map
		/// </summary>
		public int Height { get; set; }
		/// <summary>
		/// Gets or sets the width of the map
		/// </summary>
		public int Width { get; set; }
		/// <summary>
		/// Gets or sets the MapTile array representation of the map
		/// </summary>
		public MapTile[,] MapTiles { get; set; }
		/// <summary>
		/// Gets or sets the TileValues array representation of the map
		/// </summary>
		public TileValues[,] TileValues { get; set; }
		#endregion Class properties

		#region Class constructor
		/// <summary>
		/// Constructs a LoadResult object
		/// </summary>
		/// <param name="height">Height of the map</param>
		/// <param name="width">Width of the map</param>
		/// <param name="mapTiles">MapTile objects that make the map</param>
		/// <param name="tileValues">TileValues of the types of tiles that make up the map</param>
		public LoadResult(int height, int width, MapTile[,] mapTiles, TileValues[,] tileValues) {
			this.Height = height;
			this.Width = width;
			this.MapTiles = mapTiles;
			this.TileValues = tileValues;
		}
		#endregion Class constructor
	}
}
