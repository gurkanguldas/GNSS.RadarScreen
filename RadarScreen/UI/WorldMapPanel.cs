using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RadarScreen.UI
{
    public class WorldMapPanel : Panel
    {
        private int X = 0, Y = 0, PointNumber = 1;
        private double B = 0.0, L = 0.0;
        private DataTable dataTable;
        public WorldMapPanel(DataTable dataTable)
        {
            this.dataTable = dataTable;

            Image İmage = System.Drawing.Image.FromFile("WorldMap.png");

            this.BackgroundImage = İmage;
            this.ClientSize = İmage.Size;
            this.BackgroundImageLayout = ImageLayout.None;
            this.MouseClick += PanelMouseClick;
        }
        private void PanelMouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && (e.X >= 25 && e.X <= 1644) && (e.Y >= 19 && e.Y <= 752))
            {
                Graphics graphic = this.CreateGraphics();
                Rectangle rectangle = new Rectangle(e.X, e.Y, 10, 10);
                SolidBrush solid = new SolidBrush(Color.Black);
                graphic.FillEllipse(solid, rectangle);

                L = 360.0 / (1644.0 - 25.0) * (double)e.X + (-180.0 * 1644.0 - 25.0 * 180.0) / (1644.0 - 25.0);
                B = -(180.0 / (752.0 - 19.0) * (double)e.Y + (-90.0 * 752.0 - 19.0 * 90.0) / (752 - 19.0));

                dataTable.Rows.Add(false, PointNumber, B, L);
                PointNumber++;
            }
        }
    }
}
