using System;

namespace YUNkefu.Core.Dal
{
    /// <summary>
    /// 多委托存储数据时间
    /// </summary>
    public class WxOperateLogDal
    {
        /// <summary>
        /// 存储群数据库
        /// </summary>
        /// <param name="wxuin"></param>
        /// <param name="groupUserName"></param>
        /// <param name="logText"></param>
        /// <returns></returns>
        public static bool AddInGroupLog(string wxuin, string groupUserName, string logText)
        {
            string sql = string.Format(@"
INsert INto WxOperateLog (WxUin,GroupUserName,LogContent,WxLogType,CreateTime)
VALUES('{0}','{1}','{2}',0,'{3}')", wxuin, groupUserName, logText, DateTime.Now.ToString());
            return DbHelperSQL.ExecuteSql(sql) > 0;
        }
        /// <summary>
        /// 保存发送消息
        /// </summary>
        /// <param name="wxuin"></param>
        /// <param name="groupUserName"></param>
        /// <param name="logText"></param>
        /// <returns></returns>
        public static bool AddWxsendmsglog(string wxuin, string groupUserName, string logText,string to,string from)
        {
            string sql = string.Format(@"
INsert INto Wxsendmsglog (WxUin,username,LogContent,WxLogType,Wxto,Wxfrom,CreateTime)
VALUES('{0}','{1}','{2}',0,'{3}')", wxuin, groupUserName, logText,to,from, DateTime.Now.ToString());
            return DbHelperSQL.ExecuteSql(sql) > 0;
        }
        /// <summary>
        /// 保存收到消息
        /// </summary>
        /// <param name="wxuin"></param>
        /// <param name="groupUserName"></param>
        /// <param name="logText"></param>
        /// <returns></returns>
        public static bool AddWxrogermsglog(string wxuin, string groupUserName, string logText, string to, string from)
        {
            string sql = string.Format(@"
INsert INto Wxrogermsglog(WxUin,username,LogContent,WxLogType,Wxto,Wxfrom,CreateTime)
VALUES('{0}','{1}','{2}',0,'{3}','{4}','{5}')", wxuin, groupUserName, logText, to, from, DateTime.Now.ToString());
            return DbHelperSQL.ExecuteSql(sql) > 0;
        }
        /// <summary>
        ///保存微信好友数据
        /// </summary>
        /// <param name="wxuin"></param>
        /// <param name="UserName"></param>
        /// <param name="City"></param>
        /// <param name="HeadImgUrl"></param>
        /// <param name="NickName"></param>
        /// <param name="Province"></param>
        /// <param name="PYQuanPin"></param>
        /// <param name="RemarkName"></param>
        /// <param name="RemarkPYQuanPin"></param>
        /// <param name="Sex"></param>
        /// <param name="Signature"></param>
        /// <returns></returns>
        public static bool AddchartLog(string wxuin, string UserName, string City,  string HeadImgUrl, string NickName, string Province, string PYQuanPin, string RemarkName, string RemarkPYQuanPin, string Sex, string Signature)
        {
            string sql = string.Format(@"
INsert INto WXchartLog (wxuin,UserName,City,HeadImgUrl,NickName,Province,PYQuanPin,RemarkName,RemarkPYQuanPin,Sex,Signature)
VALUES('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}')", wxuin, UserName, City, HeadImgUrl, NickName, Province, PYQuanPin, RemarkName, RemarkPYQuanPin, Sex, Signature);
            return DbHelperSQL.ExecuteSql(sql) > 0;
        }
        /// <summary>
        ///  保存接收消息
        /// </summary>
        /// <param name="friendUser"></param>
        /// <param name="Name"></param>
        /// <param name="msg"></param>
        /// <param name="CreateTime"></param>
        /// <returns></returns>
        public static bool AddWxChatcontent_formMsg(string wxuin,string chartUser,string friendUser,string Name, string msg, string CreateTime)
        {
            string sql = string.Format(@"
insert into WxChatcontent (wxuin,chartUser,formname,toname,Chatcontent,CreateTime)
values('{0}','{1}','{2}','{3}','{4}','{5}')", wxuin, chartUser,friendUser, Name, msg, CreateTime);
            return DbHelperSQL.ExecuteSql(sql) > 0;
        }
        /// <summary>
        ///  保存发送消息
        /// </summary>
        /// <param name="name"></param>
        /// <param name="friendUser"></param>
        /// <param name="msg"></param>
        /// <param name="CreateTime"></param>
        /// <returns></returns>
        public static bool AddWxchatcontent_sedMsg(string wxuin,string chartUser, string name, string friendUser, string msg, string CreateTime)
        {
            string sql = string.Format(@"
insert into WxChatcontent (wxuin,chartUser,formname,toname,Chatcontent,CreateTime)
values('{0}','{1}','{2}','{3}','{4}','{5}')", wxuin,chartUser, name, friendUser,msg, CreateTime);
            return DbHelperSQL.ExecuteSql(sql) > 0;
        }
    }
}
