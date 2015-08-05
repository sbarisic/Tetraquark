using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using SFML.System;
using SFML.Graphics;
using SFML.Window;
using Tq.Graphics;
using Tq.Misc;

namespace Tq.States {
	class MenuEntry {
		string ASCII;

		public int Selected;
		string MenuTitle;
		Dictionary<string, Action> MenuEntries;

		public MenuEntry(string MenuTitle) {
			this.MenuTitle = MenuTitle;
			MenuEntries = new Dictionary<string, Action>();
			ASCII = File.ReadAllText("ascii_atom.txt");
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

		public void DrawMenu(TextBuffer GUIText) {
			string[] Entries = MenuEntries.Keys.ToArray();
			ValidateSelected();
			GUIText.Clear();

			// Outline
			GUIText[0, 0] = 218;
			GUIText[GUIText.BufferWidth - 1, 0] = 191;
			GUIText[GUIText.BufferWidth - 1, GUIText.BufferHeight - 1] = 217;
			GUIText[0, GUIText.BufferHeight - 1] = 192;
			for (int i = 1; i < GUIText.BufferWidth - 1; i++) // X
				GUIText[i, 0] = GUIText[i, GUIText.BufferHeight - 1] = 196;
			for (int i = 1; i < GUIText.BufferHeight - 1; i++) // Y
				GUIText[0, i] = GUIText[GUIText.BufferWidth - 1, i] = 179;
			GUIText.Print(2, 0, "[" + MenuTitle + "]");


			// Separator
			int SepLen = MenuTitle.Length + 2;
			for (int i = 0; i < Entries.Length; i++)
				SepLen = Math.Max(SepLen, Entries[i].Length);

			SepLen += 3;
			GUIText[SepLen, 0] = 194;
			GUIText[SepLen, GUIText.BufferHeight - 1] = 193;
			for (int i = 1; i < GUIText.BufferHeight - 1; i++)
				GUIText[SepLen, i] = 179;

			string[] ASCII_LINES = ASCII.Replace("\r", "").Split('\n');

			

			for (int i = 0; i < ASCII_LINES.Length; i++)
				GUIText.Print(GUIText.BufferWidth - ASCII_LINES[i].Length - 2, i + 2, ASCII_LINES[i]);

			// Entries
			for (int i = 0; i < Entries.Length; i++)
				GUIText.Print(2, i + 1, Entries[i],
					i == Selected ? Color.Black : ConsoleColor.Gray.ToColor(), i == Selected ? Color.White : Color.Black);
		}
	}

	class MainMenu : State {
		TextBuffer GUIText;
		MenuEntry CurrentMenu;

		public MainMenu(RenderTexture RTex) {
			GUIText = new TextBuffer(80, 30);
			GUIText.SetFontTexture(ResourceMgr.Get<Texture>("font"));
			GUIText.Sprite.Scale = RTex.Size.ToVec2f().Divide(GUIText.Sprite.Texture.Size.ToVec2f());
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

			if (!Pressed) {
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