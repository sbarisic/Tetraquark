using System.Diagnostics;

using Tetraquark2.Engine;
using Tetraquark2.Gfx;

namespace Tetraquark2 {
	static class Program {
		static GameEngine Eng;

		static void Main(string[] args) {
			Eng = new GameEngine();
			Eng.InitWindow(1920, 1080, "Tetraquark");

			WorldGenerator.Init();

			Eng.Input = new InputManager();

			Eng.Camera = new GfxCamera();
			Eng.Camera.SetZoom(3);

			Eng.World = new GameWorld();

			Eng.Run();
		}
	}
}
