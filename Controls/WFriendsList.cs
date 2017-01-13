using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using YUNkefu.Core.Entity;

namespace YUNkefu.Controls
{
    /// <summary>
    /// 通讯录列表控件
    /// </summary>
    class WFriendsList : ListBox
    {
        private int _mouse_over = -1;
        private Timer timer1;
        private System.ComponentModel.IContainer components;
        public event FriendInfoViewEventHandler FriendInfoView;
        /// <summary>
        /// 
        /// </summary>
        public WFriendsList()
        {
            DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
            UpdateStyles();
            BorderStyle = System.Windows.Forms.BorderStyle.None;
            Cursor = Cursors.Hand;

            InitializeComponent();
            timer1.Start();

        }
        /// <summary>
        /// 重绘每项
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            Rectangle bound;
            for (int i = 0; i < Items.Count; ++i)
            {
                WXUser user = Items[i] as WXUser;
                bound = GetItemRectangle(i);

                if (user != null)  //好友项
                {
                    if (!ClientRectangle.IntersectsWith(bound))
                    {
                        continue;
                    }
                    if (_mouse_over == i)
                    {
                        e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(20, 248,248,255)), bound);
                    }
                    if (SelectedIndex == i)
                    {
                        e.Graphics.FillRectangle(Brushes.LightGray, bound);
                    }
                    e.Graphics.DrawLine(new Pen(Color.FromArgb(50, Color.Black)), new Point(bound.X, bound.Y + 50), new Point(bound.X + Width, bound.Y + 50));
                    using (Font f = new System.Drawing.Font("微软雅黑", 11))
                    {
                        if (user.Icon != null)
                        {
                            e.Graphics.DrawImage(user.Icon, new Rectangle(bound.X + 8, bound.Y + 8, 34, 34));  //头像
                        }
                        e.Graphics.DrawString(user.ShowName, f, Brushes.Black, new PointF(bound.X + 50, bound.Y + 16)); //昵称
                    }
                }
                else  //分类项
                {
                    if (!ClientRectangle.IntersectsWith(bound))
                    {
                        continue;
                    }

                    using (Font f = new Font("微软雅黑", 15))
                    {
                        e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(150, Color.Gray)), bound);
                        e.Graphics.DrawString(Items[i].ToString(), f, Brushes.Black, new PointF(bound.X + 10, bound.Y + 3));
                    }
                }
            }
        }
        /// <summary>
        /// 设置每项高度
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMeasureItem(MeasureItemEventArgs e)
        {
            base.OnMeasureItem(e);
            if (e.Index > 0)
            {
                if (Items[e.Index] as WXUser != null)  //好友项
                {
                    e.ItemHeight = 50;
                }
                else //分类项
                {
                    e.ItemHeight = 30;
                }
            }
        }
        /// <summary>
        /// 鼠标移动
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            for (int i = 0; i < Items.Count; ++i)
            {
                if (GetItemRectangle(i).Contains(e.Location))
                {
                    _mouse_over = i;
                    Invalidate();
                    return;
                }
            }
            _mouse_over = -1;
        }
        /// <summary>
        /// 双击选择 查看好友信息
        /// </summary>
        /// <param name="e"></param>
        protected override void OnDoubleClick(EventArgs e)
        {
            base.OnDoubleClick(e);
            Invalidate();
            if (FriendInfoView != null)
            {
                if (SelectedIndex != -1)
                {
                    FriendInfoView(Items[SelectedIndex] as WXUser);
                }
            }
        }
        /// <summary>
        /// 鼠标离开
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            _mouse_over = -1;
            Invalidate();
        }
        /// <summary>
        /// 选择项发生变化
        /// </summary>
        /// <param name="e"></param>
        protected override void OnSelectedIndexChanged(EventArgs e)
        {
            base.OnSelectedIndexChanged(e);
            Invalidate();
        }
        /// <summary>
        /// 子组件加载
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 200;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            this.ResumeLayout(false);

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            Invalidate();
        }
    }
    /// <summary>
    /// 表示处理浏览好友信息事件的方法
    /// </summary>
    /// <param name="user"></param>
    public delegate void FriendInfoViewEventHandler(WXUser user);
}
