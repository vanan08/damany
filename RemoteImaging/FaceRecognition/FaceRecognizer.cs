using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;


namespace FaceRecognition
{
    public static class FaceRecognizer
    {
        const string DllName = "FacePca.dll";

        [DllImport(DllName, EntryPoint = "InitData")]
        public static extern void InitData(int sampleCount, int imgLen, int eigenNum);

        [DllImport(DllName)]
        public static extern void FreeData();


        [DllImport(DllName, EntryPoint = "FaceRecognition")]
        public static extern void Recognize(
            [In, Out] float[] currentFace, 
            int sampleCount, 
            [In, Out] RecognizeResult[] similarity, 
            int imgLen, 
            int eigenNum);

        [DllImport(DllName)]
        public static extern void FaceTraining(int imgWidth, int imgHeight, int eigenNum);


        
    }
}
