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
using Tq.Menu;

namespace Tq.States {
	class MenuState : State {
		TextBuffer GUIText;

		MenuEntry CurrentMenu, MainMenu, LoadUniverse, Constants;

		public MenuState(RenderTexture RTex) {
			GUIText = new TextBuffer(80, 30);
			GUIText.SetFontTexture(ResourceMgr.Get<Texture>("font"));
			GUIText.Sprite.Scale = RTex.Size.ToVec2f().Divide(GUIText.Sprite.Texture.Size.ToVec2f());
			GUIText.Sprite.Position = RTex.Texture.Size.ToVec2f() / 2;
			GUIText.Sprite.Origin = GUIText.Sprite.Texture.Size.ToVec2f() / 2;

			MainMenu = new MenuEntry("Main Menu")
				.Add(new WidgetButton("Create Universe", () => {
				}))
				.Add(new WidgetButton("Load Universe", () => CurrentMenu = LoadUniverse).Disable())
				.Add(new WidgetButton("Universal Constants", () => CurrentMenu = Constants))
				.Add(new WidgetButton("Terminate", () => Program.Running = false));

			Action BackToMainMenu = () => {
				GUIText.Clear();
				CurrentMenu = MainMenu;
			};

			LoadUniverse = new MenuEntry("Load Universe")
				.Add(new WidgetButton("Back", BackToMainMenu));

			Constants = new MenuEntry("Universal Constants")
				.Add(new WidgetTextbox("Resolution X", Program.ResX.ToString(), 6, (S) => {
					if (S.Length > 0)
						Program.ResX = int.Parse(S);
					GUIText.Print(0, 0, "Restart required for changes to take effect");
				}, char.IsNumber))
				.Add(new WidgetTextbox("Resolution Y", Program.ResY.ToString(), 6, (S) => {
					if (S.Length > 0)
						Program.ResY = int.Parse(S);
					GUIText.Print(0, 0, "Restart required for changes to take effect");
				}, char.IsNumber))
				.Add(new WidgetCheckbox("Debug", Program.Debug, (B) => Program.Debug = B))
				.Add(new WidgetButton("Back", BackToMainMenu));

			CurrentMenu = MainMenu;
			CurrentMenu.DrawMenu(GUIText);
		}

		public override void OnKey(KeyEventArgs E, bool Pressed) {
			Widget Selected = CurrentMenu.GetSelectedWidget();

			if (Selected != null && Selected.CaptureInput)
				Selected.OnKey(E.Code, Pressed);
			else if (E.Code == Keyboard.Key.Down) {
				if (Pressed)
					CurrentMenu.Selected++;
			} else if (E.Code == Keyboard.Key.Up) {
				if (Pressed)
					CurrentMenu.Selected--;
			} else if (Selected != null)
				Selected.OnKey(E.Code, Pressed);

			CurrentMenu.DrawMenu(GUIText);
		}

		public override void OnTextEntered(TextEventArgs E) {
			Widget Selected = CurrentMenu.GetSelectedWidget();
			if (Selected != null && Selected.CaptureInput)
				Selected.OnText(E.Unicode[0]);
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