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
	class Widget {
		public int Width, Height;
		public bool CaptureInput;
		public bool Enabled;

		public Widget() {
			Enabled = true;
		}

		public virtual Widget Enable() {
			Enabled = true;
			return this;
		}

		public virtual Widget Disable() {
			Enabled = false;
			return this;
		}

		public virtual void OnKey(Keyboard.Key K, bool IsDown) {
		}

		public virtual void OnText(char C) {
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
			if (!Enabled)
				return;
			if (IsDown && K == Keyboard.Key.Return)
				OnClick();
		}

		public override void Draw(TextBuffer TBuffer, int X, int Y, bool IsSelected) {
			Color Fg = ConsoleColor.Gray.ToColor();
			Color Bg = Color.Black;
			if (IsSelected) {
				Fg = Color.Black;
				if (Enabled)
					Bg = Color.White;
				else
					Bg = ConsoleColor.DarkGray.ToColor();
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
			if (!Enabled)
				return;
			if (IsDown && K == Keyboard.Key.Return)
				OnCheck(IsChecked = !IsChecked);
		}

		public override void Draw(TextBuffer TBuffer, int X, int Y, bool IsSelected) {
			Color Fg = ConsoleColor.Gray.ToColor();
			Color Bg = Color.Black;
			if (IsSelected) {
				Fg = Color.Black;
				if (Enabled)
					Bg = Color.White;
				else
					Bg = ConsoleColor.DarkGray.ToColor();
			}
			string Check = " ";
			if (IsChecked)
				Check = "X";
			TBuffer.Print(X, Y, Txt, Fg, Bg);
			TBuffer.Print(X + Txt.Length + 1, Y, "[" + Check + "]");
		}
	}

	class WidgetTextbox : Widget {
		string Title, Txt;
		int Len;
		Action<string> OnClick;
		Func<char, bool> OnInput;

		public WidgetTextbox(string Title, string Txt, int Len, Action<string> OnClick, Func<char, bool> OnInput = null) {
			this.Title = Title;
			this.Txt = Txt;
			this.Len = Len;
			this.OnClick = OnClick;
			this.OnInput = OnInput;
			Height = 1;
			Width = Txt.Length + Len + 3;
		}

		public override void OnKey(Keyboard.Key K, bool IsDown) {
			if (!Enabled)
				return;
			if (IsDown && K == Keyboard.Key.Return) {
				if (!CaptureInput)
					CaptureInput = true;
				else {
					CaptureInput = false;
					OnClick(Txt);
				}
			} else if (IsDown && K == Keyboard.Key.Escape)
				CaptureInput = false;

			if (CaptureInput && !IsDown && K == Keyboard.Key.BackSpace && Txt.Length > 0)
				Txt = Txt.Substring(0, Txt.Length - 1);
		}

		public override void OnText(char C) {
			if (!Enabled)
				return;
			if (C == (char)13 || Txt.Length >= Len)
				return;
			if (OnInput != null && !OnInput(C))
				return;
			Txt += C;
		}

		public override void Draw(TextBuffer TBuffer, int X, int Y, bool IsSelected) {
			Color Fg = ConsoleColor.Gray.ToColor();
			Color Bg = Color.Black;
			Color Fg2 = Fg;
			Color Bg2 = Bg;
			if (IsSelected) {
				if (!CaptureInput) {
					Fg = Color.Black;
					if (Enabled)
						Bg = Color.White;
					else
						Bg = ConsoleColor.DarkGray.ToColor();
				} else {
					Fg2 = Color.Black;
					Bg2 = Color.White;
				}
			}

			TBuffer.Print(X, Y, Title, Fg, Bg);
			TBuffer.Print(X + Title.Length, Y, ": ");
			TBuffer.Print(X + Title.Length + 2, Y, Txt + (CaptureInput ? "_" : ""), Fg2, Bg2);
		}
	}

	class WidgetVListBox : Widget {
		string Txt;
		string[] Entries;
		int Selected, SelectedActive;
		Action<string, int> OnChanged;

		public WidgetVListBox(string Txt, string[] Entries, int Selected, Action<string, int> OnChanged) {
			this.Txt = Txt;
			this.Entries = Entries;
			this.Selected = SelectedActive = Selected;
			this.OnChanged = OnChanged;

			Height = Entries.Length + 2;
			Width = Txt.Length + 4;
		}

		public override void OnKey(Keyboard.Key K, bool IsDown) {
			if (!Enabled)
				return;
			if (IsDown && K == Keyboard.Key.Return) {
				if (!CaptureInput) {
					if (Entries.Length > 0)
						CaptureInput = true;
				} else if (Selected >= 0 && Selected < Entries.Length) {
					SelectedActive = Selected;
					OnChanged(Entries[Selected], Selected);
					CaptureInput = false;
				}
			} else if (IsDown && K == Keyboard.Key.Escape)
				CaptureInput = false;

			if (CaptureInput && IsDown) {
				if (K == Keyboard.Key.Up)
					Selected--;
				else if (K == Keyboard.Key.Down)
					Selected++;
			}
			if (Selected < 0)
				Selected = 0;
			if (Selected >= Entries.Length)
				Selected = Entries.Length - 1;
		}

		public override void Draw(TextBuffer TBuffer, int X, int Y, bool IsSelected) {
			Color Fg = ConsoleColor.Gray.ToColor();
			Color Bg = Color.Black;
			if (IsSelected && !CaptureInput) {
				Fg = Color.Black;
				if (Enabled)
					Bg = Color.White;
				else
					Bg = ConsoleColor.DarkGray.ToColor();
			}

			TBuffer.DrawBox(X, Y, Width, Entries.Length + 1);
			TBuffer.Print(X + 1, Y, "[" + Txt + "]", Fg, Bg);

			for (int i = 0; i < Entries.Length; i++) {
				Color F = Color.White;
				Color B = Color.Black;
				if (CaptureInput && Selected == i) {
					F = Color.Black;
					B = Color.White;
				}
				string Pref = " ";
				if (Selected == i || SelectedActive == i)
					Pref = ">";
				TBuffer.Print(X + 1, Y + i + 1, Pref, F, B);
				TBuffer.Print(X + 3, Y + i + 1, Entries[i]);
			}
		}
	}
}