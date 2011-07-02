using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GWNorthEngine.Audio {
	internal abstract class BaseWrapper {
		#region Class properties
		public string Name { get; set; }
		#endregion Class properties

		#region Constructor
		public BaseWrapper(string name) {
			this.Name = name;
		}
		#endregion Constructor

		#region Destructor
		public abstract void dispose();
		#endregion Destructor
	}
}
