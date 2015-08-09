using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Threading.Tasks;
using System.IO;
using System.Yaml;
using System.Yaml.Serialization;

namespace Tq {
	[AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
	sealed class SettingAttribute : Attribute {
		public SettingAttribute() {
		}
	}

	static class Settings {
		static void ForeachSetting(Action<FieldInfo, SettingAttribute> F) {
			Assembly Cur = Assembly.GetExecutingAssembly();
			Type[] Types = Cur.GetTypes();
			for (int i = 0; i < Types.Length; i++) {
				FieldInfo[] Fields = Types[i].GetFields(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
				for (int j = 0; j < Fields.Length; j++)
					if (Attribute.IsDefined(Fields[j], typeof(SettingAttribute)))
						F(Fields[j], Fields[j].GetCustomAttribute<SettingAttribute>());
			}
		}

		public static void Load(string YamlSrc) {
			YamlMapping Yaml = (YamlMapping)YamlNode.FromYaml(YamlSrc)[0];
			ForeachSetting((F, A) => {
				if (Yaml.ContainsKey(F.Name))
					F.SetValue(null, ((YamlScalar)Yaml[F.Name]).NativeObject);
			});
		}

		public static string Save() {
			YamlMapping Map = new YamlMapping();
			YamlSerializer Serializer = new YamlSerializer();

			ForeachSetting((F, A) => {
				// HAX
				Map.Add(F.Name, YamlNode.FromYaml(Serializer.Serialize(F.GetValue(null)))[0]);

			});
			return Map.ToYaml();
		}
	}
}