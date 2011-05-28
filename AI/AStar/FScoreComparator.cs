using System.Collections.Generic;
using Microsoft.Xna.Framework;
namespace GWNorthEngine.AI.AStar {
	internal class FScoreComparator : Comparer<PathNode> {
		#region Class variables
		//singleton
		private static FScoreComparator instance = new FScoreComparator();
		#endregion Class variables

		#region Constructor
		public static FScoreComparator getInstance() {
			return instance;
		}
		#endregion Constructor

		#region Support functions
		public override int Compare(PathNode node1, PathNode node2) {
			int result = 0;
			if (node1.FScore < node2.FScore) {
				result = -1;
			} else if (node1.FScore > node2.FScore) {
				result = 1;
			} else {
				// secondary sort
				if (node1.H < node2.H) {
					result = -1;
				} else if (node1.H > node2.H) {
					result = 1;
				}
			}
			return result;
		}
		#endregion Support functions
	}
}
