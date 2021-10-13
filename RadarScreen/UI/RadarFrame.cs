using RadarScreen.DataAccess;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RadarScreen.UI
{
    public partial class RadarFrame : Form
    {
        private DataGridViewRow earthPoint;
        private DataTable satelliteTable;
        public RadarFrame(DataGridViewRow earthPoint, DataTable satelliteTable)
        {
            this.earthPoint = earthPoint;
            this.satelliteTable = satelliteTable;
            InitializeComponent();
        }
        
    }
}
