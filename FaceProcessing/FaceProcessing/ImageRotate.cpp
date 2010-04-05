#include "stdafx.h"
#include "ImageRotate.h"
#include "iostream"
using namespace std;

void ImageRotate(IplImage* img, float faceAngle)
{
	IplImage* destImg = cvCreateImage(cvSize(img->width,img->height), 8, img->nChannels);
	CvMat* warpMat = cvCreateMat(2, 3, CV_32FC1); 

	int width = img->width;
	int height = img->height;
	
	float alpha = cos(faceAngle);
	float belta = sin(faceAngle);

	cvSetReal2D(warpMat, 0, 0, alpha);
	cvSetReal2D(warpMat, 0, 1, belta);
	cvSetReal2D(warpMat, 0, 2, width*0.5f);
	cvSetReal2D(warpMat,1, 0, -belta);
	cvSetReal2D(warpMat, 1, 1, alpha);
	cvSetReal2D(warpMat, 1, 2, height*0.5f);

	cvGetQuadrangleSubPix(img, destImg, warpMat);
	cvCopy(destImg, img); 

	cvReleaseImage(&destImg);
	cvReleaseMat(&warpMat);
}
