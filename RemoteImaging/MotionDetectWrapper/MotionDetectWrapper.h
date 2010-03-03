// MotionDetectWrapper.h

#pragma once

#include "../../PreProcess/PreProcess.h"

using namespace System;

namespace MotionDetectWrapper {

	public ref class MotionDetector
	{
		// TODO: Add your methods for this class here.
	public:
		

		
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
