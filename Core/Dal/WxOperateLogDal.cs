using System;
using System.Collections.Generic;

using System.Text;

namespace YUNkefu.Core.Dal
{
    public class WxOperateLogDal
    {
        public static bool AddInGroupLog(string wxuin, string groupUserName, string logText)
        {
            string sql = string.Format(@"
INsert INto WxOperateLog (WxUin,GroupUserName,LogContent,WxLogType,CreateTime)
VALUES('{0}','{1}','{2}',0,'{3}')", wxuin, groupUserName, logText, DateTime.Now.ToString());
            return DbHelperSQL.ExecuteSql(sql) > 0;
        }
    }
}
