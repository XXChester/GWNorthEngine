using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
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
		#region Visual
		/// <summary>
		/// Sets all of the common data for the Texture2D
		/// </summary>
		/// <param name="texture">Loaded Texture2D to set the data on</param>
		/// <param name="fileName">Name of the file</param>
		/// <returns>Texture2D with the custom data appended to it</returns>
		private static Texture2D setCommonAttributes(Texture2D texture, string fileName) {
			fileName = StringUtils.scrubPathAndExtFromFileName(fileName);
			texture.Name = fileName;
			return texture;
		}

		/// <summary>
		/// Loads a texture via the ContentManager and assigns the texture custom data for debugging purposes
		/// </summary>
		/// <param name="content">ContentManager object</param>
		/// <param name="textureName">Name of the texture</param>
		/// <returns>Texture2D object</returns>
		public static Texture2D loadTexture2D(ContentManager content, string textureName) {
			Texture2D texture = content.Load<Texture2D>(textureName);
			return (setCommonAttributes(texture, textureName));
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
			return setCommonAttributes(texture, fileNameWithPathAndExtension);
		}
		
		/// <summary>
		/// Loads a SpriteFont via the ContentManager and assigns the font custom data for debugging purposes
		/// </summary>
		/// <param name="content">ContentManager object</param>
		/// <param name="spriteFontName">Name of the sprite font</param>
		/// <returns>Texture2D object</returns>
		public static SpriteFont loadSpriteFont(ContentManager content, string spriteFontName) {
			SpriteFont font = content.Load<SpriteFont>(spriteFontName);
			//TODO: Custom crap...no name field for fonts =/
			return font;
		}
		#endregion Visual

		#region Audio
		/// <summary>
		/// Loads a SoundEffect via the ContentManager and assigns the sound effect custom data for debugging purposes
		/// </summary>
		/// <param name="content">ContentManager object</param>
		/// <param name="soundEffectName">Name of the sound effect</param>
		/// <returns>Texture2D object</returns>
		public static SoundEffect loadSoundEffect(ContentManager content, string soundEffectName) {
			SoundEffect sfx = content.Load<SoundEffect>(soundEffectName);
			sfx.Name = soundEffectName;
			return sfx;
		}

		/// <summary>
		/// Loads a Song via the ContentManager and assugbs the song custom data for debugging purposes
		/// </summary>
		/// <param name="content">ContentManager object</param>
		/// <param name="songName">Name of the song</param>
		/// <returns></returns>
		public static Song loadSong(ContentManager content, string songName) {
			Song song = content.Load<Song>(songName);
			//TODO: Custom crap....name field is readonly
			return song;
		}
		#endregion Audio

	}
}
