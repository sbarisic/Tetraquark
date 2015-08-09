using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SFML.Graphics;

namespace Tq.Graphics {
	struct HSVColor {
		public double H;
		public byte S, V;

		public HSVColor(double H, byte S, byte V) {
			this.H = H;
			this.S = S;
			this.V = V;
		}

		public static implicit operator Color(HSVColor HSV) {
			double H = HSV.H % 360;
			double S = (double)HSV.S / 255;
			double V = (double)HSV.V / 255;
			double HH, P, Q, T, FF, R, G, B;
			long I;

			if (S <= 0.0) {
				R = V;
				G = V;
				B = V;
				return new Color((byte)(R * 255), (byte)(G * 255), (byte)(B * 255));
			}
			HH = H;
			if (HH >= 360.0)
				HH = 0.0;
			HH /= 60.0;
			I = (long)HH;
			FF = HH - I;
			P = V * (1.0 - S);
			Q = V * (1.0 - (S * FF));
			T = V * (1.0 - (S * (1.0 - FF)));

			switch (I) {
				case 0:
					R = V;
					G = T;
					B = P;
					break;
				case 1:
					R = Q;
					G = V;
					B = P;
					break;
				case 2:
					R = P;
					G = V;
					B = T;
					break;

				case 3:
					R = P;
					G = Q;
					B = V;
					break;
				case 4:
					R = T;
					G = P;
					B = V;
					break;
				case 5:
				default:
					R = V;
					G = P;
					B = Q;
					break;
			}
			return new Color((byte)(R * 255), (byte)(G * 255), (byte)(B * 255));
		}

		public static implicit operator HSVColor(Color Col) {
			double R = (double)Col.R / 255;
			double G = (double)Col.G / 255;
			double B = (double)Col.B / 255;
			double H, S, V, Min, Max, Delta;

			Min = R < G ? R : G;
			Min = Min < B ? Min : B;

			Max = R > G ? R : G;
			Max = Max > B ? Max : B;

			V = Max;
			Delta = Max - Min;
			if (Max > 0.0)
				S = (Delta / Max);
			else {
				S = 0.0;
				H = double.NaN;
				return new HSVColor(0, (byte)(S * 255), (byte)(V * 255));
			}
			if (R >= Max)
				H = (G - B) / Delta;
			else
				if (G >= Max)
					H = 2.0 + (B - R) / Delta;
				else
					H = 4.0 + (R - G) / Delta;

			H *= 60.0;

			if (H < 0.0)
				H += 360.0;

			return new HSVColor(H, (byte)(S * 255), (byte)(V * 255));
		}
	}
}