using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tq {
	static class Utils {
		public static void Set<K, V>(this Dictionary<K, V> Dict, K Key, V Value) {
			if (Dict.ContainsKey(Key))
				Dict.Remove(Key);
			Dict.Add(Key, Value);
		}
	}
}
