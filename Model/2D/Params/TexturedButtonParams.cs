using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
namespace GWNorthEngine.Model.Params {
	/// <summary>
	/// Params object containing the data required to build a TexturedButton object
	/// </summary>
	public class TexturedButtonParams : BaseButtonParams {
		#region Class properties
		/// <summary>
		/// Gets or sets the ContentManager object used for loading textures
		/// </summary>
		public ContentManager Content { get; set; }
		/// <summary>
		/// Gets or sets the regular texture's file name for the button
		/// </summary>
		public string RegularTextureFileName { get; set; }
		/// <summary>
		/// Gets or sets the buttons mouse over texture file name
		/// </summary>
		public string MouseOverTextureFileName { get; set; }
		#endregion Class properties
	}
}
