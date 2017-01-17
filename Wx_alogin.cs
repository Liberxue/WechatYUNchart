using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Xml;
using YUNkefu.Http;
using YUNkefu.Wxlogin;

namespace YUNkefu
{
    public partial class Wx_alogin : SkinMain
    {
        Xmloperation xmlop;
        public Xmloperation Xmlop
        {
            get { return xmlop; }
            set { xmlop = value; }
        }
        LoginService ls = new LoginService();
        public Wx_alogin(Xmloperation xmlop)
            : this()
        {
            this.xmlop = xmlop;
            //try
            //{
            //    QRCodeDecoder decoder = new QRCodeDecoder();
            //    String decodedString = decoder.decode(new QRCodeBitmapImage(new Bitmap(pictureCapture.Image)));
            //    tbDecodeResult.Text = decodedString;
            //    LoadData();
            //    LoadAcount();
            //}
            //catch (Exception wechartex)
            //{
            //    WechartLog.WriteLog(wechartex);
            //}
        }
        public Wx_alogin()
        {
            InitializeComponent();
        }

        private void alogin_Load(object sender, System.EventArgs e)
        {
            GetWx_idListView();
        }
        private void GetWx_idListView()
        {
            ListViewItem myItem = new ListViewItem();
            Wx_idListView.Columns.Clear();
            Wx_idListView.Items.Clear();
            Wx_idListView.Columns.Add("微信用户",50,HorizontalAlignment.Center);
            Wx_idListView.Columns.Add("设备ID", 180,HorizontalAlignment.Center);
            XmlNodeList nodelista = xmlop.GetTopElement("Account").ChildNodes;
            if (nodelista.Count > 0)
            {
                foreach (XmlElement ela in nodelista)//读元素值
                {
                    string aname = ela.Attributes["weixin"].Value;
                    string anum = ela.Attributes["id"].Value;
                    myItem = Wx_idListView.Items.Add(aname);
                    myItem.SubItems.Add(string.Format("{0:N2}", (anum)));
                    myItem.UseItemStyleForSubItems = true;
                }
            }

        }
        /// <summary>
        /// close
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void loginclose_Click(object sender, System.EventArgs e)
        {
            Close();
        }
        private void Wx_idListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            StringWriter sw = new StringWriter();
            JsonWriter writer = new JsonTextWriter(sw);
            writer.WriteStartObject();
            writer.WritePropertyName("wechat_id");
            writer.WriteValue(Wx_idListView.SelectedItems[0].SubItems[1].Text);
            writer.WritePropertyName("desc");
            writer.WriteValue(Wx_idListView.SelectedItems[0].SubItems[0].Text);
            writer.WritePropertyName("qrcode_url");
            writer.WriteValue(ls.GetQRCodeurl());
            writer.WritePropertyName("token");
            writer.WriteValue(Program.token);
            writer.WriteEndObject();
            writer.Flush();
            string jsonText = sw.GetStringBuilder().ToString();
            ASCIIEncoding encoding = new ASCIIEncoding();
            string postData = "" + sw.GetStringBuilder().ToString();
            byte[] data = encoding.GetBytes(postData);
            HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(Yihuyun.Yihuyun_wecharturl);
            myRequest.Method = "POST";
            myRequest.ContentType = "application/x-www-form-urlencoded";
            myRequest.ContentLength = data.Length;
            Stream newStream = myRequest.GetRequestStream();
            newStream.Write(data, 0, data.Length);
            newStream.Close();
            HttpWebResponse myResponse = (HttpWebResponse)myRequest.GetResponse();
            StreamReader reader = new StreamReader(myResponse.GetResponseStream(), Encoding.UTF8);
            string content = reader.ReadToEnd();
            JObject jo = (JObject)JsonConvert.DeserializeObject(content);
            //panel1.Text = content;//返回信息
            JObject getlogin = (JObject)JsonConvert.DeserializeObject(content);
            string msg = getlogin["msg"].ToString();
            if (msg != "success")
            {
                MessageBox.Show("您被迫下线！   \n  账号已在另一电脑登录 ！ \n  请重新登录 ！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            Close();
            frmWaitingBox f = new frmWaitingBox((obj, args) =>
            {
                Thread.Sleep(1000);
            }, 3, "手机即将处理....", false, false);
            f.ShowDialog(this);
           // f.Close();
        }
    }
}
