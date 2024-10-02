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

		public Vector2 Offset {
			get {
				return Cam.Offset;
			}

			set {
				Cam.Offset = value;
			}
		}

		public Vector2 Target {
			get {
				return Cam.Target;
			}

			set {
				Cam.Target = value;
			}
		}

		public float Zoom {
			get {
				return Cam.Zoom;
			}

			set {
				Cam.Zoom = value;
			}
		}

		public GfxCamera() {
			Cam = new Camera2D(new Vector2(0, 0), new Vector2(0, 0), 0, 1);
		}

		public void SetZoom(float Zoom) {
			Cam.Zoom = Zoom;
		}

		public Vector2 ScreenToWorld(Vector2 Pos) {
			return Raylib.GetScreenToWorld2D(Pos, Cam);
		}

		public Vector2 WorldToScreen(Vector2 Pos) {
			return Raylib.GetWorldToScreen2D(Pos, Cam);
		}
	}
}
