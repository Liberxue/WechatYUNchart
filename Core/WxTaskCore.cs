using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using YUNkefu.Http;
using YUNkefu.Core.Entity;
using YUNkefu.Core.Dal;
using System.Data;

namespace YUNkefu.Core
{
    /// <summary>
    /// 微信主任务
    /// </summary>
    public class WxTaskCore
    {
        private string Uin;
        private string Sid;
        private string robotID;
        /// <summary>
        /// 当前微信用户
        /// </summary>
        public WXUser user { get; set; }

        public WxTaskCore(string sid, string uin,string robotID)
        {
            this.Sid = sid;
            this.Uin = uin;
            this.robotID = robotID;
        }
        
        /// <summary>
        /// 同步微信消息
        /// </summary>
        public void ReviceMsg()
        {
            try
            {
                string sync_flag = "";
                JObject sync_result;
                WXService wxs = new WXService();
                wxs.Sid = this.Sid;
                wxs.Uin = this.Uin;
                while (true)
                {
                    sync_flag = wxs.WxSyncCheck();  //同步检查
                    if (sync_flag == null)
                    {
                        continue;
                    }
                    else   //有消息
                    {
                        sync_result = wxs.WxSync();  //进行同步
                        if (sync_result != null)
                        {
                            if (sync_result["AddMsgCount"] != null && sync_result["AddMsgCount"].ToString() != "0")
                            {
                                foreach (JObject m in sync_result["AddMsgList"])
                                {
                                    string from = m["FromUserName"].ToString();
                                    string to = m["ToUserName"].ToString();
                                    string content = m["Content"].ToString();
                                    string type = m["MsgType"].ToString();
                                    var msg = new WXMsg();
                                    msg.Sid = Sid;
                                    msg.Uin = Uin;
                                    msg.From = from;
                                    msg.Msg = content; //只接受文本消息
                                    msg.Readed = false;
                                    msg.Time = DateTime.Now;
                                    msg.To = to;
                                    msg.Type = int.Parse(type);
                                    if (msg.Type == 51)
                                    {
                                        continue;  //过滤系统消息
                                    }
                                    if (msg.Type == 10000)
                                    {
                                        string s = sync_result.ToString();
                                    }
                                    if (msg.From != user.UserName)  //接收别人消息
                                    {
                                        OnRevice.Invoke(msg);
                                    }                                    
                                }
                            }

                            //判断是否有修改联系人信息
                            if (sync_result["ModContactCount"] != null && sync_result["ModContactCount"].ToString() != "0")
                            {
                                OnModifyContact.Invoke(null, Sid, Uin);
                            }
                        }
                    }
                    System.Threading.Thread.Sleep(5000);
                }
            }
            catch (Exception ex)
            {
                var msg = new WXMsg();
                msg.Msg = ex.ToString();
                OnRevice.Invoke(msg);

                Tools.WriteLog(ex.ToString());
            }
        }

        /// <summary>
        /// 自动发送消息
        /// </summary>
        public void AutoSendMsg()
        {
            try
            {
                while (true)
                {
                    var dt = WeChatAdvertisementDal.QueryTask(this.robotID);
                    string msgText = string.Empty;
                    var _taskDic = new Dictionary<int, string>();
                    foreach (DataRow dr in dt.Rows)
                    {
                        var adverId = 0;
                        adverId = int.Parse(dr["AdverId"].ToString());
                        var lastSendTime_str = dr["LastSendTime"].ToString();
                        var last = DateTime.Now.AddDays(-2);
                        if (!string.IsNullOrEmpty(lastSendTime_str))
                        {
                            last = Convert.ToDateTime(lastSendTime_str);
                        }
                        if (int.Parse(dr["AdverCategory"].ToString()) == (int)EnumContainer.AdverCategoryEnum.公告语)
                        {
                            switch (int.Parse(dr["SendMode"].ToString()))
                            {
                                case (int)EnumContainer.SendModeEnum.每隔多少分钟:
                                    if (DateTime.Now.Subtract(last).TotalMinutes > int.Parse(dr["SendModeParas"].ToString()))
                                    {
                                        msgText = dr["AdverContent"].ToString();
                                        if (!_taskDic.ContainsKey(adverId))
                                            _taskDic.Add(adverId, msgText);
                                    }
                                    break;
                                case (int)EnumContainer.SendModeEnum.每天某一时刻:
                                    var tempTime = Convert.ToDateTime(string.Format("{0} {1}", DateTime.Now.ToString("yyyy-MM-dd"), dr["SendModeParas"]));
                                    var s_i = tempTime.Subtract(DateTime.Now).TotalSeconds;
                                    if (s_i <= 0 && s_i > -10)
                                    {
                                        if (DateTime.Now.Subtract(last).TotalMinutes > 2)
                                        {
                                            msgText = dr["AdverContent"].ToString();
                                            if (!_taskDic.ContainsKey(adverId))
                                                _taskDic.Add(adverId, msgText);
                                        }
                                    }
                                    break;
                                case (int)EnumContainer.SendModeEnum.指定具体时间:
                                    var temp = Convert.ToDateTime(dr["SendModeParas"]);
                                    var s_i_i = temp.Subtract(DateTime.Now).TotalSeconds;
                                    if (s_i_i <= 0 && s_i_i > -10)
                                    {

                                        if (DateTime.Now.Subtract(last).TotalMinutes > 2)
                                        {
                                            msgText = dr["AdverContent"].ToString();
                                            if (!_taskDic.ContainsKey(adverId))
                                                _taskDic.Add(adverId, msgText);
                                        }
                                    }
                                    break;
                            }
                        }
                    }
                    System.Threading.Thread.Sleep(1000);
                    foreach (var k in _taskDic)
                    {
                        //通知发送
                        WeChatAdvertisementDal.UpdateLastSendTime(k.Key.ToString());
                        if (OnNotifySend != null)
                        {
                            if (!string.IsNullOrEmpty(k.Value))
                            {
                                WxContact contact = new WxContact(this.Uin);
                                var gUserNames = contact.GetGroupUserNames();
                                var args = new NotifyArgs()
                                {
                                    Sid = this.Sid,
                                    WxUin = this.Uin,
                                    MsgContext = k.Value,
                                    MyUserName = this.user.UserName,
                                    GroupUserName = gUserNames
                                };
                                OnNotifySend(args);
                                System.Threading.Thread.Sleep(1500);
                            }
                        }
                    }
                    _taskDic.Clear();
                }
            }
            catch (Exception ex)
            {
                //写日志
                Tools.WriteLog(ex.ToString());
            }

        }

        public delegate void Revice(WXMsg msg);
        public event Revice OnRevice;

        public delegate void ModifyContact(List<WXUser> users, string sid, string uid);
        public event ModifyContact OnModifyContact;

        public delegate void NotifySend(NotifyArgs args);
        public event NotifySend OnNotifySend;
    }
}
