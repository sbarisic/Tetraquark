using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Tetraquark2 {
	static unsafe class Utils {
		public static ref T Empty<T>() {
			return ref Unsafe.NullRef<T>();
		}

		public static bool IsEmpty<T>(ref T Val) {
			if (Unsafe.IsNullRef(ref Val))
				return true;

			return false;
		}
	}
}
