using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SFML.System;
using SFML.Graphics;
using SFML.Window;

namespace Tq {
	class TqRenderer : Atomic.EventHandler {
		RenderTexture RTex;
		Sprite RTexSprite;

		Text FPSLabel, GL;

		public TqRenderer(Window Wind)
			: base(Wind) {
			RTex = new RenderTexture((uint)Scales.XRes, (uint)Scales.YRes);
			RTexSprite = new Sprite(RTex.Texture);

			FPSLabel = new Text("FPS: Unknown", ResourceMgr.Get<Font>("Inconsolata"),
				Scales.GetFontSize(7));
			FPSLabel.Position = new Vector2f(0.01f * Scales.XRes, 0);

			GL = new Text("OpenGL'd it, bitch.", ResourceMgr.Get<Font>("Inconsolata"),
				Scales.GetFontSize(38));

			GL.Rotation = 12;
			GL.Position = new Vector2f(0.5f * Scales.XRes, 0.5f * Scales.YRes);
			GL.Origin = GL.GetLocalBounds().GetSize() / 2;

			P = new GUI.Panel(new Vector2f(100, 100), new Vector2u(500, 400));
			P.SetParent(this);

			GUI.Panel P1 = new GUI.Panel(new Vector2f(10, 10), new Vector2u(400, 300));
			P1.SetParent(P);
		}

		public override bool OnKey(KeyEventArgs E, bool Down) {
			if (!base.OnKey(E, Down) && E.Code == Keyboard.Key.Escape && Down)
				Program.Running = false;
			return false;
		}

		public void Update(float Dt) {
			FPSLabel.DisplayedString = "Fps: " + Math.Round(1.0f / Dt, 1) + "; Ms: " + Math.Round(Dt, 2);
		}

		GUI.Panel P;

		public void Draw(RenderTarget RT) {
			RT.Clear(Color.Black);
			RTex.Clear(new Color(0, 76, 153));

			RTex.Draw(P);

			RTex.Draw(FPSLabel);
			RTex.Display();
			RT.Draw(RTexSprite);
		}
	}
}