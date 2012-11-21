using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace GWNorthEngine.Utils {
	/// <summary>
	/// Class containg usefull methods for picking different types of objects
	/// </summary>
	public class PickingUtils {
		/// <summary>
		/// Picks a rectangle based on its coordinates vs the actors position
		/// </summary>
		/// <param name="actorsPosition">Actors position. Could be mouse position, XBox controllers stick position etc</param>
		/// <param name="buttonsRectangle">rectangle to check for collision</param>
		/// <returns></returns>
		public static bool pickRectangle(Vector2 actorsPosition, Rectangle buttonsRectangle) {
			bool intersection = false;
			if (buttonsRectangle.Contains((int)actorsPosition.X, (int)actorsPosition.Y)) {
				intersection = true;
			}
			return intersection;
		}

		/// <summary>
		/// Determines if a Vector2 is within a BoundingBox
		/// </summary>
		/// <param name="actorsPosition">Vector2 that we want to check</param>
		/// <param name="bbox">BoundingBox that we are interogating</param>
		/// <returns>true if the Vector2 is within the BoundingBox, otherwise false</returns>
		public static bool pickVector(Vector2 actorsPosition, BoundingBox bbox) {
			BoundingBox bbox2 = new BoundingBox(new Vector3(actorsPosition, 0f), new Vector3(actorsPosition.X + 1, actorsPosition.Y + 1, 0f));
			return bbox.Intersects(bbox2);
		}

		/*
		The below picking routine has not been tested. It was converted to XNA from Brandon's engine but nothing has been built to use it yet

		public static bool pickTriangles(Vector2 actorsPosition, Vector3 topLeftCorner, Vector3 bottomRightCorner, Viewport viewPort,
			Matrix projectionMatrix, Matrix viewMatrix) {
			
			bool intersection = false;
			// create 2 positions in screenspace using the actor's position. 0 is as
			// close as possible to the camera, 1 is as far away as possible.
			Vector3 near = new Vector3(actorsPosition.X, actorsPosition.Y, 0);
			Vector3 far = new Vector3(actorsPosition.X, actorsPosition.Y, 1);

			// use Viewport.Unproject to tell what those two screen space positions
			// would be in world space. we'll need the projection matrix, view
			// matrix, and world matrix, which can just be identity.
			Vector3 nearPoint = viewPort.Unproject(near, projectionMatrix, viewMatrix, Matrix.Identity);
			Vector3 farPoint = viewPort.Unproject(far, projectionMatrix, viewMatrix, Matrix.Identity);


			// find the direction vector that goes from the nearPoint to the farPoint
			// and normalize it....
			Vector3 direction = farPoint - nearPoint;
			direction.Normalize();

			if (new Ray(nearPoint, farPoint).Intersects(new BoundingBox(bottomRightCorner, topLeftCorner)) != null) {
				intersection = true;
			}

			return intersection;
		}
		 */
	}
}
