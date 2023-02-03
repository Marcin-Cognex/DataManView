namespace DataManView
{
    partial class frmMain
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.backgroundWorkerConnector = new System.ComponentModel.BackgroundWorker();
            this.tblMain = new DataManView.DBLayoutPanel();
            this.picResultImage = new System.Windows.Forms.PictureBox();
            this.picHeader = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtIP = new System.Windows.Forms.TextBox();
            this.lblConnAddress = new System.Windows.Forms.Label();
            this.lblStats = new System.Windows.Forms.Label();
            this.lblInfo = new System.Windows.Forms.Label();
            this.lblResult = new System.Windows.Forms.Label();
            this.tblMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picResultImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picHeader)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "BackgroundImage.png");
            this.imageList1.Images.SetKeyName(1, "Header.png");
            // 
            // backgroundWorkerConnector
            // 
            this.backgroundWorkerConnector.WorkerReportsProgress = true;
            this.backgroundWorkerConnector.WorkerSupportsCancellation = true;
            this.backgroundWorkerConnector.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorkerConnector.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker1_ProgressChanged);
            this.backgroundWorkerConnector.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorkerConnector_RunWorkerCompleted);
            // 
            // tblMain
            // 
            this.tblMain.BackColor = System.Drawing.Color.Transparent;
            this.tblMain.ColumnCount = 2;
            this.tblMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblMain.Controls.Add(this.picResultImage, 0, 1);
            this.tblMain.Controls.Add(this.picHeader, 0, 0);
            this.tblMain.Controls.Add(this.panel1, 1, 0);
            this.tblMain.Controls.Add(this.lblStats, 1, 3);
            this.tblMain.Controls.Add(this.lblInfo, 0, 3);
            this.tblMain.Controls.Add(this.lblResult, 0, 2);
            this.tblMain.Location = new System.Drawing.Point(12, 12);
            this.tblMain.Name = "tblMain";
            this.tblMain.RowCount = 4;
            this.tblMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8.163265F));
            this.tblMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 91.83673F));
            this.tblMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tblMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblMain.Size = new System.Drawing.Size(725, 591);
            this.tblMain.TabIndex = 2;
            // 
            // picResultImage
            // 
            this.tblMain.SetColumnSpan(this.picResultImage, 2);
            this.picResultImage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picResultImage.Location = new System.Drawing.Point(3, 46);
            this.picResultImage.Name = "picResultImage";
            this.picResultImage.Size = new System.Drawing.Size(719, 486);
            this.picResultImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picResultImage.TabIndex = 2;
            this.picResultImage.TabStop = false;
            // 
            // picHeader
            // 
            this.picHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picHeader.Image = global::DataManTradeshowView.Properties.Resources.Header;
            this.picHeader.Location = new System.Drawing.Point(3, 3);
            this.picHeader.Name = "picHeader";
            this.picHeader.Size = new System.Drawing.Size(356, 37);
            this.picHeader.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picHeader.TabIndex = 0;
            this.picHeader.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.txtIP);
            this.panel1.Controls.Add(this.lblConnAddress);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(533, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(189, 37);
            this.panel1.TabIndex = 9;
            // 
            // txtIP
            // 
            this.txtIP.BackColor = System.Drawing.SystemColors.Window;
            this.txtIP.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.txtIP.Location = new System.Drawing.Point(70, 3);
            this.txtIP.Name = "txtIP";
            this.txtIP.Size = new System.Drawing.Size(108, 26);
            this.txtIP.TabIndex = 1;
            this.txtIP.Text = "192.168.0.12";
            // 
            // lblConnAddress
            // 
            this.lblConnAddress.AutoSize = true;
            this.lblConnAddress.ForeColor = System.Drawing.Color.DarkGray;
            this.lblConnAddress.Location = new System.Drawing.Point(46, 10);
            this.lblConnAddress.Name = "lblConnAddress";
            this.lblConnAddress.Size = new System.Drawing.Size(20, 13);
            this.lblConnAddress.TabIndex = 0;
            this.lblConnAddress.Text = "IP:";
            // 
            // lblStats
            // 
            this.lblStats.AutoSize = true;
            this.lblStats.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblStats.ForeColor = System.Drawing.Color.White;
            this.lblStats.Location = new System.Drawing.Point(365, 570);
            this.lblStats.Name = "lblStats";
            this.lblStats.Size = new System.Drawing.Size(357, 21);
            this.lblStats.TabIndex = 3;
            this.lblStats.Text = "lbl Stats";
            this.lblStats.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblInfo
            // 
            this.lblInfo.AutoSize = true;
            this.lblInfo.BackColor = System.Drawing.Color.Transparent;
            this.lblInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblInfo.ForeColor = System.Drawing.Color.White;
            this.lblInfo.Location = new System.Drawing.Point(3, 570);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(356, 21);
            this.lblInfo.TabIndex = 0;
            this.lblInfo.Text = "lbl Info";
            this.lblInfo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblInfo.Visible = false;
            // 
            // lblResult
            // 
            this.lblResult.AutoSize = true;
            this.tblMain.SetColumnSpan(this.lblResult, 2);
            this.lblResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblResult.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblResult.ForeColor = System.Drawing.Color.White;
            this.lblResult.Location = new System.Drawing.Point(3, 535);
            this.lblResult.Name = "lblResult";
            this.lblResult.Size = new System.Drawing.Size(719, 35);
            this.lblResult.TabIndex = 10;
            this.lblResult.Text = "Result";
            this.lblResult.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.BackgroundImage = global::DataManTradeshowView.Properties.Resources.BackgroundImage;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(748, 619);
            this.Controls.Add(this.tblMain);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "frmMain";
            this.Text = "DataMan View";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.tblMain.ResumeLayout(false);
            this.tblMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picResultImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picHeader)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private DBLayoutPanel tblMain;
        private System.Windows.Forms.PictureBox picHeader;
        private System.Windows.Forms.PictureBox picResultImage;
        private System.Windows.Forms.ImageList imageList1;
        private System.ComponentModel.BackgroundWorker backgroundWorkerConnector;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txtIP;
        private System.Windows.Forms.Label lblConnAddress;
        private System.Windows.Forms.Label lblStats;
        private System.Windows.Forms.Label lblInfo;
        private System.Windows.Forms.Label lblResult;
    }
}

