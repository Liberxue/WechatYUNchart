namespace testmytabcontrol
{
    partial class Form2
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
            this.myTabControlEx1 = new MyTabControl.MyTabControlEx();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage7 = new System.Windows.Forms.TabPage();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.button1 = new System.Windows.Forms.Button();
            this.myTabControlEx1.SuspendLayout();
            this.SuspendLayout();
            // 
            // myTabControlEx1
            // 
            this.myTabControlEx1.ArrowColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(79)))), ((int)(((byte)(125)))));
            this.myTabControlEx1.BackColor = System.Drawing.SystemColors.Control;
            this.myTabControlEx1.Controls.Add(this.tabPage1);
            this.myTabControlEx1.Controls.Add(this.tabPage7);
            this.myTabControlEx1.Controls.Add(this.tabPage4);
            this.myTabControlEx1.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
            this.myTabControlEx1.HaveCloseButton = true;
            this.myTabControlEx1.Location = new System.Drawing.Point(305, 42);
            this.myTabControlEx1.Margin = new System.Windows.Forms.Padding(2);
            this.myTabControlEx1.Name = "myTabControlEx1";
            this.myTabControlEx1.Padding = new System.Drawing.Point(9, 4);
            this.myTabControlEx1.SelectedIndex = 0;
            this.myTabControlEx1.ShowDrawTipText = true;
            this.myTabControlEx1.Size = new System.Drawing.Size(464, 413);
            this.myTabControlEx1.TabIndex = 1;
            this.myTabControlEx1.TipBackColor = System.Drawing.Color.DarkBlue;
            this.myTabControlEx1.标签呼吸灯颜色 = System.Drawing.SystemColors.MenuHighlight;
            // 
            // tabPage1
            // 
            this.tabPage1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tabPage1.ImageIndex = 0;
            this.tabPage1.Location = new System.Drawing.Point(4, 28);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(2);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(2);
            this.tabPage1.Size = new System.Drawing.Size(456, 381);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage111111";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage7
            // 
            this.tabPage7.Location = new System.Drawing.Point(4, 28);
            this.tabPage7.Margin = new System.Windows.Forms.Padding(2);
            this.tabPage7.Name = "tabPage7";
            this.tabPage7.Size = new System.Drawing.Size(192, 68);
            this.tabPage7.TabIndex = 3;
            this.tabPage7.Text = "tabPage7";
            this.tabPage7.UseVisualStyleBackColor = true;
            // 
            // tabPage4
            // 
            this.tabPage4.Location = new System.Drawing.Point(4, 28);
            this.tabPage4.Margin = new System.Windows.Forms.Padding(2);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(192, 68);
            this.tabPage4.TabIndex = 4;
            this.tabPage4.Text = "tabPage4";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(799, 155);
            this.button1.Margin = new System.Windows.Forms.Padding(2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(255, 54);
            this.button1.TabIndex = 7;
            this.button1.Text = "效果测试";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1075, 497);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.myTabControlEx1);
            this.Name = "Form2";
            this.Text = "Form2";
            this.Load += new System.EventHandler(this.Form2_Load);
            this.myTabControlEx1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private MyTabControl.MyTabControlEx myTabControlEx1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage7;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.Button button1;
    }
}