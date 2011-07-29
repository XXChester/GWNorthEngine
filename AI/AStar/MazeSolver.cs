using System;
using Microsoft.Xna.Framework;
namespace GWNorthEngine.AI.AStar {
	/// <summary>
	/// Child path finding class that moves in a zig zag pattern and cannot cut corners
	/// </summary>
	public class MazeSolver : PathFinder {
		#region Constructor
		/// <summary>
		/// Builds a MazeSolver instance based on the height and width passed in. Used for on the fly path calculation
		/// </summary>
		/// <param name="height">Height of the board</param>
		/// <param name="width">Width of the board</param>
		public MazeSolver(int height, int width)
			: base(height, width, RestrictionType.Restricted) {
			// used for on the fly processing
		}

		/// <summary>
		/// Builds a MazeSolver instance based on a board. Used for one off path calculation
		/// </summary>
		/// <param name="board">A* representation of the board</param>
		public MazeSolver(TypeOfSpace[,] board)
			: base(board, RestrictionType.Restricted) {
			// used for static processing
			base.getStartAndEnd();
			if (base.end.X != -1 && base.end.Y != -1 && base.start.X != -1 && base.start.Y != -1) {

			} else {
				throw new ArgumentException("Failed to find a start and ending position");
			}
		}
		#endregion Constructor

		#region Support functions
		/// <summary>
		/// Finds a path based within the board if one is available
		/// </summary>
		/// <param name="parent">Parent node of the new nodes to be processed</param>
		/// <param name="parentsIndex">Index of the parent in the open list</param>
		protected override void findPath(PathNode parent, int parentsIndex) {
			// find our surronding points
			int x, y;
			int startDistance, endDistance;
			PathNode existingNode = null;
			PathNode newNode = null;
			bool foundPieceInList;
			Point newPosition;
			for (int i = 0; i < 4; i++) {
				y = parent.Position.Y + base.directions[i, 0];
				x = parent.Position.X + base.directions[i, 1];
				if (x > -1 && y > -1 && x < base.WIDTH && y < base.HEIGHT && base.board[y, x] != PathFinder.TypeOfSpace.Unwalkable) {
					newPosition = new Point(x, y);
					base.getDistance(newPosition, parent, out startDistance, out endDistance);
					newNode = new PathNode(parent, newPosition, startDistance, endDistance);
					foundPieceInList = false;
					// check if the position is already on the open list
					for (int j = 0; j < base.openList.Count; j++) {
						existingNode = base.openList[j];
						if (existingNode != null && existingNode.Position == newPosition) {
							if (newNode.G < existingNode.G) {
								existingNode.Parent = parent;
								existingNode.G = newNode.G;
							}
							foundPieceInList = true;
							break;
						}
					}
					if (!foundPieceInList) {
						// We didn't find this piece in the open list so add it to the open list
						base.openList.Add(newNode);
					}
				}
			}
			base.openList.RemoveAt(parentsIndex);
			base.closedList.Add(parent);

			// we need to remove items that are on the closed list
			if (base.openList.Count >= 1) {
				int removal;
				PathNode pathPiece = null;
				for (int c = 0; c < base.closedList.Count; c++) {
					removal = -1;
					for (int s = 0; s < base.openList.Count; s++) {
						pathPiece = base.openList[s];
						if (pathPiece != null) {
							if (pathPiece.Position == base.closedList[c].Position) {
								removal = s;
								break;
							}
						}
					}
					if (removal != -1) {
						base.openList.RemoveAt(removal);
					}
				}
			}

			// get the lowest cost next point
			if (base.openList.Count > 0) {
				base.openList.Sort(FScoreComparator.getInstance());
				int index = base.checkForTieBreakerAndResolvePath(ref base.openList);
				PathNode lowestScorePiece = base.openList[index];
				if (base.end.Equals(lowestScorePiece.Position)) {
					this.closedList.Add(lowestScorePiece);
					return;
				} else {
					findPath(lowestScorePiece, index);
				}

			}
		}
		#endregion Support functions
	}
}
