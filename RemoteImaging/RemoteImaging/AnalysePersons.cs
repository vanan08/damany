using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FaceRecognition;
using SuspectsRepository;
using Damany.Imaging.PlugIns;


namespace RemoteImaging.ImportPersonCompare
{
    public class ImportantPersonDetail
    {
        public ImportantPersonDetail(PersonOfInterest suspect, FaceRecognition.RecognizeResult result)
        {
            this.Info = suspect;
            this.Similarity = result;
        }

        /// <summary>
        /// 是否是犯罪嫌疑人
        /// </summary>
        public bool State { get; set; }

        public PersonOfInterest Info { get; private set; }

        public string[] SimilarityRange { get; private set; }

        public FaceRecognition.RecognizeResult Similarity {get;set;}


    }
}