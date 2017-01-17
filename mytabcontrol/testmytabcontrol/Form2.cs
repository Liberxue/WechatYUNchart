using shisan13;
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
    public partial class Form2 : SkinMain
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            foreach (Control c in this.Controls)
            {
                if (c is MyTabControl.MyTabControlEx)
                {
                    ((MyTabControl.MyTabControlEx)c).tabFlickerAdd(((MyTabControl.MyTabControlEx)c).TabPages[1].Name);
                    ((MyTabControl.MyTabControlEx)c).TipTextAdd(((MyTabControl.MyTabControlEx)c).TabPages[1].Name, "22");
                }
            }
        }
    }
}
