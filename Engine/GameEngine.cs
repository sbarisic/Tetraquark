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

		public InputManager Input;
		public GameWorld World;
		public GfxCamera Camera;

		public GameEngine() {
			UpdateInterval = 30;
			UpdateTimeStep = 1.0 / UpdateInterval;
		}

		public void InitWindow(int W, int H, string Title) {
			//Raylib.SetConfigFlags(ConfigFlags.VSyncHint);
			Raylib.SetConfigFlags(ConfigFlags.Msaa4xHint);

			Raylib.InitWindow(W, H, Title);
			Raylib.SetTargetFPS(240);
		}

		void RunUpdates() {
			Stopwatch SWatch = Stopwatch.StartNew();

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

			Stopwatch SWatch = Stopwatch.StartNew();
			Thread.Sleep(1);
			double Dt = 0;

			while (!Raylib.WindowShouldClose()) {
				Dt = SWatch.Elapsed.TotalSeconds;
				SWatch.Restart();

				HandleInput(Dt);
				Draw(Dt);
			}

			IsRunning = false;
			Raylib.CloseWindow();
		}

		public void Update(double Dt) {
			World?.Update(Dt);
		}

		public void HandleInput(double Dt) {
			float Wheel = Input.GetMouseWheel();
			if (Wheel != 0) {
				Vector2 MousePos = Input.GetMousePosition();
				Vector2 WorldPos = Camera.ScreenToWorld(MousePos);

				Camera.Offset = MousePos;
				Camera.Target = WorldPos;

				float Factor = 1.0f + (0.25f * Math.Abs(Wheel));
				if (Wheel < 0)
					Factor = 1.0f / Factor;

				Camera.Zoom = Math.Clamp(Camera.Zoom * Factor, 0.125f, 64.0f);
			}

			if (Input.IsMouseButtonDown(TqMouseButton.Right)) {
				Vector2 Delta = Input.GetMouseDelta();

				Delta = Delta * (-1.0f / Camera.Zoom);
				Camera.Target = Camera.Target + Delta;
			}

			World?.HandleInput(Dt);
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
