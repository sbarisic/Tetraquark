using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Threading;

using SFML.Graphics;
using SFML.System;
using View = SFML.Graphics.View;

namespace Tq {
	static class Program {
		public static bool Running;

		[STAThread]
		static void Main() {
			Console.Title = "Tetraquark Console";
			Running = true;

			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

			PackMgr.Mount("Fonts.zip");

			string[] Files = PackMgr.GetFiles();
			for (int i = 0; i < Files.Length; i++) {
				if (Files[i].StartsWith("resources/fonts/"))
					ResourceMgr.Register<Font>(PackMgr.OpenFile(Files[i]), Path.GetFileNameWithoutExtension(Files[i]));
			}

			TqWind Wind = new TqWind();
			Wind.Show();
			RenderWindow RWind = new RenderWindow(Wind.Handle);
			Clock C = new Clock();
			
			while (Running) {
				Application.DoEvents();
				while (C.ElapsedTime.AsSeconds() < 1.0f / 60)
					;
				Wind.Update(C.Restart().AsSeconds());
				Wind.Draw(RWind);
				RWind.Display();
			}

			Environment.Exit(0);
		}
	}
}