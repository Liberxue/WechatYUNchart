using System;
using System.Collections.Generic;

using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace YUNkefu.Core
{
    /// <summary>
    /// 序列号辅助类
    /// </summary>
    public class WxSerializable
    {
        private string _uin = string.Empty;
        string fileName = string.Empty;
        public WxSerializable(string uin,YUNkefu.Core.Entity.EnumContainer.SerializType type)
        {
            try
            {
                this._uin = uin;
                var dir = System.Environment.CurrentDirectory + "\\data\\" + this._uin;
                if (!System.IO.Directory.Exists(dir))
                {
                    System.IO.Directory.CreateDirectory(dir);
                }
                fileName = dir+"\\" + type.ToString() + ".dat";//文件名称与路径
                if (!File.Exists(fileName))
                {
                    using (var stream = File.Create(fileName))
                    {
                        stream.Close();
                    }
                }
            }
            catch (Exception ex)
            {
               string aa;
            }
        }

        public void Serializable(object obj)
        {
            try
            {
                using (Stream fStream = new FileStream(fileName, FileMode.Open, FileAccess.ReadWrite))
                {
                    BinaryFormatter binFormat = new BinaryFormatter();//创建二进制序列化器
                    binFormat.Serialize(fStream, obj);
                    fStream.Close();
                }
            }
            catch (Exception ex)
            {
                string aa;
            }
        }

        public object DeSerializable()
        {
            try
            {
                using (Stream fStream = new FileStream(fileName, FileMode.Open, FileAccess.ReadWrite))
                {
                    BinaryFormatter binFormat = new BinaryFormatter();//创建二进制序列化器
                    if (fStream.Length > 0)
                    {
                        object obj = binFormat.Deserialize(fStream);//反序列化对象
                        fStream.Close();
                        return obj;
                    }
                    else
                        return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }


    }
}
