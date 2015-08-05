using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tq {
	delegate object TimerFunc(object O);
	static class Timer {
		class TimerEntry {
			public TimerFunc T;
			public float NextTime;
			public object State;

			public TimerEntry(TimerFunc T, float NextTime) {
				this.T = T;
				this.NextTime = NextTime;
				State = null;
			}
		}

		static bool InsideUpdate;
		static HashSet<TimerEntry> AllTimers;
		static TimerEntry CurrentTimer;
		static object CurrentState;

		static Timer() {
			AllTimers = new HashSet<TimerEntry>();
			InsideUpdate = false;
		}

		public static void Add(TimerFunc T, float NextTime) {
			AllTimers.Add(new TimerEntry(T, NextTime));
		}

		static void EnsureInsideLoop() {
			if (!InsideUpdate)
				throw new Exception("Cannot call Repeat outside a timer");
		}

		public static void Repeat(float NextTime) {
			EnsureInsideLoop();
			CurrentTimer.NextTime = NextTime;
			AllTimers.Add(CurrentTimer);
		}

		public static object Loop(int N, float NextTime) {
			return Loop(CurrentState, N, NextTime);
		}

		public static object Loop(object CurrentState, int N, float NextTime) {
			EnsureInsideLoop();
			if (CurrentState == null) {
				Repeat(NextTime);
				return 1;
			} else if ((int)CurrentState < N - 1) {
				Repeat(NextTime);
				return (int)CurrentState + 1;
			} else
				return CurrentState;
		}

		public static void Update(float Time) {
			InsideUpdate = true;
			var Tmrs = AllTimers.ToArray();
			for (int i = 0; i < Tmrs.Length; i++) {
				CurrentTimer = Tmrs[i];
				if (CurrentTimer.NextTime <= Time) {
					AllTimers.Remove(CurrentTimer);
					CurrentState = CurrentTimer.State;
					CurrentTimer.State = CurrentTimer.T(CurrentTimer.State);
				}
			}
			InsideUpdate = false;
		}
	}
}