using System;
using System.Collections.Generic;

using System.Text;
using YUNkefu.Core.Dal;
using System.Text.RegularExpressions;
using YUNkefu.Core.Entity;
using System.Web;
using YUNkefu.Core.ReplyStrategy.CharLogicPlus;

namespace YUNkefu.Core.ReplyStrategy
{
    /// <summary>
    /// 和机器人聊天逻辑
    /// </summary>
    public class ChatLogic:IReplyLogic
    {
        public SendMsg MakeContent(Entity.WXMsg msg)
        {
            SendMsg m = new SendMsg();
            m.type = 1;
            m.context = string.Empty;
            WxContact contact = new WxContact(msg.Uin);
           
            var plus = new CharLogicContext(contact, msg);
            if (plus != null)
            {
                m = plus.Create().MakeLogic();
            }
            return m;
        }
       
    }
}
