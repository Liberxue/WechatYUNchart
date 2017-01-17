namespace YUNkefu
{
    partial class Wx_LoginForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Wx_LoginForm));
            this.picClose = new System.Windows.Forms.PictureBox();
            this.labTitle = new System.Windows.Forms.Label();
            this.linkReturn = new System.Windows.Forms.LinkLabel();
            this.lblTip = new System.Windows.Forms.Label();
            this.picQRCode = new System.Windows.Forms.PictureBox();
            this.bt_alogin = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.picClose)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picQRCode)).BeginInit();
            this.SuspendLayout();
            // 
            // picClose
            // 
            this.picClose.Image = global::YUNkefu.Properties.Resources.close;
            this.picClose.Location = new System.Drawing.Point(209, 9);
            this.picClose.Name = "picClose";
            this.picClose.Size = new System.Drawing.Size(20, 20);
            this.picClose.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.picClose.TabIndex = 7;
            this.picClose.TabStop = false;
            this.picClose.Click += new System.EventHandler(this.picClose_Click_1);
            // 
            // labTitle
            // 
            this.labTitle.AutoSize = true;
            this.labTitle.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labTitle.ForeColor = System.Drawing.Color.Black;
            this.labTitle.Location = new System.Drawing.Point(13, 13);
            this.labTitle.Name = "labTitle";
            this.labTitle.Size = new System.Drawing.Size(72, 16);
            this.labTitle.TabIndex = 1;
            this.labTitle.Text = "labTitle";
            // 
            // linkReturn
            // 
            this.linkReturn.AutoSize = true;
            this.linkReturn.ForeColor = System.Drawing.Color.Black;
            this.linkReturn.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.linkReturn.LinkColor = System.Drawing.SystemColors.InfoText;
            this.linkReturn.Location = new System.Drawing.Point(66, 298);
            this.linkReturn.Name = "linkReturn";
            this.linkReturn.Size = new System.Drawing.Size(89, 12);
            this.linkReturn.TabIndex = 5;
            this.linkReturn.TabStop = true;
            this.linkReturn.Text = "返回二维码界面";
            this.linkReturn.Visible = false;
            this.linkReturn.VisitedLinkColor = System.Drawing.Color.Teal;
            this.linkReturn.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkReturn_LinkClicked);
            // 
            // lblTip
            // 
            this.lblTip.AutoSize = true;
            this.lblTip.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTip.ForeColor = System.Drawing.Color.Black;
            this.lblTip.Location = new System.Drawing.Point(39, 266);
            this.lblTip.Name = "lblTip";
            this.lblTip.Size = new System.Drawing.Size(149, 20);
            this.lblTip.TabIndex = 4;
            this.lblTip.Text = "手机微信扫一扫以登录";
            // 
            // picQRCode
            // 
            this.picQRCode.Location = new System.Drawing.Point(19, 54);
            this.picQRCode.Name = "picQRCode";
            this.picQRCode.Size = new System.Drawing.Size(200, 200);
            this.picQRCode.TabIndex = 6;
            this.picQRCode.TabStop = false;
            // 
            // bt_alogin
            // 
            this.bt_alogin.BackColor = System.Drawing.Color.Coral;
            this.bt_alogin.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.bt_alogin.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(249)))), ((int)(((byte)(249)))));
            this.bt_alogin.Location = new System.Drawing.Point(64, 323);
            this.bt_alogin.Name = "bt_alogin";
            this.bt_alogin.Size = new System.Drawing.Size(91, 35);
            this.bt_alogin.TabIndex = 8;
            this.bt_alogin.Tag = "";
            this.bt_alogin.Text = "一键登录";
            this.bt_alogin.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.bt_alogin.Click += new System.EventHandler(this.bt_alogin_Click);
            // 
            // Wx_LoginForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(243, 376);
            this.Controls.Add(this.bt_alogin);
            this.Controls.Add(this.picQRCode);
            this.Controls.Add(this.linkReturn);
            this.Controls.Add(this.lblTip);
            this.Controls.Add(this.labTitle);
            this.Controls.Add(this.picClose);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Wx_LoginForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "LoginForm";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.LoginForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picClose)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picQRCode)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox picClose;
        private System.Windows.Forms.Label labTitle;
        private System.Windows.Forms.LinkLabel linkReturn;
        private System.Windows.Forms.Label lblTip;
        private System.Windows.Forms.PictureBox picQRCode;
        private System.Windows.Forms.Label bt_alogin;
    }
}