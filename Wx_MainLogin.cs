using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using YUNkefu;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using YUNkefu.Core;
using YUNkefu.Wxlogin;

namespace YUNkefu
{
    public partial class Wx_MainLogin : SkinMain
    {
        Xmloperation xmlop;
        public Xmloperation Xmlop
        {
            get { return xmlop; }
            set { xmlop = value; }
        }
        public Wx_MainLogin(Xmloperation xmlop)
            : this()
        {
            this.xmlop = xmlop;
        }
        List<User> UsersList;
        public bool flag = false;  //用户登录的标记
        int CheckCode = 0; //用户是否敲击回车以便检测编号（0：无，1：有）
        public Wx_MainLogin()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 系统启动加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Main_Login_Load(object sender, System.EventArgs e)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(Environment.CurrentDirectory + '\\' + "ClentFile.xml");
            string path = "Config/Version";
            XmlNode node = doc.SelectSingleNode(path);
            string text = node.InnerText;//xml.
            vs.Text = "当前云客服版本号" + text;
            if (File.Exists("User_013.dll"))
            {
                FileStream fs = new FileStream("User_013.dll", FileMode.Open, FileAccess.Read);
                BinaryFormatter bf = new BinaryFormatter();  //创建一个序列化和反序列化类的对象  
                UsersList = (List<User>)bf.Deserialize(fs);  //调用反序列化方法，从文件User_013.dll中读取对象信息  
                for (int i = 0; i < UsersList.Count; i++)//将集合中的用户登录ID读取到下拉框中  
                {
                    if (i == 0 && UsersList[i].LoingPassword != "")  //如果第一个用户已经记住密码。  
                    {
                        chkMemoryPwd.Checked = true;
                        txtPwd.Text = UsersList[i].LoingPassword;  //给密码框赋值  
                    }
                    tbCode.Items.Add(UsersList[i].LoginName.ToString());
                    txtPwd.Text = UsersList[i].LoingPassword;  //给密码框赋值  
                }
                fs.Close();   //关闭文件流  
                tbCode.SelectedIndex = 0;   //默认下拉框选中为第一项  
            }
            else
            {
                UsersList = new List<User>();
            }
        }

        private void btDL_Click(object sender, System.EventArgs e)
        {
            if (CheckCode == 0)  //直接点击按钮登录，此时需要检测编号是否存在以及当前是否在使用！
            {
                if (tbCode.Text == "")
                {
                    msgss.Text = "提示：呃...没帐号怎么登录！";
                    tbCode.Focus();
                }
                else
                {
                    string loginName = tbCode.Text.Trim();  //将下拉框的登录名先保存在变量中  
                    for (int i = 0; i < tbCode.Items.Count; i++)  //遍历下拉框中的所有元素  
                    {
                        if (tbCode.Items[i].ToString() == loginName)
                        {
                            tbCode.Items.RemoveAt(i);  //如果当前登录用户在下拉列表中已经存在，则将其移除  
                            break;
                        }
                    }

                    for (int i = 0; i < UsersList.Count; i++)    //遍历用户集合中的所有元素  
                    {
                        if (UsersList[i].LoginName == loginName)  //如果当前登录用户在用户集合中已经存在，则将其移除  
                        {
                            UsersList.RemoveAt(i);
                            break;
                        }
                    }

                    tbCode.Items.Insert(0, loginName);  //每次都将最后一个登录的用户放插入到第一位  
                    User user;
                    if (chkMemoryPwd.Checked == true)    //如果用户要记住密码  
                    {
                        string newPwd = MyEncrypt.EncryptDES(txtPwd.Text.Trim());  //如果用户要求记住密码则对该密码进行加密
                        user = new User(loginName, newPwd);  //将登录ID和密码一起插入到用户集合中  
                        UsersList.Add(user);
                    }
                    {
                        //  user = new User(loginName, "");  //否则只插入一个用户名到用户集合中，密码设为空  
                        // UsersList.Insert(0, user);   //在用户集合中插入一个用户  
                        tbCode.SelectedIndex = 0;   //让下拉框选中集合中的第一个  
                    }
                    StringWriter sw = new StringWriter();
                    JsonWriter writer = new JsonTextWriter(sw);
                    writer.WriteStartObject();
                    writer.WritePropertyName("org_id");
                    writer.WriteValue("1");
                    writer.WritePropertyName("dev_id");
                    writer.WriteValue("1");
                    writer.WritePropertyName("id");
                    writer.WriteValue(tbCode.Text);
                    writer.WritePropertyName("passwd");
                    writer.WriteValue(txtPwd.Text);
                    writer.WriteEndObject();
                    writer.Flush();
                    string jsonText = sw.GetStringBuilder().ToString();
                    ASCIIEncoding encoding = new ASCIIEncoding();
                    string postData = "" + sw.GetStringBuilder().ToString();
                    byte[] data = encoding.GetBytes(postData);
                    HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(Yihuyun.Yihuyun_loginurl);
                    myRequest.Method = "POST";
                    myRequest.ContentType = "application/x-www-form-urlencoded";
                    myRequest.ContentLength = data.Length;
                    Stream newStream = myRequest.GetRequestStream();
                    newStream.Write(data, 0, data.Length);
                    newStream.Close();
                    HttpWebResponse myResponse = (HttpWebResponse)myRequest.GetResponse();
                    StreamReader reader = new StreamReader(myResponse.GetResponseStream(), Encoding.UTF8);
                    try
                    {
                        string content = reader.ReadToEnd();
                        JObject getlogin = (JObject)JsonConvert.DeserializeObject(content);
                        string msg = getlogin["msg"].ToString();
                        if (msg == "success")
                        {
                            JObject jo = (JObject)JsonConvert.DeserializeObject(content);
                            string token = jo["token"].ToString();
                            texttoken.Text = token;
                            this.Hide();
                            Wx_MainFrom dig = new Wx_MainFrom();
                            Program.token = token;
                            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://139.224.61.240:7655/api/v2/org?token" + texttoken.Text);
                            request.Method = "GET";
                            request.ContentType = "text/html;charset=UTF-8";
                            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                            Stream myResponseStream = response.GetResponseStream();
                            StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.UTF8);
                            string retString = myStreamReader.ReadToEnd();
                            JObject getname = (JObject)JsonConvert.DeserializeObject(content);
                            string real_name = getname["real_name"].ToString();
                            Program.opreal_name = real_name;
                            FileStream fs = new FileStream("User_013.dll", FileMode.Create, FileAccess.Write);  //创建一个文件流对象  
                            BinaryFormatter bf = new BinaryFormatter();  //创建一个序列化和反序列化对象  
                            bf.Serialize(fs, UsersList);   //要先将User类先设为可以序列化(即在类的前面加[Serializable])，将用户集合信息写入到硬盘中。
                            fs.Close();   //关闭文件流  
                            myStreamReader.Close();
                            myResponseStream.Close();
                            dig.ShowDialog();
                        }
                        else
                        {
                            msgss.Text = "登录 失败  请检查用户名密码！";
                            tbCode.Focus();
                        }


                    }
                    catch (Exception logex)
                    {
                        msgss.Text = "登录失败网络异常！";
                        //写日志
                        Tools.WriteLog(msgss.Text + logex.ToString());
                    }
                    finally
                    {
                        this.Close();
                    }
                }
            }
        }
        private void txtPwd_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                btDL_Click(sender, e);
            }
        }
        private void tbCode_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (UsersList[tbCode.SelectedIndex].LoingPassword != "") //如果用户密码不为空时  
            {
                //把用户ID所对应的密码赋给密码框
                tbCode.Text = UsersList[tbCode.SelectedIndex].LoingPassword.ToString();
                chkMemoryPwd.Checked = true;
            }
            else
            {
                tbCode.Text = "";  //如果用户密码本身就为空   
                chkMemoryPwd.Checked = false;
            }
        }

        /// <summary>
        ///最小化动画事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pic_Mxi_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
        private void pic_Mxi_MouseDown(object sender, MouseEventArgs e)
        {
            pic_Mxi.Image = Properties.Resources.mxi_hover;
            pic_Mxi.Location = new Point(pic_Mxi.Location.X + 1, pic_Mxi.Location.Y + 1);
        }

        private void pic_Mxi_MouseEnter(object sender, EventArgs e)
        {
            pic_Mxi.Image = Properties.Resources.mxi_hover;
        }

        private void pic_Mxi_MouseLeave(object sender, EventArgs e)
        {
            pic_Mxi.Image = Properties.Resources.mxi;
        }
        /// <summary>
        /// 关闭事件动画
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void loginclose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void loginclose_MouseDown(object sender, MouseEventArgs e)
        {
            loginclose.Image = Properties.Resources.close_hover;
        }

        private void loginclose_MouseEnter(object sender, EventArgs e)
        {
            loginclose.Image = Properties.Resources.close_hover;
        }

        private void loginclose_MouseLeave(object sender, EventArgs e)
        {
            loginclose.Image = Properties.Resources.close;
        }
    }
}
