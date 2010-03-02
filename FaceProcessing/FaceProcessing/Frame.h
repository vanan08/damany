#pragma once

#ifdef DLL_EXPORTS
#define DLL_API _declspec(dllexport)
#else
#define DLL_API _declspec(dllimport)
#endif

#include "highgui.h"
#include "cv.h"
#include "omp.h"

struct DLL_API Frame
{
	BYTE cameraID;
	IplImage *image;//ת������Ҫ���������Ĵ�ͼƬ,Ѧ��������
	CvRect searchRect;//�������ķ�Χ��Ѧ��������
	LONGLONG timeStamp;//������

	Frame();
};