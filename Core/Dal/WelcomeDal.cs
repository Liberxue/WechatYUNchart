using System;
using System.Collections.Generic;

using System.Text;
using System.Data;

namespace YUNkefu.Core.Dal
{
    public class WelcomeDal
    {
        public static DataTable GetCustomWelcome(string robotid)
        {
            string sql = string.Format("select * from Welcome WHERE RobotId='{0}'",
                robotid);
            return DbHelperSQL.Query(sql).Tables[0];
        }
    }
}
