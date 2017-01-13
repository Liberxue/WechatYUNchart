using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.ComponentModel;
using System.Drawing.Text;
namespace shisan13
{
    public partial class TabControlEx : TabControl
    {

        #region 本地参数
        /// <summary>
        /// 以下三行数据为呼吸灯相关过程数据
        /// </summary>
        int key = 1;
        float step = 100;
        int ima = 0, br, bg, bb;
        /// <summary>
        /// 呼吸灯传递过程颜色
        /// </summary>
        private Color _tabbaseColor = Color.FromArgb(166, 222, 255);//标签颜色
        /// <summary>
        /// 基础颜色
        /// </summary>
        private Color _baseColor = Color.FromArgb(166, 222, 255);//标签颜色
        /// <summary>
        /// 背景色
        /// </summary>
        private Color _backColor = Color.FromArgb(234, 247, 254);//背景色
        private Color _borderColor = Color.FromArgb(23, 169, 254);//框线颜色
        private Color _flashColor = Color.LightYellow;//呼吸灯效果颜色
        private Color _tipBackColor = Color.DarkBlue;//标签tip的背景色
        private Color _tipTextColor = Color.White;//标签tip的字体颜色
        /// <summary>
        /// tab标签圆角直径
        /// </summary>
        private static readonly int Radius = 8;
        private bool _haveCloseButton = false;
        private bool _drawTipText = false;
        private int _lastSelectIndex = -1;
        private int _selectIndex = 0;
        private Timer flashTime = new Timer();//控制标签页闪烁的时间控件     
        /// <summary>
        /// 具有呼吸灯效果标签的数组容器
        /// </summary>
        Dictionary<string, bool> tabFlickerDictionary = new Dictionary<string, bool>();
        /// <summary>
        /// 记录标签Tip文字的容器
        /// </summary>
        Dictionary<string, string> TipTextDictionary = new Dictionary<string, string>();
        #endregion

        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        public TabControlEx()
            : base()
        {
            flashTime.Interval = 50;//控制呼吸灯效果的时间控件
            flashTime.Enabled = false;
            flashTime.Tick += new EventHandler(tabFlicker_timer_Tick);
            _tabbaseColor = _baseColor;
            if (TabPages.Count > 0) _lastSelectIndex = 0;
            SetStyles();
        }

        #endregion

        #region 事件  
        /// <summary>
        /// 事件委托
        /// </summary>
        /// <param name="sender">传递本tabControl的指针</param>
        /// <param name="e"></param>
        public delegate void tabCloseEventHandle(object sender, MouseEventArgs e);
        /// <summary>
        /// 选中标签页在关闭前触发事件（tabcontrol的select标签页关闭前触发），与controlremoved事件相似
        /// </summary>
        [Browsable(true)]
        [Description("标签页点击关闭按钮后，关闭前触发的事件")]
        [EditorBrowsable(EditorBrowsableState.Always)]
        public event tabCloseEventHandle tabClose;
        #endregion

        #region 属性
        /// <summary>
        /// 控件基础颜色
        /// </summary>
        [Category("外观")]
        [Description("控件标签基础颜色")]
        [DefaultValue(typeof(Color), "166, 222, 255")]
        public Color BaseColor
        {
            get { return _baseColor; }
            set
            {
                _baseColor = value;
                base.Invalidate(true);
            }
        }
        /// <summary>
        /// 背景色
        /// </summary>
        [Browsable(true)]
        [Category("外观")]
        [Description("控件背景颜色")]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [DefaultValue(typeof(Color), "234, 247, 254")]
        public override Color BackColor
        {
            get { return _backColor; }
            set
            {
                _backColor = value;
                base.Invalidate(true);
            }
        }
        /// <summary>
        /// 边框线条颜色
        /// </summary>
        [Category("外观")]
        [Description("控件边框颜色")]
        [DefaultValue(typeof(Color), "23, 169, 254")]
        public Color BorderColor
        {
            get { return _borderColor; }
            set
            {
                _borderColor = value;
                base.Invalidate(true);
            }
        }
        /// <summary>
        /// Tab标签呼吸灯效果的颜色。
        /// </summary>
        [Category("外观")]
        [Description("控件标签呼吸灯颜色")]
        [DefaultValue(typeof(Color), "0, 95, 152")]
        public Color 标签呼吸灯颜色
        {
            get { return _flashColor; }
            set
            {
                _flashColor = value;
            }
        }
        /// <summary>
        /// 标签tip背景颜色
        /// </summary>
        [Category("外观")]
        [Description("标签tip背景颜色")]
        [DefaultValue(typeof(Color), "0, 128, 255")]
        public Color TipBackColor
        {
            get { return _tipBackColor; }
            set
            {
                _tipBackColor = value;
            }
        }
        /// <summary>
        /// 标签tip字体颜色
        /// </summary>
        [Category("外观")]
        [Description("标签tip字体颜色")]
        [DefaultValue(typeof(Color), "255, 255, 255")]
        public Color TipTextColor
        {
            get { return _tipTextColor; }
            set
            {
                _tipTextColor = value;
            }
        }
        /// <summary>
        /// 是否绘制关闭按钮
        /// </summary>
        [Category("外观")]
        [Description("是否绘制标签的关闭按钮")]
        [DefaultValue(typeof(bool), "false")]
        public bool HaveCloseButton
        {
            get { return _haveCloseButton; }
            set
            {
                _haveCloseButton = value;
                if (_haveCloseButton)    //绘制关闭按钮后对按钮周围空间量进行调整
                {
                    this.Padding = new Point(9, 3);
                }
                else { this.Padding = new Point(6, 3); }
            }
        }
        /// <summary>
        /// 是否在标签绘制提示
        /// </summary>
        [Category("外观")]
        [Description("是否绘制标签提示小字")]
        [DefaultValue(typeof(bool), "false")]
        public bool ShowDrawTipText
        {
            get { return _drawTipText; }
            set
            {
                _drawTipText = value;
                this.Padding = new Point(this.Padding.X, 4);
            }
        }
        #endregion

        #region 对控件部分方法重写       
        /// <summary>
        /// 对SelectedIndexChanged重写，在标签改变时记录上次选中的标签；移除当前标签特效
        /// </summary>
        /// <param name="e"></param>
        protected override void OnSelectedIndexChanged(EventArgs e)
        {
            SelectIndexLog();
            //以下判断语句移除当前选择项的呼吸灯效果和标签显示效果
            if (TabPages.Count > 0 & SelectedTab != null)
            {
                if (tabFlickerDictionary.ContainsKey(SelectedTab.Name)) tabFlickerRemove(SelectedTab.Name);
                if (TipTextDictionary.ContainsKey(SelectedTab.Name)) TipTextDictionary.Remove(SelectedTab.Name);
            }
            else if (TabPages.Count <= 0)
            {
                tabFlickerDictionary.Clear();
                TipTextDictionary.Clear();
            }

            base.OnSelectedIndexChanged(e);
        }
        /// <summary>
        /// 重写鼠标事件，鼠标在关闭区域点击时关闭选项卡
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseUp(MouseEventArgs e)
        {
            if (_haveCloseButton)
            {
                if (e.Button == MouseButtons.Left)
                {
                    Point cusorPoint = PointToClient(MousePosition);
                    //计算关闭区域  
                    Rectangle CloseRect = getCloseButtonRect(this.GetTabRect(SelectedIndex));
                    TabPage checkTab = this.SelectedTab;
                    //如果鼠标在区域内就关闭选项卡                      
                    if (CloseRect.Contains(cusorPoint))
                    {
                        if (tabClose != null)//移除选项卡之前执行tabclose事件，此事件在外部调用以在关闭标签时执行用户命令
                        { tabClose(this, e); }
                        SelectedIndex = _lastSelectIndex;      //此为设置选项卡为最后一次选择项，                 
                        this.TabPages.Remove(checkTab);
                        SelectIndexLog();
                    }
                }
            }
            base.OnMouseUp(e);
        }
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            base.Invalidate();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            base.Invalidate();
        }
        /// <summary>
        /// 重写绘制事件，执行自定义的标签绘制。关键事件
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            DrawTabContrl(e.Graphics);
        }

        #endregion

        #region 公开的自定义方法，在外部调用实现闪烁标签和标签TIP功能
        /// <summary>
        /// 将tabpage加入呼吸灯显示容器，使标签具有呼吸灯效果
        /// </summary>
        /// <param name="tabPageName"></param>
        public void tabFlickerAdd(string tabPageName)
        {
            if (SelectedTab != null)
            {
                if (SelectedTab.Name == tabPageName)    //不对当前标签添加呼吸灯效果
                { return; }
            }
            if (TabPages.ContainsKey(tabPageName))
            {
                if (!tabFlickerDictionary.ContainsKey(tabPageName))
                {
                    tabFlickerDictionary.Add(tabPageName, false);
                    if (!flashTime.Enabled) flashTime.Enabled = true;
                }
            }
        }
        /// <summary>
        /// 将tabpage移除呼吸灯显示容器
        /// </summary>
        /// <param name="tabPageName"></param>
        public void tabFlickerRemove(string tabPageName)
        {
            if (tabFlickerDictionary.ContainsKey(tabPageName))
            {
                tabFlickerDictionary.Remove(tabPageName);
            }
            if (tabFlickerDictionary.Count <= 0) flashTime.Enabled = false;
        }
        /// <summary>
        /// 将需要显示的Tip文字添加到容器以显示
        /// </summary>
        /// <param name="tabPageName"></param>
        /// <param name="text"></param>
        public void TipTextAdd(string tabPageName, string text)
        {
            if (SelectedTab != null)
            {
                if (SelectedTab.Name == tabPageName)    //不对当前标签添加Tip文字
                { return; }
            }
            if (TabPages.ContainsKey(tabPageName))
            {
                if (!TipTextDictionary.ContainsKey(tabPageName))
                {
                    TipTextDictionary.Add(tabPageName, text);
                }
                else
                {
                    TipTextDictionary[tabPageName] = text;
                }
            }
        }
        /// <summary>
        /// Tip文字不需要显示后从容器移除
        /// </summary>
        /// <param name="tabPageName"></param>
        public void TipTextRemove(string tabPageName)
        {
            if (TipTextDictionary.ContainsKey(tabPageName))
            {
                TipTextDictionary.Remove(tabPageName);
            }
        }
        #endregion

        #region 过程方法
        /// <summary>
        /// 对控件基础功能设置。自行绘制，双重缓存忽略WM_ERASEBKGND消息，调整大小重绘，接受ALPHA透明设置
        /// </summary>
        private void SetStyles()
        {
            base.SetStyle(
                 ControlStyles.UserPaint |                      // 控件将自行绘制，而不是通过操作系统来绘制  
                 ControlStyles.OptimizedDoubleBuffer |          // 该控件首先在缓冲区中绘制，而不是直接绘制到屏幕上，这样可以减少闪烁  
                 ControlStyles.AllPaintingInWmPaint |           // 控件将忽略 WM_ERASEBKGND 窗口消息以减少闪烁  
                 ControlStyles.ResizeRedraw |                   // 在调整控件大小时重绘控件  
                 ControlStyles.SupportsTransparentBackColor,    // 控件接受 alpha 组件小于 255 的 BackColor 以模拟透明  
                      true);                                    // 设置以上值为 true  
            base.UpdateStyles();
        }
        /// <summary>
        /// 整个控件重绘方法
        /// </summary>
        /// <param name="g"></param>
        private void DrawTabContrl(Graphics g)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.InterpolationMode = InterpolationMode.HighQualityBilinear;
            g.TextRenderingHint = TextRenderingHint.AntiAlias;
            DrawDrawBackgroundAndHeader(g);
            DrawTabPages(g);
            DrawBorder(g);
        }
        /// <summary>
        /// 绘制整体背景
        /// </summary>
        /// <param name="g">tabcontrol的绘图面</param>
        private void DrawDrawBackgroundAndHeader(Graphics g)
        {
            int x = 0;
            int y = 0;
            int width = 0;
            int height = 0;
            switch (Alignment)
            {
                case TabAlignment.Top:
                    x = 0;
                    y = 0;
                    width = ClientRectangle.Width;
                    height = ClientRectangle.Height - DisplayRectangle.Height;
                    break;
                case TabAlignment.Bottom:
                    x = 0;
                    y = DisplayRectangle.Height;
                    width = ClientRectangle.Width;
                    height = ClientRectangle.Height - DisplayRectangle.Height;
                    break;
                case TabAlignment.Left:
                    x = 0;
                    y = 0;
                    width = ClientRectangle.Width - DisplayRectangle.Width;
                    height = ClientRectangle.Height;
                    break;
                case TabAlignment.Right:
                    x = DisplayRectangle.Width;
                    y = 0;
                    width = ClientRectangle.Width - DisplayRectangle.Width;
                    height = ClientRectangle.Height;
                    break;
            }
            //标签所在的矩形
            Rectangle headerRect = new Rectangle(x, y, width, height);
            Color backColor = Enabled ? _backColor : SystemColors.Control;
            using (SolidBrush brush = new SolidBrush(backColor))
            {
                g.FillRectangle(brush, ClientRectangle);
                g.FillRectangle(brush, headerRect);
            }
        }
        /// <summary>
        /// 绘制tab标签
        /// </summary>
        /// <param name="g"></param>
        private void DrawTabPages(Graphics g)
        {
            Rectangle tabRect;
            Point cusorPoint = PointToClient(MousePosition);
            bool hover, hadSetClip = false;
            bool selected;
            bool alignHorizontal =
                (Alignment == TabAlignment.Top ||
                Alignment == TabAlignment.Bottom);
            LinearGradientMode mode = alignHorizontal ?
                LinearGradientMode.Vertical : LinearGradientMode.Horizontal;
            IntPtr updownHandle = UpDownButtonHandle;
            if (updownHandle != IntPtr.Zero)////此段代码重设标签绘图区域，将左右键区域排除在外，消除重叠绘图的错误
            {
                Rectangle ExludeRect = upDownRect(updownHandle);
                Rectangle tempClip = new Rectangle();
                tempClip.X = ClientRectangle.X;
                tempClip.Y = ClientRectangle.Y;
                tempClip.Width = ClientRectangle.Width - ExludeRect.Width;
                tempClip.Height = ClientRectangle.Height;
                g.SetClip(tempClip);
                hadSetClip = true;
            }

            for (int index = 0; index < base.TabCount; index++)
            {
                TabPage page = TabPages[index];
                tabRect = GetTabRect(index);
                Bitmap bmp = new Bitmap(tabRect.Width, tabRect.Height);      //使用双重缓存，先将标签绘制到bmp，然后再绘制到界面，防止界面闪烁         
                Rectangle tmpRect = new Rectangle(0, 0, tabRect.Width, tabRect.Height);
                if (Alignment == TabAlignment.Bottom)   //在底部的时候，重新绘制标签会导致工作区和标签区重叠的边线绘图错误，不对重叠部分绘制
                {
                    tmpRect = new Rectangle(0, -1, tabRect.Width, tabRect.Height);
                }
                else if (Alignment == TabAlignment.Left || Alignment == TabAlignment.Right)//绘制不了边线，将区域扩大以绘制边线
                {
                    bmp = new Bitmap(tabRect.Width + 1, tabRect.Height + 1);
                }

                Graphics gt = Graphics.FromImage(bmp);
                gt.SmoothingMode = SmoothingMode.AntiAlias;
                gt.InterpolationMode = InterpolationMode.HighQualityBilinear;
                gt.TextRenderingHint = TextRenderingHint.AntiAlias;
                hover = tabRect.Contains(cusorPoint);
                selected = SelectedIndex == index;
                Color baseColor = _baseColor;
                if (tabFlickerDictionary.ContainsKey(page.Name))
                {
                    baseColor = _tabbaseColor;
                }
                Color borderColor = _borderColor;
                if (selected)
                {
                    baseColor = GetColor(_baseColor, 0, -45, -30, -14);
                }
                else if (hover)
                {
                    baseColor = GetColor(_baseColor, 0, 35, 24, 9);
                }
                RenderTabBackgroundInternal(
                    gt,
                    tmpRect,
                    baseColor,
                    borderColor,
                    .45F,
                    mode);
                bool hasImage = DrawTabImage(gt, page, tmpRect);
                DrawtabText(gt, page, tmpRect, hasImage);
                ////////////此段代码尝试绘制关闭图标        
                if (HaveCloseButton)
                {
                    Point temPoint = new Point(cusorPoint.X - tabRect.X, cusorPoint.Y - tabRect.Y);
                    DrawCloseButton(gt, tmpRect, temPoint);
                }
                ////以下绘制TipText               
                if (ShowDrawTipText & TipTextDictionary.ContainsKey(page.Name))
                {
                    if (TipTextDictionary[page.Name] != null)
                    {
                        string text = TipTextDictionary[page.Name];
                        if (text != "")
                            DrawTipText(gt, tmpRect, text);
                    }
                }


                if (Alignment == TabAlignment.Bottom)
                { g.DrawImage(bmp, tabRect.X, tabRect.Y + 1); }
                else
                    g.DrawImage(bmp, tabRect.X, tabRect.Y);
                bmp.Dispose();
                gt.Dispose();
            }
            if (hadSetClip) g.ResetClip();
        }

        /// <summary>
        /// 按时间重设呼吸灯效果颜色
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void tabFlicker_timer_Tick(object sender, EventArgs e)
        {
            ima = ima + key;
            if (ima >= step | ima <= 0) key = key * -1;
            Color flashColor = _flashColor;
            br = (int)(-(_baseColor.R - flashColor.R) / step * ima);
            bb = (int)(-(_baseColor.B - flashColor.B) / step * ima);
            bg = (int)(-(_baseColor.G - flashColor.G) / step * ima);
            _tabbaseColor = GetColor(_baseColor, 0, br, bg, bb);
            Graphics g = Graphics.FromHwnd(this.Handle);
            if (g != null) DrawTabPages(g);
        }
        /// <summary>
        /// 根据标签页矩形区，计算关闭按钮的矩形区
        /// </summary>
        /// <param name="tabRect">标签页矩形区</param>
        /// <returns>按钮矩形区</returns>
        private Rectangle getCloseButtonRect(Rectangle tabRect)
        {
            if (Alignment == TabAlignment.Top | Alignment == TabAlignment.Bottom)
            {
                tabRect.Offset(tabRect.Width - 21, tabRect.Height / 2 - 7);
            }
            else if (Alignment == TabAlignment.Left)
            {
                tabRect.Offset(tabRect.Width / 2 - 8, 7);
            }
            else if (Alignment == TabAlignment.Right)
            {
                tabRect.Offset(tabRect.Width / 2 - 7, tabRect.Height - 21);
            }
            tabRect.Height = 15;
            tabRect.Width = 15;
            return tabRect;
        }
        /// <summary>
        /// 绘制关闭按钮
        /// </summary>
        /// <param name="g"></param>
        /// <param name="tabRect">标签的框体</param>
        /// <param name="cusorPoint">鼠标在工作区的相对坐标值</param>
        private void DrawCloseButton(Graphics g, Rectangle tabRect, Point cusorPoint)
        {
            Rectangle CBRect = getCloseButtonRect(tabRect);
            float i = 3.9F;
            float penWidth = 1.52F;
            Color lineColor = Color.FromArgb(200, 0, 0, 0);
            if (CBRect.Contains(cusorPoint))
            {
                using (SolidBrush brush = new SolidBrush(Color.FromArgb(170, 60, 0, 0)))
                {
                    g.FillEllipse(brush, CBRect);
                }
                lineColor = Color.FromArgb(200, 255, 255, 255);//此句作用为在绘制背景小圆的时候对关闭十字颜色重设
                penWidth = 1.5F;
            }
            using (Pen brush = new Pen(lineColor, penWidth))
            {
                g.DrawLine(brush, CBRect.X + i, CBRect.Y + i, CBRect.Right - i, CBRect.Bottom - i);
                g.DrawLine(brush, CBRect.X + i, CBRect.Bottom - i, CBRect.Right - i, CBRect.Top + i);

            }
        }
        /// <summary>
        /// 绘制标签Tip文字
        /// </summary>
        /// <param name="g">绘图面</param>
        /// <param name="tabRect">标签矩形框体</param>
        /// <param name="text">需要绘制的文字</param>
        private void DrawTipText(Graphics g, Rectangle tabRect, string text)
        {
            int num = text.Length, fontSize = 7;
            if (Encoding.ASCII.GetString(Encoding.ASCII.GetBytes(text)) != text)
            {
                fontSize = 5;//判断字符串是否纯字母，存在中文则变字体大小
                num = num + 1;
            }
            Rectangle tipRect = getTipRect(tabRect, num);
            GraphicsPath tipPath = CreateTipPath(tipRect);
            using (SolidBrush brush = new SolidBrush(Color.FromArgb(150, _tipBackColor)))
            {
                g.FillPath(brush, tipPath);
            }

            Font f = new System.Drawing.Font(this.Font.FontFamily, fontSize, this.Font.Style);
            tipRect.Offset(1, -1);
            TextRenderer.DrawText(
                    g,
                    text,
                    f,
                    tipRect,
                    _tipTextColor);
        }
        /// <summary>
        /// 获取Tip绘制矩形框
        /// </summary>
        /// <param name="tabRect">标签框体</param>
        /// <param name="textLength">字体长度</param>
        /// <returns></returns>
        private Rectangle getTipRect(Rectangle tabRect, int textLength)
        {
            if (Alignment == TabAlignment.Top | Alignment == TabAlignment.Bottom)
            {
                tabRect.Offset(2, 1);
                tabRect.Width++;
            }
            else if (Alignment == TabAlignment.Left)
            {
                tabRect.Offset(1, 1);
            }
            else if (Alignment == TabAlignment.Right)
            {
                tabRect.Offset(1, 1);
            }
            tabRect.Height = 10;
            tabRect.Width = 4 + textLength * 7;
            return tabRect;
        }
        /// <summary>
        /// 根据tiprect框体绘制有圆角的框体路径
        /// </summary>
        /// <param name="rect">tiprect</param>
        /// <returns></returns>
        private GraphicsPath CreateTipPath(Rectangle rect)
        {
            GraphicsPath path = new GraphicsPath();
            path.AddArc(
                rect.X,
                rect.Y,
               rect.Height,
                rect.Height,
                90F,
                180F);
            path.AddLine(
                rect.X + rect.Height / 2,
                rect.Y,
                rect.Right - rect.Height / 2,
                rect.Y);
            path.AddArc(
                rect.Right - rect.Height,
                rect.Y,
                rect.Height,
                rect.Height,
                270F,
                180F);
            path.AddLine(
                rect.X + rect.Height / 2,
                rect.Bottom,
                rect.Right - rect.Height / 2,
                rect.Bottom);

            path.CloseFigure();
            return path;
        }
        /// <summary>
        /// 绘制标签的文字
        /// </summary>
        /// <param name="g">tabcontrol的graphics</param>
        /// <param name="page">标签页tabpage</param>
        /// <param name="tabRect">标签的框体</param>
        /// <param name="hasImage">是否绘制了图片</param>
        private void DrawtabText(
            Graphics g, TabPage page, Rectangle tabRect, bool hasImage)
        {
            Rectangle textRect = tabRect;
            RectangleF newTextRect;
            StringFormat sf;
            Point padding = this.Padding;
            switch (Alignment)
            {
                case TabAlignment.Top:
                case TabAlignment.Bottom:
                    if (_haveCloseButton)
                    {
                        if (hasImage)
                        {
                            textRect.X = tabRect.X + Radius / 2 + tabRect.Height - padding.X + 6;
                            textRect.Width = tabRect.Width - Radius - tabRect.Height - 10;
                        }
                        else
                        {
                            textRect.X = tabRect.X + 3 - padding.X;
                        }
                    }
                    else
                    {
                        if (hasImage)
                        {
                            textRect.X = tabRect.X + Radius / 2 + tabRect.Height - 2;
                            textRect.Width = tabRect.Width - Radius - tabRect.Height;
                        }
                    }
                    TextRenderer.DrawText(
                        g,
                        page.Text,
                        page.Font,
                        textRect,
                        page.ForeColor);
                    break;
                case TabAlignment.Left:
                    if (_haveCloseButton)
                    {
                        if (hasImage)
                        {
                            textRect.Height = tabRect.Height - tabRect.Width + padding.X + 5;
                        }
                        else
                        {
                            textRect.Height = tabRect.Height + padding.X + 5;
                        }
                    }
                    else
                    {
                        if (hasImage)
                        {
                            textRect.Height = tabRect.Height - tabRect.Width + 2;
                        }
                    }
                    g.TranslateTransform(textRect.X, textRect.Bottom);
                    g.RotateTransform(270F);
                    sf = new StringFormat(StringFormatFlags.NoWrap);
                    sf.Alignment = StringAlignment.Center;
                    sf.LineAlignment = StringAlignment.Center;
                    sf.Trimming = StringTrimming.Character;
                    newTextRect = textRect;
                    newTextRect.X = 0;
                    newTextRect.Y = 0;
                    newTextRect.Width = textRect.Height;
                    newTextRect.Height = textRect.Width;
                    using (Brush brush = new SolidBrush(page.ForeColor))
                    {
                        g.DrawString(
                            page.Text,
                            page.Font,
                            brush,
                            newTextRect,
                            sf);
                    }
                    g.ResetTransform();
                    break;
                case TabAlignment.Right:
                    if (_haveCloseButton)
                    {
                        if (hasImage)
                        {
                            textRect.Y = tabRect.Y + Radius / 2 + tabRect.Width - padding.X;
                            textRect.Height = tabRect.Height - Radius - tabRect.Width;
                        }
                        else
                        {
                            textRect.Y = tabRect.Y + Radius / 2 + padding.X - 2;
                            textRect.Height = tabRect.Height - Radius - tabRect.Width;
                        }
                    }
                    else
                    {
                        if (hasImage)
                        {
                            textRect.Y = tabRect.Y + Radius / 2 + tabRect.Width - 2;
                            textRect.Height = tabRect.Height - Radius - tabRect.Width;
                        }
                    }
                    g.TranslateTransform(textRect.Right, textRect.Y);
                    g.RotateTransform(90F);
                    sf = new StringFormat(StringFormatFlags.NoWrap);
                    sf.Alignment = StringAlignment.Center;
                    sf.LineAlignment = StringAlignment.Center;
                    sf.Trimming = StringTrimming.Character;
                    newTextRect = textRect;
                    newTextRect.X = 0;
                    newTextRect.Y = 0;
                    newTextRect.Width = textRect.Height;
                    newTextRect.Height = textRect.Width;
                    using (Brush brush = new SolidBrush(page.ForeColor))
                    {
                        g.DrawString(
                            page.Text,
                            page.Font,
                            brush,
                            newTextRect,
                            sf);
                    }
                    g.ResetTransform();
                    break;
            }
        }
        /// <summary>
        /// 绘制框线
        /// </summary>
        /// <param name="g"></param>
        private void DrawBorder(Graphics g)
        {
            if (SelectedIndex != -1)
            {
                Rectangle tabRect = GetTabRect(SelectedIndex);
                Rectangle clipRect = ClientRectangle;
                Point[] points = new Point[6];
                switch (Alignment)
                {
                    case TabAlignment.Top:
                        points[0] = new Point(
                            tabRect.X,
                            tabRect.Bottom);
                        points[1] = new Point(
                            clipRect.X,
                            tabRect.Bottom);
                        points[2] = new Point(
                            clipRect.X,
                            clipRect.Bottom - 1);
                        points[3] = new Point(
                            clipRect.Right - 1,
                            clipRect.Bottom - 1);
                        points[4] = new Point(
                            clipRect.Right - 1,
                            tabRect.Bottom);
                        points[5] = new Point(
                            tabRect.Right,
                            tabRect.Bottom);
                        break;
                    case TabAlignment.Bottom:
                        points[0] = new Point(
                            tabRect.X,
                            tabRect.Y);
                        points[1] = new Point(
                            clipRect.X,
                            tabRect.Y);
                        points[2] = new Point(
                            clipRect.X,
                            clipRect.Y);
                        points[3] = new Point(
                            clipRect.Right - 1,
                            clipRect.Y);
                        points[4] = new Point(
                            clipRect.Right - 1,
                            tabRect.Y);
                        points[5] = new Point(
                            tabRect.Right,
                            tabRect.Y);
                        break;
                    case TabAlignment.Left:
                        points[0] = new Point(
                            tabRect.Right,
                            tabRect.Y);
                        points[1] = new Point(
                            tabRect.Right,
                            clipRect.Y);
                        points[2] = new Point(
                            clipRect.Right - 1,
                            clipRect.Y);
                        points[3] = new Point(
                            clipRect.Right - 1,
                            clipRect.Bottom - 1);
                        points[4] = new Point(
                            tabRect.Right,
                            clipRect.Bottom - 1);
                        points[5] = new Point(
                            tabRect.Right,
                            tabRect.Bottom);
                        break;
                    case TabAlignment.Right:
                        points[0] = new Point(
                            tabRect.X,
                            tabRect.Y);
                        points[1] = new Point(
                            tabRect.X,
                            clipRect.Y);
                        points[2] = new Point(
                            clipRect.X,
                            clipRect.Y);
                        points[3] = new Point(
                            clipRect.X,
                            clipRect.Bottom - 1);
                        points[4] = new Point(
                            tabRect.X,
                            clipRect.Bottom - 1);
                        points[5] = new Point(
                            tabRect.X,
                            tabRect.Bottom);
                        break;
                }
                using (Pen pen = new Pen(_borderColor))
                {
                    g.DrawLines(pen, points);
                }
            }
        }

        /// <summary>
        /// 绘制标签背景
        /// </summary>
        /// <param name="g"></param>
        /// <param name="rect"></param>
        /// <param name="baseColor"></param>
        /// <param name="borderColor"></param>
        /// <param name="basePosition"></param>
        /// <param name="mode"></param>
        internal void RenderTabBackgroundInternal(
          Graphics g,
          Rectangle rect,
          Color baseColor,
          Color borderColor,
          float basePosition,
          LinearGradientMode mode)
        {
            using (GraphicsPath path = CreateTabPath(rect))
            {
                using (LinearGradientBrush brush = new LinearGradientBrush(
                   rect, Color.Transparent, Color.Transparent, mode))
                {
                    Color[] colors = new Color[4];
                    colors[0] = GetColor(baseColor, 0, 35, 24, 9);
                    colors[1] = GetColor(baseColor, 0, 13, 8, 3);
                    colors[2] = baseColor;
                    colors[3] = GetColor(baseColor, 0, 68, 69, 54);

                    ColorBlend blend = new ColorBlend();
                    blend.Positions =
                        new float[] { 0.0f, basePosition, basePosition + 0.05f, 1.0f };
                    blend.Colors = colors;
                    brush.InterpolationColors = blend;
                    g.FillPath(brush, path);
                }

                if (baseColor.A > 80)
                {
                    Rectangle rectTop = rect;
                    if (mode == LinearGradientMode.Vertical)
                    {
                        rectTop.Height = (int)(rectTop.Height * basePosition);
                    }
                    else
                    {
                        rectTop.Width = (int)(rect.Width * basePosition);
                    }
                    using (SolidBrush brushAlpha =
                        new SolidBrush(Color.FromArgb(80, 255, 255, 255)))
                    {
                        g.FillRectangle(brushAlpha, rectTop);
                    }
                }

                rect.Inflate(-1, -1);
                using (GraphicsPath path1 = CreateTabPath(rect))
                {
                    using (Pen pen = new Pen(Color.FromArgb(255, 255, 255)))
                    {
                        if (Multiline)
                        {
                            g.DrawPath(pen, path1);
                        }
                        else
                        {
                            g.DrawLines(pen, path1.PathPoints);
                        }
                    }
                }

                using (Pen pen = new Pen(borderColor))
                {
                    if (Multiline)
                    {
                        g.DrawPath(pen, path);
                    }
                    {
                        g.DrawLines(pen, path.PathPoints);
                    }
                }
            }
        }
        /// <summary>
        /// 绘制标签图标
        /// </summary>
        /// <param name="g"></param>
        /// <param name="page"></param>
        /// <param name="rect"></param>
        /// <returns></returns>
        private bool DrawTabImage(Graphics g, TabPage page, Rectangle rect)
        {
            bool hasImage = false;
            if (ImageList != null)
            {
                Image image = null;
                if (page.ImageIndex != -1)
                {
                    image = ImageList.Images[page.ImageIndex];
                }
                else if (page.ImageKey != null)
                {
                    image = ImageList.Images[page.ImageKey];
                }

                if (image != null)
                {
                    hasImage = true;
                    Rectangle destRect = Rectangle.Empty;
                    Rectangle srcRect = new Rectangle(Point.Empty, image.Size);
                    switch (Alignment)
                    {
                        case TabAlignment.Top:
                        case TabAlignment.Bottom:
                            destRect = new Rectangle(
                                 rect.X + Radius / 2 + 2,
                                 rect.Y + 2,
                                 rect.Height - 4,
                                 rect.Height - 4);
                            break;
                        case TabAlignment.Left:
                            destRect = new Rectangle(
                                rect.X + 2,
                                rect.Bottom - (rect.Width - 4) - Radius / 2 - 2,
                                rect.Width - 4,
                                rect.Width - 4);
                            break;
                        case TabAlignment.Right:
                            destRect = new Rectangle(
                                rect.X + 2,
                                rect.Y + Radius / 2 + 2,
                                rect.Width - 4,
                                rect.Width - 4);
                            break;
                    }

                    g.DrawImage(
                        image,
                        destRect,
                        srcRect,
                        GraphicsUnit.Pixel);
                }
            }
            return hasImage;
        }
        /// <summary>
        /// 绘制tab标签的框体
        /// </summary>
        /// <param name="rect">tab框体矩形的位置和大小</param>
        /// <returns>返回一个绘制的框体</returns>
        private GraphicsPath CreateTabPath(Rectangle rect)
        {
            GraphicsPath path = new GraphicsPath();
            switch (Alignment)
            {
                case TabAlignment.Top:
                    rect.X++;
                    rect.Width -= 2;
                    path.AddLine(
                        rect.X,
                        rect.Bottom,
                        rect.X,
                        rect.Y + Radius / 2);
                    path.AddArc(
                        rect.X,
                        rect.Y,
                        Radius,
                        Radius,
                        180F,
                        90F);
                    path.AddArc(
                        rect.Right - Radius,
                        rect.Y,
                        Radius,
                        Radius,
                        270F,
                        90F);
                    path.AddLine(
                        rect.Right,
                        rect.Y + Radius / 2,
                        rect.Right,
                        rect.Bottom);
                    break;
                case TabAlignment.Bottom:
                    rect.X++;
                    rect.Width -= 2;
                    path.AddLine(
                        rect.X,
                        rect.Y,
                        rect.X,
                        rect.Bottom - Radius / 2);
                    path.AddArc(
                        rect.X,
                        rect.Bottom - Radius,
                        Radius,
                        Radius,
                        180,
                        -90);
                    path.AddLine(
                       rect.X + Radius / 2,
                       rect.Bottom,
                       rect.Right - Radius / 2,
                       rect.Bottom);
                    path.AddArc(
                        rect.Right - Radius,
                        rect.Bottom - Radius,
                        Radius,
                        Radius,
                        90,
                        -90);
                    path.AddLine(
                        rect.Right,
                        rect.Bottom - Radius / 2,
                        rect.Right,
                        rect.Y);

                    break;
                case TabAlignment.Left:
                    rect.Y++;
                    rect.Height -= 2;
                    path.AddLine(
                        rect.Right,
                        rect.Y,
                        rect.X + Radius / 2,
                        rect.Y);
                    path.AddArc(
                        rect.X,
                        rect.Y,
                        Radius,
                        Radius,
                        270F,
                        -90F);
                    path.AddArc(
                        rect.X,
                        rect.Bottom - Radius,
                        Radius,
                        Radius,
                        180F,
                        -90F);
                    path.AddLine(
                        rect.X + Radius / 2,
                        rect.Bottom,
                        rect.Right,
                        rect.Bottom);
                    break;
                case TabAlignment.Right:
                    rect.Y++;
                    rect.Height -= 2;
                    path.AddLine(
                        rect.X,
                        rect.Y,
                        rect.Right - Radius / 2,
                        rect.Y);
                    path.AddArc(
                        rect.Right - Radius,
                        rect.Y,
                        Radius,
                        Radius,
                        270F,
                        90F);
                    path.AddArc(
                        rect.Right - Radius,
                        rect.Bottom - Radius,
                        Radius,
                        Radius,
                        0F,
                        90F);
                    path.AddLine(
                        rect.Right - Radius / 2,
                        rect.Bottom,
                        rect.X,
                        rect.Bottom);
                    break;
            }
            path.CloseFigure();
            return path;
        }
        /// <summary>
        /// 自定义颜色位偏移调整函数
        /// </summary>
        /// <param name="colorBase"></param>
        /// <param name="a"></param>
        /// <param name="r"></param>
        /// <param name="g"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        private Color GetColor(Color colorBase, int a, int r, int g, int b)
        {
            int a0 = colorBase.A;
            int r0 = colorBase.R;
            int g0 = colorBase.G;
            int b0 = colorBase.B;

            if (a + a0 > 255) { a = 255; } else { a = Math.Max(a + a0, 0); }
            if (r + r0 > 255) { r = 255; } else { r = Math.Max(r + r0, 0); }
            if (g + g0 > 255) { g = 255; } else { g = Math.Max(g + g0, 0); }
            if (b + b0 > 255) { b = 255; } else { b = Math.Max(b + b0, 0); }

            return Color.FromArgb(a, r, g, b);
        }
        /// <summary>
        /// 记录选项卡上次选中的标签Index编号。
        /// </summary>
        private void SelectIndexLog()
        {
            _lastSelectIndex = _selectIndex;
            _selectIndex = SelectedIndex;
        }
        /// <summary>
        /// 得到左右箭头绘制区域，以从标签绘制区域移除。防止标签绘图区与箭头绘图区重叠
        /// </summary>
        /// <param name="upDownHandle">左右箭头句柄</param>
        /// <returns></returns>
        private Rectangle upDownRect(IntPtr upDownHandle)
        {
            Rectangle udRect = new Rectangle();
            if (upDownHandle != IntPtr.Zero)
            {
                if (IsWindowVisible(upDownHandle))
                {
                    RECT upDownRect = new RECT();
                    GetClientRect(upDownHandle, ref upDownRect);
                    udRect = Rectangle.FromLTRB(upDownRect.Left, upDownRect.Top, upDownRect.Right, upDownRect.Bottom);
                }
            }
            return udRect;
        }
        #endregion
    }

}
