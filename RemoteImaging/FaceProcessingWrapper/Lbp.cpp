#pragma once

#include <stdafx.h>
#include "../../FaceProcessing/FaceProcessing/lbp.h"

using namespace System;
using namespace System::Runtime::InteropServices;

namespace FaceProcessingWrapper {

	public ref class LbpWrapper {
	public:
		LbpWrapper()
		{
			pLbp = new Damany::Imaging::FaceCompare::LBP();
		}
		~LbpWrapper()
		{
		    delete pLbp;
			pLbp = NULL;
		}

		void Load( array<OpenCvSharp::IplImage^>^ images )
		{
			if (images->Length == 0)
				throw gcnew System::ArgumentException("Length of images is 0");


			count = images->Length;
			IplImage** IplPtrs = new IplImage* [images->Length];

			for (int i=0; i<images->Length; ++i)
			{
				IplPtrs[i] = (IplImage*) images[i]->CvPtr.ToPointer();
			}

			pLbp->LoadImages(IplPtrs, images->Length);
			delete[] IplPtrs;

		}

		array<float>^ CompareTo(OpenCvSharp::IplImage^ img)
		{
			float* result = new float[count];
			CvRect rect;
			rect.x = img->ROI.X;
			rect.y = img->ROI.Y;
			rect.width = img->ROI.Width;
			rect.height = img->ROI.Height;


			pLbp->CmpFace((IplImage*) img->CvPtr.ToPointer(), rect, result);
			array<float>^ returnResult = gcnew array<float>(count);

			for (int i=0; i<count; ++i)
			{
				returnResult[i] = result[i];
			}

			delete[] result;

			return returnResult;

		}

	private:
		Damany::Imaging::FaceCompare::LBP *pLbp;
		int count;
	};
}