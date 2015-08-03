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
		RectangleShape Rect;

		public Panel(Vector2f Pos, Vector2u Size) {
			Rect = new RectangleShape();
			ColorActive = ColorInactive;
			Rect.OutlineThickness = -1;
			Rect.OutlineColor = ColorBorder;
			Init(Pos, Size);
			Deactivate();
		}

		public override void Draw(RenderStates RS) {
			base.Draw(RS);
			Rect.FillColor = Color;
			Rect.Size = GetAABB().Size;
			Draw(Rect, RS);
		}
	}
}