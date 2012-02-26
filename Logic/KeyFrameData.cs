using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GWNorthEngine.Logic {
	/// <summary>
	/// Models the data required for Key Frame Animation
	/// </summary>
	public struct KeyFrameData : ICloneable {
		#region Class properties
		/// <summary>
		/// Gets or sets the frame we start animating from
		/// </summary>
		public int StartingKeyFrame { get; set; }
		/// <summary>
		/// Gets or sets the frame we are to end with
		/// </summary>
		public int EndingKeyFrame { get; set; }
		/// <summary>
		/// Gets or sets the Frame Rate for the animation set
		/// </summary>
		public float FrameRate { get; set; }
		/// <summary>
		/// Gets or sets the Name of the KeyFrameData
		/// </summary>
		public string Name { get; set; }
		/// <summary>
		/// Gets or sets whether we want to reset the animation when we activate this KeyFrameData object
		/// </summary>
		public bool ResetAnimationOnSet { get; set; }
		#endregion Class properties

		#region Constructor
		/// <summary>
		/// Constructs a KeyFrameData object
		/// </summary>
		/// <param name="startingKeyFrame">Frame the animation starts at</param>
		/// <param name="endingKeyFrame">Frame the animation ends at</param>
		/// <param name="frameRate">Frame rate to run the animation set at</param>
		/// <param name="name">Name of the KeyFrameData</param>
		/// <param name="resetAnimationOnSet">Determines whether we want to rest the animation when we activate this object</param>
		public KeyFrameData(int startingKeyFrame, int endingKeyFrame, float frameRate, string name, bool resetAnimationOnSet=true) 
			: this() {
			this.StartingKeyFrame = startingKeyFrame;
			this.EndingKeyFrame = endingKeyFrame;
			this.FrameRate = frameRate;
			this.Name = name;
			this.ResetAnimationOnSet = resetAnimationOnSet;
		}
		#endregion Constructor

		#region Support methods
		/// <summary>
		/// Clones the KeyFrameData object
		/// </summary>
		/// <returns>Cloned KeyFrameData object</returns>
		public object Clone() {
			return new KeyFrameData(this.StartingKeyFrame, this.EndingKeyFrame, this.FrameRate, this.Name, this.ResetAnimationOnSet);
		}
		#endregion Support methods
	}
}
