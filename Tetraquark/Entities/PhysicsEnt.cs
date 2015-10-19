using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ChipmunkSharp;
using SFML.System;

namespace Tq.Entities {
	class PhysicsEnt : Entity {
		internal cpBody Body;
		internal cpShape Shape;

		public PhysicsEnt() {
		}

		public virtual bool Frozen {
			get;
			set;
		}

		public virtual Vector2f Position {
			get {
				return Body.GetPosition().ToVec2f();
			}
			set {
				Body.SetPosition(value.ToCpVect());
			}
		}

		public virtual float Angle {
			get {
				return Body.GetAngle().ToDeg();
			}
			set {
				Body.SetAngle(value.ToRad());
			}
		}

		public virtual Vector2f LinearVelocity {
			get {
				return Body.GetVelocity().ToVec2f();
			}
			set {
				Body.SetVelocity(value.ToCpVect());
			}
		}

		public virtual float AngularVelocity {
			get {
				return Body.GetAngularVelocity().ToDeg();
			}
			set {
				Body.SetAngularVelocity(value.ToRad());
			}
		}
	}
}