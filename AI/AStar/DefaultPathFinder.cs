using System;
using Microsoft.Xna.Framework;
using GWNorthEngine.AI.AStar.Params;
namespace GWNorthEngine.AI.AStar {
	/// <summary>
	/// Default path finding class
	/// </summary>
	public class DefaultPathFinder : BasePathFinder {
		#region Constructor
		/// <summary>
		/// Builds a DefaultPathFinder
		/// </summary>
		/// <param name="parms">DefaultPathFinderParams object containing the data required to build the PathFinder</param>
		public DefaultPathFinder(DefaultPathfinderParams parms) 
			:base(parms) {
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
			int startDistance, endDistance;
			PathNode existingNode = null;
			PathNode newNode = null;
			bool foundPieceInList;
			Point newPosition;
			PathNode lowestCode = null;
			for (int i = 0; i < base.DIRECTIONS_LENGTH; i++) {
				y = parent.Position.Y + base.directions[i, 0];
				x = parent.Position.X + base.directions[i, 1];
				if (x > -1 && y > -1 && x < base.WIDTH && y < base.HEIGHT && base.board[y, x] != BasePathFinder.TypeOfSpace.Unwalkable) {
					newPosition = new Point(x, y);
					getDistance(newPosition, parent, out startDistance, out endDistance);
					newNode = new PathNode(parent, newPosition, startDistance, endDistance);
					if (lowestCode == null || lowestCode.FScore > newNode.FScore) {
						lowestCode = new PathNode(parent, newPosition, startDistance, endDistance);
					}
					if (i >= 4) {
						// if we are not allowing cutting we need to check if this diagonal would cut a corner, if it does do not process it
						if (!base.allowedToCutCorners) {
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
			if (this.LowestCost == null || this.LowestCost.FScore > lowestCode.FScore) {
				this.LowestCost = lowestCode;
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
