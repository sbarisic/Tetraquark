using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Tq.GUI {
	class Label : Base {
		Text Txt;

		public Label(string S, Font Fnt, uint FontSize)
			: this(new Vector2f(0, 0), S, Fnt, FontSize) {
			AutoColor = false;
			ColorInactive = new Color(100, 100, 100);
			ColorActive = Color.White;
		}

		public Label(Vector2f Pos, string S, Font Fnt, uint FontSize) {
			Txt = new Text(S, Fnt, FontSize);
			Txt.Style = Text.Styles.Bold;

			Init(Pos, Txt.GetSize().ToVec2u());
			Deactivate();
		}

		public override void Draw(RenderStates RS) {
			base.Draw(RS);
			Txt.Color = Color;
			Draw(Txt, RS);
		}
	}
}