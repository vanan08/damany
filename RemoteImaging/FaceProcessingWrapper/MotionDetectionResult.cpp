#pragma once

#include "stdafx.h"

using namespace System;
using namespace System::Runtime::InteropServices;

namespace FaceProcessingWrapper
{
	public ref class MotionDetectionResult
	{
	public:
		property System::Guid FrameGuid
		{
			System::Guid get()
			{
				return this->guid;

			}
			void set(System::Guid value)
			{
				this->guid = value;
			}
		}
		property OpenCvSharp::CvRect MotionRect
		{
			OpenCvSharp::CvRect get()
			{
				return this->rect;

			}
			void set(OpenCvSharp::CvRect value)
			{
				this->rect = value;
			}
		}

	private:
		System::Guid guid;
		OpenCvSharp::CvRect rect;
	};

}