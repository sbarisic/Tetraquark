using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Threading;

using OpenTK;
using OpenTK.Platform;
using OpenTK.Graphics.OpenGL;
using GraphicsMode = OpenTK.Graphics.GraphicsMode;
using ColorFormat = OpenTK.Graphics.ColorFormat;
using GraphicsContext = OpenTK.Graphics.GraphicsContext;
using GfxCtxFlags = OpenTK.Graphics.GraphicsContextFlags;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using View = SFML.Graphics.View;

namespace Tq {
	static class Program {
		public static bool Running;
		public static float GameTime {
			get {
				return (float)GameWatch.ElapsedMilliseconds / 1000;
			}
		}

		static Stopwatch GameWatch;

		// 1280 x 720
		// 960 x 540
		// 864 x 486
		[Setting]
		public static int ResX = 960;
		[Setting]
		public static int ResY = 540;
		[Setting]
		public static bool Debug = false;
		[Setting]
		public static bool Border = true;
		[Setting]
		public static int BitsPerPixel = 32;
		[Setting]
		public static int DepthBits = 16;
		[Setting]
		public static int StencilBits = 16;
		[Setting]
		public static int Samples = 0;

		[STAThread]
		static void Main() {
			Console.Title = "Tetraquark Console";
			Running = true;

			const string ConfigFile = "cfg.yaml";
			if (File.Exists(ConfigFile)) {
				Console.WriteLine("Loading config file {0}", ConfigFile);
				Settings.Load(File.ReadAllText(ConfigFile));
			}

			GameWatch = new Stopwatch();
			GameWatch.Start();
			PackMgr.Mount("Fonts.zip");
			PackMgr.Mount("ConsoleFont.zip");

			string[] Files = PackMgr.GetFiles();
			for (int i = 0; i < Files.Length; i++) {
				if (Files[i].StartsWith("resources/fonts/"))
					ResourceMgr.Register<Font>(PackMgr.OpenFile(Files[i]), Path.GetFileNameWithoutExtension(Files[i]));
				else if (Files[i].StartsWith("resources/textures/"))
					ResourceMgr.Register<Texture>(PackMgr.OpenFile(Files[i]), Path.GetFileNameWithoutExtension(Files[i]));
			}

			Scales.Init(new Vector2f(ResX, ResY));

			// OpenTK
			ToolkitOptions TOpt = ToolkitOptions.Default;
			TOpt.Backend = PlatformBackend.PreferNative;
			TOpt.EnableHighResolution = true;
			Toolkit.Init(TOpt);
			// SFML
			VideoMode VMode = new VideoMode((uint)Scales.XRes, (uint)Scales.YRes, (uint)BitsPerPixel);
			ContextSettings CSet = new ContextSettings((uint)DepthBits, (uint)StencilBits);
			CSet.MajorVersion = 4;
			CSet.MinorVersion = 2;
			CSet.AntialiasingLevel = (uint)Samples;
			Styles S = Styles.None;
			if (Border)
				S |= Styles.Titlebar | Styles.Close;
			RenderWindow RWind = new RenderWindow(VMode, "Tetraquark", S, CSet);
			RWind.Closed += (Snd, E) => Running = false;
			RWind.SetKeyRepeatEnabled(false);
			// OpenTK
			IWindowInfo WindInfo = Utilities.CreateWindowsWindowInfo(RWind.SystemHandle);
			var GfxMode = new GraphicsMode(new ColorFormat(BitsPerPixel), DepthBits, StencilBits, Samples, new ColorFormat(0));
			var GfxCtx = new GraphicsContext(GfxMode, WindInfo, (int)CSet.MajorVersion, (int)CSet.MinorVersion,
				GfxCtxFlags.Default);
			GfxCtx.MakeCurrent(WindInfo);
			GfxCtx.LoadAll();
			RWind.ResetGLStates();

			//GL.Enable(EnableCap.FramebufferSrgb);

			Renderer.Init(RWind);
			Stopwatch Clock = new Stopwatch();
			Clock.Start();

			while (Running) {
				RWind.DispatchEvents();
				while (Clock.ElapsedMilliseconds < 10)
					;
				float Dt = (float)Clock.ElapsedMilliseconds / 1000;
				Clock.Restart();
				Renderer.Update(Dt);
				Renderer.Draw(RWind);
				RWind.Display();
			}

			RWind.Close();
			RWind.Dispose();
			Console.WriteLine("Flushing configs");
			File.WriteAllText(ConfigFile, Settings.Save());
			Environment.Exit(0);
		}
	}
}