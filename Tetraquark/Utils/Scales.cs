using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SFML.System;

namespace Tq {
	static class Scales {
		public static void Init(Vector2f Resolution) {
			Res = Resolution;
			XRes = Res.X;
			YRes = Res.Y;
		}

		public static Vector2f Res {
			get;
			private set;
		}

		public static float XRes {
			get;
			private set;
		}

		public static float YRes {
			get;
			private set;
		}

		public static uint GetFontSize(float Scale) {
			return (uint)(Scale * YRes / 200);
		}
	}
}