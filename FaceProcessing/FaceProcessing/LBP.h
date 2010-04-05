#include "stdafx.h"
#include "cv.h"
#include "cxcore.h"

#ifdef DLL_EXPORTS
#define DLL_API _declspec(dllexport)
#else
#define DLL_API _declspec(dllimport)
#endif

namespace Damany {

	namespace Imaging {

		namespace FaceCompare {

class DLL_API LBP
{

public:
	LBP(IplImage* img, CvRect& faceRect);
	~LBP(); 
	bool CmpFace(IplImage* destImg, CvRect& destRect, float& score); 
	void SetThreshold(int value);

private:
	int threshold;

	float weightCoeff[49];
	float** targetHst;
	int widthsize; 
	int heightsize;	
	int blockwidth;
	int blockheight;	
	int flagwidth;
	int flagheight;
	int num1;
	int num2;

	void SetCoeff();
	int GammaCorrect(IplImage* src, IplImage* dst, double low, double high, double bottom, double top, double gamma);
	void CalcKernel(int nFWHM1, CvMat* pMat, float sigma);
	void MultiRetinex(IplImage* src, IplImage* dst);
	void SsRetinex(const IplImage* src, IplImage* dst);
	void Judge01(IplImage* src, IplImage* dst);
	void GetROI(IplImage* img, IplImage* pImg[], IplImage* pc, int width, int height);
	void SetZero(IplImage* pImg[], int n);
	int RotateInvariantLBP(IplImage* img, float* hst);
	int LBP243(IplImage* imgDst, int blockwidth, int blockheight, int flagwidth, int flagheight, float* hst[]);
	float CalcDistance(float** hstPro, int num1, int num2, float weight[]);
	void CalcBlackLBP(IplImage* img, float** hst);
	float CalcFace(IplImage* destImg);
	void CalcAvg(float** arr, int num1, int num2);

};

		}
	}
}