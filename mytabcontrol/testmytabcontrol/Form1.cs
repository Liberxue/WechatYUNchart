using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace testmytabcontrol
{
    public partial class Form1 : Form
    {
        //Timer time1 = new Timer();
        //int key=1;
        //bool k = true;
        //int ima = 0;
        public Form1()
        {
            InitializeComponent();
           // time1.Interval = 30;
           // time1.Tick+=new EventHandler (time1_Tick);
        }
       
        
       // static int i = 10;

        private void myTabControlEx1_tabClose(object sender, MouseEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //i++;
            //TabPage tab = new TabPage(i.ToString());
            //tab.Name = textBox2.Text.Trim();
            //TabControl my = new TabControl();
            //TabPage tab1 = new TabPage(i.ToString());
            //my.TabPages.Add(tab1);
            //my.TabPages.Add(tab1);
            //tab.Controls.Add(my);
            //my.Dock = DockStyle.Fill;
            //myTabControlEx1.TabPages.Add(tab);
            //myTabControlEx1.tabFlickerAdd(textBox2.Text.Trim());
            //myTabControlEx1.TipTextAdd(textBox2.Text.Trim(), textBox2.Text.Trim());
            foreach (Control c in this.Controls)
            {
                if (c is MyTabControl.MyTabControlEx)
                {
                    ((MyTabControl.MyTabControlEx)c).tabFlickerAdd(((MyTabControl.MyTabControlEx)c).TabPages[1].Name);
                    ((MyTabControl.MyTabControlEx)c).TipTextAdd(((MyTabControl.MyTabControlEx)c).TabPages[1].Name,"22");
                }
            }
         //  if (time1.Enabled) { time1.Enabled=false; } else { time1.Enabled=true; }
        }

        //public void time1_Tick(object sender,EventArgs e)
        //{ 

        //    ima=ima+30*key;
        //    if (ima >=255) { ima = 255; key = key * (-1); }
        //    if (ima <=0) { ima =0; key = key * (-1); }
        //    Color c = Color.FromArgb(ima, ima, ima);
        //    button1.BackColor = c;
        //}
    }
}
