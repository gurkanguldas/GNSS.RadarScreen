using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using RadarScreen.DataAccess;


namespace RadarScreen.UI
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new BaseFrame());
            

            /*
            double[] geodeticEarth = new double[] { 45.0, 35.0, 500.0 };

            ReadAlmanac readAlmanac = new ReadAlmanac();
            SatelliteNumber satelliteNumber = new SatelliteNumber();
            readAlmanac.toTLE("P:\\Personel\\ad\\GNSS-ops.txt", satelliteNumber.getSatellite("G01"));

            SatelliteCoordinate satelliteCoordinate = new SatelliteCoordinate(readAlmanac);
            satelliteCoordinate.TLE(12.0);
            double[] cartesianSatellite = satelliteCoordinate.getCartesianSatellite();

            PointTransform pointTransform = new PointTransform(PointTransform.getWGS84());
            double[] cartesianEarth = pointTransform.Cartesian(geodeticEarth);

            double[] localCoordinate = pointTransform.Local_Geodetic(cartesianSatellite, cartesianEarth);

            MessageBox.Show(localCoordinate[0]*180.0/Math.PI+"  "+ localCoordinate[1] * 180.0 / Math.PI + "  "+localCoordinate[2]/1000.0);
           */
        }
    }
}
