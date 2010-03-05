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

			bool PreProcessFrame(Damany::ImageProcessing::Contracts::Frame^ frame, 
				[Out] System::Guid% guidOfFrame, 
				[Out] OpenCvSharp::CvRect% motionRect)
			{
				Frame f = FaceProcessingWrapper::FrameConverter::ToUnManaged(frame);
				Frame frameToDispose;
				bool result = this->pDetector->PreProcessFrame(f, frameToDispose);

				array<System::Byte>^ guidBytes = gcnew array<System::Byte>(16);
				pin_ptr<Byte> pBytes = &guidBytes[0];
				::memcpy_s(pBytes, 16, frameToDispose.guid, 16);

				guidOfFrame = System::Guid(guidBytes);

				motionRect = OpenCvSharp::CvRect(
								frameToDispose.searchRect.x,
								frameToDispose.searchRect.y,
								frameToDispose.searchRect.width,
								frameToDispose.searchRect.height
								);
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