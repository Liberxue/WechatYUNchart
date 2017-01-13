using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace YUNkefu.Controls
{
    public class Shisan13TabControl : TabControl, IShisan13Control
    {
        [Browsable(false)]
        public int Depth { get; set; }
        [Browsable(false)]
        public shisan13Manager SkinManager { get { return shisan13Manager.Instance; } }
        [Browsable(false)]
        public MouseState MouseState { get; set; }
        
		protected override void WndProc(ref Message m)
        {
            if (m.Msg == 0x1328 && !DesignMode) m.Result = (IntPtr)1;
            else base.WndProc(ref m);
        }
    }
}
