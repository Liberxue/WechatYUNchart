using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using YUNkefu.Core;
using YUNkefu.Http;
using Newtonsoft.Json.Linq;
using YUNkefu.Core.Entity;
using System.Resources;
using System.Threading;
using YUNkefu.Core.ReplyStrategy;
using YUNkefu.Core.Dal;

namespace YUNkefu
{
    public partial class MainFrom : Form
    {
        WeiChartGroup.AlertForm af = null;
        #region  变量区
        private Dictionary<string, object> _dic = new Dictionary<string, object>();
        private ImageList iconList = new ImageList();
        private List<string> Uins = new List<string>();

        #endregion

        public MainFrom()
        {
            InitializeComponent();
            cbShowWay.SelectedIndex = 0;
        }

        private void MainFrom_Load(object sender, EventArgs e)
        {
            this.Text = Core.Constant.SoftName + Core.Constant.Version + "      初始化中...";
            string[] heads = { "微信唯一ID", "微信用户", "微信昵称", "上线时间", "总群数", "到期时间" };
            int[] widths = { 100, 210, 120, 150, 80, 150 };
            this.wxListView1.InitHead(heads, widths);
            this.wxListView1.NeedDrawItemIndex = -1;
            Uins = LoginCore.GetOnLineUin();
            foreach (var uin in Uins)
            {
                LoginCore.InitCookie(uin);
                AddToList(uin);
                Thread.Sleep(100);
            }
            this.wxListView1.SelectedIndexChanged += new EventHandler(wxListView1_SelectedIndexChanged);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            LoginForm frm = new LoginForm();
            frm.ShowInTaskbar = false;
            frm.Owner = this;
            frm.ShowDialog();
        }

        private void UpDataFromData()
        {
            this.BeginInvoke((Action)delegate()
            {
                this.labOnLineCount.Text = Uins.Count.ToString();
                this.Text = Core.Constant.SoftName + Core.Constant.Version;
            });
        }

        public void AddToList(string uin)
        {
            ((Action)(delegate()
            {

                string sid = LoginCore.GetPassTicket(uin).WxSid;
                WXService wx = new WXService();
                wx.Uin = uin;
                wx.Sid = sid;
                JObject init_result = wx.WxInit();
                var partUsers = new List<WXUser>();
                foreach (JObject contact in init_result["ContactList"])  //部分好友名单
                {
                    WXUser user = new WXUser();
                    //这个uin你改改名字 或者把头像单独提出来去获取~
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

                    if (string.IsNullOrEmpty(_me.NickName))
                    {
                        WriteLog("【警告】" + uin + "不能在此软件运行,请切换版本或重新扫描登陆");
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
                            this.wxListView1.BeginUpdate();
                            //把扫描好的微信加入到列表中
                            ListViewItem item = new ListViewItem();
                            item.Text = uin;
                            item.SubItems.Add(_me.UserName);
                            item.SubItems.Add(_me.ShowName);
                            item.SubItems.Add(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                            item.SubItems.Add("0");
                            item.SubItems.Add(table.Rows[0]["EndDate"].ToString());
                            this.wxListView1.Items.Add(item);
                            this.wxListView1.EndUpdate();
                        });
                        _dic.Add(uin, uin);
                        #region 开始任务
                        var robotID = table.Rows[0]["RobotId"].ToString();
                        WxTaskCore wt = new WxTaskCore(sid, uin, robotID);
                        wt.user = _me;
                        //接收消息事件
                        wt.OnRevice += new WxTaskCore.Revice(wt_OnRevice);
                        //接收修改联系人消息
                        wt.OnModifyContact += new WxTaskCore.ModifyContact(wt_OnModifyContact);
                        //通知发送信息
                        wt.OnNotifySend += new WxTaskCore.NotifySend(wt_OnNotifySend);
                        //每一个微信号，开启一个线程
                        ThreadStart start = new ThreadStart(wt.ReviceMsg);
                        new Thread(start).Start();
                        //启动发送线程
                        new Thread(new ThreadStart(wt.AutoSendMsg)).Start();
                        #endregion
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
                        s.SendMsg(args.MsgContext, args.MyUserName, u, 1);
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
                string afTitle = "[ " + c.GetNickName(msg.From) + "]回复[" + c.GetNickName(msg.To) + "]"+"一条消息";
                string afContent = msg.Msg;
                WeiChartGroup.AlertForm.ShowWay showWay = WeiChartGroup.AlertForm.ShowWay.UpDown;
                int afShowInTime, afShowTime, afShowOutTime;
                int afWidth, afHeigth;
                int.TryParse("100", out afShowInTime);
                int.TryParse("500", out afShowTime);
                int.TryParse("100", out afShowOutTime);
                int.TryParse("250", out afWidth);
                int.TryParse("120", out afHeigth);
                af = new WeiChartGroup.AlertForm();
                af.Show(afContent, afTitle, showWay, afWidth, afHeigth, afShowInTime, afShowTime, afShowOutTime);
                WriteLog(log);
                var m = ReplyFactory.Create(msg).MakeContent(msg);
                var sendContext = m.context;
                if (!string.IsNullOrEmpty(sendContext))
                {
                    WXService s = new WXService();
                    s.Sid = msg.Sid;
                    s.Uin = msg.Uin;
                    s.SendMsg(sendContext, msg.To, msg.From, m.type);  
                }
            }
            catch (Exception ex)
            {
                WriteLog("【错误】" + ex.ToString());
                Tools.WriteLog(ex.ToString());
            }
        }

        void wxListView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        void wxListView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            TabPage tab = new TabPage();
            tab.Name = "bomo";
            tab.Text = "微信013";
            //YUNkefu.Controls.UserControl1 test=new YUNkefu.Controls.UserControl1();
            //tab.Controls.Add(test);

            wechart form = new wechart();
            form.Uin = wxListView1.SelectedItems[0].SubItems[0].Text;
            //form.Sid = "qC5aRhb87Ztd3JE2";
            form.TopLevel = false;      //设置为非顶级控件
            tab.Controls.Add(form);
            tabControl1.Controls.Add(tab);

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
    }
}
