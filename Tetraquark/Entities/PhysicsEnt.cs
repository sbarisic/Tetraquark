using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SFML.System;

namespace Tq.Entities {
	class PhysicsEnt : Entity {
		public PhysicsEnt() {
			LinearDamping = .98f;
			AngularDamping = .98f;
		}

		public virtual bool Frozen {
			get;
			set;
		}

		public virtual Vector2f Position {
			get;
			set;
		}

		public virtual float Angle {
			get;
			set;
		}

		public virtual Vector2f LinearVelocity {
			get;
			set;
		}

		public virtual float AngularVelocity {
			get;
			set;
		}

		public virtual float LinearDamping {
			get;
			set;
		}

		public virtual float AngularDamping {
			get;
			set;
		}

		public override float Update(float Dt) {
			if (!Frozen) {
				if (LinearVelocity.X != 0 || LinearVelocity.Y != 0) {
					Position += LinearVelocity * Dt;
					LinearVelocity *= LinearDamping;
					if (Math.Abs(LinearVelocity.X) < 0.01 && Math.Abs(LinearVelocity.Y) < 0.01)
						LinearVelocity = new Vector2f(0, 0);
				}
				if (AngularVelocity != 0) {
					Angle += AngularVelocity * Dt;
					AngularVelocity *= AngularDamping;
					if (Math.Abs(AngularVelocity) < 0.01)
						AngularVelocity = 0;
				}
			}

			return 1.0f / 60;
		}
	}
}