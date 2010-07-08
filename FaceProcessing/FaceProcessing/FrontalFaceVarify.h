/*********************************************************************************************
ժ    Ҫ���жϵ�ǰ����ͼƬ���Ƿ�����������
��    �ߣ���ܰ� Ѧ����
�������:2010-4-22
********************************************************************************************/
#pragma once

#include "stdafx.h"
#include "cv.h"
#include "cxcore.h"
#include "cvaux.h"
#include "highgui.h" 

#ifdef DLL_EXPORTS
#define DLL_API _declspec(dllexport)
#else
#define DLL_API _declspec(dllimport) 
#endif

namespace Damany 
{
	namespace Imaging 
	{
		class DLL_API FrontalFaceVarify
		{
		public:
			FrontalFaceVarify(IplImage* templateEye);
			~FrontalFaceVarify();
			bool IsFrontal(IplImage* smallFaceImg);

		private:
			bool RectSymmetry(IplImage* colorImg);
			int plotbox(IplImage* Tresult, IplImage* target, IplImage* templat, CvMat* M);
			int tmp(IplImage* templat, IplImage* target, IplImage* Tresult);
			int Threshold(IplImage* Tresult, int* T);
			int Detection(IplImage* Tresult, IplImage* eye, int* T, float kw, float kh);
			int FrontFaceCenter(IplImage* P,int* center_x,int* center_y, float* column_row_k);
			int FrontFace(IplImage* eye, float t1, float t2, float t3, int* Front);
			bool IsSymmetry(IplImage* grayImg);

		private:
			IplImage* Im1;
		};
	}
}

