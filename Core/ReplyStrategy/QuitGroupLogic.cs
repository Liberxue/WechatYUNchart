using YUNkefu.Core.Entity;

namespace YUNkefu.Core.ReplyStrategy
{
    /// <summary>
    ///云客服退群发送消息
    /// </summary>
    public class QuitGroupLogic:IReplyLogic
    {
        public SendMsg MakeContent(WXMsg msg)
        {
            WxContact contact = new WxContact(msg.Uin);
            //需要更新群内成员信息
            var currMember = contact.GetOnLineGroupMember(msg.From);
            contact.Add(msg.From, currMember); //更新群内联系人信息 
            SendMsg m = new SendMsg();
            m.context = "云客服提醒:呼呼，貌似有人退群了.....";
            m.type = 1;
            return m;
        }
    }
}
