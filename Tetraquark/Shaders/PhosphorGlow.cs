using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SFML.System;
using SFML.Graphics;
using SFML.Window;

namespace Tq {
	static partial class Shaders {
		static RenderStates PhosphorGlowStates;

		public static RenderStates UsePhosphorGlow(Texture TexA, Texture TexB, float Sub) {
			if (PhosphorGlowStates.Shader == null)
				PhosphorGlowStates = new RenderStates(PhosphorGlow);

			PhosphorGlowStates.Shader.SetParameter("tex_a", TexA);
			PhosphorGlowStates.Shader.SetParameter("tex_b", TexB);
			PhosphorGlowStates.Shader.SetParameter("sub", Sub);
			return PhosphorGlowStates;
		}

		public static Shader PhosphorGlow = Shader.FromString(@"
#version 110

void main() {
	gl_Position = gl_ModelViewProjectionMatrix * gl_Vertex;
	gl_TexCoord[0] = gl_TextureMatrix[0] * gl_MultiTexCoord0;
	gl_FrontColor = gl_Color;
}
", @"
#version 110

uniform sampler2D tex_a;
uniform sampler2D tex_b;
uniform float sub;

float rtz(float F) {
	if (F < 0.01)
		return 0;
	return F;
}

void main() {
	vec4 old = texture2D(tex_a, gl_TexCoord[0].xy);
	vec4 new = texture2D(tex_b, gl_TexCoord[0].xy);
	if (old.r <	new.r + sub)
		old.r = new.r + sub;
	if (old.g <	new.g + sub)
		old.g = new.g + sub;
	if (old.b <	new.b + sub)
		old.b = new.b + sub;
	if (old.a <	new.a + sub)
		old.a = new.a + sub;
	gl_FragColor = old - sub;
}
");

	}
}