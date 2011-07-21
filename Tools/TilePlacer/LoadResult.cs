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
		/// Gets or sets the Layers of the map
		/// </summary>
		public Layer[] Layers { get; set; }
		#endregion Class properties

		#region Class constructor
		/// <summary>
		/// Constructs a LoadResult object
		/// </summary>
		/// <param name="height">Height of the map</param>
		/// <param name="width">Width of the map</param>
		/// <param name="layers">Layers of MapTile objects that make the map</param>
		public LoadResult(int height, int width, Layer[] layers) {
			this.Height = height;
			this.Width = width;
			this.Layers = layers;
		}
		#endregion Class constructor
	}
}
