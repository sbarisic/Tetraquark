using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FarseerPhysics;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using Tq.Entities;
using SFML.System;

namespace Tq.Game {
	static class Engine {
		public static List<Entity> Entities;
		public static Camera ActiveCamera;
		public static World Space;

		static Engine() {
			ConvertUnits.SetDisplayUnitToSimUnitRatio(16);
			Entities = new List<Entity>();
		}

		public static Entity[] GetAllEntities() {
			return Entities.ToArray();
		}

		public static void SpawnEntity(Entity E) {
			if (!Entities.Contains(E)) {
				if (E is PhysicsEnt)
					((PhysicsEnt)E).InitPhysics(Space);
				Entities.Add(E);
			}
		}

		public static void RemoveEntity(Entity E) {
			if (Entities.Contains(E)) {	
				Entities.Remove(E);
				if (E is PhysicsEnt)
					((PhysicsEnt)E).DestroyPhysics(Space);
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
	}
}