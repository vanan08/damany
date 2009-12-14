using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace RemoteImaging
{
    public static class SVMWrapper
    {
        const string dllName = "faceSVM.dll";

        /// Return Type: void
        ///imgWidth: int
        ///imgHeight: int
        ///eigenNum: int
        ///option: char*
        [DllImportAttribute(dllName, EntryPoint = "SvmTrain")]
        public static extern void SvmTrain(int imgWidth, int imgHeight, int eigenNum, string option);


        /// Return Type: double
        ///currentFace: float*
        [DllImportAttribute(dllName, EntryPoint = "SvmPredict")]
        public static extern double SvmPredict(float[] currentFace);


        /// Return Type: void
        ///sampleCount: int
        ///imgLen: int
        ///eigenNum: int
        [DllImportAttribute(dllName, EntryPoint = "InitSvmData")]
        public static extern void InitSvmData(int imgLen, int eigenNum);

    }

}
