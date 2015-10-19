using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SFML.System;
using Physics;
using ChipmunkSharp;
using Tq.Entities;

namespace Tq.Game {
	[Flags]
	enum PhysicsFilters : int {
		Everything = 1
	}

	static class Phys {
		static cpShapeFilter CreateFilter(PhysicsFilters F) {
			return new cpShapeFilter(cp.NO_GROUP, (int)F, (int)F);
		}

		public static cpShapeFilter Filter_Everything = CreateFilter(PhysicsFilters.Everything);

		public static Quantity CalculateBoxMass(Quantity W, Quantity H, Quantity Density) {
			return W.Convert(SI.m) * H.Convert(SI.m) * SI.m.Quant(0.1) * Density;
		}

		public static void CreateBoxPhysics(float W, float H, Quantity Density, PhysicsEnt Ent) {
			CreateBoxPhysics(new Quantity(W, SI.px), new Quantity(H, SI.px), Density, Ent);
		}

		public static void CreateBoxPhysics(Quantity W, Quantity H, Quantity Density, PhysicsEnt Ent) {
			CreateBoxPhysics(W, H, Density, out Ent.Body, out Ent.Shape);
			Ent.Body.SetUserData(Ent);
		}

		public static void CreateBoxPhysics(Quantity Width, Quantity Height, Quantity Density, out cpBody Body, out cpShape Shape) {
			float W = (float)Width.Convert(SI.px).Amount;
			float H = (float)Height.Convert(SI.px).Amount;

			Quantity Mass = Phys.CalculateBoxMass(Width, Height, Density);
			Body = new cpBody((float)Mass.Amount, MomentForBox(Mass, Width, Height));
			Shape = cpPolyShape.BoxShape(Body, W, H, 0);
			Shape.SetFilter(Phys.Filter_Everything);
			Shape.SetFriction(0.8f);
			Shape.SetElasticity(0.1f);
		}

		public static float MomentForBox(Quantity Mass, Quantity W, Quantity H) {
			return cp.MomentForBox((float)Mass.Convert(SI.kg).Amount,
				(float)W.Convert(SI.m).Amount, (float)H.Convert(SI.m).Amount);
		}

		public static void CenterGravity(cpBody Body, cpVect S, float Damping, float Dt) {
			Vector2f Pos = Body.GetPosition().ToVec2f();

			Quantity Radius = SI.px.Quant(Pos.Length()).Convert(SI.m) + SI.m.Quant(3000);
			Quantity F = Constants.G * (SI.kg.Quant(Body.GetMass()) * SI.kg.Quant(5.972e16)) / (Radius ^ 2);

			Vector2f Force = -Pos.Normalize() * (float)F.Amount;
			Body.UpdateVelocity(Force.ToCpVect(), Damping, Dt);
		}
	}
}