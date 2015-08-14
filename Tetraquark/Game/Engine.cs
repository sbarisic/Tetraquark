using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Tq.Entities;

namespace Tq.Game {
	static class Engine {
		public static List<Entity> Entities;

		static Engine() {
			Entities = new List<Entity>();
		}

		public static void SpawnEntity(Entity E) {
			if (!Entities.Contains(E))
				Entities.Add(E);
		}

		public static void RemoveEntity(Entity E) {
			if (Entities.Contains(E))
				Entities.Remove(E);
		}

		public static void UpdateEntities(float Dt) {
			for (int i = 0; i < Entities.Count; i++)
				if (Entities[i].NextThink <= Program.GameTime) {
					float Delta = Program.GameTime - Entities[i].LastThink;
					Entities[i].NextThink = Entities[i].Update(Delta) + Program.GameTime;
					Entities[i].LastThink = Program.GameTime;
				}
		}
	}
}