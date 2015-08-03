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
			TBuffer.Sprite.Position = new Vector2f(TBuffer.CharWidth, TBuffer.CharHeight);
		}

		public override bool OnKey(KeyEventArgs E, bool Down) {
			if (!base.OnKey(E, Down) && E.Code == Keyboard.Key.Escape && Down)
				Program.Running = false;
			return false;
		}

		Random Rnd = new Random();

		public void Update(float Dt) {
			if (Program.Debug)
				FPSLabel.DisplayedString = "Fps: " + Math.Round(1.0f / Dt, 1) + "; Ms: " + Math.Round(Dt, 2);
			
			for (int i = 0; i < TBuffer.BufferWidth * TBuffer.BufferHeight; i++)
				TBuffer.Set(i, (char)Rnd.Next(255), new Color((byte)Rnd.Next(255), (byte)Rnd.Next(255), (byte)Rnd.Next(255)),
					new Color((byte)Rnd.Next(255), (byte)Rnd.Next(255), (byte)Rnd.Next(255)));
		}

		public void Draw(RenderTarget RT) {
			RT.Clear(Color.Black);
			RTex.Clear(new Color(20, 20, 20));

			RTex.Draw(TBuffer);
			if (Program.Debug)
				RTex.Draw(FPSLabel);

			RTex.Display();
			RT.Draw(RTexSprite);
		}
	}
}