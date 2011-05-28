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
		//This works I believe but needs further testing and as I am not currently using it, it is commented out
		/*public static bool intersectPixels(Rectangle rectangleA, Color[] dataA, Rectangle rectangleB, Color[] dataB) {
			bool collision = false;
			int top = Math.Max(rectangleA.Top, rectangleB.Top);
			int bottom = Math.Min(rectangleA.Bottom, rectangleB.Bottom);
			int left = Math.Max(rectangleA.Left, rectangleB.Left);
			int right = Math.Min(rectangleA.Right, rectangleB.Right);

			for (int y = top; y < bottom; y++) {
				for (int x = left; x < right; x++) {
					Color colorA = dataA[(x - rectangleA.Left) +
								(y - rectangleA.Top) * rectangleA.Width];
					Color colorB = dataB[(x - rectangleB.Left) +
								(y - rectangleB.Top) * rectangleB.Width];

					if (colorA.A != 0 && colorB.A != 0) {
						collision = true;
						break;
					}
				}
			}
			return collision;
		}*/
	}
}
