using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GWNorthEngine.Engine.Params;
namespace GWNorthEngine.Engine {
	public class DefaultCamera : BaseCamera {
		#region Constructor
		public DefaultCamera(DefaultCameraParams parms)
			: base(parms) {
		}
		#endregion Constructor

		#region Support methods
		public override void update() {
			base.updateViewMatrix();
		}
		#endregion Support methods
	}
}
