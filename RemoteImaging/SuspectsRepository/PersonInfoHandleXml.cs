using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Xml;

namespace SuspectsRepository
{

    public class PersonInfoHandleXml
    {
        string FileName = null;
        private static PersonInfoHandleXml personInfoxml = null;
        public PersonInfoHandleXml()
        {
            FileName = System.IO.Path.Combine(
                Environment.CurrentDirectory,
                Properties.Settings.Default.SuspectsConfigurationFileName
                );
        }


        public static PersonInfoHandleXml GetInstance()
        {
            if (personInfoxml == null)
            {
                personInfoxml = new PersonInfoHandleXml();
            }
            return personInfoxml;
        }


        public void WriteInfo(PersonInfo info)
        {
            XDocument xDoc = XDocument.Load(FileName);

            if (xDoc.Root.Nodes().Count() > 4)
            {
                xDoc.Root.FirstNode.Remove();
            }

            xDoc.Root.Add(new XElement("person",
                new XAttribute("id", info.ID),
                new XAttribute("name", info.Name),
                new XAttribute("sex", info.Sex),
                new XAttribute("age", info.Age),
                new XAttribute("card", info.CardId),
                new XAttribute("filename", info.FileName),
                new XAttribute("similarity", info.Similarity)));
            xDoc.Save(FileName);
        }


        private System.Collections.Generic.Dictionary<string, PersonInfo> suspects
            = new System.Collections.Generic.Dictionary<string, PersonInfo>();

        public bool HasCurInfoNode(string cardid)
        {
            if (suspects.ContainsKey(cardid)) return true;

            XmlDocument doc = new XmlDocument();
            doc.Load(FileName);
            XmlNodeList resNodeList = doc.SelectNodes("//person[@card=\"" + cardid + "\"]");
            return resNodeList.Count > 0;
        }


    }
}
