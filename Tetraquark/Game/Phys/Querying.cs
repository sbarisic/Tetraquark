using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FarseerPhysics;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using SFML.System;
using Physics;
using Tq.Entities;
using Tq.Meth;

namespace Tq.Game {
	static partial class Phys {
		public static RayHit QueryRayFirst(Vector2f Start, Vector2f End) {
			RayHit Ret = new RayHit();

			Engine.Space.RayCast((Fix, Point, Norm, Frac) => {
				Ret = new RayHit(Fix, Point.ToVec(), Norm.ToVec(), Frac);
				return 0;
			}, Start.ToVec(), End.ToVec());

			return Ret;
		}

		public static Fixture QueryPoint(Vector2f Point) {
			return Engine.Space.TestPoint(Point.ToVec());
		}

		public static PhysicsEnt QueryPointEnt(this Vector2f Point) {
			Fixture F = QueryPoint(Point);
			if (F != null)
				return F.GetOwnerEnt() as PhysicsEnt;
			return null;
		}
	}
}