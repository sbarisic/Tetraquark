using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetraquark2.Engine.Tiles {
	class TileGenerator {
		public int TileID;
		public string Name;

		public TileGenerator(int ID, string Name) {
			TileID = ID;
			this.Name = Name;
		}

		public virtual void GenerateTexture(TileChunk Chunk, ref Tile T) {
			Chunk.GetTileTexturePos(TileID, out T.Tex_X, out T.Tex_Y);
		}
	}

	class BitMaskedTileGenerator : TileGenerator {
		public int TextureStartIndex;

		public BitMaskedTileGenerator(int ID, string Name, int TextureStartIndex) : base(ID, Name) {
			this.TextureStartIndex = TextureStartIndex;
		}

		public override void GenerateTexture(TileChunk Chunk, ref Tile T) {
			int TexIdx = TextureStartIndex + T.BitMask;
			Chunk.GetTileTexturePos(TexIdx, out T.Tex_X, out T.Tex_Y);
		}
	}

	class MultiTextureTileGenerator : TileGenerator {
		Random Rnd = new Random();

		public int TextureStartIndex;
		public int TextureCount;

		public MultiTextureTileGenerator(int ID, string Name, int TextureStartIndex, int TextureCount) : base(ID, Name) {
			this.TextureStartIndex = TextureStartIndex;
			this.TextureCount = TextureCount;
		}

		public override void GenerateTexture(TileChunk Chunk, ref Tile T) {
			int TexID = TextureStartIndex + Rnd.Next(0, TextureCount);
			Chunk.GetTileTexturePos(TexID, out T.Tex_X, out T.Tex_Y);
		}
	}
}
