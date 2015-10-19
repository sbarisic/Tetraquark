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

namespace Tq.States {
	class GameState : State {
		RenderSprite WorldRT;

		public GameState() {
			WorldRT = new RenderSprite(Renderer.Screen.Size);
			Engine.ActiveCamera = new Camera();
			Engine.ActiveCamera.Resolution = Renderer.Screen.Size.ToVec2f();

			Engine.Space = new cpSpace();
			Engine.Space.CollisionEnabled = true;
			Engine.Space.SetIterations(60);
			Engine.Space.SetCollisionSlop(0.5f);
			Engine.Space.SetSleepTimeThreshold(0.5f);

			Entity[] Ents = Engine.GetAllEntities();
			for (int i = 0; i < Ents.Length; i++)
				Engine.RemoveEntity(Ents[i]);

			StarshipEnt Player = new StarshipEnt();
			Player.Position = new Vector2f(100, 0);
			Engine.SpawnEntity(Player);

			StarshipEnt Player2 = new StarshipEnt();
			Player2.Position = new Vector2f(-100, 0);
			Engine.SpawnEntity(Player2);
		}

		public override void OnKey(KeyEventArgs E, bool Pressed) {
			if (!Pressed)
				return;

			if (E.Code == Keyboard.Key.Escape)
				Renderer.PushState(new MenuState());
		}

		public override void Update(float Dt) {
			Timer.Update(Program.GameTime);
			Engine.UpdateEntities(Dt);
		}

		public override void Draw(RenderSprite RT) {
			RT.Clear(Color.Cyan);
			WorldRT.Clear(Color.Transparent);
			WorldRT.Draw(VertexShapes.Quad, PrimType.Quads,
				Shaders.UseStarfield(Engine.ActiveCamera.Position, Engine.ActiveCamera.Rotation, RT.Size.ToVec2f())
				.Scale(WorldRT.Size.ToVec2f()));
			WorldRT.Draw(Shaders.DrawPhosphorGlow(WorldRT.Texture, 0.02f));

			WorldRT.SetView(Engine.ActiveCamera);
			for (int i = 0; i < Engine.Entities.Count; i++)
				Engine.Entities[i].Draw(WorldRT);
			WorldRT.SetView(WorldRT.DefaultView);

			WorldRT.Display();
			RT.Draw(WorldRT);
		}
	}
}