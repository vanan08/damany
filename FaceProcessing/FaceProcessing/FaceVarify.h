/************************************************************************************************************
摘    要：该模块主要用于验证检测到的人脸区域内，是否含有人脸
输    入：检测到的含有人脸面部区域的小图
输    出：true表示输入图片含有人脸，false表示输入图片没有人脸
作    者：薛晓利
完成日期：2010.6.12
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

