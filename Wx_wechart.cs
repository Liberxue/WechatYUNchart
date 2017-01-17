using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;
using YUNkefu.Controls;
using YUNkefu.Core.Entity;
using YUNkefu.Http;
using YUNkefu.Core;
using YUNkefu.Core.Dal;
using System.Threading;
using YUNkefu.Core.ReplyStrategy;

namespace YUNkefu
{
    /// <summary>
    /// 主界面
    /// </summary>
    public partial class Wx_wechart : Form
    {
        YUNkefu.Wx_AlertForm af = null;
        /// <summary>
        /// 主界面等待提示
        /// </summary>
        private Label _lblWait;
        /// <summary>
        /// 聊天对话框
        /// </summary>
        private WChatBox _chat2friend;
        /// <summary>
        /// 好友信息框
        /// </summary>
        private WPersonalInfo _friendInfo;
        /// <summary>
        /// 当前登录微信用户
        /// </summary>
        private WXUser _me;
        private List<Object> _contact_all = new List<object>();
        private List<object> _contact_latest = new List<object>();

        /// <summary>
        /// 构造方法
        /// </summary>
        public Wx_wechart()
        {
            InitializeComponent();

            _chat2friend = new WChatBox();
            _chat2friend.Dock = DockStyle.Fill;
            _chat2friend.Visible = false;
            _chat2friend.FriendInfoView += new FriendInfoViewEventHandler(_chat2friend_FriendInfoView);
            Controls.Add(_chat2friend);

            _friendInfo = new WPersonalInfo();
            tableLayoutPanel1.Dock = DockStyle.Fill;
            _friendInfo.Visible = false;
            _friendInfo.StartChat += new StartChatEventHandler(_friendInfo_StartChat);
            Controls.Add(_friendInfo);

            _lblWait = new Label();
            _lblWait.Text = "...数 据 加 载...";
            _lblWait.AutoSize = false;
            _lblWait.Size = this.ClientSize;
            _lblWait.TextAlign = ContentAlignment.MiddleCenter;
            _lblWait.Location = new Point(0, 0);
            Controls.Add(_lblWait);
        }

        public string Uin = "";
        public string Sid = "";
        public string robotID = "";

        #region  事件处理程序
        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmMainForm_Load(object sender, EventArgs e)
        {
            DoMainLogic();
        }
        /// <summary>
        /// 好友信息框中点击 聊天
        /// </summary>
        /// <param name="user"></param>
        void _friendInfo_StartChat(WXUser user)
        {
            if (_chat2friend != null)
            {
                _chat2friend.Visible = true;
                _chat2friend.BringToFront();
                _chat2friend.MeUser = _me;
                _chat2friend.FriendUser = user;
                chart_main.Controls.Add(_chat2friend);
                _chat2friend.Show();
            }
        }
        /// <summary>
        /// 聊天对话框中点击 好友信息
        /// </summary>
        /// <param name="user"></param>
        void _chat2friend_FriendInfoView(WXUser user)
        {
            _friendInfo.FriendUser = user;
            _friendInfo.Visible = true;
            _friendInfo.BringToFront();
            chart_main.Controls.Add(_friendInfo);
            _friendInfo.Show();
        }
        /// <summary>
        /// 聊天列表点击好友   开始聊天
        /// </summary>
        /// <param name="user"></param>
        private void wchatlist_StartChat(WXUser user)
        {
            _chat2friend.Visible = true;
            _chat2friend.BringToFront();
            _chat2friend.MeUser = _me;
            _chat2friend.FriendUser = user;
            chart_main.Controls.Add(_chat2friend);
            _chat2friend.Show();
        }
        /// <summary>
        /// 通讯录中点击好友 查看好友信息
        /// </summary>
        /// <param name="user"></param>
        private void wfriendlist_FriendInfoView(WXUser user)
        {
            _friendInfo.FriendUser = user;
            _friendInfo.Visible = true;
            _friendInfo.BringToFront();
            chart_main.Controls.Add(_chat2friend);
            _chat2friend.Show();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmMainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Environment.Exit(0);
        }
        #endregion

        #region 主逻辑
        /// <summary>
        /// 
        /// </summary>
        private void DoMainLogic()
        {
            _lblWait.BringToFront();
            ((Action)(delegate ()
            {
                Dictionary<string, string> ss = new Dictionary<string, string>();
                //先判断下键值是否存在要不卡死头像只能显示一个用户的
                //if (!ss.ContainsKey("1"))
                //{
                //    ss.Add("1", "1");
                //}
                string sid = LoginCore.GetPassTicket(Uin).WxSid;
                WXService wxs = new WXService();
                wxs.Uin = Uin;
                wxs.Sid = sid;
                wxs.robotID = robotID;
                JObject init_result = wxs.WxInit();  //初始化
                List<object> contact_all = new List<object>();
                if (init_result != null)
                {
                    _me = new WXUser();
                    _me.uin = wxs.Uin;
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
                    foreach (JObject contact in init_result["ContactList"])  //部分好友名单
                    {
                        WXUser user = new WXUser();
                        user.uin = wxs.Uin;
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
                        _contact_latest.Add(user);
                    }
                }
                JObject contact_result = wxs.GetContact(); //通讯录
                if (contact_result != null)
                {
                    foreach (JObject contact in contact_result["MemberList"])  //完整好友名单
                    {
                        WXUser user = new WXUser();
                        user.uin = wxs.Uin;
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
                        contact_all.Add(user);
                        //((Action)delegate()
                        //{
                        //    //写入所有好友信息
                        //    var b = WxOperateLogDal.AddchartLog(wxs.Uin, contact["UserName"].ToString(), contact["City"].ToString(), contact["HeadImgUrl"].ToString(), contact["NickName"].ToString(), contact["Province"].ToString(), contact["PYQuanPin"].ToString(), contact["RemarkName"].ToString(), contact["RemarkPYQuanPin"].ToString(), contact["Sex"].ToString(), contact["Signature"].ToString());
                        //}).BeginInvoke(null, null);
                        wChatList1.Invalidate();
                    }
                }
                IOrderedEnumerable<object> list_all = contact_all.OrderBy(e => (e as WXUser).ShowPinYin);

                WXUser wx; string start_char;
                foreach (object o in list_all)
                {
                    wx = o as WXUser;
                    start_char = wx.ShowPinYin == "" ? "" : wx.ShowPinYin.Substring(0, 1);
                    if (!_contact_all.Contains(start_char.ToUpper()))
                    {
                        _contact_all.Add(start_char.ToUpper());
                    }
                    _contact_all.Add(o);
                }

                this.BeginInvoke((Action)(delegate ()  //等待结束
                {
                    _lblWait.Visible = false;
                    wChatList1.Items.AddRange(_contact_latest.ToArray());  //近期联系人
                    wFriendsList1.Items.AddRange(_contact_all.ToArray());  //通讯录
                    wpersonalinfo.FriendUser = _me;
                }));
                string sync_flag = "";
                JObject sync_result;
                while (true)
                {
                    sync_flag = wxs.WxSyncCheck();  //同步检查
                    if (sync_flag == null)
                    {
                        continue;
                    }
                    //这里应该判断 sync_flag中selector的值
                    else //有消息
                    {
                        sync_result = wxs.WxSync();  //进行同步
                        if (sync_result != null)
                        {
                            if (sync_result["AddMsgCount"] != null && sync_result["AddMsgCount"].ToString() != "0")
                            {
                                foreach (JObject m in sync_result["AddMsgList"])
                                {
                                    string from = m["FromUserName"].ToString();
                                    string to = m["ToUserName"].ToString();
                                    string content = m["Content"].ToString();
                                    string MsgId = m["MsgId"].ToString();
                                    string type = m["MsgType"].ToString();//语音视频标识
                                    WXMsg msg = new WXMsg();
                                    msg.From = from;
                                    wxs.Uin = Uin;
                                    wxs.Sid = sid;
                                    msg.Sid = wxs.Sid;
                                    msg.Uin = wxs.Uin;
                                    msg.Msg = type == "1" ? content : "收到一个系统数据";  //只接受文本消息
                                    if (msg.Type == 1)  //屏蔽一些系统数据
                                    {
                                        msg.Msg = content;
                                    }
                                    msg.Readed = false;
                                    msg.Time = DateTime.Now;
                                    msg.To = to;
                                    msg.Type = int.Parse(type);
                                    //if (msg.Type == 51)  //屏蔽一些系统数据
                                    //{
                                    //    msg.Msg = "收到一个系统数据";
                                    //}
                                    if (msg.Type == 10000)//进群退群消息
                                    {
                                        string s = sync_result.ToString();
                                    }
                                    if (msg.Type == 3)//图片
                                    {
                                        msg.Msg = content + "收到一个图片";
                                        string sFilePath = Environment.CurrentDirectory + "\\IMG";
                                        string sFileName = MsgId + ".jpg";
                                        sFileName = sFilePath + "\\" + sFileName; //文件的绝对路径
                                        HttpService.HttpDownloadFile(Constant._getmsgimg_url + MsgId, msg.Uin, sFileName);
                                    }

                                    if (msg.Type == 34)//语音
                                    {
                                        msg.Msg = "收到语音点击查看";
                                        //Button newButton = new Button();//创建一个名为newButton的新按钮
                                        //newButton.Width = 150;
                                        //newButton.Text = "NewButton";
                                        //newButton.BackColor = Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
                                        //newButton.Click += new EventHandler(textBox1_Click);
                                        string sFilePath = Environment.CurrentDirectory + "\\MP3";
                                        string sFileName = MsgId + ".wav";
                                        sFileName = sFilePath + "\\" + sFileName; //文件的绝对路径
                                        HttpService.HttpDownloadFile(Constant._getmsgmp3_url + MsgId, msg.Uin, sFileName);
                                    }
                                    if (msg.Type == 43)//小视频
                                    {
                                        msg.Msg = "收到小视频" + "点击查看";
                                        string sFilePath = Environment.CurrentDirectory + "\\MP4";
                                        string sFileName = MsgId + ".MP4";
                                        sFileName = sFilePath + "\\" + sFileName;
                                        HttpService.HttpDownloadFile(Constant._getmsgvideo_url + MsgId, msg.Uin, sFileName);
                                    }
                                    if (msg.Type == 62)//视频
                                    {
                                        msg.Msg = content + "收到一个视频";
                                    }
                                    if (msg.Type == 10002)//消息撤回
                                    {
                                        msg.Msg = content + "对方撤回了消息";
                                    }
                                    if (msg.Type == 42)//好友名片
                                    {
                                        msg.Msg = content + "收到一个好友名片";
                                    }
                                    if (msg.Type == 47)//动态表情
                                    {
                                        msg.Msg = content + "收到一个动态表情";
                                    }
                                    if (msg.Type == 48)//位置消息
                                    {
                                        msg.Msg = content + "收到一个位置消息";
                                    }
                                    if (msg.Type == 49)//分享链接
                                    {
                                        msg.Msg = content + "收到一个分享链接";
                                    }
                                    BeginInvoke((Action)delegate ()
                                    {
                                        WXUser user; bool exist_latest_contact = false;
                                        foreach (object u in wChatList1.Items)
                                        {
                                            user = u as WXUser;

                                            if (user != null)
                                            {
                                                if (user.UserName == msg.From && msg.To == _me.UserName)  //接收别人消息
                                                {
                                                    wChatList1.Items.Remove(user);
                                                    wChatList1.Items.Insert(0, user);
                                                    exist_latest_contact = true;
                                                    user.ReceiveMsg(msg);
                                                    break;
                                                }
                                                else if (user.UserName == msg.To && msg.From == _me.UserName)  //同步自己在其他设备上发送的消息
                                                {
                                                    wChatList1.Items.Remove(user);
                                                    wChatList1.Items.Insert(0, user);
                                                    exist_latest_contact = true;
                                                    user.SendMsg(msg, true);
                                                    break;
                                                }
                                            }
                                        }
                                        if (!exist_latest_contact)
                                        {
                                            foreach (object o in wFriendsList1.Items)
                                            {
                                                WXUser friend = o as WXUser;
                                                if (friend != null && friend.UserName == msg.From && msg.To == _me.UserName)
                                                {
                                                    wChatList1.Items.Insert(0, friend);
                                                    friend.ReceiveMsg(msg);
                                                    break;
                                                }
                                                if (friend != null && friend.UserName == msg.To && msg.From == _me.UserName)
                                                {
                                                    wChatList1.Items.Insert(0, friend);
                                                    friend.SendMsg(msg, true);
                                                    break;
                                                }
                                            }
                                        }
                                        Wx_AlertForm.ShowWay showWay = Wx_AlertForm.ShowWay.UpDown;
                                        string aftitle = _me.NickName + "收到来自" + _me.ShowName + "一条消息";
                                        string afContent = msg.Msg;
                                        int afShowInTime, afShowTime, afShowOutTime;
                                        int afWidth, afHeigth;
                                        int.TryParse("100", out afShowInTime);
                                        int.TryParse("8000", out afShowTime);
                                        int.TryParse("800", out afShowOutTime);
                                        int.TryParse("250", out afWidth);
                                        int.TryParse("120", out afHeigth);
                                        af = new Wx_AlertForm();
                                        af.Show(afContent, aftitle, showWay, afWidth, afHeigth, afShowInTime, afShowTime, afShowOutTime);
                                    });
                                }
                            }
                        }
                    }
                    //System.Threading.Thread.Sleep(800);
                    #region 开始任务
                    //var robotID = table.Rows[0]["RobotId"].ToString();
                    WxTaskCore wt = new WxTaskCore(sid, Uin, robotID);
                    wt.user = _me;
                    //接收消息事件
                    //wt.OnRevice += new WxTaskCore.Revice(wt_OnRevice);
                    //接收修改联系人消息
                    wt.OnModifyContact += new WxTaskCore.ModifyContact(wt_OnModifyContact);
                    //通知发送信息
                    wt.OnNotifySend += new WxTaskCore.NotifySend(wt_OnNotifySend);
                    ////每一个微信号，开启一个线程
                    //ThreadStart start = new ThreadStart(wt.ReviceMsg);
                    //new Thread(start).Start();
                    //启动发送线程
                    new Thread(new ThreadStart(wt.AutoSendMsg)).Start();
                    #endregion
                }

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
            ((Action)(delegate ()
            {
                if (!string.IsNullOrEmpty(args.MsgContext))
                {
                    WXService s = new WXService();
                    s.Sid = args.Sid;
                    s.Uin = args.WxUin;
                    foreach (var u in args.GroupUserName)
                    {
                        //s.SendMsg(args.MsgContext, args.MyUserName, u, 1);
                        Tools.WriteLog("【发送】" + args.MsgContext);
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
                Tools.WriteLog(log);
                var m = ReplyFactory.Create(msg).MakeContent(msg);
                var sendContext = m.context;
                if (!string.IsNullOrEmpty(sendContext))
                {
                    WXService s = new WXService();
                    s.Sid = msg.Sid;
                    s.Uin = msg.Uin;
                    s.SendMsg(sendContext, msg.To, msg.From, m.type, msg.Uin, msg.Sid);
                }
                Wx_AlertForm.ShowWay showWay = Wx_AlertForm.ShowWay.UpDown;
                string afTitle = "[ " + c.GetNickName(msg.From) + "]回复[" + c.GetNickName(msg.To) + "]" + "一条消息";
                string afContent = msg.Msg;
                int afShowInTime, afShowTime, afShowOutTime;
                int afWidth, afHeigth;
                int.TryParse("100", out afShowInTime);
                int.TryParse("8000", out afShowTime);
                int.TryParse("800", out afShowOutTime);
                int.TryParse("250", out afWidth);
                int.TryParse("120", out afHeigth);
                af = new Wx_AlertForm();
                af.Show(afContent, afTitle, showWay, afWidth, afHeigth, afShowInTime, afShowTime, afShowOutTime);
                Tools.WriteLog("【demo】" + log);
            }

            catch (Exception ex)
            {
                Tools.WriteLog("【错误】" + ex.ToString());
                Tools.WriteLog(ex.ToString());
            }
        }

        #endregion


        private void wTabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void wpersonalinfo_Load(object sender, EventArgs e)
        {

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void tabPage3_Click(object sender, EventArgs e)
        {

        }

        private void wChatList1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void wFriendsList1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }
        private void textBox1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("hello world!");
        }

    }
}
#endregion