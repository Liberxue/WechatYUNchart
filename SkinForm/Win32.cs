using System;
using System.Runtime.InteropServices;
using System.ComponentModel;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Windows.Forms;
using System.Collections;

/// <summary>
/// Wind32API声明
/// </summary>
public class Win32
{
    public const int GWL_EXSTYLE = -20;
    public const int WS_EX_TRANSPARENT = 0x00000020;
    public const int WS_EX_LAYERED = 0x00080000;

    [StructLayout(LayoutKind.Sequential)]
    public struct Size
    {
        public Int32 cx;
        public Int32 cy;

        public Size(Int32 x, Int32 y)
        {
            cx = x;
            cy = y;
        }
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct BLENDFUNCTION
    {
        public byte BlendOp;
        public byte BlendFlags;
        public byte SourceConstantAlpha;
        public byte AlphaFormat;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct Point
    {
        public Int32 x;
        public Int32 y;

        public Point(Int32 x, Int32 y)
        {
            this.x = x;
            this.y = y;
        }
    }

    public const byte AC_SRC_OVER = 0;
    public const Int32 ULW_ALPHA = 2;
    public const byte AC_SRC_ALPHA = 1;

    /// <summary>
    /// 从左到右显示
    /// </summary>
    public const Int32 AW_HOR_POSITIVE = 0x00000001;
    /// <summary>
    /// 从右到左显示
    /// </summary>
    public const Int32 AW_HOR_NEGATIVE = 0x00000002;
    /// <summary>
    /// 从上到下显示
    /// </summary>
    public const Int32 AW_VER_POSITIVE = 0x00000004;
    /// <summary>
    /// 从下到上显示
    /// </summary>
    public const Int32 AW_VER_NEGATIVE = 0x00000008;
    /// <summary>
    /// 若使用了AW_HIDE标志，则使窗口向内重叠，即收缩窗口；否则使窗口向外扩展，即展开窗口
    /// </summary>
    public const Int32 AW_CENTER = 0x00000010;
    /// <summary>
    /// 隐藏窗口，缺省则显示窗口
    /// </summary>
    public const Int32 AW_HIDE = 0x00010000;
    /// <summary>
    /// 激活窗口。在使用了AW_HIDE标志后不能使用这个标志
    /// </summary>
    public const Int32 AW_ACTIVATE = 0x00020000;
    /// <summary>
    /// 使用滑动类型。缺省则为滚动动画类型。当使用AW_CENTER标志时，这个标志就被忽略
    /// </summary>
    public const Int32 AW_SLIDE = 0x00040000;
    /// <summary>
    /// 透明度从高到低
    /// </summary>
    public const Int32 AW_BLEND = 0x00080000;

    /// <summary>
    /// 执行动画
    /// </summary>
    /// <param name="whnd">控件句柄</param>
    /// <param name="dwtime">动画时间</param>
    /// <param name="dwflag">动画组合名称</param>
    /// <returns>bool值，动画是否成功</returns>
    [DllImport("user32")]
    public static extern bool AnimateWindow(IntPtr whnd, int dwtime, int dwflag);

    /// <summary>
    /// <para>该函数将指定的消息发送到一个或多个窗口。</para>
    /// <para>此函数为指定的窗口调用窗口程序直到窗口程序处理完消息再返回。</para>
    /// <para>而函数PostMessage不同，将一个消息寄送到一个线程的消息队列后立即返回。</para>
    /// return 返回值 : 指定消息处理的结果，依赖于所发送的消息。
    /// </summary>
    /// <param name="hWnd">要接收消息的那个窗口的句柄</param>
    /// <param name="Msg">消息的标识符</param>
    /// <param name="wParam">具体取决于消息</param>
    /// <param name="lParam">具体取决于消息</param>
    [DllImport("User32.dll", CharSet = CharSet.Auto, EntryPoint = "SendMessageA")]
    public static extern IntPtr SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

    [DllImport("user32.dll")]
    public static extern bool ReleaseCapture();

    [DllImport("user32")]
    public static extern void mouse_event(int dwFlags, int dx, int dy, int dwData, int dwExtraInfo); 

    [DllImport("gdi32.dll")]
    public static extern int CreateRoundRectRgn(int x1, int y1, int x2, int y2, int x3, int y3);

    [DllImport("user32.dll")]
    public static extern int SetWindowRgn(IntPtr hwnd, int hRgn, Boolean bRedraw);

    [DllImport("user32", EntryPoint = "GetWindowLong")]
    public static extern int GetWindowLong(
        IntPtr hwnd, int nIndex);

    [DllImport("user32.dll")]
    public static extern int SetWindowLong(
        IntPtr hwnd, int nIndex, int dwNewLong);

    [DllImport("gdi32.dll", ExactSpelling = true, SetLastError = true)]
    public static extern IntPtr CreateCompatibleDC(IntPtr hDC);

    [DllImport("user32.dll", ExactSpelling = true, SetLastError = true)]
    public static extern IntPtr GetDC(IntPtr hWnd);

    [DllImport("gdi32.dll", ExactSpelling = true)]
    public static extern IntPtr SelectObject(IntPtr hDC, IntPtr hObj);

    [DllImport("user32.dll", ExactSpelling = true)]
    public static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);

    [DllImport("gdi32.dll", ExactSpelling = true, SetLastError = true)]
    public static extern int DeleteDC(IntPtr hDC);

    [DllImport("gdi32.dll", ExactSpelling = true, SetLastError = true)]
    public static extern int DeleteObject(IntPtr hObj);

    [DllImport("user32.dll", ExactSpelling = true, SetLastError = true)]
    public static extern int UpdateLayeredWindow(IntPtr hwnd, IntPtr hdcDst, ref Point pptDst, ref Size psize, IntPtr hdcSrc, ref Point pptSrc, Int32 crKey, ref BLENDFUNCTION pblend, Int32 dwFlags);

    [DllImport("gdi32.dll", ExactSpelling = true, SetLastError = true)]
    public static extern IntPtr ExtCreateRegion(IntPtr lpXform, uint nCount, IntPtr rgnData);
}