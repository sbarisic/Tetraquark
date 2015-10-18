using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SFML.Graphics;
using SFML.System;

namespace Tq.Game {
	class Camera {
		View V;
		bool Dirty;

		Vector2f Pos, Res;
		float Rot, Zm;

		public Camera() {
			V = new View();
			Reset();
		}

		public void Reset() {
			Position = new Vector2f(0, 0);
			Resolution = new Vector2f(0, 0);
			Zoom = 1;
			Rotation = 0;
		}

		public Vector2f Resolution {
			get {
				return Res;
			}
			set {
				if (Res == value)
					return;
				Res = value;
				Dirty = true;
			}
		}

		public Vector2f Position {
			get {
				return Pos;
			}
			set {
				if (Pos == value)
					return;
				Pos = value;
				Dirty = true;
			}
		}

		public float Rotation {
			get {
				return Rot;
			}
			set {
				if (Rot == value)
					return;
				Rot = value;
				Dirty = true;
			}
		}

		public float Zoom {
			get {
				return Zm;
			}
			set {
				if (Zm == value)
					return;
				Zm = value;
				Dirty = true;
			}
		}

		public static implicit operator View(Camera C) {
			if (C.Dirty) {
				C.Dirty = false;
				C.V.Reset(new FloatRect(new Vector2f(0, 0), C.Res));
				C.V.Center = new Vector2f(0, 0);
				C.V.Move(C.Pos);
				C.V.Rotation = C.Rot;
				C.V.Zoom(C.Zm);
			}
			return C.V;
		}
	}
}