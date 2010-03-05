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
			static Frame ToUnManaged(Damany::ImageProcessing::Contracts::Frame^ managed);
	};
}

