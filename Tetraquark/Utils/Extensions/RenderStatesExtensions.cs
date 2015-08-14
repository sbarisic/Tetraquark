using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SFML.Graphics;
using SFML.System;

namespace Tq {
	static class RenderStatesExtensions {
		public static RenderStates Scale(this RenderStates RS, Vector2f S) {
			RS.Transform.Scale(S);
			return RS;
		}
	}
}
