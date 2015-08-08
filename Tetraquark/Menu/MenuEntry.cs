using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SFML.Graphics;
using SFML.System;
using SFML.Window;
using Tq.Graphics;

namespace Tq.Menu {
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

		public Widget GetSelectedWidget() {
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
			GUIText.DrawBox(X, Y, Width, Height);
			GUIText.Print(X + 2, Y, "[" + MenuTitle + "]");

			// Entries
			int YOff = 0;
			for (int i = 0; i < Entries.Length; i++) {
				Entries[i].Draw(GUIText, X + 2, Y + 2 + YOff, i == Selected);
				YOff += Entries[i].Height;
			}
			/*GUIText.Print(X + 2, Y + i + 2, Entries[i],
				i == Selected ? Color.Black : ConsoleColor.Gray.ToColor(), i == Selected ? Color.White : Color.Black);*/
		}
	}
}