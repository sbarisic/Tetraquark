using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ChipmunkSharp;
using Tq.Entities;
using SFML.System;

namespace Tq.Game {
	static class Engine {
		public static List<Entity> Entities;
		public static Camera ActiveCamera;
		public static cpSpace Space;

		static Engine() {
			Entities = new List<Entity>();
		}

		public static Entity[] GetAllEntities() {
			return Entities.ToArray();
		}

		public static void SpawnEntity(Entity E) {
			if (!Entities.Contains(E)) {
				if (E is PhysicsEnt) {
					PhysicsEnt PE = (PhysicsEnt)E;
					Space.AddBody(PE.Body);
					Space.AddShape(PE.Shape);
				}
				Entities.Add(E);
			}
		}

		public static void RemoveEntity(Entity E) {
			if (Entities.Contains(E)) {
				if (E is PhysicsEnt) {
					PhysicsEnt PE = (PhysicsEnt)E;
					Space.RemoveBody(PE.Body);
					Space.RemoveShape(PE.Shape);
				}
				Entities.Remove(E);
			}
		}

		public static void UpdateEntities(float Dt, bool UpdatePhysics = true) {
			if (UpdatePhysics)
				Space.Step(Dt);

			for (int i = 0; i < Entities.Count; i++)
				if (Entities[i].NextThink <= Program.GameTime) {
					float Delta = Program.GameTime - Entities[i].LastThink;
					Entities[i].NextThink = Entities[i].Update(Delta) + Program.GameTime;
					Entities[i].LastThink = Program.GameTime;
				}
		}

		public static PhysicsEnt QueryPoint(Vector2f Point, cpShapeFilter Filter,
			out cpPointQueryInfo QueryInfo, float MaxDistance = 0) {
			QueryInfo = null;
			cpShape S = Space.PointQueryNearest(Point.ToCpVect(), MaxDistance, Filter, ref QueryInfo);

			PhysicsEnt Ent;
			if (S != null && (Ent = S.GetBody().GetUserData() as PhysicsEnt) != null)
				return Ent;
			return null;
		}

		public static PhysicsEnt QueryPoint(Vector2f Point, cpShapeFilter Filter, float MaxDistance = 0) {
			cpPointQueryInfo QueryInfo;
			return QueryPoint(Point, Filter, out QueryInfo, MaxDistance);
		}

		public static PhysicsEnt QuerySegment(Vector2f Start, Vector2f End, cpShapeFilter Filter,
			out cpSegmentQueryInfo QueryInfo, float Radius = -1) {
			bool Found = false;

			cpSegmentQueryInfo OutQueryInfo = null;
			Engine.Space.SegmentQuery(Start.ToCpVect(), End.ToCpVect(), Radius, Filter, (Shape, Point, Norm, Alpha, Data) => {
				if (Found)
					return;
				Found = true;
				OutQueryInfo = new cpSegmentQueryInfo(Shape, Point, Norm, Alpha);
			}, null);

			if (OutQueryInfo == null)
				OutQueryInfo = new cpSegmentQueryInfo(null, cpVect.Zero, cpVect.Zero, 0);
			QueryInfo = OutQueryInfo;

			PhysicsEnt Ent;
			if (OutQueryInfo.shape != null && (Ent = OutQueryInfo.shape.GetBody().GetUserData() as PhysicsEnt) != null)
				return Ent;
			return null;
		}

		public static PhysicsEnt QuerySegment(Vector2f Start, Vector2f End, cpShapeFilter Filter, float Radius = 0) {
			cpSegmentQueryInfo QueryInfo;
			return QuerySegment(Start, End, Filter, out QueryInfo, Radius);
		}

		public static PhysicsEnt QuerySegment(Vector2f Start, Vector2f End, cpShapeFilter Filter, out Vector2f Hit,
			float Radius = 0) {
			cpSegmentQueryInfo QueryInfo;
			PhysicsEnt Ret = QuerySegment(Start, End, Filter, out QueryInfo, Radius);
			Hit = QueryInfo.point.ToVec2f();
			return Ret;
		}
	}
}