using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SFML.Graphics;
using SFML.System;

namespace Tq {
	static class TextExtensions {
		public static float GetHeight(this Text T) {
			return T.GetGlobalBounds().Height * 2;
		}

		public static Vector2f GetSize(this Text T) {
			Vector2f Size = T.GetGlobalBounds().GetSize();
			return new Vector2f(Size.X + 2, Size.Y * 2);
		}
	}
}