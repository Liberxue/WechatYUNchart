using System;
using System.Collections.Generic;
using System.Text;
using YUNkefu.Core;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Drawing;
using System.IO;

namespace YUNkefu.Http
{
    /// <summary>
    ///  微信主要业务逻辑服务类
    /// </summary>
    public class WXService
    {
        public string Uin { get; set; }
        public string Sid { get; set; }
        public string robotID { get; set; }

        /// <summary>
        /// 微信初始化
        /// </summary>
        /// <returns></returns>
        public JObject WxInit()
        {
            string init_json = "{{\"BaseRequest\":{{\"Uin\":\"{0}\",\"Sid\":\"{1}\",\"Skey\":\"{2}\",\"DeviceID\":\"e1615250492\"}}}}";

            if (Uin != null && Sid != null)
            {
                string pass_ticket = LoginCore.GetPassTicket(Uin).PassTicket;//这个位置过来了
                string skey = LoginCore.GetPassTicket(Uin).SKey;
                init_json = string.Format(init_json, Uin, Sid, skey);
                byte[] bytes = HttpService.SendPostRequest(Constant._init_url + "&pass_ticket=" + pass_ticket, init_json, Uin);
                //byte[] bytes = HttpService.SendPostRequest( WXUser_url+ "&pass_ticket=" + pass_ticket, init_json, Uin);
                string init_str = Encoding.UTF8.GetString(bytes);
                JObject init_result = JsonConvert.DeserializeObject(init_str) as JObject;
                return init_result;
            }
            else
            {
                return null;
            }
        }

        public Image GetHeadImg(string usename)
        {
            //if (bytes.Length == 0)
            //{
            //    return null;
            //}
            //return Image.FromStream(new MemoryStream(bytes));
            try
            {
                byte[] bytes = HttpService.SendGetRequest(Constant._getheadimg_url + usename, "");
                return Image.FromStream(new MemoryStream(bytes));
            }
            catch (Exception)
            {
                return null;
            }
        }
        /// <summary>
        /// 获取头像
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public Image GetIcon(string username, string uin = "")
        {
            try
            {
                byte[] bytes = HttpService.SendGetRequest(Constant._geticon_url + username, uin);
                //if (bytes.Length == 0)
                //{
                //    return null;
                //}
                return Image.FromStream(new MemoryStream(bytes));
            }

            catch(Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// 微信同步检测
        /// </summary>
        /// <returns></returns>
        public string WxSyncCheck()
        {
            string sync_key = "";
            try
            {
                var _syncKey = LoginCore.GetSyncKey(Uin);
                foreach (KeyValuePair<string, string> p in _syncKey)
                {
                    sync_key += p.Key + "_" + p.Value + "%7C";
                }
                sync_key = sync_key.TrimEnd('%', '7', 'C');

                var entity = LoginCore.GetPassTicket(Uin);
                if (Sid != null && Uin != null)
                {
                    var _synccheck_url = string.Format(Constant._synccheck_url, Sid, Uin, sync_key, (long)(DateTime.Now.ToUniversalTime() - new System.DateTime(1970, 1, 1)).TotalMilliseconds, entity.SKey.Replace("@", "%40"), "e1615250492");

                    byte[] bytes = HttpService.SendGetRequest(_synccheck_url + "&_=" + DateTime.Now.Ticks, Uin);
                    if (bytes != null)
                    {
                        return Encoding.UTF8.GetString(bytes);
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                return "";
            }
        }

        /// <summary>
        /// 微信同步
        /// </summary>
        /// <returns></returns> 
        public JObject WxSync()
        {
            var entity = LoginCore.GetPassTicket(Uin);
            string sync_json = "{{\"BaseRequest\" : {{\"DeviceID\":\"e1615250492\",\"Sid\":\"{1}\", \"Skey\":\"{5}\", \"Uin\":\"{0}\"}},\"SyncKey\" : {{\"Count\":{2},\"List\": [{3}]}},\"rr\" :{4}}}";
            string sync_keys = "";
            var _syncKey = LoginCore.GetSyncKey(Uin);
            foreach (KeyValuePair<string, string> p in _syncKey)
            {
                sync_keys += "{\"Key\":" + p.Key + ",\"Val\":" + p.Value + "},";
            }
            sync_keys = sync_keys.TrimEnd(',');
            sync_json = string.Format(sync_json, this.Uin, this.Sid, _syncKey.Count, sync_keys, (long)(DateTime.Now.ToUniversalTime() - new System.DateTime(1970, 1, 1)).TotalMilliseconds, entity.SKey);

            if (this.Sid != null && this.Uin != null)
            {
                byte[] bytes = HttpService.SendPostRequest(Constant._sync_url + this.Sid + "&lang=zh_CN&skey=" + entity.SKey + "&pass_ticket=" + entity.PassTicket, sync_json, this.Uin);
                string sync_str = Encoding.UTF8.GetString(bytes);
                if (sync_str == null)
                {
                    return null;
                }
                JObject sync_resul = JsonConvert.DeserializeObject(sync_str) as JObject;
                // Dictionary<string, string> ss = new Dictionary<string, string>();
                if (sync_resul["SyncKey"]["Count"].ToString() != "1")
                {
                    _syncKey.Clear();
                    foreach (JObject key in sync_resul["SyncKey"]["List"])
                    {
                        _syncKey.Add(key["Key"].ToString(), key["Val"].ToString());
                    }
                }
                return sync_resul;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="type"></param>
        public void SendMsg(string msg, string from, string to, int type, string Uin, string Sid)
        {
            string msg_json = "{{" +
            "\"BaseRequest\":{{" +
                "\"DeviceID\" : \"e441551176\"," +
                "\"Sid\" : \"{0}\"," +
                "\"Skey\" : \"{6}\"," +
                "\"Uin\" : \"{1}\"" +
            "}}," +
            "\"Msg\" : {{" +
                "\"ClientMsgId\" : {8}," +
                "\"Content\" : \"{2}\"," +
                "\"FromUserName\" : \"{3}\"," +
                "\"LocalID\" : {9}," +
                "\"ToUserName\" : \"{4}\"," +
                "\"Type\" : {5}" +
            "}}," +
            "\"rr\" : {7}" +
            "}}";
            var entity = LoginCore.GetPassTicket(Uin);
            if (Sid != null && Uin != null)
            {
                msg_json = string.Format(msg_json, Sid, Uin, msg, from, to, type, entity.SKey, DateTime.Now.Millisecond, DateTime.Now.Millisecond, DateTime.Now.Millisecond);
                byte[] bytes = HttpService.SendPostRequest(Constant._sendmsg_url + Sid + "&lang=zh_CN&pass_ticket=" + entity.PassTicket, msg_json, Uin);
                string send_result = Encoding.UTF8.GetString(bytes);
            }
            //((Action)delegate()
            //{
            //    //存储发送消息
            //    var b = WxOperateLogDal.AddWxsendmsglog(Uin, from, msg,to,from);
            //}).BeginInvoke(null, null);
        }

        /// <summary>
        /// 获取好友列表
        /// </summary>
        /// <returns></returns>
        public JObject GetContact()
        {
            byte[] bytes = HttpService.SendGetRequest(Constant._getcontact_url, Uin);
            string contact_str = Encoding.UTF8.GetString(bytes);
            return JsonConvert.DeserializeObject(contact_str) as JObject;
        }

        public JObject BatGetContact(List<string> groupUserName)
        {
            var entity = LoginCore.GetPassTicket(Uin);
            var _jstr = string.Empty;
            foreach (var username in groupUserName)
            {
                _jstr += string.Format("{{{{\"UserName\":\"{0}\",\"ChatRoomId\":\"\"}}}},",
                    username, "");
            }
            string json = "{{" +
                "\"BaseRequest\":{{\"Uin\":{0}," +
                "\"Sid\":\"{1}\"," +
                "\"Skey\":\"{2}\"," +
                "\"DeviceID\":\"e017670883684764\"}}," +
                "\"Count\":{3}," +
                "\"List\":[" +
                    _jstr.TrimEnd(',') +
                    "]" +
                "}}";
            try
            {
                json = string.Format(json, Uin, Sid, entity.SKey, groupUserName.Count);
            }
            catch (Exception ex)
            {
                //写日志
                Tools.WriteLog(ex.ToString());
            }

            string url = string.Format(Constant._getbatcontact_url, HttpService.GetTimeStamp(), entity.PassTicket);
            byte[] bytes = HttpService.SendPostRequest(url, json, Uin);
            string contact_str = Encoding.UTF8.GetString(bytes);

            return JsonConvert.DeserializeObject(contact_str) as JObject;
        }
    }
}
