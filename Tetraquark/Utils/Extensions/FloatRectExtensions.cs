using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SFML.System;
using SFML.Graphics;

namespace Tq {
	static class FloatRectExtensions {
		public static Vector2f GetSize(this FloatRect R) {
			return new Vector2f(R.Width, R.Height);
		}

		public static Vector2f GetPos(this FloatRect R) {
			return new Vector2f(R.Left, R.Top);
		}
	}
}
