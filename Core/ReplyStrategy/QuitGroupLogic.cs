using System;
using System.Collections.Generic;

using System.Text;
using YUNkefu.Core.Entity;

namespace YUNkefu.Core.ReplyStrategy
{
    /// <summary>
    /// 退群发送消息
    /// </summary>
    public class QuitGroupLogic:IReplyLogic
    {
        public SendMsg MakeContent(Entity.WXMsg msg)
        {
            WxContact contact = new WxContact(msg.Uin);
            //需要更新群内成员信息
            var currMember = contact.GetOnLineGroupMember(msg.From);
            contact.Add(msg.From, currMember); //更新群内联系人信息 
            SendMsg m = new SendMsg();
            m.context = "呼呼，貌似有人退群了.....";
            m.type = 1;
            return m;
        }
    }
}
