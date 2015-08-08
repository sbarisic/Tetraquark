using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SFML.Graphics;
using Tq.Graphics;

namespace Tq {
	static class TextBufferExtensions {
		public static void DrawBox(this TextBuffer TB, int X, int Y, int W, int H) {
			Action<int, int, bool, bool, bool, bool> Put = (XX, YY, L, R, U, D) => {
				if (XX - 1 >= 0 && new[] { (char)197, (char)192, (char)193, (char)194, (char)195,
					(char)196, (char)218 }.Contains(TB[XX - 1, YY]))
					L = true;
				if (XX + 1 < TB.BufferWidth && new[] { (char)197, (char)191, (char)217, (char)193,
					(char)194, (char)180, (char)196 }.Contains(TB[XX + 1, YY]))
					R = true;
				if (YY - 1 >= 0 && new[] { (char)197, (char)179, (char)180, (char)191, (char)194,
					(char)195, (char)218 }.Contains(TB[XX, YY - 1]))
					U = true;
				if (YY + 1 < TB.BufferHeight && new[] { (char)197, (char)192, (char)193, (char)217,
					(char)179, (char)180, (char)195 }.Contains(TB[XX, YY + 1]))
					D = true;

				if ((L || R) && !U && !D)
					TB[XX, YY] = 196;
				else if (!L && !R && (U || D))
					TB[XX, YY] = 179;
				else if (L && R && U && D)
					TB[XX, YY] = 197;
				else if (L && R && U && !D)
					TB[XX, YY] = 193;
				else if (L && R && !U && D)
					TB[XX, YY] = 194;
				else if (L && !R && U && D)
					TB[XX, YY] = 180;
				else if (L && !R && U && !D)
					TB[XX, YY] = 217;
				else if (L && !R && !U && D)
					TB[XX, YY] = 191;
				else if (!L && R && U && D)
					TB[XX, YY] = 195;
				else if (!L && R && U && !D)
					TB[XX, YY] = 192;
				else if (!L && R && !U && D)
					TB[XX, YY] = 218;
				else if (!L && !R && !U && !D)
					TB[XX, YY] = '*';
				else
					TB[XX, YY] = '#';
			};

			if (W == 0) {
				Put(X, Y, false, false, true, true);
				Put(X, Y + H, false, false, true, true);
			} else if (H == 0) {
				Put(X, Y, true, true, false, false);
				Put(X + W, Y, true, true, false, false);
			} else {
				Put(X, Y, false, true, false, true);
				Put(X + W, Y, true, false, false, true);
				Put(X + W, Y + H, true, false, true, false);
				Put(X, Y + H, false, true, true, false);
			}

			for (int x = 1; x < W; x++) {
				Put(X + x, Y, true, true, false, false);
				Put(X + x, Y + H, true, true, false, false);
			}
			for (int y = 1; y < H; y++) {
				Put(X, Y + y, false, false, true, true);
				Put(X + W, Y + y, false, false, true, true);
			}
		}
	}
}
