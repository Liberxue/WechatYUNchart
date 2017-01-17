using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YUNkefu.UpdateClass
{
    public class UpdateFileInfo
    {
        private string _name;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        private string _version;

        public string Version
        {
            get { return _version; }
            set { _version = value; }
        }
        private string _size;

        public string Size
        {
            get { return _size; }
            set { _size = value; }
        }
    }
}
