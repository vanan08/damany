using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace FaceRecognition
{
    public struct RecognizeResult
    {

        /// float
        private float similarity;
        public float Similarity
        {
            get { return similarity;}
            set { similarity = value; }
        }


        private String fileName;
        public System.String FileName
        {
            get { return fileName; }
            set { fileName = value; }
        }
 
    }

}
