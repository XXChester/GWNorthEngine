using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using GWNorthEngine.Model;
using GWNorthEngine.Model.Params;
namespace GWNorthEngine.Utils {
	/// <summary>
	/// Helpful methods for debugging
	/// </summary>
	public class DebugUtils {
		#region Class variables
		private Texture2D lineTexture;
		#endregion Class variables

		#region Constructor
		/// <summary>
		/// Builds a debug utils object
		/// </summary>
		public DebugUtils()
			:this(null){
		}

		/// <summary>
		/// Builds a debug utils object for outlining objects
		/// </summary>
		/// <param name="lineTexture">Line texture used to draw lines around objects such as bounding boxes</param>
		public DebugUtils(Texture2D lineTexture) {
			this.lineTexture = lineTexture;
		}
		#endregion Constructor

		#region Support methods
		/// <summary>
		/// Draws a bounding box using Line2D's
		/// </summary>
		/// <param name="spriteBatch">SpriteBatch object used to draw the bounding box</param>
		/// <param name="boundingBox">BoundingBox object to draw</param>
		/// <param name="debugColour">Colour to draw the box in</param>
		public void drawBoundingBox(SpriteBatch spriteBatch, BoundingBox boundingBox, Color debugColour) {
			drawBoundingBox(spriteBatch, boundingBox, debugColour, this.lineTexture);
		}

		/// <summary>
		/// Draws a bounding box using Line2D's
		/// </summary>
		/// <param name="spriteBatch">SpriteBatch object used to draw the bounding box</param>
		/// <param name="boundingBox">BoundingBox object to draw</param>
		/// <param name="debugColour">Colour to draw the box in</param>
		/// <param name="positionOffset">Offset to render the object at</param>
		public void drawBoundingBox(SpriteBatch spriteBatch, BoundingBox boundingBox, Color debugColour, Vector2 positionOffset) {
			drawBoundingBox(spriteBatch, boundingBox, debugColour, this.lineTexture, positionOffset);
		}

		/// <summary>
		/// Draws a bounding box using Line2D's
		/// </summary>
		/// <param name="spriteBatch">SpriteBatch object used to draw the bounding box</param>
		/// <param name="boundingBox">BoundingBox object to draw</param>
		/// <param name="debugColour">Colour to draw the box in</param>
		/// <param name="lineTexture">Texture2D used for drawing the bounding box</param>
		public static void drawBoundingBox(SpriteBatch spriteBatch, BoundingBox boundingBox, Color debugColour, Texture2D lineTexture) {
			drawBoundingBox(spriteBatch, boundingBox, debugColour, lineTexture, Vector2.Zero);
		}

		/// <summary>
		/// Draws a bounding box using Line2D's
		/// </summary>
		/// <param name="spriteBatch">SpriteBatch object used to draw the bounding box</param>
		/// <param name="boundingBox">BoundingBox object to draw</param>
		/// <param name="debugColour">Colour to draw the box in</param>
		/// <param name="lineTexture">Texture2D used for drawing the bounding box</param>
		/// <param name="positionOffset">Offset to render the object at</param>
		public static void drawBoundingBox(SpriteBatch spriteBatch, BoundingBox boundingBox, Color debugColour, Texture2D lineTexture,
			Vector2 positionOffset) {
			Vector3 min = boundingBox.Min;
			Vector3 max = boundingBox.Max;
			drawVector3s(spriteBatch, max, min, debugColour, lineTexture, positionOffset);
		}

		/// <summary>
		/// Draws a bounding sphere
		/// </summary>
		/// <param name="spriteBatch">SpriteBatch object used to draw the sphere</param>
		/// <param name="boundingSphere">BoundingSPhere object to draw</param>
		/// <param name="debugColour">Colour to draw the sphere in</param>
		public void drawBoundingSphere(SpriteBatch spriteBatch, BoundingSphere boundingSphere, Color debugColour) {
			drawBoundingSphere(spriteBatch, boundingSphere, debugColour, this.lineTexture);
		}

		/// <summary>
		/// Draws a bounding sphere
		/// </summary>
		/// <param name="spriteBatch">SpriteBatch object used to draw the sphere</param>
		/// <param name="boundingSphere">BoundingSPhere object to draw</param>
		/// <param name="debugColour">Colour to draw the sphere in</param>
		/// <param name="lineTexture">Texture2D used for drawing the bounding sphere</param>
		public static void drawBoundingSphere(SpriteBatch spriteBatch, BoundingSphere boundingSphere, Color debugColour, 
			Texture2D lineTexture) {
			Vector2 position = new Vector2(boundingSphere.Center.X, boundingSphere.Center.Y);
			spriteBatch.Draw(lineTexture, position, null, debugColour, 0f, new Vector2(boundingSphere.Radius, boundingSphere.Radius), 
				1f, SpriteEffects.None, 1f);
		}

		/// <summary>
		/// Draws a rectangle using Line2D's
		/// </summary>
		/// <param name="spriteBatch">SpriteBatch object used to draw the bounding box</param>
		/// <param name="rectangle">Rectable object to draw</param>
		/// <param name="debugColour">Colour to draw the rectangle in</param>
		/// <param name="lineTexture">Texture2D used for drawing the rectangle</param>
		public static void drawRectangle(SpriteBatch spriteBatch, Rectangle rectangle, Color debugColour, Texture2D lineTexture) {
			Vector3 min = new Vector3(rectangle.X, rectangle.Y, 0f);
			Vector3 max = new Vector3(rectangle.X + rectangle.Width, rectangle.Y + rectangle.Height, 0f);
			drawVector3s(spriteBatch, max, min, debugColour, lineTexture);
		}

		/// <summary>
		/// Draws a set of vector 3s
		/// </summary>
		/// <param name="spriteBatch">SpriteBatch object used to draw the vectors</param>
		/// <param name="max">Max position to draw</param>
		/// <param name="min">Min position to draw</param>
		/// <param name="debugColour">Colour to draw the Vectors in</param>
		/// <param name="lineTexture">Texture2D used for drawing the vectors</param>
		public static void drawVector3s(SpriteBatch spriteBatch, Vector3 max, Vector3 min, Color debugColour, Texture2D lineTexture) {
			drawVector3s(spriteBatch, max, min, debugColour, lineTexture, Vector2.Zero);
		}

		/// <summary>
		/// Draws a set of vector 3s
		/// </summary>
		/// <param name="spriteBatch">SpriteBatch object used to draw the vectors</param>
		/// <param name="max">Max position to draw</param>
		/// <param name="min">Min position to draw</param>
		/// <param name="debugColour">Colour to draw the Vectors in</param>
		/// <param name="lineTexture">Texture2D used for drawing the vectors</param>
		/// <param name="positionOffset">Offset to render the object at</param>
		public static void drawVector3s(SpriteBatch spriteBatch, Vector3 max, Vector3 min, Color debugColour, Texture2D lineTexture,
			Vector2 positionOffset) {
			Line2DParams parms = new Line2DParams();
			parms.Texture = lineTexture;
			parms.LightColour = debugColour;
			//left
			parms.Position = new Vector2(min.X, min.Y);
			parms.EndPosition = new Vector2(min.X, max.Y);
			Line2D left = new Line2D(parms);
			//top
			parms.Position = new Vector2(min.X, min.Y);
			parms.EndPosition = new Vector2(max.X, min.Y);
			Line2D top = new Line2D(parms);
			//right
			parms.Position = new Vector2(max.X, min.Y);
			parms.EndPosition = new Vector2(max.X, max.Y);
			Line2D right = new Line2D(parms);
			//bottom
			parms.Position = new Vector2(min.X, max.Y);
			parms.EndPosition = new Vector2(max.X, max.Y);
			Line2D bottom = new Line2D(parms);

			left.render(spriteBatch, positionOffset);
			top.render(spriteBatch, positionOffset);
			right.render(spriteBatch, positionOffset);
			bottom.render(spriteBatch, positionOffset);
		}

		/// <summary>
		/// Draws a radius using a SpriteDrawable2D
		/// </summary>
		/// <param name="spriteBatch">SpriteBatch object used to draw</param>
		/// <param name="position">Position to draw the radius around</param>
		/// <param name="debugColour">Colour to draw the rectangle in</param>
		/// <param name="radiusSpecificTexture">Texture2D used for drawing the radius</param>
		public static void drawRadius(SpriteBatch spriteBatch, Vector2 position, Texture2D radiusSpecificTexture, Color debugColour) {
			StaticDrawable2DParams parms = new StaticDrawable2DParams();
			parms.Position = position;
			parms.LightColour = debugColour;
			parms.Texture = radiusSpecificTexture;
			parms.Origin = new Vector2(radiusSpecificTexture.Height / 2);
			new StaticDrawable2D(parms).render(spriteBatch);
		}
		#endregion Support methods
	}

}
