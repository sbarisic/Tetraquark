using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SFML.Graphics;
using SFML.System;

namespace Tq.Game {
	static class Camera {
		static View V;
		static bool Dirty;

		static Vector2f Pos, Res;
		static float Rot, Zm;

		static Camera() {
			V = new View();
			Position = new Vector2f(0, 0);
			Res = new Vector2f(0, 0);
			Zoom = 1;
			Rotation = 0;
		}

		public static Vector2f Resolution {
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

		public static Vector2f Position {
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

		public static float Rotation {
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

		public static float Zoom {
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

		public static View ToView() {
			if (Dirty) {
				Dirty = false;
				V.Reset(new FloatRect(new Vector2f(0, 0), Res));
				V.Center = new Vector2f(0, 0);
				V.Move(Pos);
				V.Rotation = Rot;
				V.Zoom(Zm);
			}
			return V;
		}
	}
}