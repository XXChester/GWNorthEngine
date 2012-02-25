using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

using GWNorthEngine.Logic;
using GWNorthEngine.Model.Params;
namespace GWNorthEngine.Model {
	/// <summary>
	/// Basic 3D model class
	/// </summary>
	public class BaseModel {
		#region Class variables
		/// <summary>
		/// Internal XNA Model object
		/// </summary>
		protected Microsoft.Xna.Framework.Graphics.Model model;
		#endregion Class variables

		#region Class propeties
		/// <summary>
		/// Gets or sets the position of the model
		/// </summary>
		public Vector3 Position { get; set; }
		/// <summary>
		/// Gets or sets the rotation of the model
		/// </summary>
		public Vector3 Rotation { get; set; }
		/// <summary>
		/// Gets or sets the scale of the model
		/// </summary>
		public Vector3 Scale { get; set; }
		/// <summary>
		/// Gets or sets whether the model is to be drawn
		/// </summary>
		public bool Visible { get; set; }
		#endregion Class properties

		#region Constructor
		/// <summary>
		/// Builds a Model based on the parms specified
		/// </summary>
		/// <param name="parms"></param>
		public BaseModel(BaseModelParams parms) {
			this.model = parms.Model;
			this.Position = parms.Position;
			this.Rotation = parms.Rotation;
			this.Scale = parms.Scale;
			this.Visible = parms.Visible;
		}
		#endregion Constructor

		#region Support methods
		/// <summary>
		/// Renders the model
		/// <param name="camera">Camera the model uses to render with</param>
		/// </summary>
		public virtual void render(Camera3D camera) {
			if (this.Visible) {
				Matrix[] transforms = new Matrix[this.model.Bones.Count];
				this.model.CopyAbsoluteBoneTransformsTo(transforms);
				foreach (ModelMesh mesh in this.model.Meshes) {
					foreach (BasicEffect effect in mesh.Effects) {
						effect.EnableDefaultLighting();
						effect.World = transforms[mesh.ParentBone.Index] * Matrix.CreateScale(this.Scale) *
							Matrix.CreateRotationX(this.Rotation.X) * Matrix.CreateRotationY(this.Rotation.Y) *
							Matrix.CreateRotationZ(this.Rotation.Z) * Matrix.CreateTranslation(this.Position);
						effect.View = camera.ViewMatrix;
						effect.Projection = camera.ProjectionMatrix;
					}
					mesh.Draw();
				}
			}
		}
		#endregion Support methods
	}
}
