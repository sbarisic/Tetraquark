using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SFML.Window;
using SFML.System;
using SFML.Graphics;
using Tq.Graphics;

namespace Tq.States {
	class GameState : State {
		public GameState(RenderSprite RTex) {

		}

		public override void Update(float Dt) {
			Timer.Update(Program.GameTime);
		}

		public override void Draw(RenderSprite RT) {
			RT.Clear(Color.Cyan);
		}
	}
}