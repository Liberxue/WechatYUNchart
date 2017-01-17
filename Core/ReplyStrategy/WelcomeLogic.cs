using System;
using System.Collections.Generic;

using System.Text;
using YUNkefu.Core.Entity;
using YUNkefu.Core.Dal;

namespace YUNkefu.Core.ReplyStrategy
{
    /// <summary>
    /// 进群欢迎语
    /// </summary>
    public class WelcomeLogic:IReplyLogic
    {
        /// <summary>
        /// 进群欢迎语
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public SendMsg MakeContent(WXMsg msg)
        {
            var SendMsg = new SendMsg();            
            WxContact contact=new WxContact(msg.Uin);
            string groupUserName = msg.From; //群用户名
            var oldMember = contact.GetGroupMemberNames(groupUserName);
            //获取当前群内所有成员信息
            var currMember = contact.GetOnLineGroupMember(groupUserName);

            var newMemberList = new List<GroupWxUser>();

            var dic_oldUserName = new Dictionary<string, string>();
            foreach (var old_s in oldMember.MemberUserNames)
            {
                dic_oldUserName.Add(old_s.UserName, old_s.UserName);                
            }

            foreach (var s in currMember.MemberUserNames)
            {
                if (!dic_oldUserName.ContainsKey(s.UserName))
                {
                    newMemberList.Add(s);
                }               
            }

            string nickName = string.Empty;
            foreach (var m in newMemberList)
            {
                nickName += m.NickName + "、";
            }
            //获取用户信息            
            contact.Add(groupUserName, currMember); //更新群内联系人信息

            //获取系统设置的欢迎信息
            var robot = WeChatRobotDal.GetWxRobotByUin(msg.Uin);
            if (robot.Rows.Count == 0)
            {
                return new SendMsg();
            }

            var welcomeSwitch=int.Parse(robot.Rows[0]["WelcomeSwitch"].ToString());

            var ret = string.Empty;

            switch (welcomeSwitch)
            {
                case (int)EnumContainer.CommonSwichEnum.关闭不发送:                    
                    break;
                case (int)EnumContainer.CommonSwichEnum.自定义语句:
                    ret = GetCustomWelcome(robot.Rows[0]["RobotId"].ToString());
                    break;
                case (int)EnumContainer.CommonSwichEnum.通用语句:
                    ret = "请遵守群规[微笑]";
                    break;
            }

            ((Action)delegate()
            {
                //写进群数据库
                var b = WxOperateLogDal.AddInGroupLog(msg.Uin, groupUserName, msg.Msg);
            }).BeginInvoke(null, null);


            if (!string.IsNullOrEmpty(ret))
                ret = string.Format("@{0} {1}", nickName.TrimEnd('、'), ret);
            SendMsg.context = ret;
            SendMsg.type = 1;
            return SendMsg;
        }

        private string GetCustomWelcome(string robotid)
        {
            var dt = WelcomeDal.GetCustomWelcome(robotid);
            if (dt.Rows.Count == 0)
                return "";
            Random r = new Random();
            int i=r.Next(0, dt.Rows.Count - 1);            
            return dt.Rows[i]["Welcome"].ToString();
        }
    }
}
