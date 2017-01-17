using System.Text.RegularExpressions;
using YUNkefu.Core.Entity;
using YUNkefu.Core.Dal;

namespace YUNkefu.Core.ReplyStrategy.CharLogicPlus
{
    /// <summary>
    /// 自动回复逻辑
    /// </summary>
    public class AutoLogic : AbsCharLogic
    {
        public AutoLogic(WxContact contact, WXMsg msg) :
            base(contact,msg)
        {
        }
        public override SendMsg MakeLogic()
        {
            var m = new SendMsg()
            {
                type = 1,
                context = string.Empty
            };
            //获取系统机器人设置
            var robot = WeChatRobotDal.GetWxRobotByUin(base.Msg.Uin);
            if (robot.Rows.Count == 0) return m;
            var b_chatSwitch = bool.Parse(robot.Rows[0]["ChatSwitch"].ToString());
            if (!b_chatSwitch) return m;//关闭不聊天 

            var at_pat = "(@\\w+):<br/>@" + base.Contact.GetNickName(base.Msg.To) + "\\s(.*)";
            
            Regex r = new Regex(at_pat);
            if (r.IsMatch(base.Msg.Msg))
            {
                m.type = 1;
                var g = r.Match(base.Msg.Msg).Groups;
                var nickName = base.Contact.GetGMemberNickName(Msg.From, g[1].ToString());
                var context = g[2].ToString();
                //个人机器人设置
                if (context == "") context = "你要说点什么呢";
                m.context = "@" + nickName + " " + TuLingRobot.GetTextReply(context).Replace("<br>", "\r\n");
            }
            return m;
        } 
        
    }
}
