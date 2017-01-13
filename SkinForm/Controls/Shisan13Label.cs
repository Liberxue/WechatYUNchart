using System.ComponentModel;
using System.Windows.Forms;

namespace YUNkefu.Controls
{
    public class Shisan13Label : Label, IShisan13Control
    {
        [Browsable(false)]
        public int Depth { get; set; }
        [Browsable(false)]
        public shisan13Manager SkinManager { get { return shisan13Manager.Instance; } }
        [Browsable(false)]
        public MouseState MouseState { get; set; }
        protected override void OnCreateControl()
        {
            base.OnCreateControl();

            ForeColor = SkinManager.GetPrimaryTextColor();
            Font = SkinManager.ROBOTO_REGULAR_11;

            BackColorChanged += (sender, args) => ForeColor = SkinManager.GetPrimaryTextColor();
        }
    }
}
