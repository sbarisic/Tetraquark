using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Tq.GUI {
	class Panel : Base {
		Color ActiveColor = new Color(30, 30, 30, 180);
		Color InactiveColor = new Color(10, 10, 10, 180);

		RectangleShape Rect;

		public Panel(Vector2f Pos, Vector2u Size)
			: base(Pos, Size) {
			Rect = new RectangleShape();
			Rect.OutlineColor = Color.Black;
			Rect.OutlineThickness = -1;
			Rect.FillColor = InactiveColor;
		}

		public override bool OnMouseDrag(MouseMoveEventArgs E) {
			if (!base.OnMouseDrag(E)) {
				Position = ParentRelative(new Vector2f(E.X, E.Y)) - GrabPos;
				Invalidate();
			}
			return false;
		}
		
		public override void Activate() {
			base.Activate();
			Invalidate();
			Rect.FillColor = ActiveColor;
		}

		public override void Deactivate() {
			base.Deactivate();
			Invalidate();
			Rect.FillColor = InactiveColor;
		}

		public override void Draw(RenderStates RS) {
			base.Draw(RS);
			Rect.Size = GetAABB().Size;
			Draw(Rect, RS);
		}
	}
}