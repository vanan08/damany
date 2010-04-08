#pragma once

#ifdef DLL_EXPORTS
#define DLL_API _declspec(dllexport)
#else
#define DLL_API _declspec(dllimport)
#endif

#include "highgui.h"
#include "cv.h"
#include "omp.h"

#define GUID_LEN 16

struct DLL_API Frame
{
	IplImage *image;//ת������Ҫ���������Ĵ�ͼƬ,Ѧ��������
	CvRect searchRect;//�������ķ�Χ��Ѧ��������
	BYTE guid[GUID_LEN];

	Frame();
};