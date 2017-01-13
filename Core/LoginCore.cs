using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Net;
using YUNkefu.Core.Entity;
using YUNkefu.Core;
using YUNkefu.Http;

namespace YUNkefu.Core
{
    public class LoginCore
    {
        private static Dictionary<string, Dictionary<string, string>> SyncKeyDic = new Dictionary<string, Dictionary<string, string>>();
        private static Dictionary<string, PassTicketEntity> _passticket_dic = new Dictionary<string, PassTicketEntity>();

        public static void AddPassTicket(string uin, PassTicketEntity entity)
        {
            //序列化登录passticket
            WxSerializable s = new WxSerializable(uin, EnumContainer.SerializType.pass_ticket);
            s.Serializable(entity);
            if (_passticket_dic.ContainsKey(uin))
                _passticket_dic.Remove(uin);
            _passticket_dic.Add(uin, entity);
        }
        public static PassTicketEntity GetPassTicket(string uin)
        {
            if (string.IsNullOrEmpty(uin))
                uin = string.Empty;
            if (_passticket_dic.ContainsKey(uin))
            {
                return _passticket_dic[uin];
            }
            else
            {
                WxSerializable s = new WxSerializable(uin, EnumContainer.SerializType.pass_ticket);
                _passticket_dic.Add(uin, (PassTicketEntity)s.DeSerializable());
                return (PassTicketEntity)s.DeSerializable();
            }
        }
        public static int GetPassTicketCount()
        {
            return 0;
        }
        public static void AddSyncKey(string uin, Dictionary<string, string> dic)
        {
            if (SyncKeyDic.ContainsKey(uin))
                SyncKeyDic.Remove(uin);
            SyncKeyDic.Add(uin, dic);
        }
        public static Dictionary<string, string> GetSyncKey(string uin)
        {
            if (SyncKeyDic.ContainsKey(uin))
                return SyncKeyDic[uin];
            else
                return null;
        }
        public static void AddOnLineUin(List<string> uinArray)
        {
            WxSerializable s = new WxSerializable("uin", EnumContainer.SerializType.user_online);
            s.Serializable(uinArray);
        }
        public static List<string> GetOnLineUin()
        {
            WxSerializable s = new WxSerializable("uin", EnumContainer.SerializType.user_online);
            if (s != null)
            {
                var obj = s.DeSerializable();
                if (obj != null)
                {
                    return (List<string>)obj;
                }
            }
            return new List<string>();
        }

        public static void InitCookie(string uin)
        {
            WxSerializable s = new WxSerializable(uin, EnumContainer.SerializType.cookie);
            var cookies_dic = (Dictionary<string, CookieContainer>)s.DeSerializable();
            HttpService.CookiesContainerDic = cookies_dic;

        }
    }
}
