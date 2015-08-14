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

namespace Tq.Entities {
	class Entity {
		public float NextThink;
		public float LastThink;

		public Entity() {
			LastThink = Program.GameTime;
		}

		public virtual float Update(float Dt) {
			return 10;
		}

		public virtual void Draw(RenderSprite RT) {
		}
	}
}