using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SFML.Graphics;
using SFML.System;

namespace Tq.Misc {
	class Point3D {
		float X, Y, Z;

		public Point3D(float X, float Y, float Z) {
			this.X = X;
			this.Y = Y;
			this.Z = Z;
		}

		public Point3D Rotate(float X, float Y, float Z) {
			return RotateX(X).RotateY(Y).RotateZ(Z);
		}

		public Point3D RotateX(float Ang) {
			float Rad = Ang * (float)Math.PI / 180;
			float CosA = (float)Math.Cos(Rad);
			float SinA = (float)Math.Sin(Rad);
			Y = Y * CosA - Z * SinA;
			Z = Y * SinA + Z * CosA;
			return this;
		}

		public Point3D RotateY(float Ang) {
			float Rad = Ang * (float)Math.PI / 180;
			float CosA = (float)Math.Cos(Rad);
			float SinA = (float)Math.Sin(Rad);
			Z = Z * CosA - X * SinA;
			X = Z * SinA + X * CosA;
			return this;
		}

		public Point3D RotateZ(float Ang) {
			float Rad = Ang * (float)Math.PI / 180;
			float CosA = (float)Math.Cos(Rad);
			float SinA = (float)Math.Sin(Rad);
			X = X * CosA - Y * SinA;
			Y = X * SinA + Y * CosA;
			return this;
		}

		public Vector2f Project(float Width, float Height, float FOV, float ViewDistance) {
			float Factor = FOV / (ViewDistance + Z);
			return new Vector2f(this.X * Factor + Width / 2, -this.Y * Factor + Height / 2);
		}
	}
}