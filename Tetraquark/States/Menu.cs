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
	class Menu : State {
		TextBuffer GUIText;

		MenuEntry CurrentMenu, MainMenu, LoadUniverse, Constants;

		public Menu(RenderTexture RTex) {
			GUIText = new TextBuffer(80, 30);
			GUIText.SetFontTexture(ResourceMgr.Get<Texture>("font"));
			GUIText.Sprite.Scale = RTex.Size.ToVec2f().Divide(GUIText.Sprite.Texture.Size.ToVec2f()) - new Vector2f(.1f, .1f);
			GUIText.Sprite.Position = RTex.Texture.Size.ToVec2f() / 2;
			GUIText.Sprite.Origin = GUIText.Sprite.Texture.Size.ToVec2f() / 2;

			MainMenu = new MenuEntry("Main Menu")
				.Add(new WidgetButton("Create Universe", () => {
				}))
				.Add(new WidgetButton("Load Universe", () => CurrentMenu = LoadUniverse))
				.Add(new WidgetButton("Universal Constants", () => CurrentMenu = Constants))
				.Add(new WidgetButton("Terminate", () => Program.Running = false));

			Action BackToMainMenu = () => {
				GUIText.Clear();
				CurrentMenu = MainMenu;
			};

			LoadUniverse = new MenuEntry("Load Universe")
				.Add(new WidgetButton("Back", BackToMainMenu));

			Constants = new MenuEntry("Universal Constants")
				.Add(new WidgetButton("Resolution", () => {
				}))
				.Add(new WidgetButton("Back", BackToMainMenu));

			CurrentMenu = MainMenu;
			CurrentMenu.DrawMenu(GUIText);
		}

		public override void OnKey(KeyEventArgs E, bool Pressed) {
			Widget ActiveWidget = null;

			if (E.Code == Keyboard.Key.Down) {
				if (Pressed)
					CurrentMenu.Selected++;
			} else if (E.Code == Keyboard.Key.Up) {
				if (Pressed)
					CurrentMenu.Selected--;
			} else if ((ActiveWidget = CurrentMenu.GetActiveWidget()) != null)
				ActiveWidget.OnKey(E.Code, Pressed);

			CurrentMenu.DrawMenu(GUIText);
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