using Raylib_cs;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.Marshalling;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace Tetraquark2.Gfx {
	static class GfxDraw {
		public static void DrawRect(int X, int Y, int W, int H) {
			Raylib.DrawRectangle(X, Y, W, H, Color.Red);
		}

		public static void DrawRectTextured(GfxTexture Tex, int X, int Y) {
			Raylib.DrawTexture(Tex.Tex, X, Y, Color.White);
		}

		public static void DrawRectTextured(GfxRenderTexture Tex, int X, int Y) {
			Rectangle Src = new Rectangle(0, 0, Tex.Tex.Texture.Width, -Tex.Tex.Texture.Height);
			Raylib.DrawTextureRec(Tex.Tex.Texture, Src, new Vector2(X, Y), Color.White);
		}

		public static void DrawTile(GfxTexture Source, int TileWidth, int TileHeight, int SrcX, int SrcY, int DstX, int DstY) {
			Rectangle SrcRect = new Rectangle(SrcX * TileWidth, SrcY * TileWidth, TileWidth, TileHeight);
			Rectangle DstRect = new Rectangle(DstX * TileWidth, DstY * TileWidth, TileWidth, TileHeight);
			Raylib.DrawTexturePro(Source.Tex, SrcRect, DstRect, Vector2.Zero, 0, Color.White);
		}
	}
}
