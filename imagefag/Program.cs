using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace imagefag {
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	struct RECT {
		public int X1;
		public int Y1;
		public int X2;
		public int Y2;
	}

	unsafe class Program {
		[DllImport("user32.dll", SetLastError = false)]
		static extern IntPtr GetDesktopWindow();

		[DllImport("user32.dll")]
		static extern IntPtr GetShellWindow();

		[DllImport("kernel32.dll", SetLastError = true)]
		static extern bool AttachConsole(int dwProcessId);

		[DllImport("kernel32.dll")]
		static extern IntPtr GetConsoleWindow();

		[DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
		static extern bool FreeConsole();

		[DllImport("user32.dll")]
		static extern bool InvalidateRect(IntPtr hWnd, IntPtr lpRect, bool bErase);

		static IntPtr GetConsole() {
			IntPtr H = GetConsoleWindow();
			if (H == IntPtr.Zero) {
				AttachConsole(-1);
				H = GetConsoleWindow();
				FreeConsole();
			}
			return H;
		}

		static void Main(string[] args) {
			IntPtr Con = GetConsole();
			Console.WriteLine("Console: {0}", Con);

			Font F = new Font("Inconsolata.otf");
			Text GL = new Text(args[0], F, 36);
			GL.Position = new SFML.System.Vector2f(0, 50);
			GL.Color = new Color(byte.Parse(args[1]), byte.Parse(args[2]), byte.Parse(args[3]));


			Console.WriteLine("\n\n\n");
			DrawText(Con, GL);

			RECT R = new RECT();
			R.X1 = 0;
			R.Y1 = 0;
			R.X2 = 1000;
			R.Y2 = 50;
			InvalidateRect(Con, new IntPtr(&R), true);

			/*Console.WriteLine("Done!");
			Console.ReadLine();*/
		}

		static void DrawText(IntPtr Handle, Text T) {
			RenderWindow RWind = new RenderWindow(Handle);
			RWind.Draw(T);
			RWind.Display();
		}
	}
}