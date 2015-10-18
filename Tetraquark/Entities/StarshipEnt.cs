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
				Engine.ActiveCamera.Position = value;
			}
		}

		public override float Angle {
			get {
				return Rect.Rotation;
			}
			set {
				Rect.Rotation = value;
				Engine.ActiveCamera.Rotation = value;
			}
		}

		public StarshipEnt() {
			PlayerControlled = false;
			Rect = new RectangleShape(new Vector2f(16, 16));
			Rect.Origin = Rect.Size / 2;
			Rect.FillColor = Color.White;

			AngularDamping = LinearDamping = 1;

			LinearVelocity = new Vector2f(0, -100);
			AngularVelocity = 60;
		}

		public override float Update(float Dt) {
			//float Next = base.Update(Dt);
			float Next = 0;

			Position = new Vector2f((float)Math.Sin(Program.GameTime / 2) * 200, (float)Math.Cos(Program.GameTime / 2) * 200);
			Angle = new Vector2f().Angle(Position);

			return Next;
		}

		public override void Draw(RenderSprite RT) {
			Rect.FillColor = Color.White;
			RT.Draw(Rect);

			Rect.Position = new Vector2f();
			Rect.FillColor = Color.Red;
			Rect.Rotation = 0;
			RT.Draw(Rect);
		}
	}
}