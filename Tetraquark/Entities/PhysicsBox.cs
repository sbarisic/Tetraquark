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
using Physics;

namespace Tq.Entities {
	class PhysicsBox : PhysicsEnt {
		internal RectangleShape Rect;

		public PhysicsBox(float W = 32, float H = 32) {
			Rect = new RectangleShape(new Vector2f(W, H));
			Rect.Origin = Rect.Size / 2;
			Rect.FillColor = Color.White;
			Rect.Texture = ResourceMgr.Get<Texture>("box");
			Rect.Texture.Smooth = true;

			Phys.CreateBoxPhysics(W, H, Density.Wood, this);
			Body.SetVelocityUpdateFunc((S, Dmp, Dt) => Phys.CenterGravity(Body, S, Dmp, Dt));
		}

		public override void Draw(RenderSprite RT) {
			Rect.Position = Position;
			Rect.Rotation = Angle;
			RT.Draw(Rect);
		}
	}
}