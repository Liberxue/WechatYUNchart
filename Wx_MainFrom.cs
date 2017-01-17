using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using YUNkefu.Core;
using YUNkefu.Http;
using Newtonsoft.Json.Linq;
using YUNkefu.Core.Entity;
using System.Threading;
using YUNkefu.Core.ReplyStrategy;
using YUNkefu.Core.Dal;
using System.Diagnostics;

namespace YUNkefu
{
    public partial class Wx_MainFrom : SkinMain
    {
        //YUNkefu.Wx_AlertForm af = null;
        #region  变量区
        private Dictionary<string, object> _dic = new Dictionary<string, object>();
        private ImageList iconList = new ImageList();
        private List<string> Uins = new List<string>();

        #endregion

        public Wx_MainFrom()
        {
            InitializeComponent();
            cbShowWay.SelectedIndex = 0;
            Wxuserregion();
        }

        private void MainFrom_Load(object sender, EventArgs e)
        {
            Text = Constant.SoftName + Constant.Version + "      初始化中...";
            string[] heads = { "微信唯一ID", "微信用户", "微信", "上线时间", "总群数", "到期时间" };
            int[] widths = { 140, 140, 140, 130, 140, 100 };
            WechartListView.InitHead(heads, widths);
            WechartListView.Columns[0].TextAlign = HorizontalAlignment.Center;
            Uins = LoginCore.GetOnLineUin();
            foreach (var uin in Uins)
            {
                LoginCore.InitCookie(uin);
                AddToList(uin);
                Thread.Sleep(10);
            }
            this.WechartListView.SelectedIndexChanged += new EventHandler(shisan13ListView1_SelectedIndexChanged);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Wx_LoginForm frm = new Wx_LoginForm();
            frm.ShowInTaskbar = false;
            frm.Owner = this;
            frm.ShowDialog();
        }

        private void UpDataFromData()
        {
            this.BeginInvoke((Action)delegate()
            {
                this.labOnLineCount.Text = Uins.Count.ToString();
                this.Text = Constant.SoftName + Constant.Version;
            });
        }

        public void AddToList(string uin)
        {
            ((Action)(delegate()
            {

                string sid = LoginCore.GetPassTicket(uin).WxSid;
                string WXUser_url = LoginCore.GetPassTicket(uin).WXUser_url;//传值不同域名不同的
                WXService wx = new WXService();
                wx.Uin = uin;
                wx.Sid = sid;
                JObject init_result = wx.WxInit();
                var partUsers = new List<WXUser>();
                foreach (JObject contact in init_result["ContactList"])  //部分好友名单
                {
                    WXUser user = new WXUser();
                    //传值uin sin
                    user.uin = uin;
                    user.UserName = contact["UserName"].ToString();
                    user.City = contact["City"].ToString();
                    user.HeadImgUrl = contact["HeadImgUrl"].ToString();
                    user.NickName = contact["NickName"].ToString();
                    user.Province = contact["Province"].ToString();
                    user.PYQuanPin = contact["PYQuanPin"].ToString();
                    user.RemarkName = contact["RemarkName"].ToString();
                    user.RemarkPYQuanPin = contact["RemarkPYQuanPin"].ToString();
                    user.Sex = contact["Sex"].ToString();
                    user.Signature = contact["Signature"].ToString();
                    partUsers.Add(user);
                }

                var _me = new WXUser();
                if (init_result != null)
                {
                    _me.UserName = init_result["User"]["UserName"].ToString();
                    _me.City = "";
                    _me.HeadImgUrl = init_result["User"]["HeadImgUrl"].ToString();
                    _me.NickName = init_result["User"]["NickName"].ToString();
                    _me.Province = "";
                    _me.PYQuanPin = init_result["User"]["PYQuanPin"].ToString();
                    _me.RemarkName = init_result["User"]["RemarkName"].ToString();
                    _me.RemarkPYQuanPin = init_result["User"]["RemarkPYQuanPin"].ToString();
                    _me.Sex = init_result["User"]["Sex"].ToString();
                    _me.Signature = init_result["User"]["Signature"].ToString();
                    Tools.WriteLog("【警告】" + _me.NickName + "不能在此软件运行,请切换版本或重新扫描登陆");

                    if (string.IsNullOrEmpty(_me.NickName))
                    {
                        WriteLog("【警告】" +_me.NickName + "不能在此软件运行,请切换版本或重新扫描登陆");
                        Tools.WriteLog("【警告】" + uin + "不能在此软件运行,请切换版本或重新扫描登陆");
                        return;
                    }

                    var _syncKey = new Dictionary<string, string>();

                    foreach (JObject synckey in init_result["SyncKey"]["List"])  //同步键值
                    {
                        _syncKey.Add(synckey["Key"].ToString(), synckey["Val"].ToString());
                    }
                    //保存最新key
                    LoginCore.AddSyncKey(uin, _syncKey);
                    //更新数据库
                    var table = WeChatRobotDal.GetWxRobot(_me.NickName);
                    if (table.Rows.Count == 0)
                    {
                        WriteLog("【警告】" + _me.NickName + "没有加入系统中");
                        return;
                    }
                    partUsers.Add(_me);
                    WxContact _contact = new WxContact(uin);  //记住此处不适合再开线程
                    _contact.InitContact(partUsers); //初始联系人

                    WeChatRobotDal.UpdateUin(_me.NickName, uin);

                    #region 加入listview
                    if (!_dic.ContainsKey(uin))
                    {
                        this.BeginInvoke((Action)delegate()
                        {
                            this.WechartListView.BeginUpdate();
                            //把扫描好的微信加入到列表中
                            ListViewItem item = new ListViewItem();
                            item.Text = uin;
                            item.SubItems.Add(_me.UserName);
                            item.SubItems.Add(_me.ShowName);
                            item.SubItems.Add(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                            item.SubItems.Add("0");
                            item.SubItems.Add(table.Rows[0]["EndDate"].ToString());
                            this.WechartListView.Items.Add(item);
                            this.WechartListView.EndUpdate();
                            //设置下高度
                            ImageList image = new ImageList();
                            image.ImageSize = new Size(10, 50);//这边设置宽和高
                            this.WechartListView.SmallImageList = image;
                            //隐藏以下按钮
                            //this.WechartListView.Columns[1].Width = 0;
                            //this.WechartListView.Columns[0].Width = 0;
                            //this.WechartListView.Columns[4].Width = 0;
                            //this.WechartListView.Columns[5].Width = 0;
                        });
                        _dic.Add(uin, uin);
                        //#region 开始任务
                        //var robotID = table.Rows[0]["RobotId"].ToString();
                        //WxTaskCore wt = new WxTaskCore(sid, uin, robotID);
                        //wt.user = _me;
                        ////接收消息事件
                        //wt.OnRevice += new WxTaskCore.Revice(wt_OnRevice);
                        ////接收修改联系人消息
                        //wt.OnModifyContact += new WxTaskCore.ModifyContact(wt_OnModifyContact);
                        ////通知发送信息
                        //wt.OnNotifySend += new WxTaskCore.NotifySend(wt_OnNotifySend);
                        ////每一个微信号，开启一个线程
                        //ThreadStart start = new ThreadStart(wt.ReviceMsg);
                        //new Thread(start).Start();
                        ////启动发送线程
                        //new Thread(new ThreadStart(wt.AutoSendMsg)).Start();
                        //#endregion
                    }
                    #endregion
                }
                if (!Uins.Contains(uin))
                    Uins.Add(uin);
                LoginCore.AddOnLineUin(Uins);
                UpDataFromData();
            })).BeginInvoke(null, null);
        }



        #region 事件

        /// <summary>
        /// 联系人修改
        /// </summary>
        /// <param name="users"></param>
        /// <param name="sid"></param>
        /// <param name="uid"></param>
        void wt_OnModifyContact(List<WXUser> users, string sid, string uid)
        {

        }

        void wt_OnNotifySend(NotifyArgs args)
        {
            ((Action)(delegate()
            {
                if (!string.IsNullOrEmpty(args.MsgContext))
                {
                    WXService s = new WXService();
                    s.Sid = args.Sid;
                    s.Uin = args.WxUin;
                    foreach (var u in args.GroupUserName)
                    {
                      //s.SendMsg(args.MsgContext, args.MyUserName, u, 1);
                        WriteLog("【发送】" + args.MsgContext);
                    }
                }
            })).BeginInvoke(null, null);
        }

        /// <summary>
        /// 接收消息
        /// </summary>
        /// <param name="msg"></param>
        void wt_OnRevice(WXMsg msg)
        {
            try
            {
                WxContact c = new WxContact(msg.Uin);
                string log = "type:" + msg.Type.ToString() + "来源：" + msg.From + "[ " + c.GetNickName(msg.From) + "],发至:" + msg.To + " [" + c.GetNickName(msg.To) + "]" + msg.Msg;
                WriteLog(log);
                var m = ReplyFactory.Create(msg).MakeContent(msg);
                var sendContext = m.context;
                if (!string.IsNullOrEmpty(sendContext))
                {
                    WXService s = new WXService();
                    s.Sid = msg.Sid;
                    s.Uin = msg.Uin;
                    s.SendMsg(sendContext, msg.To, msg.From, m.type, msg.Uin, msg.Sid);
                }
                //string afTitle = "[ " + c.GetNickName(msg.From) + "]回复[" + c.GetNickName(msg.To) + "]" + "一条消息";
                //string afContent = msg.Msg;
                //YUNkefu.AlertForm.ShowWay showWay = YUNkefu.AlertForm.ShowWay.UpDown;
                //int afShowInTime, afShowTime, afShowOutTime;
                //int afWidth, afHeigth;
                //int.TryParse("100", out afShowInTime);
                //int.TryParse("500", out afShowTime);
                //int.TryParse("100", out afShowOutTime);
                //int.TryParse("250", out afWidth);
                //int.TryParse("120", out afHeigth);
                //af = new YUNkefu.AlertForm();
                //af.Show(afContent, afTitle, showWay, afWidth, afHeigth, afShowInTime, afShowTime, afShowOutTime);
                WriteLog("【demo】" + log);
            }

            catch (Exception ex)
            {
                WriteLog("【错误】" + ex.ToString());
                Tools.WriteLog(ex.ToString());
            }
        }

        void shisan13ListView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        void shisan13ListView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            TabPage tab = new TabPage();
            tab.Name = WechartListView.SelectedItems[0].SubItems[0].Text;
            tab.Text = WechartListView.SelectedItems[0].SubItems[2].Text+"--云客服";//获取登录列表为微信
            Wx_wechart form = new Wx_wechart();
            form.Uin = WechartListView.SelectedItems[0].SubItems[0].Text;
            form.Sid = LoginCore.GetPassTicket(WechartListView.SelectedItems[0].SubItems[0].Text).WxSid;
            form.robotID = "013";
            form.TopLevel = false;      //设置为非顶级控件
            tab.Controls.Add(form);
            WXMaintab.Controls.Add(tab);
            form.Show();
        }

        private void WriteLog(string log)
        {
            this.rtLog.BeginInvoke((Action)delegate()
            {
                if (rtLog.Text.Length > 20000)
                    this.rtLog.Text = "";
                var old = this.rtLog.Text;
                this.rtLog.Text = string.Format("[{0}]{1}", DateTime.Now.ToString("MM-dd HH:mm:ss"), log + "\r\n" + old);
            });
        }

        #endregion

        private void button1_Click(object sender, EventArgs e)
        {
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void picClose_Click(object sender, EventArgs e)
        {
            this.Close();
            Process.GetCurrentProcess().Kill();//完全关闭系统。
        }
        /// <summary>
        /// 关闭动画
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void picClose_MouseEnter(object sender, EventArgs e)
        {
            picClose.Image = Properties.Resources.close_hover;
        }

        private void picClose_MouseDown(object sender, MouseEventArgs e)
        {
            picClose.Image = Properties.Resources.close;
            picClose.Location = new Point(picClose.Location.X + 1, picClose.Location.Y + 1);
        }
        private void picClose_MouseLeave(object sender, EventArgs e)
        {
            picClose.Image = Properties.Resources.close;
        }
        private void picClose_MouseHover(object sender, EventArgs e)
        {
            picClose.Image = YUNkefu.Properties.Resources.close_hover;
        }

        /// <summary>
        ///最小化动画事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pic_Mxi_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
        private void pic_Mxi_MouseDown(object sender, MouseEventArgs e)
        {
            pic_Mxi.Image = Properties.Resources.mxi_hover;
            pic_Mxi.Location = new Point(pic_Mxi.Location.X + 1, picClose.Location.Y + 1);
        }

        private void pic_Mxi_MouseEnter(object sender, EventArgs e)
        {
            pic_Mxi.Image = Properties.Resources.mxi_hover;
        }

        private void pic_Mxi_MouseLeave(object sender, EventArgs e)
        {
            pic_Mxi.Image = Properties.Resources.mxi;
        }
        /// <summary>
        /// 增加微信触发动画事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_MouseDown(object sender, MouseEventArgs e)
        {
            btnAdd.Image = Properties.Resources.wechat_dw;
        }

        private void btnAdd_MouseEnter(object sender, EventArgs e)
        {
            btnAdd.Image = Properties.Resources.wechat_dw;
        }

        private void btnAdd_MouseLeave(object sender, EventArgs e)
        {
            btnAdd.Image = Properties.Resources.wechat_add;

        }

        private void btnAdd_MouseHover(object sender, EventArgs e)
        {
            btnAdd.Image = Properties.Resources.wechat_add_hove;
            btnAdd.Location = new Point(btnAdd.Location.X * 1, btnAdd.Location.Y * 1);
        }

        private void btnAdd_MouseMove(object sender, MouseEventArgs e)
        {
            btnAdd.Image = Properties.Resources.wechat_dw;
            btnAdd.Location = new Point(btnAdd.Location.X / 1, btnAdd.Location.Y / 1);
        }

        private void wchatlist_StartChat(WXUser user)
        {

        }

        private void wChatList1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void wfriendlist_FriendInfoView(WXUser user)
        {

        }

        private void wFriendsList1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void wpersonalinfo_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {
          
        }

        private void cbShowWay_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
        private void Wxuserregion()
        {
            Random random = new Random();
            this.chart1.Series[0].Name = "接收";
            this.chart1.Series[1].Name = "发送";
            this.chart2.Series[0].Name = "机器人回复";
            this.chart2.Series[1].Name = "定时回复";
            for (int i = 1; i < 12; i++)
            {
                this.chart1.Series[0].Points.AddXY(i, random.Next(100));
                this.chart1.Series[1].Points.AddXY(i, random.Next(100));
                this.chart2.Series[0].Points.AddXY(i, random.Next(100));
            }
            double[] yValues = { 65.62, 75.54, 60.45, 55.73, 70.42 };
            string[] xValues = { "France", "Canada", "UK", "USA", "Italy" };
            this.chart3.Series[0].CustomProperties = "DoughnutRadius=60, PieLabelStyle=Disabled, PieDrawingStyle=SoftEdge";
            this.chart3.Series[0].Label = "#PERCENT";
            this.chart3.Series[0]["PieLabelStyle"] = "Inside";
            this.chart3.Series[0].Points.DataBindXY(xValues, yValues);
            this.chart4.Series[0].CustomProperties = "DoughnutRadius=60, PieLabelStyle=Disabled, PieDrawingStyle=SoftEdge";
            this.chart4.Series[0].Label = "#PERCENT";
            this.chart4.Series[0]["PieLabelStyle"] = "Inside";
            this.chart4.Series[0].Points.DataBindXY(xValues, yValues);
        }

        private void BtImportreport_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "表格文件 (*.xls)|*.xls";
            openFileDialog.RestoreDirectory = true;
            openFileDialog.FilterIndex = 1;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                Import(openFileDialog.FileName);
            }
        }
        /// <summary>
        /// 导入excel数据
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static bool Import(string filePath)
        {
            //try
            //{
            //    //Excel就好比一个数据源一般使用
            //    //这里可以根据判断excel文件是03的还是07的，然后写相应的连接字符串
            //    string strConn = "Provider=Microsoft.Jet.OLEDB.4.0;" + "Data Source=" + filePath + ";" + "Extended Properties=Excel 8.0;";
            //    MYsqlConnection con = new OleDbConnection(strConn);
            //    con.Open();
            //    string[] names = GetExcelSheetNames(con);
            //    if (names.Length > 0)
            //    {
            //        foreach (string name in names)
            //        {
            //            OleDbCommand cmd = con.CreateCommand();
            //            cmd.CommandText = string.Format(" select * from [{0}]", name);//[sheetName]要如此格式
            //            OleDbDataReader odr = cmd.ExecuteReader();
            //            while (odr.Read())
            //            {
            //                if (odr[0].ToString() == "序号")//过滤列头  按你的实际Excel文件
            //                    continue;
            //                //数据库添加操作
            //                /*进行非法值的判断
            //                 * 添加数据到数据表中
            //                 * 添加数据时引用事物机制，避免部分数据提交
            //                 * Add(odr[1].ToString(), odr[2].ToString(), odr[3].ToString());//数据库添加操作，Add方法自己写的
            //                 * */

            //            }
            //            odr.Close();
            //        }
            //    }
            //    return true;
            //}
            //catch (Exception)
            //{
            //    return false;
            //}
            return false;
        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            Xmloperation xmlop = new Xmloperation(@"WxData.xml");
            Wx_alogin Frlogin = new Wx_alogin(xmlop);
            Frlogin.StartPosition = FormStartPosition.CenterScreen;
            Frlogin.ShowDialog();
        }
    }
    }

