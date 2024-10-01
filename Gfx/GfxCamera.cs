using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

using Raylib_cs;

namespace Tetraquark2.Gfx {
	class GfxCamera {
		public Camera2D Cam;

		public GfxCamera() {
			Cam = new Camera2D(new Vector2(0, 0), new Vector2(0, 0), 0, 1);
		}

		public void SetZoom(float Zoom) {
			Cam.Zoom = Zoom;
		}
	}
}
