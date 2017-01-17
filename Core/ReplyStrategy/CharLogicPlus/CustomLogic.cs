using YUNkefu.Core.Entity;
using YUNkefu.Core.Dal;

namespace YUNkefu.Core.ReplyStrategy.CharLogicPlus
{
    /// <summary>
    /// 自定义逻辑
    /// </summary>
    public class CustomLogic : AbsCharLogic
    {
        private string userName;
        private string nickName;
        private string context;
        public CustomLogic(WxContact contact, WXMsg msg, string msgContext,string userName) :
            base(contact,msg)
        {
            this.userName = userName;
            this.context = msgContext;
        }

        public override Entity.SendMsg MakeLogic()
        {
            if (context == "签到")
            {
                var groupUserItem = base.Contact.GetGMember(Msg.From, this.userName);
                string attrStatus = "";
                if (groupUserItem != null)
                {
                    attrStatus = groupUserItem.AttrStatus;
                    nickName = groupUserItem.NickName;
                }
                var response = CheckInDal.CheckIn(Msg.Uin, attrStatus, this.nickName);
                if (response.IsSuccess)
                {
                    string retMsg = string.Empty;
                    if (!response.IsRepeat)
                    {
                        retMsg = string.Format("@{0} 恭喜今日第{1}位签到成功\r\n本月排名:{2}\r\n累计签到:{3}天\r\n",
                            this.nickName, response.TodayRank, response.MonthRank, response.TotalCount);
                    }
                    else
                    {
                        retMsg = string.Format("@{0} 今天您已经签到过了,请勿重复签到!\r\n今日排名:{1}",
                           this.nickName, response.TodayRank);
                    }
                    return new Entity.SendMsg() { type = 1, context = retMsg };
                }
            }
            else if (context == "签到排名")
            {              
                return new Entity.SendMsg() { type = 1, context = CheckInQuery() };
            }

            return new Entity.SendMsg() { type=1,context=""};
        }


        #region 方法集合
        /// <summary>
        /// 签到查询
        /// </summary>
        /// <returns></returns>
        private string CheckInQuery()
        {
            var groupUserItem = base.Contact.GetGMember(Msg.From, this.userName);
            string attrStatus = "";
            if (groupUserItem != null)
            {
                attrStatus = groupUserItem.AttrStatus;
                nickName = groupUserItem.NickName;
            }
            var response = CheckInDal.Query(Msg.Uin, attrStatus);
            string retMsg = string.Empty;
            if (response.IsSuccess)
            {
                retMsg = string.Format("@{0} 您最后一次签到时间是{1}\r\n累计签到:{2}天\r\n",
                    this.nickName, response.LastCheckInTime.ToString("yyyy-MM-dd HH:mm:ss"),response.TotalCount);
            }
            else
            {
                retMsg = string.Format("@{0} 亲！您还未签到过呢[委屈][委屈]",
                   this.nickName, response.TotalCount);
            }
            return retMsg;
        }
        #endregion
    }
}
