#pragma once

#include <stdafx.h>
#include "../../FaceProcessing/FaceProcessing/similay.h"
#include "../../FaceProcessing/FaceProcessing/LBP.h"


using namespace System;
using namespace System::Runtime::InteropServices;

namespace FaceProcessingWrapper
{
	static public ref class StaticFunctions
	{
		public:
			static bool CompareFace(OpenCvSharp::IplImage^ one, OpenCvSharp::CvRect oneRect, 
									OpenCvSharp::IplImage^ another, OpenCvSharp::CvRect anotherRect, 
								    float% cmpResult, bool noRotate)
			{
				pin_ptr<float> pResult = &cmpResult;

				bool result = ::CompareFace( (IplImage*)  one->CvPtr.ToPointer(), 
											::cvRect(oneRect.X, oneRect.Y, oneRect.Width, oneRect.Height),
											(IplImage*) another->CvPtr.ToPointer(), 
											::cvRect(anotherRect.X, anotherRect.Y, anotherRect.Width, anotherRect.Height),
											 pResult , noRotate );
				return result;
			}

			


	};
}