using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Text;
using System.Windows.Forms;
using YUNkefu.Http;

namespace YUNkefu
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void picClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
            this.Close();           
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
                   this.BeginInvoke((Action)delegate()
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
                       var mainFrm = (MainFrom)this.Owner;
                       mainFrm.AddToList(uin);
                       this.BeginInvoke((Action)delegate()
                       {
                           this.Hide();
                           this.Close();
                       });
                       break;
                       //this.BeginInvoke((Action)delegate()
                       //{
                       //    this.Hide();
                       //    wechart frmmf = new wechart();
                       //    frmmf.Show();
                       //});
                       //break;
                   }
               }

           })).BeginInvoke(null, null);
        }

        private void linkReturn_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            linkReturn.Visible = false;
            DoLogin();
        }
    }
}
