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
		/// <returns>Newly created Texture2D object</returns>
		public static Texture2D create2DRingTexture(GraphicsDevice device, int radius) {
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
	}
}
