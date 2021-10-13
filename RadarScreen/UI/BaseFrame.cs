using RadarScreen.DataAccess;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RadarScreen.UI
{
    public partial class BaseFrame : Form
    {
        private String url = "";
        private ReadAlmanac readAlmanac;
        private SatelliteNumber satelliteNumber;
        public BaseFrame()
        {
            InitializeComponent();
            readAlmanac = new ReadAlmanac();
            satelliteNumber = new SatelliteNumber();
        }
        private void BrowseClick(object sender, System.EventArgs e)
        {
            this.dtSatellitesTable.Rows.Clear();
            this.ofbFile.InitialDirectory = "c:\\";
            this.ofbFile.RestoreDirectory = true;

            if (this.ofbFile.ShowDialog() == DialogResult.OK)
            {
                url = this.ofbFile.FileName;
            }
        }
        private void RunClick(object sender, System.EventArgs e)
        {
            for (int i = 0; i < satelliteNumber.Count; i++)
            {
                ReadSatellite(satelliteNumber.ElementAt(i).Key);
                readAlmanac.resetM();
            }
            this.btnRun.Enabled = false;
	    }
        private void ResetClick(object sender, System.EventArgs e)
        {
            this.btnRun.Enabled = true;
            this.worldMap.Invalidate();
            this.dtCreateEarthPointTable.Clear();
            this.dtSatellitesTable.Clear();
        }
        private void CreateEarthPointClickCheckBox(object sender, DataGridViewCellEventArgs e)
        {
            if(e.ColumnIndex == 0 )
            {
                this.dgwCreateEarthPoint.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
            if ((bool)this.dgwCreateEarthPoint.CurrentCell.Value == true)
            {
                // Thread radarThread = new Thread(() => RadarShow(this.dgwCreateEarthPoint.Rows[e.RowIndex]));
                //  radarThread.Start();
                RadarShow(this.dgwCreateEarthPoint.Rows[e.RowIndex]);
            }
            else
            {
            }
        }


        private void ReadSatellite(String Satellite)
        {
            readAlmanac.toTLE(url, satelliteNumber.getSatellite(Satellite));

            if(readAlmanac.getM() != 0)
            {
                double n = readAlmanac.getN() / 86400.0 * 2.0 * Math.PI;
                double a = Math.Pow(398600.4418 / n / n, 1.0 / 3.0);
                this.dtSatellitesTable.Rows.Add(Satellite, a, readAlmanac.getE(), readAlmanac.getM(), readAlmanac.getL0(), readAlmanac.getW0(), readAlmanac.getI0(), readAlmanac.getN(), readAlmanac.getN2(), readAlmanac.getN6(), readAlmanac.getT0(), readAlmanac.getWgps());
            }
        }
        private void RadarShow(DataGridViewRow earthPoint)
        {
            RadarFrame radarFrame = new RadarFrame(earthPoint , this.dtSatellitesTable);
            radarFrame.TopMost = true;
            radarFrame.Show();
        }
    }
}
