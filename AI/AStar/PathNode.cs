using Microsoft.Xna.Framework;
namespace GWNorthEngine.AI.AStar {
	/// <summary>
	/// A* node of each moveable space
	/// </summary>
	public class PathNode {
		#region Class variables
		private int g;
		private int h;
		private int fScore;
		private Point position;
		private PathNode parent;
		#endregion Class variables

		#region Class properties
		/// <summary>
		/// Get or sets the H score of a node
		/// </summary>
		public int H { get { return this.h; } set { this.h = value; } }
		/// <summary>
		/// Gets or sets the G score of a node
		/// </summary>
		public int G { get { return this.g; } set { this.g = value; } }
		/// <summary>
		/// Gets or sets the total F score of a node
		/// </summary>
		public int FScore { get { return this.fScore; } set { this.fScore = value; } }
		/// <summary>
		/// Gets or sets the nodes position within the board
		/// </summary>
		public Point Position { get { return this.position; } set { this.position = value; } }
		/// <summary>
		/// Gets or sets the nodes parent node
		/// </summary>
		public PathNode Parent { get { return this.parent; } set { this.parent = value; } }
		#endregion Class properties

		#region Constructor
		/// <summary>
		/// Construction of the PathNode object
		/// </summary>
		/// <param name="parent">Parent of the node</param>
		/// <param name="position">Position within the board of the node</param>
		/// <param name="startDistance">Distance from the start</param>
		/// <param name="endDistance">Distance from the end</param>
		public PathNode(PathNode parent, Point position, int startDistance, int endDistance) {
			this.parent = parent;
			this.position = position;
			this.g = startDistance;
			this.h = endDistance;
			calculateFScore();
		}
		#endregion Constructor

		#region Support functions
		/// <summary>
		/// Calculates and assigns the FScore of a node based on its G and H scores
		/// </summary>
		public void calculateFScore() {
			this.fScore = this.g + this.h;
		}

		/// <summary>
		/// Determines if two Nodes have an equal F score and H score
		/// </summary>
		/// <param name="compareWith">Node to compare with this node</param>
		/// <returns>true if the nodes are equal, otherwise false</returns>
		public bool fScoresEqual(PathNode compareWith) {
			bool result = false;
			if (this.fScore == compareWith.FScore) {
				if (this.h == compareWith.H) {
					result = true;
				}
			}
			return result;
		}

		/// <summary>
		/// Tostring of the node
		/// </summary>
		/// <returns>String of the node object</returns>
		public override string ToString() {
			return ("H: " + h + " G:" + g + " fScore: " + fScore + " pointX: " + position.X + " pointY: " + position.Y);
		}
		#endregion Support functions
	}
}
