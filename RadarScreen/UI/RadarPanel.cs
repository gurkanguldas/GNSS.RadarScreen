using RadarScreen.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RadarScreen.UI
{
    public class RadarPanel : Panel
    {

        private DataGridViewRow earthPoint;
        private DataTable satelliteTable;
        private Image image = Image.FromFile("Radar.jpg");

        private Graphics graphic;
        private SolidBrush solidG = new SolidBrush(Color.FromArgb(255, 0, 0));
        private SolidBrush solidR = new SolidBrush(Color.FromArgb(0, 0, 255));
        private SolidBrush solidC = new SolidBrush(Color.FromArgb(0, 255, 0));
        private SolidBrush solidE = new SolidBrush(Color.FromArgb(255, 255, 0));
        private PointTransform pointTransform = new PointTransform(PointTransform.getWGS84());

        private List<SatelliteCoordinate> satelliteCoordinates = new List<SatelliteCoordinate>();
        private double[] geodeticEarth;

        private double time = 0.0;
        public RadarPanel(DataGridViewRow earthPoint, DataTable satelliteTable)
        {
            this.earthPoint = earthPoint;
            this.satelliteTable = satelliteTable;


            this.BackgroundImage = image;
            this.ClientSize = image.Size;
            this.BackgroundImageLayout = ImageLayout.None;

            this.geodeticEarth = new double[] { Double.Parse((string)earthPoint.Cells[1].Value), 
                                                Double.Parse((string)earthPoint.Cells[2].Value),
                                                0.0 };

        }
        protected override void OnPaint(PaintEventArgs e)
        {
            // this.getCoordinates(this.CreateGraphics());
            Thread thread = new Thread(() => this.getCoordinates());
            thread.Start();
        }

        public void getCoordinates()
        {
            this.graphic = this.CreateGraphics();

            foreach (DataRow data in this.satelliteTable.Rows)
                satelliteCoordinates.Add(new SatelliteCoordinate(data));

            System.Timers.Timer timer = new System.Timers.Timer();
            timer.Interval = 100;
            timer.Elapsed += this.timerEvent;
            timer.Enabled = true;

        }
        private void timerEvent(object sender , EventArgs e)
        {

            graphic.DrawImage(image, 0, 0);
            foreach (SatelliteCoordinate satelliteCoordinate in satelliteCoordinates)
            {
                satelliteCoordinate.TLE(time);

                double[] cartesianSatellite = satelliteCoordinate.getCartesianSatellite();
                double[] cartesianEarth = pointTransform.Cartesian(geodeticEarth);
                double[] localCoordinate = pointTransform.Local_Geodetic(cartesianSatellite, cartesianEarth);
                if (localCoordinate[1] > 0.0)
                {
                    if (localCoordinate[0] < 0.0)
                        localCoordinate[0] += 2.0 * Math.PI;

                    int[] radarCoordinate = pointTransform.LocalGeodeticToImagePixelCoordinate(new int[] { 387, 52 }, new int[] { 387, 385 }, localCoordinate);

                    Rectangle rectangle = new Rectangle(radarCoordinate[0], radarCoordinate[1], 15, 15);
                    
                    if(satelliteCoordinate.getSatelliteName().Substring(0,1).Equals("G", StringComparison.OrdinalIgnoreCase))
                    {
                        graphic.FillEllipse(solidG, rectangle);
                        graphic.DrawString(satelliteCoordinate.getSatelliteName(), new Font("Helvetica", 7, FontStyle.Italic), solidG, radarCoordinate[0], radarCoordinate[1] - 20);
                    }
                    else if (satelliteCoordinate.getSatelliteName().Substring(0, 1).Equals("R", StringComparison.OrdinalIgnoreCase))
                    {
                        graphic.FillEllipse(solidR, rectangle);
                        graphic.DrawString(satelliteCoordinate.getSatelliteName(), new Font("Helvetica", 7, FontStyle.Italic), solidR, radarCoordinate[0], radarCoordinate[1] - 20);
                    }
                    else if (satelliteCoordinate.getSatelliteName().Substring(0, 1).Equals("E", StringComparison.OrdinalIgnoreCase))
                    {
                        graphic.FillEllipse(solidE, rectangle);
                        graphic.DrawString(satelliteCoordinate.getSatelliteName(), new Font("Helvetica", 7, FontStyle.Italic), solidE, radarCoordinate[0], radarCoordinate[1] - 20);
                    }
                    else
                    {
                        graphic.FillEllipse(solidC, rectangle);
                        graphic.DrawString(satelliteCoordinate.getSatelliteName(), new Font("Helvetica", 7, FontStyle.Italic), solidC, radarCoordinate[0], radarCoordinate[1] - 20);

                    }
                }
            }
            time += 0.02;

        }
    }
}

