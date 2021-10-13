using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadarScreen.DataAccess
{
    public class PointTransform
    {
		public double a = 0.0, f = 0.0;

		public PointTransform(int Referance)
        {
			Dictionary<int, Double[]> referanceElipsoid = new Dictionary<int, Double[]>();
			referanceElipsoid.Add(1, new double[] { 6378137.0, 1.0 / 298.257222101 });
			referanceElipsoid.Add(2, new double[] { 6377397.155, 1.0 / 299.1528128 });
			referanceElipsoid.Add(3, new double[] { 6378388.0 , 1.0 / 297.0 });

			a = referanceElipsoid[Referance][0];
			f = referanceElipsoid[Referance][1];

		}
		public double[] Geodetic_Curve(double[] cartesian)
		{
			double B = 0.0, L = 0.0, h = 0.0;

			double b = a * (1.0 - f);
			double e = (2.0 * f - f * f) / (1.0 - 2.0 * f + f * f);
			double c = a * a / b;
			double T = cartesian[2] * (1.0 + e) / Math.Sqrt(cartesian[0] * cartesian[0] + cartesian[1] * cartesian[1]);
			double T0;

			do
			{
				T0 = T;
				double C = 1.0 / (1.0 + T0 * T0);
				double N = c / Math.Sqrt(1.0 + e * C);
				h = Math.Sqrt((cartesian[0] * cartesian[0] + cartesian[1] * cartesian[1]) / C) - N;
				T = cartesian[2] * (1.0 + e) / Math.Sqrt(cartesian[0] * cartesian[0] + cartesian[1] * cartesian[1]) / (1.0 + e * h / (N + h));
			}
			while (Math.Abs(T - T0) > 1.0e-5);

			B = Math.Atan(T);

			if (cartesian[0] < 0.0)
				L = -(Math.Asin(cartesian[1] / Math.Sqrt(cartesian[0] * cartesian[0] + cartesian[1] * cartesian[1]))) + Math.PI;
			else
				L = Math.Asin(cartesian[1] / Math.Sqrt(cartesian[0] * cartesian[0] + cartesian[1] * cartesian[1]));

			return new double[] { B, L, h };
		}

		public double[] Cartesian(double[] geodetic_Curve)
		{
			double Xs = 0.0, Ys = 0.0, Zs = 0.0;

			double e = (2.0 * f - f * f);
			double e_ = e / (1.0 - e);
			double N = a / Math.Sqrt(1.0 - e * e * Math.Sin(geodetic_Curve[0]) * Math.Sin(geodetic_Curve[0]));

			Xs = (N + geodetic_Curve[2]) * Math.Cos(geodetic_Curve[0]) * Math.Cos(geodetic_Curve[1]);
			Ys = (N + geodetic_Curve[2]) * Math.Cos(geodetic_Curve[0]) * Math.Sin(geodetic_Curve[1]);
			Zs = ((N / (1.0 + e_)) + geodetic_Curve[2]) * Math.Sin(geodetic_Curve[0]);

			return new double[]{Xs, Ys, Zs};

		}

		public double[] Local_Geodetic(double[] cartesianSatellite , double[] cartesianEarth)
		{
			double Alfa = 0.0, Zr = 0.0, S = 0.0;
			double[] geodeticEarth = Geodetic_Curve(cartesianEarth);
			double[] cartesianDistace = new double[]
				{
					cartesianSatellite[0] - cartesianEarth[0],
					cartesianSatellite[1] - cartesianEarth[1],
					cartesianSatellite[2] - cartesianEarth[2]
				};

			double n = -cartesianDistace[0] * Math.Sin(geodeticEarth[0]) * Math.Cos(geodeticEarth[1]) - cartesianDistace[1] * Math.Sin(geodeticEarth[0]) * Math.Sin(geodeticEarth[1]) + cartesianDistace[2] * Math.Cos(geodeticEarth[0]);
			double e = cartesianDistace[1] * Math.Cos(geodeticEarth[1]) - cartesianDistace[0] * Math.Sin(geodeticEarth[1]);
			double u = cartesianDistace[0] * Math.Cos(geodeticEarth[0]) * Math.Cos(geodeticEarth[1]) + cartesianDistace[1] * Math.Cos(geodeticEarth[0]) * Math.Sin(geodeticEarth[1]) + cartesianDistace[2] * Math.Sin(geodeticEarth[0]);

			Alfa = (n < 0.0) ? Math.Atan(e / n) + Math.PI : Math.Atan(e / n);
			Zr = Math.Atan(Math.Sqrt(n * n + e * e) / u);
			S = Math.Sqrt(n * n + e * e + u * u);

			return new double[] { Alfa, Zr, S };
		}
		public int[] LocalGeodeticToImagePixelCoordinate(int[] startingPoint, int[] offsetPoint, double[] localGeodeticCoordinate)
        {
			double x = startingPoint[0] - offsetPoint[0];
			double y = startingPoint[1] - offsetPoint[1];

			x = 2.0 * x / Math.PI * localGeodeticCoordinate[1];
			y = 2.0 * y / Math.PI * localGeodeticCoordinate[1];

			double px = x * Math.Cos(localGeodeticCoordinate[0]) + y * Math.Sin(localGeodeticCoordinate[0]);
			double py = x * Math.Sin(localGeodeticCoordinate[0]) + y * Math.Cos(localGeodeticCoordinate[0]);

			px = px + offsetPoint[0];
			py = py + offsetPoint[1];

			return new int[] { (int)px, (int)py };

		}
		public static int getWGS84() => 1;
		public static int getBESSEL() => 2;
		public static int getHAYFORD() => 3;
	}
}
