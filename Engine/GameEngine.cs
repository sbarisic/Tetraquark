using Raylib_cs;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

using Tetraquark2.Gfx;

namespace Tetraquark2.Engine {
	class GameEngine {
		double UpdateInterval;
		double UpdateTimeStep;

		Thread UpdateThread;
		bool IsRunning;

		public GameWorld World;
		public GfxCamera Camera;

		public GameEngine() {
			UpdateInterval = 30;
			UpdateTimeStep = 1.0 / UpdateInterval; // 30 times per second
		}

		public void InitWindow(int W, int H, string Title) {
			Raylib.SetConfigFlags(ConfigFlags.Msaa4xHint);

			Raylib.InitWindow(W, H, Title);
			Raylib.SetTargetFPS(240);
		}

		void RunUpdates() {
			Stopwatch SWatch = new Stopwatch();

			while (IsRunning) {
				if (SWatch.Elapsed.TotalSeconds >= UpdateTimeStep) {
					SWatch.Restart();

					Update(UpdateTimeStep);
				}
			}
		}

		public void Run() {
			IsRunning = true;

			UpdateThread = new Thread(RunUpdates);
			UpdateThread.IsBackground = true;
			UpdateThread.Start();

			Stopwatch SWatch = new Stopwatch();
			Thread.Sleep(1);
			double Dt = 0;

			while (!Raylib.WindowShouldClose()) {
				Dt = SWatch.Elapsed.TotalSeconds;
				SWatch.Restart();

				Draw(Dt);
			}

			IsRunning = false;
			Raylib.CloseWindow();
		}

		public void Update(double Dt) {
			World?.Update(Dt);
		}

		public void Draw(double Dt) {
			Raylib.BeginDrawing();
			Raylib.ClearBackground(Color.Beige);

			if (Camera != null) {
				Raylib.BeginMode2D(Camera.Cam);
				World?.Draw(Dt);
				Raylib.EndMode2D();
			}

			Raylib.EndDrawing();
		}
	}
}
