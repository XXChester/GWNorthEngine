using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GWNorthEngine.Engine;
namespace GWNorthEngine.Model.Params {
	/// <summary>
	/// Object containing the data required to build the BaseRenderer
	/// </summary>
	public class BaseRendererParams {
		#region Class variables
		private int screenWidth;
		private int screenHeight; 
		private string contentRootDirectory;
		private string windowsText;
		private bool mouseVisible;
		private bool fullScreen;
		private RunningMode runningMode;
		#endregion Class variables

		#region Class propeties
		/// <summary>
		/// Gets or sets the initial screen width for the game
		/// </summary>
		public int ScreenWidth { get { return this.screenWidth; } set { this.screenWidth = value; } }
		/// <summary>
		/// Gets or sets the initial screen height for the game
		/// </summary>
		public int ScreenHeight { get { return this.screenHeight; } set { this.screenHeight = value; } }
		/// <summary>
		/// Gets or sets the content root directory for the game
		/// </summary>
		public string ContentRootDirectory { get { return this.contentRootDirectory; } set { this.contentRootDirectory = value; } }
		/// <summary>
		/// Gets or sets the windows text for the game
		/// </summary>
		public string WindowsText { get { return this.windowsText; } set { this.windowsText = value; } }
		/// <summary>
		/// Gets or sets whether the mouse is visible in the application
		/// </summary>
		public bool MouseVisible { get { return this.mouseVisible; } set { this.mouseVisible = value; } }
		/// <summary>
		/// Gets or sets whether the game will be run in full screen mode or note
		/// </summary>
		public bool FullScreen { get { return this.fullScreen; } set { this.fullScreen = value; } }
		/// <summary>
		/// Gets or sets whether the game is running in Release or Debug mode
		/// </summary>
		public RunningMode RunningMode { get { return this.runningMode; } set { this.runningMode = value; } }
		#endregion Class properties

		#region Constructor
		/// <summary>
		/// Builds the default settings for setting up the the BaseRenderer. The default settings are below;
		/// ScreenHeight:			600
		/// ScreenWidth:			800
		/// MouseVisiblity:			true
		/// FullScreen:				false
		/// ContentRootDirectory:	"Content"
		/// WindowText:				"PLEASE FILL ME IN!"
		/// RunningMOdeL			Release
		/// </summary>
		public BaseRendererParams() {
			this.screenHeight = 600;
			this.screenWidth = 800;
			this.mouseVisible = true;
			this.fullScreen = false;
			this.contentRootDirectory = "Content";
			this.windowsText = "PLEASE FILL ME IN!!";
			this.runningMode = Engine.RunningMode.Release;
		}
		#endregion Constructor
	}
}
