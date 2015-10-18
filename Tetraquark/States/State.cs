using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SFML.Window;
using SFML.System;
using SFML.Graphics;
using Tq.Graphics;

namespace Tq.States {
	class State {
		public virtual void OnActivated() {
		}

		public virtual void OnDeactivated() {
		}

		public virtual void OnClosed() {
		}

		public virtual void OnGainedFocus() {
		}

		public virtual void OnLostFocus() {
		}

		public virtual void OnKey(KeyEventArgs E, bool Pressed) {
		}

		public virtual void OnTextEntered(TextEventArgs E) {
		}

		public virtual void OnMouseButton(MouseButtonEventArgs E, bool Pressed) {
		}

		public virtual void OnMouseEntered(bool Entered) {
		}

		public virtual void OnMouseMoved(MouseMoveEventArgs E) {
		}

		public virtual void OnMouseWheelMoved(MouseWheelEventArgs E) {
		}

		public virtual void OnResized(SizeEventArgs E) {
		}

		public virtual void Update(float Dt) {
		}

		public virtual void Draw(RenderSprite RT) {
		}
	}
}