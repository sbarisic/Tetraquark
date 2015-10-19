using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ChipmunkSharp;
using SFML.System;

namespace Tq {
	static class FloatExtensions {
		public static float ToDeg(this float Rad) {
			return (float)(Rad * 180.0 / Math.PI);
		}

		public static float ToRad(this float Deg) {
			return (float)(Deg * Math.PI / 180.0);
		}
	}
}
