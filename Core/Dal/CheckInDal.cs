using System;
using System.Collections.Generic;

using System.Text;
using YUNkefu.Core.Entity;

namespace YUNkefu.Core.Dal
{
    public class CheckInDal
    {
        /// <summary>
        /// 签到
        /// </summary>
        /// <param name="uin"></param>
        /// <param name="groupUserName"></param>
        /// <param name="userName"></param>
        public static WxCheckInRes CheckIn(string uin,string attrStatus, string nickName)
        {
            WxCheckInRes response = new WxCheckInRes()
            {
                IsSuccess = false
            };
            var ret = 0;
            string sql = string.Format("select top 1 * from WxCheckIn WHERE WxUin='{0}' AND attrStatus='{1}'",
                uin, attrStatus);
            var dt = DbHelperSQL.Query(sql).Tables[0];
            int today = 0;  //今日最高
            int month = 0;  //本月最高
            int total = 0;

            //获取今日签到最高排行
            string sql_today = string.Format("select count(0) from WxCheckIn  WHERE WxUin='{0}' AND DateDiff(dd,[LastCheckInTime],getdate())=0",
                uin);
            var obj_t=DbHelperSQL.GetSingle(sql_today);
            if (obj_t != null)
                today = Convert.ToInt32(obj_t);

            //获取本月签到最高排行
            string sql_month = string.Format("select MAX([MonthRank]) from WxCheckIn  WHERE WxUin='{0}' AND  DateDiff(dd,[LastCheckInTime],getdate())<=30",
                uin);
            var obj_m = DbHelperSQL.GetSingle(sql_month);
            if (obj_t != null)
                month = Convert.ToInt32(obj_m);

            if (dt.Rows.Count == 0)
            {
                //从来没有签到过
                string sql_insert = string.Format(@"INSERT INto WxCheckIn ([WxUin],[attrStatus],[NickName],[LastCheckInTime],[TodayRank],[MonthRank],[TotalCheckIn])
VALUES('{0}','{1}','{2}','{3}','{4}',{5},{6})",
                                                  uin, attrStatus,  nickName, DateTime.Now.ToString(), today + 1, month + 1, 1);
                ret = DbHelperSQL.ExecuteSql(sql_insert);
                total = 1;
            }
            else
            {
                //检查今天是否签到过
                if (Convert.ToDateTime(dt.Rows[0]["LastCheckInTime"]).Date == DateTime.Now.Date)
                {
                    response.IsSuccess =true;
                    response.IsRepeat = true;
                    response.TodayRank = int.Parse(dt.Rows[0]["TodayRank"].ToString());
                    response.MonthRank = int.Parse(dt.Rows[0]["MonthRank"].ToString()); 
                    response.TotalCount = int.Parse(dt.Rows[0]["TotalCheckIn"].ToString()); 
                    return response;
                }
                else
                {
                    var sql_update = string.Format(@"UPDATE [dbo].[WxCheckIn] 
                set [LastCheckInTime]='{0}',[TodayRank]={1},[MonthRank]={2},[TotalCheckIn]=[TotalCheckIn]+1 WHERE [WxUin]='{3}' AND [attrStatus]='{4}'",
                         DateTime.Now.ToString(), today + 1, month + 1, uin, attrStatus);
                    ret = DbHelperSQL.ExecuteSql(sql_update);
                    total = int.Parse(dt.Rows[0]["TotalCheckIn"].ToString())+1;
                }
            }

            response.IsSuccess = ret > 0;
            response.TodayRank = today + 1;
            response.MonthRank = month + 1;
            response.TotalCount = total;
            return response;
        }

        public static WxCheckInRes Query(string uin, string attrStatus)
        {
            string sql = string.Format("select top 1 * from [dbo].[WxCheckIn] WHERE [WxUin]='{0}' AND [attrStatus]='{1}'",
               uin, attrStatus);
            var dt = DbHelperSQL.Query(sql).Tables[0];
            if (dt.Rows.Count > 0)
            {
                return new WxCheckInRes()
                {
                    IsSuccess=true,
                    TodayRank=0,
                    MonthRank=0,
                    TotalCount=int.Parse(dt.Rows[0]["TotalCheckIn"].ToString()),
                    LastCheckInTime = Convert.ToDateTime(dt.Rows[0]["LastCheckInTime"].ToString())
                };
            }
            else
            {
                return new WxCheckInRes() { IsSuccess = false };
            }
        }
    }
}
