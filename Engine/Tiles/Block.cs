using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetraquark2.Engine.Tiles {
	struct Block {
		public static readonly Block Empty = new Block();

		public Tile Floor;
		public Tile Wall;
		public Tile Roof;

		public bool IsEmpty() {
			if (Tile.IsEmpty(Floor) && Tile.IsEmpty(Wall) && Tile.IsEmpty(Roof))
				return true;

			return false;
		}

		public override string ToString() {
			return string.Format("F: {0}; W: {1}; R: {2}", Floor.ToString(), Wall.ToString(), Roof.ToString());
		}
	}
}
