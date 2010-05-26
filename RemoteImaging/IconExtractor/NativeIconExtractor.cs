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


        public static int BytesPerPixel(BitDepth depth)
        {
            switch (depth)
            {
                case BitDepth.F32:
                    break;
                case BitDepth.F64:
                    break;
                case BitDepth.S16:
                    break;
                case BitDepth.S32:
                    break;
                case BitDepth.S8:
                    break;
                case BitDepth.U16:
                    break;
                case BitDepth.U8:
                    return 1;
                    break;
                default:
                    break;
            }

            throw new NotSupportedException("enum value is not supported");
        }



        public static float[] ResizeIplTo(IplImage Face, int width, int height)
        {
            IplImage smallerFace = 
                new IplImage(new OpenCvSharp.CvSize(width, height), 
                                         Face.Depth, Face.NChannels);

            Face.Resize(smallerFace, Interpolation.Linear);

            unsafe
            {
                byte* smallFaceData = smallerFace.ImageDataPtr;
                float[] currentFace = new float[width * height * smallerFace.NChannels * BytesPerPixel(Face.Depth)];
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
