using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FarseerPhysics;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using SFML.System;
using Microsoft.Xna.Framework;

namespace Tq {
	static class VectorExtensions {
		public static Vector2 ToVec(this Vector2f V) {
			return new Vector2(V.X, V.Y);
		}

		public static Vector2f ToVec(this Vector2 V) {
			return new Vector2f(V.X, V.Y);
		}

		public static Vector2f ToSimUnits(this Vector2f V) {
			return new Vector2f(V.X.ToSimUnits(), V.Y.ToSimUnits());
		}

		public static Vector2f ToDisplayUnits(this Vector2f V) {
			return new Vector2f(V.X.ToDisplayUnits(), V.Y.ToDisplayUnits());
		}

		public static Vector2f ToVec2f(this Vector2u V) {
			return new Vector2f(V.X, V.Y);
		}

		public static Vector2f ToVec2f(this Vector2i V) {
			return new Vector2f(V.X, V.Y);
		}

		public static Vector2u ToVec2u(this Vector2f V) {
			return new Vector2u((uint)V.X, (uint)V.Y);
		}
		
		public static float LengthSq(this Vector2f V) {
			return V.X * V.X + V.Y * V.Y;
		}

		public static float Length(this Vector2f V) {
			return (float)Math.Sqrt(V.LengthSq());
		}

		public static float Distance(this Vector2f V, Vector2f V2) {
			return (V2 - V).Length();
		}

		public static float DistanceSq(this Vector2f V, Vector2f V2) {
			return (V2 - V).LengthSq();
		}

		public static Vector2f Normalize(this Vector2f V) {
			float Mag = V.Length();
			return new Vector2f(V.X / Mag, V.Y / Mag);
		}

		public static float BiggestDim(this Vector2f V) {
			if (V.X > V.Y)
				return V.X;
			return V.Y;
		}

		public static Vector2f Divide(this Vector2f V, Vector2f V2) {
			return new Vector2f(V.X / V2.X, V.Y / V2.Y);
		}

		public static Vector2f Multiply(this Vector2f V, Vector2f V2) {
			return new Vector2f(V.X * V2.X, V.Y * V2.Y);
		}

		public static float Angle(this Vector2f Start, Vector2f End) {
			return (float)Math.Atan2(End.Y - Start.Y, End.X - Start.X);
		}
	}
}
