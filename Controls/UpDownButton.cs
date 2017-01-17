using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.ComponentModel;

namespace YUNkefu
{
     partial class TabControlEx
     {
         /////// 
         ///////  此页代码解决左右箭头绘制问题
         /////// 

         #region 左右键绘制需要定义的一些参数
         private UpDownButtonNativeWindow _upDownButtonNativeWindow;
         private bool _drawUpDownButtonByCustom = true;
         private Color _arrowColor = Color.FromArgb(0, 79, 125);//箭头颜色
         /// <summary>
         /// 左右箭头颜色
         /// </summary>
         [Category("外观")]
         [Description("控件左右箭头颜色")]
         [DefaultValue(typeof(Color), "0, 95, 152")]
         public Color ArrowColor
         {
             get { return _arrowColor; }
             set
             {
                 _arrowColor = value;
                 base.Invalidate(true);
             }
         }
         /// <summary>
         /// 左右箭头是否自定义绘制
         /// </summary>
         [Browsable(true)]
         [Category("外观")]
         [Description("左右箭头是否由用户绘制，应对部分电脑同时选择\n呼吸灯效果和自绘箭头可能导致的闪烁问题")]
         [EditorBrowsable(EditorBrowsableState.Always)]
         [DefaultValue(typeof(bool), "true")]
         public bool DrawUpDownButtonByCustom
         {
             get { return _drawUpDownButtonByCustom; }
             set
             {
                 _drawUpDownButtonByCustom = value;
                 base.Invalidate(true);
             }
         }
#endregion
         #region 自定义左右键绘制事件
         private static readonly object EventPaintUpDownButton = new object();
         /// <summary>
         ///第一个参数是一个键值，这样添加以后，可以通过下面的代码获取该事件base.Events[EventPaintUpDownButton] as EventHandler;
         /// </summary>
         public event UpDownButtonPaintEventHandler PaintUpDownButton
         {
             add { base.Events.AddHandler(EventPaintUpDownButton, value); }
             remove { base.Events.RemoveHandler(EventPaintUpDownButton, value); }
         }
         //定义一个EventHandler委托类型的事件属性，该事件可以用EventHandler类型的委托进行处理
#endregion
         #region 以下为win32API的引用


         public const int WM_PAINT = 0xF;

         public const int VK_LBUTTON = 0x1;
         public const int VK_RBUTTON = 0x2;

         private const int TCM_FIRST = 0x1300;
         public const int TCM_GETITEMRECT = (TCM_FIRST + 10);

         public static readonly IntPtr TRUE = new IntPtr(1);
         /// <summary>
         /// 该结构体包含了某应用程序用来绘制它所拥有的窗口客户区所需要的信息。
         /// </summary>
         [StructLayout(LayoutKind.Sequential)]
         public struct PAINTSTRUCT
         {
             internal IntPtr hdc;
             internal int fErase;
             internal RECT rcPaint;
             internal int fRestore;
             internal int fIncUpdate;
             internal int Reserved1;
             internal int Reserved2;
             internal int Reserved3;
             internal int Reserved4;
             internal int Reserved5;
             internal int Reserved6;
             internal int Reserved7;
             internal int Reserved8;
         }
         /// <summary>
         /// 矩形坐标块
         /// </summary>
         [StructLayout(LayoutKind.Sequential)]
         public struct RECT
         {
             internal RECT(int X, int Y, int Width, int Height)
             {
                 this.Left = X;
                 this.Top = Y;
                 this.Right = Width;
                 this.Bottom = Height;
             }
             internal int Left;
             internal int Top;
             internal int Right;
             internal int Bottom;
         }

         [DllImport("user32.dll")]
         public static extern IntPtr FindWindowEx(
             IntPtr hwndParent,
             IntPtr hwndChildAfter,
             string lpszClass,
             string lpszWindow);
         /// <summary>
         /// BeginPaint函数为指定窗口进行绘图工作的准备，并用将和绘图有关的信息填充到一个PAINTSTRUCT结构中。
         /// </summary>
         /// <param name="hWnd">[输入]被重绘的窗口句柄</param>
         /// <param name="ps">:[输出]指向一个用来接收绘画信息的PAINTSTRUCT结构</param>
         /// <returns>如果函数成功，返回值是指定窗口的“显示设备描述表”句柄。如果函数失败，返回值是NULL，表明没有得到显示设备的内容。</returns>
         [DllImport("user32.dll")]
         public static extern IntPtr BeginPaint(IntPtr hWnd, ref PAINTSTRUCT ps);
         /// <summary>
         /// EndPaint函数标记指定窗口的绘画过程结束；这个函数在每次调用BeginPaint函数之后被请求，但仅仅在绘画完成以后。
         /// </summary>
         /// <param name="hWnd">[输入]已经被重画的窗口的HANDLE</param>
         /// <param name="ps">[输入]指向一个PAINTSTRUCT结构，该结构包含了绘画信息，是BeginPaint函数返回的返回值</param>
         /// <returns>返回值始终是非0</returns>
         [DllImport("user32.dll")]
         [return: MarshalAs(UnmanagedType.Bool)]
         public static extern bool EndPaint(IntPtr hWnd, ref PAINTSTRUCT ps);
         /// <summary>
         /// 该函数检取指定虚拟键的状态。该状态指定此键是UP状态，DOWN状态，还是被触发的（开关每次按下此键时进行切换）。
         /// </summary>
         /// <param name="nVirtKey">检查的虚拟键码</param>
         /// <returns>> 大于0 没按下，小于0被按下</returns>
         [DllImport("user32.dll")]
         public static extern short GetKeyState(int nVirtKey);
         /// <summary>
         /// 
         /// </summary>
         /// <param name="hWnd"></param>
         /// <param name="Msg"></param>
         /// <param name="wParam"></param>
         /// <param name="lParam">传递rect类型矩形坐标</param>
         /// <returns></returns>
         [DllImport("user32.dll")]
         public static extern IntPtr SendMessage(
             IntPtr hWnd, int Msg, int wParam, ref RECT lParam);
         /// <summary>
         /// 该函数检取光标的位置，以屏幕坐标表示
         /// </summary>
         /// <param name="lpPoint"></param>
         /// <returns></returns>
         [DllImport("user32.dll")]
         [return: MarshalAs(UnmanagedType.Bool)]
         public static extern bool GetCursorPos(ref Point lpPoint);
         /// <summary>
         /// 函数将指定的矩形移动到指定的位置,如果函数成功，返回非0，否则返回0.
         /// </summary>
         /// <param name="lpRect">[输入输出]指向一个RECT结构，其中包含了被移动矩形的逻辑坐标</param>
         /// <param name="x">[输入]指定的矩形左右移动的量。当向左移动的时候，这个参数必须是一个负值</param>
         /// <param name="y">[输入]指定的矩形上下移动的量。当想上移动的时候，这个参数应该是一个负值</param>
         /// <returns></returns>
         [DllImport("user32.dll")]
         public extern static int OffsetRect(ref RECT lpRect, int x, int y);
         /// <summary>
         /// 判断一个点是否在Rect中
         /// </summary>
         /// <param name="lprc">一个指向RECT类型的常量指针，也就是说这个值是你要进行点是否在RECT对象的RECT类型的变量</param>
         /// <param name="pt">一个类型为POINT类型的变量，也就是你要进行判断点是否在RECT对象的点</param>
         /// <returns>如果点a在rect对象中，那么返回值为非零，否则返回值为0</returns>
         [DllImport("user32.dll")]
         [return: MarshalAs(UnmanagedType.Bool)]
         public static extern bool PtInRect([In] ref RECT lprc, Point pt);
         /// <summary>
         /// 该函数返回指定窗口的边框矩形的尺寸。该尺寸以相对于屏幕坐标左上角的屏幕坐标给出。
         /// </summary>
         /// <param name="hWnd">窗口句柄</param>
         /// <param name="lpRect">指向一个RECT结构的指针，该结构接收窗口的左上角和右下角的屏幕坐标。</param>
         /// <returns>如果函数成功，返回值为非零：如果函数失败，返回值为零。若想获得更多错误信息，请调用GetLastError函数</returns>
         [DllImport("user32.dll")]
         [return: MarshalAs(UnmanagedType.Bool)]
         public static extern bool GetWindowRect(IntPtr hWnd, ref RECT lpRect);
         /// <summary>
         /// 该函数获取窗口客户区的坐标。客户区坐标指定客户区的左上角和右下角。由于客户区坐标是相对窗口客户区的左上角而言的，因此左上角坐标为（0，0）。
         /// </summary>
         /// <param name="hWnd">是程序窗口的句柄。</param>
         /// <param name="r">指向一个RECT类型的rectangle结构</param>
         /// <returns>如果函数成功，返回一个非零值。</returns>
         [DllImport("user32.dll")]
         [return: MarshalAs(UnmanagedType.Bool)]
         public static extern bool GetClientRect(IntPtr hWnd, ref RECT r);
         /// <summary>
         /// 通过该函数可以获得指定窗口的可视状态，即显示或者隐藏。
         /// </summary>
         /// <param name="hwnd"></param>
         /// <returns></returns>
         [DllImport("User32.dll", CharSet = CharSet.Auto)]
         public static extern bool IsWindowVisible(IntPtr hwnd);
         #endregion
         #region 以下多个事件重写，目的是在需要绘制左右箭头时触发事件绘制箭头
         protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            checkUpDownButton();
        }
         /// <summary>
         /// 检查是否存在左右按钮，有则以句柄创建一个upDownButtonNativeWindow。如果没有则将分配的的内存释放。
         /// </summary>
         private void checkUpDownButton()
         {
             if (UpDownButtonHandle != IntPtr.Zero)
             {
                 if (_upDownButtonNativeWindow == null)
                 {
                     _upDownButtonNativeWindow = new UpDownButtonNativeWindow(this);
                 }
             }
             else 
             {
                 if (_upDownButtonNativeWindow != null)
                 {
                     _upDownButtonNativeWindow.Dispose();
                     _upDownButtonNativeWindow = null;
                 }
             }
         }
        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            checkUpDownButton();
        }

        protected override void OnHandleDestroyed(EventArgs e)
        {
            base.OnHandleDestroyed(e);
            if (_upDownButtonNativeWindow != null)
            {
                _upDownButtonNativeWindow.Dispose();
                _upDownButtonNativeWindow = null;
            }
        }

        protected override void OnControlAdded(ControlEventArgs e)
        {
            base.OnControlAdded(e);
            checkUpDownButton();
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            checkUpDownButton();
        }

        protected override void OnControlRemoved(ControlEventArgs e)
        {
            base.OnControlRemoved(e);
            checkUpDownButton();
        }
        #endregion

        #region 左右键绘制方法

        /// <summary>
        /// 对左右键进行重绘
        /// </summary>
        /// <param name="e">左右键的参数，继承painteventargs</param>
        protected virtual void OnPaintUpDownButton(
            UpDownButtonPaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Rectangle rect = e.ClipRectangle;//程序分配给左右箭头的矩形

            Color upButtonBaseColor = _baseColor;
            Color upButtonBorderColor = _borderColor;
            Color upButtonArrowColor = _arrowColor;

            Color downButtonBaseColor = _baseColor;
            Color downButtonBorderColor = _borderColor;
            Color downButtonArrowColor = _arrowColor;
            Rectangle upButtonRect = rect;

            Bitmap bmp = new Bitmap(rect.Width, rect.Height);
            Graphics gb = Graphics.FromImage(bmp);
            Rectangle bmpRect = new Rectangle(0, 0, rect.Width, rect.Height);
            upButtonRect.X = 1;
            upButtonRect.Y = 1;
            upButtonRect.Width = rect.Width / 2 - 4;
            upButtonRect.Height -= 2;

            Rectangle downButtonRect = rect;
            downButtonRect.X = upButtonRect.Right + 2;
            downButtonRect.Y = 1;
            downButtonRect.Width = rect.Width / 2 - 4;
            downButtonRect.Height -= 2;

            if (Enabled)
            {
                if (e.MouseOver)
                {
                    if (e.MousePress)//按下键时
                    {
                        if (e.MouseInUpButton)
                        {
                            upButtonBaseColor = GetColor(_baseColor, 0, -35, -24, -9);
                        }
                        else
                        {
                            downButtonBaseColor = GetColor(_baseColor, 0, -35, -24, -9);
                        }
                    }
                    else//仅仅是鼠标over未按键时
                    {
                        if (e.MouseInUpButton)
                        {
                            upButtonBaseColor = GetColor(_baseColor, 0, 35, 24, 9);
                        }
                        else
                        {
                            downButtonBaseColor = GetColor(_baseColor, 0, 35, 24, 9);
                        }
                    }
                }
            }
            else
            {
                upButtonBaseColor = SystemColors.Control;
                upButtonBorderColor = SystemColors.ControlDark;
                upButtonArrowColor = SystemColors.ControlDark;

                downButtonBaseColor = SystemColors.Control;
                downButtonBorderColor = SystemColors.ControlDark;
                downButtonArrowColor = SystemColors.ControlDark;
            }

            g.SmoothingMode = SmoothingMode.AntiAlias;
            gb.SmoothingMode = SmoothingMode.AntiAlias;
            Color backColor = Enabled ? _backColor : SystemColors.Control;

            using (SolidBrush brush = new SolidBrush(_backColor))
            {
                rect.Inflate(1, 1);
                gb.FillRectangle(brush, bmpRect);
            }

            RenderButton(
                gb,
                upButtonRect,
                upButtonBaseColor,
                upButtonBorderColor,
                upButtonArrowColor,
                ArrowDirection.Left);
            RenderButton(
                gb,
                downButtonRect,
                downButtonBaseColor,
                downButtonBorderColor,
                downButtonArrowColor,
                ArrowDirection.Right);
            g.DrawImage(bmp, rect.X, rect.Y);
            bmp.Dispose();
            gb.Dispose();
            UpDownButtonPaintEventHandler handler =
                base.Events[EventPaintUpDownButton] as UpDownButtonPaintEventHandler;
            if (handler != null)
            {
                handler(this, e);
            }
        }
        /// <summary>
        /// 左右箭头框体绘制
        /// </summary>
        /// <param name="g"></param>
        /// <param name="rect"></param>
        /// <param name="baseColor"></param>
        /// <param name="borderColor"></param>
        /// <param name="arrowColor"></param>
        /// <param name="direction"></param>
        internal void RenderButton(
            Graphics g,
            Rectangle rect,
            Color baseColor,
            Color borderColor,
            Color arrowColor,
            ArrowDirection direction)
        {
            RenderBackgroundInternal(
                g,
                rect,
                baseColor,
                borderColor,
                0.45f,
                true,
                LinearGradientMode.Vertical);
            using (SolidBrush brush = new SolidBrush(arrowColor))
            {
                RenderArrowInternal(
                    g,
                    rect,
                    direction,
                    brush);
            }
        }
        /// <summary>
        /// 左右箭头绘制
        /// </summary>
        /// <param name="g"></param>
        /// <param name="dropDownRect"></param>
        /// <param name="direction"></param>
        /// <param name="brush"></param>
        internal void RenderArrowInternal(
             Graphics g,
             Rectangle dropDownRect,
             ArrowDirection direction,
             Brush brush)
        {
            Point point = new Point(
                dropDownRect.Left + (dropDownRect.Width / 2),
                dropDownRect.Top + (dropDownRect.Height / 2));
            Point[] points = null;
            switch (direction)
            {
                case ArrowDirection.Left:
                    points = new Point[] { 
                        new Point(point.X + 1, point.Y - 4), 
                        new Point(point.X + 1, point.Y + 4), 
                        new Point(point.X - 2, point.Y) };
                    break;

                case ArrowDirection.Up:
                    points = new Point[] { 
                        new Point(point.X - 3, point.Y + 1), 
                        new Point(point.X + 3, point.Y + 1), 
                        new Point(point.X, point.Y - 1) };
                    break;

                case ArrowDirection.Right:
                    points = new Point[] {
                        new Point(point.X - 1, point.Y - 4), 
                        new Point(point.X - 1, point.Y + 4), 
                        new Point(point.X + 2, point.Y) };
                    break;

                default:
                    points = new Point[] {
                        new Point(point.X - 3, point.Y - 1), 
                        new Point(point.X + 3, point.Y - 1), 
                        new Point(point.X, point.Y + 1) };
                    break;
            }
            g.FillPolygon(brush, points);
        }

        /// <summary>
        /// 绘制左右箭头背景
        /// </summary>
        /// <param name="g"></param>
        /// <param name="rect"></param>
        /// <param name="baseColor"></param>
        /// <param name="borderColor"></param>
        /// <param name="basePosition"></param>
        /// <param name="drawBorder"></param>
        /// <param name="mode"></param>
        internal void RenderBackgroundInternal(
          Graphics g,
          Rectangle rect,
          Color baseColor,
          Color borderColor,
          float basePosition,
          bool drawBorder,
          LinearGradientMode mode)
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
                g.FillRectangle(brush, rect);
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

            if (drawBorder)
            {
                using (Pen pen = new Pen(borderColor))
                {
                    g.DrawRectangle(pen, rect);
                }
            }
        }

        #endregion

          
        #region  自定义窗口类，将左右箭头句柄分配到此类窗口以进行绘图操作
        /// <summary>
        /// 得到左右箭头的句柄
        /// </summary>
        internal IntPtr UpDownButtonHandle
        {
            get { return FindUpDownButton(); }
        }
        private IntPtr FindUpDownButton()//多重嵌套时句柄有问题
        {
            return FindWindowEx(
                this.Handle,
                IntPtr.Zero,
                "msctls_updown32",
                null);
        }
        private class UpDownButtonNativeWindow : NativeWindow, IDisposable
        {
            private TabControlEx _owner;
            private bool _bPainting;

            public UpDownButtonNativeWindow(TabControlEx owner)
                : base()
            {
                _owner = owner;
                base.AssignHandle(_owner.UpDownButtonHandle);
            }
            private bool LeftKeyPressed()
            {
                if (SystemInformation.MouseButtonsSwapped)
                {
                    return (GetKeyState(VK_RBUTTON) < 0);
                }
                else
                {
                    return (GetKeyState(VK_LBUTTON) < 0);
                }
            }

            private void DrawUpDownButton()
            {
                bool mouseOver = false;
                bool mousePress = LeftKeyPressed();
                bool mouseInUpButton = false;

                RECT rect = new RECT();

                GetClientRect(base.Handle, ref rect);
                
                Rectangle clipRect = Rectangle.FromLTRB(
                    rect.Top, rect.Left, rect.Right, rect.Bottom);

                Point cursorPoint = new Point();
                GetCursorPos(ref cursorPoint);
                GetWindowRect(base.Handle, ref rect);

                mouseOver = PtInRect(ref rect, cursorPoint);

                cursorPoint.X -= rect.Left;
                cursorPoint.Y -= rect.Top;

                mouseInUpButton = cursorPoint.X < clipRect.Width / 2;

                using (Graphics g = Graphics.FromHwnd(base.Handle))
                {
                    UpDownButtonPaintEventArgs e =
                        new UpDownButtonPaintEventArgs(
                        g,
                        clipRect,
                        mouseOver,
                        mousePress,
                        mouseInUpButton);
                    _owner.OnPaintUpDownButton(e);                    
                }
            }

            protected override void WndProc(ref Message m)
            {
                switch (m.Msg)
                {
                    case  WM_PAINT:
                        if (!_bPainting)
                        {
                             PAINTSTRUCT ps =
                                new  PAINTSTRUCT();
                            _bPainting = true;
                            BeginPaint(m.HWnd, ref ps);
                            DrawUpDownButton();
                            EndPaint(m.HWnd, ref ps);
                            _bPainting = false;
                            m.Result = TRUE;
                        }
                        else
                        {
                            base.WndProc(ref m);
                        }
                        break;
                    default:
                        base.WndProc(ref m);
                        break;
                }
            }
          
            public void Dispose()
            {
                _owner = null;
                base.ReleaseHandle();
            }
        }



        #endregion

     }
    /// <summary>
    /// 自定义左右箭头绘制的事件委托
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
     public delegate void UpDownButtonPaintEventHandler(
       object sender,
       UpDownButtonPaintEventArgs e);
     /// <summary>
     /// 继承PaintEventArgs的参数类，增加鼠标状态传递，over，press，updown等状态
     /// </summary>
     public class UpDownButtonPaintEventArgs : PaintEventArgs
     {
         private bool _mouseOver;
         private bool _mousePress;
         private bool _mouseInUpButton;

         public UpDownButtonPaintEventArgs(
             Graphics graphics,
             Rectangle clipRect,
             bool mouseOver,
             bool mousePress,
             bool mouseInUpButton)
             : base(graphics, clipRect)
         {
             _mouseOver = mouseOver;
             _mousePress = mousePress;
             _mouseInUpButton = mouseInUpButton;
         }

         public bool MouseOver
         {
             get { return _mouseOver; }
         }

         public bool MousePress
         {
             get { return _mousePress; }
         }

         public bool MouseInUpButton
         {
             get { return _mouseInUpButton; }
         }
     }
}
