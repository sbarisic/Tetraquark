using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FarseerPhysics;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using Microsoft.Xna.Framework;
using SFML.System;
using Physics;
using Tq.Entities;

namespace Tq.Game {
	static partial class Phys {
		public static void CreateBoxPhysics(Quantity W, Quantity H, Quantity Density, PhysicsEnt Ent) {
			CreateBoxPhysics((float)W.Convert(SI.px).Amount, (float)H.Convert(SI.px).Amount, Density, Ent);
		}

		public static Fixture CreateBoxPhysics(float W, float H, Quantity Density, PhysicsEnt Ent) {
			return FixtureFactory.AttachRectangle(W.ToSimUnits(), H.ToSimUnits(), (float)Density.Convert(SI.ρ).Amount,
				new Vector2(), Ent.Body);
		}

		public static void CenterGravity(Body Body) {
			Vector2f Pos = Body.Position.ToVec();

			Quantity Radius = SI.px.Quant(Pos.Length()).Convert(SI.m) + SI.m.Quant(1000);
			Quantity F = Constants.G * (SI.kg.Quant(Body.Mass) * SI.kg.Quant(5.972e16)) / (Radius ^ 2);

			Vector2f Force = -Pos.Normalize() * (float)F.Amount;
			Body.ApplyForce(Force.ToVec());
		}
	}
}