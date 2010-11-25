#pragma once

#include "stdafx.h"
#include "../../FaceProcessing/FaceProcessing/MotionDetector.h"


using namespace System;
using namespace System::Runtime::InteropServices;

namespace FaceProcessingWrapper
{
	static public ref class Converter
	{
		public:
			static Frame ToUnManaged(Damany::Imaging::Common::Frame^ managed);
			static CvRect ToNativeRect(OpenCvSharp::CvRect managedRect);
			static OpenCvSharp::CvRect ToManagedRect(CvRect nativeRect);
			

	};
}

