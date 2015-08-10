using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenTK.Graphics.OpenGL;
using SFML.System;
using SFML.Graphics;
using SFML.Window;
using Tq.Graphics;
using Tq.Entities;

namespace Tq.States {
	class GameState : State {
		RenderSprite WorldRT;
		List<Entity> Entities;

		public GameState(RenderSprite RTex) {
			Entities = new List<Entity>();
			WorldRT = new RenderSprite(RTex.Size);
		}

		public override void Update(float Dt) {
			Timer.Update(Program.GameTime);
			for (int i = 0; i < Entities.Count; i++)
				Entities[i].Update(Dt);
		}

		public override void Draw(RenderSprite RT) {
			RT.Clear(Color.Cyan);
			WorldRT.Clear(Color.Transparent);

			for (int i = 0; i < Entities.Count; i++)
				Entities[i].Draw(WorldRT);

			WorldRT.Display();
			RT.Draw(WorldRT);
		}
	}
}