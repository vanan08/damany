using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using NDepend.Helpers.FileDirectoryPath;

namespace SuspectsRepository
{
    public class SuspectsRepositoryManager
    {
        private Dictionary<string, PersonInfo> storage;

        public Dictionary<string, PersonInfo> Storage
        {
            get { return storage; }
            private set { storage = value; }
        }

        public SuspectsRepositoryManager()
        {
            this.Storage = new Dictionary<string, PersonInfo>();
        }

        public static SuspectsRepositoryManager LoadFrom(string filePath)
        {
            if (String.IsNullOrEmpty(filePath))
                throw new ArgumentException("fileName is null or empty.", "fileName");

            string absoluteFilePath = filePath;
            string reason;
            if (!PathHelper.IsValidAbsolutePath(filePath, out reason))
            {
                absoluteFilePath = System.IO.Path.Combine(System.IO.Directory.GetCurrentDirectory(), filePath);
            }


            var mnger = new SuspectsRepositoryManager();
            mnger.FileName = filePath;

            XmlDocument doc = new XmlDocument();
            doc.Load(filePath);

            XmlNodeList nodes = doc.SelectNodes("//person");

            foreach (XmlNode n in nodes)
            {
                PersonInfo p = new PersonInfo();
                p.ID = n.Attributes["id"].Value.ToString();
                p.Name = n.Attributes["name"].Value.ToString();
                p.Sex = n.Attributes["sex"].Value.ToString();
                p.Age = Convert.ToInt32(n.Attributes["age"].Value.ToString());
                p.CardId = n.Attributes["card"].Value.ToString();
                p.FileName = GetFilePathAbsoluteFrom( absoluteFilePath, n.Attributes["filename"].Value.ToString());
                p.Similarity = Convert.ToInt32(n.Attributes["similarity"].Value);

                mnger.Suspects[p.FileName] = p;
            }

            return mnger;
        }

        public void Save()
        {
            if (string.IsNullOrEmpty(this.FileName))
            {
                throw new InvalidOperationException("Can't save, please call SaveTo instead.");
            }

            this.SaveTo(this.FileName);

        }

        private static System.Xml.Serialization.XmlSerializer GetSerializer()
        {
            var serializer = new System.Xml.Serialization.XmlSerializer(typeof(Dictionary<string, PersonInfo>));
            return serializer;
        }


        private static string GetFilePathRelativeFrom(string baseAbsolutePath, string absolutePath)
        {
            var currentAbsoluteDirectoryPath = new FilePathAbsolute(baseAbsolutePath).ParentDirectoryPath;
            return new FilePathAbsolute(absolutePath).GetPathRelativeFrom(currentAbsoluteDirectoryPath).Path;
        }

        private static string GetFilePathAbsoluteFrom(string baseAbsolutePath, string relativePath)
        {
            var absoluteBase = new FilePathAbsolute(baseAbsolutePath).ParentDirectoryPath;
            var relativeFilePath = new FilePathRelative(relativePath);

            return relativeFilePath.GetAbsolutePathFrom(absoluteBase).Path;
        }


        public void SaveTo(string filePath)
        {
            if (String.IsNullOrEmpty(filePath))
                throw new ArgumentException("fileName is null or empty.", "filePath");

            string reason;
            string absoluteFilePath = filePath;
            if (!PathHelper.IsValidAbsolutePath(filePath, out reason))
            {
                absoluteFilePath = System.IO.Path.Combine(System.IO.Directory.GetCurrentDirectory(), filePath);
            }

            XDocument xDoc = new XDocument();
            xDoc.Add(new XElement("persons"));
            foreach (var person in this.Peoples)
            {
                xDoc.Root.Add(new XElement("person",
                                new XAttribute("id", person.ID),
                                new XAttribute("name", person.Name),
                                new XAttribute("sex", person.Sex),
                                new XAttribute("age", person.Age),
                                new XAttribute("card", person.CardId),
                                new XAttribute("filename", GetFilePathRelativeFrom(absoluteFilePath, person.FileName)),
                                new XAttribute("similarity", person.Similarity)));
            }

            xDoc.Save(filePath);
        }


        public bool Contains(string id)
        {
            return Suspects.ContainsKey(id);

        }

        public string FileName
        {
            get;
            set;
        }

        public IEnumerable<PersonInfo> Peoples
        {
            get
            {
                return Suspects.Values.ToArray();
            }
        }


        public PersonInfo this[string idx]
        {
            get { return Suspects[idx]; }
        }

        public IDictionary<string, PersonInfo> Suspects
        {
            get
            {
                return storage;
            }
        }


    }
}
