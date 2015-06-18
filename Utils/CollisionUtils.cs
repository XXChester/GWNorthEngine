using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace GWNorthEngine.Utils {
	/// <summary>
	/// Helper methods for determining collision between objects
	/// </summary>
	public static class CollisionUtils {
		/// <summary>
		/// Determines if two textures pixels intersect
		/// </summary>
		/// <param name="texture1Data">Texture1's colour data</param>
		/// <param name="texture1Matrix">The matrix used to render Texture1</param>
		/// <param name="texture2Data">Texture2's colour data</param>
		/// <param name="texture2Matrix">The matrix used to render Texture2</param>
		/// <returns>True if a collision was detected, otherwise false</returns>
		public static bool doPixelsIntersect(Color[,] texture1Data, Matrix texture1Matrix, Color[,] texture2Data,
			Matrix texture2Matrix) {
			Vector2 collisionPoint;
			return doPixelsIntersect(texture1Data, texture1Matrix, texture2Data, texture2Matrix, out collisionPoint);
		}

		/*public static bool doPixelsIntersect(Color[,] texture1Data, Matrix texture1Matrix, Color[,] texture2Data,
			Matrix texture2Matrix, out Vector2 collisionPoint) {
			bool collision = false;
			collisionPoint = new Vector2(-1f);
			Matrix mat1to2 = texture1Matrix * Matrix.Invert(texture2Matrix);
			int width1 = texture1Data.GetLength(1);
			int height1 = texture1Data.GetLength(0);
			int width2 = texture2Data.GetLength(1);
			int height2 = texture2Data.GetLength(0);

			Vector2 pos1;
			Vector2 pos2;
			int x2;
			int y2;
			for (int y1 = 0; y1 < height1; y1++) {
				for (int x1 = 0; x1 < width1; x1++) {
					pos1 = new Vector2(x1, y1);
					pos2 = Vector2.Transform(pos1, mat1to2);

					x2 = (int)pos2.X;
					y2 = (int)pos2.Y;
					if ((y2 >= 0) && (y2 < height2)) {
						if ((x2 >= 0) && (x2 < width2)) {
							if (texture1Data[y1, x1].A > 0 && texture2Data[y2, x2].A > 0) {
								collisionPoint = Vector2.Transform(pos1, texture1Matrix);
								collision = true;
								break;
							}
						}
					}
				}

				if (collision) {
					break;
				}
			}

			return collision;
		}*/

		/// <summary>
		/// Determines if two textures pixels intersect and at which point
		/// </summary>
		/// <remarks>The collision is based on data in x,y format not y,x format at this time</remarks>
		/// <param name="texture1Data">Texture1's colour data</param>
		/// <param name="texture1Matrix">The matrix used to render Texture1</param>
		/// <param name="texture2Data">Texture2's colour data</param>
		/// <param name="texture2Matrix">The matrix used to render Texture2</param>
		/// <param name="collisionPoint">Out the Vector2 in which a collision occurred if at all</param>
		/// <returns>True if a collision was detected, otherwise false</returns>
		public static bool doPixelsIntersect(Color[,] texture1Data, Matrix texture1Matrix, Color[,] texture2Data,
			Matrix texture2Matrix, out Vector2 collisionPoint) {
			//TODO: Change the order to be Y,X instead of X,Y to fit the rest of my programs
			bool collision = false;
			collisionPoint = new Vector2(-1f);
			Matrix mat1to2 = texture1Matrix * Matrix.Invert(texture2Matrix);
			int width1 = texture1Data.GetLength(0);
			int height1 = texture1Data.GetLength(1);
			int width2 = texture2Data.GetLength(0);
			int height2 = texture2Data.GetLength(1);

			Vector2 pos1;
			Vector2 pos2;
			int x2;
			int y2;
			for (int x1 = 0; x1 < width1; x1++) {
				for (int y1 = 0; y1 < height1; y1++) {
					pos1 = new Vector2(x1, y1);
					pos2 = Vector2.Transform(pos1, mat1to2);

					x2 = (int)pos2.X;
					y2 = (int)pos2.Y;
					if ((x2 >= 0) && (x2 < width2)) {
						if ((y2 >= 0) && (y2 < height2)) {
							if (texture1Data[x1, y1].A > 0 && texture2Data[x2, y2].A > 0) {
								collisionPoint = Vector2.Transform(pos1, texture1Matrix);
								collision = true;
								break;
							}
						}
					}
				}

				if (collision) {
					break;
				}
			}

			return collision;
		}

		/// <summary>
		/// Determinse if a bounding box collides with any number of bounding boxes in a list
		/// </summary>
		/// <param name="bboxes">List of bounding boxes to check a collision for</param>
		/// <param name="checkWith">Single bounding box to check against</param>
		/// <returns>True if a collision was found, otherwise false</returns>
		public static bool doesBBoxCollideWithAnotherBBox(List<BoundingBox> bboxes, BoundingBox checkWith) {
			bool collision = false;
			foreach (BoundingBox bbox in bboxes) {
				if (checkWith.Intersects(bbox)) {
					collision = true;
					break;
				}
			}
			return collision;
		}

		/// <summary>
		/// Determines if a bounding box is intersected while casting a ray from a position in a direction
		/// </summary>
		/// <param name="bbox">Bounding box of the entity we are seeing if we hit</param>
		/// <param name="position">Position we are casting from</param>
		/// <param name="direction">Direction we are casting the Ray in</param>
		/// <returns></returns>
		public static Nullable<float> castRay(BoundingBox bbox, Vector2 position, Vector2 direction) {
			Ray ray = new Ray(new Vector3(position, 0f), new Vector3(direction, 0f));
			return ray.Intersects(bbox);
		}
	}
}
