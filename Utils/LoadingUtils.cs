using System;
using System.Collections.Generic;
using System.Linq;
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
		/// Loads a texture via the ContentManager and assigns the texture custom data for debugging purposes
		/// </summary>
		/// <param name="content">ContentManager object</param>
		/// <param name="textureName">Name of the texture</param>
		/// <returns>Texture2D object</returns>
		public static Texture2D loadTexture2D(ContentManager content, string textureName) {
			Texture2D texture = content.Load<Texture2D>(textureName);
			texture.Name = textureName;
			return texture;
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
