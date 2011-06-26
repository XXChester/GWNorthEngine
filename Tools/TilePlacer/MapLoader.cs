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
using GWNorthEngine.Utils;
namespace GWNorthEngine.Tools.TilePlacer {
	/// <summary>
	/// Loads a TilePlacer map file
	/// </summary>
	public class MapLoader {
		/// <summary>
		///  Delegate used for the construction of an object type that represents a Tile from the TilePlacer
		/// </summary>
		/// <typeparam name="T">Type of object we are going to create that is a subclass of MapTile</typeparam>
		/// <param name="tile">MapTile object that will be used to create the child class</param>
		/// <returns>newly created object of the Type passed in</returns>
		public delegate T TileConstructor<T>(MapTile tile) where T: MapTile;

		/// <summary>
		/// Loads a TilePlacer map from the file system
		/// </summary>
		/// <param name="content">ContentManager used to load textures for the map</param>
		/// <param name="mapName">Name of the map to load</param>
		/// <returns>LoadResult object containing the data that resulted from the loading of the map</returns>
		public static LoadResult load(ContentManager content, string mapName) {
			LoadResult loadResult = null;
			StreamReader reader = new StreamReader(mapName);
			try {
				MapTile[,] mapTiles = new MapTile[0, 0];// have to assign it here to avoid a compilation error below even though it would be assigned
				TileValues[,] tileValues = new TileValues[0, 0];// have to assign it here to avoid a compilation error below even though it would be assigned
				MapTile mapTile = null;
				Texture2D texture = null;
				TileValues pieceType;
				Point pieceIndex;

				List<Texture2D> textureList = new List<Texture2D>();
				const string TILE_HEADER = "MapTile";
				int height = -1;
				int width = -1;
				int tileNumber;
				int texturesIndex;
				int x, y;
				string textureName;
				string[] components;
				while (!reader.EndOfStream) {
					components = reader.ReadLine().Split('|');
					if (components.Length == 2) {// first line is the size of the map
						height = (Int32.Parse(components[0]));
						width = (Int32.Parse(components[1]));
						mapTiles = new MapTile[height, width];
						tileValues = new TileValues[height, width];
					} else if (components.Length == 3) {// rest of the file are tiles
						texturesIndex = -1;
						tileNumber = -1;
						tileNumber = Int32.Parse((components[0].Replace(TILE_HEADER, "")));
						textureName = components[1];
						// have to remove the file extension
						textureName = textureName.Remove(textureName.IndexOf('.'));
						texture = LoadingUtils.loadTexture2D(content, textureName);
						texturesIndex = textureList.IndexOf(texture);
						if (texturesIndex == -1) {
							textureList.Add(texture);
						} else {
							texture = textureList[texturesIndex];
						}
						if (tileNumber >= width) {
							x = (tileNumber % width);
							y = (tileNumber / width);
						} else {
							x = tileNumber;
							y = 0;
						}

						pieceType = EnumUtils.numberToEnum<TileValues>(Int32.Parse(components[2]));
						pieceIndex = new Point(x, y);
						mapTile = new MapTile(pieceIndex, texture, pieceType, new Vector2(1f));
						mapTiles[pieceIndex.Y, pieceIndex.X] = mapTile;
						tileValues[pieceIndex.Y, pieceIndex.X] = pieceType;
					}
				}
				loadResult = new LoadResult(height, width, mapTiles, tileValues);
			} finally {
				reader.Close();
				reader.Dispose();
			}
			return loadResult;
		}

		/// <summary>
		/// Initializes the type of Tiles passed in based on the MapTile object loaded from the Load(...) process
		/// </summary>
		/// <typeparam name="T">Type of object array we are going to create</typeparam>
		/// <param name="tiles">MapTile array that is used to create the new typed array</param>
		/// <param name="objectConstruction">TileConstructor delegate used to convert the MapTile object to the subclass passed in as T</param>
		/// <returns>T[,] type of objects created from MapTiles[,] passed in</returns>
		public static T[,] initTiles<T>(MapTile[,] tiles, TileConstructor<T> objectConstruction)
		where T:MapTile{
			T[,] mapPieces = new T[tiles.GetUpperBound(0) + 1, tiles.GetUpperBound(1) + 1];
			MapTile tile;
			for (int y = 0; y <= mapPieces.GetUpperBound(0); y++) {
				for (int x = 0; x <= mapPieces.GetUpperBound(1); x++) {
					tile = tiles[y, x];
					if (tile != null) {
						mapPieces[y, x] = objectConstruction.Invoke(tile);
					} else {
						mapPieces[y, x] = null;
					}
				}
			}
			return mapPieces;
		}
	}
}
