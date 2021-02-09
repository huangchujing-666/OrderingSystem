using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace OrderingSystem.WX.SysTools
{
    internal static class XmlDicHelper
    {
        /// <summary>
        ///  字典转到xml
        /// </summary>
        /// <param name="dics"></param>
        /// <returns></returns>
        public static string ProduceXml(this SortedDictionary<string, object> dics)
        {
            StringBuilder xml = new StringBuilder();

            foreach (var item in dics)
            {
                if (item.Value is int
                    || item.Value is Int64
                    || item.Value is double
                    || item.Value is float)
                {
                    xml.Append("<").Append(item.Key).Append(">")
                        .Append(item.Value)
                        .Append("</").Append(item.Key).Append(">");
                }
                else
                {
                    xml.Append("<").Append(item.Key).Append(">")
                        .Append("<![CDATA[")
                        .Append(item.Value)
                        .Append("]]>")
                        .Append("</").Append(item.Key).Append(">");
                }
            }
            return xml.ToString();
        }


        /// <summary>
        /// 把xml文本转化成字典对象
        /// </summary>
        /// <param name="xmlStr"></param>
        /// <param name="xmlDoc">通过字符串转化后的xml对象</param>
        /// <returns></returns>
        public static SortedDictionary<string, string> ChangXmlToDir(string xmlStr, ref XmlDocument xmlDoc)
        {
            if (string.IsNullOrEmpty(xmlStr))
            {
                return null;
            }
            var dirs = new SortedDictionary<string, string>();

            xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xmlStr);
            XmlNode xmlNode = xmlDoc.FirstChild;
            XmlNodeList nodes = xmlNode.ChildNodes;

            foreach (XmlNode xn in nodes)
            {
                XmlElement xe = (XmlElement)xn;
                dirs[xe.Name] = xe.InnerText;
            }

            return dirs;
        }
    }
}