#pragma once

#include <stdafx.h>
#include "../../FaceProcessing/FaceProcessing/similay.h"


using namespace System;
using namespace System::Runtime::InteropServices;

namespace FaceProcessingWrapper
{
	static public ref class StaticFunctions
	{
		public:
			static bool CompareFace(OpenCvSharp::IplImage^ one, CvRect oneRect, 
									OpenCvSharp::IplImage^ another, CvRect anotherRect, 
								    float% cmpResult, bool noRotate)
			{
				pin_ptr<float> pResult = &cmpResult;

				bool result = ::CompareFace( (IplImage*)  one->CvPtr.ToPointer(), oneRect,
											 (IplImage*) another->CvPtr.ToPointer(), anotherRect,
											 pResult , noRotate );
				return result;
			}


	};
}