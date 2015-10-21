using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FarseerPhysics;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
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
		float W, H;

		public PhysicsBox(float W = 32, float H = 32) {
			this.W = W;
			this.H = H;

			Rect = new RectangleShape(new Vector2f(W, H));
			Rect.Origin = Rect.Size / 2;
			Rect.FillColor = Color.White;
			Rect.Texture = ResourceMgr.Get<Texture>("box");
			Rect.Texture.Smooth = true;
		}

		public override void InitPhysics(World Space) {
			base.InitPhysics(Space);
			Phys.CreateBoxPhysics(W, H, Density.Wood, this);
		}

		public override void Draw(RenderSprite RT) {
			Rect.Position = Position;
			Rect.Rotation = Angle.ToDeg();

			RT.Draw(Rect);
		}
	}
}