using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FarseerPhysics;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using SFML.System;

namespace Tq.Meth {
	struct RayHit {
		public Fixture Fixture;
		public Vector2f HitPoint;
		public Vector2f Normal;
		public float Fraction;

		public RayHit(Fixture Fixture, Vector2f HitPoint, Vector2f Normal, float Fraction) {
			this.Fixture = Fixture;
			this.HitPoint = HitPoint;
			this.Normal = Normal;
			this.Fraction = Fraction;
		}
	}
}