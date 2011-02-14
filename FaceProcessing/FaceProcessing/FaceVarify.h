/************************************************************************************************************
ժ    Ҫ����ģ����Ҫ������֤��⵽�����������ڣ��Ƿ�������
��    �룺��⵽�ĺ��������沿�����Сͼ
��    ����true��ʾ����ͼƬ����������false��ʾ����ͼƬû������
��    �ߣ�Ѧ����
������ڣ�2010.6.12
*************************************************************************************************************/
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

namespace Damany {

	namespace Imaging {


class DLL_API FaceVarify
{
public:
	FaceVarify();
	/*******************************************************
	��������IsFaceImg()
	���룺�����������沿�������ͨ����ͼ
	�����true:��ͼƬΪ����ͼƬ��false:��ͼƬ��������ͼƬ
	********************************************************/
	bool IsFaceImg(IplImage* smallFaceImg);

private:
	void SearchLeftEye(IplImage* grayImg, CvRect eyeRect, CvPoint& eyePt);
	void SearchRightEye(IplImage* grayImg, CvRect eyeRect, CvPoint& eyePt);
	void SearchMouth(IplImage* grayImg, CvRect& mouthRect, CvPoint& mouthPt);
	bool JudgeFaceByColor(IplImage* pImg);
	bool SumJudge(IplImage *colorIn);
	bool RGBavgDiffJudge(IplImage *sourImg);

private:
	CvPoint leftEye_pt;
	CvPoint rightEye_pt;
	CvPoint mouth_pt; 
};
		}
}

