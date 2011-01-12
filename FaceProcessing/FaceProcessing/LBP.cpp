#include "stdafx.h"
#include "LBP.h"

Damany::Imaging::FaceCompare::LBP::LBP(int width, int height, int blockWidth, int blockHeight, int dim)
{
    threshold = 40;
	m_widthSize = width;  
	m_heightSize = height;	 
	m_blockWidth = blockWidth;  
	m_blockHeight = blockHeight;	
	m_widthBlockCount = m_widthSize/m_blockWidth;
	m_heightBlockCount = m_heightSize/m_blockHeight;
	m_totalBlock = m_heightBlockCount*m_widthBlockCount;
	m_blockDim = dim;

	SetCoeff();
	m_faceCount = 0; 
	enableMultiRetinex = true;

	targetHst = NULL;
}

void Damany::Imaging::FaceCompare::LBP::EnableMultiRetinex(bool flag)
{
	enableMultiRetinex = flag;
}

void Damany::Imaging::FaceCompare::LBP::SetThreshold(int val)
{
	threshold = val;
}

Damany::Imaging::FaceCompare::LBP::~LBP()
{
	if (targetHst != NULL)
	{
		cvReleaseMat(&targetHst);
	}
}

void Damany::Imaging::FaceCompare::LBP::SetCoeff()
{
	weightCoeff[0] = 2;
	weightCoeff[1] = 1;
	weightCoeff[2] = 1;
	weightCoeff[3] = 1;
	weightCoeff[4] = 1;
	weightCoeff[5] = 1;
	weightCoeff[6] = 2;
	weightCoeff[7] = 2;
	weightCoeff[8] = 4;
	weightCoeff[9] = 4;
	weightCoeff[10] = 1;
	weightCoeff[11] = 4;
	weightCoeff[12] = 4;
	weightCoeff[13] = 2;
	weightCoeff[14] = 1;
	weightCoeff[15] = 1;
	weightCoeff[16] = 1;
	weightCoeff[17] = 0;
	weightCoeff[18] = 1;
	weightCoeff[19] = 1;
	weightCoeff[20] = 1;
	weightCoeff[21] = 0;
	weightCoeff[22] = 1;
	weightCoeff[23] = 1;
	weightCoeff[24] = 0;
	weightCoeff[25] = 1;
	weightCoeff[26] = 1;
	weightCoeff[27] = 0;
	weightCoeff[28] = 0;
	weightCoeff[29] = 1;
	weightCoeff[30] = 1;
	weightCoeff[31] = 1;
	weightCoeff[32] = 1;
	weightCoeff[33] = 1;
	weightCoeff[34] = 0;
	weightCoeff[35] = 0;
	weightCoeff[36] = 1;
	weightCoeff[37] = 1;
	weightCoeff[38] = 2;
	weightCoeff[39] = 1;
	weightCoeff[40] = 1;
	weightCoeff[41] = 0;
	weightCoeff[42] = 0;
	weightCoeff[43] = 1;
	weightCoeff[44] = 1;
	weightCoeff[45] = 1;
	weightCoeff[46] = 1;
	weightCoeff[47] = 1;
	weightCoeff[48] = 0;
};

//int Damany::Imaging::FaceCompare::LBP::GammaCorrect(IplImage* src, IplImage* dst, double low, double high, double bottom, double top, double gamma)
//{
//	if(low<0 || low>1 || high <0 || high>1 || bottom<0 || bottom>1 || top<0 && top>1 || low>high)
//	{
//		return -1;
//	}
//
//	double low2 = low*255;
//	double high2 = high*255;
//	double bottom2 = bottom*255;
//	double top2 = top*255;
//	double err_in = high2 - low2;
//	double err_out = top2 - bottom2;
//
//	int height = src->height;
//	int width = src->width;
//	int step = src->widthStep;
//	uchar* srcData = (uchar*)src->imageData;
//	uchar* destData = (uchar*)dst->imageData;
//
//	double val;
//	for(int y=0; y<height; y++)
//	{
//		for (int x=0; x<width; x++)
//		{
//			val = srcData[y*step+x];
//			val = pow((val - low2)/err_in, gamma) * err_out + bottom2;
//			val = val>255 ? 255:val;
//			val = val<0 ? 0:val;
//			destData[y*step+x] = (uchar)val;
//		}
//	}
//	return 0;
//}

//void Damany::Imaging::FaceCompare::LBP::CalcKernel(int nFWHM1, CvMat* pMat, float sigma)
//{
//	for (int i=0;i<2*nFWHM1-1;i++)
//	{
//		for (int j=0;j<2*nFWHM1-1;j++)
//		{
//			double fp=exp(-((i-nFWHM1)*(i-nFWHM1)+(j-nFWHM1)*(j-nFWHM1))/(4*sigma))/(CV_PI*sigma);
//			cvmSet(pMat,i,j,fp);
//		}
//	}
//	CvScalar sc_sum=cvSum(pMat);
//	float fSum=sc_sum.val[0];
//
//	for (int i=0;i<2*nFWHM1-1;i++)
//	{
//		for (int j=0;j<2*nFWHM1-1;j++)
//		{
//			double fp=exp(-((i-nFWHM1)*(i-nFWHM1)+(j-nFWHM1)*(j-nFWHM1))/(4*sigma))/(CV_PI*sigma);
//			fp=fp/fSum;
//			cvmSet(pMat, i, j, fp);
//		}
//	}
//}

//void Damany::Imaging::FaceCompare::LBP::MultiRetinex(IplImage* src, IplImage* dst)
//{
//	IplImage* img[14];
//	for (int i=0; i<4; i++)
//	{
//		img[i] = cvCloneImage(src);
//	}
//	for (int i=4; i<14; i++)
//	{
//		img[i] = cvCreateImage(cvGetSize(src),IPL_DEPTH_32F, 1);
//	}
//
//	int nTemplate1=161;
//	float fSigma1=1458;
//	int nFWHM1=(nTemplate1+1)/2;
//	CvMat* P1=cvCreateMat(2*nFWHM1-1, 2*nFWHM1-1, CV_32FC1);
//	CalcKernel(nFWHM1, P1, fSigma1);
//	cvFilter2D(img[0],img[1],P1,cvPoint(-1,-1));
//
//	int nTemplate2=31;
//	float fSigma2=53.38f;
//	int nFWHM2=(nTemplate2+1)/2;
//	CvMat* P2=cvCreateMat(2*nFWHM2-1, 2*nFWHM2-1, CV_32FC1);
//	CalcKernel(nFWHM2, P2, fSigma2);
//	cvFilter2D(img[0],img[2],P2,cvPoint(-1,-1));
//
//	int nTemplate3=11;
//	float fSigma3=8;
//	int nFWHM3=(nTemplate3+1)/2;
//	CvMat* P3=cvCreateMat(2*nFWHM3-1, 2*nFWHM3-1, CV_32FC1);
//	CalcKernel(nFWHM3, P3, fSigma3);
//	cvFilter2D(img[0],img[3],P3,cvPoint(-1,-1));
//
//	for (int i=0; i<4; i++)
//	{
//		cvConvert(img[i], img[i+4]);
//		cvLog(img[i+4], img[i+4]);
//	}
//
//	float fLight=1.1f;
//	cvConvertScale(img[4],img[4],fLight);
//
//	for (int i=5; i<8; i++)
//	{
//		cvAbsDiff(img[4], img[i], img[i+3]);
//	}
//
//	cvAdd(img[8],img[9],img[11]);
//	cvAdd(img[11],img[10],img[12]);
//
//	cvConvertScale(img[12],img[13],0.3);
//	cvConvertScale(img[13],img[13],255);
//	cvConvert(img[13], img[0]);
//
//	GammaCorrect( img[0], dst, 0.1,0.9, 0.00, 1.0, 1.0);
//
//	for (int i=0; i<14; i++)
//	{
//		cvReleaseImage(&img[i]);
//	}
//	cvReleaseMat(&P1);
//	cvReleaseMat(&P2);
//	cvReleaseMat(&P3);
//}

//void Damany::Imaging::FaceCompare::LBP::SsRetinex(const IplImage* src, IplImage* dst)
//{
//	IplImage *img1 = 0;
//	IplImage *img2 = 0;
//	IplImage *img3 = 0;
//
//	img1=cvCloneImage(src);
//	img2=cvCloneImage(img1);
//	img3=cvCloneImage(img1);
//	float fLight=1.1f;
//	int nTemplate=80;
//	float fSigma=70;
//	int nFWHM=3*nTemplate;
//	CvMat* P=cvCreateMat(2*nFWHM+1, 2*nFWHM+1, CV_32FC1);
//	CalcKernel(nFWHM, P, fSigma);
//	cvFilter2D(img1,img2,P,cvPoint(-1,-1));
//	img3=cvCloneImage(img2);
//	CvMat* mImg2=cvCreateMat(img3->width, img3->height, CV_32FC1);
//	CvMat* mImg1=cvCreateMat(img1->width, img1->height, CV_32FC1);
//	CvMat* mImg3=cvCreateMat(img1->width, img1->height, CV_32FC1);
//
//	cvConvert(img3,mImg3);
//	cvLog(mImg3,mImg3);
//
//	cvConvert(img1,mImg1);
//	cvConvert(img2,mImg2);
//	cvLog(mImg1,mImg2);
//	cvConvertScale(mImg2,mImg2,fLight);
//	cvAbsDiff(mImg2,mImg3,mImg3);
//
//	cvConvertScale(mImg3,mImg3,255);	
//	cvConvert(mImg3,img3);	
//	GammaCorrect( img3, dst, 0.1,0.9, 0.00, 1.0, 1.0);
//
//	cvReleaseImage(&img1);
//	cvReleaseImage(&img2);
//	cvReleaseImage(&img3);	
//
//	cvReleaseMat(&mImg1);
//	cvReleaseMat(&mImg2);
//	cvReleaseMat(&mImg3);
//	cvReleaseMat(&P);
//}

void Damany::Imaging::FaceCompare::LBP::Judge01(IplImage* src, IplImage* dst)//src,dst均为8U
{
	CvScalar tempVal;
	tempVal.val[0]=1;
	cvAndS(src, tempVal, dst, NULL);
}

void Damany::Imaging::FaceCompare::LBP::GetROI(IplImage* img, IplImage* pImg[], 
	IplImage* pc, int width, int height)
{
	cvSetImageROI(pImg[0], cvRect(6,4,width,height));
	cvCopy(img,pImg[0]);
	cvResetImageROI(pImg[0]);

	cvSetImageROI(pImg[1], cvRect(5,4,width,height));
	cvCopy(img,pImg[1]);
	cvResetImageROI(pImg[1]);

	cvSetImageROI(pImg[4], cvRect(6,3,width,height));
	cvCopy(img,pImg[4]);
	cvResetImageROI(pImg[4]);

	cvSetImageROI(pImg[5], cvRect(5,3,width,height));
	cvCopy(img,pImg[5]);
	cvResetImageROI(pImg[5]);

	cvSetImageROI(pc, cvRect(3,3,width,height));
	cvCopy(img, pc);
	cvResetImageROI(pc);
}

void Damany::Imaging::FaceCompare::LBP::SetZero(IplImage* pImg[], int n)
{
	for (int i=0; i<n; i++)
	{
		cvSetZero(pImg[i]);
	}
}

int Damany::Imaging::FaceCompare::LBP::BlockRotateInvariantLBP(IplImage* img, float* hst)
{
	float w1 = (cos(CV_PI/12)*3-2)*(sin(CV_PI/12)*3);
	float w2 = (3-cos(CV_PI/12)*3)*(sin(CV_PI/12)*3); 
	float w3 = (cos(CV_PI/12)*3-2)*(1-sin(CV_PI/12)*3); 
	float w4 = (3-cos(CV_PI/12)*3)*(1-sin(CV_PI/12)*3);
	float w5 = (cos(CV_PI/6)*3-2)/2; 
	float w6 = (3-cos(CV_PI/6)*3)/2; 
	float w7 = (3-cos(CV_PI/8)*3)*(3-cos(CV_PI/8)*3); 
	float w8 = (cos(CV_PI/8)*3-2)*(3-cos(CV_PI/8)*3); 
	float w9 = (cos(CV_PI/8)*3-2)*(cos(CV_PI/8)*3-2);
	
	int height = img->height;
	int width = img->width;

	IplImage* xImg = cvCreateImage(cvSize(width+6, height+6), 8, 1);
	cvSetZero(xImg);

	IplImage* pImg[8];
	for (int i=0; i<8; i++)
	{
		pImg[i] = cvCreateImage(cvSize(width+6, height+6), 8, 1);
	}
	SetZero(pImg, 8);

	IplImage* pc=cvCreateImage(cvSize(width+6, height+6), 8, 1);
	cvSetZero(pc);

	GetROI(img, pImg, pc, width, height);

	IplImage* xI[3];
	for (int i=0; i<3; i++)
	{
		xI[i] = cvCreateImage(cvSize(width+6, height+6), 8, 1);
		cvSetZero(xI[i]);
	}

	IplImage* temp2 = cvCreateImage(cvSize(width+6, height+6), 8, 1);
	cvSetZero(temp2);

	cvCmp(pImg[4], pc, xI[0], CV_CMP_GE);
	Judge01(xI[0], xI[0]);

	IplImage* pTemp[4];
	for (int i=0; i<4; i++)
	{
		pTemp[i] = cvCreateImage(cvSize(width+6, height+6), 8, 1);
		cvSetZero(pTemp[i]);
	}

	cvAddWeighted(pImg[0], w1, pImg[1], w2, 0, pTemp[0]);
	cvAddWeighted(pImg[4], w3, pImg[5], w4, 0, pTemp[1]);
	cvAdd(pTemp[0], pTemp[1], pTemp[2], 0); 
	cvCmp(pTemp[2], pc, xI[1], CV_CMP_GE);
	Judge01(xI[1], xI[1]);

	cvCmp(xI[0], xI[1], temp2, CV_CMP_NE);
	Judge01(temp2, temp2);

	cvSetImageROI(pImg[2], cvRect(6, 5, width, height));
	cvCopy(img, pImg[2]);
	cvResetImageROI(pImg[2]);

	cvSetImageROI(pImg[3], cvRect(5, 5, width, height));
	cvCopy(img, pImg[3]);
	cvResetImageROI(pImg[3]);

	SetZero(pTemp, 3);

	cvAddWeighted(pImg[0], w5, pImg[2], w5, 0, pTemp[0]);
	cvAddWeighted(pImg[1], w6, pImg[3], w6, 0, pTemp[1]);
	cvAdd(pTemp[0], pTemp[1], pTemp[2], 0);
	cvCmp(pTemp[2], pc,xI[2], CV_CMP_GE);
	Judge01(xI[2], xI[2]);

	cvAdd(xI[0],xI[1],pTemp[3],0);
	cvAdd(pTemp[3],xI[2],xImg,0);

	cvSetZero(pTemp[3]);
	cvCmp(xI[2],xI[1],pTemp[3],CV_CMP_NE);
	Judge01(pTemp[3], pTemp[3]);
	cvAdd(pTemp[3],temp2,temp2,0);

	SetZero(pTemp, 4);

	cvSetImageROI(pImg[0], cvRect(6,6,width,height));
	cvCopy(img,pImg[0]);
	cvResetImageROI(pImg[0]);

	cvSetImageROI(pImg[1], cvRect(5,6,width,height));
	cvCopy(img,pImg[1]);
	cvResetImageROI(pImg[1]);

	cvAddWeighted(pImg[0],w7,pImg[1],w8,0,pTemp[0]);
	cvAddWeighted(pImg[2],w8,pImg[3],w9,0,pTemp[1]);
	cvAdd(pTemp[0],pTemp[1],pTemp[2],0);
	cvCmp(pTemp[2],pc,xI[1],CV_CMP_GE);
	Judge01(xI[1], xI[1]);

	cvAdd(xImg,xI[1],xImg,0);
	cvCmp(xI[2],xI[1],pTemp[3],CV_CMP_NE);
	Judge01(pTemp[3],pTemp[3]);
	cvAdd(pTemp[3],temp2,temp2,0);

	SetZero(pTemp, 4);

	cvSetImageROI(pImg[0], cvRect(4,6,width,height));
	cvCopy(img,pImg[0]);
	cvResetImageROI(pImg[0]);

	cvSetImageROI(pImg[2], cvRect(4,5,width,height));
	cvCopy(img,pImg[2]);
	cvResetImageROI(pImg[2]);

	cvAddWeighted(pImg[0],w5,pImg[1],w5,0,pTemp[0]);
	cvAddWeighted(pImg[2],w6,pImg[3],w6,0,pTemp[1]);
	cvAdd(pTemp[0],pTemp[1],pTemp[2],0);
	cvCmp(pTemp[2],pc,xI[2],CV_CMP_GE);
	Judge01(xI[2], xI[2]);

	cvAdd(xImg,xI[2],xImg,0);
	cvCmp(xI[2],xI[1],pTemp[3],CV_CMP_NE);
	Judge01(pTemp[3],pTemp[3]);
	cvAdd(pTemp[3],temp2,temp2,0);

	SetZero(pTemp, 4);

	cvSetImageROI(pImg[1], cvRect(3,6,width,height));
	cvCopy(img,pImg[1]);
	cvResetImageROI(pImg[1]);

	cvSetImageROI(pImg[3], cvRect(3,5,width,height));
	cvCopy(img,pImg[3]);
	cvResetImageROI(pImg[3]);

	cvAddWeighted(pImg[0],w1,pImg[2],w2,0,pTemp[0]);
	cvAddWeighted(pImg[1],w3,pImg[3],w4,0,pTemp[1]);
	cvAdd(pTemp[0],pTemp[1],pTemp[2],0);
	cvCmp(pTemp[2],pc,xI[1],CV_CMP_GE);
	Judge01(xI[1], xI[1]);

	cvAdd(xImg,xI[1],xImg,0);
	cvCmp(xI[2],xI[1],pTemp[3],CV_CMP_NE);
	Judge01(pTemp[3],pTemp[3]);
	cvAdd(pTemp[3],temp2,temp2,0);

	cvCmp(pImg[1],pc,xI[2],CV_CMP_GE);
	cvAdd(xImg,xI[2],xImg,0);
	cvCmp(xI[2],xI[1],pTemp[3],CV_CMP_NE);
	Judge01(pTemp[3],pTemp[3]);
	cvAdd(pTemp[3],temp2,temp2,0);

	SetZero(pTemp, 3);

	cvSetImageROI(pImg[0], cvRect(2,6,width,height));
	cvCopy(img,pImg[0]);
	cvResetImageROI(pImg[0]);

	cvSetImageROI(pImg[2], cvRect(2,5,width,height));
	cvCopy(img,pImg[2]);
	cvResetImageROI(pImg[2]);

	cvAddWeighted(pImg[0],w1,pImg[2],w2,0,pTemp[0]);
	cvAddWeighted(pImg[1],w3,pImg[3],w4,0,pTemp[1]);
	cvAdd(pTemp[0],pTemp[1],pTemp[2],0);
	cvCmp(pTemp[2],pc,xI[1],CV_CMP_GE);
	Judge01(xI[1],xI[1]);

	cvAdd(xImg,xI[1],xImg,0);
	cvCmp(xI[2],xI[1],pTemp[3],CV_CMP_NE);
	Judge01(pTemp[3],pTemp[3]);
	cvAdd(pTemp[3],temp2,temp2,0);

	SetZero(pTemp, 4);

	cvSetImageROI(pImg[1], cvRect(1,6,width,height));
	cvCopy(img,pImg[1]);
	cvResetImageROI(pImg[1]);

	cvSetImageROI(pImg[3], cvRect(1,5,width,height));
	cvCopy(img,pImg[3]);
	cvResetImageROI(pImg[3]);

	cvAddWeighted(pImg[0],w5,pImg[1],w5,0,pTemp[0]);
	cvAddWeighted(pImg[2],w6,pImg[3],w6,0,pTemp[1]);
	cvAdd(pTemp[0],pTemp[1],pTemp[2],0);
	cvCmp(pTemp[2],pc,xI[2],CV_CMP_GE);
	Judge01(xI[2],xI[2]);
	cvAdd(xI[2],xImg,xImg,0);
	cvCmp(xI[2],xI[1],pTemp[3],CV_CMP_NE);
	Judge01(pTemp[3],pTemp[3]);
	cvAdd(pTemp[3],temp2,temp2,0);

	SetZero(pTemp, 4);

	cvSetImageROI(pImg[0], cvRect(0,6,width,height));
	cvCopy(img,pImg[0]);
	cvResetImageROI(pImg[0]);

	cvSetImageROI(pImg[2], cvRect(0,5,width,height));
	cvCopy(img,pImg[2]);
	cvResetImageROI(pImg[2]);

	cvAddWeighted(pImg[0],w7,pImg[2],w8,0,pTemp[0]);
	cvAddWeighted(pImg[1],w8,pImg[3],w9,0,pTemp[1]);
	cvAdd(pTemp[0],pTemp[1],pTemp[2],0);
	cvCmp(pTemp[2],pc,xI[1],CV_CMP_GE);
	Judge01(xI[1],xI[1]);
	cvAdd(xI[1],xImg,xImg,0);
	cvCmp(xI[2],xI[1],pTemp[3],CV_CMP_NE);
	Judge01(pTemp[3],pTemp[3]);
	cvAdd(pTemp[3],temp2,temp2,0);

	SetZero(pTemp, 4);

	cvSetImageROI(pImg[0], cvRect(1,4,width,height));
	cvCopy(img,pImg[0]);
	cvResetImageROI(pImg[0]);

	cvSetImageROI(pImg[1], cvRect(0,4,width,height));
	cvCopy(img,pImg[1]);
	cvResetImageROI(pImg[1]);

	cvAddWeighted(pImg[1],w5,pImg[2],w5,0,pTemp[0]);
	cvAddWeighted(pImg[0],w6,pImg[3],w6,0,pTemp[1]);
	cvAdd(pTemp[0],pTemp[1],pTemp[2],0);
	cvCmp(pTemp[2],pc,xI[2],CV_CMP_GE);
	Judge01(xI[2],xI[2]);
	cvAdd(xI[2],xImg,xImg,0);
	cvCmp(xI[2],xI[1],pTemp[3],CV_CMP_NE);
	Judge01(pTemp[3],pTemp[3]);
	cvAdd(pTemp[3],temp2,temp2,0);

	SetZero(pTemp, 4);

	cvSetImageROI(pImg[2], cvRect(1,3,width,height));
	cvCopy(img,pImg[2]);
	cvResetImageROI(pImg[2]);

	cvSetImageROI(pImg[3], cvRect(0,3,width,height));
	cvCopy(img,pImg[3]);
	cvResetImageROI(pImg[3]);

	cvAddWeighted(pImg[0],w2,pImg[1],w1,0,pTemp[0]);
	cvAddWeighted(pImg[2],w4,pImg[3],w3,0,pTemp[1]);
	cvAdd(pTemp[0],pTemp[1],pTemp[2],0);
	cvCmp(pTemp[2],pc,xI[1],CV_CMP_GE);
	Judge01(xI[1],xI[1]);
	cvAdd(xI[1],xImg,xImg,0);
	cvCmp(xI[2],xI[1],pTemp[3],CV_CMP_NE);
	Judge01(pTemp[3],pTemp[3]);
	cvAdd(pTemp[3],temp2,temp2,0);

	cvCmp(pImg[3],pc,xI[2],CV_CMP_GE);
	Judge01(xI[2],xI[2]);
	cvAdd(xImg,xI[2],xImg,0);
	cvCmp(xI[2],xI[1],pTemp[3],CV_CMP_NE);
	Judge01(pTemp[3],pTemp[3]);
	cvAdd(pTemp[3],temp2,temp2,0);

	SetZero(pTemp, 4);

	cvSetImageROI(pImg[0], cvRect(1,2,width,height));
	cvCopy(img,pImg[0]);
	cvResetImageROI(pImg[0]);

	cvSetImageROI(pImg[1], cvRect(0,2,width,height));
	cvCopy(img,pImg[1]);
	cvResetImageROI(pImg[1]);

	cvAddWeighted(pImg[0],w2,pImg[1],w1,0,pTemp[0]);
	cvAddWeighted(pImg[2],w4,pImg[3],w3,0,pTemp[1]);
	cvAdd(pTemp[0],pTemp[1],pTemp[2],0);
	cvCmp(pTemp[2],pc,xI[1],CV_CMP_GE);
	Judge01(xI[1],xI[1]);
	cvAdd(xI[1],xImg,xImg,0);
	cvCmp(xI[2],xI[1],pTemp[3],CV_CMP_NE);
	Judge01(pTemp[3],pTemp[3]);
	cvAdd(pTemp[3],temp2,temp2,0);

	SetZero(pTemp, 4);

	cvSetImageROI(pImg[2], cvRect(1,1,width,height));
	cvCopy(img,pImg[2]);
	cvResetImageROI(pImg[2]);

	cvSetImageROI(pImg[3], cvRect(0,1,width,height));
	cvCopy(img,pImg[3]);
	cvResetImageROI(pImg[3]);

	cvAddWeighted(pImg[1],w5,pImg[3],w5,0,pTemp[0]);
	cvAddWeighted(pImg[0],w6,pImg[2],w6,0,pTemp[1]);
	cvAdd(pTemp[0],pTemp[1],pTemp[2],0);
	cvCmp(pTemp[2],pc,xI[2],CV_CMP_GE);
	Judge01(xI[2],xI[2]);
	cvAdd(xI[2],xImg,xImg,0);
	cvCmp(xI[2],xI[1],pTemp[3],CV_CMP_NE);
	Judge01(pTemp[3],pTemp[3]);
	cvAdd(pTemp[3],temp2,temp2,0);

	SetZero(pTemp, 4);

	cvSetImageROI(pImg[0], cvRect(1,0,width,height));
	cvCopy(img,pImg[0]);
	cvResetImageROI(pImg[0]);

	cvSetImageROI(pImg[1], cvRect(0,0,width,height));
	cvCopy(img,pImg[1]);
	cvResetImageROI(pImg[1]);

	cvAddWeighted(pImg[1],w7,pImg[0],w8,0,pTemp[0]);
	cvAddWeighted(pImg[3],w8,pImg[2],w9,0,pTemp[1]);
	cvAdd(pTemp[0],pTemp[1],pTemp[2],0);
	cvCmp(pTemp[2],pc,xI[1],CV_CMP_GE);
	Judge01(xI[1],xI[1]);
	cvAdd(xI[1],xImg,xImg,0);
	cvCmp(xI[2],xI[1],pTemp[3],CV_CMP_NE);
	Judge01(pTemp[3],pTemp[3]);
	cvAdd(pTemp[3],temp2,temp2,0);

	SetZero(pTemp, 4);

	cvSetImageROI(pImg[3], cvRect(2,1,width,height));
	cvCopy(img,pImg[3]);
	cvResetImageROI(pImg[3]);

	cvSetImageROI(pImg[1], cvRect(2,0,width,height));
	cvCopy(img,pImg[1]);
	cvResetImageROI(pImg[1]);

	cvAddWeighted(pImg[1],w5,pImg[0],w5,0,pTemp[0]);
	cvAddWeighted(pImg[2],w6,pImg[3],w6,0,pTemp[1]);
	cvAdd(pTemp[0],pTemp[1],pTemp[2],0);
	cvCmp(pTemp[2],pc,xI[2],CV_CMP_GE);
	Judge01(xI[2],xI[2]);
	cvAdd(xI[2],xImg,xImg,0);
	cvCmp(xI[2],xI[1],pTemp[3],CV_CMP_NE);
	Judge01(pTemp[3],pTemp[3]);
	cvAdd(pTemp[3],temp2,temp2,0);

	SetZero(pTemp, 4);

	cvSetImageROI(pImg[2], cvRect(3,1,width,height));
	cvCopy(img,pImg[2]);
	cvResetImageROI(pImg[2]);

	cvSetImageROI(pImg[0], cvRect(3,0,width,height));
	cvCopy(img,pImg[0]);
	cvResetImageROI(pImg[0]);

	cvAddWeighted(pImg[0],w3,pImg[1],w1,0,pTemp[0]);
	cvAddWeighted(pImg[2],w4,pImg[3],w2,0,pTemp[1]);
	cvAdd(pTemp[0],pTemp[1],pTemp[2],0);
	cvCmp(pTemp[2],pc,xI[1],CV_CMP_GE);
	Judge01(xI[1],xI[1]);
	cvAdd(xI[1],xImg,xImg,0);
	cvCmp(xI[2],xI[1],pTemp[3],CV_CMP_NE);
	Judge01(pTemp[3],pTemp[3]);
	cvAdd(pTemp[3],temp2,temp2,0);

	cvCmp(pImg[0],pc,xI[2],CV_CMP_GE);
	Judge01(xI[2],xI[2]);
	cvAdd(xImg,xI[2],xImg,0);
	cvCmp(xI[2],xI[1],pTemp[3],CV_CMP_NE);
	Judge01(pTemp[3],pTemp[3]);
	cvAdd(pTemp[3],temp2,temp2,0);

	SetZero(pTemp, 4);

	cvSetImageROI(pImg[3], cvRect(4,1,width,height));
	cvCopy(img,pImg[3]);
	cvResetImageROI(pImg[3]);

	cvSetImageROI(pImg[1], cvRect(4,0,width,height));
	cvCopy(img,pImg[1]);
	cvResetImageROI(pImg[1]);

	cvAddWeighted(pImg[0],w3,pImg[1],w1,0,pTemp[0]);
	cvAddWeighted(pImg[2],w4,pImg[3],w2,0,pTemp[1]);
	cvAdd(pTemp[0],pTemp[1],pTemp[2],0);
	cvCmp(pTemp[2],pc,xI[1],CV_CMP_GE);
	Judge01(xI[1],xI[1]);
	cvAdd(xI[1],xImg,xImg,0);
	cvCmp(xI[2],xI[1],pTemp[3],CV_CMP_NE);
	Judge01(pTemp[3],pTemp[3]);
	cvAdd(pTemp[3],temp2,temp2,0);

	SetZero(pTemp, 4);

	cvSetImageROI(pImg[2], cvRect(5,1,width,height));
	cvCopy(img,pImg[2]);
	cvResetImageROI(pImg[2]);

	cvSetImageROI(pImg[0], cvRect(5,0,width,height));
	cvCopy(img,pImg[0]);
	cvResetImageROI(pImg[0]);

	cvAddWeighted(pImg[0],w5,pImg[1],w5,0,pTemp[0]);
	cvAddWeighted(pImg[2],w6,pImg[3],w6,0,pTemp[1]);
	cvAdd(pTemp[0],pTemp[1],pTemp[2],0);
	cvCmp(pTemp[2],pc,xI[2],CV_CMP_GE);
	Judge01(xI[2],xI[2]);
	cvAdd(xI[2],xImg,xImg,0);
	cvCmp(xI[2],xI[1],pTemp[3],CV_CMP_NE);
	Judge01(pTemp[3],pTemp[3]);
	cvAdd(pTemp[3],temp2,temp2,0);

	SetZero(pTemp, 4);

	cvSetImageROI(pImg[3], cvRect(6,1,width,height));
	cvCopy(img,pImg[3]);
	cvResetImageROI(pImg[3]);

	cvSetImageROI(pImg[1], cvRect(6,0,width,height));
	cvCopy(img,pImg[1]);
	cvResetImageROI(pImg[1]);

	cvAddWeighted(pImg[0],w8,pImg[1],w7,0,pTemp[0]);
	cvAddWeighted(pImg[2],w9,pImg[3],w8,0,pTemp[1]);
	cvAdd(pTemp[0],pTemp[1],pTemp[2],0);
	cvCmp(pTemp[2],pc,xI[1],CV_CMP_GE);
	Judge01(xI[1],xI[1]);
	cvAdd(xI[1],xImg,xImg,0);
	cvCmp(xI[2],xI[1],pTemp[3],CV_CMP_NE);
	Judge01(pTemp[3],pTemp[3]);
	cvAdd(pTemp[3],temp2,temp2,0);

	SetZero(pTemp, 4);

	cvSetImageROI(pImg[0], cvRect(6,2,width,height));
	cvCopy(img,pImg[0]);
	cvResetImageROI(pImg[0]);

	cvSetImageROI(pImg[1], cvRect(5,2,width,height));
	cvCopy(img,pImg[1]);
	cvResetImageROI(pImg[1]);

	cvAddWeighted(pImg[0],w5,pImg[1],w6,0,pTemp[0]);
	cvAddWeighted(pImg[2],w6,pImg[3],w5,0,pTemp[1]);
	cvAdd(pTemp[0],pTemp[1],pTemp[2],0);
	cvCmp(pTemp[2],pc,xI[2],CV_CMP_GE);
	Judge01(xI[2],xI[2]);
	cvAdd(xI[2],xImg,xImg,0);
	cvCmp(xI[2],xI[1],pTemp[3],CV_CMP_NE);
	Judge01(pTemp[3],pTemp[3]);
	cvAdd(pTemp[3],temp2,temp2,0);

	cvAddWeighted(pImg[0],w1,pImg[1],w2,0,pTemp[0]);
	cvAddWeighted(pImg[4],w3,pImg[5],w4,0,pTemp[1]);
	cvAdd(pTemp[0],pTemp[1],pTemp[2],0);
	cvCmp(pTemp[2],pc,xI[1],CV_CMP_GE);
	Judge01(xI[1],xI[1]);
	cvAdd(xI[1],xImg,xImg,0);
	cvCmp(xI[2],xI[1],pTemp[3],CV_CMP_NE);
	Judge01(pTemp[3],pTemp[3]);
	cvAdd(pTemp[3],temp2,temp2,0);

	cvCmp(xI[0],xI[1],pTemp[3],CV_CMP_NE);
	Judge01(pTemp[3],pTemp[3]);
	cvAdd(pTemp[3],temp2,temp2,0);

	for (int i=0;i<temp2->height;i++)
	{
		for (int j=0;j<temp2->width;j++)
		{
			if (((uchar*)(temp2->imageData+i*temp2->widthStep))[j]>2)
				((uchar*)(xImg->imageData+i*xImg->widthStep))[j]=25;
		}
	}

	IplImage* XX=cvCreateImage(cvSize(width-1-6+1,height-1-6+1),IPL_DEPTH_8U,1);
	cvSetImageROI(xImg, cvRect(6,6,width-1-6+1,height-1-6+1));
	cvCopy(xImg, XX);
	cvResetImageROI(xImg);

	for (int c=0;c<26;c++)
	{
		hst[c] = 0;
		//for (int i=0;i<XX->height;i++)
		//{
		//	for (int j=0;j<XX->width;j++)
		//	{
		//		if (((uchar*)(XX->imageData+i*XX->widthStep))[j]==(c))
		//			hst[c]++;
		//	}
		//}
	}

	int index = 0;
	for (int i=0; i<XX->height; i++)
	{
		for (int j=0; j<XX->width; j++)
		{
			index = int(((uchar*)(XX->imageData+i*XX->widthStep))[j]);
			hst[index]++;
		}
	}

	for (int i=0; i<8; i++)
	{
		cvReleaseImage(&pImg[i]);
	}
	cvReleaseImage(&pc);
	cvReleaseImage(&XX);
	cvReleaseImage(&xImg);
	for (int i=0; i<3; i++)
	{
		cvReleaseImage(&xI[i]);
	}
	cvReleaseImage(&temp2);
	for (int i=0; i<4; i++)
	{
		cvReleaseImage(&pTemp[i]);
	}
	return 0;
}

int Damany::Imaging::FaceCompare::LBP::LBP243(IplImage* imgDst, int blockwidth, int blockheight,
	int widthBlockCount, int heightBlockCount, float* hst[])
{
	IplImage *ipatch = cvCreateImage(cvSize(blockwidth, blockheight), 8, 1);

	for (int j=0; j<heightBlockCount; j++)
	{
		for (int i=0; i<widthBlockCount; i++)
		{
			if (fabs(weightCoeff[j*widthBlockCount+i]) > 0.00001)
			{
				cvSetImageROI(imgDst, cvRect(i*blockwidth, j*blockheight, blockwidth, blockheight));
				cvCopy(imgDst, ipatch);	
				BlockRotateInvariantLBP(ipatch, hst[j*widthBlockCount+i]);
				cvResetImageROI(imgDst); 
			}
		}
	}

	cvReleaseImage(&ipatch);
	return 0;
}

void Damany::Imaging::FaceCompare::LBP::Norm(float** arr, int blockCount, int blockDim)
{
	float sum = 0.0f;
	for (int i=0; i<blockCount; i++)
	{
		sum = 0.0f;
		for (int j=0; j<blockDim; j++)
		{
			sum += arr[i][j];
		}
		for (int j=0; j<blockDim; j++)
		{
			if (fabs(sum) > 0.00001)
			{
				arr[i][j] /= sum;
			}
			if (fabs(arr[i][j]) < 0.000001)
			{
				arr[i][j] = 0.000001f;
			}
		}
	}
};

float Damany::Imaging::FaceCompare::LBP::CalcWeight(int rowIndex, int colIndex)
{
	CvMat *subMat = cvCreateMat(m_faceCount, 1, targetHst->type);
	float w = 0.0f;
	cvGetCol(targetHst, subMat, colIndex);
	float curVal = cvGetReal2D(targetHst, rowIndex, colIndex);
	float tempVal = 0.0f;
	for (int i=0; i<m_faceCount; i++)
	{
		tempVal = cvGetReal2D(subMat, i, 0);
		w += fabs(tempVal-curVal);
	}

	cvReleaseMat(&subMat);
	w = m_faceCount - w;
	return 0.1*w;
}

void Damany::Imaging::FaceCompare::LBP::CalcDistance(float** hstPro, int blockCount, int blockDim, float score[])
{
	//CalcAvg(hstPro, num1, num2);
	float dist = 0;
	float s = 0;
	float targetVal = 0.0f;
	float dstVal = 0.0f;
	int colIndex = 0;
	float minVal = 0.0f;
	float maxVal = 0.0f;
	for (int i=0; i<m_faceCount; i++)
	{
		score[i] = 0.0f;
	}
	for (int k=0; k<m_faceCount; k++)
	{
		dist = 0; 
		for(int i=0; i<blockCount; i++)
		{
			s=0; 
			for (int j=0; j<blockDim; j++) 
			{
				if (fabs(weightCoeff[i]) > 0.00001)
				{
					targetVal = cvGetReal2D(targetHst, k, i*blockDim+j);
					s += (targetVal-hstPro[i][j])*(targetVal-hstPro[i][j])/(targetVal+hstPro[i][j]);	
					/*float w = CalcWeight(k, i*blockDim+j);
					s *= w;*/
				}
			}

			dist = weightCoeff[i]*s + dist;
		    score[k] = (100 - dist)>0 ? (100-dist):0;
		}
	}
}

void Damany::Imaging::FaceCompare::LBP::CalcBlockLBP(IplImage* img, float** hst)
{
	IplImage *imgDst = cvCloneImage(img);
	cvSmooth(imgDst, imgDst, CV_GAUSSIAN, 5); 

	/*enableMultiRetinex = false;
	if(enableMultiRetinex)
	{
		MultiRetinex(imgSource, imgDst);
	}
	else
	{*/
	//}

	LBP243(imgDst, m_blockWidth, m_blockHeight, m_widthBlockCount, m_heightBlockCount, hst);

	cvReleaseImage(&imgDst);  
}

void Damany::Imaging::FaceCompare::LBP::CalcFace(IplImage* destImg, float score[])
{
	float** hstPro=new float*[m_totalBlock];   
	for(int   i=0;i<m_heightBlockCount*m_widthBlockCount;i++)
	{
		hstPro[i]=new float[m_blockDim]; 
		for (int j=0; j<m_blockDim; j++)
		{
			hstPro[i][j] = 0.0f;
		}
	}

	CalcBlockLBP(destImg, hstPro); 
	Norm(hstPro, m_totalBlock, m_blockDim);

	/*CvMat *curHst = cvCreateMat(1, num1*num2, CV_32FC1);
	for (int i=0; i<num1; i++)
	{
	for (int j=0; j<num2; j++)
	{
	cvSetReal2D(curHst, 0, i*num2+j, hstPro[i][j]);
	}
	}
	fs.CalcDist(curHst, score);
	cvReleaseMat(&curHst);*/

	CalcDistance(hstPro, m_totalBlock, m_blockDim, score); 

	for (int i=0; i<m_totalBlock; i++)
	{
		delete[] hstPro[i];
	}
	delete[] hstPro; 
}

void Damany::Imaging::FaceCompare::LBP::CmpFace(IplImage* destImg, float score[])
{

	/*IplImage* pImg = cvCreateImage(cvSize(destRect.width,destRect.height), 8, 1);
	cvCopy(destImg, pImg);
	LightAdjuest(pImg);
	cvCopy(pImg, destImg);
	cvReleaseImage(&pImg);*/

	CalcFace(destImg, score);
}

//void Damany::Imaging::FaceCompare::LBP::Compositor(float arr[], int count)//从小到大排序
//{
//	float temp = 0.0f;
//	for (int i=0; i<count-1; i++)
//	{
//		for (int j=i; j<count; j++)
//		{
//			if (arr[j] < arr[i])
//			{
//				temp = arr[i];
//				arr[i] = arr[j];
//				arr[j] = temp;
//			}
//		}
//	}
//}

//void Damany::Imaging::FaceCompare::LBP::CalcMaxMin(float arr[], int count, float val, 
//	float& minMaxVal, float& maxMinVal)
//{
//	int index = 0;
//	for (int i=0; i<count; i++)
//	{
//		if (fabs(arr[i] - val) < 1e-10)
//		{
//			index = i;
//			break;
//		}
//	}
//
//	if (index == 0)//如果是最小值
//	{
//		minMaxVal = -30.0;
//		for (int j=index+1; j<count; j++)
//		{
//			if (arr[j] > val)
//			{
//				maxMinVal = arr[j];
//				break;
//			}
//		}
//	}
//	else if (index == (count-1))//如果是最大值
//	{
//		maxMinVal = 30.0;
//		for (int j=count-2; j>0; j--)
//		{
//			if (arr[j] < val)
//			{
//				minMaxVal = arr[j];
//				break;
//			}
//		}
//	}
//	else//既不是最大值也不是最小值
//	{
//		for (int j=index-1; j>0; j--)
//		{
//			if (arr[j] < val)
//			{
//				minMaxVal = arr[j];
//				break;
//			}
//		}
//		for (int j=index+1;j<count; j++)
//		{
//			if (arr[j] > val)
//			{
//				maxMinVal = arr[j];
//				break;
//			}
//		}
//	}
//}

//uchar Damany::Imaging::FaceCompare::LBP::Magnitude(uchar data[], int count, uchar cData)
//{
//	uchar val = 0;
//	for (int i=0; i<count; i++)
//	{
//		if (data[i] > cData)
//		{
//			float tempVal = pow(2.0f, i);
//			val += int(tempVal);
//		}
//	}
//
//	return val;
//}

//void Damany::Imaging::FaceCompare::LBP::Transform(IplImage *srcImg, IplImage *dstImg)
//{
//	uchar *srcData = (uchar*)srcImg->imageData;
//	uchar *dstData = (uchar*)dstImg->imageData;
//	cvZero(dstImg);
//	uchar data[8];
//	uchar cData = 0;
//	uchar val;
//
//	int height = srcImg->height;
//	int width = srcImg->width;
//	int step = srcImg->widthStep;
//	for (int i=1; i<height-1; i++)
//	{
//		for (int j=1; j<width-1; j++)
//		{
//			data[0] = srcData[i*step+j-1];
//			data[1] = srcData[(i+1)*step+j-1];
//			data[2] = srcData[(i+1)*step+j];
//			data[3] = srcData[(i+1)*step+j+1];
//			data[4] = srcData[i*step+j+1];
//			data[5] = srcData[(i-1)*step+j+1];
//			data[6] = srcData[(i-1)*step+j];
//			data[7] = srcData[(i-1)*step+j-1];
//
//			cData = srcData[i*step+j];
//			val = Magnitude(data, 8, cData);
//			dstData[i*step+j] = val;
//		}
//	}
//}

//float Damany::Imaging::FaceCompare::LBP::HistSimilar(IplImage *img1, IplImage *img2)
//{
//	IplImage *dstImg1 = cvCreateImage(cvGetSize(img1), 8, 1);
//	IplImage *dstImg2 = cvCreateImage(cvGetSize(img2), 8, 1);
//	Transform(img1, dstImg1);
//	Transform(img2, dstImg2);
//	int size = 32;
//	float range[] = {0, 255};
//	float *ranges[] = {range};
//	CvHistogram *hist1 = cvCreateHist(1, &size, CV_HIST_ARRAY, ranges, 1);
//	CvHistogram *hist2 = cvCreateHist(1, &size, CV_HIST_ARRAY, ranges, 1);
//	cvCalcHist(&dstImg1, hist1, 0, 0);
//	cvNormalizeHist(hist1, 1.0);
//	cvCalcHist(&dstImg2, hist2, 0, 0);
//	cvNormalizeHist(hist2, 1.0);
//	float val = cvCompareHist(hist1, hist2, CV_COMP_CORREL);
//
//	cvReleaseImage(&dstImg1);
//	cvReleaseImage(&dstImg2);
//	cvReleaseHist(&hist1);
//	cvReleaseHist(&hist2);
//
//	return val;
//}


void Damany::Imaging::FaceCompare::LBP::LoadImages(IplImage* imgs[], int count)
{
	m_faceCount = count; 

	if (count <= 0)
	{
		return;
	}

	targetHst = cvCreateMat(m_faceCount, m_totalBlock*m_blockDim, CV_32FC1);
	cvSetZero(targetHst); 

	float** hst;
	hst = new float*[m_totalBlock];   
	for(int i=0; i<m_heightBlockCount*m_widthBlockCount; i++) 
	{
		hst[i] = new float[m_blockDim];
		for (int j=0; j<m_blockDim; j++)
		{
			hst[i][j] = 0.0f;
		}
	}

	int colIndex = 0; 
	for (int k=0; k<m_faceCount; k++)
	{
		//LightAdjuest(imgs[k]);
		CalcBlockLBP(imgs[k], hst); 
		Norm(hst, m_totalBlock, m_blockDim); 

		float val = 0.0f;
		for(int i=0; i<m_totalBlock; i++)
		{
			for (int j=0; j<m_blockDim; j++)
			{
				val = hst[i][j];
				cvSetReal2D(targetHst, k, colIndex, val);
				colIndex++;  
			}
		}
		colIndex = 0;
	}

	for (int i=0; i<m_totalBlock; i++)
	{
		delete[] hst[i];
	}
	delete[] hst;
}

//void Damany::Imaging::FaceCompare::LBP::LightAdjuest(IplImage* grayImg)
//{
//	CvScalar avg = cvAvg(grayImg);
//	float avgVal = avg.val[0];
//
//	IplImage* temp = cvCreateImage(cvGetSize(grayImg), 8, 1);
//	IplImage* img = cvCreateImage(cvGetSize(grayImg), 8, 1);
//
//	if (avgVal < 50)
//	{
//		cvSmooth(grayImg, temp, CV_GAUSSIAN, 7, 7);
//		cvEqualizeHist(temp, img); 
//	}
//	else if (avgVal < 100)
//	{
//		cvSmooth(grayImg, temp, CV_GAUSSIAN, 7, 7);
//		cvEqualizeHist(temp, img); 
//	}
//	else if (avgVal < 150)
//	{
//		cvSmooth(grayImg, img, CV_GAUSSIAN, 5, 5);
//	}
//	else 
//	{
//		GammaCorrect(grayImg, img, 0, 0.9, 0.1, 0.6, 2); 
//	}
//
//	cvCopy(img, grayImg); 
//
//	cvReleaseImage(&temp);
//	cvReleaseImage(&img);
//}