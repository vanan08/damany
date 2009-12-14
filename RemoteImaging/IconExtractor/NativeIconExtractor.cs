using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using OpenCvSharp;

namespace ImageProcess
{
    public static class NativeIconExtractor
    {
        const string FaceSearchDll = "FaceSelDll.dll";

        [DllImport(FaceSearchDll, EntryPoint = "FaceImagePreprocess")]
        public static extern void NormalizeFace(
            System.IntPtr faceIplPtr,
            ref System.IntPtr normalizedFace,
            CvRect roi);


        public static float[] ResizeIplTo(IplImage Face, int width, int height, BitDepth bitDepth, int channel)
        {
            IplImage smallerFace = new IplImage(new OpenCvSharp.CvSize(width, height), bitDepth, channel);

            Face.Resize(smallerFace, Interpolation.Linear);

            unsafe
            {
                byte* smallFaceData = smallerFace.ImageDataPtr;
                float[] currentFace = new float[width * height * 8 * channel];
                for (int i = 0; i < smallerFace.Height; i++)
                {
                    for (int j = 0; j < smallerFace.Width; j++)
                    {
                        currentFace[i * smallerFace.WidthStep + j] = 
                            (float)smallFaceData[i * smallerFace.WidthStep + j];
                    }
                }

                smallerFace.Dispose();

                return currentFace;
            }
        }

    }
}
