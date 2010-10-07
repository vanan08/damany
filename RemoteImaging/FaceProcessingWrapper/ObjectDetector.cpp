#pragma once

#include <stdafx.h>
#include <cxcore.h>
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
		void UpdateBackground( OpenCvSharp::IplImage^ background )
		{
			pDetector->SetBkg( (IplImage*) background->CvPtr.ToPointer());
		}

		array<OpenCvSharp::CvRect>^ ProcessFrame( OpenCvSharp::IplImage^ frame )//����ǰ֡
		{
			pDetector->FrameProcess( (IplImage*) frame->CvPtr.ToPointer() );
			
			CvSeq *Rects = NULL;
			pDetector->GetObjRects( Rects );

			if (Rects == NULL)
			{
				return gcnew array<OpenCvSharp::CvRect>(0);
			}

			array<OpenCvSharp::CvRect>^ managed = gcnew array<OpenCvSharp::CvRect>(Rects->total);
			for (int i=0; i<Rects->total; ++i)
			{
				CvRect *native = (CvRect*) cvGetSeqElem( Rects, i);
				OpenCvSharp::CvRect mr;
				mr.X      = native->x;
				mr.Y      = native->y;
				mr.Width  = native->width;
				mr.Height = native->height;

				managed[i] = mr;
			}

		    return managed;
		}
		
		void SetBkgUpRatio( float fUpRatio )//���ñ�������ϵ��
		{
			pDetector->SetBkgUpRatio(fUpRatio);
		}

		void SetROI( OpenCvSharp::CvRect roi )//���ڵ�һ�ε���FrameProcessǰ�������ã���������ò���Ч
		{
			CvRect native;
			native.x      = roi.X;
			native.y      = roi.Y;
			native.width  = roi.Width;
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