using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using GWNorthEngine.Utils;
namespace GWNorthEngine.Input {
	/// <summary>
	/// Manager that handles the states of different input devices
	/// </summary>
	public class InputManager {
		#region Class variables
		//singleton instance
		private static InputManager instance = new InputManager();

		private KeyboardState previousKeyboardState;
		private KeyboardState currentKeyboardState;
		private MouseState previousMouseState;
		private MouseState currentMouseState;
		private GamePadState[] previousGamePadStates;
		private GamePadState[] currentGamePadStates;
		#endregion Class variables

		#region Class properties
		/// <summary>
		/// Returns the mouses X position
		/// </summary>
		public int MouseX { get { return this.currentMouseState.X; } }
		
		/// <summary>
		/// Returns the mouses Y position
		/// </summary>
		public int MouseY { get { return this.currentMouseState.Y; } }

		/// <summary>
		/// Returns a Vector2 of the mouses position
		/// </summary>
		public Vector2 MousePosition { get { return new Vector2(MouseX, MouseY); } }
		#endregion Class properties

		#region Constructor
		/// <summary>
		/// Sets the default values of the input manager
		/// </summary>
		public InputManager() {
			this.currentKeyboardState = Keyboard.GetState();
			this.currentMouseState = Mouse.GetState();

			const int NUMBER_OF_GAME_PADS = 4;
			this.currentGamePadStates = new GamePadState[NUMBER_OF_GAME_PADS];
			this.previousGamePadStates = new GamePadState[NUMBER_OF_GAME_PADS];
			for (int i = 0; i < NUMBER_OF_GAME_PADS; i++) {
				this.currentGamePadStates[i] = GamePad.GetState(EnumUtils.numberToEnum<PlayerIndex>(i));
			}
		}
		#endregion Constructor

		#region Support methods
		/// <summary>
		/// Retrieves the singleton instance
		/// </summary>
		/// <returns>InputManager</returns>
		public static InputManager getInstance() {
			return instance;
		}

		/// <summary>
		/// Main loop that updates all of the devices
		/// </summary>
		public void update() {
			this.previousKeyboardState = this.currentKeyboardState;
			this.previousMouseState = this.currentMouseState;
			for (int i = 0; i < this.previousGamePadStates.Length; i++) {
				this.previousGamePadStates[i] = this.currentGamePadStates[i];
			}

			this.currentKeyboardState = Keyboard.GetState();
			this.currentMouseState = Mouse.GetState();
			for (int i = 0; i < this.currentGamePadStates.Length; i++) {
				this.currentGamePadStates[i] = GamePad.GetState(EnumUtils.numberToEnum<PlayerIndex>(i));
			}
		}

		#region Keyboard
		/// <summary>
		/// Determines if a key was just pressed down
		/// </summary>
		/// <param name="key">Key that we are checking for</param>
		/// <returns>True if the key was just pressed, otherwise false</returns>
		public bool wasKeyPressed(Keys key) {
			bool result = false;
			if (this.previousKeyboardState.IsKeyUp(key) && this.currentKeyboardState.IsKeyDown(key)) {
				result = true;
			}
			return result;
		}

		/// <summary>
		/// Determines if a key was just released
		/// </summary>
		/// <param name="key">Key that we are checking for</param>
		/// <returns>True if the key was just released, otherwise false</returns>
		public bool wasKeyReleased(Keys key) {
			bool result = false;
			if (this.previousKeyboardState.IsKeyDown(key) && this.currentKeyboardState.IsKeyUp(key)) {
				result = true;
			}
			return result;
		}

		/// <summary>
		/// Determines if a key is held down
		/// </summary>
		/// <param name="key">Key that we are checking for</param>
		/// <returns>True if the key is held, otherwise false</returns>
		public bool isKeyDown(Keys key) {
			bool result = false;
			if (this.previousKeyboardState.IsKeyDown(key) && this.currentKeyboardState.IsKeyDown(key)) {
				result = true;
			}
			return result;
		}
		#endregion Keyboard

		#region Mouse
		/// <summary>
		/// Determines if a button was just pressed down
		/// </summary>
		/// <param name="button">Button that we are checking for</param>
		/// <returns>True if the button was just pressed, otherwise false</returns>
		public bool wasButtonPressed(MouseButton button) {
			bool result = false;
			if (MouseButton.Left.Equals(button)) {
				result = wasLeftButtonPressed();
			} else if (MouseButton.Right.Equals(button)) {
				result = wasRightButtonPressed();
			} else if (MouseButton.Middle.Equals(button)) {
				result = wasMiddleButtonPressed();
			}
			return result;
		}

		/// <summary>
		/// Determines if a button was just released
		/// </summary>
		/// <param name="button">Button that we are checking for</param>
		/// <returns>True if the button was just released, otherwise false</returns>
		public bool wasButtonReleased(MouseButton button) {
			bool result = false;
			if (MouseButton.Left.Equals(button)) {
				result = wasLeftButtonReleased();
			} else if (MouseButton.Right.Equals(button)) {
				result = wasRightButtonReleased();
			} else if (MouseButton.Middle.Equals(button)) {
				result = wasMiddleButtonReleased();
			}
			return result;
		}

		/// <summary>
		/// Determines if a button is held down
		/// </summary>
		/// <param name="button">Button that we are checking for</param>
		/// <returns>True if the button is held down, otherwise false</returns>
		public bool isButtonDown(MouseButton button) {
			bool result = false;
			if (MouseButton.Left.Equals(button)) {
				result = isLeftButtonDown();
			} else if (MouseButton.Right.Equals(button)) {
				result = isRightButtonDown();
			} else if (MouseButton.Middle.Equals(button)) {
				result = isMiddleButtonDown();
			}
			return result;
		}

		/// <summary>
		/// Determines if the left mouse button was just pressed down
		/// </summary>
		/// <returns>True if the button was just pressed, otherwise false</returns>
		public bool wasLeftButtonPressed() {
			bool result = false;
			if (this.previousMouseState.LeftButton == ButtonState.Released && this.currentMouseState.LeftButton == ButtonState.Pressed) {
				result = true;
			}
			return result;
		}

		/// <summary>
		/// Determines if the left mouse button was just released
		/// </summary>
		/// <returns>True if the button was just released, otherwise false</returns>
		public bool wasLeftButtonReleased() {
			bool result = false;
			if (this.previousMouseState.LeftButton == ButtonState.Pressed && this.currentMouseState.LeftButton == ButtonState.Released) {
				result = true;
			}
			return result;
		}

		/// <summary>
		/// Determines if the left mouse button is held down
		/// </summary>
		/// <returns>True if the button is held down, otherwise false</returns>
		public bool isLeftButtonDown() {
			bool result = false;
			if (this.previousMouseState.LeftButton == ButtonState.Pressed && this.currentMouseState.LeftButton == ButtonState.Pressed) {
				result = true;
			}
			return result;
		}

		/// <summary>
		/// Determines if the right mouse button was just pressed down
		/// </summary>
		/// <returns>True if the button was just pressed, otherwise false</returns>
		public bool wasRightButtonPressed() {
			bool result = false;
			if (this.previousMouseState.RightButton == ButtonState.Released && this.currentMouseState.RightButton == ButtonState.Pressed) {
				result = true;
			}
			return result;
		}

		/// <summary>
		/// Determines if the left mouse button was just released
		/// </summary>
		/// <returns>True if the button was just released, otherwise false</returns>
		public bool wasRightButtonReleased() {
			bool result = false;
			if (this.previousMouseState.RightButton == ButtonState.Pressed && this.currentMouseState.RightButton == ButtonState.Released) {
				result = true;
			}
			return result;
		}

		/// <summary>
		/// Determines if the right mouse button is held down
		/// </summary>
		/// <returns>True if the button is held down, otherwise false</returns>
		public bool isRightButtonDown() {
			bool result = false;
			if (this.previousMouseState.RightButton == ButtonState.Pressed && this.currentMouseState.RightButton == ButtonState.Pressed) {
				result = true;
			}
			return result;
		}

		/// <summary>
		/// Determines if the middle mouse button was just pressed down
		/// </summary>
		/// <returns>True if the button was just pressed, otherwise false</returns>
		public bool wasMiddleButtonPressed() {
			bool result = false;
			if (this.previousMouseState.MiddleButton == ButtonState.Released && this.currentMouseState.MiddleButton == ButtonState.Pressed) {
				result = true;
			}
			return result;
		}

		/// <summary>
		/// Determines if the left mouse button was just released
		/// </summary>
		/// <returns>True if the button was just released, otherwise false</returns>
		public bool wasMiddleButtonReleased() {
			bool result = false;
			if (this.previousMouseState.MiddleButton == ButtonState.Pressed && this.currentMouseState.MiddleButton == ButtonState.Released) {
				result = true;
			}
			return result;
		}

		/// <summary>
		/// Determines if the middle mouse button is held down
		/// </summary>
		/// <returns>True if the button is held down, otherwise false</returns>
		public bool isMiddleButtonDown() {
			bool result = false;
			if (this.previousMouseState.MiddleButton == ButtonState.Pressed && this.currentMouseState.MiddleButton == ButtonState.Pressed) {
				result = true;
			}
			return result;
		}
		#endregion Mouse

		#region XBOX
		/// <summary>
		/// Determines if a specific player just pressed a buton
		/// </summary>
		/// <param name="playerIndex">Player we are checking</param>
		/// <param name="button">Button that we are checking</param>
		/// <returns>True if the player's button was just pressed, otherwise false</returns>
		public bool wasButtonPressed(PlayerIndex playerIndex, Buttons button) {
			bool result = false;
			if (this.previousGamePadStates[(int)playerIndex].IsButtonUp(button) && this.currentGamePadStates[(int)playerIndex].IsButtonDown(button)) {
				result = true;
			}
			return result;
		}

		/// <summary>
		/// Determines if a specific player's buton was just released
		/// </summary>
		/// <param name="playerIndex">Player we are checking</param>
		/// <param name="button">Button that we are checking</param>
		/// <returns>True if the player's button was just released, otherwise false</returns>
		public bool wasButtonReleased(PlayerIndex playerIndex, Buttons button) {
			bool result = false;
			if (this.previousGamePadStates[(int)playerIndex].IsButtonDown(button) && this.currentGamePadStates[(int)playerIndex].IsButtonUp(button)) {
				result = true;
			}
			return result;
		}

		/// <summary>
		/// Determines if a specific player is holding a button down
		/// </summary>
		/// <param name="playerIndex">Player we are checking</param>
		/// <param name="button">Button that we are checking</param>
		/// <returns>True if the player's button is held down, otherwise false</returns>
		public bool isButtonDown(PlayerIndex playerIndex, Buttons button) {
			bool result = false;
			if (this.previousGamePadStates[(int)playerIndex].IsButtonDown(button) && this.currentGamePadStates[(int)playerIndex].IsButtonDown(button)) {
				result = true;
			}
			return result;
		}
		#endregion
		#endregion Support methods
	}
}
