using YUNkefu;
using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using YUNkefu.Http;

namespace YUNkefu
{
    public partial class Wx_LoginForm : SkinMain
    {
        //#region 拖动无边框窗体
        //[DllImport("user32.dll")]
        //public static extern bool ReleaseCapture();//改变窗体大小
        //[DllImport("user32.dll")]
        //public static extern bool SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);//发送windows消息
        //#endregion

        //#region 窗体边框阴影效果变量申明
        //const int CS_DropSHADOW = 0x20000;
        //const int GCL_STYLE = (-26);
        ////声明Win32 API
        //[DllImport("user32.dll", CharSet = CharSet.Auto)]
        //public static extern int SetClassLong(IntPtr hwnd, int nIndex, int dwNewLong);
        //[DllImport("user32.dll", CharSet = CharSet.Auto)]
        //public static extern int GetClassLong(IntPtr hwnd, int nIndex);
        //#endregion
        public Wx_LoginForm()
        {
            InitializeComponent();
        }
        LoginService ls = new LoginService();        

        private void LoginForm_Load(object sender, EventArgs e)
        {
            this.labTitle.Text = Core.Constant.SoftName;
            //获取登录的
            DoLogin();
        }


        private void DoLogin()
        {
            picQRCode.Image = null;
            picQRCode.SizeMode = PictureBoxSizeMode.Zoom;
            lblTip.Text = "手机微信扫一扫以登录";
           

            ((Action)(delegate()
           {
               //异步加载二维码              
               Image qrcode = ls.GetQRCode();
               if (qrcode != null)
               {
                   BeginInvoke((Action)delegate()
                   {
                       picQRCode.Image = qrcode;
                   });
               }
               else
               {
                   MessageBox.Show("获取二维码失败");
               }

               object login_result = null;

               //循环判断手机扫面二维码结果
               while (true)
               {
                   int i = 0;
                   login_result = ls.LoginCheck();
                   if (login_result is Image) //已扫描 未登录
                   {
                       this.BeginInvoke((Action)delegate()
                       {
                           lblTip.Text = "请点击手机上登录按钮";
                           picQRCode.SizeMode = PictureBoxSizeMode.CenterImage;  //显示头像
                           picQRCode.Image = login_result as Image;
                           linkReturn.Visible = true;
                       });
                   }

                   if (login_result is string)  //已完成登录
                   {
                       //访问登录跳转URL
                       var uin=ls.GetSidUid(login_result as string);
                       //打开主界面
                       var mainFrm = (Wx_MainFrom)Owner;
                       mainFrm.AddToList(uin);
                       this.BeginInvoke((Action)delegate()
                       {
                           Hide();
                           Close();
                       });
                       break;
                   }
               }

           })).BeginInvoke(null, null);
        }

        private void linkReturn_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            linkReturn.Visible = false;
            DoLogin();
        }

        private void picClose_MouseDown(object sender, MouseEventArgs e)
        {
            picClose.Image = Properties.Resources.close_dw;
            picClose.Location = new Point(picClose.Location.X + 1, picClose.Location.Y + 1);
        }

        private void picClose_MouseEnter(object sender, EventArgs e)
        {
            picClose.Image = Properties.Resources.close_dw;
        }

        private void picClose_MouseMove(object sender, MouseEventArgs e)
        {
            picClose.Image = Properties.Resources.close_dw;
        }

        private void picClose_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void bt_alogin_Click(object sender, EventArgs e)
        {
            Xmloperation xmlop = new Xmloperation(@"WxData.xml");
            Wx_alogin Frlogin = new Wx_alogin(xmlop);
            Frlogin.StartPosition = FormStartPosition.WindowsDefaultLocation;
            Frlogin.ShowDialog();
        }

    }
}
