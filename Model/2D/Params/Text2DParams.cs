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
namespace GWNorthEngine.Model.Params {
	/// <summary>
	/// Object containing the data required to write text to the screen
	/// </summary>
	public class Text2DParams : Base2DSpriteDrawableParams {
		#region Class variables
		private SpriteFont font;
		private string writtenText;
		#endregion Class variables

		#region Class propeties
		/// <summary>
		/// Gets or sets the font to write with
		/// </summary>
		public SpriteFont Font { get { return this.font; } set { this.font = value; } }
		/// <summary>
		/// Gets or sets the text to be written
		/// </summary>
		public string WrittenText { get { return this.writtenText; } set { this.writtenText = value; } }
		#endregion Class properties
	}
}
