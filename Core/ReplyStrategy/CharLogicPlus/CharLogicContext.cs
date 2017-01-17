using System.Text.RegularExpressions;
using YUNkefu.Core.Entity;

namespace YUNkefu.Core.ReplyStrategy.CharLogicPlus
{
    public class CharLogicContext
    {
        WxContact contact;
        WXMsg msg;
        public CharLogicContext(WxContact contact, Entity.WXMsg msg)
        {
            this.contact = contact;
            this.msg = msg;
        }
        public AbsCharLogic Create()
        {
            string msg_context = string.Empty;
            Regex regex = new Regex(@"(@\w+):<br/>(菜单)");
            if (regex.IsMatch(msg.Msg))
            {
                return new MenuLogic(contact, msg);  //菜单
            }

            var nomal_pat = "(@\\w+):<br/>(\\w+)";
            Regex n_r = new Regex(nomal_pat);
            int flag = 1; //1.普通消息 2,at机器人
            var userName=string.Empty;
            if (n_r.IsMatch(msg.Msg))
            {
                flag = 1;
                msg_context = n_r.Match(msg.Msg).Groups[2].ToString();
                userName=n_r.Match(msg.Msg).Groups[1].ToString();
            }


            var at_pat = "(@\\w+):<br/>@" + contact.GetNickName(msg.To) + "\\s(.*)";

            Regex r = new Regex(at_pat);
            if (r.IsMatch(msg.Msg))
            {
                flag = 2;
                msg_context = r.Match(msg.Msg).Groups[2].ToString();  //内容
                userName=r.Match(msg.Msg).Groups[1].ToString();
            }
            //如果内容必须使用自定义逻辑
            if (UseCustomLogic(msg_context))
            {
                return new CustomLogic(contact, msg, msg_context,userName); //自定义逻辑
            }
            else
            {
                if (flag == 1)
                {
                    return new CustomLogic(contact, msg, msg_context, userName); //自定义逻辑
                }
                if (flag == 2)
                {
                    return new AutoLogic(contact, msg);
                }
            }
            return null;
        }

        private bool UseCustomLogic(string context)
        {
            var key = new string[] { 
            "签到","签到排名"
            };
            foreach (var c in key)
            {
                if (c.Equals(context))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
