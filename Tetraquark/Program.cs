using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

using SFML.Graphics;
using SFML.Window;

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
			RenderWindow RWind = new RenderWindow(Wind.Handle);
			Wind.Show();

			Text T = new Text("The slow red fox doesn't jump over the hyperactive dog",
				ResourceMgr.Get<Font>("Inconsolata"), 16);
			T.Position = new SFML.System.Vector2f(20, 80);

			Wind.KeyUp += (S, E) => {
				if (E.KeyCode == Keys.P)
					RWind.Capture().SaveToFile("test.png");
			};

			while (Running) {
				RWind.Clear(Color.Black);
				RWind.Draw(T);

				RWind.Display();

				Application.DoEvents();
			}

			/*Console.WriteLine("Done!");
			Console.ReadLine();*/
			Environment.Exit(0);
		}
	}
}