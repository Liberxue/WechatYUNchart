using System;
using System.Xml;

namespace YUNkefu
{
    public enum XDirection
    { Up = -2, Down = 0 }

    public class Xmloperation
    {
        XmlDocument doc;
        public XmlDocument Doc
        { get { return doc; } }
        String xmlfile;
        public Xmloperation()
        {

        }
        public Xmloperation(string xmlfile)
        {
            this.xmlfile = xmlfile;
            doc = new XmlDocument();
        }
        public XmlElement GetTopElement(string elementname)
        {
            doc.Load(xmlfile);
            XmlElement el = null;
            XmlNodeList topM = doc.DocumentElement.ChildNodes;
            foreach (XmlElement element in topM)
            {
                if (element.Name == elementname)
                {
                    el = element;
                }
            }
            return el;
        }  //获得对应名字的父节点元素
        public XmlElement GetQryElement(XmlElement element, String keyattriname, String keyattrivalue, XDirection direction)     //向上或向下查找对应父元素下符合单个属性值的的第一个二级节点元素并返回
        {
            XmlElement el = null;
            XmlNodeList nodelist = element.ChildNodes;
            int k = nodelist.Count;
            for (int i = 0; i < k; i++)
            {
                int x = (direction == XDirection.Down) ? i : (k - i - 1);
                if ((((XmlElement)nodelist[x]).Attributes[keyattriname].Value == keyattrivalue) ||
                    ((keyattriname == "Date") && DateTime.Parse(((XmlElement)nodelist[x]).Attributes[keyattriname].Value) <= DateTime.Parse(keyattrivalue))
                    )
                {
                    el = (XmlElement)nodelist[x];
                    break;
                }
            }
            return el;
        }


        public int GetQryElementNub(string elementname, String keyattriname, String keyattrivalue)     //向上或向下查找对应父元素下符合单个属性值的的第一个二级节点元素并返回
        {
            int m = 0;
            XmlElement element = GetTopElement(elementname);
            XmlNodeList nodelist = element.ChildNodes;
            int k = nodelist.Count;
            for (int i = 0; i < k; i++)
            {
                if (((XmlElement)nodelist[i]).Attributes[keyattriname].Value == keyattrivalue)
                {
                    m++;
                }
            }
            return m;
        }

        public String GetAttrivalue(string elementname, String keyattriname, String keyattrivalue, String attriname)
        {
            string attrivalue = "";
            doc.Load(xmlfile);
            XmlElement element = GetTopElement(elementname);
            XmlNodeList nodelist = element.ChildNodes;
            foreach (XmlElement el in nodelist)
            {
                if (el.Attributes[keyattriname].Value == keyattrivalue)
                {

                    attrivalue = el.Attributes[attriname].Value;
                }
            }
            return attrivalue;
        }
        public void Addinnode(string elementname, String nodename, String[] attrinamelist, String[] attrivaluelist)
        {
            doc.Load(xmlfile);
            XmlElement element = GetTopElement(elementname);
            XmlElement el = doc.CreateElement(nodename);
            for (int i = 0; i < attrinamelist.Length; i++)
            {
                el.SetAttribute(attrinamelist[i], attrivaluelist[i]);
            }
            element.AppendChild(el);
            doc.Save(xmlfile);
        }

        public void InsetAfterEl(String elementname, String nodename, String keyattriname, String keyattrivalue, String[] attrinamelist, String[] attrivaluelist)
        {
            //doc.Load(xmlfile);
            XmlElement element = GetTopElement(elementname);
            XmlElement elx = GetQryElement(element, keyattriname, keyattrivalue, XDirection.Up);
            XmlElement el = doc.CreateElement(nodename);
            for (int i = 0; i < attrinamelist.Length; i++)
            {
                el.SetAttribute(attrinamelist[i], attrivaluelist[i]);
            }
            //重设index
            if (elx == null || elx.Attributes["Date"].Value != el.Attributes["Date"].Value)
                el.SetAttribute("Index", el.Attributes["Index"].Value + "-1");
            else
                el.SetAttribute("Index", el.Attributes["Index"].Value + "-" + (Int32.Parse((elx.Attributes["Index"].Value).Substring(9)) + 1).ToString());
            element.InsertAfter(el, elx);
            //element.AppendChild(el);
            doc.Save(xmlfile);
        }

        public void AttriValueEdit(String elementname, String keyattriname, String keyattrivalue, String[] attrinamelist, String[] attrivaluelist)
        {
            doc.Load(xmlfile);
            XmlElement element = GetTopElement(elementname);
            XmlNodeList nodelist = element.ChildNodes;
            foreach (XmlElement el in nodelist)
            {
                if (el.Attributes[keyattriname].Value == keyattrivalue)
                {
                    for (int i = 0; i < attrinamelist.Length; i++)
                    {
                        el.SetAttribute(attrinamelist[i], attrivaluelist[i]);
                    }
                    break;
                }
            }
            doc.Save(xmlfile);
        }

        public void MoveUpDown(String elementname, int index, XDirection direction)
        {
            doc.Load(xmlfile);
            XmlElement element = GetTopElement(elementname);
            XmlNodeList nodelist = element.ChildNodes;
            XmlElement el = (XmlElement)nodelist[index];
            if ((index != 0 && direction == XDirection.Up) || (index != nodelist.Count - 1 && direction == XDirection.Down))
            {
                element.RemoveChild(el);
                element.InsertAfter(el, (XmlElement)nodelist[(index + (int)direction)]);
                doc.Save(xmlfile);
            }
        }

        public Object[] GetAttriList(String elementname, String attriname)
        {
            XmlElement element = GetTopElement(elementname);
            XmlNodeList nodelist = element.ChildNodes;
            int k = nodelist.Count;
            Object[] obtlist = null;
            if (k > 0)
            {
                obtlist = new Object[k];
                for (int i = 0; i < k; i++)
                {
                    obtlist[i] = ((XmlElement)nodelist[i]).Attributes[attriname].Value;
                }
            }
            return obtlist;
        }
    }
}
