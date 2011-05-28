using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
namespace GWNorthEngine.AI.AStar {
	// built using the steps described at http://www.policyalmanac.org/games/aStarTutorial.htm
	/// <summary>
	/// Abstract class containing the common information for basic A* Pathfinding
	/// </summary>
	public abstract class PathFinder {
		/// <summary>
		/// Type of spaces the board can contain
		/// </summary>
		public enum TypeOfSpace {
			/// <summary>
			/// Starting position on the board
			/// </summary>
			Start,
			/// <summary>
			/// Non-moveable piece on the board
			/// </summary>
			Unwalkable,
			/// <summary>
			/// Moveable piece on the board
			/// </summary>
			Walkable,
			/// <summary>
			/// Goal position on the board
			/// </summary>
			End
		}

		/// <summary>
		/// Restriction type used for searching of the next node
		/// </summary>
		public enum RestrictionType {
			/// <summary>
			/// Can only generates moves in a zig zag pattern, cannot cut corners or move diagonally
			/// </summary>
			Restricted,
			/// <summary>
			/// Can generate moves in any direction
			/// </summary>
			All
		}

		#region Class variables
		/// <summary>
		/// Height of the board
		/// </summary>
		protected readonly int HEIGHT;
		/// <summary>
		/// Width of the board
		/// </summary>
		protected readonly int WIDTH;
		/// <summary>
		/// Directions we can generate moves in
		/// </summary>
		protected int[,] directions;
		/// <summary>
		/// A* readable board representation
		/// </summary>
		protected TypeOfSpace[,] board;
		/// <summary>
		/// List of open nodes
		/// </summary>
		protected List<PathNode> openList;
		/// <summary>
		/// List of closed nodes
		/// </summary>
		protected List<PathNode> closedList;
		/// <summary>
		/// Path to the target from the starting position
		/// </summary>
		protected List<Point> path;
		/// <summary>
		/// Ending position in the board[,]
		/// </summary>
		protected Point end;
		/// <summary>
		/// Starting position in the board[,]
		/// </summary>
		protected Point start;
		/// <summary>
		/// Random number generator used to break ties in the path
		/// </summary>
		protected Random tieBreakerRandom;
		#endregion Class variables

		#region Class properties
		/// <summary>
		/// Gets the path leading from the starting position to the ending position if there is one
		/// </summary>
		public List<Point> Path { get { return this.path; } }
		#endregion Class properties

		#region Constructor
		private PathFinder(RestrictionType restrictionType) {
			this.tieBreakerRandom = new Random();
			if (restrictionType == RestrictionType.Restricted) {
				this.directions = new int[4, 2] {
					{0,1},		// right
					{0,-1},		// left
					{1,0},		// bottom
					{-1, 0}		// top
				};
			} else {
				this.directions = new int[8, 2] {
					{0,1},		// right
					{0,-1},		// left
					{1,0},		// bottom
					{-1, 0},	// top
					{-1,-1},	// top left corner
					{-1,1},		// top right corner
					{1,-1},		// bottom left corner
					{1,1}		// bottom right corner

				};
			}
		}

		/// <summary>
		/// Builds a base A* PathFinder instance for on the fly processing
		/// </summary>
		/// <param name="height">Height of the board</param>
		/// <param name="width">Width of the board</param>
		/// <param name="restrictionType">Movement restriction type</param>
		public PathFinder(int height, int width, RestrictionType restrictionType)
			: this(restrictionType) {
			this.HEIGHT = height;
			this.WIDTH = width;
		}

		/// <summary>
		/// Builds a base A* PathFinder instance for static processing
		/// </summary>
		/// <param name="board">A* representation of the board</param>
		/// <param name="restrictionType">Movement restriction type</param>
		public PathFinder(TypeOfSpace[,] board, RestrictionType restrictionType)
			: this(restrictionType) {
			this.board = board;
			this.HEIGHT = board.GetUpperBound(0);
			this.WIDTH = board.GetUpperBound(1);
		}
		#endregion Constructor

		#region Support functions
		/// <summary>
		/// Finds a path based within the board if one is available
		/// </summary>
		/// <param name="parent">Parent node of the new nodes to be processed</param>
		/// <param name="parentsIndex">Index of the parent in the open list</param>
		protected abstract void findPath(PathNode parent, int parentsIndex);

		/// <summary>
		/// Retrieves the start distance and the end distance from the current position
		/// </summary>
		/// <param name="currentPosition">Current position we are at in the search</param>
		/// <param name="startDistance">Output the distance from the start</param>
		/// <param name="endDistance">Output the distance from the end</param>
		protected virtual void getDistance(Point currentPosition, out int startDistance, out int endDistance) {
			startDistance = getDistance(currentPosition, this.start);
			endDistance = getDistance(currentPosition, this.end);
		}

		/// <summary>
		/// Retrieves the distance from the current position and the specified position
		/// </summary>
		/// <param name="currentPosition">Current position we are at</param>
		/// <param name="comparePosition">Posiiton we are looking for the distance from</param>
		/// <returns></returns>
		protected virtual int getDistance(Point currentPosition, Point comparePosition) {
			return (10 * (Math.Abs(currentPosition.X - comparePosition.X) + Math.Abs(currentPosition.Y - comparePosition.Y)));
		}

		/// <summary>
		/// Figures out the starting and ending positions in the board
		/// </summary>
		protected virtual void getStartAndEnd() {
			for (int y = 0; y < HEIGHT; y++) {
				for (int x = 0; x < WIDTH; x++) {
					if (board[y, x] == TypeOfSpace.End) {
						this.end = new Point(x, y);
					} else if (board[y, x] == TypeOfSpace.Start) {
						this.start = new Point(x, y);
					}
				}
			}
		}

		/// <summary>
		/// Determines a path when there are 2 nodes with the same FScore
		/// </summary>
		/// <param name="surrondingCosts">Surronding nodes that we are looking at</param>
		/// <returns>Index of the node to use to carry on the search</returns>
		protected int checkForTieBreakerAndResolvePath(ref List<PathNode> surrondingCosts) {
			//TODO: Currently this is limited to only taking one of 2 paths, when really you could have 7 alternate paths
			int result = 0;
			if (surrondingCosts.Count > 1 && surrondingCosts[0].fScoresEqual(surrondingCosts[1])) {
				result = this.tieBreakerRandom.Next(2);
			}
			return result;
		}

		/// <summary>
		/// Finds a path from the starting position to the ending position if one exists
		/// </summary>
		public virtual void findPath() {
			this.openList = new List<PathNode>();
			this.closedList = new List<PathNode>();
			Point absoluteParent = new Point(-1, -1);
			int startDistance, endDistance;
			getDistance(this.start, out startDistance, out endDistance);
			PathNode parent = new PathNode(null, absoluteParent, startDistance, endDistance);
			PathNode startingPiece = new PathNode(parent, this.start, startDistance, endDistance);
			this.openList.Add(startingPiece);
			findPath(startingPiece, 0);
			this.path = new List<Point>();
			PathNode piece = this.closedList[this.closedList.Count - 1];
			// only create a path if there is one
			if (piece.Position.Equals(this.end)) {
				this.path.Add(piece.Position);
				for (; ; ) {
					piece = piece.Parent;
					if (!piece.Position.Equals(this.start)) {
						this.path.Add(piece.Position);
					} else {
						// we are at the start so break
						break;
					}
				}
			}
		}

		/// <summary>
		/// Finds a path from the starting position to the ending position if one exists
		/// </summary>
		/// <param name="board">Board to search through</param>
		public virtual void findPath(PathFinder.TypeOfSpace[,] board) {
			this.board = board;
			getStartAndEnd();
			if (this.end.X != -1 && this.end.Y != -1 && this.start.X != -1 && this.start.Y != -1) {
				findPath();
			} else {
				throw new ArgumentException("Failed to find a start and ending position");
			}
		}
		#endregion Support functions
	}
}
