using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

		public GameState(RenderSprite RTex) {
			WorldRT = new RenderSprite(RTex.Size);
			Camera.Resolution = RTex.Size.ToVec2f();

			StarshipEnt Player = new StarshipEnt();
			Engine.SpawnEntity(Player);
		}

		public override void Update(float Dt) {
			Timer.Update(Program.GameTime);
			Engine.UpdateEntities(Dt);
		}

		public override void Draw(RenderSprite RT) {
			RT.Clear(Color.Cyan);
			WorldRT.Clear(Color.Transparent);
			WorldRT.Draw(VertexShapes.Quad, PrimType.Quads,
				Shaders.UseStarfield(Camera.Position, RT.Size.ToVec2f()).Scale(WorldRT.Size.ToVec2f()));
			WorldRT.Draw(Shaders.DrawPhosphorGlow(WorldRT.Texture, 0.02f));

			WorldRT.SetView(Camera.ToView());
			for (int i = 0; i < Engine.Entities.Count; i++)
				Engine.Entities[i].Draw(WorldRT);
			WorldRT.SetView(WorldRT.DefaultView);

			WorldRT.Display();
			RT.Draw(WorldRT);
		}
	}
}