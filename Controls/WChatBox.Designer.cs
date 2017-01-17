namespace YUNkefu.Controls
{
    partial class WChatBox
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.plTop = new System.Windows.Forms.Panel();
            this.btnInfo = new System.Windows.Forms.Button();
            this.lblInfo = new System.Windows.Forms.Label();
            this.btnBack = new System.Windows.Forms.Button();
            this.rSendContent = new System.Windows.Forms.TextBox();
            this.SendContent = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.WXcontent = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.Pic_emjoy = new System.Windows.Forms.PictureBox();
            this.webKitBrowser1 = new WebKit.WebKitBrowser();
            this.panImg = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.WxTabControl = new YUNkefu.Controls.Shisan13TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.addWxreply = new System.Windows.Forms.Button();
            this.Wxreply = new YUNkefu.Controls.Shisan13ListView();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.shisan13TabSelector1 = new YUNkefu.Controls.Shisan13TabSelector();
            this.shisan13ListView1 = new YUNkefu.Controls.Shisan13ListView();
            this.plTop.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Pic_emjoy)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.WxTabControl.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // plTop
            // 
            this.plTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.plTop.Controls.Add(this.btnInfo);
            this.plTop.Controls.Add(this.lblInfo);
            this.plTop.Controls.Add(this.btnBack);
            this.plTop.Location = new System.Drawing.Point(3, 3);
            this.plTop.Name = "plTop";
            this.plTop.Size = new System.Drawing.Size(593, 42);
            this.plTop.TabIndex = 7;
            // 
            // btnInfo
            // 
            this.btnInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnInfo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnInfo.FlatAppearance.BorderSize = 0;
            this.btnInfo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnInfo.Image = global::YUNkefu.Properties.Resources.info1;
            this.btnInfo.Location = new System.Drawing.Point(559, 6);
            this.btnInfo.Name = "btnInfo";
            this.btnInfo.Size = new System.Drawing.Size(30, 30);
            this.btnInfo.TabIndex = 2;
            this.btnInfo.UseVisualStyleBackColor = true;
            this.btnInfo.Click += new System.EventHandler(this.btnInfo_Click);
            // 
            // lblInfo
            // 
            this.lblInfo.AutoSize = true;
            this.lblInfo.BackColor = System.Drawing.Color.Transparent;
            this.lblInfo.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblInfo.ForeColor = System.Drawing.Color.Black;
            this.lblInfo.Location = new System.Drawing.Point(264, 13);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(101, 20);
            this.lblInfo.TabIndex = 1;
            this.lblInfo.Text = "与 张三 聊天中";
            // 
            // btnBack
            // 
            this.btnBack.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBack.FlatAppearance.BorderSize = 0;
            this.btnBack.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBack.Image = global::YUNkefu.Properties.Resources.back1;
            this.btnBack.Location = new System.Drawing.Point(3, 4);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(36, 29);
            this.btnBack.TabIndex = 0;
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // rSendContent
            // 
            this.rSendContent.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rSendContent.BackColor = System.Drawing.Color.White;
            this.rSendContent.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.rSendContent.Location = new System.Drawing.Point(5, 621);
            this.rSendContent.Multiline = true;
            this.rSendContent.Name = "rSendContent";
            this.rSendContent.Size = new System.Drawing.Size(595, 92);
            this.rSendContent.TabIndex = 0;
            this.rSendContent.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox1_KeyDown);
            // 
            // SendContent
            // 
            this.SendContent.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.SendContent.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SendContent.Location = new System.Drawing.Point(469, 3);
            this.SendContent.Name = "SendContent";
            this.SendContent.Size = new System.Drawing.Size(52, 25);
            this.SendContent.TabIndex = 1;
            this.SendContent.Text = "发送";
            this.SendContent.UseVisualStyleBackColor = true;
            this.SendContent.Click += new System.EventHandler(this.SendContent_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.panel1.Controls.Add(this.SendContent);
            this.panel1.Controls.Add(this.WXcontent);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.pictureBox2);
            this.panel1.Controls.Add(this.Pic_emjoy);
            this.panel1.Location = new System.Drawing.Point(0, 591);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(596, 31);
            this.panel1.TabIndex = 14;
            // 
            // WXcontent
            // 
            this.WXcontent.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.WXcontent.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.WXcontent.Location = new System.Drawing.Point(527, 2);
            this.WXcontent.Name = "WXcontent";
            this.WXcontent.Size = new System.Drawing.Size(65, 25);
            this.WXcontent.TabIndex = 2;
            this.WXcontent.Text = "聊天记录";
            this.WXcontent.UseVisualStyleBackColor = true;
            this.WXcontent.Click += new System.EventHandler(this.WXcontent_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.Image = global::YUNkefu.Properties.Resources.print_spree;
            this.pictureBox1.Location = new System.Drawing.Point(46, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(25, 24);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 12;
            this.pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox2.Image = global::YUNkefu.Properties.Resources.file;
            this.pictureBox2.Location = new System.Drawing.Point(85, 4);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(25, 24);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox2.TabIndex = 13;
            this.pictureBox2.TabStop = false;
            // 
            // Pic_emjoy
            // 
            this.Pic_emjoy.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Pic_emjoy.Image = global::YUNkefu.Properties.Resources.emjoy;
            this.Pic_emjoy.Location = new System.Drawing.Point(3, 4);
            this.Pic_emjoy.Name = "Pic_emjoy";
            this.Pic_emjoy.Size = new System.Drawing.Size(25, 24);
            this.Pic_emjoy.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.Pic_emjoy.TabIndex = 11;
            this.Pic_emjoy.TabStop = false;
            this.Pic_emjoy.Click += new System.EventHandler(this.Pic_emjoy_Click_1);
            // 
            // webKitBrowser1
            // 
            this.webKitBrowser1.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.webKitBrowser1.Dock = System.Windows.Forms.DockStyle.Top;
            this.webKitBrowser1.Location = new System.Drawing.Point(3, 51);
            this.webKitBrowser1.Name = "webKitBrowser1";
            this.webKitBrowser1.Size = new System.Drawing.Size(593, 538);
            this.webKitBrowser1.TabIndex = 1;
            this.webKitBrowser1.Url = new System.Uri("http://www.taobao.com", System.UriKind.Absolute);
            // 
            // panImg
            // 
            this.panImg.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.panImg.BackColor = System.Drawing.Color.Transparent;
            this.panImg.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.panImg.Cursor = System.Windows.Forms.Cursors.Hand;
            this.panImg.Location = new System.Drawing.Point(4, 198);
            this.panImg.Name = "panImg";
            this.panImg.Size = new System.Drawing.Size(409, 392);
            this.panImg.TabIndex = 3;
            this.panImg.Visible = false;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.SystemColors.HighlightText;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 72.02881F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 27.97119F));
            this.tableLayoutPanel1.Controls.Add(this.plTop, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.WxTabControl, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.shisan13TabSelector1, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.webKitBrowser1, 0, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6.843575F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 93.15643F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(833, 716);
            this.tableLayoutPanel1.TabIndex = 16;
            // 
            // WxTabControl
            // 
            this.WxTabControl.Controls.Add(this.tabPage1);
            this.WxTabControl.Controls.Add(this.tabPage2);
            this.WxTabControl.Depth = 0;
            this.WxTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.WxTabControl.Location = new System.Drawing.Point(602, 51);
            this.WxTabControl.MouseState = YUNkefu.MouseState.HOVER;
            this.WxTabControl.Name = "WxTabControl";
            this.WxTabControl.SelectedIndex = 0;
            this.WxTabControl.Size = new System.Drawing.Size(228, 662);
            this.WxTabControl.TabIndex = 11;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.addWxreply);
            this.tabPage1.Controls.Add(this.Wxreply);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(220, 636);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "快捷回复";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // addWxreply
            // 
            this.addWxreply.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.addWxreply.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.addWxreply.Location = new System.Drawing.Point(180, 0);
            this.addWxreply.Name = "addWxreply";
            this.addWxreply.Size = new System.Drawing.Size(39, 23);
            this.addWxreply.TabIndex = 1;
            this.addWxreply.Text = "添加";
            this.addWxreply.UseVisualStyleBackColor = true;
            this.addWxreply.Click += new System.EventHandler(this.addWxreply_Click);
            // 
            // Wxreply
            // 
            this.Wxreply.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Wxreply.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.Wxreply.Depth = 0;
            this.Wxreply.Font = new System.Drawing.Font("Roboto", 32F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World);
            this.Wxreply.FullRowSelect = true;
            this.Wxreply.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.Wxreply.Location = new System.Drawing.Point(-4, 3);
            this.Wxreply.MouseLocation = new System.Drawing.Point(-1, -1);
            this.Wxreply.MouseState = YUNkefu.MouseState.OUT;
            this.Wxreply.Name = "Wxreply";
            this.Wxreply.NeedDrawItemIndex = 0;
            this.Wxreply.OwnerDraw = true;
            this.Wxreply.Size = new System.Drawing.Size(237, 640);
            this.Wxreply.TabIndex = 0;
            this.Wxreply.UseCompatibleStateImageBehavior = false;
            this.Wxreply.View = System.Windows.Forms.View.Details;
            this.Wxreply.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.Wxreply_MouseDoubleClick);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.shisan13ListView1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(220, 636);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "聊天记录";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // shisan13TabSelector1
            // 
            this.shisan13TabSelector1.BaseTabControl = this.WxTabControl;
            this.shisan13TabSelector1.Depth = 0;
            this.shisan13TabSelector1.Location = new System.Drawing.Point(602, 3);
            this.shisan13TabSelector1.MouseState = YUNkefu.MouseState.HOVER;
            this.shisan13TabSelector1.Name = "shisan13TabSelector1";
            this.shisan13TabSelector1.Size = new System.Drawing.Size(228, 42);
            this.shisan13TabSelector1.TabIndex = 12;
            this.shisan13TabSelector1.Text = "shisan13TabSelector1";
            // 
            // shisan13ListView1
            // 
            this.shisan13ListView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.shisan13ListView1.Depth = 0;
            this.shisan13ListView1.Font = new System.Drawing.Font("Roboto", 32F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World);
            this.shisan13ListView1.FullRowSelect = true;
            this.shisan13ListView1.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.shisan13ListView1.Location = new System.Drawing.Point(-2, 3);
            this.shisan13ListView1.MouseLocation = new System.Drawing.Point(-1, -1);
            this.shisan13ListView1.MouseState = YUNkefu.MouseState.OUT;
            this.shisan13ListView1.Name = "shisan13ListView1";
            this.shisan13ListView1.NeedDrawItemIndex = 0;
            this.shisan13ListView1.OwnerDraw = true;
            this.shisan13ListView1.Size = new System.Drawing.Size(239, 653);
            this.shisan13ListView1.TabIndex = 0;
            this.shisan13ListView1.UseCompatibleStateImageBehavior = false;
            this.shisan13ListView1.View = System.Windows.Forms.View.Details;
            // 
            // WChatBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.rSendContent);
            this.Controls.Add(this.panImg);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "WChatBox";
            this.Size = new System.Drawing.Size(835, 716);
            this.Load += new System.EventHandler(this.WChatBox_Load);
            this.Resize += new System.EventHandler(this.WChatBox_Resize);
            this.plTop.ResumeLayout(false);
            this.plTop.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Pic_emjoy)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.WxTabControl.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel plTop;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.TextBox rSendContent;
        private System.Windows.Forms.Button SendContent;
        private System.Windows.Forms.Label lblInfo;
        private System.Windows.Forms.Button btnInfo;
        private System.Windows.Forms.PictureBox Pic_emjoy;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Panel panel1;
        private WebKit.WebKitBrowser webKitBrowser1;
        private System.Windows.Forms.Button WXcontent;
        private System.Windows.Forms.Panel panImg;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private Shisan13TabControl WxTabControl;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private Shisan13TabSelector shisan13TabSelector1;
        private Shisan13ListView Wxreply;
        private System.Windows.Forms.Button addWxreply;
        private Shisan13ListView shisan13ListView1;
    }
}
