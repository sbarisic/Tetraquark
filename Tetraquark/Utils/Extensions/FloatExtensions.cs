using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SFML.System;
using FarseerPhysics;

namespace Tq {
	static class FloatExtensions {
		public static float ToDeg(this float Rad) {
			return (float)(Rad * 180.0 / Math.PI);
		}

		public static float ToRad(this float Deg) {
			return (float)(Deg * Math.PI / 180.0);
		}

		public static float ToSimUnits(this float F) {
			return ConvertUnits.ToSimUnits(F);
		}

		public static float ToDisplayUnits(this float F) {
			return ConvertUnits.ToDisplayUnits(F);
		}
	}
}
