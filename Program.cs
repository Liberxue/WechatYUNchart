using System;

using System.Windows.Forms;

namespace YUNkefu
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        /// 
        public static string opId = "";  //记录操作员id，默认为空
        public static string token = "";  //记录操作员编码，默认为空
        public static string opName = "";  //记录操作员姓名，默认为空
        public static string opreal_name = "";//记录登录ID
        public static string optime = ""; //记录上次登录时间默认
        public static string optel = "";//记录登录ID
        public static string opQx = "";//获取当前登录用户权限组
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Wx_MainFrom());
        }
    }
}
