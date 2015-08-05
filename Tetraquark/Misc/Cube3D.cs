using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SFML.System;

namespace Tq.Misc {
	class Cube3D {
		Point3D[] Verts;
		public float RX, RY, RZ;

		public Cube3D() {
			Verts = new Point3D[] {
				new Point3D(-1,1,-1),
				new Point3D(1,1,-1),
				new Point3D(1,-1,-1),
				new Point3D(-1,-1,-1),
				new Point3D(-1,1,1),
				new Point3D(1,1,1),
				new Point3D(1,-1,1),
				new Point3D(-1,-1,1)
			};
		}

		public Vector2f[] Project(float W, float H, float FOV = 256, float Dist = 4) {
			Vector2f[] Transf = new Vector2f[Verts.Length];
			for (int i = 0; i < Verts.Length; i++)
				Transf[i] = Verts[i].Rotate(RX, RY, RZ).Project(W, H, FOV, Dist);
			return Transf;
		}
	}
}