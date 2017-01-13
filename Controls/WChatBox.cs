using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Data.Linq;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Imaging;
using YUNkefu.Core.Entity;

namespace YUNkefu.Controls
{
    /// <summary>
    /// 消息聊天框
    /// </summary>
    public partial class WChatBox : UserControl
    {
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

                        lblInfo.Text = "与 " + _friendUser.ShowName + " 聊天中";
                        lblInfo.Location = new Point((plTop.Width - lblInfo.Width) / 2, lblInfo.Location.Y);
                        IEnumerable<KeyValuePair<DateTime, WXMsg>> dic = _friendUser.RecvedMsg.Concat(_friendUser.SentMsg);
                        dic = dic.OrderBy(i => i.Key);
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
        /// <summary>
        /// 加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WChatBox_Load(object sender, EventArgs e)
        {
            webKitBrowser1.IsWebBrowserContextMenuEnabled = false;
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
        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != null && _friendUser != null && _meUser != null)
            {
                WXMsg msg = new WXMsg();
                msg.From = _meUser.UserName;
                msg.Msg = textBox1.Text;
                msg.Readed = false;
                msg.To = _friendUser.UserName;
                msg.Type = 1;
                msg.Time = DateTime.Now;

                _friendUser.SendMsg(msg, false);
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
                button2_Click(null, null);
                e.Handled = true;
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
            if (FriendInfoView != null)
            {
                FriendInfoView(_friendUser);
            }
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
            if (_meUser == null || _friendUser == null)
            {
                return;
            }
            string str = @"<script type=""text/javascript"">window.location.hash = ""#ok"";</script> 
            <div class=""chat_content_group self"">   
            <img class=""chat_content_avatar"" src=""" + _me_base64 + @""" width=""40px"" height=""40px"">  
            <p class=""chat_nick"">" +_meUser.ShowName + @"</p>   
            <p class=""chat_content"">" + msg.Msg + @"</p>
            </div><a id='ok'></a>";
            if (_totalHtml == "")
            {
                _totalHtml = _baseHtml;
            }
            webKitBrowser1.DocumentText = _totalHtml = _totalHtml.Replace("<a id='ok'></a>", "") + str;
        }
        /// <summary>
        /// UI界面显示接收消息
        /// </summary>
        private void ShowReceiveMsg(WXMsg msg)
        {
            if (_meUser == null || _friendUser == null)
            {
                return;
            }
            string str = @"<script type=""text/javascript"">window.location.hash = ""#ok"";</script> 
            <div class=""chat_content_group buddy"">   
            <img class=""chat_content_avatar"" src=""" + _friend_base64 + @""" width=""40px"" height=""40px"">  
            <p class=""chat_nick"">" + _friendUser.ShowName + @"</p>   
            <p class=""chat_content"">" + msg.Msg + @"</p>
            </div><a id='ok'></a>";
            if (_totalHtml == "")
            {
                _totalHtml = _baseHtml;
            }
            msg.Readed = true;
            webKitBrowser1.DocumentText = _totalHtml = _totalHtml.Replace("<a id='ok'></a>", "") + str;
        }
        private string _totalHtml = "";
        private string _baseHtml = @"<html><head>
        <script type=""text/javascript"">window.location.hash = ""#ok"";</script>
        <style type=""text/css"">

        /*滚动条宽度*/  
        ::-webkit-scrollbar {  
        width: 8px;  
        }  
   
        /* 轨道样式 */  
        ::-webkit-scrollbar-track {  
        }  
   
        /* Handle样式 */  
        ::-webkit-scrollbar-thumb {  
        border-radius: 10px;  
        background: rgba(0,0,0,0.2);   
        }  
  
        /*当前窗口未激活的情况下*/  
        ::-webkit-scrollbar-thumb:window-inactive {  
        background: rgba(0,0,0,0.1);   
        }  
  
        /*hover到滚动条上*/  
        ::-webkit-scrollbar-thumb:vertical:hover{  
        background-color: rgba(0,0,0,0.3);  
        }  
        /*滚动条按下*/  
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
    }
}
