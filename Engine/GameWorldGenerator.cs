using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetraquark2.Engine {
	static class GameWorldGenerator {

		public static void Generate(GameTileChunk Chunk) {

			for (int Y = 0; Y < Chunk.Height; Y++)
				for (int X = 0; X < Chunk.Width; X++) {

					GameBlock B = Chunk.GetBlock(X, Y);
					B.Floor.Id = 1;

					if (X == 3 && Y == 3) {
						B.Wall.Id = 2;
						B.Wall.Tex_X = 1;
						B.Wall.Tex_Y = 1;
					}

					Chunk.SetBlock(X, Y, B);
				}

		}
	}
}
