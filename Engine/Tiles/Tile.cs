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

		public byte BitMask;

		public override string ToString() {
			return string.Format("Id {0} (X {1}, Y {2}, BM {3})", Id, Tex_X, Tex_Y, BitMask);
		}


		public static bool IsEmpty(Tile Tile) {
			return Tile.Id == 0;
		}
	}
}
