using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SFML.Graphics;
using SFML.System;

namespace Tq.Graphics {
	class RenderSprite : RenderTarget, Drawable, IDisposable {
		RenderTexture RT;
		Sprite RTSprite;

		public RenderSprite(int W, int H, bool DepthBuffer = false) {
			RT = new RenderTexture((uint)W, (uint)H, DepthBuffer);
			RT.Clear(Color.Black);
			RTSprite = new Sprite(RT.Texture);
		}

		public RenderSprite(Vector2u Size)
			: this((int)Size.X, (int)Size.Y) {
		}

		public void Dispose() {
			RT.Dispose();
			RTSprite.Dispose();
		}

		public View DefaultView {
			get {
				return RT.DefaultView;
			}
		}

		public Vector2u Size {
			get {
				return RT.Size;
			}
		}

		public Texture Texture {
			get {
				return RT.Texture;
			}
		}

		public void Clear() {
			RT.Clear();
		}

		public void Clear(Color color) {
			RT.Clear(color);
		}

		public void Draw(Drawable drawable) {
			RT.Draw(drawable);
		}

		public void Draw(Drawable drawable, RenderStates states) {
			RT.Draw(drawable, states);
		}

		public void Draw(Vertex[] vertices, PrimitiveType type) {
			RT.Draw(vertices, type);
		}

		public void Draw(Vertex[] vertices, PrimitiveType type, RenderStates states) {
			RT.Draw(vertices, type, states);
		}

		public void Draw(Vertex[] vertices, uint start, uint count, PrimitiveType type) {
			RT.Draw(vertices, start, count, type);
		}

		public void Draw(Vertex[] vertices, uint start, uint count, PrimitiveType type, RenderStates states) {
			RT.Draw(vertices, start, count, type, states);
		}

		public View GetView() {
			return RT.GetView();
		}

		public IntRect GetViewport(View view) {
			return RT.GetViewport(view);
		}

		public Vector2i MapCoordsToPixel(Vector2f point) {
			return RT.MapCoordsToPixel(point);
		}

		public Vector2i MapCoordsToPixel(Vector2f point, View view) {
			return RT.MapCoordsToPixel(point, view);
		}

		public Vector2f MapPixelToCoords(Vector2i point) {
			return RT.MapPixelToCoords(point);
		}

		public Vector2f MapPixelToCoords(Vector2i point, View view) {
			return RT.MapPixelToCoords(point, view);
		}

		public void PopGLStates() {
			RT.PopGLStates();
		}

		public void PushGLStates() {
			RT.PushGLStates();
		}

		public void ResetGLStates() {
			RT.ResetGLStates();
		}

		public void SetView(View view) {
			RT.SetView(view);
		}

		public void Display() {
			RT.Display();
		}

		public void Draw(RenderTarget target, RenderStates states) {
			RTSprite.Draw(target, states);
		}
	}
}