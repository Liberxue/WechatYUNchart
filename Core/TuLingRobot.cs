using System;

using System.Text;
using System.IO;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace YUNkefu.Core
{
    /// <summary>
    /// 图灵机器人
    /// </summary>
    public class TuLingRobot
    {
        private static string apiKey = "eb9b18d5d3c744ed81aa260ddad630ad";
        private static string secret = "2781964098965f0d";
        private static string apiurl = "http://www.tuling123.com/openapi/api"; //接口地址
        private static string userName = "013机器人";


        public static string GetTextReply(string text)
        {
            var INFO = Encoding.UTF8.GetString(Encoding.UTF8.GetBytes(text));
            var getURl = "http://www.tuling123.com/openapi/api?key=" + apiKey + "&info=" + INFO;
            Uri uri = new Uri(getURl);
            HttpWebRequest getUrl = WebRequest.Create(uri) as HttpWebRequest;
            getUrl.Method = "GET";
            HttpWebResponse response = getUrl.GetResponse() as HttpWebResponse;
            Stream respStream = response.GetResponseStream();
            StreamReader stream = new StreamReader(respStream, Encoding.UTF8);
            string respStr = stream.ReadToEnd();
            stream.Close();

            JObject init_result = JsonConvert.DeserializeObject(respStr) as JObject;
            var code=init_result["code"].ToString();
            if (code.Equals("100000"))
            {
                return init_result["text"].ToString();
            }
            else if (code.Equals("200000"))
            {
                var _t=init_result["text"].ToString();
                var _url = init_result["url"].ToString();
                return _t + "\r\n" + _url;
            }
            else
            {
                return "";
            }            
        }

    }
}
