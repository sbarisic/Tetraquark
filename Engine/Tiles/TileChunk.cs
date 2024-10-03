using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

using Tetraquark2.Gfx;

namespace Tetraquark2.Engine.Tiles {
	class TileChunk {
		public int TileWidth;
		public int TileHeight;
		public int Width;
		public int Height;

		public int ChunkX;
		public int ChunkY;

		Block EmptyBlock = new Block();

		bool Dirty;
		public Block[] Tiles;
		public GfxRenderTexture ChunkTexture;

		public GfxTexture TilesetTexture;
		public GfxTexture BlendMaskTexture;

		public TileChunk(int TileWidth, int TileHeight, int Width, int Height) {
			this.Width = Width;
			this.Height = Height;
			this.TileWidth = TileWidth;
			this.TileHeight = TileHeight;

			Tiles = new Block[Width * Height];

			ChunkTexture = new GfxRenderTexture(TileWidth * Width, TileHeight * Height);

			Block Blk = GetBlock(3, 2);
			Blk.Wall.Tex_X = 1;
			Blk.Wall.Tex_Y = 0;

			SetBlock(3, 2, Blk);

			Dirty = true;
		}

		public void GetTileTexturePos(int TileIndex, out int X, out int Y) {
			int TilesCountX = TilesetTexture.Width / TileWidth;

			X = TileIndex % TilesCountX;
			Y = TileIndex / TilesCountX;
		}

		bool RangeValid(int X, int Y) {
			if (X < 0 || Y < 0 || X >= Width || Y >= Height)
				return false;

			return true;
		}

		public Block GetBlock(int X, int Y) {
			if (!RangeValid(X, Y))
				return new Block();

			return Tiles[Y * Width + X];
		}

		public ref Block GetBlockRef(int X, int Y) {
			if (!RangeValid(X, Y))
				return ref EmptyBlock;

			return ref Tiles[Y * Width + X];
		}

		public void SetBlock(int X, int Y, Block Tile) {
			if (!RangeValid(X, Y))
				return;

			Tiles[Y * Width + X] = Tile;
			Dirty = true;
		}

		public void PreDraw() {
			if (!Dirty)
				return;
			Dirty = false;

			ChunkTexture.BeginRender();



			// Draw floors
			for (int Y = 0; Y < Height; Y++)
				for (int X = 0; X < Width; X++) {
					Tile T = Tiles[Y * Width + X].Floor;

					if (Tile.IsEmpty(T))
						continue;

					GfxDraw.DrawTile(TilesetTexture, TileWidth, TileHeight, T.Tex_X, T.Tex_Y, X, Y);
				}

			// Draw walls
			for (int Y = 0; Y < Height; Y++)
				for (int X = 0; X < Width; X++) {
					Tile T = Tiles[Y * Width + X].Wall;

					if (Tile.IsEmpty(T))
						continue;

					GfxDraw.DrawTile(TilesetTexture, TileWidth, TileHeight, T.Tex_X, T.Tex_Y, X, Y);
				}


			ChunkTexture.EndRender();
		}

		public void Draw() {
			GfxDraw.DrawRectTextured(ChunkTexture, ChunkX, ChunkY);
		}
	}
}
