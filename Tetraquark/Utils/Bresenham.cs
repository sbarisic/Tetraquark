using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tq {
	static partial class Utils {
		public static void BresenLine(Action<int, int> OnLine, int X0, int Y0, int X1, int Y1) {
			int DX = System.Math.Abs(X1 - X0), SX = X0 < X1 ? 1 : -1;
			int DY = System.Math.Abs(Y1 - Y0), SY = Y0 < Y1 ? 1 : -1;
			int Err = (DX > DY ? DX : -DY) / 2, E2;

			for (; ; ) {
				OnLine(X0, Y0);
				if (X0 == X1 && Y0 == Y1)
					break;
				E2 = Err;
				if (E2 > -DX) {
					Err -= DY;
					X0 += SX;
				}
				if (E2 < DY) {
					Err += DX;
					Y0 += SY;
				}
			}
		}
	}
}