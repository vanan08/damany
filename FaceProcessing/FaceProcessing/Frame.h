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
	IplImage *image;//转换后需要搜索人脸的大图片,薛晓利给出
	CvRect searchRect;//搜索脸的范围，薛晓利给出
	BYTE guid[16];

	Frame();
};