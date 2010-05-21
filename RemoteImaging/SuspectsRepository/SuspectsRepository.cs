using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using Damany.Imaging.Common;
using NDepend.Helpers.FileDirectoryPath;
using OpenCvSharp;
using System.IO;
using Damany.Util;

namespace SuspectsRepository
{
    public class SuspectsRepositoryManager
    {
        private Dictionary<Guid, PersonOfInterest> storage;
        FaceSearchWrapper.FaceSearch faceSearcher;

        const string imageDirectory = "ImageRepository";
        const string badGuyGrayDirectory = imageDirectory + @"\Bad\Gray";
        const string badGuyColorDirectory = imageDirectory + @"\Bad\Color";
        const string goodGuyGrayDirectory = imageDirectory + @"\Good\Gray";
        const string goodGuyColorDirectory = imageDirectory + @"\Good\Color";
        const string svmDirectory = @"SVM";
        const string pcaDirectory = @"PCA";
        const string configIniName = "config.ini";
        const string wantedXml = "wanted.xml";

        public string RootDirectoryPathAbsolute { get; set; }

        private SuspectsRepositoryManager(string rootDirectorPathAbsolute)
        {
            this.RootDirectoryPathAbsolute = rootDirectorPathAbsolute;
            this.storage = new Dictionary<Guid, PersonOfInterest>();
            this.faceSearcher = new FaceSearchWrapper.FaceSearch();
        }

        public static SuspectsRepositoryManager LoadFrom(string filePath)
        {
            if (String.IsNullOrEmpty(filePath))
                throw new ArgumentException("fileName is null or empty.", "fileName");

            string absoluteFilePath = filePath;
            string reason;
            if (!PathHelper.IsValidAbsolutePath(filePath, out reason))
            {
                absoluteFilePath = Path.Combine(Directory.GetCurrentDirectory(), filePath);
            }

            var mnger = new SuspectsRepositoryManager(absoluteFilePath);
            mnger.Load();

            return mnger;
        }

        public void Save()
        {
            if (string.IsNullOrEmpty(this.GetWantedXMlPathAbsolute()))
            {
                throw new InvalidOperationException("Can't save, please call SaveTo instead.");
            }

            this.SaveTo(this.GetWantedXMlPathAbsolute());
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

        private string GetImageDirectoryAbsolute()
        {
            return Path.Combine(this.RootDirectoryPathAbsolute, "ImageRepository");
        }
        private string GetBadGuyGrayDirectoryAbsolute()
        {
            return Path.Combine(this.RootDirectoryPathAbsolute, badGuyGrayDirectory);
        }
        private string GetBadGuyColorDirectoryAbsolute()
        {
            return Path.Combine(this.RootDirectoryPathAbsolute, badGuyColorDirectory);
        }
        private string GetGoodGuyGrayDirectoryAbsolute()
        {
            return Path.Combine(this.RootDirectoryPathAbsolute, goodGuyGrayDirectory);
        }
        private string GetGoodGuyColorDirectoryAbsolute()
        {
            return Path.Combine(this.RootDirectoryPathAbsolute, goodGuyColorDirectory);
        }
        private string GetSvmDirectoryAbsolute()
        {
            return Path.Combine(this.RootDirectoryPathAbsolute, svmDirectory);
        }
        private string GetPcaDirectoryAbsolute()
        {
            return Path.Combine(this.RootDirectoryPathAbsolute, pcaDirectory);
        }

        private static string GetConfigIniPathAbsolute(string directoryPath)
        {
            return Path.Combine(directoryPath, "config.ini");
        }
        private string GetWantedXMlPathAbsolute()
        {
            return Path.Combine(this.RootDirectoryPathAbsolute, wantedXml);
        }

        public static SuspectsRepositoryManager CreateNewIn(string directoryPath)
        {
            var mnger = new SuspectsRepositoryManager(directoryPath);

            string[] directories = new string[] 
            { 
                mnger.GetImageDirectoryAbsolute(),
                mnger.GetBadGuyGrayDirectoryAbsolute(),
                mnger.GetBadGuyColorDirectoryAbsolute(), 
                mnger.GetGoodGuyGrayDirectoryAbsolute(), 
                mnger.GetGoodGuyColorDirectoryAbsolute(),
                mnger.GetSvmDirectoryAbsolute(),
                mnger.GetPcaDirectoryAbsolute(),
            };


            foreach (string dir in directories)
            {
                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }
            }

            File.WriteAllText(GetConfigIniPathAbsolute(directoryPath), Properties.Resource.config);
            return mnger;
        }

        public void Load()
        {

            XmlDocument doc = new XmlDocument();
            doc.Load(this.GetWantedXMlPathAbsolute());

            XmlNodeList nodes = doc.SelectNodes("//person");

            foreach (XmlNode n in nodes)
            {
                var absolutePath = GetFilePathAbsoluteFrom(GetWantedXMlPathAbsolute(), n.Attributes["filename"].Value.ToString());
                if (!File.Exists(absolutePath)) continue;

                var ipl = IplImage.FromFile(absolutePath);

                var x = int.Parse(n.Attributes["X"].Value);
                var y = int.Parse(n.Attributes["Y"].Value);
                var w = int.Parse(n.Attributes["W"].Value);
                var h = int.Parse(n.Attributes["H"].Value);

                ipl.ROI = new CvRect(x, y, w, h);

                var p = new PersonOfInterest(ipl);

                Guid guid;
                if (Guid.TryParse(n.Attributes["id"].Value, out guid))
                {
                    p.Guid = guid;
                }

                p.ID = n.Attributes["id"].Value;
                p.Name = n.Attributes["name"].Value;
                p.Gender = (Gender)Enum.Parse(typeof(Gender), n.Attributes["sex"].Value);
                p.ImageFilePath = absolutePath;

                AddPerson(p);
            }

        }


        public void SaveTo(string filePath)
        {
            if (String.IsNullOrEmpty(filePath))
                throw new ArgumentException("fileName is null or empty.", "filePath");

            string reason;
            string absoluteFilePath = filePath;
            if (!PathHelper.IsValidAbsolutePath(filePath, out reason))
            {
                absoluteFilePath = Path.Combine(Directory.GetCurrentDirectory(), filePath);
            }

            XDocument xDoc = new XDocument();
            xDoc.Add(new XElement("persons"));
            foreach (var person in this.Peoples)
            {
                xDoc.Root.Add(new XElement("person",
                                           new XAttribute("id", person.Guid),
                                           new XAttribute("name", person.Name),
                                           new XAttribute("sex", person.Gender),
                                           new XAttribute("age", person.Age),
                                           new XAttribute("X", person.Ipl.ROI.X),
                                           new XAttribute("Y", person.Ipl.ROI.Y),
                                           new XAttribute("W", person.Ipl.ROI.Width),
                                           new XAttribute("H", person.Ipl.ROI.Height),
                                           new XAttribute("filename", GetFilePathRelativeFrom(absoluteFilePath, person.ImageFilePath))
                                ));
            }

            xDoc.Save(filePath);
        }


        public bool Contains(Guid id)
        {
            return this.storage.ContainsKey(id);

        }

        public IEnumerable<PersonOfInterest> Peoples
        {
            get
            {
                return this.storage.Values.AsEnumerable();
            }
        }

        private void AddPerson(PersonOfInterest p)
        {
            this.storage[p.Guid] = p;
        }

        public void Clear()
        {
            this.storage.Clear();
        }

        public void AddNewPerson(PersonOfInterest p)
        {
            String newImageFileName =
                p.Guid.ToString().ToUpper() + ".jpg";

            string badGuyColorFilePath = Path.Combine(GetBadGuyColorDirectoryAbsolute(), newImageFileName);

            if (!File.Exists(badGuyColorFilePath))
            {
                var roi = p.Ipl.ROI;
                p.Ipl.ResetROI();
                p.Ipl.SaveImage(badGuyColorFilePath);
                p.Ipl.ROI = roi;

                p.ImageFilePath = badGuyColorFilePath;
            }

            this.storage[p.Guid] = p;
        }

        public void UpdateRepository()
        {
            //FaceProcessingWrapper.SVM.Train(this.RootDirectoryPathAbsolute);

            //FaceProcessingWrapper.PCA.Train(this.RootDirectoryPathAbsolute);
        }

        public int Count
        {
            get
            {
                return this.storage.Count;
            }
        }


        public PersonOfInterest this[Guid id]
        {
            get { return this.storage[id]; }
        }

    }
}
