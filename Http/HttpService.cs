using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;
using System.Windows.Forms;

namespace YUNkefu.Http
{
    public class HttpService
    {
        /// <summary>
        /// cookie容器
        /// </summary>
        public static Dictionary<string, CookieContainer> CookiesContainerDic = new Dictionary<string, CookieContainer>();


        public static byte[] SendGetRequest(string url, string uid)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "get";

                if (!string.IsNullOrEmpty(uid))
                {
                    CookieContainer _cookiesContainer = CookiesContainerDic[uid];

                    if (_cookiesContainer == null)
                    {
                        _cookiesContainer = new CookieContainer();
                    }
                    if (!CookiesContainerDic.ContainsKey(uid))
                    {
                        CookiesContainerDic.Add(uid, _cookiesContainer);
                    }

                    request.CookieContainer = _cookiesContainer;  //启用cookie
                }

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream response_stream = response.GetResponseStream();

                int count = (int)response.ContentLength;
                int offset = 0;
                byte[] buf = new byte[count];
                while (count > 0)  //读取返回数据
                {
                    int n = response_stream.Read(buf, offset, count);
                    if (n == 0) break;
                    count -= n;
                    offset += n;
                }
                return buf;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }

        /// <summary>
        /// get请求，并返回cookie
        /// </summary>
        /// <param name="url"></param>
        /// <param name="cookieContainer"></param>
        /// <returns></returns>
        public static byte[] SendGetRequest(string url, ref CookieContainer cookieContainer)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "get";
                request.CookieContainer = cookieContainer;  //启用cookie               

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream response_stream = response.GetResponseStream();

                int count = (int)response.ContentLength;
                int offset = 0;
                byte[] buf = new byte[count];
                while (count > 0)  //读取返回数据
                {
                    int n = response_stream.Read(buf, offset, count);
                    if (n == 0) break;
                    count -= n;
                    offset += n;
                }
                return buf;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }

        public static byte[] SendPostRequest(string url, string body, string uid)
        {
            try
            {
                byte[] request_body = Encoding.UTF8.GetBytes(body);

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "post";
                request.ContentLength = request_body.Length;

                Stream request_stream = request.GetRequestStream();

                request_stream.Write(request_body, 0, request_body.Length);

                if (!string.IsNullOrEmpty(uid))
                {
                    CookieContainer _cookiesContainer = null;
                    if (CookiesContainerDic.ContainsKey(uid))
                    {
                        _cookiesContainer = CookiesContainerDic[uid];
                    }

                    if (_cookiesContainer == null)
                    {
                        _cookiesContainer = new CookieContainer();
                    }
                    if (!CookiesContainerDic.ContainsKey(uid))
                    {
                        CookiesContainerDic.Add(uid, _cookiesContainer);
                    }

                    request.CookieContainer = _cookiesContainer;  //启用cookie
                }


                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream response_stream = response.GetResponseStream();

                int count = (int)response.ContentLength;
                int offset = 0;
                byte[] buf = new byte[count];
                while (count > 0)  //读取返回数据
                {
                    int n = response_stream.Read(buf, offset, count);
                    if (n == 0) break;
                    count -= n;
                    offset += n;
                }
                return buf;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>  
        /// 获取时间戳  
        /// </summary>  
        /// <returns></returns>  
        public static string GetTimeStamp()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalSeconds).ToString();
        }
        /// <summary>
        /// Http下载文件
        /// </summary>
        public static string HttpDownloadFile(string url, string uid, string path)
        {
            try
            {
                byte[] bytes = SendGetRequest(url, uid);

                using (StreamWriter sw = new StreamWriter(path, false))
                {
                    sw.Write(bytes);
                }

                File.WriteAllBytes(path, bytes);
                //////创建本地文件写入流
                //Stream stream = new FileStream(path, FileMode.Create);
                //stream.Write(bytes, 0, bytes.Length);
                return path;
            }
            catch
            {
                return null;
            }
        }
    }
}
