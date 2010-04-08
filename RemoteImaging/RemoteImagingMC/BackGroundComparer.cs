using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace RemoteImaging
{
    public static class BackGroundComparer
    {
        const string dllName = "Back.dll";

        [DllImportAttribute(dllName, EntryPoint = "IsFace")]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        public static extern bool IsFace(IntPtr faceImg, IntPtr backImg, OpenCvSharp.CvRect SubRect);
    }
}
