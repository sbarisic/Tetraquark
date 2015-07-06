using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Tq.GUI {
	class Base : Atomic.EventHandler, Drawable, RenderTarget {
		internal RenderTexture RT;
		internal Sprite RTSprite;
		internal bool Invalid;

		public Base(Vector2f Pos, Vector2u Size) {
			RT = new RenderTexture(Size.X, Size.Y);
			RTSprite = new Sprite(RT.Texture);
			RTSprite.Position = Pos;
			Invalidate();
		}

		public virtual View DefaultView {
			get {
				return RT.DefaultView;
			}
		}

		public virtual Vector2u Size {
			get {
				return RT.Size;
			}
		}

		public virtual Transform InverseTransform {
			get {
				return RTSprite.InverseTransform;
			}
		}

		public virtual Vector2f Origin {
			get {
				return RTSprite.Origin;
			}
			set {
				RTSprite.Origin = value;
			}
		}

		public virtual Vector2f Position {
			get {
				return RTSprite.Position;
			}
			set {
				RTSprite.Position = value;
			}
		}

		public virtual float Rotation {
			get {
				return RTSprite.Rotation;
			}
			set {
				RTSprite.Rotation = value;
			}
		}

		public virtual Vector2f Scale {
			get {
				return RTSprite.Scale;
			}
			set {
				RTSprite.Scale = value;
			}
		}

		public virtual Transform Transform {
			get {
				return RTSprite.Transform;
			}
		}

		public virtual void Draw(RenderStates RS) {
		}

		public virtual void DrawChildern(RenderStates RS) {
			foreach (var Child in Childern)
				if (Child is Base)
					Draw((Drawable)Child, RS);
		}

		public override AABB GetAABB() {
			Vector2f Pos = Position;
			if (GetParent() != null)
				Pos += GetParent().GetAABB().Pos;
			return new AABB(Pos, Size.ToVec2f());
		}

		public virtual void Invalidate() {
			Invalid = true;
			Atomic.EventHandler Parent = GetParent();
			if (Parent != null && Parent is Base)
				((Base)Parent).Invalidate();
		}

		public virtual void Clear() {
			RT.Clear();
		}

		public virtual void Clear(Color color) {
			RT.Clear(color);
		}

		public virtual void Draw(RenderTarget target, RenderStates states) {
			if (Invalid) {
				Invalid = false;
				Clear(Color.Transparent);
				Draw(states);
				DrawChildern(states);
				RT.Display();
			}
			RTSprite.Draw(target, states);
		}

		public virtual void Draw(Drawable drawable) {
			RT.Draw(drawable);
		}

		public virtual void Draw(Drawable drawable, RenderStates states) {
			RT.Draw(drawable, states);
		}

		public virtual void Draw(Vertex[] vertices, PrimitiveType type) {
			RT.Draw(vertices, type);
		}

		public virtual void Draw(Vertex[] vertices, PrimitiveType type, RenderStates states) {
			RT.Draw(vertices, type, states);
		}

		public virtual void Draw(Vertex[] vertices, uint start, uint count, PrimitiveType type) {
			RT.Draw(vertices, start, count, type);
		}

		public virtual void Draw(Vertex[] vertices, uint start, uint count, PrimitiveType type, RenderStates states) {
			RT.Draw(vertices, start, count, type, states);
		}

		public virtual View GetView() {
			return RT.GetView();
		}

		public virtual IntRect GetViewport(View view) {
			return RT.GetViewport(view);
		}

		public virtual Vector2i MapCoordsToPixel(Vector2f point) {
			return RT.MapCoordsToPixel(point);
		}

		public virtual Vector2i MapCoordsToPixel(Vector2f point, View view) {
			return RT.MapCoordsToPixel(point, view);
		}

		public virtual Vector2f MapPixelToCoords(Vector2i point) {
			return RT.MapPixelToCoords(point);
		}

		public virtual Vector2f MapPixelToCoords(Vector2i point, View view) {
			return RT.MapPixelToCoords(point, view);
		}

		public virtual void PopGLStates() {
			RT.PopGLStates();
		}

		public virtual void PushGLStates() {
			RT.PushGLStates();
		}

		public virtual void ResetGLStates() {
			RT.ResetGLStates();
		}

		public virtual void SetView(View view) {
			RT.SetView(view);
		}
	}
}