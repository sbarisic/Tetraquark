using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SFML.Graphics;

namespace Tq {
	static class ColorExtensions {

		public static Color ToColor(this ConsoleColor Clr) {
			switch (Clr) {
				case ConsoleColor.Black:
					return Color.Black;
				case ConsoleColor.Blue:
					return Color.Blue;
				case ConsoleColor.Cyan:
					return Color.Cyan;
				case ConsoleColor.DarkBlue:
					return new Color(0, 0, 128);
				case ConsoleColor.DarkCyan:
					return new Color(0, 128, 128);
				case ConsoleColor.DarkGray:
					return new Color(128, 128, 128);
				case ConsoleColor.DarkGreen:
					return new Color(0, 128, 0);
				case ConsoleColor.DarkMagenta:
					return new Color(128, 0, 128);
				case ConsoleColor.DarkRed:
					return new Color(128, 0, 0);
				case ConsoleColor.DarkYellow:
					return new Color(128, 128, 0);
				case ConsoleColor.Gray:
					return new Color(192, 192, 192);
				case ConsoleColor.Green:
					return Color.Green;
				case ConsoleColor.Magenta:
					return Color.Magenta;
				case ConsoleColor.Red:
					return Color.Red;
				case ConsoleColor.White:
					return Color.White;
				case ConsoleColor.Yellow:
					return Color.Yellow;
			}
			return Color.Magenta;
		}
	}
}
