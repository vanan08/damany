#pragma once

#include <stdafx.h>
#include "../../FaceProcessing/FaceProcessing/MotionDetector.h"
#include "Converter.h"


using namespace System;
using namespace System::Runtime::InteropServices;

namespace FaceProcessingWrapper
{
	public ref class MotionDetector
	{
		public:
			MotionDetector()
			{
				this->pDetector = new FaceProcessing::CMotionDetector();
			}
			~MotionDetector()
			{
				if (this->pDetector != NULL)
				{
					delete this->pDetector;
					this->pDetector = NULL;
				}
			}

			bool PreProcessFrame(ImageProcess::Frame^ frame, [Out] ImageProcess::Frame^ % lastFrame)
			{
				Frame f = FaceProcessingWrapper::FrameConverter::ToUnManaged(frame);
				Frame frameToDispose;
				bool result = this->pDetector->PreProcessFrame(f, frameToDispose);

			    lastFrame = FaceProcessingWrapper::FrameConverter::ToManaged(frameToDispose);

				return result;
			}

			property System::Drawing::Rectangle ForbiddenRegion
			{
				void set(System::Drawing::Rectangle rect)
				{
					this->pDetector->SetAlarmArea(rect.Left, rect.Top, rect.Right, rect.Bottom, false);
				}
			}


			void SetRectThr(const int fCount, const int gCount)
			{
				this->pDetector->SetRectThr(fCount, gCount);
			}

			property bool DrawMotionRect
			{
				void set(bool value)
				{
					this->pDetector->SetDrawRect(value);
				}
			}


		private:
			FaceProcessing::CMotionDetector *pDetector;
	};
}