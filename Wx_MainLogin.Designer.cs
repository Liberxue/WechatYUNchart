namespace YUNkefu
{
    partial class Wx_MainLogin
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Wx_MainLogin));
            this.btDL = new System.Windows.Forms.Label();
            this.tbCode = new System.Windows.Forms.ComboBox();
            this.lblTag = new System.Windows.Forms.Label();
            this.title = new System.Windows.Forms.Label();
            this.chkMemoryPwd = new YUNkefu.Controls.Shisan13CheckBox();
            this.shisan13FlatButton1 = new YUNkefu.Controls.Shisan13FlatButton();
            this.shisan13FlatButton2 = new YUNkefu.Controls.Shisan13FlatButton();
            this.txtPwd = new YUNkefu.Controls.Shisan13SingleLineTextField();
            this.vs = new YUNkefu.Controls.Shisan13FlatButton();
            this.msgss = new YUNkefu.Controls.Shisan13FlatButton();
            this.texttoken = new YUNkefu.Controls.Shisan13Label();
            this.pic_Mxi = new System.Windows.Forms.PictureBox();
            this.loginclose = new System.Windows.Forms.PictureBox();
            this.keydl = new YUNkefu.Controls.Shisan13FlatButton();
            ((System.ComponentModel.ISupportInitialize)(this.pic_Mxi)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.loginclose)).BeginInit();
            this.SuspendLayout();
            // 
            // btDL
            // 
            this.btDL.BackColor = System.Drawing.Color.Coral;
            this.btDL.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btDL.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(249)))), ((int)(((byte)(249)))));
            this.btDL.Location = new System.Drawing.Point(181, 276);
            this.btDL.Name = "btDL";
            this.btDL.Size = new System.Drawing.Size(109, 41);
            this.btDL.TabIndex = 4;
            this.btDL.Tag = "";
            this.btDL.Text = "登    录";
            this.btDL.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btDL.Click += new System.EventHandler(this.btDL_Click);
            // 
            // tbCode
            // 
            this.tbCode.BackColor = System.Drawing.SystemColors.ControlLight;
            this.tbCode.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.tbCode.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbCode.ForeColor = System.Drawing.Color.Black;
            this.tbCode.Location = new System.Drawing.Point(169, 192);
            this.tbCode.Name = "tbCode";
            this.tbCode.Size = new System.Drawing.Size(159, 22);
            this.tbCode.TabIndex = 1;
            this.tbCode.Tag = "";
            this.tbCode.SelectionChangeCommitted += new System.EventHandler(this.tbCode_SelectionChangeCommitted);
            // 
            // lblTag
            // 
            this.lblTag.BackColor = System.Drawing.Color.Transparent;
            this.lblTag.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTag.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblTag.Image = global::YUNkefu.Properties.Resources.tag;
            this.lblTag.Location = new System.Drawing.Point(341, 46);
            this.lblTag.Name = "lblTag";
            this.lblTag.Size = new System.Drawing.Size(52, 26);
            this.lblTag.TabIndex = 54;
            this.lblTag.Tag = "";
            this.lblTag.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // title
            // 
            this.title.AutoSize = true;
            this.title.BackColor = System.Drawing.Color.Transparent;
            this.title.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.title.ForeColor = System.Drawing.Color.AliceBlue;
            this.title.Location = new System.Drawing.Point(187, 81);
            this.title.Name = "title";
            this.title.Size = new System.Drawing.Size(116, 37);
            this.title.TabIndex = 53;
            this.title.Text = "云客服";
            // 
            // chkMemoryPwd
            // 
            this.chkMemoryPwd.AutoSize = true;
            this.chkMemoryPwd.Depth = 0;
            this.chkMemoryPwd.Font = new System.Drawing.Font("Roboto", 10F);
            this.chkMemoryPwd.Location = new System.Drawing.Point(344, 226);
            this.chkMemoryPwd.Margin = new System.Windows.Forms.Padding(0);
            this.chkMemoryPwd.MouseLocation = new System.Drawing.Point(-1, -1);
            this.chkMemoryPwd.MouseState = YUNkefu.MouseState.HOVER;
            this.chkMemoryPwd.Name = "chkMemoryPwd";
            this.chkMemoryPwd.Ripple = true;
            this.chkMemoryPwd.Size = new System.Drawing.Size(90, 30);
            this.chkMemoryPwd.TabIndex = 3;
            this.chkMemoryPwd.Text = "记住密码";
            this.chkMemoryPwd.UseVisualStyleBackColor = true;
            // 
            // shisan13FlatButton1
            // 
            this.shisan13FlatButton1.AutoSize = true;
            this.shisan13FlatButton1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.shisan13FlatButton1.Depth = 0;
            this.shisan13FlatButton1.Icon = null;
            this.shisan13FlatButton1.Location = new System.Drawing.Point(100, 183);
            this.shisan13FlatButton1.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.shisan13FlatButton1.MouseState = YUNkefu.MouseState.HOVER;
            this.shisan13FlatButton1.Name = "shisan13FlatButton1";
            this.shisan13FlatButton1.Primary = false;
            this.shisan13FlatButton1.Size = new System.Drawing.Size(66, 36);
            this.shisan13FlatButton1.TabIndex = 59;
            this.shisan13FlatButton1.Text = "账号：";
            this.shisan13FlatButton1.UseVisualStyleBackColor = false;
            // 
            // shisan13FlatButton2
            // 
            this.shisan13FlatButton2.AutoSize = true;
            this.shisan13FlatButton2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.shisan13FlatButton2.Depth = 0;
            this.shisan13FlatButton2.Icon = null;
            this.shisan13FlatButton2.Location = new System.Drawing.Point(99, 226);
            this.shisan13FlatButton2.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.shisan13FlatButton2.MouseState = YUNkefu.MouseState.HOVER;
            this.shisan13FlatButton2.Name = "shisan13FlatButton2";
            this.shisan13FlatButton2.Primary = false;
            this.shisan13FlatButton2.Size = new System.Drawing.Size(66, 36);
            this.shisan13FlatButton2.TabIndex = 60;
            this.shisan13FlatButton2.Text = "密码：";
            this.shisan13FlatButton2.UseVisualStyleBackColor = false;
            // 
            // txtPwd
            // 
            this.txtPwd.Depth = 0;
            this.txtPwd.Hint = "";
            this.txtPwd.Location = new System.Drawing.Point(169, 235);
            this.txtPwd.MaxLength = 32767;
            this.txtPwd.MouseState = YUNkefu.MouseState.HOVER;
            this.txtPwd.Name = "txtPwd";
            this.txtPwd.PasswordChar = '\0';
            this.txtPwd.SelectedText = "";
            this.txtPwd.SelectionLength = 0;
            this.txtPwd.SelectionStart = 0;
            this.txtPwd.Size = new System.Drawing.Size(159, 23);
            this.txtPwd.TabIndex = 2;
            this.txtPwd.TabStop = false;
            this.txtPwd.UseSystemPasswordChar = true;
            // 
            // vs
            // 
            this.vs.AutoSize = true;
            this.vs.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.vs.Depth = 0;
            this.vs.Icon = null;
            this.vs.Location = new System.Drawing.Point(226, 323);
            this.vs.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.vs.MouseState = YUNkefu.MouseState.HOVER;
            this.vs.Name = "vs";
            this.vs.Primary = false;
            this.vs.Size = new System.Drawing.Size(96, 36);
            this.vs.TabIndex = 62;
            this.vs.Text = "当前版本：";
            this.vs.UseVisualStyleBackColor = true;
            // 
            // msgss
            // 
            this.msgss.AutoSize = true;
            this.msgss.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.msgss.Depth = 0;
            this.msgss.Icon = null;
            this.msgss.Location = new System.Drawing.Point(122, 146);
            this.msgss.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.msgss.MouseState = YUNkefu.MouseState.HOVER;
            this.msgss.Name = "msgss";
            this.msgss.Primary = false;
            this.msgss.Size = new System.Drawing.Size(16, 36);
            this.msgss.TabIndex = 63;
            this.msgss.UseVisualStyleBackColor = true;
            // 
            // texttoken
            // 
            this.texttoken.AutoSize = true;
            this.texttoken.Depth = 0;
            this.texttoken.Font = new System.Drawing.Font("Roboto", 11F);
            this.texttoken.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.texttoken.Location = new System.Drawing.Point(5, 331);
            this.texttoken.MouseState = YUNkefu.MouseState.HOVER;
            this.texttoken.Name = "texttoken";
            this.texttoken.Size = new System.Drawing.Size(112, 18);
            this.texttoken.TabIndex = 64;
            this.texttoken.Text = "shisan13Label1";
            this.texttoken.Visible = false;
            // 
            // pic_Mxi
            // 
            this.pic_Mxi.BackColor = System.Drawing.Color.Transparent;
            this.pic_Mxi.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pic_Mxi.ErrorImage = null;
            this.pic_Mxi.Image = ((System.Drawing.Image)(resources.GetObject("pic_Mxi.Image")));
            this.pic_Mxi.Location = new System.Drawing.Point(408, 2);
            this.pic_Mxi.Name = "pic_Mxi";
            this.pic_Mxi.Size = new System.Drawing.Size(20, 20);
            this.pic_Mxi.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pic_Mxi.TabIndex = 65;
            this.pic_Mxi.TabStop = false;
            this.pic_Mxi.Click += new System.EventHandler(this.pic_Mxi_Click);
            this.pic_Mxi.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pic_Mxi_MouseDown);
            this.pic_Mxi.MouseEnter += new System.EventHandler(this.pic_Mxi_MouseEnter);
            this.pic_Mxi.MouseLeave += new System.EventHandler(this.pic_Mxi_MouseLeave);
            // 
            // loginclose
            // 
            this.loginclose.BackColor = System.Drawing.Color.Transparent;
            this.loginclose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.loginclose.ErrorImage = null;
            this.loginclose.Image = global::YUNkefu.Properties.Resources.close;
            this.loginclose.Location = new System.Drawing.Point(432, 2);
            this.loginclose.Name = "loginclose";
            this.loginclose.Size = new System.Drawing.Size(20, 20);
            this.loginclose.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.loginclose.TabIndex = 66;
            this.loginclose.TabStop = false;
            this.loginclose.Click += new System.EventHandler(this.loginclose_Click);
            this.loginclose.MouseDown += new System.Windows.Forms.MouseEventHandler(this.loginclose_MouseDown);
            this.loginclose.MouseEnter += new System.EventHandler(this.loginclose_MouseEnter);
            this.loginclose.MouseLeave += new System.EventHandler(this.loginclose_MouseLeave);
            // 
            // keydl
            // 
            this.keydl.AutoSize = true;
            this.keydl.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.keydl.Depth = 0;
            this.keydl.Icon = null;
            this.keydl.Location = new System.Drawing.Point(163, 279);
            this.keydl.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.keydl.MouseState = YUNkefu.MouseState.HOVER;
            this.keydl.Name = "keydl";
            this.keydl.Primary = false;
            this.keydl.Size = new System.Drawing.Size(16, 36);
            this.keydl.TabIndex = 67;
            this.keydl.UseVisualStyleBackColor = true;
            this.keydl.Click += new System.EventHandler(this.btDL_Click);
            // 
            // Main_Login
            // 
            this.AcceptButton = this.keydl;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImage = global::YUNkefu.Properties.Resources._011;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(457, 356);
            this.Controls.Add(this.btDL);
            this.Controls.Add(this.loginclose);
            this.Controls.Add(this.pic_Mxi);
            this.Controls.Add(this.texttoken);
            this.Controls.Add(this.msgss);
            this.Controls.Add(this.vs);
            this.Controls.Add(this.txtPwd);
            this.Controls.Add(this.shisan13FlatButton2);
            this.Controls.Add(this.shisan13FlatButton1);
            this.Controls.Add(this.chkMemoryPwd);
            this.Controls.Add(this.tbCode);
            this.Controls.Add(this.lblTag);
            this.Controls.Add(this.title);
            this.Controls.Add(this.keydl);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Main_Login";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Login";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.Main_Login_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pic_Mxi)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.loginclose)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label title;
        private System.Windows.Forms.Label lblTag;
        private System.Windows.Forms.ComboBox tbCode;
        private System.Windows.Forms.Label btDL;
        private YUNkefu.Controls.Shisan13CheckBox chkMemoryPwd;
        private YUNkefu.Controls.Shisan13FlatButton shisan13FlatButton1;
        private YUNkefu.Controls.Shisan13FlatButton shisan13FlatButton2;
        private YUNkefu.Controls.Shisan13SingleLineTextField txtPwd;
        private YUNkefu.Controls.Shisan13FlatButton vs;
        private YUNkefu.Controls.Shisan13FlatButton msgss;
        private YUNkefu.Controls.Shisan13Label texttoken;
        private System.Windows.Forms.PictureBox pic_Mxi;
        private System.Windows.Forms.PictureBox loginclose;
        private YUNkefu.Controls.Shisan13FlatButton keydl;
    }
}