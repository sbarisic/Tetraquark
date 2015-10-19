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

namespace Tq.Entities {
	class StarshipEnt : PhysicsEnt {
		protected RectangleShape Rect;
		public bool PlayerControlled;

		public StarshipEnt() {
			const float Sz = 32.0f;

			PlayerControlled = false;
			Rect = new RectangleShape(new Vector2f(Sz, Sz));
			Rect.Origin = Rect.Size / 2;
			Rect.FillColor = Color.White;

			float Mass = Sz * Sz * 1.0f / 1000.0f;
			Body = new cpBody(Mass, cp.MomentForBox(Mass, Sz, Sz));
			Shape = cpPolyShape.BoxShape(Body, Sz, Sz, 0);
			Shape.SetFriction(0.7f);
			Shape.SetElasticity(0.1f);
			
			Body.SetVelocityUpdateFunc((S, Damping, Dt) => {
				cpVect P = Body.GetPosition();
				float SqDist = cpVect.cpvlength(P);
				cpVect G = cpVect.cpvmult(P, -980.0f / (SqDist * cp.cpfsqrt(SqDist)));
				Body.UpdateVelocity(G, Damping, Dt);
			});

			//LinearVelocity = new Vector2f(0, -100);
			AngularVelocity = Utils.Random(-360, 360);
		}

		public override float Update(float Dt) {
			/*Position = new Vector2f((float)Math.Sin(Program.GameTime / 2) * 200, (float)Math.Cos(Program.GameTime / 2) * 200);
			Angle = new Vector2f().Angle(Position);*/
			return 0;
		}

		public override void Draw(RenderSprite RT) {
			/*Engine.ActiveCamera.Position = Position;
			Engine.ActiveCamera.Rotation = Angle;
			RT.SetView(Engine.ActiveCamera);*/

			Rect.Position = Position;
			Rect.Rotation = Angle;
			RT.Draw(Rect);
		}
	}
}