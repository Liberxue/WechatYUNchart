using System.ComponentModel;
using System.Windows.Forms;

namespace YUNkefu.Controls
{
    public sealed class Shisan13Divider : Control, IShisan13Control
    {
        [Browsable(false)]
        public int Depth { get; set; }
        [Browsable(false)]
        public shisan13Manager SkinManager { get { return shisan13Manager.Instance; } }
        [Browsable(false)]
        public MouseState MouseState { get; set; }
        
        public Shisan13Divider()
        {
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            Height = 1;
            BackColor = SkinManager.GetDividersColor();
        }
    }
}
