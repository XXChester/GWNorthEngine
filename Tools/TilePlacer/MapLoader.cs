using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
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
		/// Loads the metadata information from an XmlDocument
		/// </summary>
		/// <param name="map">XmlDocument containing the metadata information</param>
		/// <param name="height">Height of the map</param>
		/// <param name="width">Width of the map</param>
		private static void loadMapMetadata(XmlDocument map, ref int height, ref int width) {
			XmlNode metaNode;
			XmlNode childNode;
			XmlNodeList metaNodes = map.GetElementsByTagName(Constants.XML_HEADER_SIZE);
			XmlNodeList childNodes;
			for (int i = 0; i < metaNodes.Count; i++) {
				metaNode = metaNodes[i];
				if (metaNode != null) {
					childNodes = metaNode.ChildNodes;
					// go through his children
					if (childNodes != null) {
						for (int j = 0; j < childNodes.Count; j++) {
							childNode = childNodes[j];
							switch (childNode.Name) {
								case Constants.XML_HEIGHT:
									height = Int32.Parse(childNode.FirstChild.Value);
									break;
								case Constants.XML_WIDTH:
									width = Int32.Parse(childNode.FirstChild.Value);
									break;
							}
						}
					}
				}
			}
		}

		/// <summary>
		/// Loads the tiles of a map
		/// </summary>
		/// <param name="content">ContentManager used to load textures for the map</param>
		/// <param name="map">XmlDocument object containing the tile information</param>
		/// <param name="width">Width of the map</param>
		/// <param name="mapTiles">MapTiles[,] loaded via the xml</param>
		/// <param name="tileValues">TileValues[,] loaded via the xml</param>
		private static void loadMap(ContentManager content, XmlDocument map, int width, ref MapTile[,] mapTiles, ref TileValues[,] tileValues) {
			MapTile mapTile = null;
			Texture2D texture = null;
			TileValues pieceType;
			Point pieceIndex;
			List<Texture2D> textureList = new List<Texture2D>();
			int tileNumber;
			int texturesIndex;
			int x, y;
			string textureName;
			XmlNode tileNode;
			XmlNode childNode;
			XmlNodeList tileNodes = map.GetElementsByTagName(Constants.XML_HEADER_TILE);
			XmlNodeList childNodes;
			for (int i = 0; i < tileNodes.Count; i++) {
				tileNode = tileNodes[i];
				texturesIndex = -1;
				tileNumber = -1;
				if (tileNode != null) {
					tileNumber = -1;
					textureName = null;
					pieceType = TileValues.Unknown;
					// go through his children
					childNodes = tileNode.ChildNodes;
					if (childNodes != null) {
						for (int j = 0; j < childNodes.Count; j++) {
							childNode = childNodes[j];
							switch (childNode.Name) {
								case Constants.XML_TILE_FILE_NAME:
									textureName = childNode.FirstChild.Value.Remove(childNode.FirstChild.Value.IndexOf('.'));
									break;
								case Constants.XML_TILE_INDEX:
									tileNumber = Int32.Parse(childNode.FirstChild.Value);
									break;
								case Constants.XML_TILE_VALUE:
									pieceType = EnumUtils.numberToEnum<TileValues>(Int32.Parse(childNode.FirstChild.Value));
									break;
							}
						}
						texture = LoadingUtils.loadTexture2D(content, textureName);
						texturesIndex = textureList.IndexOf(texture);
						if (texturesIndex == -1) {
							textureList.Add(texture);
						} else {
							texture = textureList[texturesIndex];
						}
						x = tileNumber % width;
						y = tileNumber / width;
						pieceIndex = new Point(x, y);
						mapTile = new MapTile(pieceIndex, texture, pieceType, new Vector2(1f));
						mapTiles[pieceIndex.Y, pieceIndex.X] = mapTile;
						tileValues[pieceIndex.Y, pieceIndex.X] = pieceType;
					}
				}
			}
		}

		/// <summary>
		/// Loads a TilePlacer map from the file system
		/// </summary>
		/// <param name="content">ContentManager used to load textures for the map</param>
		/// <param name="mapName">Name of the map to load</param>
		/// <returns>LoadResult object containing the data that resulted from the loading of the map</returns>
		public static LoadResult load(ContentManager content, string mapName) {
			LoadResult loadResult = null;
			XmlDocument map = new XmlDocument();
			map.Load(mapName);
			int height = -1;
			int width = -1;
			
			// load the meta information
			loadMapMetadata(map, ref height, ref width);
			MapTile[,] mapTiles = new MapTile[height, width];
			TileValues[,] tileValues = new TileValues[height, width];
			
			// load the tiles
			loadMap(content, map, width, ref mapTiles, ref tileValues);
			
			// populate our result object
			loadResult = new LoadResult(height, width, mapTiles, tileValues);
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
