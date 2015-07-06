using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using SFML.System;

namespace Tq.Atomic {
	class EventHandler {
		public static EventHandler GrabbedHander;

		protected HashSet<EventHandler> Childern;
		protected bool Enabled, Active;
		protected EventHandler ActiveChild;
		EventHandler Parent;

		public EventHandler() {
			Childern = new HashSet<EventHandler>();
			Enable();
		}

		public virtual void AddChild(EventHandler C) {
			if (Childern.Contains(C))
				return;
			if (C.Parent != null)
				C.Parent.RemoveChild(C);
			C.Parent = this;
			Childern.Add(C);
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

		public virtual void Enable() {
			Enabled = true;
		}

		public virtual void Disable() {
			Enabled = false;
		}

		public virtual void Activate() {
			Active = true;
			if (GetParent() != null)
				GetParent().DoActivate();
		}

		public virtual void Deactivate() {
			Active = false;
		}

		void DoActivate() {
			if (!Active)
				Activate();
		}

		void DoDeactivate() {
			foreach (var Child in Childern)
				Child.DoDeactivate();
			if (Active)
				Deactivate();
		}

		public virtual bool OnMouse(MouseEventArgs E, bool Down) {
			bool ValidChildern = false;

			if (!Down)
				ActiveChild = null;
			foreach (var Child in Childern) {
				if (Child.Enabled && Child.GetAABB().IsInside(E.X, E.Y)) {
					if (!Down) {
						Child.DoActivate();
						ActiveChild = Child;
					}
					Child.OnMouse(E, Down);
					ValidChildern = true;
				} else if (!Down)
					Child.DoDeactivate();
			}

			if (!ValidChildern && Down)
				GrabbedHander = this;
			return ValidChildern;
		}

		public virtual bool OnMouseMove(MouseEventArgs E) {
			bool ValidChildern = false;

			foreach (var Child in Childern)
				if (Child.Enabled && Child.GetAABB().IsInside(E.X, E.Y)) {
					Child.OnMouseMove(E);
					ValidChildern = true;
				}

			/*if (!(MouseDown && E.Button != MouseButtons.None && !ValidChildern)){
				MouseDown = false;
				GrabbedHander = null;
			}*/
			return ValidChildern;
		}

		public virtual bool OnMouseDrag(MouseEventArgs E) {
			if (E.Button == MouseButtons.None) {
				GrabbedHander = null;
				return true;
			}
			return false;
		}

		public virtual bool OnMouseClick(MouseEventArgs E, bool IsDouble) {
			bool ValidChildern = false;
			foreach (var Child in Childern)
				if (Child.Enabled && Child.GetAABB().IsInside(E.X, E.Y)) {
					Child.OnMouseClick(E, IsDouble);
					ValidChildern = true;
				}
			return ValidChildern;
		}

		public virtual bool OnKey(KeyEventArgs E, bool Down) {
			bool ValidChildern = false;
			if (ActiveChild != null) {
				ActiveChild.OnKey(E, Down);
				ValidChildern = true;
			}
			return ValidChildern;
		}

		public virtual bool OnKeyPress(KeyPressEventArgs E) {
			bool ValidChildern = false;
			if (ActiveChild != null) {
				ActiveChild.OnKeyPress(E);
				ValidChildern = true;
			}
			return ValidChildern;
		}
	}
}