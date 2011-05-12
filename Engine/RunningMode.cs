using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GWNorthEngine.Engine {
	/// <summary>
	/// Enum used to tell the Engine DLL what mode the front end application is running in
	/// </summary>
	public enum RunningMode {
		/// <summary>
		/// Front end application is in release mode
		/// </summary>
		Release,
		/// <summary>
		/// Front end application is in debug mode
		/// </summary>
		Debug
	}
}
