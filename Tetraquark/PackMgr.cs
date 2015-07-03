using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IO.Compression;

namespace Tq {
	static class PackMgr {
		static List<Tuple<string, ZipArchive>> Packs;
		static Dictionary<string, ZipArchiveEntry> Files;

		static PackMgr() {
			Packs = new List<Tuple<string, ZipArchive>>();
			Files = new Dictionary<string, ZipArchiveEntry>();
		}

		static void RebuildFiles() {
			Files.Clear();
			for (int i = Packs.Count - 1; i >= 0; i--) {
				ZipArchive Archive = Packs[i].Item2;
				for (int j = 0; j < Archive.Entries.Count; j++)
					if (!Files.ContainsKey(Archive.Entries[j].FullName) && !Archive.Entries[j].FullName.EndsWith("/"))
						Files.Add(Archive.Entries[j].FullName, Archive.Entries[j]);
			}
		}

		static void EnsureExists(string Pth) {
			if (!Files.ContainsKey(Pth))
				throw new Exception("Mounted file not found: " + Pth);
		}

		static void SanitizePath(ref string Pth) {
			Pth = Pth.Replace('\\', '/');
			Pth = Pth.Replace("/./", "/");
			if (Pth.StartsWith("/"))
				Pth = Pth.Substring(1);
			if (Pth.Contains("/../"))
				throw new Exception("/../ not allowed");
		}

		static ZipArchive GetPack(string Pack, out int Idx) {
			SanitizePath(ref Pack);
			Idx = -1;
			for (int i = 0; i < Packs.Count; i++)
				if (Packs[i].Item1 == Pack) {
					Idx = i;
					return Packs[i].Item2;
				}
			return null;
		}

		static ZipArchive GetPack(string Pack) {
			SanitizePath(ref Pack);
			int Idx;
			return GetPack(Pack, out Idx);
		}

		public static void Mount(string Pack) {
			SanitizePath(ref Pack);
			if (!File.Exists(Pack))
				throw new Exception("Pack file does not exist: " + Pack);
			if (GetPack(Pack) != null)
				throw new Exception("Pack already mounted: " + Pack);

			Console.WriteLine("Mounting {0}", Pack);
			Packs.Add(new Tuple<string, ZipArchive>(Pack, ZipFile.OpenRead(Pack)));
			RebuildFiles();
		}

		public static void Unmount(string Pack) {
			SanitizePath(ref Pack);
			int Idx = -1;
			ZipArchive Pck = GetPack(Pack, out Idx);
			if (Pck == null)
				throw new Exception("Pack not mounted: " + Pack);

			Console.WriteLine("Unmounting {0}", Pack);
			Packs.RemoveAt(Idx);
			RebuildFiles();
		}

		public static string[] GetFiles() {
			return Files.Keys.ToArray();
		}

		public static Stream OpenFile(string Pth) {
			SanitizePath(ref Pth);
			EnsureExists(Pth);
			
			Stream S = Files[Pth].Open();
			MemoryStream MS = new MemoryStream();
			S.CopyTo(MS);
			S.Close();
			return MS;
		}

		public static byte[] ReadAllBytes(string Pth) {
			MemoryStream MS = new MemoryStream();
			Stream S = OpenFile(Pth);
			S.CopyTo(MS);
			S.Close();
			return MS.ToArray();
		}

		public static string ReadAllText(string Pth) {
			Stream S = OpenFile(Pth);
			StreamReader SR = new StreamReader(S);
			return SR.ReadToEnd();
		}
	}
}