using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Runtime.InteropServices;
using System.IO;
using System.Xml;
using System.Diagnostics;
using System.Windows.Forms;
using System.Net.NetworkInformation;

namespace YUNkefu.UpdateClass
{
    public class VersionHelper
    {
        /// <summary>
        /// 获取需要更新的文件
        /// </summary>
        /// <returns></returns>
        public ServerInfor GetUpdateFile(string path)
        {
            ServerInfor Sinfo = new ServerInfor();
            try
            {
                INIClass ini = new INIClass(path);
                if (!ini.ExistINIFile())
                    return null;
                Sinfo.Application = ini.IniReadValue("UpdateLogin", "Application");
                Sinfo.UpdateName = ini.IniReadValue("UpdateLogin", "UpdateName");
                Sinfo.OtherInfo = ini.IniReadValue("UpdateLogin", "OtherInfo");
                Sinfo.IcoName = ini.IniReadValue("UpdateLogin", "IcoName");
                Sinfo.DeskTopName = ini.IniReadValue("UpdateLogin", "DeskTopName");
                if (isConnected())
                {
                    string xmlPath = System.AppDomain.CurrentDomain.BaseDirectory + "ClentFile.xml";
                    XmlDocument localDoc;
                    if (!File.Exists(xmlPath))
                    {
                        localDoc = new XmlDocument();
                        //创建头文件声明
                        XmlDeclaration xmldecl = localDoc.CreateXmlDeclaration("1.0", "utf-8", null);
                        //Add the new node to the document.
                        XmlElement root1 = localDoc.DocumentElement;
                        localDoc.InsertBefore(xmldecl, root1);
                        XmlElement Node = localDoc.CreateElement("Config");//创建一个Config节点          
                        localDoc.AppendChild(Node);
                        XmlElement Node1 = localDoc.CreateElement("Version");
                        Node.AppendChild(Node1);
                        XmlElement Node2 = localDoc.CreateElement("Remark");
                        Node.AppendChild(Node2);
                        XmlElement Node3 = localDoc.CreateElement("Files");
                        Node.AppendChild(Node3);
                        localDoc.Save(xmlPath);
                    }
                    localDoc = new XmlDocument();
                    localDoc.Load(xmlPath);
                    //先获取本地的文件列表
                    var localNodes = XMLHelper.GetXmlNodeListByXpath(localDoc, "/Config/Files/File");
                    List<UpdateFileInfo> localFile = new List<UpdateFileInfo>();
                    foreach (XmlNode item in localNodes)
                    {
                        var name = XMLHelper.GetNodeAttributeValue(item, "Name");
                        var version = XMLHelper.GetNodeAttributeValue(item, "Version");
                        localFile.Add(new UpdateFileInfo() { Name = name, Version = version });
                    }
                    //再获取服务器的文件列表
                    List<UpdateFileInfo> serverFile = new List<UpdateFileInfo>();
                    XmlDocument serverDoc = new XmlDocument();
                    //先以外网地址匹配需要更新的文件
                    string serConf = ini.IniReadValue("UpdateLogin", "ListURL2");
                    serverDoc.Load(serConf);
                    var serverNodes = XMLHelper.GetXmlNodeListByXpath(serverDoc, "/Config/Files/File");
                    foreach (XmlNode item in serverNodes)
                    {
                        var name = XMLHelper.GetNodeAttributeValue(item, "Name");
                        var version = XMLHelper.GetNodeAttributeValue(item, "Version");
                        var size = XMLHelper.GetNodeAttributeValue(item, "Size");
                        serverFile.Add(new UpdateFileInfo() { Name = name, Version = version, Size = size });
                    }
                    foreach (var item in serverFile)
                    {
                        var localItem = localFile.Find(delegate(UpdateFileInfo file)
                        {
                            return file.Name == item.Name;
                        });
                        if (localItem != null)
                        {
                            if (localItem.Version != item.Version)
                                Sinfo.DownloadFileList.Add(item);
                        }
                        else
                            Sinfo.DownloadFileList.Add(item);
                    }
                    if (Sinfo.DownloadFileList.Count > 0)
                    {
                        Sinfo.INI = ini;
                        Sinfo.Version = XMLHelper.GetXmlNodeValueByXpath(serverDoc, "/Config/Version");
                        Sinfo.Remark = XMLHelper.GetXmlNodeValueByXpath(serverDoc, "/Config/Remark");
                        //判断是否能通过内网下载，不能则以外网下载
                        string url = ini.IniReadValue("UpdateLogin", "ListURL");
                        if (url != serConf)
                        {
                            if (isExists(url))
                                serConf = url;
                        }
                        Sinfo.Server = serConf.Substring(0, serConf.LastIndexOf('/')) + "/";
                        if (ini.IniReadValue("UpdateLogin", "QuickLoad") == "1")
                            Sinfo._QuckLoad = true;
                        else
                            Sinfo._QuckLoad = false;
                    }
                }
            }
            catch
            {
                return Sinfo;
            }
            return Sinfo;
        }

        #region 检查网络状态
        [DllImport("wininet.dll")]
        extern static bool InternetGetConnectedState(out int connectionDescription, int reservedValue);
        /// <summary>
        /// 检测网络状态
        /// </summary>
        bool isConnected()
        {
            int I = 0;
            bool state = InternetGetConnectedState(out I, 0);
            return state;
        }

        private bool isExists(string url)
        {
            HttpWebRequest req = null;
            HttpWebResponse res = null;
            try
            {
                req = (HttpWebRequest)WebRequest.Create(url);
                req.Method = "HEAD";
                req.Timeout = 10000;
                res = (HttpWebResponse)req.GetResponse();
                return (res.StatusCode == HttpStatusCode.OK);
            }
            catch
            {
                return false;
            }
            finally
            {
                if (res != null)
                {
                    res.Close();
                    res = null;
                }
                if (req != null)
                {
                    req.Abort();
                    req = null;
                }
            }
        }
        #endregion
    }
}
