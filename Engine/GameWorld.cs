using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tetraquark2.Engine.Tiles;
using Tetraquark2.Gfx;

namespace Tetraquark2.Engine
{
    class GameWorld {
		public List<Entity> Entities = new List<Entity>();

		TileChunk Map;

		public GameWorld() {
			Map = new TileChunk(32, 32, 10, 10);
			Map.TilesetTexture = new GfxTexture("data/textures/tilemap_1.png");

			WorldGenerator.Generate(Map);
		}

		public void Update(double Dt) {
			for (int i = 0; i < Entities.Count; i++) {
				Entities[i].Update(Dt);
			}
		}

		public void HandleInput(double Dt) {
		}

		public void Draw(double Dt) {
			Map.PreDraw();
			Map.Draw();

			for (int i = 0; i < Entities.Count; i++) {
				Entities[i].Draw(Dt);
			}
		}
	}
}
