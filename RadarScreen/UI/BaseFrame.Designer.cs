

using System;

namespace RadarScreen.UI
{
    partial class BaseFrame
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dtCreateEarthPointTable = new System.Data.DataTable();
            this.dtSatellitesTable = new System.Data.DataTable();

            this.dgwCreateEarthPoint = new System.Windows.Forms.DataGridView();
            this.dgwSatellites = new System.Windows.Forms.DataGridView();
            this.pnlWorldMapScrool = new System.Windows.Forms.Panel();
            this.pnlActionPanel = new System.Windows.Forms.TableLayoutPanel();
            this.pnlTopPanel = new System.Windows.Forms.Panel(); ;
            this.btnRun = new System.Windows.Forms.Button();
            this.btnReset = new System.Windows.Forms.Button();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.worldMap = new WorldMapPanel(this.dtCreateEarthPointTable);
            this.ofbFile = new System.Windows.Forms.OpenFileDialog();
            // 
            // dtSatellitesTable
            // 
            this.dtSatellitesTable.Columns.Add("PRN");
            this.dtSatellitesTable.Columns.Add("a[km]");
            this.dtSatellitesTable.Columns.Add("e[]");
            this.dtSatellitesTable.Columns.Add("M[o]");
            this.dtSatellitesTable.Columns.Add("l[o]");
            this.dtSatellitesTable.Columns.Add("w[o]");
            this.dtSatellitesTable.Columns.Add("i[o]");
            this.dtSatellitesTable.Columns.Add("n[rev/day]");
            this.dtSatellitesTable.Columns.Add("n2[rev/day^2]");
            this.dtSatellitesTable.Columns.Add("n6[rev/day^3]");
            this.dtSatellitesTable.Columns.Add("t0");
            this.dtSatellitesTable.Columns.Add("Wgps");
            // 
            // dgwSatellites
            // 
            this.dgwSatellites.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dgwSatellites.ReadOnly = true;
            this.dgwSatellites.AllowUserToDeleteRows = false;
            this.dgwSatellites.AllowUserToAddRows = false;
            this.dgwSatellites.DataSource = dtSatellitesTable;
            this.dgwSatellites.Height = 207;
            // 
            // dtCreateEarthPointTable
            // 
            this.dtCreateEarthPointTable.Columns.Add("Select", typeof(bool));
            this.dtCreateEarthPointTable.Columns.Add("PRN");
            this.dtCreateEarthPointTable.Columns.Add("Latitude");
            this.dtCreateEarthPointTable.Columns.Add("Longitude");

            this.dtCreateEarthPointTable.Columns[0].ReadOnly = false;
            this.dtCreateEarthPointTable.Columns[1].ReadOnly = true;
            this.dtCreateEarthPointTable.Columns[2].ReadOnly = true;
            this.dtCreateEarthPointTable.Columns[3].ReadOnly = true;

            // 
            // dgwCreateEarthPoint
            // 
            this.dgwCreateEarthPoint.Dock = System.Windows.Forms.DockStyle.Right;
            this.dgwCreateEarthPoint.AllowUserToDeleteRows = false;
            this.dgwCreateEarthPoint.AllowUserToAddRows = false;
            this.dgwCreateEarthPoint.DataSource = dtCreateEarthPointTable;
            this.dgwCreateEarthPoint.Width = 250;
            this.dgwCreateEarthPoint.CellContentClick += this.CreateEarthPointClickCheckBox;

            //this.dtCreateEarthPointTable.Rows.Add(4);

            // 
            // pnlWorldMapScrool
            // 
            this.pnlWorldMapScrool.AutoScroll = true;
            this.pnlWorldMapScrool.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlWorldMapScrool.Controls.Add(this.worldMap);
            // 
            // btnRun
            // 
            this.btnRun.Text = "RUN";
            this.btnRun.Height = 30;
            this.btnRun.Width = 100;
            this.btnRun.Click += new EventHandler(this.RunClick);
            // 
            // btnReset
            // 
            this.btnReset.Text = "RESET";
            this.btnReset.Height = 30;
            this.btnReset.Width = 100;
            this.btnReset.Click += new EventHandler(this.ResetClick);
            // 
            // btnBrowse
            // 
            this.btnBrowse.Text = "...";
            this.btnBrowse.Height = 30;
            this.btnBrowse.Width = 100;
            this.btnBrowse.Click += new EventHandler(this.BrowseClick);
            // 
            // pnlActionPanel
            // 
            this.pnlActionPanel.Height = 35;
            this.pnlActionPanel.Width = 330;
            this.pnlActionPanel.Location = new System.Drawing.Point((this.pnlTopPanel.Width - this.pnlActionPanel.Width) / 2, (this.pnlTopPanel.Height - this.pnlActionPanel.Height) / 2);
            this.pnlActionPanel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pnlActionPanel.ColumnCount = 3;
            this.pnlActionPanel.RowCount = 1;
            this.pnlActionPanel.Controls.Add(btnRun, 0, 0);
            this.pnlActionPanel.Controls.Add(btnReset, 1, 0);
            this.pnlActionPanel.Controls.Add(btnBrowse, 2, 0);
            // 
            // pnlTopPanel
            // 
            this.pnlTopPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTopPanel.Controls.Add(this.pnlActionPanel);
            this.pnlTopPanel.Height = 40;
            // 
            // BaseFrame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);

            this.Controls.Add(this.pnlWorldMapScrool);
            this.Controls.Add(this.dgwCreateEarthPoint);
            this.Controls.Add(this.dgwSatellites);
            this.Controls.Add(this.pnlTopPanel);

        }

        #endregion

        private System.Data.DataTable dtCreateEarthPointTable;
        private System.Data.DataTable dtSatellitesTable;

        private System.Windows.Forms.DataGridView dgwCreateEarthPoint;
        private System.Windows.Forms.DataGridView dgwSatellites;
        private System.Windows.Forms.Panel pnlWorldMapScrool;
        private System.Windows.Forms.TableLayoutPanel pnlActionPanel;
        private System.Windows.Forms.Panel pnlTopPanel;
        private System.Windows.Forms.Panel worldMap;
        private System.Windows.Forms.Button btnRun;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.OpenFileDialog ofbFile;
    }
}

