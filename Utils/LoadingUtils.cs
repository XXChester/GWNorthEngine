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
		
		/// <summary>
		/// Loads a texture via the ContentManager and assigns the texture a name for debugging purposes
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
		/// Loads a SoundEffect via the ContentManager and assigns the sound effect a name for debugging purposes
		/// </summary>
		/// <param name="content">ContentManager object</param>
		/// <param name="soundEffectName">Name of the sound effect</param>
		/// <returns>Texture2D object</returns>
		public static SoundEffect loadSoundEffect(ContentManager content, string soundEffectName) {
			SoundEffect sfx = content.Load<SoundEffect>(soundEffectName);
			sfx.Name = soundEffectName;
			return sfx;
		}

	}
}
