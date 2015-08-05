using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SFML.System;

namespace Tq.Meth {
	struct AABB {
		public float X, Y, W, H;

		public Vector2f Diagonal {
			get {
				return new Vector2f(W - X, H - Y);
			}
		}

		public Vector2f Pos {
			get {
				return new Vector2f(X, Y);
			}
		}

		public Vector2f Pos2 {
			get {
				return new Vector2f(X + W, Y + H);
			}
		}

		public Vector2f Size {
			get {
				return new Vector2f(W, H);
			}
		}

		public static AABB FromPoints(params Vector2f[] Points) {
			float? XMin = null, XMax = null, YMin = null, YMax = null;
			for (int i = 0; i < Points.Length; i++) {
				XMin = Math.Min(XMin ?? Points[i].X, Points[i].X);
				XMax = Math.Max(XMax ?? Points[i].X, Points[i].X);
				YMin = Math.Min(YMax ?? Points[i].Y, Points[i].Y);
				YMax = Math.Max(YMax ?? Points[i].Y, Points[i].Y);
			}
			return new AABB(XMin.Value, YMin.Value, (XMax - XMax).Value, (YMax - YMin).Value);
		}

		public AABB(float X, float Y, float W, float H) {
			this.X = X;
			this.Y = Y;
			this.W = W;
			this.H = H;
		}

		public AABB(float X, float Y, Vector2f Size)
			: this(X, Y, Size.X, Size.Y) {
		}

		public AABB(Vector2f Pos, float W, float H)
			: this(Pos.X, Pos.Y, W, H) {
		}

		public AABB(Vector2f Pos, Vector2f Size)
			: this(Pos.X, Pos.Y, Size.X, Size.Y) {
		}

		public static bool IsInside(float X, float Y, float W, float H, Vector2f V) {
			return (V.X > X && V.X < X + W && V.Y > Y && V.Y < Y + H);
		}

		public bool IsInside(Vector2f V) {
			return IsInside(X, Y, W, H, V);
		}

		public bool IsInside(float X, float Y) {
			return IsInside(new Vector2f(X, Y));
		}

		public bool Overlaps(AABB Box) {
			if (X > Box.X + Box.W || Box.X > X + W || Y > Box.Y + Box.H || Box.Y > Y + H)
				return false;
			return true;
		}

		public AABB GetOverlap(AABB Box) {
			var X1 = Math.Max(X, Box.X);
			var Y1 = Math.Max(Y, Box.Y);
			var X2 = Math.Max(X + W, Box.X + Box.W);
			var Y2 = Math.Max(Y + H, Box.Y + Box.H);
			if (X1 <= X2 && Y1 <= Y2)
				return new AABB(X1, Y1, X2 - X1, Y2 - Y1);
			return new AABB();
		}
	}
}