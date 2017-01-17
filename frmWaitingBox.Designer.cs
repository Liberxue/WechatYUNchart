namespace YUNkefu
{
    partial class frmWaitingBox
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
            this.labTimer = new System.Windows.Forms.Label();
            this.labMessage = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // labTimer
            // 
            this.labTimer.AutoSize = true;
            this.labTimer.Font = new System.Drawing.Font("Tahoma", 9.5F);
            this.labTimer.ForeColor = System.Drawing.Color.Red;
            this.labTimer.Location = new System.Drawing.Point(176, 65);
            this.labTimer.Name = "labTimer";
            this.labTimer.Size = new System.Drawing.Size(30, 16);
            this.labTimer.TabIndex = 8;
            this.labTimer.Text = "0秒";
            // 
            // labMessage
            // 
            this.labMessage.AutoSize = true;
            this.labMessage.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labMessage.Location = new System.Drawing.Point(117, 31);
            this.labMessage.Name = "labMessage";
            this.labMessage.Size = new System.Drawing.Size(184, 19);
            this.labMessage.TabIndex = 7;
            this.labMessage.Text = "正在启动微信，请稍后...";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::YUNkefu.Properties.Resources.loading;
            this.pictureBox1.Location = new System.Drawing.Point(26, 22);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(64, 64);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 9;
            this.pictureBox1.TabStop = false;
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // panel1
            // 
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(313, 116);
            this.panel1.TabIndex = 10;
            // 
            // frmWaitingBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(313, 116);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.labTimer);
            this.Controls.Add(this.labMessage);
            this.Controls.Add(this.panel1);
            this.Name = "frmWaitingBox";
            this.Text = "frmWaitingBox";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labTimer;
        private System.Windows.Forms.Label labMessage;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Panel panel1;
    }
}