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
		public static float GameTime {
			get {
				return (float)GameWatch.ElapsedMilliseconds / 1000;
			}
		}

		static Stopwatch GameWatch;

		[Setting]
		public static int ResX = (int)VideoMode.DesktopMode.Width - 100;
		[Setting]
		public static int ResY = (int)VideoMode.DesktopMode.Height - 100;
		[Setting]
		public static bool Debug = false;

		[STAThread]
		static void Main() {
			Console.Title = "Tetraquark Console";
			Running = true;

			const string ConfigFile = "cfg.yaml";
			if (File.Exists(ConfigFile)) {
				Console.WriteLine("Loading config file {0}", ConfigFile);
				Settings.Load(File.ReadAllText(ConfigFile));
			}

			GameWatch = new Stopwatch();
			GameWatch.Start();
			PackMgr.Mount("Fonts.zip");
			PackMgr.Mount("ConsoleFont.zip");

			string[] Files = PackMgr.GetFiles();
			for (int i = 0; i < Files.Length; i++) {
				if (Files[i].StartsWith("resources/fonts/"))
					ResourceMgr.Register<Font>(PackMgr.OpenFile(Files[i]), Path.GetFileNameWithoutExtension(Files[i]));
				else if (Files[i].StartsWith("resources/textures/"))
					ResourceMgr.Register<Texture>(PackMgr.OpenFile(Files[i]), Path.GetFileNameWithoutExtension(Files[i]));
			}

			/*ResX = 1280;
			ResY = 720;

			ResX = 960;
			ResY = 540;

			ResX = 864;
			ResY = 486;*/

			Scales.Init(new Vector2f(ResX, ResY));

			RenderWindow RWind = new RenderWindow(new VideoMode((uint)Scales.XRes, (uint)Scales.YRes),
				"Tetraquark", Styles.None);
			RWind.SetKeyRepeatEnabled(false);

			Renderer Rend = new Renderer(RWind);
			Stopwatch Clock = new Stopwatch();
			Clock.Start();

			while (Running) {
				RWind.DispatchEvents();
				while (Clock.ElapsedMilliseconds < 10)
					;
				float Dt = (float)Clock.ElapsedMilliseconds / 1000;
				Clock.Restart();
				Rend.Update(Dt);
				Rend.Draw(RWind);
				RWind.Display();
			}

			RWind.Close();
			Console.WriteLine("Flushing configs");
			File.WriteAllText(ConfigFile, Settings.Save());
			Environment.Exit(0);
		}
	}
}