using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO;

using OpenTK.Graphics.OpenGL;
using SFML.System;
using SFML.Graphics;
using SFML.Window;
using PrimType = SFML.Graphics.PrimitiveType;
using Tq.Graphics;
using Tq.Menu;

namespace Tq.States {
	class MenuState : State {
		TextBuffer GUIText;
		MenuEntry CurrentMenu, MainMenu, LoadUniverse, Constants;
		RenderSprite CRT, AAA, BBB;

		public MenuState(RenderSprite RTex) {
			GUIText = new TextBuffer(80, 30);
			GUIText.SetFontTexture(ResourceMgr.Get<Texture>("font"));
			GUIText.Sprite.Scale = RTex.Size.ToVec2f().Divide(GUIText.Sprite.Texture.Size.ToVec2f());
			GUIText.Sprite.Position = RTex.Texture.Size.ToVec2f() / 2;
			GUIText.Sprite.Origin = GUIText.Sprite.Texture.Size.ToVec2f() / 2;

			AAA = new RenderSprite(RTex.Size);
			BBB = new RenderSprite(RTex.Size);
			CRT = new RenderSprite(RTex.Size);

			MainMenu = new MenuEntry("Main Menu")
				.Add(new WidgetButton("Create Universe", () => Renderer.PushState(new GameState(RTex))))
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
							.Add(new WidgetCheckbox("Border", Program.Border, (B) => Program.Border = B))
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

		public override void Draw(RenderSprite RT) {
			RT.Clear(Color.Black);

			CRT.Clear(Color.Transparent);
			CRT.Draw(GUIText, Shaders.UseCRT(GUIText.Sprite.Texture, 0.1f, 0.8f, 0.9f, 0.015f));
			CRT.Display();

			RT.Draw(Shaders.DrawPhosphorGlow(CRT.Texture));
			/*BBB.Clear(Color.Transparent);
			BBB.Draw(AAA);
			BBB.Display();

			AAA.Clear(Color.Transparent);
			AAA.Draw(VertexShapes.Quad, PrimType.Quads,
				Shaders.UsePhosphorGlow(BBB.Texture, CRT.Texture, 0.08f).Scale(RT.Size.ToVec2f()));
			AAA.Display();
			
			RT.Draw(AAA);*/
		}
	}
}