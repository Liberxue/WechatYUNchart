using System;
using System.Collections.Generic;

namespace YUNkefu.Core.Entity
{
    public class WXMsg
    {
        public string Sid { get; set; }
        public string Uin { get; set; }

        /// <summary>
        /// 消息发送方
        /// </summary>
        public string From
        {
            get;
            set;
        }
        /// <summary>
        /// 消息接收方
        /// </summary>
        public string To
        {
            set;
            get;
        }        

        /// <summary>
        /// 消息发送时间
        /// </summary>
        public DateTime Time
        {
            get;
            set;
        }
        /// <summary>
        /// 是否已读
        /// </summary>
        public bool Readed
        {
            get;
            set;
        }
        /// <summary>
        /// 消息内容
        /// </summary>
        public string Msg
        {
            get;
            set;
        }
        /// <summary>
        /// 消息类型 1:文本消息  51系统消息  10000 进群退群消息
        /// </summary>
        public int Type
        {
            get;
            set;
        }
    }

    public class SendMsg
    {
        public string context { get; set; }
        public int type { get; set; }
    }

    public class NotifyArgs : EventArgs
    {
        public string MsgContext { get; set; }
        public string WxUin { get; set; }
        public string Sid { get; set; }
        public string MyUserName { get; set; }
        public List<string> GroupUserName { get; set; }
    }
}
