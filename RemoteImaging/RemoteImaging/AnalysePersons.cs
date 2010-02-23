using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FaceRecognition;
using SuspectsRepository;


namespace RemoteImaging.ImportPersonCompare
{
    public class ImportantPersonDetail
    {
        public ImportantPersonDetail(PersonInfo suspect, FaceRecognition.RecognizeResult result)
        {
            this.Info = suspect;
            this.Similarity = result;
        }

        /// <summary>
        /// 是否是犯罪嫌疑人
        /// </summary>
        public bool State { get; set; }

        public PersonInfo Info { get; private set; }

        public string[] SimilarityRange { get; private set; }

        public FaceRecognition.RecognizeResult Similarity {get;set;}


    }
}