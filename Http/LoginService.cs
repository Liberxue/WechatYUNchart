using System;
using System.Text;
using System.Drawing;
using System.IO;
using YUNkefu.Core;
using System.Net;
using YUNkefu.Core.Entity;

namespace YUNkefu.Http
{
    public class LoginService
    {
        private static string _session_id = string.Empty;
        /// <summary>
        /// 获取登录二维码
        /// </summary>
        /// <returns></returns>
        public Image GetQRCode()
        {
            byte[] bytes = HttpService.SendGetRequest(Constant._session_id_url, "");
            _session_id = Encoding.UTF8.GetString(bytes).Split(new string[] { "\"" }, StringSplitOptions.None)[1];
            bytes = HttpService.SendGetRequest(Constant._qrcode_url + _session_id, "");
            return Image.FromStream(new MemoryStream(bytes));
        }
        /// <summary>
        /// 解析url获取qr登录地址
        /// </summary>
        /// <returns></returns>
        public string GetQRCodeurl()
        {
            byte[] bytes = HttpService.SendGetRequest(Constant._session_id_url, "");
            _session_id = Encoding.UTF8.GetString(bytes).Split(new string[] { "\"" }, StringSplitOptions.None)[1];
            string wxQRurl = "";
            wxQRurl = Constant._Aloginqrcode_url + _session_id;
            return wxQRurl;
        }

        /// <summary>
        /// 登录扫描检测
        /// </summary>
        /// <returns></returns>
        public object LoginCheck()
        {
            if (_session_id == null)
            {
                return null;
            }
            byte[] bytes = HttpService.SendGetRequest(Constant._login_check_url + _session_id, "");
            string login_result = Encoding.UTF8.GetString(bytes);
            if (login_result.Contains("=201")) //已扫描 未登录
            {
                string base64_image = login_result.Split(new string[] { "\'" }, StringSplitOptions.None)[1].Split(',')[1];
                byte[] base64_image_bytes = Convert.FromBase64String(base64_image);
                MemoryStream memoryStream = new MemoryStream(base64_image_bytes, 0, base64_image_bytes.Length);
                memoryStream.Write(base64_image_bytes, 0, base64_image_bytes.Length);
                //转成图片
                return Image.FromStream(memoryStream);
            }
            else if (login_result.Contains("=200"))  //已扫描 已登录
            {
                string login_redirect_url = login_result.Split(new string[] { "\"" }, StringSplitOptions.None)[1];
                return login_redirect_url;
            }
            else
            {
                return null; 
            }
        }

        /// <summary>
        /// 获取sid uid   结果存放在cookies中
        /// </summary>
        public string GetSidUid(string login_redirect)
        {
            try
            {
                CookieContainer cookieContainer = new CookieContainer();
                byte[] bytes = HttpService.SendGetRequest(login_redirect + "&fun=new&version=v2&lang=zh_CN", ref cookieContainer);
                string pass_ticket = Encoding.UTF8.GetString(bytes);
                string url = login_redirect;
                Uri uri = new Uri(url);
                string WXUser_url = (uri.Host);
                string pass_Ticket = pass_ticket.Split(new string[] { "pass_ticket" }, StringSplitOptions.None)[1].TrimStart('>').TrimEnd('<', '/');
                string sKey = pass_ticket.Split(new string[] { "skey" }, StringSplitOptions.None)[1].TrimStart('>').TrimEnd('<', '/');
                string wxSid = pass_ticket.Split(new string[] { "wxsid" }, StringSplitOptions.None)[1].TrimStart('>').TrimEnd('<', '/');
                string wxUin = pass_ticket.Split(new string[] { "wxuin" }, StringSplitOptions.None)[1].TrimStart('>').TrimEnd('<', '/');

                var passticketEntity = new PassTicketEntity()
                {
                    PassTicket = pass_Ticket,
                    SKey = sKey,
                    WxSid = wxSid,
                    WxUin = wxUin,
                    WXUser_url = WXUser_url
                };

                LoginCore.AddPassTicket(wxUin, passticketEntity);
                if (HttpService.CookiesContainerDic.ContainsKey(wxUin))
                {
                    HttpService.CookiesContainerDic.Remove(wxUin);
                }
                WxSerializable s = new WxSerializable(wxUin, EnumContainer.SerializType.cookie);
                HttpService.CookiesContainerDic.Add(wxUin, cookieContainer);
                s.Serializable(HttpService.CookiesContainerDic);
                return wxUin;
            }
            catch {
                return null;
            }
            
        }
    }
}
