using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace YUNkefu.UpdateClass
{
    public class SynFileInfo
    {
        public string DocID { get; set; }
        public string DocName { get; set; }
        public long FileSize { get; set; }
        public double Size { get; set; }
        public string SynSpeed { get; set; }
        public string SynProgress { get; set; }
        public Image Image { get; set; }
        public DataGridViewRow RowObject { get; set; }
        public string Version { get; set; }
        public DateTime LastTime { get; set; }

        public SynFileInfo(object[] objectArr)
        {
            int i = 0;
            Image = (Image)objectArr[i]; i++;
            DocID = objectArr[i].ToString(); i++;
            DocName = objectArr[i].ToString(); i++;
            FileSize = Convert.ToInt64(objectArr[i]); i++;
            SynSpeed = objectArr[i].ToString(); i++;
            SynProgress = objectArr[i].ToString(); i++;
            this.Size = Convert.ToDouble(objectArr[i].ToString()); i++;
            Version = objectArr[i].ToString(); i++;
            RowObject = (DataGridViewRow)objectArr[i];
            
        }
    }
}
