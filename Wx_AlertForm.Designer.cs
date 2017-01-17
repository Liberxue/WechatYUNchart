namespace YUNkefu
{
    partial class Wx_AlertForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Wx_AlertForm));
            this.labContent = new System.Windows.Forms.Label();
            this.labtitle = new System.Windows.Forms.Label();
            this.picClose = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.picClose)).BeginInit();
            this.SuspendLayout();
            // 
            // labContent
            // 
            this.labContent.AllowDrop = true;
            this.labContent.AutoEllipsis = true;
            this.labContent.AutoSize = true;
            this.labContent.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labContent.Location = new System.Drawing.Point(25, 70);
            this.labContent.Name = "labContent";
            this.labContent.Size = new System.Drawing.Size(50, 20);
            this.labContent.TabIndex = 0;
            this.labContent.Text = "label1";
            // 
            // labtitle
            // 
            this.labtitle.AllowDrop = true;
            this.labtitle.AutoEllipsis = true;
            this.labtitle.AutoSize = true;
            this.labtitle.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.labtitle.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labtitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.labtitle.Location = new System.Drawing.Point(7, 1);
            this.labtitle.Name = "labtitle";
            this.labtitle.Size = new System.Drawing.Size(37, 20);
            this.labtitle.TabIndex = 1;
            this.labtitle.Text = "标题";
            this.labtitle.Click += new System.EventHandler(this.picClose_Click);
            // 
            // picClose
            // 
            this.picClose.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.picClose.Image = ((System.Drawing.Image)(resources.GetObject("picClose.Image")));
            this.picClose.Location = new System.Drawing.Point(233, 1);
            this.picClose.Name = "picClose";
            this.picClose.Size = new System.Drawing.Size(20, 20);
            this.picClose.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.picClose.TabIndex = 41;
            this.picClose.TabStop = false;
            this.picClose.Click += new System.EventHandler(this.picClose_Click);
            this.picClose.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picClose_MouseDown);
            this.picClose.MouseEnter += new System.EventHandler(this.picClose_MouseLeave);
            this.picClose.MouseLeave += new System.EventHandler(this.picClose_MouseEnter);
            this.picClose.MouseHover += new System.EventHandler(this.picClose_MouseEnter);
            this.picClose.MouseMove += new System.Windows.Forms.MouseEventHandler(this.picClose_MouseDown);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.panel1.Location = new System.Drawing.Point(1, 1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(252, 22);
            this.panel1.TabIndex = 42;
            // 
            // Wx_AlertForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ClientSize = new System.Drawing.Size(255, 163);
            this.Controls.Add(this.labtitle);
            this.Controls.Add(this.labContent);
            this.Controls.Add(this.picClose);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Wx_AlertForm";
            this.Text = "AlertForm";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.AlertForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picClose)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labContent;
        private System.Windows.Forms.Label labtitle;
        private System.Windows.Forms.PictureBox picClose;
        private System.Windows.Forms.Panel panel1;
    }
}