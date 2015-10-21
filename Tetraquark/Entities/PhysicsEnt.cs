using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FarseerPhysics;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using FarseerPhysics.Dynamics.Joints;
using Tq.Game;
using SFML.System;

namespace Tq.Entities {
	class PhysicsEnt : Entity {
		internal Body Body;

		public PhysicsEnt() {
		}

		public virtual void InitPhysics(World Space) {
			Body = new Body(Engine.Space);
			Body.BodyType = BodyType.Dynamic;
			Body.SetOwnerEnt(this);
		}

		public virtual void DestroyPhysics(World Space) {
			if (Body != null) {
				Body.Dispose();
				Body = null;
			}
		}

		public virtual bool Frozen {
			get;
			set;
		}

		public virtual Vector2f Position {
			get {
				return Body.Position.ToVec().ToDisplayUnits();
			}
			set {
				Body.Position = value.ToSimUnits().ToVec();
			}
		}

		public virtual float Angle {
			get {
				return Body.Rotation;
			}
			set {
				Body.Rotation = value;
			}
		}

		public virtual Vector2f LinearVelocity {
			get {
				return Body.LinearVelocity.ToVec().ToDisplayUnits();
			}
			set {
				Body.LinearVelocity = value.ToSimUnits().ToVec();
			}
		}

		public virtual float AngularVelocity {
			get {
				return Body.AngularVelocity;
			}
			set {
				Body.AngularVelocity = value;
			}
		}

		public Vector2f LocalCenter {
			get {
				return Body.LocalCenter.ToVec();
			}
		}

		public virtual Vector2f WorldToLocal(Vector2f Vec) {
			return Body.GetLocalVector(Vec.ToVec()).ToVec();
		}

		public virtual Vector2f LocalToWorld(Vector2f Vec) {
			return Body.GetWorldVector(Vec.ToVec()).ToVec();
		}

		public virtual void ApplyForce(Vector2f F) {
			Body.ApplyForce(F.ToVec());
		}
	}
}