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

		public Window(Vector2f Pos, Vector2u Size, Font Fnt, string Title, bool CloseButton = true) {
			Lbl = new Label(Title, Fnt, Scales.GetFontSize(18));
			Lbl.SetParent(this);

			Border = new RectangleShape();
			BorderSize = (int)(Scales.YRes * 0.01f);
			TitleHeight = Lbl.FontSize + (int)(Lbl.FontSize * 0.2); //(int)Lbl.Size.Y;
			Lbl.Position = new Vector2f(TitleHeight / 4, 0);

			Border.OutlineColor = ColorBorder;
			Border.OutlineThickness = -1;

			ClientArea = new Panel(new Vector2f(BorderSize, TitleHeight), Size);
			ClientArea.SetParent(this);
			Init(Pos, Size + new Vector2u((uint)(BorderSize * 2), (uint)(TitleHeight + BorderSize)));

			if (CloseButton) {
				Button QuitButton = new Button(new Vector2f(), new Vector2u((uint)(TitleHeight * 2.2f), (uint)TitleHeight - 1),
								"X", Fnt, Scales.GetFontSize(18), () => GetParent().RemoveChild(this));
				QuitButton.ColorActive = ColorClose;
				QuitButton.Position = new Vector2f(this.Size.X - QuitButton.Size.X - BorderSize, 0);
				QuitButton.SetParent(this);
			}
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

		bool WasResized;
		Vector2u NewSize;

		public override bool OnMouseDrag(MouseMoveEventArgs E) {
			if (!base.OnMouseDrag(E)) {
				if (GrabPos.X < BorderSize) { // Left
					WasResized = true;
					//float OldX = Position.X;
					Position = new Vector2f((ParentRelative(new Vector2f(E.X, E.Y)) - GrabPos).X, Position.Y);
					//NewSize = new Vector2u((uint)(OldX - Position.X), Size.Y);
				} else if (Size.X - BorderSize < GrabPos.X) { // Right
					WasResized = true;
					Position = new Vector2f((ParentRelative(new Vector2f(E.X, E.Y)) - GrabPos).X, Position.Y);
				} else if (GrabPos.Y < BorderSize) { // Top
					WasResized = true;
					Position = new Vector2f(Position.X, (ParentRelative(new Vector2f(E.X, E.Y)) - GrabPos).Y);
				} else if (Size.Y - BorderSize < GrabPos.Y) { // Bottom
					WasResized = true;
					Position = new Vector2f(Position.X, (ParentRelative(new Vector2f(E.X, E.Y)) - GrabPos).Y);
				} else {
					Position = ParentRelative(new Vector2f(E.X, E.Y)) - GrabPos;
				}
				Invalidate();
			} else if (WasResized) {
				WasResized = false;
				//Console.WriteLine("Resized! {0}", NewSize);
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