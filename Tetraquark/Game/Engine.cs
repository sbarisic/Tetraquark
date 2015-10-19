using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ChipmunkSharp;
using Tq.Entities;

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

		public static void UpdateEntities(float Dt) {
			Space.Step(Dt);

			for (int i = 0; i < Entities.Count; i++)
				if (Entities[i].NextThink <= Program.GameTime) {
					float Delta = Program.GameTime - Entities[i].LastThink;
					Entities[i].NextThink = Entities[i].Update(Delta) + Program.GameTime;
					Entities[i].LastThink = Program.GameTime;
				}
		}
	}
}