namespace YUNkefu
{
    partial class Wx_alogin
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
            this.Wx_idListView = new YUNkefu.Controls.Shisan13ListView();
            this.loginclose = new System.Windows.Forms.PictureBox();
            this.addzbt = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.loginclose)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // Wx_idListView
            // 
            this.Wx_idListView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.Wx_idListView.Depth = 0;
            this.Wx_idListView.Font = new System.Drawing.Font("Roboto", 32F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World);
            this.Wx_idListView.FullRowSelect = true;
            this.Wx_idListView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.Wx_idListView.Location = new System.Drawing.Point(2, 28);
            this.Wx_idListView.MouseLocation = new System.Drawing.Point(-1, -1);
            this.Wx_idListView.MouseState = YUNkefu.MouseState.OUT;
            this.Wx_idListView.Name = "Wx_idListView";
            this.Wx_idListView.NeedDrawItemIndex = 0;
            this.Wx_idListView.OwnerDraw = true;
            this.Wx_idListView.Size = new System.Drawing.Size(241, 349);
            this.Wx_idListView.TabIndex = 0;
            this.Wx_idListView.UseCompatibleStateImageBehavior = false;
            this.Wx_idListView.View = System.Windows.Forms.View.Details;
            this.Wx_idListView.SelectedIndexChanged += new System.EventHandler(this.Wx_idListView_SelectedIndexChanged);
            // 
            // loginclose
            // 
            this.loginclose.BackColor = System.Drawing.Color.Transparent;
            this.loginclose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.loginclose.ErrorImage = null;
            this.loginclose.Image = global::YUNkefu.Properties.Resources.close;
            this.loginclose.Location = new System.Drawing.Point(27, 2);
            this.loginclose.Name = "loginclose";
            this.loginclose.Size = new System.Drawing.Size(20, 20);
            this.loginclose.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.loginclose.TabIndex = 67;
            this.loginclose.TabStop = false;
            this.loginclose.Click += new System.EventHandler(this.loginclose_Click);
            // 
            // addzbt
            // 
            this.addzbt.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.addzbt.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.addzbt.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(249)))), ((int)(((byte)(249)))));
            this.addzbt.Location = new System.Drawing.Point(-1, 0);
            this.addzbt.Name = "addzbt";
            this.addzbt.Size = new System.Drawing.Size(22, 22);
            this.addzbt.TabIndex = 77;
            this.addzbt.Tag = "";
            this.addzbt.Text = "+";
            this.addzbt.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this.loginclose);
            this.panel1.Controls.Add(this.addzbt);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(243, 25);
            this.panel1.TabIndex = 79;
            // 
            // Wx_alogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(243, 376);
            this.Controls.Add(this.Wx_idListView);
            this.Controls.Add(this.panel1);
            this.Name = "Wx_alogin";
            this.Text = "alogin";
            this.Load += new System.EventHandler(this.alogin_Load);
            ((System.ComponentModel.ISupportInitialize)(this.loginclose)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.Shisan13ListView Wx_idListView;
        private System.Windows.Forms.PictureBox loginclose;
        private System.Windows.Forms.Label addzbt;
        private System.Windows.Forms.Panel panel1;
    }
}