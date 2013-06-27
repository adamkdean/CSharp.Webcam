namespace CSharp.Webcam
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
            this.cameraDisplayBox = new System.Windows.Forms.PictureBox();
            this.txtLog = new System.Windows.Forms.TextBox();
            this.btnConfig = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.cameraDisplayBox)).BeginInit();
            this.SuspendLayout();
            // 
            // cameraDisplayBox
            // 
            this.cameraDisplayBox.Location = new System.Drawing.Point(12, 12);
            this.cameraDisplayBox.Name = "cameraDisplayBox";
            this.cameraDisplayBox.Size = new System.Drawing.Size(320, 240);
            this.cameraDisplayBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.cameraDisplayBox.TabIndex = 1;
            this.cameraDisplayBox.TabStop = false;
            // 
            // txtLog
            // 
            this.txtLog.BackColor = System.Drawing.Color.White;
            this.txtLog.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLog.Location = new System.Drawing.Point(338, 12);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.ReadOnly = true;
            this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtLog.Size = new System.Drawing.Size(380, 241);
            this.txtLog.TabIndex = 3;
            this.txtLog.TabStop = false;
            // 
            // btnConfig
            // 
            this.btnConfig.Location = new System.Drawing.Point(286, 230);
            this.btnConfig.Name = "btnConfig";
            this.btnConfig.Size = new System.Drawing.Size(46, 23);
            this.btnConfig.TabIndex = 4;
            this.btnConfig.Text = "Config";
            this.btnConfig.UseVisualStyleBackColor = true;
            this.btnConfig.Click += new System.EventHandler(this.btnConfig_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(730, 263);
            this.Controls.Add(this.btnConfig);
            this.Controls.Add(this.txtLog);
            this.Controls.Add(this.cameraDisplayBox);
            this.Name = "frmMain";
            this.Text = "CSharp.Webcam";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.Load += new System.EventHandler(this.frmMain_Load);
            ((System.ComponentModel.ISupportInitialize)(this.cameraDisplayBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox cameraDisplayBox;
        private System.Windows.Forms.TextBox txtLog;
        private System.Windows.Forms.Button btnConfig;
    }
}

