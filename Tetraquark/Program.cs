using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

using SFML.Graphics;
using SFML.Window;

namespace Tetraquark {
	static class Program {
		public static bool Running;

		[STAThread]
		static void Main() {
			Console.Title = "Tetraquark Console";
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

			Running = true;

			TqWind Wind = new TqWind();
			RenderWindow RWind = new RenderWindow(Wind.Handle);
			Wind.Show();

			while (Running) {
				Application.DoEvents();
				RWind.Clear(Color.Black);

				RWind.Display();
			}

			Environment.Exit(0);
		}
	}
}