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

namespace Tq.Entities {
	class StarshipEnt : PhysicsEnt {
		protected RectangleShape Rect;
		public bool PlayerControlled;

		public override Vector2f Position {
			get {
				return Rect.Position;
			}
			set {
				Rect.Position = value;
			}
		}

		public StarshipEnt() {
			PlayerControlled = false;
			Rect = new RectangleShape(new Vector2f(16, 16));
			Rect.FillColor = Color.White;
		}

		public override void Draw(RenderSprite RT) {
			RT.Draw(Rect);
		}
	}
}