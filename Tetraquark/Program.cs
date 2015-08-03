using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Threading;

using SFML.Graphics;
using SFML.System;
using SFML.Window;
using View = SFML.Graphics.View;

namespace Tq {
	static class Program {
		public static bool Running;
		public static bool Debug;

		[STAThread]
		static void Main() {
			Console.Title = "Tetraquark Console";
			Running = true;
			Debug = true;

			PackMgr.Mount("Fonts.zip");
			PackMgr.Mount("ConsoleFont.zip");

			string[] Files = PackMgr.GetFiles();
			for (int i = 0; i < Files.Length; i++) {
				if (Files[i].StartsWith("resources/fonts/"))
					ResourceMgr.Register<Font>(PackMgr.OpenFile(Files[i]), Path.GetFileNameWithoutExtension(Files[i]));
				else if (Files[i].StartsWith("resources/textures/"))
					ResourceMgr.Register<Texture>(PackMgr.OpenFile(Files[i]), Path.GetFileNameWithoutExtension(Files[i]));
			}

			var Bounds = new Vector2f();
			VideoMode Desktop = VideoMode.DesktopMode;
			//Bounds = new Vector2f(Desktop.Width - 50, Desktop.Height - 50);
			Bounds = new Vector2f(864, 486);
			//Bounds = new Vector2f(960, 540);
			//Bounds = new Vector2f(1280, 720);
			Scales.Init(Bounds);

		

			RenderWindow RWind = new RenderWindow(new VideoMode((uint)Scales.XRes, (uint)Scales.YRes),
				"Tetraquark", Styles.None);
			Renderer Rend = new Renderer(RWind);
			Clock C = new Clock();

			while (Running) {
				RWind.DispatchEvents();
				while (C.ElapsedTime.AsSeconds() < 1.0f / 60)
					;
				Rend.Update(C.Restart().AsSeconds());
				Rend.Draw(RWind);
				RWind.Display();
			}

			Environment.Exit(0);
		}
	}
}