using System;
using System.Collections.Generic;

using System.Text;
using System.Data;

namespace YUNkefu.Core.Dal
{
    public class WeChatRobotDal
    {
        public static DataTable GetWxRobot(string nickName)
        {
            string sql = string.Format("select * from WeChatRobot WHERE NickName='{0}'", nickName);
            var dt = DbHelperSQL.Query(sql);
            return dt.Tables[0];
        }

        public static DataTable GetWxRobotByUin(string uin)
        {
            string sql = string.Format("select * from WeChatRobot WHERE WxUin='{0}'", uin);
            var dt = DbHelperSQL.Query(sql);
            return dt.Tables[0];
        }

        public static bool UpdateUin(string nickName,string wxUin)
        {
            string sql = string.Format("UPDATE WeChatRobot SET WxUin='{0}' WHERE NickName='{1}'",
                wxUin, nickName);
            return DbHelperSQL.ExecuteSql(sql) > 0;
        }
    }
}
