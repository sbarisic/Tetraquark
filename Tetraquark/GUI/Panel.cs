using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SFML.Graphics;
using SFML.System;

namespace Tq.GUI {
	class Panel : Base {
		Color ActiveColor = new Color(30, 30, 30, 180);
		Color InactiveColor = new Color(10, 10, 10, 180);

		RectangleShape Rect;
		int N = NN++;
		static int NN = 0;

		public Panel(Vector2f Pos, Vector2u Size)
			: base(Pos, Size) {
			Rect = new RectangleShape();
			Rect.OutlineColor = Color.Black;
			Rect.OutlineThickness = -1;
			Rect.FillColor = InactiveColor;
		}

		Vector2f ClickPos;

		public override bool OnMouse(System.Windows.Forms.MouseEventArgs E, bool Down) {
			if (!base.OnMouse(E, Down)) {
				if (Down) {
					Deactivate();
					Activate();
					ClickPos = Relative(new Vector2f(E.X, E.Y));
				}
			}
			return false;
		}

		public override bool OnMouseDrag(System.Windows.Forms.MouseEventArgs E) {
			if (!base.OnMouseDrag(E)) {
				Position = GetParent().Relative(new Vector2f(E.X, E.Y)) - ClickPos;
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