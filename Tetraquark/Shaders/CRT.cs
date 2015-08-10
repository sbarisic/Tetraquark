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
		static RenderStates CRTStates;

		public static RenderStates UseCRT(Texture Texture, float Blur = 0.1f, float Chromatic = 0.8f,
			float Lines = 0.9f, float Noise = 0.02f) {
			if (CRTStates.Shader == null)
				CRTStates = new RenderStates(CRT);

			CRTStates.Shader.SetParameter("texture", Texture);
			CRTStates.Shader.SetParameter("resolution", Texture.Size.ToVec2f());
			CRTStates.Shader.SetParameter("blur", Blur);
			CRTStates.Shader.SetParameter("chromatic", Chromatic);
			CRTStates.Shader.SetParameter("lines", Lines);
			CRTStates.Shader.SetParameter("noise", Noise);
			return CRTStates;
		}

		public static Shader CRT = Shader.FromString(@"
#version 110

void main() {
	gl_Position = gl_ModelViewProjectionMatrix * gl_Vertex;
	gl_TexCoord[0] = gl_TextureMatrix[0] * gl_MultiTexCoord0;
	gl_FrontColor = gl_Color;
}
", @"
#version 120

uniform sampler2D texture;
uniform vec2 resolution;
uniform float blur; // 0.1
uniform float chromatic; // 0.3
uniform float lines; // 0.9
uniform float noise; // 0.02

float Rand(vec2 co) {
	return fract(sin(dot(co.xy, vec2(12.9898, 78.233))) * 43758.5453);
}

vec2 Barrel(vec2 Coord) {
	Coord = Coord - resolution / 2.0;
	vec2 CoordCenter = Coord - 0.5;
	float Dist = dot(CoordCenter, CoordCenter);
	return (Coord + CoordCenter * Dist * 0.0000005) + resolution / 2.0;
}

vec4 GetPixel(vec2 Pos) {
	Pos = Barrel(Pos);
	if (Pos.x < 0 || Pos.y < 0 || Pos.x > resolution.x || Pos.y > resolution.y)
		discard;

	vec4 R = texture2D(texture, (Pos - vec2(chromatic, 0)) / resolution.xy);  
	vec4 G = texture2D(texture, (Pos) / resolution.xy);
	vec4 B = texture2D(texture, (Pos + vec2(chromatic, 0)) / resolution.xy);  
	return vec4(R.r, G.g, B.b, G.a);
}

vec4 Scanlines(vec3 Full, vec3 Empty, float AlphaMult) {
	float YCoord = Barrel(gl_TexCoord[0].xy * resolution).y;
	if (mod(floor(YCoord / 1), 2) == 0)
		return vec4(Full, AlphaMult);
	return vec4(Empty, AlphaMult);
}

vec4 Mix(vec4 A, vec4 B) {
	return mix(A, B, B.a);
}

vec4 Blur(float AlphaMult) {
	return Mix(GetPixel((gl_TexCoord[0].xy * resolution).xy),
				vec4(GetPixel((gl_TexCoord[0].xy * resolution).xy + vec2(1.0, 1.0)).xyz, AlphaMult));
}

void main() {
	vec4 Default = GetPixel((gl_TexCoord[0].xy * resolution).xy);
	Default = vec4(Mix(Default, Blur(blur)).rgb, Default.a);
	Default = Default * Scanlines(vec3(1.0), vec3(lines), 1.0);
	gl_FragColor = (Default + vec4(vec3(Rand(gl_TexCoord[0].xy) * noise), 0.0)) * gl_Color;
}
");

	}
}