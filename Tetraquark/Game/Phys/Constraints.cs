using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FarseerPhysics;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using FarseerPhysics.Dynamics.Joints;
using SFML.System;
using Physics;
using Tq.Entities;

namespace Tq.Game {
	static partial class Phys {
		public static WeldJoint Weld(PhysicsEnt A, PhysicsEnt B) {
			return JointFactory.CreateWeldJoint(Engine.Space, A.Body, B.Body, A.Body.LocalCenter, B.Body.LocalCenter);
		}
	}
}