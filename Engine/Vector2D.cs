using System;
using System.ComponentModel;
using Microsoft.Xna.Framework.Design;
using System.Text;
using System.Runtime.InteropServices;

using Microsoft.Xna.Framework;

namespace GWNorthEngine.Engine {
	/// <summary>
	/// Models a reference type Vector2
	/// </summary>
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	[TypeConverter(typeof(Vector2Converter))]
	public class Vector2D : IEquatable<Vector2D> {
		#region Private Fields
		private static Vector2D zeroVector = new Vector2D(0f, 0f);
		private static Vector2D unitVector = new Vector2D(1f, 1f);
		private static Vector2D unitXVector = new Vector2D(1f, 0f);
		private static Vector2D unitYVector = new Vector2D(0f, 1f);
		#endregion Private Fields

		#region Public Fields
		/// <summary>
		/// X value
		/// </summary>
		public float X;
		/// <summary>
		/// Y value
		/// </summary>
		public float Y;
		#endregion Public Fields

		#region Properties
		/// <summary>
		/// Zero Vector2D
		/// </summary>
		public static Vector2D Zero { get { return zeroVector; } }
		/// <summary>
		/// Vector2D of 1f,1f
		/// </summary>
		public static Vector2D One { get { return unitVector; } }
		/// <summary>
		/// Vector2D of 1f, 0f
		/// </summary>
		public static Vector2D UnitX { get { return unitXVector; } }
		/// <summary>
		/// Vector2D of 0f, 1f
		/// </summary>
		public static Vector2D UnitY { get { return unitYVector; } }
		#endregion Properties

		#region Constructors
		/// <summary>
		/// Constructor for "Zero" vector.
		/// </summary>
		/// A <see cref="System.Single"/>
		/// </param>
		public Vector2D() : this(0f, 0f) { }

		/// <summary>
		/// Constructor for "square" vector.
		/// </summary>
		/// <param name="value">
		/// A <see cref="System.Single"/>
		/// </param>
		public Vector2D(float value) : this(value, value) { }

		/// <summary>
		/// Constructs a Vector2D from an XNA Vector
		/// </summary>
		/// <param name="vector">XNA Vector to create from</param>
		public Vector2D(Vector2 vector) : this(vector.X, vector.Y) { }

		/// <summary>
		/// Constructor foe standard 2D vector.
		/// </summary>
		/// <param name="x">
		/// A <see cref="System.Single"/>
		/// </param>
		/// <param name="y">
		/// A <see cref="System.Single"/>
		/// </param>
		public Vector2D(float x, float y) {
			this.X = x;
			this.Y = y;
		}
		#endregion Constructors

		#region Public Methods
		public static void Reflect(ref Vector2D vector, ref Vector2D normal, out Vector2D result) {
			float dot = Dot(vector, normal);
			result = new Vector2D();
			result.X = vector.X - ((2f * dot) * normal.X);
			result.Y = vector.Y - ((2f * dot) * normal.Y);
		}

		public static Vector2D Reflect(Vector2D vector, Vector2D normal) {
			Vector2D result;
			Reflect(ref vector, ref normal, out result);
			return result;
		}

		public static Vector2D Add(Vector2D value1, Vector2D value2) {
			value1.X += value2.X;
			value1.Y += value2.Y;
			return value1;
		}

		public static void Add(ref Vector2D value1, ref Vector2D value2, out Vector2D result) {
			result = new Vector2D();
			result.X = value1.X + value2.X;
			result.Y = value1.Y + value2.Y;
		}

		public static Vector2D Barycentric(Vector2D value1, Vector2D value2, Vector2D value3, float amount1, float amount2) {
			return new Vector2D(
				MathHelper.Barycentric(value1.X, value2.X, value3.X, amount1, amount2),
				MathHelper.Barycentric(value1.Y, value2.Y, value3.Y, amount1, amount2));
		}

		public static void Barycentric(ref Vector2D value1, ref Vector2D value2, ref Vector2D value3, float amount1, float amount2, out Vector2D result) {
			result = new Vector2D(
				MathHelper.Barycentric(value1.X, value2.X, value3.X, amount1, amount2),
				MathHelper.Barycentric(value1.Y, value2.Y, value3.Y, amount1, amount2));
		}

		public static Vector2D CatmullRom(Vector2D value1, Vector2D value2, Vector2D value3, Vector2D value4, float amount) {
			return new Vector2D(
				MathHelper.CatmullRom(value1.X, value2.X, value3.X, value4.X, amount),
				MathHelper.CatmullRom(value1.Y, value2.Y, value3.Y, value4.Y, amount));
		}

		public static void CatmullRom(ref Vector2D value1, ref Vector2D value2, ref Vector2D value3, ref Vector2D value4, float amount, out Vector2D result) {
			result = new Vector2D(
				MathHelper.CatmullRom(value1.X, value2.X, value3.X, value4.X, amount),
				MathHelper.CatmullRom(value1.Y, value2.Y, value3.Y, value4.Y, amount));
		}

		public static Vector2D Clamp(Vector2D value1, Vector2D min, Vector2D max) {
			return new Vector2D(
				MathHelper.Clamp(value1.X, min.X, max.X),
				MathHelper.Clamp(value1.Y, min.Y, max.Y));
		}

		public static void Clamp(ref Vector2D value1, ref Vector2D min, ref Vector2D max, out Vector2D result) {
			result = new Vector2D(
				MathHelper.Clamp(value1.X, min.X, max.X),
				MathHelper.Clamp(value1.Y, min.Y, max.Y));
		}

		/// <summary>
		/// Returns float precison distanve between two vectors
		/// </summary>
		/// <param name="value1">
		/// A <see cref="Vector2D"/>
		/// </param>
		/// <param name="value2">
		/// A <see cref="Vector2D"/>
		/// </param>
		/// <returns>
		/// A <see cref="System.Single"/>
		/// </returns>
		public static float Distance(Vector2D value1, Vector2D value2) {
			return (float)Math.Sqrt((value1.X - value2.X) * (value1.X - value2.X) + (value1.Y - value2.Y) * (value1.Y - value2.Y));
		}

		public static void Distance(ref Vector2D value1, ref Vector2D value2, out float result) {
			result = (float)Math.Sqrt((value1.X - value2.X) * (value1.X - value2.X) + (value1.Y - value2.Y) * (value1.Y - value2.Y));
		}

		public static float DistanceSquared(Vector2D value1, Vector2D value2) {
			return (value1.X - value2.X) * (value1.X - value2.X) + (value1.Y - value2.Y) * (value1.Y - value2.Y);
		}

		public static void DistanceSquared(ref Vector2D value1, ref Vector2D value2, out float result) {
			result = (value1.X - value2.X) * (value1.X - value2.X) + (value1.Y - value2.Y) * (value1.Y - value2.Y);
		}

		/// <summary>
		/// Devide first vector with the secund vector
		/// </summary>
		/// <param name="value1">
		/// A <see cref="Vector2D"/>
		/// </param>
		/// <param name="value2">
		/// A <see cref="Vector2D"/>
		/// </param>
		/// <returns>
		/// A <see cref="Vector2D"/>
		/// </returns>
		public static Vector2D Divide(Vector2D value1, Vector2D value2) {
			value1.X /= value2.X;
			value1.Y /= value2.Y;
			return value1;
		}

		public static void Divide(ref Vector2D value1, ref Vector2D value2, out Vector2D result) {
			result = new Vector2D();
			result.X = value1.X / value2.X;
			result.Y = value1.Y / value2.Y;
		}

		public static Vector2D Divide(Vector2D value1, float divider) {
			float factor = 1.0f / divider;
			value1.X *= factor;
			value1.Y *= factor;
			return value1;
		}

		public static void Divide(ref Vector2D value1, float divider, out Vector2D result) {
			float factor = 1.0f / divider;
			result = new Vector2D();
			result.X = value1.X * factor;
			result.Y = value1.Y * factor;
		}

		public static float Dot(Vector2D value1, Vector2D value2) {
			return value1.X * value2.X + value1.Y * value2.Y;
		}

		public static void Dot(ref Vector2D value1, ref Vector2D value2, out float result) {
			result = value1.X * value2.X + value1.Y * value2.Y;
		}

		public override bool Equals(object obj) {
			return (obj is Vector2D) ? this == ((Vector2D)obj) : false;
		}

		public bool Equals(Vector2D other) {
			return this == other;
		}

		public override int GetHashCode() {
			return (int)(this.X + this.Y);
		}

		public static Vector2D Hermite(Vector2D value1, Vector2D tangent1, Vector2D value2, Vector2D tangent2, float amount) {
			value1.X = MathHelper.Hermite(value1.X, tangent1.X, value2.X, tangent2.X, amount);
			value1.Y = MathHelper.Hermite(value1.Y, tangent1.Y, value2.Y, tangent2.Y, amount);
			return value1;
		}

		public static void Hermite(ref Vector2D value1, ref Vector2D tangent1, ref Vector2D value2, ref Vector2D tangent2, float amount, out Vector2D result) {
			result = new Vector2D();
			result.X = MathHelper.Hermite(value1.X, tangent1.X, value2.X, tangent2.X, amount);
			result.Y = MathHelper.Hermite(value1.Y, tangent1.Y, value2.Y, tangent2.Y, amount);
		}

		public float Length() {
			return (float)Math.Sqrt((double)(X * X + Y * Y));
		}

		public float LengthSquared() {
			return X * X + Y * Y;
		}

		public static Vector2D Lerp(Vector2D value1, Vector2D value2, float amount) {
			return new Vector2D(
				MathHelper.Lerp(value1.X, value2.X, amount),
				MathHelper.Lerp(value1.Y, value2.Y, amount));
		}

		public static void Lerp(ref Vector2D value1, ref Vector2D value2, float amount, out Vector2D result) {
			result = new Vector2D(
				MathHelper.Lerp(value1.X, value2.X, amount),
				MathHelper.Lerp(value1.Y, value2.Y, amount));
		}

		public static Vector2D Max(Vector2D value1, Vector2D value2) {
			return new Vector2D(
				MathHelper.Max(value1.X, value2.X),
				MathHelper.Max(value1.Y, value2.Y));
		}

		public static void Max(ref Vector2D value1, ref Vector2D value2, out Vector2D result) {
			result = new Vector2D(
				MathHelper.Max(value1.X, value2.X),
				MathHelper.Max(value1.Y, value2.Y));
		}

		public static Vector2D Min(Vector2D value1, Vector2D value2) {
			return new Vector2D(
				MathHelper.Min(value1.X, value2.X),
				MathHelper.Min(value1.Y, value2.Y));
		}

		public static void Min(ref Vector2D value1, ref Vector2D value2, out Vector2D result) {
			result = new Vector2D(
				MathHelper.Min(value1.X, value2.X),
				MathHelper.Min(value1.Y, value2.Y));
		}

		public static Vector2D Multiply(Vector2D value1, Vector2D value2) {
			value1.X *= value2.X;
			value1.Y *= value2.Y;
			return value1;
		}

		public static Vector2D Multiply(Vector2D value1, float scaleFactor) {
			value1.X *= scaleFactor;
			value1.Y *= scaleFactor;
			return value1;
		}

		public static void Multiply(ref Vector2D value1, float scaleFactor, out Vector2D result) {
			result = new Vector2D();
			result.X = value1.X * scaleFactor;
			result.Y = value1.Y * scaleFactor;
		}

		public static void Multiply(ref Vector2D value1, ref Vector2D value2, out Vector2D result) {
			result = new Vector2D();
			result.X = value1.X * value2.X;
			result.Y = value1.Y * value2.Y;
		}

		public static Vector2D Negate(Vector2D value) {
			value.X = -value.X;
			value.Y = -value.Y;
			return value;
		}

		public static void Negate(ref Vector2D value, out Vector2D result) {
			result = new Vector2D();
			result.X = -value.X;
			result.Y = -value.Y;
		}

		public void Normalize() {
			float factor = 1f / (float)Math.Sqrt((double)(X * X + Y * Y));
			X *= factor;
			Y *= factor;
		}

		public static Vector2D Normalize(Vector2D value) {
			float factor = 1f / (float)Math.Sqrt((double)(value.X * value.X + value.Y * value.Y));
			value.X *= factor;
			value.Y *= factor;
			return value;
		}

		public static void Normalize(ref Vector2D value, out Vector2D result) {
			result = new Vector2D();
			float factor = 1f / (float)Math.Sqrt((double)(value.X * value.X + value.Y * value.Y));
			result.X = value.X * factor;
			result.Y = value.Y * factor;
		}

		public static Vector2D SmoothStep(Vector2D value1, Vector2D value2, float amount) {
			return new Vector2D(
				MathHelper.SmoothStep(value1.X, value2.X, amount),
				MathHelper.SmoothStep(value1.Y, value2.Y, amount));
		}

		public static void SmoothStep(ref Vector2D value1, ref Vector2D value2, float amount, out Vector2D result) {
			result = new Vector2D(
				MathHelper.SmoothStep(value1.X, value2.X, amount),
				MathHelper.SmoothStep(value1.Y, value2.Y, amount));
		}

		public static Vector2D Subtract(Vector2D value1, Vector2D value2) {
			value1.X -= value2.X;
			value1.Y -= value2.Y;
			return value1;
		}

		public static void Subtract(ref Vector2D value1, ref Vector2D value2, out Vector2D result) {
			result = new Vector2D();
			result.X = value1.X - value2.X;
			result.Y = value1.Y - value2.Y;
		}

		public static Vector2D Transform(Vector2D position, Matrix matrix) {
			Transform(ref position, ref matrix, out position);
			return position;
		}

		public static void Transform(ref Vector2D position, ref Matrix matrix, out Vector2D result) {
			result = new Vector2D((position.X * matrix.M11) + (position.Y * matrix.M21) + matrix.M41,
								 (position.X * matrix.M12) + (position.Y * matrix.M22) + matrix.M42);
		}

		public static Vector2D Transform(Vector2D value, Quaternion rotation) {
			throw new NotImplementedException();
		}

		public static void Transform(ref Vector2D value, ref Quaternion rotation, out Vector2D result) {
			throw new NotImplementedException();
		}

		public static void Transform(Vector2D[] sourceArray, ref Matrix matrix, Vector2D[] destinationArray) {
			throw new NotImplementedException();
		}

		public static void Transform(Vector2D[] sourceArray, ref Quaternion rotation, Vector2D[] destinationArray) {
			throw new NotImplementedException();
		}

		public static void Transform(Vector2D[] sourceArray, int sourceIndex, ref Matrix matrix, Vector2D[] destinationArray, int destinationIndex, int length) {
			throw new NotImplementedException();
		}

		public static void Transform(Vector2D[] sourceArray, int sourceIndex, ref Quaternion rotation, Vector2D[] destinationArray, int destinationIndex, int length) {
			throw new NotImplementedException();
		}

		public static Vector2D TransformNormal(Vector2D normal, Matrix matrix) {
			Vector2D.TransformNormal(ref normal, ref matrix, out normal);
			return normal;
		}

		public static void TransformNormal(ref Vector2D normal, ref Matrix matrix, out Vector2D result) {
			result = new Vector2D((normal.X * matrix.M11) + (normal.Y * matrix.M21),
								 (normal.X * matrix.M12) + (normal.Y * matrix.M22));
		}

		public static void TransformNormal(Vector2D[] sourceArray, ref Matrix matrix, Vector2D[] destinationArray) {
			throw new NotImplementedException();
		}

		public static void TransformNormal(Vector2D[] sourceArray, int sourceIndex, ref Matrix matrix, Vector2D[] destinationArray, int destinationIndex, int length) {
			throw new NotImplementedException();
		}

		public override string ToString() {
			StringBuilder sb = new StringBuilder(24);
			sb.Append("{X:");
			sb.Append(this.X);
			sb.Append(" Y:");
			sb.Append(this.Y);
			sb.Append("}");
			return sb.ToString();
		}

		#endregion Public Methods

		#region Operators
		public static Vector2D operator -(Vector2D value) {
			value.X = -value.X;
			value.Y = -value.Y;
			return value;
		}

		public static bool operator ==(Vector2D value1, Vector2D value2) {
			return value1.X == value2.X && value1.Y == value2.Y;
		}

		public static bool operator !=(Vector2D value1, Vector2D value2) {
			return value1.X != value2.X || value1.Y != value2.Y;
		}

		public static Vector2D operator +(Vector2D value1, Vector2D value2) {
			value1.X += value2.X;
			value1.Y += value2.Y;
			return value1;
		}

		public static Vector2D operator -(Vector2D value1, Vector2D value2) {
			value1.X -= value2.X;
			value1.Y -= value2.Y;
			return value1;
		}

		public static Vector2D operator *(Vector2D value1, Vector2D value2) {
			value1.X *= value2.X;
			value1.Y *= value2.Y;
			return value1;
		}

		public static Vector2D operator *(Vector2D value, float scaleFactor) {
			value.X *= scaleFactor;
			value.Y *= scaleFactor;
			return value;
		}

		public static Vector2D operator *(float scaleFactor, Vector2D value) {
			value.X *= scaleFactor;
			value.Y *= scaleFactor;
			return value;
		}

		public static Vector2D operator /(Vector2D value1, Vector2D value2) {
			value1.X /= value2.X;
			value1.Y /= value2.Y;
			return value1;
		}

		public static Vector2D operator /(Vector2D value1, float divider) {
			float factor = 1 / divider;
			value1.X *= factor;
			value1.Y *= factor;
			return value1;
		}

		public static implicit operator Vector2(Vector2D x) { return new Vector2(x.X, x.Y); }
		#endregion Operators
	}
}
