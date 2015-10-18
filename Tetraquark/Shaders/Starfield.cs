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
		static RenderStates StarfieldStates;

		public static RenderStates UseStarfield(Vector2f Position, float Rotation, Vector2f Resolution,
			float Additive = 0.2f, float Subtractive = 0.04f, float Reductive = 0.2f, float Iterations = 4) {
			if (StarfieldStates.Shader == null)
				StarfieldStates = new RenderStates(Starfield);

			StarfieldStates.Shader.SetParameter("pos", Position);
			StarfieldStates.Shader.SetParameter("rot", Rotation * (float)Math.PI / 180.0f);
			StarfieldStates.Shader.SetParameter("res", Resolution);
			StarfieldStates.Shader.SetParameter("additive", Additive);
			StarfieldStates.Shader.SetParameter("subtractive", Subtractive);
			StarfieldStates.Shader.SetParameter("reductive", Reductive);
			StarfieldStates.Shader.SetParameter("iterations", Iterations);
			return StarfieldStates;
		}

		public static Shader Starfield = Shader.FromString(@"
#version 110

void main() {
	gl_Position = gl_ModelViewProjectionMatrix * gl_Vertex;
	gl_TexCoord[0] = gl_TextureMatrix[0] * gl_MultiTexCoord0;
	gl_FrontColor = gl_Color;
}
", @"
#version 120

uniform vec2 pos;
uniform float rot;
uniform vec2 res;
uniform float additive; // 0.4
uniform float subtractive; // 0.06
uniform float reductive; // 0.1
uniform float iterations;

float Rand(vec2 co) {
	return fract(sin(dot(co.xy, vec2(12.9898, 78.233))) * 43758.5453);
}

vec3 GetStarColor(vec2 Pos, float Tresh) {
	vec3 Clr = vec3(0, 0, 0);
	vec3 Add = vec3(additive);
	vec3 Sub = vec3(subtractive);
	vec2 Off = vec2(4245.72589416, 4164.14236874);

	mat2 RotationMatrix = mat2(cos(-rot), -sin(-rot), sin(-rot), cos(-rot));
	vec2 SS = (gl_TexCoord[0].xy * res - res / 2) * RotationMatrix + res / 2;
	SS *= vec2(1, -1);

	float i = 0;
	for (; i < iterations; i += 1)
		if (Rand(floor(SS + (pos + Off * i) * (1.0 - reductive * i)) / res) > Tresh)
			Clr += Add - Sub * i;

	return Clr;
}

void main() {
	gl_FragColor = vec4(GetStarColor(pos, 0.997), 1);
}
");

	}
}