using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Imaging;
using YUNkefu.Core.Entity;
using YUNkefu.Core;
using YUNkefu.Core.Dal;
using YUNkefu.Core.ReplyStrategy;
using System.Data;

namespace YUNkefu.Controls
{
    /// <summary>
    /// 消息聊天框
    /// </summary>
    public partial class WChatBox : UserControl
    {
        /// <summary>
        /// face
        /// </summary>
        /// 
        public bool flag = false;  //显示图片或者隐藏标志位
        public bool emotionFlag = false;
        public event FriendInfoViewEventHandler FriendInfoView;
        private string _friend_base64 = "data:img/jpg;base64,";
        private string _me_base64 = "data:img/jpg;base64,";

        //聊天好友
        private WXUser _friendUser;
        public WXUser FriendUser
        {
            get
            {
                return _friendUser;
            }
            set
            {
                if (value != _friendUser)
                {
                    if (_friendUser != null)
                    {
                        _friendUser.MsgRecved -= new MsgRecvedEventHandler(_friendUser_MsgRecved);
                        _friendUser.MsgSent -= new MsgSentEventHandler(_friendUser_MsgSent);
                    }
                    _friendUser = value;
                    if (_friendUser != null)
                    {
                        _friendUser.MsgRecved += new MsgRecvedEventHandler(_friendUser_MsgRecved);
                        _friendUser.MsgSent += new MsgSentEventHandler(_friendUser_MsgSent);

                        if (_friendUser.Icon != null)  //头像信息
                        {
                            using (MemoryStream ms = new MemoryStream())
                            {
                                _friendUser.Icon.Save(ms, ImageFormat.Png);
                                _friend_base64 = "data:img/jpg;base64," + Convert.ToBase64String(ms.ToArray());
                            }
                        }

                        webKitBrowser1.DocumentText = _totalHtml = "";
                        lblInfo.Text = "与 " + _friendUser.ShowName + " 聊天中...";
                        lblInfo.Location = new Point((plTop.Width - lblInfo.Width) / 2, lblInfo.Location.Y);
                        IEnumerable<KeyValuePair<DateTime, WXMsg>> dic = _friendUser.RecvedMsg.Concat(_friendUser.SentMsg);
                        _totalHtml = (_totalHtml = _totalHtml.Replace("<a id='ok'></a>", ""));
                        foreach (KeyValuePair<DateTime, WXMsg> p in dic)  //恢复聊天记录
                        {
                            if (p.Value.From == _friendUser.UserName)
                            {
                                ShowReceiveMsg(p.Value);
                            }
                            else
                            {
                                ShowSendMsg(p.Value);
                            }
                            p.Value.Readed = true;
                        }
                    }
                }
            }
        }
        //自己
        private WXUser _meUser;
        public WXUser MeUser
        {
            get
            {
                return _meUser;
            }
            set
            {
                _meUser = value;

                if (_meUser.Icon != null)  //头像信息
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        _meUser.Icon.Save(ms, ImageFormat.Png);
                        _me_base64 = "data:img/jpg;base64," + Convert.ToBase64String(ms.ToArray());
                    }
                }
            }
        }

        /// <summary>
        /// 构造方法
        /// </summary>
        public WChatBox()
        {
            InitializeComponent();
        }
        private void LoadingEmotion()
        {
            PictureBox[,] picList = new PictureBox[12, 18];
            for (int i = 0; i < 12; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    int emotionSequenceCount = i * 10 + j;
                    picList[i, j] = new PictureBox();
                    picList[i, j].Height = picList[i, j].Width = 32;
                    picList[i, j].Image = Image.FromFile(".\\Face2\\" + emotionSequenceCount + ".gif");
                    picList[i, j].Top = i * 32;
                    picList[i, j].Left = j * 32;
                    picList[i, j].Tag = "[" + emotionSequenceCount + "]";
                    string emjoy = emotionSequenceCount.ToString();
                    picList[i, j].Parent = panImg;
                    //表情替换
                    picList[i, j].Click += new EventHandler((sender, e) =>
                    {
                        switch (emjoy)
                        {
                            case "0":
                                emjoy = ("微笑");
                                break;

                            case "1":
                                emjoy = ("撇嘴");
                                break;
                            case "2":
                                emjoy = ("色");
                                break;
                            case "3":
                                emjoy = ("发呆");
                                break;
                            case "4":
                                emjoy = ("得意");
                                break;
                            case "5":
                                emjoy = ("流泪");
                                break;
                            case "6":
                                emjoy = ("害羞");
                                break;
                            case "7":
                                emjoy = ("闭嘴");
                                break;
                            case "8":
                                emjoy = ("睡");
                                break;
                            case "9":
                                emjoy = ("大哭");
                                break;
                            case "10":
                                emjoy = ("尴尬");
                                break;
                            case "11":
                                emjoy = ("发怒");
                                break;
                            case "12":
                                emjoy = ("调皮");
                                break;
                            case "13":
                                emjoy = ("呲牙");
                                break;
                            case "14":
                                emjoy = ("惊讶");
                                break;
                            case "15":
                                emjoy = ("难过");
                                break;
                            case "16":
                                emjoy = ("酷");
                                break;
                            case "17":
                                emjoy = ("冷汗");
                                break;
                            case "18":
                                emjoy = ("抓狂");
                                break;
                            case "19":
                                emjoy = ("吐");
                                break;
                            case "20":
                                emjoy = ("偷笑");
                                break;
                            case "21":
                                emjoy = ("愉快");
                                break;
                            case "22":
                                emjoy = ("白眼");
                                break;
                            case "23":
                                emjoy = ("傲慢");
                                break;
                            case "24":
                                emjoy = ("饥饿");
                                break;
                            case "25":
                                emjoy = ("困");
                                break;
                            case "26":
                                emjoy = ("惊恐");
                                break;
                            case "27":
                                emjoy = ("流汗");
                                break;
                            case "28":
                                emjoy = ("憨笑");
                                break;
                            case "29":
                                emjoy = ("悠闲");
                                break;
                            case "30":
                                emjoy = ("奋斗");
                                break;
                            case "31":
                                emjoy = ("咒骂");
                                break;
                            case "32":
                                emjoy = ("疑问");
                                break;
                            case "33":
                                emjoy = ("嘘");
                                break;
                            case "34":
                                emjoy = ("晕");
                                break;
                            case "35":
                                emjoy = ("疯了");
                                break;
                            case "36":
                                emjoy = ("衰");
                                break;
                            case "37":
                                emjoy = ("骷髅");
                                break;
                            case "38":
                                emjoy = ("敲打");
                                break;
                            case "39":
                                emjoy = ("再见");
                                break;
                            case "40":
                                emjoy = ("擦汗");
                                break;
                            case "41":
                                emjoy = ("抠鼻");
                                break;
                            case "42":
                                emjoy = ("鼓掌");
                                break;
                            case "43":
                                emjoy = ("糗大了");
                                break;
                            case "44":
                                emjoy = ("坏笑");
                                break;
                            case "45":
                                emjoy = ("左哼哼");
                                break;
                            case "46":
                                emjoy = ("右哼哼");
                                break;
                            case "47":
                                emjoy = ("哈欠");
                                break;
                            case "48":
                                emjoy = ("鄙视");
                                break;
                            case "49":
                                emjoy = ("委屈");
                                break;
                            case "50":
                                emjoy = ("快哭了");
                                break;
                            case "51":
                                emjoy = ("阴险");
                                break;
                            case "52":
                                emjoy = ("亲亲");
                                break;
                            case "53":
                                emjoy = ("吓");
                                break;
                            case "54":
                                emjoy = ("可怜");
                                break;
                            case "55":
                                emjoy = ("菜刀");
                                break;
                            case "56":
                                emjoy = ("西瓜");
                                break;
                            case "57":
                                emjoy = ("啤酒");
                                break;
                            case "58":
                                emjoy = ("篮球");
                                break;
                            case "59":
                                emjoy = ("乒乓");
                                break;
                            case "60":
                                emjoy = ("咖啡");
                                break;
                            case "61":
                                emjoy = ("饭");
                                break;
                            case "62":
                                emjoy = ("猪头");
                                break;
                            case "63":
                                emjoy = ("玫瑰");
                                break;
                            case "64":
                                emjoy = ("凋谢");
                                break;
                            case "65":
                                emjoy = ("嘴唇");
                                break;
                            case "66":
                                emjoy = ("爱心");
                                break;
                            case "67":
                                emjoy = ("心碎");
                                break;
                            case "68":
                                emjoy = ("蛋糕");
                                break;
                            case "69":
                                emjoy = ("闪电");
                                break;
                            case "70":
                                emjoy = ("炸弹");
                                break;
                            case "71":
                                emjoy = ("刀");
                                break;
                            case "72":
                                emjoy = ("足球");
                                break;
                            case "73":
                                emjoy = ("瓢虫");
                                break;
                            case "74":
                                emjoy = ("便便");
                                break;
                            case "75":
                                emjoy = ("月亮");
                                break;
                            case "76":
                                emjoy = ("太阳");
                                break;
                            case "77":
                                emjoy = ("礼物");
                                break;
                            case "78":
                                emjoy = ("拥抱");
                                break;
                            case "79":
                                emjoy = ("强");
                                break;
                            case "80":
                                emjoy = ("弱");
                                break;
                            case "81":
                                emjoy = ("握手");
                                break;
                            case "82":
                                emjoy = ("胜利");
                                break;
                            case "83":
                                emjoy = ("抱拳");
                                break;
                            case "84":
                                emjoy = ("勾引");
                                break;
                            case "85":
                                emjoy = ("拳头");
                                break;
                            case "86":
                                emjoy = ("差劲");
                                break;
                            case "87":
                                emjoy = ("爱你");
                                break;
                            case "88":
                                emjoy = ("NO");
                                break;
                            case "89":
                                emjoy = ("OK");
                                break;
                            case "90":
                                emjoy = ("爱情");
                                break;
                            case "91":
                                emjoy = ("飞吻");
                                break;
                            case "92":
                                emjoy = ("跳跳");
                                break;
                            case "93":
                                emjoy = ("发抖");
                                break;
                            case "94":
                                emjoy = ("怄火");
                                break;
                            case "95":
                                emjoy = ("转圈");
                                break;
                            case "96":
                                emjoy = ("磕头");
                                break;
                            case "97":
                                emjoy = ("回头");
                                break;
                            case "98":
                                emjoy = ("跳绳");
                                break;
                            case "99":
                                emjoy = ("投降");
                                break;
                            case "100":
                                emjoy = ("激动");
                                break;
                            case "101":
                                emjoy = ("乱舞");
                                break;
                            case "102":
                                emjoy = ("献吻");
                                break;
                            case "103":
                                emjoy = ("左太极");
                                break;
                            case "104":
                                emjoy = ("右太极");
                                break;
                            case "105":
                                emjoy = ("微笑");
                                break;
                            case "106":
                                emjoy = ("微笑");
                                break;
                            case "107":
                                emjoy = ("微笑");
                                break;
                            case "108":
                                emjoy = ("微笑");
                                break;
                            case "109":
                                emjoy = ("猫");
                                break;
                            case "110":
                                emjoy = ("狗");
                                break;
                            case "111":
                                emjoy = ("钱");
                                break;
                            case "112":
                                emjoy = ("灯泡 ");
                                break;
                            case "113":
                                emjoy = ("饮料");
                                break;
                            case "114":
                                emjoy = ("音乐");
                                break;
                            case "115":
                                emjoy = ("药");
                                break;
                            case "116":
                                emjoy = ("吻");
                                break;
                            case "117":
                                emjoy = ("微笑");
                                break;
                            case "118":
                                emjoy = ("电话");
                                break;
                            case "119":
                                emjoy = ("时钟");
                                break;
                            case "120":
                                emjoy = ("邮件");
                                break;
                            case "121":
                                emjoy = ("电视");
                                break;
                            default:
                                Console.WriteLine("表情无效");
                                break;
                        }
                        rSendContent.AppendText("[" + emjoy + "]");
                        emotionFlag = false;
                        panImg.Visible = emotionFlag;
                    });
                    panImg.Controls.Add(picList[i, j]);
                }
            }
        }
        /// <summary>
        /// 加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WChatBox_Load(object sender, EventArgs e)
        {
            webKitBrowser1.IsWebBrowserContextMenuEnabled = false;
            LoadingEmotion();
            string[] heads = { "选中内容双击即可" };
            int[] widths = { 300 };
            Wxreply.InitHead(heads, widths);
            Wxreply.Columns[0].TextAlign = HorizontalAlignment.Center;
            string sql = string.Format("select content from Wxreply");
            var dt = DbHelperSQL.Query(sql);
            DataSet ds = new DataSet();
            ds = DbHelperSQL.Query(sql); //查询表的信息
            int rowCount, columnCount, i, j;
            rowCount = ds.Tables[0].Rows.Count;
            columnCount = ds.Tables[0].Columns.Count;
            Wxreply.BeginUpdate();
            Wxreply.Items.Clear();
            string[] lvitem = new string[columnCount];
            for (i = 0; i < rowCount; i++)
            {
                for (j = 0; j < columnCount; j++)
                {
                    lvitem[j] = ds.Tables[0].Rows[i][j].ToString();
                }
                ListViewItem lvi = new ListViewItem(lvitem);
                Wxreply.Items.Add(lvi);
            }
            Wxreply.EndUpdate();
        }
        /// <summary>
        /// 发送消息完成
        /// </summary>
        /// <param name="msg"></param>
        void _friendUser_MsgSent(WXMsg msg)
        {
            ShowSendMsg(msg);
        }
        /// <summary>
        /// 收到新消息
        /// </summary>
        /// <param name="msg"></param>
        void _friendUser_MsgRecved(WXMsg msg)
        {
            ShowReceiveMsg(msg);
        }

        /// <summary>
        /// 点击发送
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 
        private void SendContent_Click(object sender, EventArgs e)
        {

            try
            //if (rSendContent.Text != null && _friendUser != null && _meUser != null)
            {
                if (rSendContent.Text.Trim() == "") //Trim()是去除空格
                {
                    MessageBox.Show("(*^__^*) 嘻嘻……\n不能发空消息", "提 示");
                }
                else
                {
                    WXMsg msg = new WXMsg();
                    msg.Uin = _meUser.uin;
                    msg.Sid = LoginCore.GetPassTicket(_meUser.uin).WxSid;
                    msg.From = _meUser.UserName;
                    msg.Msg = rSendContent.Text;
                    msg.Readed = false;
                    msg.To = _friendUser.UserName;
                    msg.Type = 1;
                    msg.Time = DateTime.Now;
                    _friendUser.SendMsg(msg, false);
                    rSendContent.Clear();
                    rSendContent.Focus();
                }
            }
            catch
            {
                return;
            }
        }
        /// <summary>
        /// 消息输入框 回车
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SendContent_Click(sender, e);
                SendContent.Focus();
            }
        }

        /// <summary>
        /// 返回
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBack_Click(object sender, EventArgs e)
        {
            Visible = false;
        }

        /// <summary>
        /// 查看详细信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnInfo_Click(object sender, EventArgs e)
        {
            FriendInfoView.Invoke(_friendUser);
        }
        /// <summary>
        /// 大小改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WChatBox_Resize(object sender, EventArgs e)
        {
            lblInfo.Location = new Point((plTop.Width - lblInfo.Width) / 2, lblInfo.Location.Y);
        }
        #region  消息框操作相关
        /// <summary>
        /// UI界面显示发送消息
        /// </summary>
        private void ShowSendMsg(WXMsg msg)
        {
            //if (_meUser == null || _friendUser == null)
            //{
            //    return;
            //}
            try
            {
                string str = @"<script type=""text/javascript"">window.location.hash = ""#ok"";</script> 
            <div class=""chat_content_group self"">   
             <p style=""text-align:center;color:#A4B4BB;font-size:9px;"">" + msg.Time + @"</p> 
            <img class=""chat_content_avatar"" src="" + _me_base64 + @"" width=""40px"" height=""40px"">  
            <p class=""chat_nick"">" + _meUser.ShowName + @"</p>   
            <p class=""chat_content"">" + msg.Msg + @"</p>
            </div><a id='ok'></a>";
                if (_totalHtml == "")
                {
                    _totalHtml = _baseHtml;
                }
                webKitBrowser1.DocumentText = _totalHtml = _totalHtml.Replace("<a id='ok'></a>", "") + str;
                ((Action)delegate ()
                {
                    var b = WxOperateLogDal.AddWxChatcontent_formMsg(_meUser.uin, _friendUser.ShowName, _meUser.ShowName, _friendUser.ShowName, msg.Msg, msg.Time.ToString());
                }).BeginInvoke(null, null);
            }
            catch
            {
                return;
            }
        }
        /// <summary>
        /// UI界面显示接收消息
        /// </summary>
        private void ShowReceiveMsg(WXMsg msg)
        {
            //if (_meUser == null || _friendUser == null)
            //{
            //    return;
            //}
            var m = ReplyFactory.Create(msg).MakeContent(msg);
            try
            {
                string str = @"<script type=""text/javascript"">window.location.hash = ""#ok"";</script> 
            <div class=""chat_content_group buddy"">   
             <p style=""text-align:center;color:#A4B4BB;font-size:9px;"">" + msg.Time + @"</p> 
            <img class=""chat_content_avatar"" src="" + _friend_base64 + @"" width=""40px"" height=""40px"">  
            <p class=""chat_nick"">" + _friendUser.ShowName + @"</p>  
            <p class=""chat_content"">" + msg.Msg + @"</p>
            </div><a id='ok'></a>";
                if (_totalHtml == "")
                {
                    _totalHtml = _baseHtml;
                }
                msg.Readed = true;
                webKitBrowser1.DocumentText = _totalHtml = _totalHtml.Replace("<a id='ok'></a>", "") + str;
                ((Action)delegate ()
                {
                    var b = WxOperateLogDal.AddWxchatcontent_sedMsg(_meUser.uin, _friendUser.ShowName, _friendUser.ShowName, _meUser.ShowName, msg.Msg, msg.Time.ToString());
                }).BeginInvoke(null, null);
            }
            catch
            {
                return;
            }
        }
        private string _totalHtml = "";
        /// <summary>
        /// html+css+javascript UI样式
        /// </summary>
        private string _baseHtml = @"<html><head>
        <script type=""text/javascript"">window.location.hash = ""#ok"";</script>
        <style type=""text/css"">
        ::-webkit-scrollbar { width: 8px;}  
        ::-webkit-scrollbar-track {  
        }  
        ::-webkit-scrollbar-thumb {  
        border-radius: 10px;  
        background: rgba(0,0,0,0.2);   
        }   
        ::-webkit-scrollbar-thumb:window-inactive {  
        background: rgba(0,0,0,0.1);   
        }  
        ::-webkit-scrollbar-thumb:vertical:hover{  
        background-color: rgba(0,0,0,0.3);  
        }  
        ::-webkit-scrollbar-thumb:vertical:active{  
        background-color: rgba(0,0,0,0.7);  
        }  
        textarea{width: 500px;height: 300px;border: none;padding: 5px;}  

	    .chat_content_group.self {
        text-align: right;
        }
        .chat_content_group {
        padding: 5px;
        }
        .chat_content_group.self>.chat_content {
        text-align: left;
        }
        .chat_content_group.self>.chat_content {
        background: #7ccb6b;
        color:#fff;
        /*background: -webkit-gradient(linear,left top,left bottom,from(white,#e1e1e1));
        background: -webkit-linear-gradient(white,#e1e1e1);
        background: -moz-linear-gradient(white,#e1e1e1);
        background: -ms-linear-gradient(white,#e1e1e1);
        background: -o-linear-gradient(white,#e1e1e1);
        background: linear-gradient(#fff,#e1e1e1);*/
        }
        .chat_content {
        display: inline-block;
        min-height: 16px;
        max-width: 50%;
        color:#292929;
        background: #c3f1fd;
        font-family:微软雅黑;
        font-size:14px;
        /*background: -webkit-gradient(linear,left top,left bottom,from(#cf9),to(#9c3));
        background: -webkit-linear-gradient(#cf9,#9c3);
        background: -moz-linear-gradient(#cf9,#9c3);
        background: -ms-linear-gradient(#cf9,#9c3);
        background: -o-linear-gradient(#cf9,#9c3);
        background: linear-gradient(#cf9,#9c3);*/
        -webkit-border-radius: 5px;
        -moz-border-radius: 5px;
        border-radius: 5px;
        padding: 10px 15px;
        word-break: break-all;
        /*box-shadow: 1px 1px 5px #000;*/
        line-height: 1.4;
        }
        .chat_content_group.self>.chat_nick {
        text-align: right;
        }
        .chat_nick {
        font-size: 14px;
        margin: 0 0 10px;
        color:#8b8b8b;
        }
        .chat_content_group.self>.chat_content_avatar {
        float: right;
        margin: 0 0 0 10px;
        }
        .chat_content_group.buddy {
        text-align: left;
        }
        .chat_content_group {
        padding: 10px;
        }
        .chat_content_avatar {
        float: left;
        width: 40px;
        height: 40px;
        margin-right: 10px;
        }
        .imgtest{margin:10px 5px;
        overflow:hidden;}
        .list_ul figcaption p{
        font-size:11px;
        color:#aaa;
        }
        .imgtest figure div{
        display:inline-block;
        margin:5px auto;
        width:100px;
        height:100px;
        border-radius:100px;
        border:2px solid #fff;
        overflow:hidden;
        -webkit-box-shadow:0 0 3px #ccc;
        box-shadow:0 0 3px #ccc;
        }
        .imgtest img{width:100%;
        min-height:100%; text-align:center;}
	    </style>
        </head><body>";
        #endregion
        /// <summary>
        /// 聊天记录查看
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WXcontent_Click(object sender, EventArgs e)
        {
            Getwxcontent_List();
            WxTabControl.SelectedIndex = 1;
        }
        private void Getwxcontent_List()
        {
            string[] heads = {"用户", "内容","时间"};
            int[] widths = { 100,180,30 };
            shisan13ListView1.InitHead(heads, widths);
            shisan13ListView1.Columns[0].TextAlign = HorizontalAlignment.Center;
            string sql = string.Format("select chartUser,Chatcontent,CreateTime from wxchatcontent where wxuin='1262787126'");
            var dt = DbHelperSQL.Query(sql);
            DataSet ds = new DataSet();
            ds = DbHelperSQL.Query(sql); //查询表的信息
            int rowCount, columnCount, i, j;
            rowCount = ds.Tables[0].Rows.Count;
            columnCount = ds.Tables[0].Columns.Count;
            shisan13ListView1.BeginUpdate();
            shisan13ListView1.Items.Clear();
            string[] lvitem = new string[columnCount];
            for (i = 0; i < rowCount; i++)
            {
                for (j = 0; j < columnCount; j++)
                {
                    lvitem[j] = ds.Tables[0].Rows[i][j].ToString();
                }
                ListViewItem lvi = new ListViewItem(lvitem);
                shisan13ListView1.Items.Add(lvi);
            }
        }

        private void Pic_emjoy_Click_1(object sender, EventArgs e)
        {
            emotionFlag = !emotionFlag;
            panImg.Visible = emotionFlag;
        }
        /// <summary>
        /// 双击自动回复
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Wxreply_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            rSendContent.Text = Wxreply.SelectedItems[0].SubItems[0].Text;//选中的content
            SendContent_Click(sender, e);
        }

        private void addWxreply_Click(object sender, EventArgs e)
        {
        }
    }
}