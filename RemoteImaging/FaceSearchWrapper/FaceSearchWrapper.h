// FaceSearchWrapper.h

#pragma once
#include "../../FaceSelDll/FaceSelect.h"
#include "FaceSearchConfiguration.h"

using namespace System;
using namespace System::Runtime::InteropServices;


namespace FaceSearchWrapper {

	public ref class FaceSearch
	{
		// TODO: Add your methods for this class here.
	public:
		FaceSearch::FaceSearch(void);
		FaceSearch::~FaceSearch(void);

		//Michael Add -- 设置类变量的接口
		void SetROI( int x, int y, int width, int height );
		void SetFaceParas( int iMinFace, double dFaceChangeRatio);
		void SetDwSmpRatio( double dRatio );
		void SetOutputDir( const char* dir );

		//SetExRatio : 设置输出的图片应在人脸的四个方向扩展多少比例
		//如果人脸框大小保持不变，4个值都应该为0.0f
		void SetExRatio( double topExRatio, double bottomExRatio, double leftExRatio, double rightExRatio );
		void SetLightMode(int iMode);
		void AddInFrame(Damany::Imaging::Contracts::Frame^ frame);
		array<ImageProcess::Target^>^ SearchFacesFastMode(Damany::Imaging::Contracts::Frame^ frame);
		array<ImageProcess::Target^>^ SearchFaces();
		OpenCvSharp::IplImage^ NormalizeImage(OpenCvSharp::IplImage^ imgIn, OpenCvSharp::CvRect roi);
		array<OpenCvSharp::IplImage^>^ NormalizeImageForTraining(OpenCvSharp::IplImage^ imgIn, OpenCvSharp::CvRect roi);

		property FaceSearchConfiguration^ Configuration
		{
			FaceSearchConfiguration^ get()
			{
				return this->config;
			}

			void set(FaceSearchConfiguration^ cfg)
			{
				this->config = cfg;

				this->pFaceSearch->SetFaceParas(this->config->MinFaceWidth, this->config->FaceWidthRatio);
				this->pFaceSearch->SetExRatio(this->config->TopRation,
					                          this->config->BottomRation,
											  this->config->LeftRation,
											  this->config->RightRation);
				this->pFaceSearch->SetLightMode(this->config->EnvironmentMode);
				this->pFaceSearch->SetROI(this->config->SearchRectangle->Left,
					                      this->config->SearchRectangle->Top,
										  this->config->SearchRectangle->Width,
										  this->config->SearchRectangle->Height);

			}
		}
		

	private:
		::CvRect ManagedRectToUnmanaged(OpenCvSharp::CvRect^ managedRect);
		OpenCvSharp::CvRect UnmanagedRectToManaged(const ::CvRect& unmanaged);
		FaceSearchConfiguration^ config;
		CFaceSelect *pFaceSearch;
	};
}
