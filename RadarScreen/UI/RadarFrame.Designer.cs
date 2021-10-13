
namespace RadarScreen.UI
{
    partial class RadarFrame
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.radarScrool = new System.Windows.Forms.Panel();
            this.radarPanel = new RadarPanel(earthPoint , satelliteTable);

            // 
            // radarScrool
            //

            this.radarScrool.AutoScroll = true;
            this.radarScrool.Controls.Add(this.radarPanel);
            this.radarScrool.Dock = System.Windows.Forms.DockStyle.Fill;
            // 
            // RadarFrame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(775, 773);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Controls.Add(this.radarScrool);

        }

        #endregion

        private RadarPanel radarPanel;
        private System.Windows.Forms.Panel radarScrool;
    }
}