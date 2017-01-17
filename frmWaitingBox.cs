using System;
using System.Drawing;
using System.Windows.Forms;

namespace YUNkefu
{
    public partial class frmWaitingBox : SkinMain
    {
        #region Properties
        private int _MaxWaitTime;
        private int _WaitTime;
        private bool _CancelEnable;
        private IAsyncResult _AsyncResult;
        private EventHandler<EventArgs> _Method;
        private bool _IsShown = true;
        private readonly int _EffectCount = 10;
        private readonly int _EffectTime = 500;
        /// <summary>
        /// 控制界面显示的特性
        /// </summary>
        private Timer _Timer;
        public string Message { get; private set; }
        public int TimeSpan { get; set; }
        public bool FormEffectEnable { get; set; }
        #endregion

        #region frmWaitingBox
        public frmWaitingBox(EventHandler<EventArgs> method, int maxWaitTime, string waitMessage, bool cancelEnable, bool timerVisable)
        {
            maxWaitTime *= 1000;
            Initialize(method, maxWaitTime, waitMessage, cancelEnable, timerVisable);
        }
        public frmWaitingBox(EventHandler<EventArgs> method)
        {
            int maxWaitTime = 60 * 1000;
            string waitMessage = "正在处理数据，请稍后...";
            bool cancelEnable = true;
            bool timerVisable = true;
            Initialize(method, maxWaitTime, waitMessage, cancelEnable, timerVisable);
        }
        public frmWaitingBox(EventHandler<EventArgs> method, string waitMessage)
        {
            int maxWaitTime = 60 * 1000;
            bool cancelEnable = true;
            bool timerVisable = true;
            Initialize(method, maxWaitTime, waitMessage, cancelEnable, timerVisable);
        }
        public frmWaitingBox(EventHandler<EventArgs> method, bool cancelEnable, bool timerVisable)
        {
            int maxWaitTime = 60 * 1000;
            string waitMessage = "正在处理数据，请稍后...";
            Initialize(method, maxWaitTime, waitMessage, cancelEnable, timerVisable);
        }
        #endregion

        #region Initialize
        private void Initialize(EventHandler<EventArgs> method, int maxWaitTime, string waitMessage, bool cancelEnable, bool timerVisable)
        {
            InitializeComponent();
            //initialize form
            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.CenterParent;
            this.ShowInTaskbar = false;
            Color[] c = GetRandColor();
            this.panel1.BackColor = c[0];
            this.BackColor = c[1];
            this.labMessage.Text = waitMessage;
            _Timer = new Timer();
            _Timer.Interval = _EffectTime / _EffectCount;
            _Timer.Tick += _Timer_Tick;
            this.Opacity = 0;
            FormEffectEnable = true;
            //para
            TimeSpan = 500;
            Message = string.Empty;
            _CancelEnable = cancelEnable;
            _MaxWaitTime = maxWaitTime;
            _WaitTime = 0;
            _Method = method;
            //this.pictureBoxCancel.Visible = _CancelEnable;
            this.labTimer.Visible = timerVisable;
            this.timer1.Interval = TimeSpan;
            this.timer1.Start();
        }
        #endregion

        #region Color
        private Color[] GetRandColor()
        {
            int rMax = 248;
            int rMin = 204;
            int gMax = 250;
            int gMin = 215;
            int bMax = 250;
            int bMin = 240;
            Random r = new Random(DateTime.Now.Millisecond);
            int r1 = r.Next(rMin, rMax);
            int r2 = r1 + 5;
            int g1 = r.Next(gMin, gMax);
            int g2 = g1 + 5;
            int b1 = r.Next(bMin, bMax);
            int b2 = b1 + 5;
            Color c1 = Color.FromArgb(r1, g1, b1);
            Color c2 = Color.FromArgb(r2, g2, b2);
            Color[] c = { c1, c2 };
            return c;
        }
        #endregion

        #region Events
        private void btnCancel_Click(object sender, EventArgs e)
        {
            Message = "您结束了当前操作！";
            Close();
        }
        private void frmWaitingBox_Shown(object sender, EventArgs e)
        {
            _AsyncResult = _Method.BeginInvoke(null, null, null, null);
            //Effect
            if (FormEffectEnable)
            {
                _Timer.Start();
            }
            else
                Opacity = 1;
        }
        private void frmWaitingBox_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (FormEffectEnable)
            {
                if (Opacity >= 1)
                    e.Cancel = true;
                _Timer.Start();
            }
        }
        private void _Timer_Tick(object sender, EventArgs e)
        {
            if (_IsShown)
            {
                if (Opacity >= 1)
                {
                    _Timer.Stop();
                    _IsShown = false;
                }
                Opacity += 1.00 / _EffectCount;
            }
            else
            {
                if (Opacity <= 0)
                {
                    _Timer.Stop();
                    _IsShown = true;
                    Close();
                }
                Opacity -= 1.00 / _EffectCount;
            }
        }

        #endregion

        private void timer1_Tick(object sender, System.EventArgs e)
        {
            _WaitTime += TimeSpan;
            this.labTimer.Text = string.Format("{0}秒", _WaitTime / 1000);
            if (!this._AsyncResult.IsCompleted)
            {
                if (_WaitTime > _MaxWaitTime)
                {
                    Message = string.Format("处理数据超时{0}秒，结束当前操作！", _MaxWaitTime / 1000);
                    this.Close();
                }
            }
            else
            {
                this.Message = string.Empty;
                this.Close();
            }
        }
    }
}