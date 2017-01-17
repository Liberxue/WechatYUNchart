using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YUNkefu
{
    [Serializable]
    public class User
    {

        public User(string loginName, string loginPwd)
        {
            _loginName = loginName;
            _loingPassword = loginPwd;

        }

        private string _loginName;

        public string LoginName
        {
            get { return _loginName; }
            set { _loginName = value; }
        }

        private string _loingPassword;

        public string LoingPassword
        {
            get
            {
                if (_loingPassword != "")
                {
                    return MyEncrypt.DecryptDES(_loingPassword);
                }
                return _loingPassword;
            }
            set { _loingPassword = value; }
        }
    }
}

