using Raylib_cs;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetraquark2.Gfx {
	class GfxTexture {
		public Texture2D Tex;

		public GfxTexture(string FileName) {
			Tex = Raylib.LoadTexture(FileName);

			Raylib.SetTextureFilter(Tex, TextureFilter.Point);
			Raylib.SetTextureWrap(Tex, TextureWrap.Clamp);
		}

		public void Draw() {
		}
	}

	class GfxRenderTexture {
		public RenderTexture2D Tex;

		public GfxRenderTexture(int Width, int Height) {
			Tex = Raylib.LoadRenderTexture(Width, Height);

			Raylib.SetTextureFilter(Tex.Texture, TextureFilter.Point);
			Raylib.SetTextureWrap(Tex.Texture, TextureWrap.Clamp);
		}

		public void BeginRender() {
			Raylib.BeginTextureMode(Tex);
		}

		public void EndRender() {
			Raylib.EndTextureMode();
		}
	}
}
