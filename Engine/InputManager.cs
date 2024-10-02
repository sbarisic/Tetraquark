using Raylib_cs;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

using Tetraquark2.Gfx;

namespace Tetraquark2.Engine {
	enum TqMouseButton {
		Left = MouseButton.Left,
		Right = MouseButton.Right,
		Middle = MouseButton.Middle
	}

	class InputManager {
		public float GetMouseWheel() {
			return Raylib.GetMouseWheelMove();
		}

		public bool IsMouseButtonPressed(TqMouseButton Btn) {
			return Raylib.IsMouseButtonPressed((MouseButton)Btn);
		}

		public bool IsMouseButtonReleased(TqMouseButton Btn) {
			return Raylib.IsMouseButtonReleased((MouseButton)Btn);
		}

		public bool IsMouseButtonDown(TqMouseButton Btn) {
			return Raylib.IsMouseButtonDown((MouseButton)Btn);
		}

		public Vector2 GetMousePosition() {
			return Raylib.GetMousePosition();
		}

		public Vector2 GetMouseDelta() {
			return Raylib.GetMouseDelta();
		}
	}
}
