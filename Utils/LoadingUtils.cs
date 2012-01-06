using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Reflection;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
namespace GWNorthEngine.Utils {
	/// <summary>
	/// Contains helper methods for loading data from the file system
	/// </summary>
	public static class LoadingUtils {
		/// <summary>
		/// Generic load which appends the asset name to the assets properties if the field is available
		/// </summary>
		/// <typeparam name="T">Type of object to load</typeparam>
		/// <param name="content">ContentManager object</param>
		/// <param name="assetName">Name of the asset to load from the content pipeline</param>
		/// <returns>T object</returns>
		public static T load<T>(ContentManager content, string assetName) {
			T asset = content.Load<T>(assetName);
			if (asset != null) {
				PropertyInfo nameProperty = asset.GetType().GetProperty("Name");
				if (nameProperty != null && nameProperty.CanWrite) {
					nameProperty.SetValue(asset, assetName, null);
				}
			}
			return asset;
		}

		/// <summary>
		/// Loads a Texture2D not via the Content pipeline
		/// </summary>
		/// <param name="device">active GraphicsDevice reference</param>
		/// <param name="fileNameWithPathAndExtension">Texute's name we are trying to load with the full path and file extension (Assets\Test.png)</param>
		/// <returns>Texture2D object</returns>
		/// <author>Largely based on http://jakepoz.com/jake_poznanski__speeding_up_xna.html </author>
		public static Texture2D loadTexture2D(GraphicsDevice device, string fileNameWithPathAndExtension) {
			Texture2D texture = null;
			RenderTarget2D renderTarget = null;

			using (Stream titleStream = TitleContainer.OpenStream(fileNameWithPathAndExtension)) {
				texture = Texture2D.FromStream(device, titleStream);
			}

			//Setup a render target to hold our final texture which will have premulitplied alpha values
			renderTarget = new RenderTarget2D(device, texture.Width, texture.Height);
			device.SetRenderTarget(renderTarget);
			device.Clear(Color.Black);

			//Multiply each color by the source alpha, and write in just the color values into the final texture
			BlendState blendColor = new BlendState();
			blendColor.ColorWriteChannels = ColorWriteChannels.Red | ColorWriteChannels.Green | ColorWriteChannels.Blue;
			blendColor.AlphaDestinationBlend = Blend.Zero;
			blendColor.ColorDestinationBlend = Blend.Zero;
			blendColor.AlphaSourceBlend = Blend.SourceAlpha;
			blendColor.ColorSourceBlend = Blend.SourceAlpha;

			// first pass render the texture to the RenderTarget with a blend state
			SpriteBatch spriteBatch = new SpriteBatch(device);
			spriteBatch.Begin(SpriteSortMode.Immediate, blendColor);
			spriteBatch.Draw(texture, texture.Bounds, Color.White);
			spriteBatch.End();

			//Now copy over the alpha values from the PNG source texture to the final one, without multiplying them
			BlendState blendAlpha = new BlendState();
			blendAlpha.ColorWriteChannels = ColorWriteChannels.Alpha;
			blendAlpha.AlphaDestinationBlend = Blend.Zero;
			blendAlpha.ColorDestinationBlend = Blend.Zero;
			blendAlpha.AlphaSourceBlend = Blend.One;
			blendAlpha.ColorSourceBlend = Blend.One;

			// second pass render the texture to the RenderTarget with an alpha state
			spriteBatch.Begin(SpriteSortMode.Immediate, blendAlpha);
			spriteBatch.Draw(texture, texture.Bounds, Color.White);
			spriteBatch.End();

			//Release the GPU back to drawing to the screen
			device.SetRenderTarget(null);
			texture = renderTarget;
			texture.Name = fileNameWithPathAndExtension;
			return texture;
		}
	}
}
