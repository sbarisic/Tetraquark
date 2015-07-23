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

		Text FPSLabel;

		public TqRenderer(Window Wind)
			: base(Wind) {
			RTex = new RenderTexture((uint)Scales.XRes, (uint)Scales.YRes);
			RTexSprite = new Sprite(RTex.Texture);

			FPSLabel = new Text("FPS: Unknown", ResourceMgr.Get<Font>("Inconsolata"),
				Scales.GetFontSize(16));
			FPSLabel.Position = new Vector2f(0.01f * Scales.XRes, 0);

			Wnd = new GUI.Window(new Vector2f(Scales.XRes * 0.1f, Scales.YRes * 0.1f),
				new Vector2u((uint)(Scales.XRes * 0.8f), (uint)(Scales.YRes * 0.8f)),
				ResourceMgr.Get<Font>("Inconsolata"), "AAAAAAAAAAAAAAAA");
			Wnd.SetParent(this);

			GUI.Window Wind1 = new GUI.Window(new Vector2f(Scales.XRes * 0.01f, Scales.YRes * 0.01f),
				new Vector2u((uint)(Scales.XRes * 0.4f), (uint)(Scales.YRes * 0.4f)),
				ResourceMgr.Get<Font>("Inconsolata"), "One");
			Wind1.SetParent(Wnd.ClientArea);
			Wind1.Disable();

			GUI.Window Wind2 = new GUI.Window(new Vector2f(Scales.XRes * 0.05f, Scales.YRes * 0.05f),
				new Vector2u((uint)(Scales.XRes * 0.4f), (uint)(Scales.YRes * 0.4f)),
				ResourceMgr.Get<Font>("Inconsolata"), "Two");
			Wind2.SetParent(Wnd.ClientArea);
		}

		public override bool OnKey(KeyEventArgs E, bool Down) {
			if (!base.OnKey(E, Down) && E.Code == Keyboard.Key.Escape && Down)
				Program.Running = false;
			return false;
		}

		public void Update(float Dt) {
			FPSLabel.DisplayedString = "Fps: " + Math.Round(1.0f / Dt, 1) + "; Ms: " + Math.Round(Dt, 2);
		}

		GUI.Window Wnd;

		public void Draw(RenderTarget RT) {
			RT.Clear(Color.Black);
			RTex.Clear(new Color(20, 20, 20));

			RTex.Draw(Wnd);
			RTex.Draw(FPSLabel);
			RTex.Display();
			RT.Draw(RTexSprite);
		}
	}
}