using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Tq.GUI {
	class Button : Base {
		RectangleShape Rect;
		Label Lbl;
		Action OnClick;

		public Button(Vector2f Pos, Vector2u Size, string Txt, Font Fnt, uint FontSize, Action OnClick) {
			this.OnClick = OnClick;
			Rect = new RectangleShape();
			//ColorActive = ColorInactive;
			Rect.OutlineThickness = -1;
			Rect.OutlineColor = ColorBorder;

			Lbl = new Label(Txt, Fnt, FontSize);
			Lbl.Position = Size.ToVec2f() / 2 - Lbl.Size.ToVec2f() / 2;
			Lbl.SetParent(this);

			Init(Pos, Size);
			Deactivate();
		}

		public override bool OnMouse(MouseButtonEventArgs E, bool Down) {
			if (!base.OnMouse(E, Down)) {
				if (!Down) {
					Color = ColorActive;
					OnClick();
				} else
					Color = ColorPressed;
				Invalidate();
			}
			return false;
		}

		public override bool OnMouseMove(MouseMoveEventArgs E) {
			if (!base.OnMouseMove(E))
				Activate();
			return false;
		}

		public override void Activate() {
			base.Activate();
			ToTop();
			Lbl.Color = Lbl.ColorActive;
			Lbl.Invalidate();
		}

		public override void Deactivate() {
			base.Deactivate();
			Lbl.Color = Lbl.ColorInactive;
			Lbl.Invalidate();
		}

		public override void Draw(RenderStates RS) {
			base.Draw(RS);
			Rect.FillColor = Color;
			Rect.Size = GetAABB().Size;
			Draw(Rect, RS);
		}
	}
}