#pragma once
#include "stdafx.h"
#include "cv.h"
#include "cvaux.h"
#include "cxcore.h"
#include "FaceSelect.h"

int FaceSearch(IplImage* targetImg, CvRect& rect);//找人脸,得到人脸所在位置的框框