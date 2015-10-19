using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Physics;

namespace Tq.Game {
	static class SI {
		public static IUnitSystem System = UnitSystemFactory.CreateSystem("SI");

		//Base units
		public static Unit m = System.AddBaseUnit("m", "meter");
		public static Unit kg = System.AddBaseUnit("kg", "kilogram", true);
		public static Unit s = System.AddBaseUnit("s", "second");
		public static Unit A = System.AddBaseUnit("A", "ampere");
		public static Unit K = System.AddBaseUnit("K", "kelvin");
		public static Unit mol = System.AddBaseUnit("mol", "mole");
		public static Unit cd = System.AddBaseUnit("cd", "candela");

		//Derived units
		public static Unit a = System.AddDerivedUnit("a", "acceleration", m / (s ^ 2));
		public static Unit Hz = System.AddDerivedUnit("Hz", "hertz", s ^ -1);
		public static Unit N = System.AddDerivedUnit("N", "newton", kg * m * (s ^ -2));
		public static Unit Pa = System.AddDerivedUnit("Pa", "pascal", N * (m ^ -2));
		public static Unit J = System.AddDerivedUnit("J", "joule", N * m);
		public static Unit W = System.AddDerivedUnit("W", "watt", J / s);
		public static Unit C = System.AddDerivedUnit("C", "coulomb", s * A);
		public static Unit V = System.AddDerivedUnit("V", "volt", W / A);
		public static Unit v = System.AddDerivedUnit("v", "velocity", m / s);
		public static Unit F = System.AddDerivedUnit("F", "farad", C / V);
		//public static Unit Ω = System.AddDerivedUnit("Ω", "joule", V / A);
		public static Unit S = System.AddDerivedUnit("S", "siemens", A / V);
		public static Unit Wb = System.AddDerivedUnit("Wb", "weber", V * s);
		public static Unit T = System.AddDerivedUnit("T", "tesla", Wb * (s ^ -2));
		public static Unit H = System.AddDerivedUnit("H", "inductance", Wb / A);
		public static Unit lx = System.AddDerivedUnit("lx", "immulinance", (m ^ -2) * cd);
		public static Unit Sv = System.AddDerivedUnit("Sv", "sievert", J / kg);
		public static Unit kat = System.AddDerivedUnit("kat", "katal", (s ^ -1) * mol);
		public static Unit ρ = System.AddDerivedUnit("ρ", "density", kg / (m ^ 3));
		public static Unit dm = System.AddDerivedUnit("dm", "decimeter", m / 10);
		public static Unit cm = System.AddDerivedUnit("cm", "centimeter", dm / 10);
		public static Unit mm = System.AddDerivedUnit("mm", "milimeter", cm / 10);

		//Incoherent units
		public static Unit min = System.AddDerivedUnit("min", "minute", 60 * s);
		public static Unit h = System.AddDerivedUnit("h", "hour", 60 * min);

		// Game specific
		//public static float MetersPerPixel = 1.0f / 16;
		public static Unit px = System.AddDerivedUnit("px", "pixel", m / 16);
	}

	static class Constants {
		public static Quantity G = SI.System.AddDerivedUnit("Nm^2/Kg^2", "Nm^2/Kg^2",
			(SI.N * (SI.m ^ 2)) / (SI.kg ^ 2)).Quant(6.674e-11);

		public static Quantity c = SI.v.Quant(299792458);
	}

	static class Density {
		public static Quantity Helium = SI.ρ.Quant(0.179);
		public static Quantity Aerographite = SI.ρ.Quant(0.2);
		public static Quantity Aerogel = SI.ρ.Quant(1.0);
		public static Quantity Air = SI.ρ.Quant(1.2);
		public static Quantity TungstenHexafluoride = SI.ρ.Quant(12.4);
		public static Quantity LiquidHydrogen = SI.ρ.Quant(70);
		public static Quantity Styrofoam = SI.ρ.Quant(75);
		public static Quantity Cork = SI.ρ.Quant(240);
		public static Quantity Pine = SI.ρ.Quant(373);
		public static Quantity Lithium = SI.ρ.Quant(535);
		public static Quantity Wood = SI.ρ.Quant(700);
		public static Quantity Oak = SI.ρ.Quant(710);
		public static Quantity Potassium = SI.ρ.Quant(860);
		public static Quantity Sodium = SI.ρ.Quant(970);
		public static Quantity Ice = SI.ρ.Quant(916.7);
		public static Quantity Water = SI.ρ.Quant(1000);
		public static Quantity Nylon = SI.ρ.Quant(1150);
		public static Quantity Plastics = SI.ρ.Quant(1175);
		public static Quantity Tetrachloroethene = SI.ρ.Quant(1622);
		public static Quantity Magnesium = SI.ρ.Quant(1740);
		public static Quantity Beryllium = SI.ρ.Quant(1850);
		public static Quantity Glycerol = SI.ρ.Quant(1261);
		public static Quantity Concrete = SI.ρ.Quant(2000);
		public static Quantity Silicon = SI.ρ.Quant(2330);
		public static Quantity Aluminium = SI.ρ.Quant(2700);
		public static Quantity Diiodomethane = SI.ρ.Quant(3325);
		public static Quantity Diamond = SI.ρ.Quant(3500);
		public static Quantity Titanium = SI.ρ.Quant(4540);
		public static Quantity Selenium = SI.ρ.Quant(4800);
		public static Quantity Vanadium = SI.ρ.Quant(6100);
		public static Quantity Antimony = SI.ρ.Quant(6690);
		public static Quantity Zinc = SI.ρ.Quant(7000);
		public static Quantity Chromium = SI.ρ.Quant(7200);
		public static Quantity Tin = SI.ρ.Quant(7310);
		public static Quantity Manganese = SI.ρ.Quant(7325);
		public static Quantity Iron = SI.ρ.Quant(7870);
		public static Quantity Niobium = SI.ρ.Quant(8570);
		public static Quantity Brass = SI.ρ.Quant(8600);
		public static Quantity Cadmium = SI.ρ.Quant(8650);
		public static Quantity Cobalt = SI.ρ.Quant(8900);
		public static Quantity Nickel = SI.ρ.Quant(8900);
		public static Quantity Copper = SI.ρ.Quant(8940);
		public static Quantity Bismuth = SI.ρ.Quant(9750);
		public static Quantity Molybdenum = SI.ρ.Quant(10220);
		public static Quantity Silver = SI.ρ.Quant(10500);
		public static Quantity Lead = SI.ρ.Quant(11340);
		public static Quantity Thorium = SI.ρ.Quant(11700);
		public static Quantity Rhodium = SI.ρ.Quant(12410);
		public static Quantity Mercury = SI.ρ.Quant(13546);
		public static Quantity Tantalum = SI.ρ.Quant(16600);
		public static Quantity Uranium = SI.ρ.Quant(18800);
		public static Quantity Tungsten = SI.ρ.Quant(19300);
		public static Quantity Gold = SI.ρ.Quant(19320);
		public static Quantity Plutonium = SI.ρ.Quant(19840);
		public static Quantity Platinum = SI.ρ.Quant(21450);
		public static Quantity Iridium = SI.ρ.Quant(22420);
		public static Quantity Osmium = SI.ρ.Quant(22570);
	}
}