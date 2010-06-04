#pragma once

#include <stdafx.h>
#include "../../FaceProcessing/FaceProcessing/ObjDetect.h"


namespace FaceProcessingWrapper 
{

	public ref class ObjectDetector 
	{
	public:
		ObjectDetector()
		{
			this->pDetector = new Kings::Imaging::CObjectDetector();
		}
		~ObjectDetector()
		{
			delete pDetector;
		}

		//设置pFrame进入背景，请确保SetBkg与FrameProcess不会同时被调用
		void UpdateBackground( OpenCvSharp::IplImage background )
		{
			pDetector->SetBkg( (IplImage*) background.CvPtr.ToPointer());
		}

		array<OpenCvSharp::CvRect>^ ProcessFrame( IplImage* pFrame )//处理当前帧
		{
		    return gcnew array<OpenCvSharp::CvRect>(0);
		}
		
		void SetBkgUpRatio( float fUpRatio )//设置背景更新系数
		{
			pDetector->SetBkgUpRatio(fUpRatio);
		}

		void SetROI( OpenCvSharp::CvRect roi )//请在第一次调用FrameProcess前进行设置，否则该设置不生效
		{
			CvRect native;
			native.x = roi.X;
			native.y = roi.Y;
			native.width = roi.Width;
			native.height = roi.Height;

			pDetector->SetROI(native);
		}


		property bool AutoUpdateBackground
		{
			void set(bool enable)
			{
				pDetector->SetUpBkgMode(!enable);
			}
		}

	private:
		Kings::Imaging::CObjectDetector* pDetector;

	};

}