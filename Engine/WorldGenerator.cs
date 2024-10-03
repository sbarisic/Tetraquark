using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Tetraquark2.Engine.Tiles;

namespace Tetraquark2.Engine {
	static class WorldGenerator {
		static List<TileGenerator> TileGenerators;

		public static void Init() {
			TileGenerators = new List<TileGenerator>();

			TileGenerator Gen_Grass = new MultiTextureTileGenerator(1, "Grass", 20, 8);
			TileGenerators.Add(Gen_Grass);

			TileGenerator Gen_StoneWall = new BitMaskedTileGenerator(2, "Stone", 2);
			TileGenerators.Add(Gen_StoneWall);

			TileGenerator Gen_Sand = new MultiTextureTileGenerator(3, "Sand", 30, 8);
			TileGenerators.Add(Gen_Sand);
		}

		public static TileGenerator GetGenerator(int TileID) {
			for (int i = 0; i < TileGenerators.Count; i++) {
				if (TileGenerators[i].TileID == TileID)
					return TileGenerators[i];
			}

			return null;
		}

		static byte CalculateBitMask(Tile N, Tile W, Tile Center, Tile E, Tile S) {
			byte Mask = 0;
			int ID = Center.Id;

			if (N.Id == ID)
				Mask = (byte)(Mask | (1 << 0));

			if (W.Id == ID)
				Mask = (byte)(Mask | (1 << 1));

			if (E.Id == ID)
				Mask = (byte)(Mask | (1 << 2));

			if (S.Id == ID)
				Mask = (byte)(Mask | (1 << 3));

			return Mask;
		}

		static void GenerateTexture(TileChunk Chunk, ref Tile T) {
			if (T.Id == 0)
				return;

			TileGenerator G = GetGenerator(T.Id);
			if (G == null)
				return;

			G.GenerateTexture(Chunk, ref T);
		}

		public static void Generate(TileChunk Chunk) {
			string[][] DatMapFloor = File.ReadAllLines("data/test_map_floor.dat").Select(L => L.ToArray().Select(C => C.ToString()).ToArray()).ToArray();
			string[][] DatMap = File.ReadAllLines("data/test_map.dat").Select(L => L.ToArray().Select(C => C.ToString()).ToArray()).ToArray();

			for (int Y = 0; Y < Chunk.Height; Y++) {
				for (int X = 0; X < Chunk.Width; X++) {
					int MapEntry = int.Parse(DatMap[Y][X]);

					Block B = new Block();
					B.Floor.Id = int.Parse(DatMapFloor[Y][X]) + 1;

					if (MapEntry != 0)
						B.Wall.Id = MapEntry + 1;

					Chunk.SetBlock(X, Y, B);
				}
			}


			for (int Y = 0; Y < Chunk.Height; Y++) {
				for (int X = 0; X < Chunk.Width; X++) {
					ref Block N = ref Chunk.GetBlockRef(X, Y - 1);
					ref Block W = ref Chunk.GetBlockRef(X - 1, Y);
					ref Block Center = ref Chunk.GetBlockRef(X, Y);
					ref Block E = ref Chunk.GetBlockRef(X + 1, Y);
					ref Block S = ref Chunk.GetBlockRef(X, Y + 1);

					byte BitMask_Floor = CalculateBitMask(N.Floor, W.Floor, Center.Floor, E.Floor, S.Floor);
					byte BitMask_Wall = CalculateBitMask(N.Wall, W.Wall, Center.Wall, E.Wall, S.Wall);
					byte BitMask_Roof = CalculateBitMask(N.Roof, W.Roof, Center.Roof, E.Roof, S.Roof);

					Center.Floor.BitMask = BitMask_Floor;
					GenerateTexture(Chunk, ref Center.Floor);

					Center.Wall.BitMask = BitMask_Wall;
					GenerateTexture(Chunk, ref Center.Wall);

					Center.Roof.BitMask = BitMask_Roof;
					GenerateTexture(Chunk, ref Center.Roof);
				}
			}
		}
	}
}
