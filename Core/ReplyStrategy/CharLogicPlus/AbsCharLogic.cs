using System;
using System.Collections.Generic;

using System.Text;
using YUNkefu.Core.Entity;

namespace YUNkefu.Core.ReplyStrategy.CharLogicPlus
{
    /// <summary>
    /// 聊天模式接口
    /// </summary>
    public abstract class AbsCharLogic
    {
        protected WxContact Contact;
        protected WXMsg Msg { get; set; }        
        public AbsCharLogic(WxContact contact, WXMsg msg)
        {
            this.Contact = contact;
            this.Msg = msg;
        }
        public abstract SendMsg MakeLogic();
    }
}
