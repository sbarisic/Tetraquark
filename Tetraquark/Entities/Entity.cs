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

		public virtual void Update(float Dt) {
		}

		public virtual void Draw(RenderSprite RT) {
		}
	}
}