using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SFML.Graphics;
using SFML.Window;

namespace Tq {
	static partial class Shaders {
		public static Shader CRT = Shader.FromString(@"
void main() {
	gl_Position = gl_ModelViewProjectionMatrix * gl_Vertex;
	gl_TexCoord[0] = gl_TextureMatrix[0] * gl_MultiTexCoord0;
	gl_FrontColor = gl_Color;
}
", @"
uniform sampler2D texture;
uniform vec2 resolution;
uniform float blur; // 0.1
uniform float chromatic; // 0.3
uniform float lines; // 0.9

vec2 Barrel(vec2 coord) {
	coord = coord - resolution / 2;
	vec2 cc = coord - 0.5;
	float dist = dot(cc, cc);
	return (coord + cc * dist * 0.0000005) + resolution / 2;
}

vec4 GetPixel(vec2 Pos) {
	Pos = Barrel(Pos);
	if (Pos.x < 0 || Pos.y < 0 || Pos.x > resolution.x || Pos.y > resolution.y)
		discard;

	vec4 r = texture2D(texture, (Pos - vec2(chromatic, 0)) / resolution.xy);  
	vec4 g = texture2D(texture, (Pos) / resolution.xy);
	vec4 b = texture2D(texture, (Pos + vec2(chromatic, 0)) / resolution.xy);  
	return vec4(r.r, g.g, b.b, r.a);
}

vec4 Scanlines(vec3 FULL, vec3 EMPTY, float AlphaMult) {
	float yCoord = Barrel(gl_TexCoord[0].xy * resolution).y;
	if (mod(floor(yCoord / 1), 2) == 0)
		return vec4(FULL, AlphaMult);
	return vec4(EMPTY, AlphaMult);
}

vec4 MIX(vec4 A, vec4 B) {
	return mix(A, B, B.a);
}

vec4 Blur(float AlphaMult) {
	return MIX(GetPixel((gl_TexCoord[0].xy * resolution).xy),
				vec4(GetPixel((gl_TexCoord[0].xy * resolution).xy + vec2(1.0, 1.0)).xyz, AlphaMult));
}

void main() {
	vec4 Default = GetPixel((gl_TexCoord[0].xy * resolution).xy);
	Default = vec4(MIX(Default, Blur(blur)).rgb, Default.a);
	Default = Default * Scanlines(vec3(1.0), vec3(lines), 1.0);
	gl_FragColor = Default * gl_Color;
}
");

	}
}
