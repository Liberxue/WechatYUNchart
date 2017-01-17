namespace YUNkefu
{
    partial class Wx_wechart
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.wTabControl1 = new YUNkefu.Controls.WTabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.wChatList1 = new YUNkefu.Controls.WChatList();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.wFriendsList1 = new YUNkefu.Controls.WFriendsList();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.wpersonalinfo = new YUNkefu.Controls.WPersonalInfo();
            this.chart_main = new System.Windows.Forms.Panel();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tableLayoutPanel1.SuspendLayout();
            this.wTabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 22.52252F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 77.47748F));
            this.tableLayoutPanel1.Controls.Add(this.wTabControl1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.chart_main, 1, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(2040, 1008);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // wTabControl1
            // 
            this.wTabControl1.Controls.Add(this.tabPage1);
            this.wTabControl1.Controls.Add(this.tabPage2);
            this.wTabControl1.Controls.Add(this.tabPage3);
            this.wTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wTabControl1.ItemSize = new System.Drawing.Size(152, 60);
            this.wTabControl1.Location = new System.Drawing.Point(0, 0);
            this.wTabControl1.Margin = new System.Windows.Forms.Padding(0);
            this.wTabControl1.Name = "wTabControl1";
            this.wTabControl1.Padding = new System.Drawing.Point(0, 0);
            this.wTabControl1.SelectedIndex = 0;
            this.wTabControl1.Size = new System.Drawing.Size(459, 1008);
            this.wTabControl1.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.wTabControl1.TabIndex = 0;
            this.wTabControl1.SelectedIndexChanged += new System.EventHandler(this.wTabControl1_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.wChatList1);
            this.tabPage1.Location = new System.Drawing.Point(4, 64);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(451, 940);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "会话";
            this.tabPage1.UseVisualStyleBackColor = true;
            this.tabPage1.Click += new System.EventHandler(this.tabPage1_Click);
            // 
            // wChatList1
            // 
            this.wChatList1.BackColor = System.Drawing.SystemColors.Menu;
            this.wChatList1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.wChatList1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.wChatList1.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.wChatList1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.wChatList1.FormattingEnabled = true;
            this.wChatList1.Location = new System.Drawing.Point(0, 0);
            this.wChatList1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.wChatList1.Name = "wChatList1";
            this.wChatList1.Size = new System.Drawing.Size(473, 906);
            this.wChatList1.TabIndex = 0;
            this.wChatList1.StartChat += new YUNkefu.Controls.StartChatEventHandler(this.wchatlist_StartChat);
            this.wChatList1.SelectedIndexChanged += new System.EventHandler(this.wChatList1_SelectedIndexChanged);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.wFriendsList1);
            this.tabPage2.Location = new System.Drawing.Point(4, 64);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Size = new System.Drawing.Size(451, 940);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "通讯录";
            this.tabPage2.UseVisualStyleBackColor = true;
            this.tabPage2.Click += new System.EventHandler(this.tabPage2_Click);
            // 
            // wFriendsList1
            // 
            this.wFriendsList1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.wFriendsList1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.wFriendsList1.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.wFriendsList1.FormattingEnabled = true;
            this.wFriendsList1.Location = new System.Drawing.Point(0, 0);
            this.wFriendsList1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.wFriendsList1.Name = "wFriendsList1";
            this.wFriendsList1.Size = new System.Drawing.Size(473, 906);
            this.wFriendsList1.TabIndex = 0;
            this.wFriendsList1.FriendInfoView += new YUNkefu.Controls.FriendInfoViewEventHandler(this.wfriendlist_FriendInfoView);
            this.wFriendsList1.SelectedIndexChanged += new System.EventHandler(this.wFriendsList1_SelectedIndexChanged);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.wpersonalinfo);
            this.tabPage3.Location = new System.Drawing.Point(4, 64);
            this.tabPage3.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(451, 940);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "我";
            this.tabPage3.UseVisualStyleBackColor = true;
            this.tabPage3.Click += new System.EventHandler(this.tabPage3_Click);
            // 
            // wpersonalinfo
            // 
            this.wpersonalinfo.BackColor = System.Drawing.Color.White;
            this.wpersonalinfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wpersonalinfo.FriendUser = null;
            this.wpersonalinfo.Location = new System.Drawing.Point(0, 0);
            this.wpersonalinfo.Margin = new System.Windows.Forms.Padding(0);
            this.wpersonalinfo.Name = "wpersonalinfo";
            this.wpersonalinfo.ShowTopPanel = false;
            this.wpersonalinfo.Size = new System.Drawing.Size(451, 940);
            this.wpersonalinfo.TabIndex = 0;
            this.wpersonalinfo.Load += new System.EventHandler(this.wpersonalinfo_Load);
            // 
            // chart_main
            // 
            this.chart_main.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.chart_main.Location = new System.Drawing.Point(463, 4);
            this.chart_main.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.chart_main.Name = "chart_main";
            this.chart_main.Size = new System.Drawing.Size(1573, 1000);
            this.chart_main.TabIndex = 1;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(67, 4);
            // 
            // Wx_wechart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1920, 1006);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MinimizeBox = false;
            this.Name = "Wx_wechart";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "wechart";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMainForm_FormClosing);
            this.Load += new System.EventHandler(this.frmMainForm_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.wTabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.WTabControl wTabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private Controls.WPersonalInfo wpersonalinfo;
        private Controls.WChatList wChatList1;
        private Controls.WFriendsList wFriendsList1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.Panel chart_main;
    }
}