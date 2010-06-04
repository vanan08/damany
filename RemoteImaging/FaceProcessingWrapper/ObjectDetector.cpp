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

		//����pFrame���뱳������ȷ��SetBkg��FrameProcess����ͬʱ������
		void UpdateBackground( OpenCvSharp::IplImage background )
		{
			pDetector->SetBkg( (IplImage*) background.CvPtr.ToPointer());
		}

		array<OpenCvSharp::CvRect>^ ProcessFrame( IplImage* pFrame )//����ǰ֡
		{
		    return gcnew array<OpenCvSharp::CvRect>(0);
		}
		
		void SetBkgUpRatio( float fUpRatio )//���ñ�������ϵ��
		{
			pDetector->SetBkgUpRatio(fUpRatio);
		}

		void SetROI( OpenCvSharp::CvRect roi )//���ڵ�һ�ε���FrameProcessǰ�������ã���������ò���Ч
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