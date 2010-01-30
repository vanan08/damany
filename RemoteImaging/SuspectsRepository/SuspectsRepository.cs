using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using NDepend.Helpers.FileDirectoryPath;
using System.Drawing;
using OpenCvSharp;
using System.IO;

namespace SuspectsRepository
{
    public class SuspectsRepositoryManager
    {
        private Dictionary<string, PersonInfo> storage;
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
            this.storage = new Dictionary<string, PersonInfo>();
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
                absoluteFilePath = System.IO.Path.Combine(System.IO.Directory.GetCurrentDirectory(), filePath);
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

        private string GetImageDirectoryAbsolute()
        {
            return System.IO.Path.Combine(this.RootDirectoryPathAbsolute, "ImageRepository");
        }
        private string GetBadGuyGrayDirectoryAbsolute()
        {
            return System.IO.Path.Combine(this.RootDirectoryPathAbsolute, badGuyGrayDirectory);
        }
        private string GetBadGuyColorDirectoryAbsolute()
        {
            return System.IO.Path.Combine(this.RootDirectoryPathAbsolute, badGuyColorDirectory);
        }
        private string GetGoodGuyGrayDirectoryAbsolute()
        {
            return System.IO.Path.Combine(this.RootDirectoryPathAbsolute, goodGuyGrayDirectory);
        }
        private string GetGoodGuyColorDirectoryAbsolute()
        {
            return System.IO.Path.Combine(this.RootDirectoryPathAbsolute, goodGuyColorDirectory);
        }
        private string GetSvmDirectoryAbsolute()
        {
            return System.IO.Path.Combine(this.RootDirectoryPathAbsolute, svmDirectory);
        }
        private string GetPcaDirectoryAbsolute()
        {
            return System.IO.Path.Combine(this.RootDirectoryPathAbsolute, pcaDirectory);
        }

        private static string GetConfigIniPathAbsolute(string directoryPath)
        {
            return System.IO.Path.Combine(directoryPath, "config.ini");
        }
        private string GetWantedXMlPathAbsolute()
        {
             return System.IO.Path.Combine(this.RootDirectoryPathAbsolute, wantedXml);
        }

        public static SuspectsRepositoryManager CreateNewIn(string directoryPath)
        {
            SuspectsRepositoryManager mnger = new SuspectsRepositoryManager(directoryPath);

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
                if (!System.IO.Directory.Exists(dir))
                {
                    System.IO.Directory.CreateDirectory(dir);
                }
            }

            System.IO.File.WriteAllText(GetConfigIniPathAbsolute(directoryPath), Properties.Resource.config);
            return mnger;
        }

        public void Load()
        {

            XmlDocument doc = new XmlDocument();
            doc.Load( this.GetWantedXMlPathAbsolute() );

            XmlNodeList nodes = doc.SelectNodes("//person");

            foreach (XmlNode n in nodes)
            {
                PersonInfo p = new PersonInfo();
                p.ID = n.Attributes["id"].Value.ToString();
                p.Name = n.Attributes["name"].Value.ToString();
                p.Sex = n.Attributes["sex"].Value.ToString();
                p.Age = Convert.ToInt32(n.Attributes["age"].Value.ToString());
                p.CardId = n.Attributes["card"].Value.ToString();
                p.FileName = GetFilePathAbsoluteFrom( GetWantedXMlPathAbsolute(), n.Attributes["filename"].Value.ToString());
                p.Similarity = Convert.ToInt32(n.Attributes["similarity"].Value);

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
            return this.storage.ContainsKey(id);

        }

        public IEnumerable<PersonInfo> Peoples
        {
            get
            {
                return this.storage.Values.AsEnumerable();
            }
        }

        private void AddPerson(PersonInfo p)
        {
            this.storage[p.FileName] = p;
        }

        public void AddNewPerson(PersonInfo p, string imageFilePathAbsolute, Rectangle faceRect)
        {
            String newImageFileName = 
                System.Guid.NewGuid().ToString().ToUpper() + System.IO.Path.GetExtension(imageFilePathAbsolute);

            //搜索人脸
            var iplFace = 
                BitmapConverter.ToIplImage( (Bitmap) Bitmap.FromFile(imageFilePathAbsolute) );

            string badGuyColorFilePath = Path.Combine(GetBadGuyColorDirectoryAbsolute(), newImageFileName);
            iplFace.SaveImage(badGuyColorFilePath);

            //归一化
            OpenCvSharp.CvRect rect = new OpenCvSharp.CvRect(
                                                                faceRect.X,
                                                                faceRect.Y,
                                                                faceRect.Width,
                                                                faceRect.Height);

            IplImage faceFound = faceSearcher.NormalizeImage(iplFace, new CvRect(0, 0, iplFace.Width, iplFace.Height));

            OpenCvSharp.IplImage[] normalizedImages =
                faceSearcher.NormalizeImageForTraining(iplFace, rect);

            for (int i = 0; i < normalizedImages.Length; ++i)
            {
                if (i != 2) continue;

                string normalizedFaceName = string.Format("{0}_{1:d4}.jpg",
                    System.IO.Path.GetFileNameWithoutExtension(badGuyColorFilePath), i);

                string grayFilePath = System.IO.Path.Combine(GetBadGuyGrayDirectoryAbsolute(), normalizedFaceName);

                normalizedImages[i].SaveImage(grayFilePath);
            }

            p.FileName = badGuyColorFilePath;

            this.storage[p.FileName] = p;
        }

        public void UpdateRepository()
        {
            FaceProcessingWrapper.SVM.Train(this.RootDirectoryPathAbsolute);

            FaceProcessingWrapper.PCA.Train(this.RootDirectoryPathAbsolute);

        }

        public int Count
        {
            get
            {
                return this.storage.Count;
            }
        }


        public PersonInfo this[string idx]
        {
            get { return this.storage[idx]; }
        }

    }
}
