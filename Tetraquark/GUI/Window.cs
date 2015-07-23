using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Tq.GUI {
	class Window : Base {
		int TitleHeight, BorderSize;

		public Panel ClientArea;
		RectangleShape Border;
		Label Lbl;

		public Window(Vector2f Pos, Vector2u Size, Font Fnt, string Title) {
			Lbl = new Label(Title, Fnt, Scales.GetFontSize(16));
			Lbl.SetParent(this);

			Border = new RectangleShape();

			BorderSize = (int)(Scales.YRes * 0.01f);
			TitleHeight = (int)Lbl.Size.Y;
			Init(Pos, Size + new Vector2u((uint)(BorderSize * 2), (uint)(TitleHeight + BorderSize)));

			Lbl.Position = new Vector2f(TitleHeight / 4, 0);

			Border.OutlineColor = ColorBorder;
			Border.OutlineThickness = -1;

			ClientArea = new Panel(new Vector2f(BorderSize, TitleHeight), Size);
			ClientArea.SetParent(this);
			Deactivate();
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

		public override bool OnMouseDrag(MouseMoveEventArgs E) {
			if (!base.OnMouseDrag(E) /*&& AABB.IsInside(0, 0, Size.X, TitleHeight, GrabPos)*/) {
				Position = ParentRelative(new Vector2f(E.X, E.Y)) - GrabPos;
				Invalidate();
			}
			return false;
		}

		public override void Draw(RenderStates RS) {
			base.Draw(RS);
			Border.FillColor = Color;
			Border.Size = Size.ToVec2f();
			Draw(Border, RS);
			//Draw(Label, RS);
		}
	}
}