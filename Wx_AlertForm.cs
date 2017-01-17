using System;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;
using System.Media;
using YUNkefu;
namespace YUNkefu
{
    public partial class Wx_AlertForm : SkinMain
    {
        private bool _start = true;
        private int _showYPoint = 0;
        private int _showXPoint = 0;
        private ShowWay _showWay = ShowWay.UpDown;
        private int _showTime = 3000;//展示时间,单位毫秒
        private int _showInTime = 200;//出现时间,单位毫秒
        private int _showOutTime = 500;//消失时间,单位毫秒
        protected System.Windows.Forms.Timer _timer = new System.Windows.Forms.Timer();
        private int _timerRunCount = 0;
        private int _maxTimerRunCount = 0;

        /// <summary>
        /// 展示方式枚举
        /// </summary>
        public enum ShowWay
        {
            UpDown,//上升下降
            Fade //淡出淡入
        }

        public Wx_AlertForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 显示提示窗口
        /// </summary>
        /// <param name="paramsList">
        /// params[0]:内容
        /// params[1]:标题
        /// params[2]:显示方式
        /// params[3]:宽度
        /// params[4]:高度
        /// params[5]:出现时间
        /// params[6]:停留时间
        /// params[7]:消失时间
        /// </param>
        public void Show(params object[] paramsList)
        {
            if (paramsList == null || paramsList.Length == 0) return;
            else
            {
                string afContent = paramsList[0] != null ? paramsList[0].ToString() : "";
                string aftitle = (paramsList.Length > 1 && paramsList[1] != null) ? paramsList[1].ToString().Length < 20 ?
                    paramsList[1].ToString() : paramsList[1].ToString().Substring(0, 20) + "..." : "";
                ShowWay afShowWay = (paramsList.Length > 2 && paramsList[2] != null && paramsList[2] is ShowWay) ? (ShowWay)paramsList[2] : ShowWay.UpDown;
                int afWidth = 0, afHeigth = 0;
                int afshowInTime = 200, afshowTime = 3000, afshowOutTime = 500;
                if (paramsList.Length > 3 && paramsList[3] != null) int.TryParse(paramsList[3].ToString(), out afWidth);
                else afWidth = this.Width;
                if (paramsList.Length > 4 && paramsList[4] != null) int.TryParse(paramsList[4].ToString(), out afHeigth);
                else afHeigth = this.Height;
                if (paramsList.Length > 5 && paramsList[5] != null) int.TryParse(paramsList[5].ToString(), out afshowInTime);
                else afshowInTime = _showInTime;
                if (paramsList.Length > 6 && paramsList[6] != null) int.TryParse(paramsList[6].ToString(), out afshowTime);
                else afshowTime = _showTime;
                if (paramsList.Length > 7 && paramsList[7] != null) int.TryParse(paramsList[7].ToString(), out afshowOutTime);
                else afshowTime = _showOutTime;

                afWidth = afWidth > 0 ? afWidth : this.Width;
                afHeigth = afHeigth > 0 ? afHeigth : this.Height;
                afshowInTime = afshowInTime > 0 ? afshowInTime : _showInTime;
                afshowTime = afshowTime > 0 ? afshowTime : _showTime;
                afshowOutTime = afshowOutTime > 0 ? afshowOutTime : _showOutTime;
                this.Show(afContent, aftitle, afShowWay, afWidth, afHeigth, afshowInTime, afshowTime, afshowOutTime);
            }
        }

        protected void Show(string content, string title, ShowWay showWay, int width, int height, int showInTime, int showTime, int showOutTime)
        {
            _timerRunCount = 0;
            this.ShowInTaskbar = false;
            this.StartPosition = FormStartPosition.Manual;
           // if (content != null) labContent.Text = content.ToString();//提示的内容
            if (content != null)
            {
                labContent.Text = content.Length < 30 ? content : content.Substring(0, 30) + "...";
            }
            //标题
            if (title != null)
            {
                labtitle.Text = title.Length < 20 ? title : title.Substring(0, 20) + "...";
            }

            this.Width = width;
            this.Height = height;
            _showInTime = showInTime;
            _showTime = showTime;
            _showOutTime = showOutTime;
            _showWay = showWay;

            _showYPoint = Screen.PrimaryScreen.WorkingArea.Height - this.Height;
            _showXPoint = Screen.PrimaryScreen.WorkingArea.Width - this.Width;
            int nowYPoint = Screen.PrimaryScreen.WorkingArea.Height;

            int sleepTime = _showInTime / 10;//每次变化的时间
            switch (showWay)
            {
                case ShowWay.Fade:
                    {
                        #region 窗口淡入
                        this.Location = new Point(_showXPoint, _showYPoint);
                        this.Opacity = 0;
                        base.Show();
                        double changeOpcation = 0;
                        while (_start)
                        {
                            changeOpcation += 0.1;
                            if (changeOpcation >= 1)
                            {
                                changeOpcation = 1;
                                _start = false;
                            }
                            this.Opacity = changeOpcation;
                            this.Refresh();
                            Thread.Sleep(sleepTime);
                        }
                        #endregion
                        break;
                    }
                case ShowWay.UpDown:
                default:
                    {
                        #region 窗口上升
                        this.Location = new Point(_showXPoint, nowYPoint);
                        base.Show();
                        int reduceHeight = this.Height / 10;//每次移动的高度
                        while (_start)
                        {
                            nowYPoint -= reduceHeight;
                            if (nowYPoint <= _showYPoint)
                            {
                                nowYPoint = _showYPoint;
                                _start = false;
                            }
                            this.Location = new Point(_showXPoint, nowYPoint);
                            this.Refresh();
                            Thread.Sleep(sleepTime);
                        }
                        #endregion
                        break;
                    }
            }
            if (content != null) labContent.Text = content.ToString();//提示的内容
            //标题
            if (title != null)
            {
                this.Text = title.Length < 20 ? title : title.Substring(0, 20) + "...";
            }

            _timer.Enabled = true;
            _timer.Interval = 100;
            _timer.Tick += new EventHandler(TimerTick);
            _maxTimerRunCount = _showTime / _timer.Interval;
            _timer.Start();
        }

        protected void TimerTick(Object obj, EventArgs ea)
        {
            _timerRunCount++;
            if (_timerRunCount >= _maxTimerRunCount && !this.IsDisposed)
            {
                _timer.Stop();
                _timerRunCount = 0;
                _showYPoint = Screen.PrimaryScreen.WorkingArea.Height;
                _showXPoint = Screen.PrimaryScreen.WorkingArea.Width - this.Width;
                int nowYPoint = Screen.PrimaryScreen.WorkingArea.Height - this.Height;
                int sleepTime = _showOutTime / 10;//每次变化的时间
                _start = true;
                switch (_showWay)
                {
                    case ShowWay.Fade:
                        {
                            #region 窗口淡出
                            double changeOpcation = 1;
                            while (_start)
                            {
                                changeOpcation -= 0.1;
                                if (changeOpcation <= 0)
                                {
                                    changeOpcation = 0;
                                    _start = false;
                                }
                                this.Opacity = changeOpcation;
                                this.Refresh();
                                Thread.Sleep(sleepTime);
                            }
                            #endregion
                            break;
                        }
                    case ShowWay.UpDown:
                    default:
                        {
                            #region 窗口下降
                            int reduceHeight = this.Height / 10;//每次移动的高度
                            while (_start)
                            {
                                nowYPoint += reduceHeight;
                                if (nowYPoint >= _showYPoint)
                                {
                                    nowYPoint = _showYPoint;
                                    _start = false;
                                }
                                this.Location = new Point(_showXPoint, nowYPoint);
                                this.Refresh();
                                Thread.Sleep(sleepTime);
                            }
                            #endregion
                            break;
                        }
                }
                this.Dispose();
            }
        }

        private void AlertForm_Load(object sender, EventArgs e)
        {
            SoundPlayer player = new SoundPlayer(@"msg.wav");
            player.Play();
        }

        private void picClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// 关闭动画
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void picClose_MouseEnter(object sender, EventArgs e)
        {
            picClose.Image = YUNkefu.Properties.Resources.close_hover;
        }

        private void picClose_MouseDown(object sender, MouseEventArgs e)
        {
            picClose.Image = YUNkefu.Properties.Resources.close;
            picClose.Image = YUNkefu.Properties.Resources.close_hover;
        }
        private void picClose_MouseLeave(object sender, EventArgs e)
        {
            picClose.Image = YUNkefu.Properties.Resources.close;
        }
        private void picClose_MouseHover(object sender, EventArgs e)
        {
            picClose.Image = YUNkefu.Properties.Resources.close_hover;
        }
    }
}
