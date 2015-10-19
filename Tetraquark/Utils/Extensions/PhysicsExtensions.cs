using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Physics;
using SFML.System;

namespace Tq {
	static class UnitExtensions {
		public static Quantity Quant(this Unit U, double Amount) {
			return new Quantity(Amount, U);
		}
	}

	static class QuantityExtensions {
		public static float ToPixels(this Quantity Q) {
			return (float)Q.Convert(Game.SI.px).Amount;
		}

		public static float ToMeters(this Quantity Q) {
			return (float)Q.Convert(Game.SI.m).Amount;
		}
	}
}