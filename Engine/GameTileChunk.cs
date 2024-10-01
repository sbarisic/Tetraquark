using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Tetraquark2.Gfx;

namespace Tetraquark2.Engine {
	struct GameTile {
		public int Id;

		public int Tex_X;
		public int Tex_Y;

		public static bool IsEmpty(GameTile Tile) {
			return Tile.Id == 0;
		}
	}

	struct GameBlock {
		public GameTile Floor;
		public GameTile Wall;
		public GameTile Roof;
	}

	class GameTileChunk {
		public int TileWidth;
		public int TileHeight;
		public int Width;
		public int Height;

		bool Dirty;
		public GameBlock[] Tiles;
		public GfxRenderTexture ChunkTexture;

		public GfxTexture TilesetTexture;


		public GameTileChunk(int TileWidth, int TileHeight, int Width, int Height) {
			this.Width = Width;
			this.Height = Height;
			this.TileWidth = TileWidth;
			this.TileHeight = TileHeight;

			Tiles = new GameBlock[Width * Height];

			ChunkTexture = new GfxRenderTexture(TileWidth * Width, TileHeight * Height);

			GameBlock Blk = GetBlock(3, 2);
			Blk.Wall.Tex_X = 1;
			Blk.Wall.Tex_Y = 0;

			SetBlock(3, 2, Blk);

			Dirty = true;
		}

		public GameBlock GetBlock(int X, int Y) {
			return Tiles[Y * Width + X];
		}

		public void SetBlock(int X, int Y, GameBlock Tile) {
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
					GameTile T = Tiles[Y * Width + X].Floor;

					if (GameTile.IsEmpty(T))
						continue;

					GfxDraw.DrawTile(TilesetTexture, TileWidth, TileHeight, T.Tex_X, T.Tex_Y, X, Y);
				}

			// Draw walls
			for (int Y = 0; Y < Height; Y++)
				for (int X = 0; X < Width; X++) {
					GameTile T = Tiles[Y * Width + X].Wall;

					if (GameTile.IsEmpty(T))
						continue;

					GfxDraw.DrawTile(TilesetTexture, TileWidth, TileHeight, T.Tex_X, T.Tex_Y, X, Y);
				}


			ChunkTexture.EndRender();
		}

		public void Draw() {
			GfxDraw.DrawRectTextured(ChunkTexture, 0, 0);
		}
	}
}
