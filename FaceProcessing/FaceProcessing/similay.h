#pragma once

#ifdef DLL_EXPORTS
#define DLL_API _declspec(dllexport)
#else
#define DLL_API _declspec(dllimport)
#endif


#include "stdafx.h"
#include "cvgabor.h"
#include "LightCorrect.h"
#include "iostream"
using namespace std;

class similayFace
{
private:
	IplImage* targetImg;//target image for color 
	IplImage* targetResize;//resized target gray face image
	IplImage* targetGabor;//data after Gabor filter
	CvHistogram* targetHist;//target image for histogram
	double targetShape;

	IplImage* sourImg;
	IplImage* destFaceGray;
	IplImage* destFaceResize;
	IplImage* destResult;
	IplImage* destLarge;
	
private:
	void InitTargetFace(IplImage* img, CvRect faceRect);
	void CalcTargetHist();
	void GetCode(double m0, double mi, double p0, double pi, char& ch);
	int GetInt(char res[], int count);
	void Gabor(IplImage* img, IplImage* resImg);
	void InitRect(CvRect& sourImgRect);
	void CmpHistogram(IplImage* destImg, double& result);//compare the histogram of current with target image
	void ImageRotate(IplImage* img, float faceAngle);//rotate image with angle faceAngle

public:
	similayFace(IplImage* img, CvRect faceRect);
	~similayFace();
	bool CmpFaceWith_NoRotate(IplImage* img, CvRect& rect, float& cmpResult, bool noRotate=true); 
	bool CmpFaceWith_Rotate(IplImage* img, CvRect& rect, float& cmpResult);

	//bool CmpFace(IplImage* sourImg, CvRect& sourRect, IplImage* destImg, CvRect& destRect, float& cmpRes, bool noRotate=true);
};

 bool DLL_API CompareFace(IplImage* sourImg, CvRect sourRect, IplImage* destImg, CvRect destRect, float* cmpResult, bool noRotate=true);
