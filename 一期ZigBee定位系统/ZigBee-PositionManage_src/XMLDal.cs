using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PositionManage
{
    public static class XMLDal
    {
        private static System.Xml.XmlDocument _xml = null;

        private readonly static string _xmlpath = System.IO.Path.GetFullPath(Program.GetAppSettingValue("DataSource", "./Data.xml"));

        private static void LoadXML()
        {
            _xml = new System.Xml.XmlDocument();

            if (System.IO.File.Exists(_xmlpath))
            {
                _xml.Load(_xmlpath);
            }
            else
            {
                _xml.LoadXml("<?xml version=\"1.0\" encoding=\"utf-8\" ?><root></root>");
                _xml.Save(_xmlpath);
            }
        }

        public static string GetDataFromXML(string DictName, string NodeName, string ElementName, string ElementID, out string ErrorMsg)
        {
            if (_xml == null)
            {
                LoadXML();
            }

            System.Xml.XmlNode xNode = _xml.SelectSingleNode(string.Format("/root/{0}/{1}[@{2}='{3}']", DictName, NodeName, ElementName, ElementID));
            if (xNode != null)
            {
                ErrorMsg = "";
                return xNode.InnerText;
            }
            else
            {
                ErrorMsg = "找不到对应的数据!";
                return "";
            }
        }

        public static Dictionary<string, string> GetXMLDatas(string DictName, string NodeName, string ElementName , out string ErrorMsg)
        {
            if (_xml == null)
            {
                LoadXML();
            }

            System.Xml.XmlNodeList xNodes = _xml.SelectNodes(string.Format("/root/{0}/{1}[@{2}]", DictName, NodeName, ElementName));
            if (xNodes != null && xNodes.Count > 0)
            {
                ErrorMsg = "";
                Dictionary<string, string> dict = new Dictionary<string, string>();
                foreach (System.Xml.XmlNode xNode in xNodes)
                {
                    dict.Add(xNode.Attributes[ElementName].Value, xNode.InnerText);
                }
                return dict;
            }
            else
            {
                ErrorMsg = "找不到对应的数据!";
                return null;
            }
        }

        public static bool SaveDataToXML(string DictName, string NodeName, string ElementName, string ElementID, string ElementData, out string ErrorMsg)
        {
            if (_xml == null)
            {
                LoadXML();
            }

            System.Xml.XmlNode xNode = _xml.SelectSingleNode(string.Format("/root/{0}/{1}[@{2}='{3}']", DictName, NodeName, ElementName, ElementID));
            if (xNode != null)
            {
                xNode.InnerText = ElementData;
            }
            else
            {
                try
                {
                    System.Xml.XmlNode xRoot = _xml.SelectSingleNode("/root/" + DictName);
                    if (xRoot == null)
                    {
                        xRoot = _xml.CreateNode(System.Xml.XmlNodeType.Element, DictName, null);
                        if (_xml.ChildNodes.Count > 0)
                        {
                            _xml.DocumentElement.AppendChild(xRoot);
                        }
                        else
                        {
                            _xml.AppendChild(xRoot);
                        }
                    }
                    System.Xml.XmlElement xElm = _xml.CreateElement(NodeName);
                    xElm.SetAttribute(ElementName, ElementID);
                    xElm.InnerText = ElementData;
                    xRoot.AppendChild(xElm);
                    _xml.Save(_xmlpath);
                }
                catch (Exception ex)
                {
                    ErrorMsg = ex.Message;
                    return false;
                }
            }
            ErrorMsg = "";
            return true;
        }
    }
}
