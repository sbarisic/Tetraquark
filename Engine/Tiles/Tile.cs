using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetraquark2.Engine.Tiles {
	struct Tile {
		public int Id;

		public int Tex_X;
		public int Tex_Y;

		public static bool IsEmpty(Tile Tile) {
			return Tile.Id == 0;
		}
	}
}
