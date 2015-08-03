using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using SFML.Graphics;
using SFML.Audio;

namespace Tq {
	static class ResourceMgr {
		static Dictionary<string, Font> Fonts;
		static Dictionary<string, Texture> Textures;

		static ResourceMgr() {
			Fonts = new Dictionary<string, Font>();
			Textures = new Dictionary<string, Texture>();
		}

		public static void Register<T>(string Path, string Name) {
			if (!File.Exists(Path))
				throw new Exception("Resource doesn't exist: " + Path);
			Register<T>(File.OpenRead(Path), Name);
		}

		public static void Register<T>(Stream S, string Name) {
			string Log = string.Format("Registering {0} '{1}' ... ", typeof(T).Name, Name);
			try {
				if (typeof(T) == typeof(Font))
					Fonts.Set(Name, new Font(S));
				else if (typeof(T) == typeof(Texture))
					Textures.Set(Name, new Texture(S));

				Log += "OK";
			} catch (Exception E) {
				Log += "FAIL\n" + E.Message;
			}
			Console.WriteLine(Log);
		}

		public static T Get<T>(string Name) where T : class {
			if (typeof(T) == typeof(Font))
				return Fonts[Name] as T;
			else if (typeof(T) == typeof(Texture))
				return Textures[Name] as T;
			else
				throw new Exception("Unsupported");
		}
	}
}