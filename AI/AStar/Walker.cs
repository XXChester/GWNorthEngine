using System;
using Microsoft.Xna.Framework;
namespace GWNorthEngine.AI.AStar {
	/// <summary>
	/// Child path finding class that moves in a any direction and can optionally cut corners
	/// </summary>
	public class Walker : PathFinder {
		#region Class variables
		private bool allowedToCutCorners;
		#endregion Class variables

		#region Constructor
		/// <summary>
		/// Builds a Walker instance based on the height, width and ability to cut corners. Used for on the fly path calculation
		/// </summary>
		/// <param name="height">Height of the board</param>
		/// <param name="width">Width of the board</param>
		/// <param name="allowedToCutCorners">Ability to cut across corners</param>
		public Walker(int height, int width, bool allowedToCutCorners)
			:base(height, width, RestrictionType.All) {
			// used for on the fly processing
			this.allowedToCutCorners = allowedToCutCorners;
		}

		/// <summary>
		/// Builds a Walker instance based on the board and ability to cut corners. Used for one off path calculation
		/// </summary>
		/// <param name="board">A* representation of the board</param>
		/// <param name="allowedToCutCorners">Ability to cut across corners</param>
		public Walker(TypeOfSpace[,] board, bool allowedToCutCorners)
			: base(board, RestrictionType.All) {
			// used for static processing
			this.allowedToCutCorners = allowedToCutCorners;
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
			int x, y, cornerCheckX, cornerCheckY;
			int newG;
			int startDistance, endDistance;
			PathNode existingNode = null;
			PathNode newNode = null;
			bool foundPieceInList;
			Point newPosition;
			for (int i = 0; i < 8; i++) {
				y = parent.Position.Y + base.directions[i, 0];
				x = parent.Position.X + base.directions[i, 1];
				if (x > -1 && y > -1 && x < base.WIDTH && y < base.HEIGHT && base.board[y, x] != PathFinder.TypeOfSpace.Unwalkable) {
					newPosition = new Point(x, y);
					if (i < 4) {
						getDistance(newPosition, out startDistance, out endDistance);
						newNode = new PathNode(parent, newPosition, startDistance, endDistance);
					} else {
						getDistance(newPosition, out startDistance, out endDistance);
						newNode = new PathNode(parent, newPosition, startDistance, endDistance);
						// if we are not allowing cutting we need to check if this diagonal would cut a corner, if it does do not process it
						if (!this.allowedToCutCorners) {
							// to get the corners we simply get the new x/y positions direction and multiply it by -1 and add it to x/y
							cornerCheckY = (base.directions[i, 0] * -1) + y;
							cornerCheckX = (base.directions[i, 1] * -1) + x;
							if (cornerCheckY > -1 &&  cornerCheckX > -1 && cornerCheckY < HEIGHT && cornerCheckX < WIDTH) {
								if (base.board[cornerCheckY, x] == TypeOfSpace.Unwalkable || base.board[y, cornerCheckX] == TypeOfSpace.Unwalkable) {
									continue;
								}
							}
						}
					}
					foundPieceInList = false;
					// check if the position is already on the open list
					for (int j = 0; j < base.openList.Count; j++) {
						existingNode = base.openList[j];
						if (existingNode != null && existingNode.Position == newPosition && existingNode.Parent != null) {
							if ((newPosition.X - parent.Position.X) == 1 && (newPosition.Y - parent.Position.Y) == 1) {
								newG = newNode.G + 14;
							} else {
								newG = newNode.G + 10;
							}
							if (newG < existingNode.G) {
								existingNode.Parent.Position = newPosition;
								existingNode.G = newG;
								existingNode.H = endDistance;
								existingNode.FScore = startDistance + endDistance;
							}
							foundPieceInList = true;
							break;
						}
					}
					if (!foundPieceInList) {
						// Now that we found this piece, find and set his fScore
						base.openList.Add(newNode);
					}
				}
			}
			base.openList.RemoveAt(parentsIndex);
			base.closedList.Add(parent);

			// we need to remove items that are on the closed list
			if (base.openList.Count > 0) {
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
				int index = checkForTieBreakerAndResolvePath(ref base.openList);
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
