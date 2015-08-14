using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SFML.Graphics;
using SFML.System;

namespace Tq.Graphics {
	static class VertexShapes {
		public static Vertex[] Quad;

		static VertexShapes() {
			Quad = new Vertex[] {
				new Vertex(new Vector2f(0, 0), Color.White, new Vector2f(0, 1)), 
				new Vertex(new Vector2f(1, 0), Color.White, new Vector2f(1, 1)),
				new Vertex(new Vector2f(1, 1), Color.White, new Vector2f(1, 0)),
				new Vertex(new Vector2f(0, 1), Color.White, new Vector2f(0, 0)),
			};
		}
	}
}