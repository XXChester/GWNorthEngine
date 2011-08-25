using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Runtime.InteropServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using GWNorthEngine.Engine.Params;
using GWNorthEngine.Scripting;

namespace GWNorthEngine.Engine {
	/// <summary>
	///Base renderer for an XNA based applicaton
	/// </summary>
	public class BaseRenderer : Game {
		#region Class variables
		/// <summary>
		/// GraphicsDeviceManager object
		/// </summary>
		protected GraphicsDeviceManager graphics;
		/// <summary>
		/// SpriteBatch object
		/// </summary>
		protected SpriteBatch spriteBatch;
		/// <summary>
		/// RunningMode enum
		/// </summary>
		protected RunningMode runningMode;
		private bool scriptConsoleRunning;
		private MouseState previousMouseState;	//used for scripting
		#region External declarations
		private BaseRendererParams builtWithParms;
		[DllImport("kernel32.dll")]
		private static extern IntPtr GetConsoleWindow();
		[DllImport("user32.dll")]
		private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
		[DllImport("user32.dll", EntryPoint = "SetWindowPos")]
		private static extern IntPtr SetWindowPos(IntPtr hWnd, int hWndInsertAfter, int x, int Y, int cx, int cy, int wFlags);
		#endregion External declarations

		#endregion Class variables

		#region Cosntructor

		#endregion Constructor

		#region Initialization
		/// <summary>
		/// Initializes variables based on the parms object
		/// </summary>
		/// <param name="parms">BaseRendererParms object</param>
		protected void initialize(BaseRendererParams parms) {
			this.builtWithParms = parms;
			this.graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = parms.ContentRootDirectory;
			this.IsMouseVisible = parms.MouseVisible;
			this.runningMode = parms.RunningMode;
		}

		/// <summary>
		/// Allows the game to perform any initialization it needs to before starting to run.
		/// This is where it can query for any required services and load any non-graphic
		/// related content.  Calling base.Initialize will enumerate through any components
		/// and initialize them as well.
		/// </summary>
		protected override void Initialize() {
			// Create a new SpriteBatch, which can be used to draw textures.
			spriteBatch = new SpriteBatch(GraphicsDevice);
			this.graphics.PreferredBackBufferWidth = this.builtWithParms.ScreenWidth;
			this.graphics.PreferredBackBufferHeight = this.builtWithParms.ScreenHeight;
			this.graphics.IsFullScreen = this.builtWithParms.FullScreen;
			this.graphics.ApplyChanges();
			this.Window.Title = this.builtWithParms.WindowsText;
			base.Initialize();

			try {
#if WINDOWS
#if DEBUG

				// Incase the Engine is running in DEBUG mode but the application is in Release mode
				if (this.runningMode == RunningMode.Debug) {
					SetWindowPos(GetConsoleWindow(), 0, 0, 0, 0, 0, 0x0001);
					Thread consoleInputThread = new Thread(new ThreadStart(listenForInput));
					consoleInputThread.Start();
					this.scriptConsoleRunning = true;
				} else {
					ShowWindow(GetConsoleWindow(), 0);
				}
#else
			ShowWindow(GetConsoleWindow(), 0);
#endif
#else
			ShowWindow(GetConsoleWindow(), 0);
#endif
			} catch (InvalidOperationException) {
				// Means we are not running a console window so just exit the method
			}
		}

#if WINDOWS
#if DEBUG
		private void listenForInput() {
			try {
				// Incase the Engine is running in DEBUG mode but the application is in Release mode
				if (this.runningMode == RunningMode.Debug) {
					string input;
					do {
						Console.Write(">>");
						while (!Console.KeyAvailable) {
							if (!this.scriptConsoleRunning) {
								return;
							}
							Thread.Sleep(50);
						}
						input = Console.ReadLine();
						ScriptManager.getInstance().handleInput(input);
					} while (this.scriptConsoleRunning);
				}
			} catch (InvalidOperationException) {
				// Means we are not running a console window so just exit the method
			}
		}
#endif
#endif
		#endregion Initialization

		#region Support methods
		/// <summary>
		/// Allows the games scripting component to run and lock on to objects for mapping purposes
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Update(GameTime gameTime) {
#if WINDOWS
#if DEBUG
			// Incase the Engine is running in DEBUG mode but the application is in Release mode
			if (this.runningMode == RunningMode.Debug) {
				if (ScriptManager.getInstance().LockOnEnabled) {
					MouseState currentState = Mouse.GetState();
					if (this.previousMouseState.LeftButton == ButtonState.Released && currentState.LeftButton == ButtonState.Pressed) {
						ScriptManager.getInstance().handleMouseClick(new Vector2(currentState.X, currentState.Y));
					}
					ScriptManager.getInstance().handleMouseMovement(new Vector2(currentState.X, currentState.Y));
					this.previousMouseState = currentState;
				}
			}
#endif
#endif
			base.Update(gameTime);
		}
		/// <summary>
		/// UnloadContent will be called once per game and is the place to unload all content
		/// </summary>
		protected override void UnloadContent() {
			this.scriptConsoleRunning = false;
			Content.Unload();//incase we didn't dispose something ourself
			Content.Dispose();
		}
		#endregion Support methods
	}
}
