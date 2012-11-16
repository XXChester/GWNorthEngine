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
using GWNorthEngine.Engine;
namespace GWNorthEngine.Tools.TilePlacer {
	/// <summary>
	/// Models the basic data of a TilePlacer tile
	/// </summary>
	public class MapTile {
		#region Class propeties
		/// <summary>
		/// Gets or sets Index in the array that this tile resides at
		/// </summary>
		public Point Index { get; set; }
		/// <summary>
		/// Gets or sets the world position of the tile
		/// </summary>
		public virtual Vector2 WorldPosition { get; set; }
		/// <summary>
		/// Gets or sets the Texture the tile uses
		/// </summary>
		public Texture2D Texture { get; set; }
		/// <summary>
		/// Gets or sets the TileValue of the tile which defines which type of tile it is
		/// </summary>
		public TileValues TileValue { get; set; }
		#endregion Class properties

		#region Constructor
		/// <summary>
		/// Basic construction of the MapTile
		/// </summary>
		/// <param name="index">Index the tile resides at in the array</param>
		/// <param name="texture">Texture the tile is rendered with</param>
		public MapTile(Point index, Texture2D texture)
			: this(index, texture, TileValues.Unknown, new Vector2(1f)) {
		}

		/// <summary>
		/// Construction of the MapTile
		/// </summary>
		/// <param name="index">Index the tile resides at in the array</param>
		/// <param name="texture">Texture the tile is rendered with</param>
		/// <param name="tileValue">Type of the tile</param>
		/// <param name="scale">Scale of the tiles</param>
		public MapTile(Point index, Texture2D texture, TileValues tileValue, Vector2 scale) {
			this.Index = index;
			this.Texture = texture;
			this.TileValue = tileValue;
			this.WorldPosition = new Vector2(index.X * (texture.Width * scale.X), index.Y * (texture.Height * scale.Y));
		}
		#endregion Constructor

		#region Destructor
		/// <summary>
		/// Disposes the texture that is used to represent the tile
		/// </summary>
		public virtual void dispose() {
			if (this.Texture != null) {
				this.Texture.Dispose();
			}
		}
		#endregion Destructor
	}
}
