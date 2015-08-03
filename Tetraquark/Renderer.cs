using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using SFML.System;
using SFML.Graphics;
using SFML.Window;

namespace Tq {
	class Renderer : Atomic.EventHandler {
		RenderTexture RTex;
		Sprite RTexSprite;

		Text FPSLabel;
		Graphics.TextBuffer TBuffer;

		public Renderer(Window Wind)
			: base(Wind) {
			RTex = new RenderTexture((uint)Scales.XRes, (uint)Scales.YRes);
			RTexSprite = new Sprite(RTex.Texture);

			FPSLabel = new Text("FPS: Unknown", ResourceMgr.Get<Font>("Inconsolata"),
				Scales.GetFontSize(16));
			FPSLabel.Position = new Vector2f(0.01f * Scales.XRes, 0);

			TBuffer = new Graphics.TextBuffer(80, 25);
			TBuffer.SetFontTexture(ResourceMgr.Get<Texture>("font")); // Load font
			/*TBuffer.Sprite.Origin = TBuffer.Sprite.Texture.Size.ToVec2f() / 2;
			TBuffer.Sprite.Position = RTex.Size.ToVec2f() / 2;
			TBuffer.Sprite.Rotation = 16;*/

			TBuffer.Sprite.Scale = RTex.Size.ToVec2f().Divide(TBuffer.Sprite.Texture.Size.ToVec2f());

			for (int i = 1; i < TBuffer.BufferWidth - 1; i++) {
				TBuffer[i, 0] = new Graphics.TextBufferEntry((char)196, Color.White, Color.Black);
				TBuffer[i, TBuffer.BufferHeight - 1] = new Graphics.TextBufferEntry((char)196, Color.White, Color.Black);
			}
			for (int i = 1; i < TBuffer.BufferHeight - 1; i++) {
				TBuffer[0, i] = new Graphics.TextBufferEntry((char)179, Color.White, Color.Black);
				TBuffer[TBuffer.BufferWidth - 1, i] = new Graphics.TextBufferEntry((char)179, Color.White, Color.Black);
				for (int j = 0; j < 11; j++)
					TBuffer.Print(22 + j * 5, i, Rnd.Next(1000, 9999).ToString(), Color.Black, Color.White);
			}
			for (int i = 0; i < TBuffer.BufferHeight - 1; i++)
				TBuffer[20, i] = (char)179;

			TBuffer[0, 0] = (char)218;
			TBuffer[TBuffer.BufferWidth - 1, 0] = (char)191;
			TBuffer[TBuffer.BufferWidth - 1, TBuffer.BufferHeight - 1] = (char)217;
			TBuffer[0, TBuffer.BufferHeight - 1] = (char)192;
			TBuffer[20, 0] = (char)194;
			TBuffer[20, TBuffer.BufferHeight - 1] = (char)193;

			TBuffer.Print(2, "Debug Window");
			TBuffer.Print(2, 1, "[ INVALID DATA ]", Color.Black, Color.White);
			for (int i = 2; i < TBuffer.BufferHeight - 1; i++)
				TBuffer.Print(2, i, "ERROR", Color.White, Color.Black);
		}

		public override bool OnKey(KeyEventArgs E, bool Down) {
			if (!base.OnKey(E, Down) && E.Code == Keyboard.Key.Escape && Down)
				Program.Running = false;
			return false;
		}

		Random Rnd = new Random();
		float Next = 0;

		public void Update(float Dt) {
			if (Program.Debug)
				FPSLabel.DisplayedString = "Fps: " + Math.Round(1.0f / Dt, 1) + "; Ms: " + Math.Round(Dt, 2);

			/*if (Program.GameTime > Next) {
				Next = Program.GameTime + 1;
				for (int i = 0; i < TBuffer.BufferWidth * TBuffer.BufferHeight; i++)
					TBuffer[i] = new Graphics.TextBufferEntry((char)Rnd.Next(255),
						new Color((byte)Rnd.Next(255), (byte)Rnd.Next(255), (byte)Rnd.Next(255)),
						new Color((byte)Rnd.Next(255), (byte)Rnd.Next(255), (byte)Rnd.Next(255)));
			}*/
		}

		public void Draw(RenderTarget RT) {
			RT.Clear(Color.Black);
			RTex.Clear(new Color(20, 20, 20));

			Shader.Bind(Shaders.CRT);
			Shaders.CRT.SetParameter("texture", TBuffer.Sprite.Texture);
			//Shaders.CRT.SetParameter("time", Program.GameTime);
			Shaders.CRT.SetParameter("resolution", TBuffer.Sprite.Texture.Size.ToVec2f());
			RTex.Draw(TBuffer);
			Shader.Bind(null);
			if (Program.Debug)
				RTex.Draw(FPSLabel);

			RTex.Display();
			RT.Draw(RTexSprite);
		}
	}
}