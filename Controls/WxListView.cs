using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;

using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace YUNkefu.Controls
{
    public partial class WxListView : ListView
    {
        public WxListView()
        {
            InitializeComponent();
            this.View = View.Details;
            this.OwnerDraw = true;
            this.FullRowSelect = true;
        }

        protected override void OnDrawColumnHeader(DrawListViewColumnHeaderEventArgs e)
        {
            //重新绘制
            int tColumnCount;
            Rectangle tRect = new Rectangle();
            Point tPoint = new Point();

            Font tFont = new Font("宋体", 9, FontStyle.Regular);
            SolidBrush tBackBrush = new SolidBrush(System.Drawing.Color.FromArgb(((int)(((byte)(56)))), ((int)(((byte)(56)))), ((int)(((byte)(56))))));
            SolidBrush tFtontBrush;

            tFtontBrush = new SolidBrush(System.Drawing.SystemColors.GradientActiveCaption);

            if (this.Columns.Count == 0)
                return;
            tColumnCount = this.Columns.Count;
            tRect.Y = 0;
            tRect.Height = e.Bounds.Height - 1;
            tPoint.Y = 3;
            for (int i = 0; i < tColumnCount; i++)
            {
                if (i == 0)
                {
                    tRect.X = 0;
                    tRect.Width = this.Columns[i].Width;
                }
                else
                {
                    tRect.X += tRect.Width;
                    tRect.X += 1;
                    tRect.Width = this.Columns[i].Width - 1;
                }

                e.Graphics.FillRectangle(tBackBrush, tRect);
                tPoint.X = tRect.X + 3;
                e.Graphics.DrawString(this.Columns[i].Text, tFont, tFtontBrush, tPoint);
                base.OnDrawColumnHeader(e);
            }
        }
        /// <summary>
        /// 设置需要重绘SubItem index
        /// </summary>
        public int NeedDrawItemIndex { get; set; }
        protected override void OnDrawItem(DrawListViewItemEventArgs e)
        {
            base.OnDrawItem(e);
            if (View != View.Details)
            {
                e.DrawDefault = true;
            }
        }

        protected override void OnDrawSubItem(DrawListViewSubItemEventArgs e)
        {
            if (e.Header.Index != NeedDrawItemIndex)
            {
                e.DrawDefault = true;
                base.OnDrawSubItem(e);
            }
            else
            {
                e.Graphics.DrawImage(Properties.Resources.im,new Point(10,10));
            }

        }

        

        #region 更新表头
        public void InitHead(string[] heards,int[] widths)
        {
            var i = 0;
            foreach (var s in heards)
            {
                this.Columns.Add(s, widths[i], HorizontalAlignment.Center); //一步添加
                i++;
            }
        }
        #endregion

    }
}
