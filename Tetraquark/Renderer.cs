using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using SFML.System;
using SFML.Graphics;
using SFML.Window;
using Tq.States;
using Tq.Graphics;

namespace Tq {
	static class Renderer {
		static RenderSprite RTgt;

		static Text FPSLabel;
		static Stack<State> States;

		public static void Init(Window Wind) {
			States = new Stack<State>();
			RTgt = new RenderSprite((int)Scales.XRes, (int)Scales.YRes);
			FPSLabel = new Text("FPS: Unknown", ResourceMgr.Get<Font>("Inconsolata"), Scales.GetFontSize(16));
			FPSLabel.Position = new Vector2f(0.01f * Scales.XRes, 0);

			Wind.Closed += (S, E) => {
				if (States.Count > 0)
					States.Peek().OnClosed();
			};

			Wind.GainedFocus += (S, E) => {
				if (States.Count > 0)
					States.Peek().OnGainedFocus();
			};

			Wind.LostFocus += (S, E) => {
				if (States.Count > 0)
					States.Peek().OnLostFocus();
			};

			Wind.KeyPressed += (S, E) => {
				if (E.Alt && E.Code == Keyboard.Key.F4) {
					Program.Running = false;
					return;
				}
				if (States.Count > 0)
					States.Peek().OnKey(E, true);
			};

			Wind.KeyReleased += (S, E) => {
				if (States.Count > 0)
					States.Peek().OnKey(E, false);
			};

			Wind.TextEntered += (S, E) => {
				if (States.Count > 0)
					States.Peek().OnTextEntered(E);
			};

			Wind.MouseButtonPressed += (S, E) => {
				if (States.Count > 0)
					States.Peek().OnMouseButton(E, true);
			};

			Wind.MouseButtonReleased += (S, E) => {
				if (States.Count > 0)
					States.Peek().OnMouseButton(E, false);
			};

			Wind.MouseEntered += (S, E) => {
				if (States.Count > 0)
					States.Peek().OnMouseEntered(true);
			};

			Wind.MouseLeft += (S, E) => {
				if (States.Count > 0)
					States.Peek().OnMouseEntered(false);
			};

			Wind.MouseMoved += (S, E) => {
				if (States.Count > 0)
					States.Peek().OnMouseMoved(E);
			};

			Wind.MouseWheelMoved += (S, E) => {
				if (States.Count > 0)
					States.Peek().OnMouseWheelMoved(E);
			};

			Wind.Resized += (S, E) => {
				if (States.Count > 0)
					States.Peek().OnResized(E);
			};

			PushState(new MenuState(RTgt));
		}

		public static void PushState(State State) {
			States.Push(State);
		}

		public static State PopState() {
			return States.Pop();
		}

		public static void Update(float Dt) {
			if (Program.Debug)
				FPSLabel.DisplayedString = "Fps: " + Math.Round(1.0f / Dt, 1) + "; Ms: " + Math.Round(Dt, 2);

			if (States.Count > 0)
				States.Peek().Update(Dt);
		}

		public static void Draw(RenderTarget RT) {
			if (States.Count > 0)
				States.Peek().Draw(RTgt);
			RTgt.Display();

			RT.Clear(Color.Transparent);
			RT.Draw(RTgt);
			if (Program.Debug)
				RT.Draw(FPSLabel);
		}
	}
}