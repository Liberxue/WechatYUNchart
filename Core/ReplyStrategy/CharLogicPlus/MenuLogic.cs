using YUNkefu.Core.Entity;

namespace YUNkefu.Core.ReplyStrategy.CharLogicPlus
{
    /// <summary>
    /// 菜单命令
    /// </summary>
    public class MenuLogic : AbsCharLogic
    {
        public MenuLogic(WxContact contact, WXMsg msg) :
            base(contact, msg)
        {
        }
        public override SendMsg MakeLogic()
        {
            var m = new SendMsg();
            string[] cmdArray = { "[太阳]签到",
                               "[太阳]签到排名",                                                                                  
                               "[太阳]抽签",
                               "[太阳]星座",
                               "[太阳]今日运势",
                               "[太阳]处女座今日运势",
                               "[太阳]人品计算器张三",
                               "[太阳]成语接龙",
                               "[太阳]上海天气",
                               "[太阳]快递单45689565",
                               "[太阳]讲个笑话",                              
                               "[太阳]3乘以12等于多少"                                                     
                           };
            var cmd = string.Empty;
            var me_nickName = base.Contact.GetNickName(base.Msg.To);
            foreach (var c in cmdArray)
            {
                cmd += c + "\r\n";
            }
            m.type = 1;
            m.context = @"[玫瑰][玫瑰][玫瑰] 菜单 [玫瑰][玫瑰][玫瑰]\r\n菜单无需@\r\n其余口令需要AT" + me_nickName + "~~~~~~~~~~~~~~\r\n" + cmd + "~~~~~~~~~~~~~~\r\n以上展示部分口令\r\n机器人可以任意回答\r\n如果回答不匹配可以任意尝试";
            return m;
        }
    }
}
