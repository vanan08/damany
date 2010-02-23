#pragma once

#include "stdafx.h"
#include "../../FaceProcessing/FaceProcessing/MotionDetector.h"


using namespace System;
using namespace System::Runtime::InteropServices;

namespace FaceProcessingWrapper
{
	static public ref class FrameConverter
	{
		public:
		static Frame ToUnManaged(ImageProcess::Frame^ managed);
		static ImageProcess::Frame^ ToManaged(Frame unmanaged);
		static ImageProcess::Frame^ foo(Frame f);
	};
}

