using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using YUNkefu.Http;

namespace YUNkefu.Core.Entity
{
    [Serializable]
    public class WXUser
    {
        /// <summary>
        /// 微信唯一标识
        /// </summary>
        public string uin { get; set; }
        /// <summary>
        /// 用户ID
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 昵称
        /// </summary>
        public string NickName { get; set; }
        /// <summary>
        /// 头像url
        /// </summary>
        public string HeadImgUrl { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string RemarkName { get; set; }
        /// <summary>
        /// 性别  男1 女2 其他0
        /// </summary>
        public string Sex { get; set; }
        /// <summary>
        /// 前面
        /// </summary>
        public string Signature { get; set; }
        /// <summary>
        /// 市
        /// </summary>
        public string City { get; set; }
        /// <summary>
        /// 省
        /// </summary>
        public string Province { get; set; }
        /// <summary>
        /// 拼音全拼
        /// </summary>
        private string _pyQuanPin;
        public string PYQuanPin
        {
            get
            {
                return _pyQuanPin;
            }
            set
            {
                _pyQuanPin = value;
            }
        }
        /// <summary>
        /// 备注名拼音全屏
        /// </summary>
        private string _remarkPYQuanPin;
        public string RemarkPYQuanPin
        {
            get
            {
                return _remarkPYQuanPin;
            }
            set
            {
                _remarkPYQuanPin = value;
            }
        }
        private Image _icon = null;
        private bool _loading_icon = false;

        /// <summary>
        /// 头像
        /// </summary>
        public Image Icon
        {
            get
            {
                if (_icon == null && !_loading_icon)
                {
                    _loading_icon = true;
                    ((Action)(delegate()
                    {
                        WXService wxs = new WXService();
                        if (UserName.Contains("@@"))  //讨论组
                        {
                            _icon = wxs.GetIcon(HeadImgUrl, uin);
                        }
                        else if (UserName.Contains("@"))  //好友
                        {
                            _icon = wxs.GetIcon(HeadImgUrl, uin);
                        }
                        else
                        {
                            _icon = wxs.GetIcon(HeadImgUrl, uin);
                        }
                        _loading_icon = false;
                    })).BeginInvoke(null, null);
                }
                return _icon;
            }
        }

        /// <summary>
        /// 显示名称
        /// </summary>
        public string ShowName
        {
            get
            {
                return (RemarkName == null || RemarkName == "") ? NickName : RemarkName;
            }
        }
        /// <summary>
        /// 显示的拼音全拼
        /// </summary>
        public string ShowPinYin
        {
            get
            {
                return (_remarkPYQuanPin == null || _remarkPYQuanPin == "") ? _pyQuanPin : _remarkPYQuanPin;
            }
        }

        //发送给对方的消息  
        private Dictionary<DateTime, WXMsg> _sentMsg = new Dictionary<DateTime, WXMsg>();
        public Dictionary<DateTime, WXMsg> SentMsg
        {
            get
            {
                return _sentMsg;
            }
        }
        //收到对方的消息
        private Dictionary<DateTime, WXMsg> _recvedMsg = new Dictionary<DateTime, WXMsg>();
        public Dictionary<DateTime, WXMsg> RecvedMsg
        {
            get
            {
                return _recvedMsg;
            }
        }

        public event MsgSentEventHandler MsgSent;
        public event MsgRecvedEventHandler MsgRecved;

        /// <summary>
        /// 接收来自该用户的消息
        /// </summary>
        /// <param name="msg"></param>
        public void ReceiveMsg(WXMsg msg)
        {
            _recvedMsg.Add(msg.Time, msg);
            if (MsgRecved != null)
            {
                MsgRecved(msg);
                return;
            }
            //try
            //{
            //    _recvedMsg.Add(msg.Time, msg);
            //}
            //catch
            //{
            //    // MsgRecved(null);
            //    return;
            //}
        }
        /// <summary>
        /// 向该用户发送消息
        /// </summary>
        /// <param name="msg"></param>
        public void SendMsg(WXMsg msg, bool showOnly)
        {
            try
            {
                if (!showOnly)
                {
                    WXService wxs = new WXService();
                    wxs.SendMsg(msg.Msg, msg.From, msg.To, msg.Type, msg.Uin, msg.Sid);
                }
                _sentMsg.Add(msg.Time, msg);
                MsgSent.Invoke(msg);
            }
            catch
            {

                return;
            }
        }
        /// <summary>
        /// 获取该用户发送的未读消息
        /// </summary>
        /// <returns></returns>
        public List<WXMsg> GetUnReadMsg()
        {
            List<WXMsg> list = null;
            foreach (KeyValuePair<DateTime, WXMsg> p in _recvedMsg)
            {
                if (!p.Value.Readed)
                {
                    if (list == null)
                    {
                        list = new List<WXMsg>();
                    }
                    list.Add(p.Value);
                }
            }

            return list;
        }
        /// <summary>
        /// 获取最近的一条消息
        /// </summary>
        /// <returns></returns>
        public WXMsg GetLatestMsg()
        {
            WXMsg msg = null;
            if (_sentMsg.Count > 0 && _recvedMsg.Count > 0)
            {
                msg = _sentMsg.Last().Value.Time > _recvedMsg.Last().Value.Time ? _sentMsg.Last().Value : _recvedMsg.Last().Value;
            }
            else if (_sentMsg.Count > 0)
            {
                msg = _sentMsg.Last().Value;
            }
            else if (_recvedMsg.Count > 0)
            {
                msg = _recvedMsg.Last().Value;
            }
            else
            {
                msg = null;
            }
            return msg;
        }
    }
    /// <summary>
    /// 表示处理消息发送完成事件的方法
    /// </summary>
    /// <param name="msg"></param>
    public delegate void MsgSentEventHandler(WXMsg msg);
    /// <summary>
    /// 表示处理接收到新消息事件的方法
    /// </summary>
    /// <param name="msg"></param>
    public delegate void MsgRecvedEventHandler(WXMsg msg);
}

/// <summary>
/// 微信群内成员信息
/// </summary>
[Serializable]
public class GroupWxUser
{
    public string UserName { get; set; }
    public string NickName { get; set; }
    public string DisplayName { get; set; }
    public string AttrStatus { get; set; }
}


