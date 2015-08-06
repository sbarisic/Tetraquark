using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tq {
	static partial class Utils {
		static Random Rnd = new Random();

		public static int Random(int Max) {
			return Rnd.Next(Max);
		}

		public static int Random(int Min, int Max) {
			return Rnd.Next(Min, Max);
		}
	}
}
