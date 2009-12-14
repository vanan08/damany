// MotionDetectWrapper.h

#pragma once

#include "../../OutPut/PreProcess.h"

using namespace System;

namespace MotionDetectWrapper {

	public ref class MotionDetector
	{
		// TODO: Add your methods for this class here.
	public:
		bool DetectFrame(ImageProcess::Frame^ next, ImageProcess::Frame^ last)
		{
			if (next == nullptr)
			{
				throw gcnew System::ArgumentException("Argument is null", "next");
			}

			Frame nextUf;
			Frame lastUf;

			ManagedFrameToUnmanaged(next, nextUf);

			bool result = ::PreProcessFrame(nextUf, lastUf);

			UnmanagedFrameToManaged(lastUf, last);

			return result;
		}


		property bool DrawMotionRect
		{
			void set(bool draw) { ::SetDrawRect(draw); }
		}

		void SetAlarmArea(int leftX, int leftY, int rightX, int rightY, bool markIt)
		{
			::SetAlarmArea(leftX, leftY, rightX, rightY, markIt);

		}

		void SetRectThr(int fCount, int gCount)
		{
			::SetRectThr(fCount, gCount);
		}

	private:
		void ManagedFrameToUnmanaged(ImageProcess::Frame ^managed, Frame &unmanaged)
		{
			unmanaged.cameraID = managed->cameraID;
			unmanaged.image = (::IplImage*) managed->image->CvPtr.ToPointer();

			unmanaged.searchRect.x = managed->searchRect.X;
			unmanaged.searchRect.y = managed->searchRect.Y;
			unmanaged.searchRect.width = managed->searchRect.Width;
			unmanaged.searchRect.height = managed->searchRect.Height;

			unmanaged.timeStamp = managed->timeStamp;
		}

		void UnmanagedFrameToManaged(const Frame &unmanaged, ImageProcess::Frame ^managed)
		{
			managed->cameraID =	unmanaged.cameraID;

			if (unmanaged.image == NULL)
			{
				managed->image = nullptr;
			}
			else
			{
				managed->image = gcnew OpenCvSharp::IplImage( (IntPtr) unmanaged.image);
				managed->image->IsEnabledDispose = false;

			}

			managed->searchRect.X =	unmanaged.searchRect.x;   
			managed->searchRect.Y =	unmanaged.searchRect.y;     
			managed->searchRect.Width =	unmanaged.searchRect.width; 
			managed->searchRect.Height =	unmanaged.searchRect.height;

			managed->timeStamp =	unmanaged.timeStamp;       
		}
	};
}
