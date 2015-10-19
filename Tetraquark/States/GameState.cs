using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ChipmunkSharp;
using OpenTK.Graphics.OpenGL;
using SFML.System;
using SFML.Graphics;
using SFML.Window;
using PrimType = SFML.Graphics.PrimitiveType;
using Tq.Graphics;
using Tq.Entities;
using Tq.Game;
using Physics;

namespace Tq.States {
	class GameState : State {
		RenderSprite WorldRT;

		public GameState() {
			WorldRT = new RenderSprite(Renderer.Screen.Size);
			Engine.ActiveCamera = new Camera();
			Engine.ActiveCamera.Resolution = Renderer.Screen.Size.ToVec2f();

			Engine.Space = new cpSpace();
			Engine.Space.CollisionEnabled = true;
			Engine.Space.SetIterations(50);
			Engine.Space.SetCollisionSlop(0.5f);
			Engine.Space.SetSleepTimeThreshold(0.5f);
			Engine.Space.SetDamping(0.9f);

			Entity[] Ents = Engine.GetAllEntities();
			for (int i = 0; i < Ents.Length; i++)
				Engine.RemoveEntity(Ents[i]);

			for (int i = 0; i < 10; i++) {
				PhysicsBox Box = new PhysicsBox();
				Box.Position = new Vector2f(Utils.Random(-400, 400), Utils.Random(-400, 400));
				Engine.SpawnEntity(Box);
			}

			/*StarshipEnt Ply = new StarshipEnt();
			Ply.Position = new Vector2f(1, 1);
			Engine.SpawnEntity(Ply);*/
		}

		public override void OnKey(KeyEventArgs E, bool Pressed) {
			if (!Pressed)
				return;

			if (E.Code == Keyboard.Key.Escape)
				Renderer.PushState(new MenuState());
		}


		bool HasA, HasB;
		Vector2f A, B;
		VertexArray VA, VB;

		public override void OnMouseButton(MouseButtonEventArgs E, bool Pressed) {
			if (!Pressed)
				return;

			if (E.Button == Mouse.Button.Middle) {
				UpdatePhysics = !UpdatePhysics;
				return;
			}

			Vector2f Point = WorldRT.MapPixelToCoords(new Vector2i(E.X, E.Y), Engine.ActiveCamera);

			if (E.Button == Mouse.Button.Left) {
				if (!HasA) {
					HasA = true;
					A = Point;
				} else if (!HasB) {
					HasB = true;
					B = Point;
					if (VA == null) {
						VA = new VertexArray(PrimType.Lines);
						VB = new VertexArray(PrimType.Points);
					}
					VA.Clear();
					VA.Append(new Vertex(A, Color.White));
					VA.Append(new Vertex(B, Color.White));
				} else {
					HasA = HasB = false;
				}
			}

			if (HasA && HasB) {
				VA.Clear();
				Vector2f Hit;
				for (int i = 0; i < 360; i++) {
					B = A + (new Vector2f((float)Math.Sin(((float)i).ToRad()), (float)Math.Cos(((float)i).ToRad())) * 200);
					if (Engine.QuerySegment(A, B, Phys.Filter_Everything, out Hit) == null)
						Hit = B;

					VA.Append(new Vertex(A, Color.White));
					VA.Append(new Vertex(Hit, Color.White));
				}
			}
		}

		bool UpdatePhysics = true;
		public override void Update(float Dt) {
			Timer.Update(Program.GameTime);
			Engine.UpdateEntities(Dt, UpdatePhysics);
		}

		public override void Draw(RenderSprite RT) {
			RT.Clear(Color.Cyan);
			WorldRT.Clear(Color.Transparent);
			WorldRT.Draw(VertexShapes.Quad, PrimType.Quads,
				Shaders.UseStarfield(Engine.ActiveCamera.Position / 2, Engine.ActiveCamera.Rotation, RT.Size.ToVec2f())
				.Scale(WorldRT.Size.ToVec2f()));
			WorldRT.Draw(Shaders.DrawPhosphorGlow(WorldRT.Texture, 0.02f));

			WorldRT.SetView(Engine.ActiveCamera);
			for (int i = 0; i < Engine.Entities.Count; i++)
				Engine.Entities[i].Draw(WorldRT);
			if (HasA && HasB) {
				WorldRT.Draw(VA);
				GL.PointSize(5);
				WorldRT.Draw(VB);
				GL.PointSize(1);
			}

			WorldRT.SetView(WorldRT.DefaultView);

			WorldRT.Display();
			RT.Draw(WorldRT);
		}
	}
}