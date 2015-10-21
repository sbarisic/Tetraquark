using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FarseerPhysics;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
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
		Vector2f WorldMousePos;
		bool UpdatePhysics = true;

		public GameState() {
			WorldRT = new RenderSprite(Renderer.Screen.Size);
			Engine.ActiveCamera = new Camera();
			Engine.ActiveCamera.Resolution = Renderer.Screen.Size.ToVec2f();

			Entity[] Ents = Engine.GetAllEntities();
			for (int i = 0; i < Ents.Length; i++)
				Engine.RemoveEntity(Ents[i]);

			Engine.Space = new World(new Vector2f(0, 0).ToVec());

			/*for (int i = 0; i < 10; i++) {
				PhysicsBox Box = new PhysicsBox();
				Box.Position = new Vector2f(Utils.Random(-400, 400), Utils.Random(-400, 400));
				Engine.SpawnEntity(Box);
			}*/

			/*StarshipEnt Ply = new StarshipEnt();
			Ply.Position = new Vector2f(1, 1);
			Engine.SpawnEntity(Ply);*/
		}

		public override void OnKey(KeyEventArgs E, bool Pressed) {
			if (!Pressed)
				return;

			if (E.Code == Keyboard.Key.Space)
				UpdatePhysics = !UpdatePhysics;
			else if (E.Code == Keyboard.Key.Escape)
				Renderer.PushState(new MenuState());
		}

		bool HasA, HasB;
		Vector2f A, B;
		VertexArray VA, VB;
		PhysicsEnt DraggedEnt;

		public override void OnMouseButton(MouseButtonEventArgs E, bool Pressed) {
			Vector2f Point = WorldRT.MapPixelToCoords(new Vector2i(E.X, E.Y), Engine.ActiveCamera);

			if (E.Button == Mouse.Button.Middle) {
				if (Pressed)
					DraggedEnt = Phys.QueryPointEnt(Point.ToSimUnits());
				else
					DraggedEnt = null;
				return;
			} else if (E.Button == Mouse.Button.Right && Pressed) {
				Entity Ent = Phys.QueryPointEnt(Point.ToSimUnits());

				if (Ent != null) {
					Engine.RemoveEntity(Ent);
				} else {
					PhysicsBox Box = new PhysicsBox();
					Engine.SpawnEntity(Box);
					Box.Position = Point;
				}
				return;
			}

			if (E.Button == Mouse.Button.Left && Pressed) {
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

			if (HasA && HasB && Pressed) {
				PhysicsBox EntA = Phys.QueryPointEnt(A.ToSimUnits()) as PhysicsBox;
				PhysicsBox EntB = Phys.QueryPointEnt(B.ToSimUnits()) as PhysicsBox;

				if (EntA != null && EntB != null && EntA != EntB) {
					UpdatePhysics = false;
					EntA.Rect.FillColor = EntB.Rect.FillColor = Color.Cyan;
					Phys.Weld(EntA, EntB);
				}
			}
		}

		public override void OnMouseMoved(MouseMoveEventArgs E) {
			WorldMousePos =  WorldRT.MapPixelToCoords(new Vector2i(E.X, E.Y), Engine.ActiveCamera);
		}

		public override void Update(float Dt) {
			foreach (Body B in Engine.Space.BodyList)
				Phys.CenterGravity(B);

			if (DraggedEnt != null) {
				float Ang = (float)(DraggedEnt.Position.Angle(WorldMousePos) - Math.PI / 2);
				float Len = DraggedEnt.Position.DistanceSq(WorldMousePos);
				DraggedEnt.LinearVelocity = new Vector2f((float)Math.Sin(Ang) * -Len, (float)Math.Cos(Ang) * Len);
			}

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