using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RadarScreen.DataAccess
{
    public class SatelliteCoordinate
    {
        private double X, Y, Z, tk;

        private String satellite;
        private WgpsToUTC time;

        private double e, M, l, w, ik, n, n2, n6, t0, Wgps;

		public SatelliteCoordinate(DataRow satelliteCollection )
        {
			e = Double.Parse((string)satelliteCollection.ItemArray[2]);
			M = Double.Parse((string)satelliteCollection.ItemArray[3]);
			l = Double.Parse((string)satelliteCollection.ItemArray[4]);
			w = Double.Parse((string)satelliteCollection.ItemArray[5]);
			ik = Double.Parse((string)satelliteCollection.ItemArray[6]);
			n = Double.Parse((string)satelliteCollection.ItemArray[7]);
			n2 = Double.Parse((string)satelliteCollection.ItemArray[8]);
			n6 = Double.Parse((string)satelliteCollection.ItemArray[9]);
			t0 = Double.Parse((string)satelliteCollection.ItemArray[10]);
			Wgps = Double.Parse((string)satelliteCollection.ItemArray[11]);
			satellite = (string)satelliteCollection.ItemArray[0];

			time = new WgpsToUTC(t0, Wgps);

		}
		public void TLE(double hour)
		{
			GPSweek gweek = new GPSweek(time.getDay(), time.getMonth(), time.getYear());

			double Wgps = gweek.GPS_month;
			double LeapSec = 0.0;

			if (satellite.Substring(0, 1).Equals("C" , StringComparison.OrdinalIgnoreCase))
			{
				if (1356 < Wgps && Wgps <= 1512)
					LeapSec = 0.0 / 86400.0;

				else if (1512 < Wgps && Wgps <= 1669)
					LeapSec = 1.0 / 86400.0;

				else if (1669 < Wgps && Wgps <= 1825)
					LeapSec = 2.0 / 86400.0;

				else if (1825 < Wgps && Wgps <= 1930)
					LeapSec = 3.0 / 86400.0;

				else
					LeapSec = 4.0 / 86400.0;
			}
			else
			{
				if (990 <= Wgps && Wgps <= 1356)
					LeapSec = 13.0 / 86400.0;

				else if (1356 < Wgps && Wgps <= 1512)
					LeapSec = 14.0 / 86400.0;

				else if (1512 < Wgps && Wgps <= 1669)
					LeapSec = 15.0 / 86400.0;

				else if (1669 < Wgps && Wgps <= 1825)
					LeapSec = 16.0 / 86400.0;

				else if (1825 < Wgps && Wgps <= 1930)
					LeapSec = 17.0 / 86400.0;

				else
					LeapSec = 18.0 / 86400.0;
			}

			Coordinate XYZ = new Coordinate(satellite);

			double GM = 0.0;

			if (satellite.Substring(0, 1).Equals("G", StringComparison.OrdinalIgnoreCase))
				GM = 398600.5;

			else if (satellite.Substring(0, 1).Equals("R", StringComparison.OrdinalIgnoreCase))
				GM = 398600.4418;

			else if (satellite.Substring(0, 1).Equals("E", StringComparison.OrdinalIgnoreCase))
				GM = 398600.4418;

			else
				GM = 398600.4418;

			double dt = gweek.Day - 1 + hour / 24.0 - t0 - LeapSec;
			double nk = (n + n2 * dt + n6 * dt * dt) / 86400.0 * 2.0 * Math.PI;
			double a = Math.Pow(GM / nk / nk, 1.0 / 3.0);
			double Mk = M / 180.0 * Math.PI + nk * dt * 86400.0;

			Mk = Mk % (2.0 * Math.PI);
			while (Mk < 0.0)
				Mk += 2.0 * Math.PI;

			double Ek0 = Mk + e * Math.Sin(Mk);
			double Ek = Mk + e * Math.Sin(Ek0);

			while (Math.Abs(Ek - Ek0) >= 1.0e-5)
			{
				Ek0 = Ek;
				Ek = Mk + e * Math.Sin(Ek0);
			}

			double rk = a * (1.0 - e * Math.Cos(Ek));

			int month = time.getMonth();
			int year = time.getYear();
			int day = time.getDay();

			if (month <= 2)
			{
				month += 12;
				year--;
			}

			double JD = (int)(365.25 * year) + (int)(30.6001 * (month + 1.0)) + day + hour / 24.0 + 1720981.5 - LeapSec;
			double JD2000 = 2451545.0;
			double D = JD - JD2000;
			double D0 = (int)D - 0.5;
			double H = (D - D0) * 24.0;
			double T = D / 36525.0;
			double GMST = 6.697374558 + 0.06570982441908 * D0 + 1.00273790935 * H + 0.000026 * T * T;
			double Q = (125.04 - 0.052954 * D) / (180.0 / Math.PI);
			double L = (208.47 + 0.98565 * D) / (180.0 / Math.PI);
			double E = (23.4393 - 0.0000004 * D) / (180.0 / Math.PI);
			double K = -0.000319 * Math.Sin(Q) - 0.000024 * Math.Sin(2.0 * L);
			double eqeq = K * Math.Cos(E);
			double GAST = GMST + eqeq;
			GAST = (GAST % 24.0) / 12.0 * Math.PI;

			double lk = l / 180.0 * Math.PI - GAST;

			double c = Math.Sqrt(1 - e * e) * Math.Sin(Ek);
			double v = (Math.Cos(Ek) - e);

			double fk = (v > 0.0) ? Math.Atan(c / v) : Math.Atan(c / v) + Math.PI;
			double uk = w / 180.0 * Math.PI + fk;

			double[] xyz = XYZ.toPlace(rk, uk, ik / 180.0 * Math.PI, lk);
			X = xyz[0] * 1.0e3;
			Y = xyz[1] * 1.0e3;
			Z = xyz[2] * 1.0e3;
		}
		public double[] getCartesianSatellite() => new double[] { X, Y, Z };
		public String getSatelliteName() => satellite;
	}
}
