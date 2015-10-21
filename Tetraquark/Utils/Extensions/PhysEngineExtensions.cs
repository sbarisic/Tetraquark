using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FarseerPhysics;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using SFML.System;
using Tq.Entities;

namespace Tq {
	static class PhysEngineExtensions {
		public static Entity GetOwnerEnt(this Body B) {
			return B.UserData as Entity;
		}

		public static Entity GetOwnerEnt(this Fixture Fix) {
			return Fix.Body.GetOwnerEnt();
		}

		public static void SetOwnerEnt(this Body B, Entity Ent) {
			B.UserData = Ent;
		}

		/*public static void RemoveBodyAndShapes(this cpSpace Space, cpBody B) {
			cpShape[] S = Space.GetAllShapes(B);
			for (int i = 0; i < S.Length; i++)
				Space.RemoveShape(S[i]);
			Space.RemoveBody(B);
		}

		public static void AddBodyAndShapes(this cpSpace Space, cpBody B) {
			Space.AddBody(B);
			cpShape[] S = Space.GetAllShapes(B);
			for (int i = 0; i < S.Length; i++)
				Space.AddShape(S[i]);
		}

		public static cpShape[] GetAllShapes(this cpSpace Space, cpBody B) {
			//List<cpShape> Shapes = new List<cpShape>();
			HashSet<cpShape> Shapes = new HashSet<cpShape>();

			B.EachShape((S, ShapeList) => {
				HashSet<cpShape> ShapeSet = (HashSet<cpShape>)ShapeList;
				if (!ShapeSet.Contains(S))
					ShapeSet.Add(S);
			}, Shapes);

			return Shapes.ToArray();
		}*/
	}
}