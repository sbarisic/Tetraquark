using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SFML.Graphics;
using SFML.System;
using SFML.Window;
using Tq.Graphics;

namespace Tq.States {
	class Widget {
		public int Width, Height;
		public virtual void OnKey(Keyboard.Key K, bool IsDown) {
		}

		public virtual void Draw(TextBuffer TBuffer, int X, int Y, bool IsSelected) {
		}
	}

	class WidgetButton : Widget {
		string Txt;
		Action OnClick;

		public WidgetButton(string Txt, Action OnClick) {
			this.Txt = Txt;
			this.OnClick = OnClick;
			Height = 1;
			Width = Txt.Length;
		}

		public override void OnKey(Keyboard.Key K, bool IsDown) {
			if (IsDown && K == Keyboard.Key.Return)
				OnClick();
		}

		public override void Draw(TextBuffer TBuffer, int X, int Y, bool IsSelected) {
			Color Fg = ConsoleColor.Gray.ToColor();
			Color Bg = Color.Black;
			if (IsSelected) {
				Fg = Color.Black;
				Bg = Color.White;
			}
			TBuffer.Print(X, Y, Txt, Fg, Bg);
		}
	}

	class WidgetCheckbox : Widget {
		string Txt;
		bool IsChecked;
		Action<bool> OnCheck;

		public WidgetCheckbox(string Txt, bool IsChecked, Action<bool> OnClick) {
			this.Txt = Txt;
			this.OnCheck = OnClick;
			this.IsChecked = IsChecked;
			Height = 1;
			Width = Txt.Length + 4;
		}

		public override void OnKey(Keyboard.Key K, bool IsDown) {
			if (IsDown && K == Keyboard.Key.Return)
				OnCheck(IsChecked = !IsChecked);
		}

		public override void Draw(TextBuffer TBuffer, int X, int Y, bool IsSelected) {
			Color Fg = ConsoleColor.Gray.ToColor();
			Color Bg = Color.Black;
			if (IsSelected) {
				Fg = Color.Black;
				Bg = Color.White;
			}
			string Check = " ";
			if (IsChecked)
				Check = "X";
			TBuffer.Print(X, Y, "[" + Check + "] " + Txt, Fg, Bg);
		}
	}

	class MenuEntry {
		public int Selected;
		string MenuTitle;
		List<Widget> MenuEntries;

		public MenuEntry(string MenuTitle) {
			this.MenuTitle = MenuTitle;
			MenuEntries = new List<Widget>();
		}

		void ValidateSelected() {
			if (Selected < 0)
				Selected = 0;
			if (Selected >= MenuEntries.Count)
				Selected = MenuEntries.Count - 1;
		}

		public MenuEntry Add(Widget W) {
			MenuEntries.Add(W);
			return this;
		}

		public Widget GetActiveWidget() {
			ValidateSelected();
			if (Selected != -1)
				return MenuEntries[Selected];
			return null;
		}

		public void DrawMenu(TextBuffer GUIText) {	
			ValidateSelected();
			Widget[] Entries = MenuEntries.ToArray();

			// Separator
			int Width = MenuTitle.Length + 5;
			int Height = 3;

			for (int i = 0; i < Entries.Length; i++) {
				Width = Math.Max(Width, Entries[i].Width + 3);
				Height += Entries[i].Height;
			}

			int X = GUIText.BufferWidth / 2 - Width / 2 - 1;
			int Y = GUIText.BufferHeight / 2 - Height / 2 - 1;

			//DrawBox(GUIText, Width, 0, 0, GUIText.BufferHeight - 1);

			// Outline
			for (int x = 0; x < Width; x++)
				for (int y = 0; y < Height; y++)
					GUIText[X + x, Y + y] = 0;
			DrawBox(GUIText, X, Y, Width, Height);
			GUIText.Print(X + 2, Y, "[" + MenuTitle + "]");

			// Entries
			for (int i = 0; i < Entries.Length; i++)
				Entries[i].Draw(GUIText, X + 2, Y + i + 2, i == Selected);
				/*GUIText.Print(X + 2, Y + i + 2, Entries[i],
					i == Selected ? Color.Black : ConsoleColor.Gray.ToColor(), i == Selected ? Color.White : Color.Black);*/
		}


		void DrawBox(TextBuffer TB, int X, int Y, int W, int H) {
			Action<int, int, bool, bool, bool, bool> Put = (XX, YY, L, R, U, D) => {
				if (XX - 1 >= 0 && new[] { (char)197, (char)192, (char)193, (char)194, (char)195,
					(char)196, (char)218 }.Contains(TB[XX - 1, YY]))
					L = true;
				if (XX + 1 < TB.BufferWidth && new[] { (char)197, (char)191, (char)217, (char)193,
					(char)194, (char)180, (char)196 }.Contains(TB[XX + 1, YY]))
					R = true;
				if (YY - 1 >= 0 && new[] { (char)197, (char)179, (char)180, (char)191, (char)194,
					(char)195, (char)218 }.Contains(TB[XX, YY - 1]))
					U = true;
				if (YY + 1 < TB.BufferHeight && new[] { (char)197, (char)192, (char)193, (char)217,
					(char)179, (char)180, (char)195 }.Contains(TB[XX, YY + 1]))
					D = true;

				if ((L || R) && !U && !D)
					TB[XX, YY] = 196;
				else if (!L && !R && (U || D))
					TB[XX, YY] = 179;
				else if (L && R && U && D)
					TB[XX, YY] = 197;
				else if (L && R && U && !D)
					TB[XX, YY] = 193;
				else if (L && R && !U && D)
					TB[XX, YY] = 194;
				else if (L && !R && U && D)
					TB[XX, YY] = 180;
				else if (L && !R && U && !D)
					TB[XX, YY] = 217;
				else if (L && !R && !U && D)
					TB[XX, YY] = 191;
				else if (!L && R && U && D)
					TB[XX, YY] = 195;
				else if (!L && R && U && !D)
					TB[XX, YY] = 192;
				else if (!L && R && !U && D)
					TB[XX, YY] = 218;
				else if (!L && !R && !U && !D)
					TB[XX, YY] = '*';
				else
					TB[XX, YY] = '#';
			};

			if (W == 0) {
				Put(X, Y, false, false, true, true);
				Put(X, Y + H, false, false, true, true);
			} else if (H == 0) {
				Put(X, Y, true, true, false, false);
				Put(X + W, Y, true, true, false, false);
			} else {
				Put(X, Y, false, true, false, true);
				Put(X + W, Y, true, false, false, true);
				Put(X + W, Y + H, true, false, true, false);
				Put(X, Y + H, false, true, true, false);
			}

			for (int x = 1; x < W; x++) {
				Put(X + x, Y, true, true, false, false);
				Put(X + x, Y + H, true, true, false, false);
			}
			for (int y = 1; y < H; y++) {
				Put(X, Y + y, false, false, true, true);
				Put(X + W, Y + y, false, false, true, true);
			}
		}
	}
}