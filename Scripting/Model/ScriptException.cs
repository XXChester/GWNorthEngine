using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GWNorthEngine.Scripting.Model {
	/// <summary>
	/// Used to handle exceptions thrown within the script management
	/// </summary>
	internal class ScriptException : Exception {
		/// <summary>
		/// Constructor to create a new ScriptException
		/// </summary>
		/// <param name="message">Message to send to the console</param>
		public ScriptException(string message)
			: base(message) {
		}
	}
}
