using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace GWNorthEngine.Utils {
	/// <summary>
	/// Utility class to perform operations on texures or involving textures
	/// </summary>
	public class TextureUtils {
		/// <summary>
		/// Creates a 2D coloured texture
		/// </summary>
		/// <param name="device">GraphicsDevice object</param>
		/// <param name="width">Width of the texture</param>
		/// <param name="height">Height of the texture</param>
		/// <param name="colour">Colour of the texture</param>
		/// <returns>Newly created Texture2D object</returns>
		public static Texture2D create2DColouredTexture(GraphicsDevice device, int width, int height, Color colour) {
			Color[] colours = new Color[width * height];
			for (int y = 0; y < height; y++) {
				for (int x = 0; x < width; x++) {
					colours[y + x * width] = colour;
				}
			}
			Texture2D texture = new Texture2D(device, width, height, false, SurfaceFormat.Color);
			texture.SetData<Color>(colours, 0, width * height);
			return texture;
		}

		/// <summary>
		/// Creates a 2D coloured ring texture
		/// </summary>
		/// <param name="device">GraphicsDevice object</param>
		/// <param name="radius">Radius of the circle</param>
		/// /// <param name="colour">Colour of the texture</param>
		/// <returns>Newly created Texture2D object</returns>
		public static Texture2D create2DRingTexture(GraphicsDevice device, int radius, Color colour) {
			// taken from an answer at http://stackoverflow.com/questions/2983809/how-to-draw-circle-with-specific-color-in-xna
			int outerRadius = radius * 2 + 2; // So circle doesn't go out of bounds
			Texture2D texture = new Texture2D(device, outerRadius, outerRadius);

			Color[] data = new Color[outerRadius * outerRadius];

			// Colour the entire texture transparent first.
			for (int i = 0; i < data.Length; i++) {
				data[i] = Color.Transparent;
			}

			// Work out the minimum step necessary using trigonometry + sine approximation.
			double angleStep = 1f / radius;

			int x;
			int y;
			for (double angle = 0; angle < Math.PI * 2; angle += angleStep) {
				x = (int)Math.Round(radius + radius * Math.Cos(angle));
				y = (int)Math.Round(radius + radius * Math.Sin(angle));

				data[y * outerRadius + x + 1] = Color.White;
			}

			texture.SetData<Color>(data, 0, data.Length);
			return texture;
		}

		/// <summary>
		/// Retrieves the texures colour information
		/// </summary>
		/// <param name="texture">Texture to retrieve the colour array from</param>
		/// <returns>Colour[] of the texture</returns>
		public static Color[] getColourData(Texture2D texture) {
			Color[] colourData = new Color[texture.Height * texture.Width];
			texture.GetData<Color>(colourData);

			return colourData;
		}

		/*public static Color[,] getColourData2D(Texture2D texture, int startX = 0, int startY = 0, int width = -1, int height = -1) {
			int indexX = 0;
			int indexY = 0;
			if (height == -1) {
				height = texture.Height;
			}
			if (width == -1) {
				width = texture.Width;
			}
			Color[] colors1D = getColourData(texture);

			Color[,] colors2D = new Color[height, width];
			for (int y = startY; y < height; y++) {
			
				indexX = 0;
				for (int x = startX; x < width; x++) {	
					colors2D[indexY, indexX] = colors1D[y + x * height];
					indexX++;
				}
				indexY++;
			}

			return colors2D;
		}*/

		/// <summary>
		/// Retrieves the Texture's colour data in a 2D array
		/// </summary>
		/// <remarks>The texture data is in x,y format not y,x format at this time</remarks>
		/// <param name="texture">Texture that we want to analyze</param>
		/// <param name="startX">Optional parameter of where to start on the X axis in the texture</param>
		/// <param name="startY">Optional parameter of where to start on the Y axis in the texture</param>
		/// <param name="width">Optional parameter of where to stop analyzing on the X axis</param>
		/// <param name="height">Optional parameter of where to stop analyzing on the Y axis</param>
		/// <returns>Color[,] of the texture</returns>
		public static Color[,] getColourData2D(Texture2D texture, int startX = 0, int startY = 0, int width = -1, int height = -1) {
			//TODO: Change the order to be Y,X instead of X,Y to fit the rest of my programs
			int indexX = 0;
			int indexY = 0;
			if (height == -1) {
				height = texture.Height;
			}
			if (width == -1) {
				width = texture.Width;
			}
			Color[] colors1D = getColourData(texture);

			Color[,] colors2D = new Color[width, height];
			for (int x = startX; x < width; x++) {
				indexY = 0;
				for (int y = startY; y < height; y++) {
					colors2D[indexX, indexY] = colors1D[x + y * width];
					indexY++;
				}
				indexX++;
			}

			return colors2D;
		}
	}
}
