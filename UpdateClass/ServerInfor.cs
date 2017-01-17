using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YUNkefu.UpdateClass
{
    public class ServerInfor
    {
        private string _server;

        public string Server
        {
            get { return _server; }
            set { _server = value; }
        }

        private string _version;

        public string Version
        {
            get { return _version; }
            set { _version = value; }
        }

        private string _remark;

        public string Remark
        {
            get { return _remark; }
            set { _remark = value; }
        }

        private List<UpdateFileInfo> _downloadfilelist;
        public List<UpdateFileInfo> DownloadFileList
        {
            get { return _downloadfilelist; }
            set { _downloadfilelist = value; }
        }

        private bool _quckload;

        public bool _QuckLoad
        {
            get { return _quckload; }
            set { _quckload = value; }
        }

        private string _application;

        public string Application
        {
            get { return _application; }
            set { _application = value; }
        }

        private string _updatename;

        public string UpdateName
        {
            get { return _updatename; }
            set { _updatename = value; }
        }

        private string _otherinfo;

        public string OtherInfo
        {
            get { return _otherinfo; }
            set { _otherinfo = value; }
        }

        private string _desktopname;

        public string DeskTopName
        {
            get { return _desktopname; }
            set { _desktopname = value; }
        }

        private string _iconame;

        public string IcoName
        {
            get { return _iconame; }
            set { _iconame = value; }
        }

        public INIClass INI
        {
            get;
            set;
        }

        public ServerInfor()
        {
            DownloadFileList = new List<UpdateFileInfo>();
        }
    }
}
