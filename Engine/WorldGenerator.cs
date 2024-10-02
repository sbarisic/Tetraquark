using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Tetraquark2.Engine.Tiles;

namespace Tetraquark2.Engine {
	static class WorldGenerator {
		static List<TileGenerator> TileGenerators;

		public static void Init() {
			TileGenerators = new List<TileGenerator>();
		}

		public static TileGenerator GetGenerator(int TileID) {
			for (int i = 0; i < TileGenerators.Count; i++) {
				if (TileGenerators[i].TileID == TileID)
					return TileGenerators[i];
			}

			return null;
		}

		public static void Generate(TileChunk Chunk) {

			for (int Y = 0; Y < Chunk.Height; Y++)
				for (int X = 0; X < Chunk.Width; X++) {

					Block B = Chunk.GetBlock(X, Y);
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
