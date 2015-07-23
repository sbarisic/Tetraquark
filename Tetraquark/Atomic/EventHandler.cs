using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SFML.System;
using SFML.Window;

namespace Tq.Atomic {
	class EventHandler {
		static EventHandler GrabbedHander;
		LinkedList<EventHandler> Childern;
		bool Enabled, Active;
		EventHandler ActiveChild;
		EventHandler Parent;

		protected Vector2f GrabPos;

		protected bool IsEnabled {
			get {
				return Enabled;
			}
		}

		public EventHandler() {
			Childern = new LinkedList<EventHandler>();
		}

		public EventHandler(Window Wnd)
			: this() {
			Wnd.MouseMoved += (S, E) => {
				if (GrabbedHander != null)
					GrabbedHander.OnMouseDrag(E);
				else
					OnMouseMove(E);
			};
			Wnd.MouseButtonPressed += (S, E) => OnMouse(E, true);
			Wnd.MouseButtonReleased += (S, E) => {
				OnMouse(E, false);
				OnMouseClick(E, false);
			};
			Wnd.KeyPressed += (S, E) => OnKey(E, true);
			Wnd.KeyReleased += (S, E) => OnKey(E, false);
			Wnd.TextEntered += (S, E) => OnKeyPress(E);

			Enable();
		}

		public virtual void AddChild(EventHandler C, bool Last = false) {
			if (Childern.Contains(C))
				return;
			if (C.Parent != null)
				C.Parent.RemoveChild(C);
			C.Parent = this;
			if (Last)
				Childern.AddLast(C);
			else
				Childern.AddFirst(C);
		}

		public virtual void RemoveChild(EventHandler C) {
			if (!Childern.Contains(C))
				return;
			C.Parent = null;
			Childern.Remove(C);
		}

		public virtual EventHandler GetParent() {
			return Parent;
		}

		public virtual EventHandler[] GetChildern() {
			return Childern.ToArray();
		}

		public virtual void SetParent(EventHandler P) {
			if (Parent != null)
				Parent.RemoveChild(this);
			P.AddChild(this);
		}

		public virtual AABB GetAABB() {
			return new AABB();
		}

		public virtual Vector2f Relative(Vector2f V) {
			return V - GetAABB().Pos;
		}

		public virtual Vector2f ParentRelative(Vector2f V) {
			return GetParent().Relative(V);
		}

		public virtual void Enable() {
			Enabled = true;
		}

		public virtual void Disable() {
			Enabled = false;
		}

		public virtual void Activate() {
			Active = true;
			EventHandler Parent = GetParent();
			if (Parent != null)
				Parent.DoActivate();
		}

		public virtual void Deactivate() {
			Active = false;
		}

		void DoActivate() {
			if (!Active)
				Activate();
		}

		void DoDeactivate() {
			var Childern = GetChildern();
			foreach (var Child in Childern)
				Child.DoDeactivate();
			if (Active)
				Deactivate();
		}

		public virtual bool OnMouse(MouseButtonEventArgs E, bool Down) {
			bool ValidChildern = false;

			if (Down)
				ActiveChild = null;

			var Childern = GetChildern();
			foreach (var Child in Childern) {
				if (!ValidChildern  && Child.GetAABB().IsInside(E.X, E.Y)) {
					if (Child.Enabled) {
						if (Down) {
							Child.DoDeactivate();
							Child.DoActivate();
							ActiveChild = Child;
						}
						Child.OnMouse(E, Down);
					}
					ValidChildern = true;
				} else if (Down)
					Child.DoDeactivate();
			}

			if (!ValidChildern && Down && Enabled && Active) {
				GrabbedHander = this;
				GrabPos = Relative(new Vector2f(E.X, E.Y));
			}

			return ValidChildern;
		}

		public virtual bool OnMouseMove(MouseMoveEventArgs E) {
			bool ValidChildern = false;
			var Childern = GetChildern();
			foreach (var Child in Childern)
				if (!ValidChildern && Child.GetAABB().IsInside(E.X, E.Y)) {
					if (Child.Enabled)
						Child.OnMouseMove(E);
					ValidChildern = true;
				}

			return ValidChildern;
		}

		public virtual bool OnMouseDrag(MouseMoveEventArgs E) {
			if (!Mouse.IsButtonPressed(Mouse.Button.Left)) {
				GrabbedHander = null;
				return true;
			}
			return false;
		}

		public virtual bool OnMouseClick(MouseButtonEventArgs E, bool IsDouble) {
			bool ValidChildern = false;
			var Childern = GetChildern();
			foreach (var Child in Childern)
				if (!ValidChildern && Child.GetAABB().IsInside(E.X, E.Y)) {
					if (Child.Enabled)
						Child.OnMouseClick(E, IsDouble);
					ValidChildern = true;
				}
			return ValidChildern;
		}

		public virtual bool OnKey(KeyEventArgs E, bool Down) {
			bool ValidChildern = false;
			if (ActiveChild != null && ActiveChild.Enabled) {
				ActiveChild.OnKey(E, Down);
				ValidChildern = true;
			}
			return ValidChildern;
		}

		public virtual bool OnKeyPress(TextEventArgs E) {
			bool ValidChildern = false;
			if (ActiveChild != null && ActiveChild.Enabled) {
				ActiveChild.OnKeyPress(E);
				ValidChildern = true;
			}
			return ValidChildern;
		}
	}
}