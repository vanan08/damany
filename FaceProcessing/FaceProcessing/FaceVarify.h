/************************************************************************************************************
摘    要：用于验证抓拍得到的彩色图片中是否含有人脸
作    者：薛晓利
完成日期：2010-4-22
*************************************************************************************************************/
#pragma once

#include "stdafx.h"
#include "cv.h"
#include "cxcore.h"
#include "cvaux.h"

#ifdef DLL_EXPORTS
#define DLL_API _declspec(dllexport)
#else
#define DLL_API _declspec(dllimport) 
#endif

namespace Damany 
{
	namespace Imaging 
	{
		class DLL_API FaceVarify
		{
		public:
			FaceVarify();
			bool IsFaceImg(IplImage* smallFaceImg);

		private:
			void SearchLeftEye(IplImage* grayImg, CvRect eyeRect, CvPoint& eyePt);
			void SearchRightEye(IplImage* grayImg, CvRect eyeRect, CvPoint& eyePt);
			void SearchMouth(IplImage* grayImg, CvRect& mouthRect, CvPoint& mouthPt);

		private:
			CvPoint leftEye_pt;
			CvPoint rightEye_pt;
			CvPoint mouth_pt; 
		};
	}
}

