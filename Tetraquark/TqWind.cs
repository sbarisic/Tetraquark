using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using SFML.System;
using SFML.Graphics;
using SFML.Window;

namespace Tq {
	public partial class TqWind : Form {
		Atomic.EventHandler AtomicEvents;
		RenderTexture RTex;
		Sprite RTexSprite;

		Text FPSLabel, GL;

		public TqWind() {
			InitializeComponent();
		}

		private void TqWind_Load(object sender, EventArgs e) {
			AtomicEvents = new Atomic.EventHandler();
			MouseDown += (S, E) =>
				AtomicEvents.OnMouse(E, true);
			MouseUp += (S, E) =>
				AtomicEvents.OnMouse(E, false);
			MouseClick += (S, E) =>
				AtomicEvents.OnMouseClick(E, false);
			MouseDoubleClick += (S, E) =>
				AtomicEvents.OnMouseClick(E, true);
			MouseMove += (S, E) => {
				if (Atomic.EventHandler.GrabbedHander != null)
					Atomic.EventHandler.GrabbedHander.OnMouseDrag(E);
				else
					AtomicEvents.OnMouseMove(E);
			};
			KeyDown += (S, E) => {
				AtomicEvents.OnKey(E, true);
				if (E.KeyCode == Keys.Escape)
					Close();
			};
			KeyUp += (S, E) =>
				AtomicEvents.OnKey(E, false);
			KeyPress += (S, E) =>
				AtomicEvents.OnKeyPress(E);

			//var Bounds = Screen.GetBounds(this);
			//var Bounds = new System.Drawing.Rectangle(0, 0, 864, 486);
			var Bounds = new System.Drawing.Rectangle(0, 0, 960, 540);
			//var Bounds = new System.Drawing.Rectangle(0, 0, 1280, 720);

			SetBounds(0, 0, Bounds.Width, Bounds.Height);
			CenterToScreen();

			Scales.Init(new Vector2f(Bounds.Width, Bounds.Height));
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
			P.SetParent(AtomicEvents);

			GUI.Panel P1 = new GUI.Panel(new Vector2f(10, 10), new Vector2u(400, 300));
			P1.SetParent(P);

			GUI.Panel P2 = new GUI.Panel(new Vector2f(10, 10), new Vector2u(300, 200));
			P2.SetParent(P1);

			GUI.Panel P3 = new GUI.Panel(new Vector2f(10, 10), new Vector2u(200, 100));
			P3.SetParent(P2);

			GUI.Panel P4 = new GUI.Panel(new Vector2f(10, 10), new Vector2u(100, 50));
			P4.SetParent(P3);
		}

		protected override void OnClosed(EventArgs e) {
			base.OnClosed(e);
			Program.Running = false;
		}

		public void Update(float Dt) {
			FPSLabel.DisplayedString = "FPS: " + Math.Round(1.0f / Dt, 1) + "; MS: " + Dt;
		}

		GUI.Panel P;

		public void Draw(RenderTarget RT) {
			RT.Clear(Color.Black);
			RTex.Clear(new Color(0, 76, 153));

			//RTex.Draw(GL);
			RTex.Draw(P);

			RTex.Draw(FPSLabel);
			RTex.Display();
			RT.Draw(RTexSprite);
		}
	}
}