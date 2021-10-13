using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadarScreen.DataAccess
{
    public class ReadAlmanac
    {
		private double M    = 0.0,
					   e    = 0.0,
					   t0   = 0.0,
					   l0   = 0.0,
					   i0   = 0.0,
					   n    = 0.0,
					   n2   = 0.0,
					   n6   = 0.0,
					   B    = 0.0,
					   w0   = 0.0,
					   Wgps = 0.0;

		private String _satellite;

		public void toTLE(String url, String satellite)
		{
			_satellite = satellite;

			StreamReader buffer = new StreamReader(new FileStream(url, FileMode.Open, FileAccess.Read));
			String read = buffer.ReadLine();


			while (read != null)
			{
				if (read.Length > 0)
					if (read.Substring(2, 5).Equals(satellite, StringComparison.OrdinalIgnoreCase))
					{

						read = read.Replace('.', ',');
						Wgps = 2000.0 + Double.Parse(read.Substring(18, 2));
						t0 = Double.Parse(read.Substring(20, 12));
						n2 = Double.Parse(read.Substring(33, 1)+"0"+read.Substring(34, 9));
						n6 = Double.Parse(read.Substring(45, 5) + "e" + read.Substring(50, 2));
						B = Double.Parse(read.Substring(54, 5) + "e" + read.Substring(59, 2));

						read = buffer.ReadLine();

						read = read.Replace('.', ',');
						i0 = Double.Parse(read.Substring(8, 8));
						l0 = Double.Parse(read.Substring(17, 8));
						e = Double.Parse("0," + read.Substring(26, 7));
						w0 = Double.Parse(read.Substring(34, 8));
						M = Double.Parse(read.Substring(43, 8));
						n = Double.Parse(read.Substring(53, 8));
					}
				
				read = buffer.ReadLine();
			}
		}
		public double getM() => M;
		public double getE() => e;
		public double getT0()=> t0;
		public double getL0()=> l0;
		public double getI0()=> i0;
		public double getN() => n;
		public double getN2()=> n2;
		public double getN6()=> n6;
		public double getB() => B;
		public double getW0()=> w0;
		public double getWgps()=> Wgps;
		public String getSatellite() => _satellite;
		public void resetM() => M = 0;
		
	}
}
