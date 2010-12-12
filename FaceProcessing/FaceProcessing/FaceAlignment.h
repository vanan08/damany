#include "stdafx.h"
#include "VJfacedetect.h"
#include "AAM_Util.h"
#include "AAM_Basic.h"
#include "AAM_IC.h"
#include "cv.h"
#include "cxcore.h"
#include "highgui.h"
#include "iostream"
#include "fstream"
using namespace std;

#ifdef DLL_EXPORTS
#define DLL_API _declspec(dllexport)
#else
#define DLL_API _declspec(dllimport)
#endif

namespace Damany {

	namespace Imaging {

		namespace FaceCompare {

class DLL_API FaceAlignment
{
public:
	FaceAlignment(char* modelPath, char* classifierPathm, int featurePointCount=68);
	bool LibFaceAlignment(IplImage *faceImg, IplImage *faceLbpImg, CvPoint featurePt[]);
	bool RealTimeAlignment(IplImage *faceImg, IplImage *faceLbpImg);

private:
	bool Alignment(IplImage* faceImg, IplImage* faceLbpImg, CvPoint featurePt[]=NULL);
	void CheckRange(CvPoint fpt[], int count, int min_x, int min_y, int max_x, int max_y);
	void GetLBPimg(IplImage* scaleImg, IplImage* lbpImg, CvPoint& leftEye);
	float CalcEyeMouthDist(CvPoint leftEye, CvPoint rightEye, CvPoint mouth);
	float CalcEyeDist(CvPoint leftEye, CvPoint rightEye);
	void ShapeRotate(CvPoint fPt[], int fCount, float angle, int originX, int originY);
	void ImageRotate(IplImage* img, CvPoint fPt[]);
	float GetAngle(CvPoint fPt[]);
	void GetEyeMouthPos(CvPoint fPt[], CvPoint& leftEye, CvPoint& rightEye, CvPoint& mouth);
	void RemoveNonFaceArea(CvPoint fPt[], IplImage* img, int fCount);
	void RemoveBackground(IplImage* img, CvRect& faceRect, CvPoint fpt[]);
	void RemoveArea(IplImage* img, CvRect& faceRect, CvPoint& pt1, CvPoint& pt2, bool LEFT=true);
	void ShapeResize(CvPoint fpt[], int fCount, float xScale, float yScale);
	void ShapeNormalize(CvPoint fpt[], int fCount, int minX, int minY);
	int MaxY(CvPoint fPt[], int fCount);
	int MaxX(CvPoint fPt[], int fCount);
	int MinY(CvPoint fPt[], int fCount);
	int MinX(CvPoint fPt[], int fCount);

private:
	AAM_Pyramid model;
	VJfacedetect facedet;
	int fptCount;//AAM feature point count
};

		}
	}
}