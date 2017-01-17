using System;
using System.Data;
using System.Web;

namespace YUNkefu.Core.Dal
{
    /// <summary>
    /// 自定义广告语
    /// </summary>
    public class WeChatAdvertisementDal
    {
        /// <summary>
        /// 查询所有任务
        /// </summary>
        /// <param name="robotid"></param>
        /// <returns></returns>
        public static DataTable QueryTask(string robotid)
        {
            
            string sql = string.Format("select * from WeChatAdvertisement where robotid='{0}' and BeginTime<='{1}' and ENDtime>='{1}'",
                   robotid, DateTime.Now.ToString("yyyy-MM-dd HH:mm"));
            var dt = DbHelperSQL.Query(sql).Tables[0];
            HttpRuntime.Cache.Insert("adver", dt);
            return dt;         
        }

        /// <summary>
        /// 修改最后发送时间
        /// </summary>
        /// <param name="adverId"></param>
        /// <returns></returns>
        public static bool UpdateLastSendTime(string adverId)
        {
            string sql = string.Format("UPDATE WeChatAdvertisement set LastSendTime='{0}' WHERE  AdverId='{1}'",
                  DateTime.Now.ToString("yyyy-MM-dd HH:mm"), adverId);
            return DbHelperSQL.ExecuteSql(sql) > 0;
        }
    }
}
