using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace FaceRecognition
{
    [StructLayout(LayoutKind.Sequential)]
    public struct RecognizeResult
    {

        /// float
        public float similarity;

        /// char*
        [MarshalAs(UnmanagedType.LPStr)]
        public String fileName;
 
    }

}
