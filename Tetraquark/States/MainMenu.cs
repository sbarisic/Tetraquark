using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO;

using SFML.System;
using SFML.Graphics;
using SFML.Window;
using Tq.Graphics;
using Tq.Misc;

namespace Tq.States {
	class MenuEntry {
		public int Selected;
		string MenuTitle;
		Dictionary<string, Action> MenuEntries;

		public MenuEntry(string MenuTitle) {
			this.MenuTitle = MenuTitle;
			MenuEntries = new Dictionary<string, Action>();
		}

		void ValidateSelected() {
			if (Selected < 0)
				Selected = 0;
			if (Selected >= MenuEntries.Count)
				Selected = MenuEntries.Count - 1;
		}

		public void Add(string Name, Action OnClick) {
			MenuEntries.Add(Name, OnClick);
		}

		public void Enter() {
			ValidateSelected();
			if (Selected != -1)
				MenuEntries.Values.ToArray()[Selected]();
		}

		string GenerateGarbage() {
			string[] Sub = new string[] { "registry", "data" };
			string[] SubSub = new string[] { "value", "status", "freq", };

			return "os." + Sub[Utils.Random(Sub.Length)] + "." + SubSub[Utils.Random(SubSub.Length)] + " = " + Utils.Random(1000, 9999);
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

		public void DrawMenu(TextBuffer GUIText) {
			string[] Entries = MenuEntries.Keys.ToArray();
			ValidateSelected();

			// Separator
			int SepLen = MenuTitle.Length + 2;
			for (int i = 0; i < Entries.Length; i++)
				SepLen = Math.Max(SepLen, Entries[i].Length);

			SepLen += 3;
			DrawBox(GUIText, SepLen, 0, 0, GUIText.BufferHeight - 1);

			// Outline
			DrawBox(GUIText, 0, 0, GUIText.BufferWidth - 1, GUIText.BufferHeight - 1);
			GUIText.Print(2, 0, "[" + MenuTitle + "]");

			// Entries
			for (int i = 0; i < Entries.Length; i++)
				GUIText.Print(2, i + 2, Entries[i],
					i == Selected ? Color.Black : ConsoleColor.Gray.ToColor(), i == Selected ? Color.White : Color.Black);
		}
	}

	class MainMenu : State {
		TextBuffer GUIText;
		MenuEntry CurrentMenu;

		public MainMenu(RenderTexture RTex) {
			GUIText = new TextBuffer(80, 30);
			GUIText.SetFontTexture(ResourceMgr.Get<Texture>("font"));
			GUIText.Sprite.Scale = RTex.Size.ToVec2f().Divide(GUIText.Sprite.Texture.Size.ToVec2f()) - new Vector2f(.1f, .1f);
			GUIText.Sprite.Position = RTex.Texture.Size.ToVec2f() / 2;
			GUIText.Sprite.Origin = GUIText.Sprite.Texture.Size.ToVec2f() / 2;

			CurrentMenu = new MenuEntry("Main Menu");
			CurrentMenu.Add("New Game", () => {
			});
			CurrentMenu.Add("Load Game", () => {
			});
			CurrentMenu.Add("Settings", () => {
			});
			CurrentMenu.Add("Exit", () => Program.Running = false);

			CurrentMenu.DrawMenu(GUIText);
		}

		public override void OnKey(KeyEventArgs E, bool Pressed) {
			if (E.Code == Keyboard.Key.Escape && Pressed)
				Program.Running = false;

			if (Pressed) {
				if (E.Code == Keyboard.Key.Down)
					CurrentMenu.Selected++;
				else if (E.Code == Keyboard.Key.Up)
					CurrentMenu.Selected--;
				else if (E.Code == Keyboard.Key.Return)
					CurrentMenu.Enter();
				CurrentMenu.DrawMenu(GUIText);
			}
		}

		public override void Update(float Dt) {
			Timer.Update(Program.GameTime);
		}

		public override void Draw(RenderTexture RT) {
			RT.Clear(Color.Black);
			Shader.Bind(Shaders.CRT);
			Shaders.CRT.SetParameter("texture", GUIText.Sprite.Texture);
			Shaders.CRT.SetParameter("resolution", GUIText.Sprite.Texture.Size.ToVec2f());
			Shaders.CRT.SetParameter("blur", 0.1f);
			Shaders.CRT.SetParameter("chromatic", 0.8f);
			Shaders.CRT.SetParameter("lines", 0.9f);
			RT.Draw(GUIText);
			Shader.Bind(null);
		}
	}
}