//#include "StdAfx.h"
#include <stdio.h>
#include "FaceSelect.h"

#ifndef max
#define max(a,b)            (((a) > (b)) ? (a) : (b))
#endif
//#define BKGANA//通过背景分析去除干扰的开关


const char* cascade_name_face = "C:\\data\\haarcascade_frontalface_alt.xml";

//////////////////////Michael Add Eye-Mouth Analysis/////////////////////////////////////////
#ifdef SAVE_FACEANA_DEBUG_INFO
CString g_curDebugInfoFilePrefix = _T("");
void setCurDebugInfoPath( CString& strPrefix )
{
	g_curDebugInfoFilePrefix = strPrefix;
}
#endif

const char* cascade_name_eye = "C:/data/LEye18x12.xml";
const int nRgnBound_leye[4] = { 0, 0, 50, 50 };//在人脸的多大范围内搜索，百分比

const char* cascade_name_reye = "C:/data/REye18x12.xml";
const int nRgnBound_reye[4] = { 50, 0, 100, 50 };//在人脸的多大范围内搜索，百分比

const char* cascade_name_mouth = "C:/data/Mouth25x15.xml";
const int nRgnBound_mouth[4] = { 0, 50, 100, 100 };//在人脸的多大范围内搜索，百分比
/////////////////////End -- Michael Add Eye-Mouth Analysis///////////////////////////////////


//////////////////////Yuki Add Upperbody Analysis/////////////////////////////////////////
const char* cascade_name_upperbody = "./data/haarcascade_upperbody.xml";
//////////////////////End--Yuki Add Upperbody Analysis/////////////////////////////////////////


void CFaceSelect::InitClass()//Michael Add 20090507
{
	m_cvSeqStorage = cvCreateMemStorage(0);
	m_cvImageSeq = cvCreateSeq(0, sizeof(CvSeq), sizeof(IplImage*), m_cvSeqStorage);
	//20090716 Defined Interface
	m_cvFrameSeq = cvCreateSeq(0, sizeof(CvSeq), sizeof(Frame), m_cvSeqStorage );
	//End -- 20090716 Defined Interface
	m_cvSeqFaceSeqStorage = cvCreateMemStorage(0);
	m_cvSeqFaceSeq = cvCreateSeq(0, sizeof(CvSeq), sizeof(CvSeq*), m_cvSeqFaceSeqStorage);
	m_cvSeqFaceStorageStorage = cvCreateMemStorage(0);
	m_cvSeqFaceStorage = cvCreateSeq(0, sizeof(CvSeq), sizeof(CvMemStorage*), \
		m_cvSeqFaceStorageStorage);
	m_cvRealFaceSeqStorage = cvCreateMemStorage(0);
	m_cvRealFaceSeq = cvCreateSeq(0, sizeof(CvSeq), sizeof(int), m_cvRealFaceSeqStorage);

	InitCascade(cascade_name_face, m_cascade_face, m_casMem_face);

	initSubFaceFeatureCascade( );//Michael Add Eye-Mouth Analysis

	m_outputDir[0] = 0;
	m_resString[0] = 0;

	m_ExRatio_t = 0.5f;
	m_ExRatio_b = 0.5f;
	m_ExRatio_l = 0.5f;
	m_ExRatio_r = 0.5f;

	m_nTotalValidImages = 0;
	m_targets = 0;
}

CFaceSelect::CFaceSelect(void)
: m_cvSeqStorage(NULL)
, m_cvImageSeq(NULL)
, m_cvFrameSeq(NULL)
, m_cascade_face(NULL)
, m_cvSeqFaceSeq(NULL)
, m_cvRealFaceSeq(NULL)
, m_cvRealFaceSeqStorage(NULL)
, m_cvSeqFaceSeqStorage(NULL)
, m_cvSeqFaceStorage(NULL)
, m_cvSeqFaceStorageStorage(NULL)
, m_pDiffRectStorage(NULL)
, m_pDiffJudgeStorage(NULL)
, m_sqDiffFaceRect(NULL)
, m_sqDiffFaceJudge(NULL)
, m_pHistStorage(NULL)
, m_sqHist(NULL)
, m_pImgNoStorage(NULL)
, m_sqImgNo(NULL)
, m_dSglBkgProportion(0.15)
, m_dFaceProportion(0.5)
, m_bScaleFaceDetection(1)
, m_dScale(0.5)
, m_dForeheadNoise(0.75)
, m_dUpBkgNoise(0.13)
, m_dSideBkgNoise(0.05)
, m_dEarNoise(0.30)
, m_iFaceSize(60/*30*/)//Michael Change 20090508 -- 30 -> 60
, m_iSFactorValv(23)
, m_iSizeWeight(5)
, m_cascade_eye(NULL)//left eye
, m_cascade_reye(NULL)
, m_cascade_mouth(NULL)
, m_storage_subfacefeature(NULL)
, m_dFaceChangeRatio(4.0)
, m_rcROI( cvRect(0,0,0,0) )
, m_casMem_face(NULL)
, m_casMem_eye(NULL)
, m_casMem_reye(NULL)
, m_casMem_mouth(NULL)
, m_iLightCondition(0)
, m_bDebugForBkgAna(0)
{
	InitClass();//Michael Change 20090507
}

CFaceSelect::~CFaceSelect(void)
{
	ReleaseTargets( m_targets, m_nTotalValidImages );

	ReleaseCascade(m_cascade_face, m_casMem_face);
	ReleaseImageSeq();
	ReleaseFaceSeq();
	ReleaseStorageSeq();

	releaseSubFaceFeatureCascade( );//Michael Add Eye-Mouth Analysis

}

void CFaceSelect::ReleaseImageSeq()
{
	IplImage **ppImage;
	for (int i=0; i<m_cvImageSeq->total; i++)
	{
		ppImage = CV_GET_SEQ_ELEM(IplImage*, m_cvImageSeq, i);
		cvReleaseImage(ppImage);
	}

	cvClearSeq(m_cvImageSeq);
	cvClearSeq(m_cvFrameSeq);
	cvClearSeq(m_cvRealFaceSeq);
	cvReleaseMemStorage(&m_cvRealFaceSeqStorage);
	cvReleaseMemStorage(&m_cvSeqStorage);
	m_cvRealFaceSeq = NULL;
	m_cvRealFaceSeqStorage = NULL;
	m_cvImageSeq = NULL;
	m_cvFrameSeq = NULL;
	m_cvSeqStorage = NULL;
}

void CFaceSelect::ReleaseFaceSeq()
{
	CvSeq **ppSeq;
	for (int i=0; i<m_cvSeqFaceSeq->total; i++)
	{
		ppSeq = CV_GET_SEQ_ELEM(CvSeq*, m_cvSeqFaceSeq, i);
		cvClearSeq(*ppSeq);
	}
	cvClearSeq(m_cvSeqFaceSeq);
	cvReleaseMemStorage(&m_cvSeqFaceSeqStorage);
	m_cvSeqFaceSeq = NULL;
	m_cvSeqFaceSeqStorage = NULL;
}

void CFaceSelect::ReleaseStorageSeq()
{
	CvMemStorage **ppMemSeq;
	for (int i=0; i<m_cvSeqFaceStorage->total; i++)
	{
		ppMemSeq = CV_GET_SEQ_ELEM(CvMemStorage*, m_cvSeqFaceStorage, i);
		cvReleaseMemStorage(ppMemSeq);
	}
	cvClearSeq(m_cvSeqFaceStorage);
	cvReleaseMemStorage(&m_cvSeqFaceStorageStorage);
	m_cvSeqFaceStorage = NULL;
	m_cvSeqFaceStorageStorage = NULL;
}

void CFaceSelect::AddInImage(const char* strFileName)
{
	IplImage *pImage;
	pImage = cvLoadImage(strFileName);

	if( !pImage ) return;//处理载入失败的情况

	cvSeqPush(m_cvImageSeq, &pImage);

	//找脸并存储——抓图间的时间间隔较长可在此期间进行人脸检测。否则可放在后面遍历处理中进行。
	CvMemStorage *storage = cvCreateMemStorage(0);
	CvSeq* faces = FaceDetect( pImage, storage, cvSize(m_iFaceSize, m_iFaceSize), m_bScaleFaceDetection, m_dScale);
	
	AddInFaceSeq(&faces, &storage);
}

bool CFaceSelect::AddInImage( IplImage *pImg, CvRect roi )
{
	assert(pImg != NULL);
	
	IplImage *pImage = 0;	
	pImage = cvCloneImage( pImg );
	assert(pImage != NULL);//处理载入失败的情况
	m_rcROI = roi;

	//bool bNormROI = false;
	//if( roi.width > 1 && roi.height > 1 )
	//{
	//	if( roi.x >= 0 && roi.x + roi.width - 1 < pImg->width
	//		&& roi.y >= 0 && roi.y + roi.height - 1 < pImg->height )
	//	{
	//		bNormROI = true;
	//	}
	//}
	//if( bNormROI )
	//	pImage = GetSubImage( pImg, roi );
	//else
	//	pImage = cvCloneImage( pImg );
	//m_rcROI = cvRect(0, 0, 0, 0);
	//if( !pImage ) return false;

	cvSeqPush(m_cvImageSeq, &pImage);

	//找脸并存储——抓图间的时间间隔较长可在此期间进行人脸检测。否则可放在后面遍历处理中进行。
	CvMemStorage *storage = cvCreateMemStorage(0);
	CvSeq* faces = FaceDetect( pImage, storage, cvSize(m_iFaceSize, m_iFaceSize), m_bScaleFaceDetection, m_dScale);

	AddInFaceSeq(&faces, &storage);

	return true;
}

void CFaceSelect::AddInFaceSeq(CvSeq **SeqFace, CvMemStorage **MemFace)
{
	cvSeqPush(m_cvSeqFaceSeq, SeqFace);
	cvSeqPush(m_cvSeqFaceStorage, MemFace);
}

void CFaceSelect::ShowAllImage(int iIntervalTime)
{
	IplImage **ppImage;

	cvNamedWindow("ShowAllImage", 1);
	
	for (int i=0; i<m_cvImageSeq->total; i++)
	{
		ppImage = CV_GET_SEQ_ELEM(IplImage*, m_cvImageSeq, i);

		//找脸并存储
		CvMemStorage *storage = cvCreateMemStorage(0);
		CvSeq* faces = FaceDetect( *ppImage, storage, cvSize(m_iFaceSize, m_iFaceSize), m_bScaleFaceDetection, m_dScale);

		//Debug 画脸
		for( int ii = 0; ii < (faces?faces->total:0); ii++ )
		{
			CvRect* r = (CvRect*)cvGetSeqElem( faces, ii );
			CvRect rScale = *r;
			CvScalar color ={255,0,0,0};
			IplImage* imgFace = GetSubImage( *ppImage, rScale );

			cvRectangle(*ppImage,cvPoint(rScale.x,rScale.y),cvPoint( rScale.x+rScale.width, rScale.y+rScale.height), color, 3, 8, 0 );
		
			cvReleaseImage( &imgFace );
		}
		cvReleaseMemStorage(&storage);
		cvShowImage("ShowAllImage", *ppImage);

		cvWaitKey(iIntervalTime);
	}
	cvDestroyWindow("ShowAllImage");
}

IplImage* CFaceSelect::GetSubImage(IplImage* pOriImage, CvRect roi)
{
	IplImage * pSubImage = NULL;   

     cvSetImageROI(pOriImage, roi);
     pSubImage = cvCreateImage( cvSize(pOriImage->roi->width, pOriImage->roi->height), pOriImage->depth, pOriImage->nChannels );   
     pSubImage->origin = pOriImage->origin;   
     cvCopy(pOriImage, pSubImage);   
     cvResetImageROI(pOriImage);  

     return pSubImage;   
}

void CFaceSelect::FaceJudge(IplImage *pImage, double &dEquFactor, int &iSFactor, double &dFaceSize, double &dSVariant, double &dVVariant, double &dHVariant, double &dContrast, double &dVarient)
{
	dVarient = 0.0f;
	int iQuantizeStep = 2; //直方图量化步长
	float fChnlRang[2] = {0, 255}; //直方图范围
	int iDimSize = 256/iQuantizeStep;
	float *pfChnlRang =  fChnlRang;

	IplImage *pHSVImage = cvCreateImage(cvGetSize(pImage), pImage->depth, pImage->nChannels);
	IplImage* h_plane = cvCreateImage( cvGetSize(pHSVImage), 8, 1 );
	IplImage* s_plane = cvCreateImage( cvGetSize(pHSVImage), 8, 1 );
	IplImage* v_plane = cvCreateImage( cvGetSize(pHSVImage), 8, 1 );
	IplImage* s_plane_thr = cvCreateImage( cvGetSize(pHSVImage), 8, 1 );
	
	cvCvtColor(pImage, pHSVImage, CV_BGR2HSV);
	cvCvtPixToPlane( pHSVImage, h_plane, s_plane, v_plane, 0 );

	CvSize head = cvGetSize(pImage);
	CvRect roi;
	//去除脸图两侧可能的背景区域
	roi.x = (int)(head.width*m_dSglBkgProportion);
	roi.y = 0;
	roi.width = (int)(head.width*(1-m_dSglBkgProportion*2));
	roi.height = head.height;

	/*
	//debug 显示画框图像
	cvNamedWindow("Image",1);
	CvScalar color = {255,0,0,0};
	cvRectangle(pImage, cvPoint(roi.x, roi.y), cvPoint(roi.x+roi.width, roi.y+roi.height), color, 1, 8, 0);
	cvShowImage("Image", pImage);
	cvWaitKey(2000);
	// */

	//S变量直方图
	CvHistogram *pHist_s = cvCreateHist(1, &iDimSize, CV_HIST_ARRAY, (float**)(&pfChnlRang), 1);
	cvSetImageROI(s_plane, roi);
	cvCalcHist(&s_plane, pHist_s, 0, 0);
	cvResetImageROI(s_plane);
	
	///////////////////////////////////////////////////////
	double dDummy;
	CalcHistVariences(pHist_s, iDimSize-1, dDummy, dSVariant);
	//*
	//H分量的参照信息
	IplImage *GBImg = cvCreateImage( cvGetSize(pHSVImage), 8, 1 );
	cvCvtPixToPlane( pHSVImage, GBImg, h_plane, v_plane, 0 );
	CvHistogram *pHist_GB = cvCreateHist(1, &iDimSize, CV_HIST_ARRAY, (float**)(&pfChnlRang), 1);
	cvSetImageROI(GBImg, roi);
	cvCalcHist(&GBImg, pHist_GB, 0, 0);
	cvResetImageROI(GBImg);
	CalcHistVariences(pHist_GB, iDimSize-1, dDummy, dHVariant);
	//iHFactor = GetHistRange(pHist_h, iDimSize-1, dMaxPix, iLowBnd, iUpBnd, m_dFaceProportion);
	//iHRange = iUpBnd - iLowBnd;
	//画直方图
	//DrawHistImage(pHist_GB, iDimSize-1, roi.width * roi.height);

	//V分量的参考信息
	cvClearHist(pHist_GB);
	cvCvtPixToPlane( pHSVImage, h_plane, v_plane, GBImg, 0 );
	cvSetImageROI(GBImg, roi);
	cvCalcHist(&GBImg, pHist_GB, 0, 0);
	cvResetImageROI(GBImg);
	CalcHistVariences(pHist_GB, iDimSize-1,dDummy, dVVariant);
	//iHFactor = GetHistRange(pHist_h, iDimSize-1, dMaxPix, iLowBnd, iUpBnd, m_dFaceProportion);
	//iHRange = iUpBnd - iLowBnd;
	//画直方图
	//DrawHistImage_H(pHist_GB, iDimSize-1, roi.width * roi.height);
	
	cvReleaseHist(&pHist_GB);
	cvReleaseImage(&GBImg);
	//*/
	///////////////////////////////////////////////////////////////////////////////


	//直方图二值化
	double dMaxPix = roi.width * roi.height;
	int iLowBnd =0 ;
	int iUpBnd = 0;
	iSFactor = GetHistRange(pHist_s, iDimSize-1, dMaxPix, iLowBnd, iUpBnd, m_dFaceProportion);
	int iSRange = iUpBnd - iLowBnd;
	iLowBnd = iLowBnd*iQuantizeStep;
	iUpBnd = (iUpBnd+1)*iQuantizeStep-1;
	cvThreshold(s_plane, s_plane_thr, iUpBnd, 255, CV_THRESH_TOZERO_INV);
	cvThreshold(s_plane_thr, s_plane, iLowBnd, 255, CV_THRESH_BINARY_INV);

	//画直方图
	//DrawHistImage(pHist_s, iDimSize-1, dMaxPix);
	cvReleaseHist(&pHist_s);

	//Debug 降噪前
	//dEquFactor = GetFaceHoriEquality(s_plane);
	//降噪
	DeNoise(s_plane, 2, 2, 0, 0);
	IplImage* s_plane_not = cvCreateImage( cvGetSize(pHSVImage), 8, 1 );
	cvNot(s_plane, s_plane_not);
	DeNoise(s_plane_not, 2, 2, 0, 0);
	cvNot(s_plane_not, s_plane);
	cvReleaseImage(&s_plane_not);

	//截取有效脸部区域
	int iLeft = 0;
	int iLenth = 0;
	GetEffectiveFaceWidth(s_plane, m_dEarNoise, 0, iLeft, iLenth);
	int iFaceOffset = 0;
#ifdef OBSOLETE_FUNC
	iFaceOffset = GetEffectiveFaceHeight(s_plane, m_dForeheadNoise, 0, iLenth);
#endif
	iFaceOffset = GetEffectiveFaceHeightBKG(s_plane, m_dUpBkgNoise, 0);
	CvRect rRealFace;
	rRealFace.height = s_plane->height - iFaceOffset;
	rRealFace.width = iLenth;
	rRealFace.x = iLeft;
	rRealFace.y = iFaceOffset;
	dFaceSize = rRealFace.height * rRealFace.width;
	//IplImage *imgRealFace = GetSubImage(s_plane, rRealFace);

	/*
	//Debug 显示
	//cvNamedWindow("FaceImage");
	CvScalar color ={255,0,0,0};
	cvRectangle(pImage,cvPoint(rRealFace.x,rRealFace.y),cvPoint( rRealFace.x+rRealFace.width, rRealFace.y+rRealFace.height), color, 1, 8, 0 );
	//cvShowImage("FaceImage", pImage);
	//cvWaitKey(20);
	//cvDestroyWindow("FaceImage");
	//*/

	//Debug保存
	//cvSaveImage("Face.bmp", s_plane);
	/*
	//Debug 显示
	cvNamedWindow("FaceImage");
	CvScalar color ={255,0,0,0};
	cvRectangle(s_plane,cvPoint(rRealFace.x,rRealFace.y),cvPoint( rRealFace.x+rRealFace.width, rRealFace.y+rRealFace.height), color, 3, 8, 0 );
	cvShowImage("FaceImage", s_plane);
	cvWaitKey(1000);
	cvDestroyWindow("FaceImage");
	//*/

	//计算图像对比度
	IplImage *pContrastImg = cvCreateImage(cvGetSize(pHSVImage), 8, 1 );
	cvCvtColor(pImage, pContrastImg, CV_BGR2GRAY);
	IplImage *pSubContrast = GetSubImage(pContrastImg, rRealFace);
	int idummy;
	float fdummy;
	int iContr = ContrastJudge(pSubContrast, fdummy, idummy, 0.5);
	dContrast = 255/(double)iContr;
	cvReleaseImage(&pSubContrast);
	cvReleaseImage(&pContrastImg);

	//*
	//计算图像方差
	IplImage *pOriSImg = cvCreateImage( cvGetSize(pHSVImage), 8, 1 );
	cvCvtPixToPlane( pHSVImage, h_plane, pOriSImg, v_plane, 0 );
	cvSetImageROI(pOriSImg, rRealFace);
	CvScalar sMean;
	CvScalar sVaria;
	double dMean;
	cvAvgSdv(pOriSImg, &sMean, &sVaria, 0);
	cvResetImageROI(pOriSImg);
	dMean = sMean.val[0];
	dVarient = sVaria.val[0];
	dVarient = 200/dVarient;
	
	if (iSFactor<31)
		cvCvtColor(pImage, pOriSImg, CV_BGR2GRAY);
	else
		cvCvtPixToPlane( pImage, h_plane, v_plane, pOriSImg, 0 );

	//脸区直方图
	int iFaceHistDim = 256;
	double dCutVal = 0.80;
	CvRect rUpRealFace;
	rUpRealFace.height = rRealFace.height;
	rUpRealFace.width = rRealFace.width;
	rUpRealFace.x = rRealFace.x;
	rUpRealFace.y = rRealFace.y;
	dMaxPix = rUpRealFace.width * rUpRealFace.height;
	IplImage *pRealFaceImg = GetSubImage(pOriSImg, rUpRealFace);

	//Debug
	//cvSaveImage("..\\Debug\\Face_comp.bmp", pRealFaceImg);
	
	//根据亮度校正二值化方法，还可以细化
	cvEqualizeHist(pRealFaceImg, pRealFaceImg);
	if (iSFactor<31)
	{
		dCutVal = 0.60;
	}

	IplImage *pBlurFaceImg = cvCreateImage(cvSize(rUpRealFace.width, rUpRealFace.height), 8, 1);
	CvHistogram *pHistFace = cvCreateHist(1, &iFaceHistDim, CV_HIST_ARRAY, (float**)(&pfChnlRang), 1);
	cvCalcHist(&pRealFaceImg, pHistFace, 0, 0);
	//画图
	//DrawHistImage(pHistFace, iFaceHistDim-1, dMaxPix);

	//二值化
	iUpBnd = iFaceHistDim-10;
	idummy = GetHistRangeSingle(pHistFace, iFaceHistDim-1, dMaxPix, iUpBnd, iLowBnd, dCutVal);
	
	cvThreshold(pRealFaceImg, pBlurFaceImg, iUpBnd, 255, CV_THRESH_TOZERO_INV);
	cvThreshold(pBlurFaceImg, pRealFaceImg, iLowBnd, 255, CV_THRESH_BINARY_INV);
	cvReleaseHist(&pHistFace);

	//去噪
	//*
	DeNoise(pRealFaceImg, 2, 2, 0, 0);
	s_plane_not = cvCreateImage( cvGetSize(pRealFaceImg), 8, 1 );
	cvNot(pRealFaceImg, s_plane_not);
	DeNoise(s_plane_not, 2, 2, 0, 0);
	cvNot(s_plane_not, pRealFaceImg);
	cvReleaseImage(&s_plane_not);
	//*/
	IplImage *imgRealFace = pRealFaceImg;
	
	/*
	cvEqualizeHist(pRealFaceImg, pBlurFaceImg);
	pHistFace = cvCreateHist(1, &iFaceHistDim, CV_HIST_ARRAY, (float**)(&pfChnlRang), 1);
	cvCalcHist(&pBlurFaceImg, pHistFace, 0, 0);
	DrawHistImage_H(pHistFace, iFaceHistDim-1, dMaxPix);
	cvReleaseHist(&pHistFace);
	//*/

	/*
	//Debug保存
	static int iNum = 0;
	char cFN[20];
	sprintf_s(cFN, 20, "..\\Debug\\Face%d.bmp", iNum);
	cvSaveImage(cFN, pRealFaceImg);
	sprintf_s(cFN, 20, "..\\Debug\\FaceB%d.bmp", iNum++);
	cvSaveImage(cFN, pBlurFaceImg);
	//*/

	cvReleaseImage(&pBlurFaceImg);
	cvReleaseImage(&pOriSImg);


	//*/


	//获取均衡系数_上下分块
	CvRect UpperRect, LowerRect;
	UpperRect.height = imgRealFace->height / 2;
	UpperRect.width = LowerRect.width = imgRealFace->width;
	UpperRect.x = LowerRect.x = 0;
	UpperRect.y = 0;
	LowerRect.y = imgRealFace->height /2;
	LowerRect.height = imgRealFace->height - (imgRealFace->height /2);
	
	IplImage *pUpperImg = GetSubImage(imgRealFace, UpperRect);
	IplImage *pLowerImg = GetSubImage(imgRealFace, LowerRect);

	dEquFactor = GetFaceHoriEquality(pUpperImg) + GetFaceHoriEquality(pLowerImg);
	dEquFactor *= 50;

	/*//四块模式
	//获取均衡系数_上下分块
	CvRect UpperRect, LowerRect, UpperLowRect, LowerUpRect;
	UpperRect.width = LowerRect.width = UpperLowRect.width = LowerUpRect.width = imgRealFace->width;
	UpperRect.x = LowerRect.x = UpperLowRect.x = LowerUpRect.x = 0;
	UpperRect.y = 0;
	UpperRect.height = imgRealFace->height / 4;
	UpperLowRect.y = imgRealFace->height / 4;
	UpperLowRect.height = imgRealFace->height /2 - imgRealFace->height / 4;
	LowerUpRect.y = imgRealFace->height /2;
	LowerUpRect.height = imgRealFace->height / 4;
	LowerRect.y = imgRealFace->height /2 + imgRealFace->height / 4;
	LowerRect.height = imgRealFace->height - (imgRealFace->height/2 + imgRealFace->height/4);

	IplImage *pUpperImg = GetSubImage(imgRealFace, UpperRect);
	IplImage *pUpperLowImg = GetSubImage(imgRealFace, UpperLowRect);
	IplImage *pLowerUpImg = GetSubImage(imgRealFace, LowerUpRect);
	IplImage *pLowerImg = GetSubImage(imgRealFace, LowerRect);

	dEquFactor = GetFaceHoriEquality(pUpperImg) + GetFaceHoriEquality(pLowerUpImg) + GetFaceHoriEquality(pUpperLowImg) + GetFaceHoriEquality(pLowerImg);
	dEquFactor *= 25;
	//*/

	

	/* //H变量处理
	iLowBnd = iLowBnd*iQuantizeStep;
	iUpBnd = (iUpBnd+1)*iQuantizeStep-1;
	cvThreshold(h_plane, h_plane_thr, iUpBnd, 255, CV_THRESH_BINARY_INV);
	cvThreshold(h_plane, h_plane_thr_sec, iLowBnd, 255, CV_THRESH_BINARY);
	cvXor(h_plane_thr, h_plane_thr_sec, h_plane);

	cvNamedWindow("hImage");
	cvShowImage("hImage", h_plane);

	cvAnd(h_plane, s_plane, v_plane);
	cvNamedWindow("AndImage");
	cvShowImage("AndImage", v_plane);
	//*/


	cvReleaseImage(&pUpperImg);
	cvReleaseImage(&pLowerImg);
	cvReleaseImage(&imgRealFace);
	cvReleaseImage(&pHSVImage);
	cvReleaseImage(&h_plane);
	cvReleaseImage(&s_plane);
	cvReleaseImage(&v_plane);
	cvReleaseImage(&s_plane_thr);
}

void CFaceSelect::CalcHistVariences(CvHistogram* pHist, int iMaxRange, double &dMean, double &dVarient)
{
	double dSize = 0;
	int i;

	dMean = 0;
	dVarient = 0;
	for (i=0; i<=iMaxRange; i++)
	{
		dMean += (double)(i)*((double)cvQueryHistValue_1D(pHist, i));
		dSize += cvQueryHistValue_1D(pHist, i);
	}
	dMean /= dSize;

	for (i=0; i<=iMaxRange; i++)
	{
		dVarient += (((double)i-dMean)*((double)i-dMean))*((double)cvQueryHistValue_1D(pHist, i));
	}
	dVarient /= dSize;
}
//*
void CFaceSelect::DrawHistImage(CvHistogram *pHist, int iMaxRange, double dMaxPix)
{
	int i, j;
	int iVal;
	static int iSHistNum = 0;
	char sFileName[60];

	IplImage *pHistImage = cvCreateImage(cvSize((iMaxRange+1)*4+10, 1024), 8, 1);
	cvZero(pHistImage);

	*(pHistImage->imageDataOrigin) = 0;

	//cvNamedWindow("HistImage", 1);

	for (i=5; i<=iMaxRange+5; i++)
	{
		iVal = (int)(cvQueryHistValue_1D(pHist, i-5)/dMaxPix*1024);
		for (j=0; j<=iVal; j++)
		{
			*(pHistImage->imageData + j*pHistImage->widthStep + i*4) = (char)(0xFF);
			*(pHistImage->imageData + j*pHistImage->widthStep + i*4 + 1) = (char)(0xFF);
			*(pHistImage->imageData + j*pHistImage->widthStep + i*4 + 2) = (char)(0xFF);
			*(pHistImage->imageData + j*pHistImage->widthStep + i*4 + 3) = (char)(0xFF);
		}
	}

	sprintf_s(sFileName, 60, "..\\Debug\\Face%d_SHist.bmp", iSHistNum++);	
	cvSaveImage(sFileName, pHistImage);

	//cvShowImage("HistImage", pHistImage);
	//cvWaitKey(2000);
	//cvDestroyWindow("HistImage");
	cvReleaseImage(&pHistImage);
}

void CFaceSelect::DrawHistImage_H(CvHistogram *pHist, int iMaxRange, double dMaxPix)
{
	int i, j;
	int iVal;
	static int iHHistNum = 0;
	char sFileName[60];

	IplImage *pHistImage = cvCreateImage(cvSize((iMaxRange+1)*4+10, 1024), 8, 1);
	cvZero(pHistImage);

	*(pHistImage->imageDataOrigin) = 0;

	//cvNamedWindow("HistImage", 1);

	for (i=5; i<=iMaxRange+5; i++)
	{
		iVal = (int)(cvQueryHistValue_1D(pHist, i-5)/dMaxPix*1024);
		for (j=0; j<=iVal; j++)
		{
			*(pHistImage->imageData + j*pHistImage->widthStep + i*4) = (char)(0xFF);
			*(pHistImage->imageData + j*pHistImage->widthStep + i*4 + 1) = (char)(0xFF);
			*(pHistImage->imageData + j*pHistImage->widthStep + i*4 + 2) = (char)(0xFF);
			*(pHistImage->imageData + j*pHistImage->widthStep + i*4 + 3) = (char)(0xFF);
		}
	}

	sprintf_s(sFileName, 60, "..\\Debug\\Face%d_HHist.bmp", iHHistNum++);	
	cvSaveImage(sFileName, pHistImage);

	//cvShowImage("HistImage", pHistImage);
	//cvWaitKey(2000);
	//cvDestroyWindow("HistImage");
	cvReleaseImage(&pHistImage);
}
//*/

int CFaceSelect::GetEffectiveFaceHeightBKG(IplImage *pImage, double dSizeValv, char cValue)
{
	int iOffset = 0;
	double dRowNum = 0;
	double dAccumNum = 0;
	char cPixVal = 0;
	int i, j;
	double dTotalNum;

	dTotalNum = CountPixels(pImage, cValue);

	if (pImage->origin == 0) //左上开始
	{
		for (i=0; i<pImage->height; i++)
		{
			dRowNum = 0;
			for (j=0; j<pImage->width; j++)
			{
				cPixVal = *(pImage->imageData + i*pImage->widthStep + j);
				if(cPixVal == cValue)
					dRowNum++;
			}
			dAccumNum += dRowNum;

			if ((dAccumNum/dTotalNum) >= dSizeValv)
			{
				iOffset = i+1;
				break;
			}
		}
	} 
	else //左下开始
	{
		for (i=pImage->height-1; i>=0; i--)
		{
			dRowNum = 0;
			for (j=0; j<pImage->width; j++)
			{
				cPixVal = *(pImage->imageData + i*pImage->widthStep + j);
				if(cPixVal == cValue)
					dRowNum++;
			}
			dAccumNum += dRowNum;

			if ((dAccumNum/dTotalNum) >= dSizeValv)
			{
				iOffset = pImage->height - i;
				break;
			}
		}
	}

	return iOffset;
}

#ifdef OBSOLETE_FUNC
void CFaceSelect::CalcVariences(IplImage* pImage, double &dMean, double &dVarient)
{
	double dSize = pImage->height * pImage->width;
	int i,j;

	dMean = 0;
	dVarient = 0;
	for (i=0; i<pImage->height; i++)
	{
		for (j=0; j<pImage->width; j++)
		{
			dMean += *(pImage->imageData + i*pImage->widthStep + j);
		}
	}
	dMean /= dSize;

	for (i=0; i<pImage->height; i++)
	{
		for (j=0; j<pImage->width; j++)
		{
			dVarient += ((double)(*(pImage->imageData + i*pImage->widthStep + j)) - dMean) * \
				((double)(*(pImage->imageData + i*pImage->widthStep + j)) - dMean);
		}
	}
	dVarient /= dSize;
}

int CFaceSelect::GetEffectiveFaceHeight(IplImage *pImage, double dSizeValv, char cValue, int iLenth)
{
	int iOffset = 0;
	double dRowNum = 0;
	char cPixVala = 0;
	char cPixValb = 0;
	int i, j;

	if (pImage->origin == 0) //左上开始
	{
		for (i=1; i<pImage->height; i++)
		{
			dRowNum = 0;
			for (j=0; j<pImage->width; j++)
			{
				cPixVala = *(pImage->imageData + i*pImage->widthStep + j);
				cPixValb = *(pImage->imageData + (i-1)*pImage->widthStep + j);
				if((cPixVala==cValue)&&(cPixValb==cValue))
					dRowNum++;
			}

			if ((dRowNum/(double)(iLenth)) >= dSizeValv)
			{
				iOffset = i+1;
				break;
			}
		}
	} 
	else //左下开始
	{
		for (i=pImage->height-2; i>=0; i--)
		{
			dRowNum = 0;
			for (j=0; j<pImage->width; j++)
			{
				cPixVala = *(pImage->imageData + i*pImage->widthStep + j);
				cPixVala = *(pImage->imageData + (i+1)*pImage->widthStep + j);
				if((cPixVala==cValue)&&(cPixValb==cValue))
					dRowNum++;
			}

			if ((dRowNum/(double)(iLenth)) >= dSizeValv)
			{
				iOffset = pImage->height - i;
				break;
			}
		}
	}

	return iOffset;
}

void CFaceSelect::GetEffectiveFaceWidthBKG(IplImage *pImage, double dSizeValv, char cValue, int &iOffL, int &iLenth)
{
	int iOffR = 0;
	double dColNum = 0;
	double dAccumNum = 0;
	char cPixVal = 0;
	int i, j;
	double dTotalNum;

	iOffL = 0;
	iLenth = pImage->width;

	dTotalNum = CountPixels(pImage, cValue);

	for (i=0; i<pImage->width; i++)
	{
		dColNum = 0;
		for (j=0; j<pImage->height; j++)
		{
			cPixVal = *(pImage->imageData + j*pImage->widthStep + i);
			if(cPixVal == cValue)
				dColNum++;
		}
		dAccumNum += dColNum;

		if ((dAccumNum/dTotalNum) >= dSizeValv)
		{
			iOffL = i+1;
			break;
		}
	}

	dAccumNum = 0;
	for (i=pImage->width-1; i>=0; i--)
	{
		dColNum = 0;
		for (j=0; j<pImage->height; j++)
		{
			cPixVal = *(pImage->imageData + j*pImage->widthStep + i);
			if(cPixVal == cValue)
				dColNum++;
		}
		dAccumNum += dColNum;

		if ((dAccumNum/dTotalNum) >= dSizeValv)
		{
			iOffR = i;
			break;
		}
	}

	iLenth = iOffR - iOffL;
}
#endif // OBSOLETE_FUNC

void CFaceSelect::GetEffectiveFaceWidth(IplImage *pImage, double dSizeValv, char cValue, int &iOffL, int &iLenth)
{
	int iOffR = 0;
	double dColNum = 0;
	char cPixVala = 0;
	char cPixValb = 0;
	int i, j;

	iOffL = 0;
	iLenth = pImage->width;

	for (i=1; i<pImage->width; i++)
	{
		dColNum = 0;
		for (j=0; j<pImage->height; j++)
		{
			cPixVala = *(pImage->imageData + j*pImage->widthStep + i);
			cPixValb = *(pImage->imageData + j*pImage->widthStep + i - 1);
			if((cPixVala==cValue)&&(cPixValb==cValue))
				dColNum++;
		}

		if (dColNum >= (double)(pImage->width)*dSizeValv)
		{
			iOffL = i;
			break;
		}
	}

	for (i=pImage->width-2; i>=0; i--)
	{
		dColNum = 0;
		for (j=0; j<pImage->height; j++)
		{
			cPixVala = *(pImage->imageData + j*pImage->widthStep + i);
			cPixValb = *(pImage->imageData + j*pImage->widthStep + i + 1);
			if((cPixVala==cValue)&&(cPixValb==cValue))
				dColNum++;
		}

		if (dColNum >= (double)(pImage->width)*dSizeValv)
		{
			iOffR = i;
			break;
		}
	}

	iLenth = iOffR - iOffL;
}

int CFaceSelect::GetHistRangeSingle(CvHistogram *pHist, int iMaxRange, double dMaxPix, int iUpper, int &iLower, double dPortion)
{
	int iMaxIdx= 0;
	float fMaxVal = 0.f;
	double dStopVal = dMaxPix*dPortion; //脸区至少m_dFaceProportion是皮肤
	int i=iUpper;

	cvGetMinMaxHistValue( pHist, 0, &fMaxVal, 0, &iMaxIdx);

	while ((double)fMaxVal < dStopVal)
	{
		fMaxVal += cvQueryHistValue_1D(pHist, i);
		i--;
		if (i<0)
		{
			break;
		}
	}
	iLower = i+1;
	return iMaxIdx;
}

int CFaceSelect::GetHistRange(CvHistogram *pHist, int iMaxRange, double dMaxPix, int &iLower, int &iUpper, double dPortion)
{
	int iMaxIdx= 0;
	float fMaxVal = 0.f;
	double dStopVal = dMaxPix*dPortion; //脸区至少m_dFaceProportion是皮肤
	int j=1;
	int k=1;  
	cvGetMinMaxHistValue( pHist, 0, &fMaxVal, 0, &iMaxIdx);

	while ((double)fMaxVal < dStopVal)
	{
		if (iMaxIdx + k > iMaxRange)
		{
			if (iMaxIdx - j < 0)
				break;

			fMaxVal += cvQueryHistValue_1D(pHist, iMaxIdx - j);
			j++;
		}
		else if (iMaxIdx - j < 0)
		{
			if (iMaxIdx + k > iMaxRange)
				break;

			fMaxVal += cvQueryHistValue_1D(pHist, iMaxIdx + k);
			k++;
		}
		else 
		{
			if ((iMaxIdx + k > iMaxRange)||(iMaxIdx - j < 0))
				break;

			if (cvQueryHistValue_1D(pHist, iMaxIdx - j) >= cvQueryHistValue_1D(pHist, iMaxIdx + k))
			{
				fMaxVal += cvQueryHistValue_1D(pHist, iMaxIdx - j);
				j++;
			}
			else
			{
				fMaxVal += cvQueryHistValue_1D(pHist, iMaxIdx + k);
				k++;
			}
		}
	}
	iLower = iMaxIdx - j;
	iUpper = iMaxIdx + k;

	return iMaxIdx;
}

double CFaceSelect::GetFaceHoriEquality(IplImage* pImage)
{
	double dEqu = 0;
	double dLeft, dMid, dRight;
	CvSize head = cvGetSize(pImage);
	int iEorO = head.width%2;
	CvRect roi_l;
	CvRect roi_r;
	CvRect roi_m;
	roi_l.x = 0;
	roi_l.y = roi_r.y = roi_m.y = 0;
	roi_r.x = roi_l.width = roi_r.width = head.width/2;
	roi_m.width = 1;
	roi_l.height = roi_r.height = roi_m.height = head.height;
	if (iEorO == 1)
	{
		roi_r.x += 1;
		roi_m.x = head.width/2;
	}

	IplImage *pFaceImage_l = cvCreateImage(cvSize(roi_l.width, roi_l.height), 8, 1);
	cvSetImageROI(pImage, roi_l);
	cvCopy(pImage, pFaceImage_l);
	cvResetImageROI(pImage);
	dLeft = CountPixels(pFaceImage_l, 0);

	IplImage *pFaceImage_r = cvCreateImage(cvSize(roi_r.width, roi_r.height), 8, 1);
	cvSetImageROI(pImage, roi_r);
	cvCopy(pImage, pFaceImage_r);
	cvResetImageROI(pImage);
	dRight = CountPixels(pFaceImage_r, 0);

	IplImage *pFaceImage_m = cvCreateImage(cvSize(roi_m.width, roi_m.height), 8, 1);
	dMid = 0;
	if (iEorO == 1)
	{
		cvSetImageROI(pImage, roi_m);
		cvCopy(pImage, pFaceImage_m);
		cvResetImageROI(pImage);
		dMid = CountPixels(pFaceImage_m, 0);
	}

	cvReleaseImage(&pFaceImage_l);
	cvReleaseImage(&pFaceImage_m);
	cvReleaseImage(&pFaceImage_r);

	dEqu = (dLeft - dRight)/(dLeft+dRight+dMid);
	dEqu = dEqu>0 ? dEqu : (-dEqu);

	return dEqu;
}

double CFaceSelect::CountPixels(IplImage *pImage, char cVal)
{
	double dcnt = 0;
	int i, j;
	char cPixVal;
	//ASSERT(pImage->nChannels == 1);

	for (i=0; i<pImage->height; i++)
	{
		for (j=0; j<pImage->width; j++)
		{
			cPixVal = *(pImage->imageData + i*pImage->widthStep + j);
			if(cPixVal == cVal)
				dcnt++;
		}
	}

	return dcnt;
}

void CFaceSelect::DeNoise(IplImage* pImage, int iW, int iH, int iWO, int iHO)
{
	IplConvKernel *pConv = cvCreateStructuringElementEx(iW, iH, iWO, iHO, CV_SHAPE_RECT);
	IplImage *pErodImg = cvCreateImage(cvGetSize(pImage), 8, 1);

	cvErode(pImage, pErodImg, pConv, 1);
	cvDilate(pErodImg, pImage, pConv, 1);

	cvReleaseImage(&pErodImg);
	cvReleaseStructuringElement(&pConv);
}

void CFaceSelect::TestFaceJudge()
{
	IplImage **ppImage;
	int iSFactor = 0;
	double dJdgFactor = 0;
	double dFaceSize = 0, dContrast = 0, dVariant = 0;
	double dVVariant = 0, dSVariant = 0, dHVariant = 0;
	char sFileName[60];
	//cvNamedWindow("ShowAllImage", 1);
	for (int i=0; i<m_cvImageSeq->total; i++)
	{
		ppImage = CV_GET_SEQ_ELEM(IplImage*, m_cvImageSeq, i);
		FaceJudge(*ppImage, dJdgFactor, iSFactor, dFaceSize, dSVariant, dVVariant, dHVariant, dContrast, dVariant);
		sprintf_s(sFileName, 60, "%d_%5f_.bmp", iSFactor, (float)(dJdgFactor));
		cvSaveImage(sFileName, *ppImage);
		//cvShowImage("ShowAllImage", *ppImage);
		//cvWaitKey(iIntervalTime);
	}
	//cvDestroyWindow("ShowAllImage");
}

///////////////////////////////////////////找脸相关/////////////////////////////////////////////////////
void CFaceSelect::SpecialEqualizeHist( const CvArr* src, int iMaxIdx, float fMaxVal, CvArr* dst )
{
	CvHistogram* hist = 0;
	CvMat* lut = 0;
	IplImage *pBinImg = 0;
	IplImage *pEquImg = 0;
	IplImage *pTmpImgA = 0;
	IplImage *pTmpImgB = 0;

	__BEGIN__;

	int i, hist_sz = 256;
	CvSize img_sz;
	float scale;
	float* h;
	int sum = 0;
	int type;
	int iBkgNum;
	int iForgNum;
	float *fMaxHistVal;

	CV_FUNCNAME( "SpecialEqualizeHist" );

	CV_CALL( type = cvGetElemType( src ));
	if( type != CV_8UC1 )
		CV_ERROR( CV_StsUnsupportedFormat, "Only 8uC1 images are supported" );

	CV_CALL( hist = cvCreateHist( 1, &hist_sz, CV_HIST_ARRAY ));
	CV_CALL( lut = cvCreateMat( 1, 256, CV_8UC1 ));
	CV_CALL( cvCalcArrHist( (CvArr**)&src, hist ));
	CV_CALL( img_sz = cvGetSize( src ));

	CV_CALL( pBinImg = cvCreateImage(img_sz, 8, 1));
	CV_CALL( pEquImg = cvCreateImage(img_sz, 8, 1));
	CV_CALL( pTmpImgA = cvCreateImage(img_sz, 8, 1));
	CV_CALL( pTmpImgB = cvCreateImage(img_sz, 8, 1));

	double dCutPortion =1 - (1 - (double)fMaxVal/(double)(img_sz.width*img_sz.height))*1/3;
	int iL, iU;
	int iDummy = GetHistRange(hist, 255, img_sz.width*img_sz.height, iL, iU, dCutPortion);

	for( i = 0; i <= iL; i++ )
	{
		lut->data.ptr[i] = (uchar)0xFF;
	}
	for (i=iL+1; i<iU; i++)
	{
		lut->data.ptr[i] = 0;
	}
	for( i=iU; i<256; i++ )
	{
		lut->data.ptr[i] = (uchar)0xFF;
	}
	CV_CALL( cvLUT( src, pBinImg, lut ));
	
	CV_CALL( DeNoise( pBinImg, 3, 3, 1, 1 ));
	//CV_CALL( cvSaveImage( "..\\Debug\\binary_DN.bmp", pBinImg ));
	CV_CALL( iForgNum = cvCountNonZero( pBinImg ));
	iBkgNum = img_sz.width*img_sz.height - iForgNum;
	CV_CALL( cvAnd( src, pBinImg, pTmpImgA ));
	//CV_CALL( cvSaveImage( "..\\Debug\\TmpA.bmp", pTmpImgA ));
	//DrawHistImage_H(hist, 255, img_sz.width*img_sz.height);
	CV_CALL( cvClearHist(hist ));
	CV_CALL( cvCalcArrHist( (CvArr**)&pTmpImgA, hist ));
	//DrawHistImage(hist, 255, img_sz.width*img_sz.height);
	CV_CALL( fMaxHistVal = cvGetHistValue_1D( hist, 0 ));
	*fMaxHistVal = *fMaxHistVal - iBkgNum;
	//DrawHistImage_H(hist, 255, iForgNum);

	scale = 255.f/(iForgNum);
	h = (float*)cvPtr1D( hist->bins, 0 );

	for( i = 0; i < hist_sz; i++ )
	{
		sum += cvRound(h[i]);
		lut->data.ptr[i] = (uchar)cvRound(sum*scale);
	}

	lut->data.ptr[0] = 0;
	CV_CALL( cvLUT( src, pEquImg, lut ));
	CV_CALL( cvAnd( pEquImg, pBinImg, pTmpImgA ));
	CV_CALL( cvNot( pBinImg, pBinImg ));
	CV_CALL( cvAnd( src, pBinImg, pTmpImgB));
	CV_CALL( cvOr( pTmpImgA, pTmpImgB, dst ));

	/*
	CV_CALL( cvSaveImage( "..\\Debug\\Orignal.bmp", src ));
	CV_CALL( cvSaveImage( "..\\Debug\\Enhanced.bmp", dst ));
	CvHistogram* dsthist = 0;
	CV_CALL( dsthist = cvCreateHist( 1, &hist_sz, CV_HIST_ARRAY ));
	CV_CALL( cvCalcArrHist( (CvArr**)&dst, dsthist ));
	DrawHistImage_H(dsthist, 255, iForgNum+iBkgNum);
	cvReleaseHist(&hist);
	//*/

	__END__;

	cvReleaseHist(&hist);
	cvReleaseMat(&lut);
	cvReleaseImage(&pBinImg);
	cvReleaseImage(&pEquImg);
	cvReleaseImage(&pTmpImgA);
	cvReleaseImage(&pTmpImgB);
}

int CFaceSelect::ContrastJudge(IplImage *imgGray, float &fMaxVal, int &iMaxIdx, double dFold )
{
	float fChnlRang[2] = {0, 255}; //直方图范围
	float *pfChnlRang = fChnlRang;
	int iDimSize = 256;
	CvHistogram *pGrayHist = cvCreateHist(1, &iDimSize, CV_HIST_ARRAY, (float**)(&pfChnlRang),1);
	cvCalcHist(&imgGray, pGrayHist);
	cvGetMinMaxHistValue(pGrayHist, 0, &fMaxVal, 0, &iMaxIdx);
	double dPortion = (double)fMaxVal/(double)(imgGray->width*imgGray->height);
	int iL, iU, iDummy;
	dPortion = (dFold*dPortion)>0.99 ? 0.99/dFold : dPortion;
	if (dFold>=1)
		iDummy = GetHistRange(pGrayHist, 255, imgGray->width*imgGray->height,iL, iU, dPortion*dFold);
	else
		iDummy = GetHistRange(pGrayHist, 255, imgGray->width*imgGray->height,iL, iU, dFold);
	int iRang = iU-iL;
	cvReleaseHist(&pGrayHist);
	return iRang;
}

CvSeq* CFaceSelect::FaceDetect( IplImage* pImage,  CvMemStorage* pStorage, CvSize rcMinSize, bool bScale, double dScale)
{
	if (!pImage)
	{
		return 0;
	}

	if( !m_cascade_face )
	{
		return 0;
	}

	if( !pStorage )
	{
		return 0;
	}

	if( pImage->depth != 8 || pImage->nChannels != 3 )
	{
		return 0;
	}

	cvClearMemStorage( pStorage );

	CvSeq *pFaces = 0;
	IplImage* img = pImage;
	CvSeq* faces = 0;
	CvMemStorage* storage = cvCreateMemStorage(0);

	//Michael Add 2009508 -- Set ROI
	bool bGetSubImg = false;
	if( m_rcROI.width > 1 && m_rcROI.height > 1 )
	{
		if( ( m_rcROI.x + m_rcROI.width ) == img->width ) m_rcROI.width = m_rcROI.width - 1;
		if( ( m_rcROI.y + m_rcROI.height ) == img->height ) m_rcROI.height = m_rcROI.height - 1;

		if( m_rcROI.x >= 0 && m_rcROI.x + m_rcROI.width < img->width
			&& m_rcROI.y >= 0 && m_rcROI.y + m_rcROI.height < img->height )
		{
			IplImage* imgSub = GetSubImage( img, m_rcROI );
			img = imgSub;
			imgSub = 0;

			bGetSubImg = true;
		}
	}
	//cvSaveImage( "cutimg.jpg", img );
	//End -- Set ROI

	if (bScale == 1)
	{
		if( bGetSubImg )
		{
			IplImage* imgBak = cvCloneImage( img );
			cvReleaseImage( &img );
			img = NULL;
			ResizeImg( imgBak, img, dScale );
			cvReleaseImage( &imgBak );
		}
		else
		{
			ResizeImg( pImage, img, dScale );//降采样 Michael Change 20090508 -- 考虑划定乐区域的情况，img中存储子区域图
		}
		
		//Michael Add 20090508 -- Convert rcMinSize
		rcMinSize.height = (int)(rcMinSize.height*dScale);
		rcMinSize.width = (int)(rcMinSize.width*dScale);
		//End -- Convert rcMinSize
	}

	IplImage* imgGray = cvCreateImage( cvSize(img->width,img->height), 8, 1 );
	cvCvtColor( img, imgGray, CV_BGR2GRAY );

	//判断对比度
	/*
	float fChnlRang[2] = {0, 255}; //直方图范围
	float *pfChnlRang = fChnlRang;
	int iDimSize = 256;
	CvHistogram *pGrayHist = cvCreateHist(1, &iDimSize, CV_HIST_ARRAY, (float**)(&pfChnlRang),1);
	cvCalcHist(&imgGray, pGrayHist);
	float fMaxVal;
	int iMaxIdx;
	cvGetMinMaxHistValue(pGrayHist, 0, &fMaxVal, 0, &iMaxIdx);
	double dPortion = (double)fMaxVal/(double)(imgGray->width*imgGray->height);
	int iL, iU;
	dPortion = dPortion>0.99 ? 0.99 : dPortion;
	int iDummy = GetHistRange(pGrayHist, 255, imgGray->width*imgGray->height,iL, iU, dPortion*3);
	int iRang = iU-iL;
	cvReleaseHist(&pGrayHist);
	//*/

	float fMaxVal;
	int iMaxIdx;
	int iRang = ContrastJudge(imgGray, fMaxVal, iMaxIdx, 3);
	CvSize max_size;
	max_size.width = (int)(rcMinSize.width*m_dFaceChangeRatio);
	max_size.height = (int)(rcMinSize.height*m_dFaceChangeRatio);

	IplImage* imgBinary = cvCreateImage( cvSize(img->width,img->height), 8, 1 );

	if (iRang<8)
	{
		//SkinFilter(img, imgGray, iMaxIdx, false, imgBinary);

		cvEqualizeHist( imgGray, imgGray );

		if( m_cascade_face )
		{
			//Debug 测时
			//double t0 = (double)cvGetTickCount();

			faces = cvSpecialHaarDetectObjects( max_size, imgBinary, imgGray, m_cascade_face, storage,
				1.1, 3, CV_HAAR_DO_CANNY_PRUNING,
				rcMinSize );

			//Debug 测时
			//double t = (double)cvGetTickCount() - t0;
			//t /= ((double)cvGetTickFrequency()*1000);
			//#ifdef TIME_ANA
			//		FILE *fp;
			//		fp = fopen("time_faceDetect.txt","w+");
			//		fprintf(fp,"%gms\n ",t);
			//		fclose( fp );
			//#endif
		}
		int iRealFace = faces?faces->total:0;
		cvSeqPush(m_cvRealFaceSeq, &iRealFace);

		CvSeq *facesec = 0;
		CvMemStorage *storagesec = cvCreateMemStorage(0);
		if( m_cascade_face )
		{
			//Debug 测时
			//double t0 = (double)cvGetTickCount();
			
			int iNeighbour;
			if (m_iLightCondition==0)
				iNeighbour=1;
			if (m_iLightCondition==1)
				iNeighbour=2;
			if (m_iLightCondition==2)
				iNeighbour=2;

			facesec = cvSpecialHaarDetectObjects( max_size, imgBinary, imgGray, m_cascade_face, storagesec,
				1.1, iNeighbour, CV_HAAR_DO_CANNY_PRUNING,
				rcMinSize );

			if (faces==0)
			{
				faces = facesec;
			}
			else
			{
				for (int i=0; i<(facesec?facesec->total:0); i++)
				{
					CvRect* rec = (CvRect*)cvGetSeqElem( facesec, i );
					cvSeqPush( faces, rec );
				}
			}
		}
		
		if (m_cascade_face)
		{
			if (bScale == 1)
			{
				pFaces = cvCreateSeq( 0, sizeof(CvSeq), sizeof(CvRect), pStorage );
				GetOrgRectSeq( pFaces, faces, dScale );
			} 
			else
			{
				pFaces = cvCloneSeq(faces, pStorage);
			}		
		}
		cvReleaseMemStorage(&storagesec);
	}
	else
	{
		//SkinFilter(img, imgGray, iMaxIdx, true, imgBinary);
//		cvSaveImage("bin.jpg", imgBinary);

		int iNeighbour_a, iNeighbour_b;
		if (m_iLightCondition==0)
		{
			iNeighbour_a=1;
			iNeighbour_b=1;
		}
		if (m_iLightCondition==1)
		{
			iNeighbour_a=2;
			iNeighbour_b=1;
		}
		if (m_iLightCondition==2)
		{
			iNeighbour_a=1;
			iNeighbour_b=1;
		}

		//*
		if( m_cascade_face )
		{
			//Debug 测时
			//double t0 = (double)cvGetTickCount();

			faces = cvSpecialHaarDetectObjects( max_size, imgBinary, imgGray, m_cascade_face, storage,
				1.1, 3, CV_HAAR_DO_CANNY_PRUNING,
				rcMinSize );		
		}
		int iRealFace = faces?faces->total:0;

		CvSeq* facesext = 0;
		CvMemStorage* storagext = cvCreateMemStorage(0);
		if( m_cascade_face )
		{
			//Debug 测时
			//double t0 = (double)cvGetTickCount();

			facesext = cvSpecialHaarDetectObjects( max_size, imgBinary, imgGray, m_cascade_face, storagext,
				1.1, iNeighbour_a, CV_HAAR_DO_CANNY_PRUNING,
				rcMinSize );		
		}
	//*/

		cvSmooth(imgGray, imgGray);
		SpecialEqualizeHist(imgGray, iMaxIdx, fMaxVal, imgGray);

		CvSeq *facesec = 0;
		CvSeq *facesecext = 0;
		CvMemStorage *storagesec = cvCreateMemStorage(0);
		CvMemStorage *storagesecext = cvCreateMemStorage(0);
		if( m_cascade_face )
		{
			//Debug 测时
			//double t0 = (double)cvGetTickCount();

			facesec = cvSpecialHaarDetectObjects( max_size, imgBinary, imgGray, m_cascade_face, storagesec,
				1.1, 3, CV_HAAR_DO_CANNY_PRUNING,
				rcMinSize );
		}
		iRealFace += facesec?facesec->total:0;
		cvSeqPush(m_cvRealFaceSeq, &iRealFace);

		if( m_cascade_face )
		{
			//Debug 测时
			//double t0 = (double)cvGetTickCount();

			facesecext = cvSpecialHaarDetectObjects( max_size, imgBinary, imgGray, m_cascade_face, storagesecext,
				1.1, iNeighbour_b, CV_HAAR_DO_CANNY_PRUNING,
				rcMinSize );
		}

		if( m_cascade_face )
		{
			if (faces==0)
			{
				faces = facesec;
			}
			else
			{
				for (int i=0; i<(facesec?facesec->total:0); i++)
				{
					CvRect* rec = (CvRect*)cvGetSeqElem( facesec, i );
					cvSeqPush( faces, rec );
				}
			}

			if (faces==0)
			{
				faces = facesext;
			}
			else
			{
				for (int i=0; i<(facesext?facesext->total:0); i++)
				{
					CvRect* rec = (CvRect*)cvGetSeqElem( facesext, i );
					cvSeqPush( faces, rec );
				}
			}

			if (faces==0)
			{
				faces = facesecext;
			}
			else
			{
				for (int i=0; i<(facesecext?facesecext->total:0); i++)
				{
					CvRect* rec = (CvRect*)cvGetSeqElem( facesecext, i );
					cvSeqPush( faces, rec );
				}
			}
		}

		if (m_cascade_face)
		{
			if (bScale == 1)
			{
				pFaces = cvCreateSeq( 0, sizeof(CvSeq), sizeof(CvRect), pStorage );
				GetOrgRectSeq( pFaces, faces, dScale );
			} 
			else
			{
				pFaces = cvCloneSeq(faces, pStorage);
			}
		}
		cvReleaseMemStorage(&storagext);
		cvReleaseMemStorage(&storagesec);
		cvReleaseMemStorage(&storagesecext);
	}

	//Michael Add 20090508 -- Transfer face in cvROI to Whole Image
	if( bGetSubImg && pFaces )
	{
		int nFaceId  = 0;
		for( nFaceId = 0; nFaceId < pFaces->total; nFaceId++ )
		{
			CvRect* r = (CvRect*)cvGetSeqElem( pFaces, nFaceId );
			transferLocalRc2SceneRc( r, r, &m_rcROI, 1.0f );
		}
	}
	//End -- Transfer face in cvROI to Whole Image

	if ( bScale == 1 || bGetSubImg )//Michael Change 20090508 -- For GetSubImg
	{
		cvReleaseImage(&img);
	}
	cvReleaseImage( &imgGray );
	cvReleaseImage(&imgBinary);
	cvReleaseMemStorage(&storage);

	return pFaces;
}


void CFaceSelect::ResizeImg( IplImage* imgOrg, IplImage* &imgDes, double dScale )
{
	imgDes = cvCreateImage( cvSize( cvRound (imgOrg->width * dScale),
		cvRound (imgOrg->height * dScale)),
		imgOrg->depth, imgOrg->nChannels );
	cvResize( imgOrg, imgDes, CV_INTER_LINEAR );

}

bool CFaceSelect::GetOrgRect( CvRect* rcOrg, const CvRect* rcScale, double dScale )
{
	if( !rcOrg ) return false;

	rcOrg->x = int( rcScale->x / dScale );
	rcOrg->y = int( rcScale->y / dScale );
	rcOrg->width = int( rcScale->width / dScale );
	rcOrg->height = int( rcScale->height / dScale );

	return true;
}

bool CFaceSelect::GetOrgRectSeq( CvSeq* orgFaces, const CvSeq* scaleFaces, double dScale )
{
	if( !orgFaces || !scaleFaces ) return false;

	cvClearSeq( orgFaces );

	int i = 0;
	int nfaces = scaleFaces->total;
	for( i = 0; i < nfaces; i++ )
	{
		CvRect* r = (CvRect*)cvGetSeqElem( scaleFaces, i );
		CvRect rScale = *r;
		GetOrgRect( &rScale, r, dScale );

		cvSeqPush( orgFaces, &rScale );
	}

	return true;
}


bool CFaceSelect::InitCascade( const char* cascade_name, CvHaarClassifierCascade* &cascade, CvMemStorage *casMem )
{
	if( !cascade )
	{
		casMem = cvCreateMemStorage(0);
		cascade = (CvHaarClassifierCascade*)cvLoad( cascade_name, casMem, 0, 0 );

		if( !cascade )
		{
			return false;
		}
	}
	return true;
}

void CFaceSelect::ReleaseCascade( CvHaarClassifierCascade* &cascade, CvMemStorage *casMem )
{
	if( cascade )
	{
		cvReleaseHaarClassifierCascade( &cascade );
		cvReleaseMemStorage(&casMem);
	}
}

////////////////////////////////////END -- 找脸相关///////////////////////////////////////////////
bool CFaceSelect::NeedEnhance(IplImage *pImage)
{
	IplImage *pGray = cvCreateImage(cvGetSize(pImage), 8, 1);
	cvCvtColor(pImage, pGray, CV_BGR2GRAY);
	int iDimSize = 256;
	CvHistogram *pHist = cvCreateHist(1, &iDimSize, CV_HIST_ARRAY);
	cvCalcHist(&pGray, pHist, 0, 0);
	float fMaxVal;
	int iMaxIdx;
	cvGetMinMaxHistValue( pHist, 0, &fMaxVal, 0, &iMaxIdx);
	cvReleaseHist(&pHist);
	cvReleaseImage( &pGray );

	if (iMaxIdx>110)
		return false;
	else
		return true;
}

void CFaceSelect::RyanDebug(const char *cFileName)
{
	static int iFaceNum = 0;
	static int iImageNum = 0;
	int iSFactor = 0;
	double dVVariant, dSVariant, dHVariant;
	double dJdgFactor = 0;
	char sFileName[100];
	double dSize = 0;
	double dSizeFactor = 0, dContrast = 0, dVariant = 0;
	double dConf;
	double dEnhConf;
	CvScalar color = {0,0,0,0};
	int iBasicSize = m_iFaceSize;

	IplImage *pImage = cvLoadImage(cFileName, CV_LOAD_IMAGE_UNCHANGED);
	IplImage *pDrawImage = cvLoadImage(cFileName, CV_LOAD_IMAGE_UNCHANGED);

	//找脸并存储
	CvMemStorage *storage = cvCreateMemStorage(0);
	CvSeq* faces = FaceDetect( pImage, storage, cvSize(m_iFaceSize, m_iFaceSize), m_bScaleFaceDetection, m_dScale);

	//Debug 画脸
	for( int ii = 0; ii < (faces?faces->total:0); ii++ )
	{
		bool bRealFace;
		CvRect* r = (CvRect*)cvGetSeqElem( faces, ii );
		CvRect rScale = *r;

		CvRect recUpBody;
		GetUpperbody(r, cvGetSize(pImage), &recUpBody);

		if (m_iLightCondition==0)
		{
			//判断脸框大小
			if ((r->height > iBasicSize*m_dFaceChangeRatio)||(r->width > iBasicSize*m_dFaceChangeRatio))
			{
				bRealFace = false;
			}
			else
			{
				bRealFace = true;
				dConf = 0.0f;
				dEnhConf = 0.0f;

				//* //检测并保存人脸
				IplImage *SubFace = GetSubImage(pImage, rScale);
				FaceJudge(SubFace, dJdgFactor, iSFactor, dSize, dSVariant, dVVariant, dHVariant, dContrast, dVariant);

				if ((iSFactor < m_iSFactorValv) || (dVariant<7) || (dVariant>20) ||(dContrast>30))
				{
					bRealFace = false;

					//*
					dConf = 0.0f;
					rateFace( SubFace, dConf );

					bool bNeedEnhance = NeedEnhance(SubFace);

					dEnhConf = 0.0f;
					if (bNeedEnhance)
					{
						IplImage *pEnhSubFace = GetSubImage(pImage, rScale);
						ColorImageEnhance(pEnhSubFace);
						rateFace(pEnhSubFace, dEnhConf);
						cvReleaseImage(&pEnhSubFace);
					}

					if (dConf<0.5 && dEnhConf<0.5)
					{
						bRealFace = false;
					}
					else
					{
						bRealFace = true;
					}
					//*/
				}
				else
				{
					bRealFace = true;

					//*
					dConf = 0.0f;
					rateFace( SubFace, dConf );

					bool bNeedEnhance = NeedEnhance(SubFace);

					dEnhConf = 0.0f;
					if (bNeedEnhance)
					{
						IplImage *pEnhSubFace = GetSubImage(pImage, rScale);
						ColorImageEnhance(pEnhSubFace);
						rateFace(pEnhSubFace, dEnhConf);
						cvReleaseImage(&pEnhSubFace);
					}
					//*/
				}

				if (dConf>0.80 || dEnhConf>0.80)
				{
					bRealFace = true;
				} 
				else
				{					
					//直方图判据
					if (bRealFace == true)
					{						
						if (dHVariant>400)
						{
							bRealFace = true;
						} 
						else if (dHVariant<150)
						{
							bRealFace = false;
						}
						else if (dHVariant < 250)
						{
							if (dVVariant < 90)
							{
								bRealFace = true;
							} 
							else
							{
								bRealFace = false;
							}
						}
						else
						{
							if (dVVariant < 150)
							{
								bRealFace = true;
							} 
							else
							{
								bRealFace = false;
							}
						}
					}					
				}
				cvReleaseImage(&SubFace);
			}
		}
		if (m_iLightCondition==1)
		{
			//判断脸框大小
			if ((r->height > iBasicSize*m_dFaceChangeRatio)||(r->width > iBasicSize*m_dFaceChangeRatio))
			{
				bRealFace = false;
			}
			else
			{
				bRealFace = true;
				dConf = 0.0f;
				dEnhConf = 0.0f;

				//* //检测并保存人脸
				IplImage *SubFace = GetSubImage(pImage, rScale);
				FaceJudge(SubFace, dJdgFactor, iSFactor, dSize, dSVariant, dVVariant, dHVariant, dContrast, dVariant);

				if ((iSFactor < m_iSFactorValv) || (dVariant<7) || (dVariant>20) ||(dContrast>30)||(dContrast<5))
				{
					bRealFace = false;

					//*
					dConf = 0.0f;
					rateFace( SubFace, dConf );

					bool bNeedEnhance = NeedEnhance(SubFace);

					dEnhConf = 0.0f;
					if (bNeedEnhance)
					{
						IplImage *pEnhSubFace = GetSubImage(pImage, rScale);
						ColorImageEnhance(pEnhSubFace);
						rateFace(pEnhSubFace, dEnhConf);
						cvReleaseImage(&pEnhSubFace);
					}

					if (dConf<0.5 && dEnhConf<0.5)
					{
						bRealFace = false;
					}
					else
					{
						bRealFace = true;
					}
					//*/
				}
				else
				{
					bRealFace = true;

					//*
					dConf = 0.0f;
					rateFace( SubFace, dConf );

					bool bNeedEnhance = NeedEnhance(SubFace);

					dEnhConf = 0.0f;
					if (bNeedEnhance)
					{
						IplImage *pEnhSubFace = GetSubImage(pImage, rScale);
						ColorImageEnhance(pEnhSubFace);
						rateFace(pEnhSubFace, dEnhConf);
						cvReleaseImage(&pEnhSubFace);
					}
					//*/
				}

				if (dConf>0.60 || dEnhConf>0.60)
				{
					bRealFace = true;
				} 
				else
				{
					//*
					//直方图判据
					if (bRealFace == true)
					{
						if (dHVariant>200)
						{
							bRealFace = true;
						} 
						else if (dHVariant<80)
						{
							bRealFace = false;
						}
						else
						{
							if (dVVariant < 350)
							{
								bRealFace = true;
							} 
							else
							{
								bRealFace = false;
							}
						}						
					}
					//*/
				}
				cvReleaseImage(&SubFace);
			}
		}
		if (m_iLightCondition==2)
		{
			//判断脸框大小
			if ((r->height > iBasicSize*m_dFaceChangeRatio)||(r->width > iBasicSize*m_dFaceChangeRatio))
			{
				bRealFace = false;
			}
			else
			{
				bRealFace = true;
				dConf = 0.0f;
				dEnhConf = 0.0f;

				//* //检测并保存人脸
				IplImage *SubFace = GetSubImage(pImage, rScale);

				//*
				dConf = 0.0f;
				rateFace( SubFace, dConf );

				bool bNeedEnhance = NeedEnhance(SubFace);

				dEnhConf = 0.0f;
				if (bNeedEnhance)
				{
					IplImage *pEnhSubFace = GetSubImage(pImage, rScale);
					ColorImageEnhance(pEnhSubFace);
					rateFace(pEnhSubFace, dEnhConf);
					cvReleaseImage(&pEnhSubFace);
				}

				if (dConf>0.6)
					bRealFace = true;
				else
					bRealFace = false;

				if (bRealFace==false)
				{
					if (bNeedEnhance)
					{
						if (dConf<=0.2 && dEnhConf<=0.2)
						{
							bRealFace = false;
						}
						else
						{
							if (dEnhConf>0.5)
							{
								bRealFace=true;
							}
							else
							{
								/////////////////////////////////////
								FaceJudge(SubFace, dJdgFactor, iSFactor, dSize, dSVariant, dVVariant, dHVariant, dContrast, dVariant);
								if ((dVariant<8) || (dVariant>25) ||(dContrast>40)||(dContrast<6)||(dHVariant<20)||((iSFactor<23) && (dVVariant>100))||((iSFactor<23) && (dHVariant<50)))
								{
									bRealFace = false;
								}
								else
								{
									if ((iSFactor<23)&&(dVVariant<15)&&(dEnhConf<0.4))
									{
										bRealFace = false;
									}
									else
										bRealFace = true;
								}
							}
						}
					}
					else
					{
						if (dConf<0.4)
						{
							bRealFace = false;
						}
						else
						{
							////////////////////////////////////////////
							FaceJudge(SubFace, dJdgFactor, iSFactor, dSize, dSVariant, dVVariant, dHVariant, dContrast, dVariant);
							if ((iSFactor>15)&&(dVariant>7) && (dVariant<25) && (dContrast<30)&&(dContrast>4.5)&&(dHVariant>150))
							{
								bRealFace = true;
							}
							else
							{
								if ((iSFactor<=15)&&(dVariant>7) && (dVariant<30) && (dContrast<30) && (dHVariant>550))
									bRealFace = true;
								else
									bRealFace = false;
							}
						}
					}
				}
				cvReleaseImage(&SubFace);
			}
		}
		
		if (bRealFace == false)
		{
			sprintf_s(sFileName, 100, "..\\Result\\Fake\\Face%d.bmp", iFaceNum++);
			color.val[0] = 255;
			color.val[2] = 0;
			recUpBody = rScale;
			//recUpBody.height = recUpBody.width = recUpBody.x = recUpBody.y = 0;
		}
		else
		{
			static double stcdStdSize = dSize;
			dSizeFactor = stcdStdSize/dSize*10;

			if (iSFactor<31)
			{
				dJdgFactor = dJdgFactor / (dJdgFactor<10 ? 2.5:3.5);
			}
			dJdgFactor = dJdgFactor<1?dJdgFactor*3:dJdgFactor;
			double dCombin = (dJdgFactor+dVariant+dContrast)/3;

			color.val[0] = 0;
			color.val[2] = 255;
			sprintf_s(sFileName, 100, "..\\Result\\Real\\Face%d__%5f__%5f.bmp", iFaceNum++, (float)(dCombin), (float)(dSizeFactor));
			//sprintf_s(sFileName, 200, "../Debug/Face%d____%5f__%5f__%5f.jpg", iFaceNum++, (float)dHVariant, (float)dVVariant, (float)dSVariant);
		}
	
		CvRect rSave;
		rSave.height = rScale.height*2;
		rSave.width = rScale.width;
		rSave.x = rScale.x;
		rSave.y = rScale.y;
		if (rSave.y+rSave.height > pImage->height)
			rSave.height = pImage->height-rSave.y;
		IplImage *pSaveImg = GetSubImage(pImage,rSave);
		cvSaveImage(sFileName, pSaveImg);
		cvReleaseImage(&pSaveImg);

		cvRectangle(pDrawImage,cvPoint(recUpBody.x,recUpBody.y),cvPoint( recUpBody.x+recUpBody.width, recUpBody.y+recUpBody.height), color, 2, 8, 0 );
		//*/
	}
	sprintf_s(sFileName, 100, "..\\Result\\Image\\Image%d.jpg", iImageNum++);
	cvSaveImage(sFileName, pDrawImage);

	cvReleaseImage(&pDrawImage);
	cvReleaseImage(&pImage);
	cvReleaseMemStorage(&storage);
}

void CFaceSelect::StepLengthen(IplImage *pImage, double dFactorL, double dFactorH)
{
	double dBottom, dTop;
	cvMinMaxLoc(pImage, &dBottom, &dTop);

	int iDimSize = 256;
	CvHistogram *pHist = cvCreateHist(1, &iDimSize, CV_HIST_ARRAY);
	cvCalcHist(&pImage, pHist, 0, 0);

	float *fHist = (float*)cvPtr1D( pHist->bins, 0 );
	double dValNum = (double)(pImage->width*pImage->height)*dFactorL;
	double dAccumNum = 0;
	int iIdx = 0;
	while ((dAccumNum < dValNum)&&(iIdx < 255))
	{
		dAccumNum += fHist[iIdx];
		iIdx++;
	}
	int iDown = iIdx;

	dValNum = (double)(pImage->width*pImage->height)*dFactorH;
	dAccumNum = 0;
	iIdx = 255;
	while ((dAccumNum<dValNum)&&(iIdx > 0))
	{
		dAccumNum += fHist[iIdx];
		iIdx--;
	}
	int iUp = iIdx;
	double dDiffer = iUp - iDown;

	CvMat *pLut = cvCreateMat(1, 256, CV_8UC1);
	for (double i = dBottom; i<=dTop; i++)
	{
		pLut->data.ptr[(int)i] = (uchar)cvRound((i - dBottom)*255.0/dDiffer);
	}

	cvLUT(pImage, pImage, pLut);

	cvReleaseHist(&pHist);
	cvReleaseMat(&pLut);
}

void CFaceSelect::ColorImageEnhance(IplImage *pImage)
{
	/*
	IplImage *pBImg = cvCreateImage(cvGetSize(pImage), 8, 1);
	IplImage *pGImg = cvCreateImage(cvGetSize(pImage), 8, 1);
	IplImage *pRImg = cvCreateImage(cvGetSize(pImage), 8, 1);

	cvCvtPixToPlane(pImage, pBImg, pGImg, pRImg, 0);
	//StepLengthen(pBImg, 0.0, 0.0);
	//StepLengthen(pGImg, 0.0, 0.0);
	//StepLengthen(pRImg, 0.0, 0.0);
	cvSmooth(pBImg, pBImg);
	cvSmooth(pGImg, pGImg);
	cvSmooth(pRImg, pRImg);
	cvEqualizeHist(pBImg, pBImg);
	cvEqualizeHist(pGImg, pGImg);
	cvEqualizeHist(pRImg, pRImg);
	cvCvtPlaneToPix(pBImg, pGImg, pRImg, 0, pImage);

	cvReleaseImage(&pBImg);
	cvReleaseImage(&pGImg);
	cvReleaseImage(&pRImg);
	*/

	IplImage *pHSVImg = cvCreateImage(cvGetSize(pImage), 8, 3);
	cvCvtColor(pImage, pHSVImg, CV_BGR2YCrCb);
	IplImage *pBImg = cvCreateImage(cvGetSize(pImage), 8, 1);
	IplImage *pGImg = cvCreateImage(cvGetSize(pImage), 8, 1);
	IplImage *pRImg = cvCreateImage(cvGetSize(pImage), 8, 1);

	cvCvtPixToPlane(pHSVImg, pBImg, pGImg, pRImg, 0);
	//*
	//StepLengthen(pBImg, 0.0, 0.002);
	cvSmooth(pBImg, pBImg);
	cvEqualizeHist(pBImg, pBImg);
	cvSmooth(pBImg, pBImg, CV_MEDIAN);
	//*/

	/*
	CvMat *pLut = cvCreateMat(1, 256, CV_8UC1);
	for (double i=0; i<256; i++)
	{
		double tmp = pow(i/255.0, 0.67)*255.0;
		pLut->data.ptr[(uchar)i] = (uchar)cvRound(tmp);
	}
	cvLUT(pBImg, pBImg, pLut);
	cvReleaseMat(&pLut);
	//*/
	
	cvCvtPlaneToPix(pBImg, pGImg, pRImg, 0, pHSVImg);
	cvCvtColor(pHSVImg, pImage, CV_YCrCb2BGR);

	cvReleaseImage(&pBImg);
	cvReleaseImage(&pGImg);
	cvReleaseImage(&pRImg);
	cvReleaseImage(&pHSVImg);
}

void CFaceSelect::EnhanceTest(const char *cFileName)
{
	IplImage *pImage = cvLoadImage(cFileName, CV_LOAD_IMAGE_UNCHANGED);
	static int iFaceNum = 0;
	char sFileName[200];

	IplImage *pGray = cvCreateImage(cvGetSize(pImage), 8, 1);
	cvCvtColor(pImage, pGray, CV_BGR2GRAY);
	int iDimSize = 256;
	CvHistogram *pHist = cvCreateHist(1, &iDimSize, CV_HIST_ARRAY);
	cvCalcHist(&pGray, pHist, 0, 0);
	float fMaxVal;
	int iMaxIdx;
	cvGetMinMaxHistValue( pHist, 0, &fMaxVal, 0, &iMaxIdx);
	cvNormalizeHist(pHist, 1);

	double dVariant = 0;
	/*
	CvScalar sMean;
	CvScalar sVaria;
	double dMean;
	cvAvgSdv(pHist->bins, &sMean, &sVaria, 0);
	dMean = sMean.val[0];
	dVariant = sVaria.val[0];
*/
	double dDummy;
	CalcHistVariences(pHist, iDimSize-1, dDummy, dVariant);

	//sprintf_s(sFileName, 200, "Enhanced%d__%5f.bmp", iFaceNum++, (float)dVariant);
	sprintf_s(sFileName, 200, "Enhanced%d__%d.bmp", iFaceNum++, iMaxIdx);

	ColorImageEnhance(pImage);
	cvSaveImage(sFileName, pImage);
	//StepLengthen(pGray, 0.055, 0.055);
	//cvSaveImage(sFileName, pGray);

	cvReleaseHist(&pHist);
	cvReleaseImage(&pGray);
	cvReleaseImage(&pImage);
}

void CFaceSelect::EyeFaceTest(const char *cFileName)
{
	IplImage *pImage = cvLoadImage(cFileName, CV_LOAD_IMAGE_UNCHANGED);

	static int iFaceNum = 0;
	int iSFactor = 0;
	double dJdgFactor = 0;
	char sFileName[400];
	CvRect rSub;
	rSub.height = pImage->height/2;
	rSub.width = pImage->width;
	rSub.x = rSub.y = 0;
	IplImage *pFaceImg = GetSubImage(pImage, rSub);
	char cTmpName[8];
	strncpy_s(cTmpName, 8, cFileName+31, _TRUNCATE);

	//ColorImageEnhance(pFaceImg);

	/*
	double dConf = 0.0f;
	rateFace( pFaceImg, dConf );
	if( dConf < 0.5 )
	{
		sprintf_s(sFileName, 200, "../Debug/False/_Img%d__%5f.jpg",  iFaceNum++, (float)(dConf));
	}
	else
	{
		sprintf_s(sFileName, 200, "../Debug/True/_Img%d__%5f.jpg",  iFaceNum++,  (float)(dConf));
	}
	cvSaveImage(sFileName, pFaceImg);
	//*/

	/*
	double dVVariant, dSVariant, dHVariant;
	double dSize = 0;
	double dContrast, dVariant;
	FaceJudge(pFaceImg, dJdgFactor, iSFactor, dSize, dSVariant, dVVariant, dHVariant, dContrast, dVariant);
	sprintf_s(sFileName, 400, "../Debug/Face%d____%d__%5f__%5f____%5f__%5f__%5f.jpg", iFaceNum++,  iSFactor, (float)dVariant, (float)dContrast, (float)dHVariant, (float)dVVariant, (float)dSVariant);
	//if (iSFactor >= m_iSFactorValv)
		cvSaveImage(sFileName, pFaceImg);
	//*/

		{
			bool bRealFace;
			int iSFactor = 0;
			double dVVariant, dSVariant, dHVariant;
			double dJdgFactor = 0;
			
			double dSize = 0;
			double dContrast = 0, dVariant = 0;
			double dConf;
			double dEnhConf;
			
				bRealFace = true;
				dConf = 0.0f;
				dEnhConf = 0.0f;

				//* //检测并保存人脸
				IplImage *SubFace = GetSubImage(pImage, rSub);

				//*
				dConf = 0.0f;
				rateFace( SubFace, dConf );

				bool bNeedEnhance = NeedEnhance(SubFace);

				dEnhConf = 0.0f;
				if (bNeedEnhance)
				{
					IplImage *pEnhSubFace = GetSubImage(pImage, rSub);
					ColorImageEnhance(pEnhSubFace);
					rateFace(pEnhSubFace, dEnhConf);
					cvReleaseImage(&pEnhSubFace);
				}

				if (dConf>0.6)
					bRealFace = true;
				else
					bRealFace = false;

				if (bRealFace==false)
				{
					if (bNeedEnhance)
					{
						if (dConf<=0.2 && dEnhConf<=0.2)
						{
							bRealFace = false;
						}
						else
						{
							if (dEnhConf>0.5)
							{
								bRealFace=true;
							}
							else
							{
								/////////////////////////////////////
								FaceJudge(SubFace, dJdgFactor, iSFactor, dSize, dSVariant, dVVariant, dHVariant, dContrast, dVariant);
								if ((dVariant<8) || (dVariant>25) ||(dContrast>40)||(dContrast<6)||(dHVariant<20)||((iSFactor<23) && (dVVariant>100))||((iSFactor<23) && (dHVariant<50)))
								{
									bRealFace = false;
								}
								else
								{
									if ((iSFactor<23)&&(dVVariant<15)&&(dEnhConf<0.4))
									{
										bRealFace = false;
									}
									else
										bRealFace = true;
								}
							}
						}
					}
					else
					{
						if (dConf<0.4)
						{
							bRealFace = false;
						}
						else
						{
							////////////////////////////////////////////
							FaceJudge(SubFace, dJdgFactor, iSFactor, dSize, dSVariant, dVVariant, dHVariant, dContrast, dVariant);
							if ((iSFactor>15)&&(dVariant>7) && (dVariant<25) && (dContrast<30)&&(dContrast>4.5)&&(dHVariant>150))
							{
								bRealFace = true;
							}
							else
							{
								if ((iSFactor<=15)&&(dVariant>7) && (dVariant<30) && (dContrast<30) && (dHVariant>550))
									bRealFace = true;
								else
									bRealFace = false;
							}
						}
					}
				}

				if( bRealFace==false )
				{
					sprintf_s(sFileName, 400, "../Debug/False/_Img%d__%5f_%5f____%d__%5f__%5f____%5f__%5f__%5f.jpg",  iFaceNum++, (float)(dConf), (float)(dEnhConf), iSFactor, (float)dVariant, (float)dContrast, (float)dHVariant, (float)dVVariant, (float)dSVariant);
				}
				else
				{
					sprintf_s(sFileName, 400, "../Debug/True/_Img%d__%5f_%5f____%d__%5f__%5f____%5f__%5f__%5f.jpg",  iFaceNum++,  (float)(dConf), (float)(dEnhConf), iSFactor, (float)dVariant, (float)dContrast, (float)dHVariant, (float)dVVariant, (float)dSVariant);
				}
				cvSaveImage(sFileName, SubFace);
				//*/


#ifdef _TEST_JUDGE_
				FaceJudge(SubFace, dJdgFactor, iSFactor, dSize, dSVariant, dVVariant, dHVariant, dContrast, dVariant);


				if (/*(iSFactor < m_iSFactorValv) ||*/ (dVariant<7) || (dVariant>25) ||(dContrast>40)||(dContrast<4.5))
				{
					bRealFace = false;

					//*
					dConf = 0.0f;
					rateFace( SubFace, dConf );

					bool bNeedEnhance = true;

					dEnhConf = 0.0f;
					if (bNeedEnhance)
					{
						IplImage *pEnhSubFace = GetSubImage(pImage, rSub);
						ColorImageEnhance(pEnhSubFace);
						rateFace(pEnhSubFace, dEnhConf);
						cvReleaseImage(&pEnhSubFace);
					}

					if (dConf<0.5 && dEnhConf<0.5)
					{
						bRealFace = false;
					}
					else
					{
						bRealFace = true;
					}
					//*/
				}
				else
				{
					bRealFace = true;

					//*
					dConf = 0.0f;
					rateFace( SubFace, dConf );

					bool bNeedEnhance = true;

					dEnhConf = 0.0f;
					if (bNeedEnhance)
					{
						IplImage *pEnhSubFace = GetSubImage(pImage, rSub);
						ColorImageEnhance(pEnhSubFace);
						rateFace(pEnhSubFace, dEnhConf);
						cvReleaseImage(&pEnhSubFace);
					}
					//*/
				}

				if (dConf>0.60 || dEnhConf>0.60)
				{
					bRealFace = true;
				} 
				else
				{
					/*
					//直方图判据
					if (bRealFace == true)
					{
						if (dHVariant>200)
						{
							bRealFace = true;
						} 
						else if (dHVariant<80)
						{
							bRealFace = false;
						}
						else
						{
							if (dVVariant < 350)
							{
								bRealFace = true;
							} 
							else
							{
								bRealFace = false;
							}
						}						
					}
					//*/
				
			}

				if( bRealFace==false )
				{
					sprintf_s(sFileName, 400, "../Debug/False/_Img%d__%5f_%5f____%d__%5f__%5f____%5f__%5f__%5f.jpg",  iFaceNum++, (float)(dConf), (float)(dEnhConf), iSFactor, (float)dVariant, (float)dContrast, (float)dHVariant, (float)dVVariant, (float)dSVariant);
				}
				else
				{
					sprintf_s(sFileName, 400, "../Debug/True/_Img%d__%5f_%5f____%d__%5f__%5f____%5f__%5f__%5f.jpg",  iFaceNum++,  (float)(dConf), (float)(dEnhConf), iSFactor, (float)dVariant, (float)dContrast, (float)dHVariant, (float)dVVariant, (float)dSVariant);
				}
				cvSaveImage(sFileName, SubFace);
#endif
				cvReleaseImage(&SubFace);
		}

	cvReleaseImage(&pImage);
	cvReleaseImage(&pFaceImg);
}

void CFaceSelect::BRGtoHSV(IplImage* pBGRImg, IplImage*& pHImg, IplImage*& pSImg, IplImage*& pVImg)
{
	pHImg = cvCreateImage(cvGetSize(pBGRImg), IPL_DEPTH_32F, 1);
	pSImg = cvCreateImage(cvGetSize(pBGRImg), IPL_DEPTH_32F, 1);
	pVImg = cvCreateImage(cvGetSize(pBGRImg), IPL_DEPTH_32F, 1);

	float *hdata, *sdata, *vdata;
	uchar *bgrdata;
	int istep, i, j;
	CvSize size;
	uchar b, g, r;
	double tmpa, tmpb, tmpc;
	double pi = 3.1415926;

	cvGetRawData(pBGRImg, (uchar**)&bgrdata);
	cvGetRawData(pHImg, (uchar**)&hdata, &istep, &size);
	cvGetRawData(pSImg, (uchar**)&sdata, &istep, &size);
	cvGetRawData(pVImg, (uchar**)&vdata, &istep, &size);

	istep /= sizeof(float);

	for (j=0; j<size.height; j++)
	{
		for (i=0; i<size.width; i++)
		{
			b = bgrdata[i*3];
			g = bgrdata[i*3+1];
			r = bgrdata[i*3+2];

			tmpa = 0.299 * (double)r + 0.587 * (double)g + 0.114 * (double)b;
			tmpb = 0.492 * ((double)b-tmpa);
			tmpc = 0.877 * ((double)r-tmpa);

			vdata[i] = (short)cvRound(tmpa/255*65535-32768);
			hdata[i] = (short)cvRound(cvSqrt( tmpb*tmpb + tmpc*tmpc )/361*65535-32768);
			sdata[i] = (short)cvRound(atan( tmpc/tmpb )/pi*65500);// cvFastArctan(tmpc, tmpb);
		}
		hdata += istep;
		sdata += istep;
		vdata += istep;
		bgrdata += pBGRImg->widthStep;
	}
}

void CFaceSelect::FaceTest(const char *cFileName)
{
	IplImage *pImage = cvLoadImage(cFileName, CV_LOAD_IMAGE_UNCHANGED);

	static int iFaceNum = 0;
	int iSFactor = 0;
	double dJdgFactor = 0;
	char sFileName[400];
	CvRect rSub;
	rSub.height = pImage->height/2;
	rSub.width = pImage->width;
	rSub.x = rSub.y = 0;
	IplImage *pFaceImg = GetSubImage(pImage, rSub);
	char cTmpName[8];
	strncpy_s(cTmpName, 8, cFileName+31, _TRUNCATE);

	{
		IplImage *pHImg = cvCreateImage(cvGetSize(pFaceImg), IPL_DEPTH_32F, 1);
		IplImage *pSImg = cvCreateImage(cvGetSize(pFaceImg), IPL_DEPTH_32F, 1);
		IplImage *pVImg = cvCreateImage(cvGetSize(pFaceImg), IPL_DEPTH_32F, 1);
		IplImage *pIImg = cvCreateImage(cvGetSize(pFaceImg), IPL_DEPTH_32F, 1);
		IplImage *pQImg = cvCreateImage(cvGetSize(pFaceImg), IPL_DEPTH_32F, 1);

		IplImage *pMaskImg = cvCreateImage(cvGetSize(pFaceImg), IPL_DEPTH_8U, 1);

		float *hdata, *sdata, *vdata, *idata, *qdata;
		uchar *bgrdata, *mdata;
		int istep, i, j;
		CvSize size;
		uchar b, g, r;
		float tmpa, tmpb, tmpc;
		float pi = 3.1416f;

		cvGetRawData(pFaceImg, (uchar**)&bgrdata);
		cvGetRawData(pMaskImg, (uchar**)&mdata);
		cvGetRawData(pHImg, (uchar**)&hdata, &istep, &size);
		cvGetRawData(pSImg, (uchar**)&sdata, &istep, &size);
		cvGetRawData(pVImg, (uchar**)&vdata, &istep, &size);
		cvGetRawData(pIImg, (uchar**)&idata, &istep, &size);
		cvGetRawData(pQImg, (uchar**)&qdata, &istep, &size);

		istep /= sizeof(float);

		for (j=0; j<size.height; j++)
		{
			for (i=0; i<size.width; i++)
			{
				b = bgrdata[i*3];
				g = bgrdata[i*3+1];
				r = bgrdata[i*3+2];

				tmpa = 0.299 * (float)r + 0.587 * (float)g + 0.114 * (float)b;
				idata[i] = 0.596 * (float)r - 0.275 * (float)g - 0.321 * (float)b;
				qdata[i] = 0.212 * (float)r - 0.523 * (float)g + 0.311 * (float)b;
				tmpb = 0.492 * ((float)b-tmpa);
				tmpc = 0.877 * ((float)r-tmpa);

				vdata[i] = tmpa;
				hdata[i] = cvSqrt( tmpb*tmpb + tmpc*tmpc );
				sdata[i] = atan( tmpc/tmpb )/pi*180+180;// cvFastArctan(tmpc, tmpb);

				if ( ((105<=sdata[i]&&sdata[i]<=150)||(285<=sdata[i]&&sdata[i]<=330)) && (30<=idata[i]&&idata[i]<=90)&&(qdata[i]>=-20))
				{
					mdata[i]=255;
				} 
				else
				{
					mdata[i]=0;
				}
			}
			hdata += istep;
			sdata += istep;
			vdata += istep;
			idata += istep;
			qdata += istep;
			bgrdata += pFaceImg->widthStep;
			mdata += pMaskImg->widthStep;
		}

		sprintf_s(sFileName, 400, "../Debug/_Img%d.jpg",  iFaceNum++);
		cvSaveImage(sFileName, pMaskImg);

		cvReleaseImage(&pHImg);
		cvReleaseImage(&pSImg);
		cvReleaseImage(&pVImg);
		cvReleaseImage(&pIImg);
		cvReleaseImage(&pQImg);
		cvReleaseImage(&pMaskImg);
	}
	
	cvReleaseImage(&pImage);
	cvReleaseImage(&pFaceImg);
}

void CFaceSelect::CompareTest(const char *cFileName)
{
	IplImage *pLabplane;
	IplImage *L_plane;
	IplImage *a_plane;
	IplImage *b_plane;
	IplImage *pImage; 
	

	static int iFaceNum = 0;
	int iSFactor = 0;
	double dJdgFactor = 0;
	char sFileName[200];

	//char cTmpName[8];
	//strncpy_s(cTmpName, 8, cFileName+31, _TRUNCATE);

	/*
	double dVVariant, dSVariant, dHVariant;
	double dSize = 0;
	double dContrast, dVariant;
	FaceJudge(pFaceImg, dJdgFactor, iSFactor, dSize, dSVariant, dVVariant, dHVariant, dContrast, dVariant);
	sprintf_s(sFileName, 200, "../Debug/Face%d__%s__%5f__%5f__%5f.jpg", iFaceNum++, cTmpName, (float)dHVariant, (float)dVVariant, (float)dSVariant);
	cvSaveImage(sFileName, pFaceImg);
	//*/

	/*
	float fChnlRang[2] = {0, 255}; //直方图范围
		int iDimSize = 256;
		float *pfChnlRang =  fChnlRang;
		double dMaxPix;
	
		sprintf_s(sFileName, 200, "%s_15.jpg", cFileName);
		pImage = cvLoadImage(sFileName, CV_LOAD_IMAGE_UNCHANGED);
		pLabplane = cvCreateImage(cvGetSize(pImage), pImage->depth, pImage->nChannels);
		L_plane = cvCreateImage(cvGetSize(pImage), 8, 1);
		a_plane = cvCreateImage(cvGetSize(pImage), 8, 1);
		b_plane = cvCreateImage(cvGetSize(pImage), 8, 1);
	
		dMaxPix = pImage->height*pImage->width;
		cvCvtColor(pImage, pLabplane, CV_BGR2Lab);
		cvCvtPixToPlane(pLabplane, L_plane, a_plane, b_plane, 0);
		CvHistogram *pHist_a1 = cvCreateHist(1, &iDimSize, CV_HIST_ARRAY, (float**)(&pfChnlRang), 1);
		CvHistogram *pHist_b1 = cvCreateHist(1, &iDimSize, CV_HIST_ARRAY, (float**)(&pfChnlRang), 1);
		cvCalcHist(&a_plane, pHist_a1, 0, 0);
		cvCalcHist(&b_plane, pHist_b1, 0, 0);
		//DrawHistImage(pHist_a1, iDimSize-1, dMaxPix);
		//DrawHistImage_H(pHist_b1, iDimSize-1, dMaxPix);
		cvNormalizeHist(pHist_a1, 1);
		cvNormalizeHist(pHist_b1, 1);
		cvReleaseImage(&L_plane);
		cvReleaseImage(&a_plane);
		cvReleaseImage(&b_plane);
		cvReleaseImage(&pLabplane);
		cvReleaseImage(&pImage);
	
	
	
		sprintf_s(sFileName, 200, "%s_16.jpg", cFileName);//, iFaceNum++
		pImage = cvLoadImage(sFileName, CV_LOAD_IMAGE_UNCHANGED);
		pLabplane = cvCreateImage(cvGetSize(pImage), pImage->depth, pImage->nChannels);
		L_plane = cvCreateImage(cvGetSize(pImage), 8, 1);
		a_plane = cvCreateImage(cvGetSize(pImage), 8, 1);
		b_plane = cvCreateImage(cvGetSize(pImage), 8, 1);
	
		dMaxPix = pImage->height*pImage->width;
		cvCvtColor(pImage, pLabplane, CV_BGR2Lab);
		cvCvtPixToPlane(pLabplane, L_plane, a_plane, b_plane, 0);
		CvHistogram *pHist_a2 = cvCreateHist(1, &iDimSize, CV_HIST_ARRAY, (float**)(&pfChnlRang), 1);
		CvHistogram *pHist_b2 = cvCreateHist(1, &iDimSize, CV_HIST_ARRAY, (float**)(&pfChnlRang), 1);
		cvCalcHist(&a_plane, pHist_a2, 0, 0);
		cvCalcHist(&b_plane, pHist_b2, 0, 0);
		//DrawHistImage(pHist_a1, iDimSize-1, dMaxPix);
		//DrawHistImage_H(pHist_b1, iDimSize-1, dMaxPix);
		cvNormalizeHist(pHist_a2, 1);
		cvNormalizeHist(pHist_b2, 1);
		cvReleaseImage(&L_plane);
		cvReleaseImage(&a_plane);
		cvReleaseImage(&b_plane);
		cvReleaseImage(&pLabplane);
		cvReleaseImage(&pImage);
	
		double da = cvCompareHist(pHist_a1, pHist_a2, CV_COMP_CORREL);
		double db = cvCompareHist(pHist_b1, pHist_b2, CV_COMP_CORREL);
	
		cvReleaseHist(&pHist_a1);
		cvReleaseHist(&pHist_b1);
		cvReleaseHist(&pHist_a2);
		cvReleaseHist(&pHist_b2);*/

	float fChnlRang[2] = {-32768, 32767}; //直方图范围
	int iDimSize = 65536;
	float *pfChnlRang =  fChnlRang;
	double dMaxPix;

	sprintf_s(sFileName, 200, "%s_20.jpg", cFileName);
	IplImage *pOriImage = cvLoadImage(sFileName, CV_LOAD_IMAGE_UNCHANGED);
	CvRect roi;
	CvSize orisize = cvGetSize(pOriImage);
	roi.height = orisize.height;
	roi.width = orisize.width*2/3;
	roi.y = 0;
	roi.x = orisize.width/6;
	pImage = GetSubImage(pOriImage, roi);
	cvReleaseImage(&pOriImage);
	
	dMaxPix = pImage->height*pImage->width;
	BRGtoHSV(pImage, a_plane, b_plane, L_plane);
	CvHistogram *pHist_a1 = cvCreateHist(1, &iDimSize, CV_HIST_ARRAY, (float**)(&pfChnlRang), 1);
	CvHistogram *pHist_b1 = cvCreateHist(1, &iDimSize, CV_HIST_ARRAY, (float**)(&pfChnlRang), 1);
	
	cvCalcHist(&a_plane, pHist_a1, 0, 0);
	cvCalcHist(&b_plane, pHist_b1, 0, 0);
	//DrawHistImage(pHist_a1, iDimSize-1, dMaxPix);
	//DrawHistImage_H(pHist_b1, iDimSize-1, dMaxPix);
	cvNormalizeHist(pHist_a1, 1);
	cvNormalizeHist(pHist_b1, 1);
	cvReleaseImage(&L_plane);
	cvReleaseImage(&a_plane);
	cvReleaseImage(&b_plane);
	cvReleaseImage(&pImage);



	sprintf_s(sFileName, 200, "%s_21.jpg", cFileName);//, iFaceNum++
	pOriImage = cvLoadImage(sFileName, CV_LOAD_IMAGE_UNCHANGED);
	orisize = cvGetSize(pOriImage);
	roi.height = orisize.height;
	roi.width = orisize.width*2/3;
	roi.y = 0;
	roi.x = orisize.width/6;
	pImage = GetSubImage(pOriImage, roi);
	cvReleaseImage(&pOriImage);

	dMaxPix = pImage->height*pImage->width;
	CvHistogram *pHist_a2 = cvCreateHist(1, &iDimSize, CV_HIST_ARRAY, (float**)(&pfChnlRang), 1);
	CvHistogram *pHist_b2 = cvCreateHist(1, &iDimSize, CV_HIST_ARRAY, (float**)(&pfChnlRang), 1);
	BRGtoHSV(pImage, a_plane, b_plane, L_plane);
	cvCalcHist(&a_plane, pHist_a2, 0, 0);
	cvCalcHist(&b_plane, pHist_b2, 0, 0);
	//DrawHistImage(pHist_a1, iDimSize-1, dMaxPix);
	//DrawHistImage_H(pHist_b1, iDimSize-1, dMaxPix);
	cvNormalizeHist(pHist_a2, 1);
	cvNormalizeHist(pHist_b2, 1);
	cvReleaseImage(&L_plane);
	cvReleaseImage(&a_plane);
	cvReleaseImage(&b_plane);
	cvReleaseImage(&pImage);

	double da = cvCompareHist(pHist_a1, pHist_a2, CV_COMP_CORREL);
	double db = cvCompareHist(pHist_b1, pHist_b2, CV_COMP_CORREL);

	cvReleaseHist(&pHist_a1);
	cvReleaseHist(&pHist_b1);
	cvReleaseHist(&pHist_a2);
	cvReleaseHist(&pHist_b2);
	
}

void CFaceSelect::SkinTest(const char *cFileName)
{
	IplImage *pLabplane;
	IplImage *L_plane;
	IplImage *a_plane;
	IplImage *b_plane;
	IplImage *pImage; 


	static int iFaceNum = 0;
	int iSFactor = 0;
	double dJdgFactor = 0;
	char sFileName[200];

	//char cTmpName[8];
	//strncpy_s(cTmpName, 8, cFileName+31, _TRUNCATE);

	/*
	double dVVariant, dSVariant, dHVariant;
	double dSize = 0;
	double dContrast, dVariant;
	FaceJudge(pFaceImg, dJdgFactor, iSFactor, dSize, dSVariant, dVVariant, dHVariant, dContrast, dVariant);
	sprintf_s(sFileName, 200, "../Debug/Face%d__%s__%5f__%5f__%5f.jpg", iFaceNum++, cTmpName, (float)dHVariant, (float)dVVariant, (float)dSVariant);
	cvSaveImage(sFileName, pFaceImg);
	//*/

	///////////////////////////输出返回的字符串/////////////////////////
	FILE *fp = 0;
	fopen_s(&fp, "../Debug/result.txt","a+");

	float fChnlRang[2] = {0, 255}; //直方图范围
	int iDimSize = 256;
	float *pfChnlRang =  fChnlRang;
	double dMaxPix;

	//sprintf_s(sFileName, 200, "%s_15.jpg", cFileName);
	pImage = cvLoadImage(cFileName, CV_LOAD_IMAGE_UNCHANGED);
	pLabplane = cvCreateImage(cvGetSize(pImage), pImage->depth, pImage->nChannels);
	L_plane = cvCreateImage(cvGetSize(pImage), 8, 1);
	a_plane = cvCreateImage(cvGetSize(pImage), 8, 1);
	b_plane = cvCreateImage(cvGetSize(pImage), 8, 1);

	dMaxPix = pImage->height*pImage->width;
	cvCvtColor(pImage, pLabplane, CV_BGR2YCrCb);
	cvCvtPixToPlane(pLabplane, L_plane, a_plane, b_plane, 0);
	CvHistogram *pHist_a1 = cvCreateHist(1, &iDimSize, CV_HIST_ARRAY, (float**)(&pfChnlRang), 1);
	CvHistogram *pHist_b1 = cvCreateHist(1, &iDimSize, CV_HIST_ARRAY, (float**)(&pfChnlRang), 1);
	cvCalcHist(&a_plane, pHist_a1, 0, 0);
	cvCalcHist(&b_plane, pHist_b1, 0, 0);
	//DrawHistImage(pHist_a1, iDimSize-1, dMaxPix);
	//DrawHistImage_H(pHist_b1, iDimSize-1, dMaxPix);
	float fMaxVala, fMaxValb;
	int iMaxIdxa, iMaxIdxb;
	cvGetMinMaxHistValue( pHist_a1, 0, &fMaxVala, 0, &iMaxIdxa);
	cvGetMinMaxHistValue( pHist_b1, 0, &fMaxValb, 0, &iMaxIdxb);

	sprintf_s(sFileName, 200, "%d____%d\n", iMaxIdxa, iMaxIdxb);//, iFaceNum++
	

	cvReleaseHist(&pHist_a1);
	cvReleaseHist(&pHist_b1);
	cvReleaseImage(&L_plane);
	cvReleaseImage(&a_plane);
	cvReleaseImage(&b_plane);
	cvReleaseImage(&pLabplane);
	cvReleaseImage(&pImage);

	fprintf( fp, sFileName );
	fclose( fp );
	fp = 0;
}

void CFaceSelect::SkinTestShow(const char *cFileName)
{
	IplImage *pLabplane;
	IplImage *L_plane;
	IplImage *a_plane;
	IplImage *b_plane;
	IplImage *pImage; 


	static int iFaceNum = 0;
	int iSFactor = 0;
	double dJdgFactor = 0;
	char sFileName[200];

	//char cTmpName[8];
	//strncpy_s(cTmpName, 8, cFileName+31, _TRUNCATE);

	/*
	double dVVariant, dSVariant, dHVariant;
	double dSize = 0;
	double dContrast, dVariant;
	FaceJudge(pFaceImg, dJdgFactor, iSFactor, dSize, dSVariant, dVVariant, dHVariant, dContrast, dVariant);
	sprintf_s(sFileName, 200, "../Debug/Face%d__%s__%5f__%5f__%5f.jpg", iFaceNum++, cTmpName, (float)dHVariant, (float)dVVariant, (float)dSVariant);
	cvSaveImage(sFileName, pFaceImg);
	//*/

	///////////////////////////输出返回的字符串/////////////////////////
	//FILE *fp = 0;
	//fp = fopen("../Debug/result.txt","a+");

	float fChnlRang[2] = {0, 255}; //直方图范围
	int iDimSize = 256;
	float *pfChnlRang =  fChnlRang;
	double dMaxPix;

	//sprintf_s(sFileName, 200, "%s_15.jpg", cFileName);
	pImage = cvLoadImage(cFileName, CV_LOAD_IMAGE_UNCHANGED);
	pLabplane = cvCreateImage(cvGetSize(pImage), pImage->depth, pImage->nChannels);
	L_plane = cvCreateImage(cvGetSize(pImage), 8, 1);
	a_plane = cvCreateImage(cvGetSize(pImage), 8, 1);
	b_plane = cvCreateImage(cvGetSize(pImage), 8, 1);

	dMaxPix = pImage->height*pImage->width;
	cvCvtColor(pImage, pLabplane, CV_BGR2HSV);
	cvCvtPixToPlane(pLabplane, b_plane, L_plane, a_plane, 0);
	CvHistogram *pHist_a1 = cvCreateHist(1, &iDimSize, CV_HIST_ARRAY, (float**)(&pfChnlRang), 1);
	CvHistogram *pHist_b1 = cvCreateHist(1, &iDimSize, CV_HIST_ARRAY, (float**)(&pfChnlRang), 1);
	cvCalcHist(&a_plane, pHist_a1, 0, 0);
	cvCalcHist(&b_plane, pHist_b1, 0, 0);
	//DrawHistImage(pHist_a1, iDimSize-1, dMaxPix);
	//DrawHistImage_H(pHist_b1, iDimSize-1, dMaxPix);
	CvMat *lut;
	lut = cvCreateMat(1, 256, CV_8UC1);
	for (int i=0; i<42; i++)
		lut->data.ptr[i] = (uchar)0xFF;
	for (int i=42; i<167; i++)
		lut->data.ptr[i] = 0;
	for (int i=167; i<256; i++)
		lut->data.ptr[i] = (uchar)0xFF;

	cvLUT( b_plane, L_plane, lut );

	cvZero(lut);
	for (int i=10; i<250; i++)
		lut->data.ptr[i] = (uchar)0xFF;

	IplImage* bImg = cvCreateImage(cvGetSize(pImage), 8, 1);
	cvLUT(a_plane, bImg, lut);

	cvAnd(L_plane, bImg, L_plane);

	sprintf_s(sFileName, 200, "..\\Debug\\bin_%d.jpg", iFaceNum++);//
	cvSaveImage(sFileName, L_plane);


	cvReleaseMat(&lut);
	cvReleaseImage(&bImg);
	cvReleaseHist(&pHist_a1);
	cvReleaseHist(&pHist_b1);
	cvReleaseImage(&L_plane);
	cvReleaseImage(&a_plane);
	cvReleaseImage(&b_plane);
	cvReleaseImage(&pLabplane);
	cvReleaseImage(&pImage);

	//fprintf( fp, sFileName );
	//fclose( fp );
	//fp = 0;
}

void CFaceSelect::DebugTest(const char *cFileName)
{
	RyanDebug(cFileName);
	//CodeDebugTest_Michael( cFileName );
	//EyeFaceTest(cFileName);
	//CompareTest(cFileName);
	//SkinTest(cFileName);
	//SkinTestShow(cFileName);
	//EnhanceTest(cFileName);
	//FaceTest(cFileName);
}

void CFaceSelect::SkinFilter(IplImage *pImage, IplImage *pGrayImage, int iMaxIdx, bool bRmvBkg, IplImage *pBiImg)
{
	IplImage *pLabplane;
	IplImage *L_plane;
	IplImage *a_plane;
	IplImage *b_plane;

	float fChnlRang[2] = {0, 255}; //直方图范围
	int iDimSize = 256;
	float *pfChnlRang =  fChnlRang;

	pLabplane = cvCreateImage(cvGetSize(pImage), pImage->depth, pImage->nChannels);
	L_plane = cvCreateImage(cvGetSize(pImage), 8, 1);
	a_plane = cvCreateImage(cvGetSize(pImage), 8, 1);
	b_plane = cvCreateImage(cvGetSize(pImage), 8, 1);

	cvCvtColor(pImage, pLabplane, CV_BGR2HSV);
	cvCvtPixToPlane(pLabplane, b_plane, L_plane, a_plane, 0);
	CvHistogram *pHist_a1 = cvCreateHist(1, &iDimSize, CV_HIST_ARRAY, (float**)(&pfChnlRang), 1);
	CvHistogram *pHist_b1 = cvCreateHist(1, &iDimSize, CV_HIST_ARRAY, (float**)(&pfChnlRang), 1);
	cvCalcHist(&a_plane, pHist_a1, 0, 0);
	cvCalcHist(&b_plane, pHist_b1, 0, 0);
	//DrawHistImage(pHist_a1, iDimSize-1, dMaxPix);
	//DrawHistImage_H(pHist_b1, iDimSize-1, dMaxPix);

	CvMat *lut;
	lut = cvCreateMat(1, 256, CV_8UC1);
	for (int i=0; i<42; i++)
		lut->data.ptr[i] = (uchar)0xFF;
	for (int i=42; i<167; i++)
		lut->data.ptr[i] = 0;
	for (int i=167; i<256; i++)
		lut->data.ptr[i] = (uchar)0xFF;

	cvLUT( b_plane, L_plane, lut );

	cvZero(lut);
	for (int i=10; i<250; i++)
		lut->data.ptr[i] = (uchar)0xFF;

	cvLUT(a_plane, pBiImg, lut);
	cvAnd(L_plane, pBiImg, pBiImg);

	if (bRmvBkg)
	{
		cvZero(lut);
		lut->data.ptr[iMaxIdx] = (uchar)0xFF;
		cvNot(lut, lut);
		cvLUT( pGrayImage, L_plane, lut );
		cvAnd(L_plane, pBiImg, pBiImg);
	}

	cvReleaseMat(&lut);
	cvReleaseHist(&pHist_a1);
	cvReleaseHist(&pHist_b1);
	cvReleaseImage(&L_plane);
	cvReleaseImage(&a_plane);
	cvReleaseImage(&b_plane);
	cvReleaseImage(&pLabplane);
}

char* CFaceSelect::SelectBestImage( int outputMode )
{
	ReleaseTargets( m_targets, m_nTotalValidImages );
	m_resString[0] = 0;

	IplImage **ppImage;
	CvSeq **ppFace;
	int* piRealFaceNum = 0;
	int iFaceNum = 0;
	bool bSizeInited = 0;
	int iSFactor = 0;
	double dVVariant, dSVariant, dHVariant;
	double dJdgFactor = 0;
	double dSize = 0;
	double dSizeFactor = 0, dContrast = 0, dVariant = 0;
	double dConf;
	double dEnhConf;
	int iBasicSize = m_iFaceSize;
	double dminCost = 1000;
	int iminImgIdx = 0;
	int iminFcIdx = 0;
	double dCombin;
	//////////////////////////////////////////////////////////////////////////
	MultiObjectInitialize();
	CvMemStorage *pFrmRectStorage = cvCreateMemStorage(0);
	CvMemStorage *pFrmJudgeStorage = cvCreateMemStorage(0);
	CvSeq *sqSglFrmRect = cvCreateSeq(0, sizeof(CvSeq), sizeof(CvRect), pFrmRectStorage);
	CvSeq *sqSglFrmJudge = cvCreateSeq(0, sizeof(CvSeq), sizeof(double), pFrmJudgeStorage);
	//////////////////////////////////////////////////////////////////////////

#ifdef BKGANA
	RemoveNoiseByBkgAna( );//Michael Add Rough Background Remove, 通过背景分析，去除背景中的噪声框
#endif

	for (int i=0; i<(m_cvImageSeq?m_cvImageSeq->total:0); i++)
	{
		ppImage = CV_GET_SEQ_ELEM(IplImage*, m_cvImageSeq, i);
		ppFace = CV_GET_SEQ_ELEM(CvSeq*, m_cvSeqFaceSeq, i);
		piRealFaceNum = CV_GET_SEQ_ELEM(int, m_cvRealFaceSeq, i);

		CvSeq *pFace = *ppFace;

		//////////////////////////////////////////////////////////////////////////
		//清理一帧图像内的脸区链表
		cvClearSeq(sqSglFrmRect);
		cvClearSeq(sqSglFrmJudge);
		//////////////////////////////////////////////////////////////////////////

		for (int j=0; j<(pFace?(*piRealFaceNum):0); j++)
		{
			bool bRealFace;
			CvRect* FaceRect = (CvRect*)cvGetSeqElem( pFace, j );

			if ((FaceRect->height > iBasicSize*m_dFaceChangeRatio)||(FaceRect->width > iBasicSize*m_dFaceChangeRatio))
			{
				bRealFace = false;
			}
			else
			{
				bRealFace = true;
				dConf = 0.0f;
				dEnhConf = 0.0f;

				//* //检测并保存人脸
				IplImage *SubFace = GetSubImage(*ppImage, *FaceRect);
				FaceJudge(SubFace, dJdgFactor, iSFactor, dSize, dSVariant, dVVariant, dHVariant, dContrast, dVariant);

				dConf = 0.0f;
				rateFace( SubFace, dConf );
				if (dConf == 0.0)
				{
					bRealFace = false;
				}

				cvReleaseImage(&SubFace);
			}

			if (bRealFace == false)
			{
				continue;
			}
			else
			{
				static double stcdStdSize = dSize;
				dSizeFactor = stcdStdSize/dSize*10;

				if (iSFactor<31)
				{
					dJdgFactor = dJdgFactor / (dJdgFactor<10 ? 2.5:3.5);
				}
				dJdgFactor = dJdgFactor<1?dJdgFactor*3:dJdgFactor;
				dCombin = (dJdgFactor+dVariant+dContrast)/3;
			}

			double dcost = dCombin*((double)(10-m_iSizeWeight))+dSizeFactor*((double)m_iSizeWeight);

			//////////////////////////////////////////////////////////////////////////
			//去除覆盖框
			MergeResultRectSeq(sqSglFrmRect, sqSglFrmJudge, FaceRect, &dcost);
			//////////////////////////////////////////////////////////////////////////
		}

		for (int j=(pFace?(*piRealFaceNum):0); j<(pFace?pFace->total:0); j++)
		{
			bool bRealFace;
			CvRect* FaceRect = (CvRect*)cvGetSeqElem( pFace, j );

			/*
			if ((FaceRect->height > iBasicSize*m_dFaceChangeRatio)||(FaceRect->width > iBasicSize*m_dFaceChangeRatio))
			{
				bRealFace = false;
			}
			else
			{
				bRealFace = true;
				dConf = 0.0f;
				dEnhConf = 0.0f;

				//* //检测并保存人脸
				IplImage *SubFace = GetSubImage(*ppImage, *FaceRect);
				FaceJudge(SubFace, dJdgFactor, iSFactor, dSize, dSVariant, dVVariant, dHVariant, dContrast, dVariant);

				if ((iSFactor < m_iSFactorValv) || (dVariant<7) || (dVariant>20) ||(dContrast>30))
				{
					bRealFace = false;

					//*
					dConf = 0.0f;
					rateFace( SubFace, dConf );

					bool bNeedEnhance = NeedEnhance(SubFace);

					dEnhConf = 0.0f;
					if (bNeedEnhance)
					{
						IplImage *pEnhSubFace = GetSubImage(*ppImage, *FaceRect);
						ColorImageEnhance(pEnhSubFace);
						rateFace(pEnhSubFace, dEnhConf);
						cvReleaseImage(&pEnhSubFace);
					}

					if (dConf<0.5 && dEnhConf<0.5)
					{
						bRealFace = false;
					}
					else
					{
						bRealFace = true;
					}
					//*//*
				}
				else
				{
					bRealFace = true;
				}

				if (dConf>0.75 || dEnhConf>0.75)
				{
					bRealFace = true;
				} 
				else
				{
					//直方图判据
					if (bRealFace == true)
					{
						if (dContrast<10 && dContrast>5)
						{
							if (dHVariant>200)
							{
								bRealFace = true;
							} 
							else if (dHVariant<80)
							{
								bRealFace = false;
							}
							else
							{
								if (dVVariant < 350)
								{
									bRealFace = true;
								} 
								else
								{
									bRealFace = false;
								}
							}
						}
						else
						{
							if (dHVariant>400)
							{
								bRealFace = true;
							} 
							else if (dHVariant<150)
							{
								bRealFace = false;
							}
							else if (dHVariant < 250)
							{
								if (dVVariant < 90)
								{
									bRealFace = true;
								} 
								else
								{
									bRealFace = false;
								}
							}
							else
							{
								if (dVVariant < 150)
								{
									bRealFace = true;
								} 
								else
								{
									bRealFace = false;
								}
							}
						}
					}
				}

				cvReleaseImage(&SubFace);
			}
			//*/

			if (m_iLightCondition==0)
			{
				//判断脸框大小
				if ((FaceRect->height > iBasicSize*m_dFaceChangeRatio)||(FaceRect->width > iBasicSize*m_dFaceChangeRatio))
				{
					bRealFace = false;
				}
				else
				{
					bRealFace = true;
					dConf = 0.0f;
					dEnhConf = 0.0f;

					//* //检测并保存人脸
					IplImage *SubFace = GetSubImage(*ppImage, *FaceRect);
					FaceJudge(SubFace, dJdgFactor, iSFactor, dSize, dSVariant, dVVariant, dHVariant, dContrast, dVariant);

					if ((iSFactor < m_iSFactorValv) || (dVariant<7) || (dVariant>20) ||(dContrast>30))
					{
						bRealFace = false;

						//*
						dConf = 0.0f;
						rateFace( SubFace, dConf );

						bool bNeedEnhance = NeedEnhance(SubFace);

						dEnhConf = 0.0f;
						if (bNeedEnhance)
						{
							IplImage *pEnhSubFace = GetSubImage(*ppImage, *FaceRect);
							ColorImageEnhance(pEnhSubFace);
							rateFace(pEnhSubFace, dEnhConf);
							cvReleaseImage(&pEnhSubFace);
						}

						if (dConf<0.5 && dEnhConf<0.5)
						{
							bRealFace = false;
						}
						else
						{
							bRealFace = true;
						}
						//*/
					}
					else
					{
						bRealFace = true;

						//*
						dConf = 0.0f;
						rateFace( SubFace, dConf );

						bool bNeedEnhance = NeedEnhance(SubFace);

						dEnhConf = 0.0f;
						if (bNeedEnhance)
						{
							IplImage *pEnhSubFace = GetSubImage(*ppImage, *FaceRect);
							ColorImageEnhance(pEnhSubFace);
							rateFace(pEnhSubFace, dEnhConf);
							cvReleaseImage(&pEnhSubFace);
						}
						//*/
					}

					if (dConf>0.80 || dEnhConf>0.80)
					{
						bRealFace = true;
					} 
					else
					{					
						//直方图判据
						if (bRealFace == true)
						{						
							if (dHVariant>400)
							{
								bRealFace = true;
							} 
							else if (dHVariant<150)
							{
								bRealFace = false;
							}
							else if (dHVariant < 250)
							{
								if (dVVariant < 90)
								{
									bRealFace = true;
								} 
								else
								{
									bRealFace = false;
								}
							}
							else
							{
								if (dVVariant < 150)
								{
									bRealFace = true;
								} 
								else
								{
									bRealFace = false;
								}
							}
						}					
					}
					cvReleaseImage(&SubFace);
				}
			}
			if (m_iLightCondition==1)
			{
				//判断脸框大小
				if ((FaceRect->height > iBasicSize*m_dFaceChangeRatio)||(FaceRect->width > iBasicSize*m_dFaceChangeRatio))
				{
					bRealFace = false;
				}
				else
				{
					bRealFace = true;
					dConf = 0.0f;
					dEnhConf = 0.0f;

					//* //检测并保存人脸
					IplImage *SubFace = GetSubImage(*ppImage, *FaceRect);
					FaceJudge(SubFace, dJdgFactor, iSFactor, dSize, dSVariant, dVVariant, dHVariant, dContrast, dVariant);

					if ((iSFactor < m_iSFactorValv) || (dVariant<7) || (dVariant>20) ||(dContrast>30)||(dContrast<5))
					{
						bRealFace = false;

						//*
						dConf = 0.0f;
						rateFace( SubFace, dConf );

						bool bNeedEnhance = NeedEnhance(SubFace);

						dEnhConf = 0.0f;
						if (bNeedEnhance)
						{
							IplImage *pEnhSubFace = GetSubImage(*ppImage, *FaceRect);
							ColorImageEnhance(pEnhSubFace);
							rateFace(pEnhSubFace, dEnhConf);
							cvReleaseImage(&pEnhSubFace);
						}

						if (dConf<0.5 && dEnhConf<0.5)
						{
							bRealFace = false;
						}
						else
						{
							bRealFace = true;
						}
						//*/
					}
					else
					{
						bRealFace = true;

						//*
						dConf = 0.0f;
						rateFace( SubFace, dConf );

						bool bNeedEnhance = NeedEnhance(SubFace);

						dEnhConf = 0.0f;
						if (bNeedEnhance)
						{
							IplImage *pEnhSubFace = GetSubImage(*ppImage, *FaceRect);
							ColorImageEnhance(pEnhSubFace);
							rateFace(pEnhSubFace, dEnhConf);
							cvReleaseImage(&pEnhSubFace);
						}
						//*/
					}

					if (dConf>0.60 || dEnhConf>0.60)
					{
						bRealFace = true;
					} 
					else
					{
						//*
						//直方图判据
						if (bRealFace == true)
						{
							if (dHVariant>200)
							{
								bRealFace = true;
							} 
							else if (dHVariant<80)
							{
								bRealFace = false;
							}
							else
							{
								if (dVVariant < 350)
								{
									bRealFace = true;
								} 
								else
								{
									bRealFace = false;
								}
							}						
						}
						//*/
					}
					cvReleaseImage(&SubFace);
				}
			}
			if (m_iLightCondition==2)
			{
				//判断脸框大小
				if ((FaceRect->height > iBasicSize*m_dFaceChangeRatio)||(FaceRect->width > iBasicSize*m_dFaceChangeRatio))
				{
					bRealFace = false;
				}
				else
				{
					bRealFace = true;
					dConf = 0.0f;
					dEnhConf = 0.0f;

					//* //检测并保存人脸
					IplImage *SubFace = GetSubImage(*ppImage, *FaceRect);
					
					//*
					dConf = 0.0f;
					rateFace( SubFace, dConf );

					bool bNeedEnhance = NeedEnhance(SubFace);

					dEnhConf = 0.0f;
					if (bNeedEnhance)
					{
						IplImage *pEnhSubFace = GetSubImage(*ppImage, *FaceRect);
						ColorImageEnhance(pEnhSubFace);
						rateFace(pEnhSubFace, dEnhConf);
						cvReleaseImage(&pEnhSubFace);
					}

					if (dConf>0.6)
						bRealFace = true;
					else
						bRealFace = false;

					if (bRealFace==false)
					{
						if (bNeedEnhance)
						{
							if (dConf<=0.2 && dEnhConf<=0.2)
							{
								bRealFace = false;
							}
							else
							{
								if (dEnhConf>0.5)
								{
									bRealFace=true;
								}
								else
								{
									/////////////////////////////////////
									FaceJudge(SubFace, dJdgFactor, iSFactor, dSize, dSVariant, dVVariant, dHVariant, dContrast, dVariant);
									if ((dVariant<8) || (dVariant>25) ||(dContrast>40)||(dContrast<6)||(dHVariant<20)||((iSFactor<23) && (dVVariant>100))||((iSFactor<23) && (dHVariant<50)))
									{
										bRealFace = false;
									}
									else
									{
										if ((iSFactor<23)&&(dVVariant<15)&&(dEnhConf<0.4))
										{
											bRealFace = false;
										}
										else
											bRealFace = true;
									}
								}
							}
						}
						else
						{
							if (dConf<0.4)
							{
								bRealFace = false;
							}
							else
							{
								////////////////////////////////////////////
								FaceJudge(SubFace, dJdgFactor, iSFactor, dSize, dSVariant, dVVariant, dHVariant, dContrast, dVariant);
								if ((iSFactor>15)&&(dVariant>7) && (dVariant<25) && (dContrast<30)&&(dContrast>4.5)&&(dHVariant>150))
								{
									bRealFace = true;
								}
								else
								{
									if ((iSFactor<=15)&&(dVariant>7) && (dVariant<30) && (dContrast<30) && (dHVariant>550))
										bRealFace = true;
									else
										bRealFace = false;
								}
							}
						}
					}
					cvReleaseImage(&SubFace);
				}
			}

			//////////////////////////////////////////////////////////////////////////
			//test
			//bRealFace = false;

			if (bRealFace == false)
			{
				continue;
			}
			else
			{
				static double stcdStdSize = dSize;
				dSizeFactor = stcdStdSize/dSize*10;

				if (iSFactor<31)
				{
					dJdgFactor = dJdgFactor / (dJdgFactor<10 ? 2.5:3.5);
				}
				dJdgFactor = dJdgFactor<1?dJdgFactor*3:dJdgFactor;
				dCombin = (dJdgFactor+dVariant+dContrast)/3;
			}

			double dcost = dCombin*((double)(10-m_iSizeWeight))+dSizeFactor*((double)m_iSizeWeight);

			//////////////////////////////////////////////////////////////////////////
			//去除覆盖框
			MergeResultRectSeq(sqSglFrmRect, sqSglFrmJudge, FaceRect, &dcost);
			//////////////////////////////////////////////////////////////////////////

/*
			//测试嵌套框
			//////////////////////////////////////////////////////////////////////////
			IplImage *pCloneImg = cvCreateImage(cvGetSize(*ppImage), 8, 3);
			pCloneImg = cvCloneImage(*ppImage);
			CvScalar color = {0,0,255,0};
			for (int i=0; i<sqSglFrmRect->total; i++)
			{
				color.val[0] = ((int)color.val[0]+255)%510;
				CvRect *pRectD = (CvRect*)cvGetSeqElem(sqSglFrmRect, i);
				cvRectangle(pCloneImg,cvPoint(pRectD->x,pRectD->y),cvPoint( pRectD->x+pRectD->width, pRectD->y+pRectD->height), color, 1, 8, 0 );
			}
			cvSaveImage("test.bmp", pCloneImg);
			cvReleaseImage(&pCloneImg);
			//////////////////////////////////////////////////////////////////////////
//*/
		}

		//////////////////////////////////////////////////////////////////////////
		//判断多人最优
		MultiObjectJudge(sqSglFrmRect, sqSglFrmJudge, i);
		//////////////////////////////////////////////////////////////////////////
	}

	int nTotalImages = (m_cvImageSeq?m_cvImageSeq->total:0);
	int* facesCntArray = 0;
	int nTotalValidImages = 0;
	if( nTotalImages > 0 )
	{
		facesCntArray = new int[nTotalImages];
		memset( facesCntArray, 0, sizeof(int) * nTotalImages );
	}

	for (int i=0; i<(m_sqDiffFaceRect?m_sqDiffFaceRect->total:0); i++)
	{
		int *piImgNo = (int*)cvGetSeqElem(m_sqImgNo, i);
		if( outputMode == 1 )
		{
			facesCntArray[*piImgNo]++;
			if( facesCntArray[*piImgNo] == 1 )
			{
				nTotalValidImages++;
			}
		}
		else if( outputMode == 0 )
		{
			ppImage = CV_GET_SEQ_ELEM(IplImage*, m_cvImageSeq, *piImgNo);
			CvRect* FaceRect = (CvRect*)cvGetSeqElem( m_sqDiffFaceRect, i );
			//半身
			CvRect recUpBody;
			GetUpperbody(FaceRect, cvGetSize(*ppImage), &recUpBody);
			IplImage *SubFace = GetSubImage(*ppImage, recUpBody);

			/////////////////////////////////////////输出////////////////////////////////////////////////////////
			//存子图
			char strOutFilename[MAXLEN_FNAME];
			sprintf_s( strOutFilename, MAXLEN_FNAME, "bestFace_%d.jpg", iFaceNum++);
			char strOutPath[MAXLEN_PATH];
			sprintf_s( strOutPath, MAXLEN_PATH, "%s%s", m_outputDir, strOutFilename );
			cvSaveImage(strOutPath, SubFace);

			strcat_s( m_resString, MAXLEN_RESSTR, strOutFilename );
			char strNum[10];
			sprintf_s( strNum, 10, "\t%d\t",  *piImgNo );
			strcat_s( m_resString, MAXLEN_RESSTR, strNum );
			////////////////////////////////////////////////////////////////////////////////////////////////////////////////
			cvReleaseImage(&SubFace);
		}

	}

	Target* targetArray = 0;
	if( outputMode == 1 && nTotalValidImages > 0 )
	{
//*
		m_nTotalValidImages = nTotalValidImages;
		targetArray = new Target[nTotalValidImages];
		int curId = 0;
		for( int i = 0; i < nTotalImages; i++ )
		{
			int curFaces = facesCntArray[i];
			if( curFaces > 0 )
			{
				targetArray[curId].BaseFrame = *((Frame*)cvGetSeqElem( m_cvFrameSeq, i ));
				targetArray[curId].FaceCount = curFaces;
				targetArray[curId].FaceData = new IplImage*[curFaces];
				targetArray[curId].FaceRects = new CvRect[curFaces];//20090827 Added for Record Face Positions
				targetArray[curId].FaceOrgRects = new CvRect[curFaces];//20090929 Added for Face Recognition Purpose
				for( int j = 0; j < curFaces; j++ )
				{
					targetArray[curId].FaceData[j] = 0;
					targetArray[curId].FaceRects[j] = cvRect(0,0,0,0);//20090827 Added for Record Face Positions
					targetArray[curId].FaceOrgRects[j] = cvRect(0,0,0,0);//20090929 Added for Face Recognition Purpose
				}
				curId++;
			}
		}

		for (int i=0; i<(m_sqDiffFaceRect?m_sqDiffFaceRect->total:0); i++)
		{
			int *piImgNo = (int*)cvGetSeqElem(m_sqImgNo, i);
			ppImage = CV_GET_SEQ_ELEM(IplImage*, m_cvImageSeq, *piImgNo);
			CvRect* FaceRect = (CvRect*)cvGetSeqElem( m_sqDiffFaceRect, i );
			//半身
			CvRect recUpBody;
			GetUpperbody(FaceRect, cvGetSize(*ppImage), &recUpBody);
			IplImage *SubFace = GetSubImage(*ppImage, recUpBody);

			int nSubImageWidth = SubFace->width;
			int nSubImageHeight = SubFace->height;
			int nSubImageWidthStep = SubFace->widthStep;
			BYTE* data = (BYTE*)(SubFace->imageDataOrigin);

			int curFaces = facesCntArray[*piImgNo];
			curId = 0;
			for( int j = 0; j < nTotalImages; j++ )
			{
				if( j == *piImgNo )
					break;
				int curFaces = facesCntArray[j];
				if( curFaces > 0 ) curId++;
			}
			for( int j = 0; j < curFaces; j++ )
			{
				if( targetArray[curId].FaceData[j] == 0 )
				{
					targetArray[curId].FaceData[j] = SubFace;
					targetArray[curId].FaceRects[j] = recUpBody;//20090827 Added for Record Face Positions
					targetArray[curId].FaceOrgRects[j] = *FaceRect;//20090929 Added for Face Recognition Purpose
					break;
				}
			}
			
			SubFace = 0;
		}
//*/
	}

		

	delete[] facesCntArray;
/*
	/////////////////////////////////////////输出////////////////////////////////////////////////////////
	char strNum[10];
	sprintf_s( strNum, 10, "%d\t", iminImgIdx );
	strcat_s( m_resString, MAXLEN_RESSTR, strNum );//输出第几幅图为最优
	sprintf_s( strNum, 10, "%d\t", 1 );//输出该图中找到几个脸
	strcat_s( m_resString, MAXLEN_RESSTR, strNum );

	//存子图
	char strOutFilename[MAXLEN_FNAME];
	sprintf_s( strOutFilename, MAXLEN_FNAME, "bestFace.jpg" );
	char strOutPath[MAXLEN_PATH];
	sprintf_s( strOutPath, MAXLEN_PATH, "%s%s", m_outputDir, strOutFilename );
	cvSaveImage(strOutPath, SubFace);

	strcat_s( m_resString, MAXLEN_RESSTR, strOutFilename );
	////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//*/
	//////////////////////////////////////////////////////////////////////////
	//清理内存
	cvReleaseMemStorage(&pFrmJudgeStorage);
	cvReleaseMemStorage(&pFrmRectStorage);
	MultiObjectRelease();
	//////////////////////////////////////////////////////////////////////////
	ClearImgSeq();//清空各个链表

	if( outputMode == 0 )
		return m_resString;

	if( outputMode == 1 )
	{
		m_targets = targetArray;
		targetArray = 0;
		//ReleaseTargets( m_targets, m_nTotalValidImages );//内存泄露检测
		return (char*)m_targets;
	}
}

void CFaceSelect::MultiObjectInitialize()
{
	m_pDiffRectStorage = cvCreateMemStorage(0);
	m_pDiffJudgeStorage = cvCreateMemStorage(0);
	m_sqDiffFaceRect = cvCreateSeq(0, sizeof(CvSeq), sizeof(CvRect), m_pDiffRectStorage);
	m_sqDiffFaceJudge = cvCreateSeq(0, sizeof(CvSeq), sizeof(double), m_pDiffJudgeStorage);

	m_pHistStorage = cvCreateMemStorage(0);
	m_sqHist = cvCreateSeq(0, sizeof(CvSeq), sizeof(CvHistogram*), m_pHistStorage);

	m_pImgNoStorage = cvCreateMemStorage(0);
	m_sqImgNo = cvCreateSeq(0, sizeof(CvSeq), sizeof(int), m_pImgNoStorage);
}

void CFaceSelect::MultiObjectRelease()
{
	cvReleaseMemStorage(&m_pDiffRectStorage);
	cvReleaseMemStorage(&m_pDiffJudgeStorage);

	for (int i=0; i<(m_sqHist?m_sqHist->total:0); i++)
	{
		CvHistogram **ppHist = CV_GET_SEQ_ELEM(CvHistogram*, m_sqHist, i);
		cvReleaseHist(ppHist);
	}
	cvReleaseMemStorage(&m_pHistStorage);
	cvReleaseMemStorage(&m_pImgNoStorage);
}

bool CFaceSelect::HistCompare(CvHistogram *pHistA, CvHistogram *pHistB, CvHistogram *pHist_a, CvHistogram*pHist_b)
{
	double dSimilarA = cvCompareHist(pHistA, pHist_a, CV_COMP_CORREL);
	double dSimilarB = cvCompareHist(pHistB, pHist_b, CV_COMP_CORREL);

	if ((dSimilarA>0.50)&&(dSimilarB>0.50))
		return true;
	else
		return false;
}

void CFaceSelect::EnlargeCompareRect(CvRect *rect, CvSize ImgSz, CvRect *RectOut)
{
	RectOut->height = rect->height*2;
	RectOut->width = rect->width;
	RectOut->x = rect->x;
	RectOut->y = rect->y;
	if (RectOut->y+RectOut->height > ImgSz.height)
	{
		RectOut->height = ImgSz.height - RectOut->y;
	}
}

void CFaceSelect::MultiObjectJudge(CvSeq *sqFrmRect, CvSeq *sqFrmJudge, int iFrmNo)
{
	int i, j, k;
	IplImage **ppCurImg;
	CvRect LargRect;
	float fChnlRang[2] = {-32768, 32767}; //直方图范围
	int iDimSize = 65536;
	float *pfChnlRang =  fChnlRang;
	IplImage *pSubImg;
	IplImage *pLabplane;
	IplImage *L_plane; 
	IplImage *a_plane; 
	IplImage *b_plane; 
	int iCmpedIdx[100];//一张图片中最多有100张脸
	int iCmpedNo;
	bool bSameFace;

	if (iFrmNo == 0)
	{
		ppCurImg = CV_GET_SEQ_ELEM(IplImage*, m_cvImageSeq, iFrmNo);
		//直接添加
		for (i=0; i<(sqFrmRect?sqFrmRect->total:0); i++)
		{
			CvRect *pRect = (CvRect*)cvGetSeqElem( sqFrmRect, i );
			double *pJudge = (double*)cvGetSeqElem( sqFrmJudge, i);

			cvSeqPush(m_sqDiffFaceRect, pRect);
			cvSeqPush(m_sqDiffFaceJudge, pJudge);
			cvSeqPush(m_sqImgNo, &iFrmNo);

			EnlargeCompareRect(pRect, cvGetSize(*ppCurImg), &LargRect);

			CvRect TrimRect;
			TrimRect.height = LargRect.height;
			TrimRect.width = LargRect.width*2/3;
			TrimRect.y = LargRect.y;
			TrimRect.x = LargRect.x+LargRect.width/6;

			pSubImg = GetSubImage(*ppCurImg, TrimRect);			
			BRGtoHSV(pSubImg, a_plane, b_plane, L_plane);
			CvHistogram *pHist_a = cvCreateHist(1, &iDimSize, CV_HIST_ARRAY, (float**)(&pfChnlRang), 1);
			CvHistogram *pHist_b = cvCreateHist(1, &iDimSize, CV_HIST_ARRAY, (float**)(&pfChnlRang), 1);
			cvCalcHist(&a_plane, pHist_a, 0, 0);
			cvCalcHist(&b_plane, pHist_b, 0, 0);
			cvNormalizeHist(pHist_a, 1);
			cvNormalizeHist(pHist_b, 1);

			cvSeqPush(m_sqHist, &pHist_a);
			cvSeqPush(m_sqHist, &pHist_b);

			cvReleaseImage(&L_plane);
			cvReleaseImage(&a_plane);
			cvReleaseImage(&b_plane);
			cvReleaseImage(&pSubImg);
		}
	} 
	else
	{
		ppCurImg = CV_GET_SEQ_ELEM(IplImage*, m_cvImageSeq, iFrmNo);

		iCmpedNo = 0;

		for (i=0; i<(sqFrmRect?sqFrmRect->total:0); i++)
		{
			CvRect *pRect = (CvRect*)cvGetSeqElem( sqFrmRect, i );
			double *pJudge = (double*)cvGetSeqElem( sqFrmJudge, i);

			EnlargeCompareRect(pRect, cvGetSize(*ppCurImg), &LargRect);

			CvRect TrimRect;
			TrimRect.height = LargRect.height;
			TrimRect.width = LargRect.width*2/3;
			TrimRect.y = LargRect.y;
			TrimRect.x = LargRect.x+LargRect.width/6;		

			pSubImg = GetSubImage(*ppCurImg, TrimRect);			
			BRGtoHSV(pSubImg, a_plane, b_plane, L_plane);
			CvHistogram *pHist_a = cvCreateHist(1, &iDimSize, CV_HIST_ARRAY, (float**)(&pfChnlRang), 1);
			CvHistogram *pHist_b = cvCreateHist(1, &iDimSize, CV_HIST_ARRAY, (float**)(&pfChnlRang), 1);
			cvCalcHist(&a_plane, pHist_a, 0, 0);
			cvCalcHist(&b_plane, pHist_b, 0, 0);
			cvNormalizeHist(pHist_a, 1);
			cvNormalizeHist(pHist_b, 1);

			cvReleaseImage(&L_plane);
			cvReleaseImage(&a_plane);
			cvReleaseImage(&b_plane);
			cvReleaseImage(&pSubImg);

			bSameFace = false;
			for (j=0; j<(m_sqDiffFaceRect?m_sqDiffFaceRect->total:0); j++)
			{
				for (k=0; k<iCmpedNo; k++)
				{
					if (iCmpedIdx[k]== j)
						goto CMP_END;
				}

				CvHistogram **ppHistA = CV_GET_SEQ_ELEM(CvHistogram*, m_sqHist, j*2);
				CvHistogram **ppHistB = CV_GET_SEQ_ELEM(CvHistogram*, m_sqHist, j*2+1);

				bSameFace = HistCompare(*ppHistA, *ppHistB, pHist_a, pHist_b);

				//////////////////////////////////////////////////////////////////////////
				//test
				//bSameFace = false;

				if (bSameFace==true)
				{
					iCmpedIdx[iCmpedNo++] = j;
					double *pdBestJudge = (double*)cvGetSeqElem(m_sqDiffFaceJudge, j);
					if ((*pJudge) < (*pdBestJudge))
					{
						*pdBestJudge = *pJudge;
						CvRect *pBestRect = (CvRect*)cvGetSeqElem(m_sqDiffFaceRect, j);
						pBestRect->height = pRect->height;
						pBestRect->width = pRect->width;
						pBestRect->x = pRect->x;
						pBestRect->y = pRect->y;
						int *piBestIdx = (int*)cvGetSeqElem(m_sqImgNo, j);
						*piBestIdx = iFrmNo;
					}
					//图像在时间上递变，存储最新的标准最有效
					//直接拷贝——可以改为加权平均
					cvCopyHist(pHist_a, ppHistA);
					cvCopyHist(pHist_b, ppHistB);
					break;
				}
CMP_END:;
			}

			if (bSameFace==false)
			{
				iCmpedIdx[iCmpedNo++] = m_sqDiffFaceRect?m_sqDiffFaceRect->total:0;
				cvSeqPush(m_sqDiffFaceRect, pRect);
				cvSeqPush(m_sqDiffFaceJudge, pJudge);
				cvSeqPush(m_sqImgNo, &iFrmNo);
				cvSeqPush(m_sqHist, &pHist_a);
				cvSeqPush(m_sqHist, &pHist_b);
			}
			else
			{
				cvReleaseHist(&pHist_a);
				cvReleaseHist(&pHist_b);
			}
		}
	}
}


///////////////////////////////////Michael Add Eye-Mouth Analysis////////////////////////////////////////////

#ifdef SAVE_FACEANA_DEBUG_INFO//调试用到的函数
bool GetOrgRect( CvRect* rcOrg, const CvRect* rcScale, double dScale )
{
	if( !rcOrg ) return false;

	rcOrg->x = int( rcScale->x / dScale );
	rcOrg->y = int( rcScale->y / dScale );
	rcOrg->width = int( rcScale->width / dScale );
	rcOrg->height = int( rcScale->height / dScale );

	return true;
}


void drawFaces( IplImage* img, CvSeq* faces, double dScale )
{
	static CvScalar colors[] = 
	{
		{{0,0,255}},
		{{0,128,255}},
		{{0,255,255}},
		{{0,255,0}},
		{{255,128,0}},
		{{255,255,0}},
		{{255,0,0}},
		{{255,0,255}}
	};

	if( !faces || !img ) return; 

	int i = 0;
	int nfaces = faces->total;
	for( i = 0; i < nfaces; i++ )
	{
		CvRect* r = (CvRect*)cvGetSeqElem( faces, i );
		CvRect rScale = *r;
		GetOrgRect( &rScale, r, dScale );
		cvRectangle(img,cvPoint(rScale.x,rScale.y),cvPoint( rScale.x+rScale.width, rScale.y+rScale.height), colors[i%8], 3, 8, 0 );
		/*
		CvPoint center;
		int radius;
		center.x = cvRound((r->x + r->width*0.5)*scale);
		center.y = cvRound((r->y + r->height*0.5)*scale);
		radius = cvRound((r->width + r->height)*0.25*scale);
		cvCircle( img, center, radius, colors[i%8], 3, 8, 0 );
		*/
	}
}

void drawFaces( IplImage* img, CvSeq* faces )
{
	drawFaces( img, faces, 1.0f );
}
#endif


bool CFaceSelect::initSubFaceFeatureCascade( )
{
	InitCascade( cascade_name_eye, m_cascade_eye, m_casMem_eye );
	InitCascade( cascade_name_reye, m_cascade_reye, m_casMem_reye );
	InitCascade( cascade_name_mouth, m_cascade_mouth, m_casMem_mouth );

	m_storage_subfacefeature = cvCreateMemStorage( 0 );

	return ( m_cascade_eye && m_cascade_reye && m_cascade_mouth );
}
void CFaceSelect::releaseSubFaceFeatureCascade( )
{
	ReleaseCascade( m_cascade_eye, m_casMem_eye );
	ReleaseCascade( m_cascade_reye, m_casMem_reye );
	ReleaseCascade( m_cascade_mouth, m_casMem_mouth );

	cvReleaseMemStorage( &m_storage_subfacefeature );
}

bool CFaceSelect::getAllSubFeatures( IplImage* imgFace, CvSeq* leyes, CvSeq* reyes, CvSeq* mouths )
{
	if( !imgFace ) return false;

	const int NormWidth = 200;

	IplImage* imgNormFace = 0;
	double normRatio = 1.0f;

	if( imgFace->width >= NormWidth )
	{
		imgNormFace = cvCloneImage( imgFace );
	}
	else
	{
		imgNormFace = cvCreateImage( cvSize( NormWidth, imgFace->height * NormWidth / imgFace->width), imgFace->depth, imgFace->nChannels);
		cvResize( imgFace, imgNormFace );

		normRatio = NormWidth / (double)imgFace->width;
	}

	CvSeq* leyes_InNormFace = getSubFaceFeature( imgNormFace, nRgnBound_leye, m_cascade_eye,
		m_storage_subfacefeature, cvSize(18, 12) );
	CvSeq* reyes_InNormFace = getSubFaceFeature( imgNormFace, nRgnBound_reye, m_cascade_reye,
		m_storage_subfacefeature, cvSize(18, 12) );
	CvSeq* mouths_InNormFace = getSubFaceFeature( imgNormFace, nRgnBound_mouth, m_cascade_mouth,
		m_storage_subfacefeature, cvSize(25, 15) );

	cvClearSeq( leyes );
	cvClearSeq( reyes );
	cvClearSeq( mouths );
	GetOrgRectSeq( leyes, leyes_InNormFace, normRatio );
	GetOrgRectSeq( reyes, reyes_InNormFace, normRatio );
	GetOrgRectSeq( mouths, mouths_InNormFace, normRatio );

	cvReleaseImage( &imgNormFace );
	return true;
}

CvSeq* CFaceSelect::getSubFaceFeature( IplImage* imgFace, const int rgnBound[], CvHaarClassifierCascade* cascade,
						 CvMemStorage* storage, CvSize min_size )
{
	if( !imgFace ) return 0;
	if( !cascade ) return 0;
	if( !storage ) return 0;

	CvSeq* objects = 0;
	CvRect rgn;
	rgn.x = imgFace->width * rgnBound[0] / 100;
	rgn.y = imgFace->height * rgnBound[1] / 100;
	rgn.width = imgFace->width * ( rgnBound[2] - rgnBound[0] ) / 100;
	rgn.height = imgFace->height * ( rgnBound[3] - rgnBound[1] ) / 100;	

	IplImage* imgSubFace = 0;
	imgSubFace = cvCreateImage( cvSize( rgn.width, rgn.height ), imgFace->depth, imgFace->nChannels);
	cvSetImageROI (imgFace, rgn );	
	cvCopy( imgFace, imgSubFace );

	//IplImage* imgGray = cvCreateImage( cvSize(imgSubFace->width,imgSubFace->height), 8, 1 );
	//cvCvtColor( imgSubFace, imgGray, CV_BGR2GRAY );
	//cvEqualizeHist( imgGray, imgGray );

	objects = cvHaarDetectObjects( imgSubFace, cascade, storage,
		1.1, 2, CV_HAAR_DO_CANNY_PRUNING,
		min_size );

	//cvReleaseImage( &imgGray );

	if( objects )
	{
		int i = 0;
		for( i = 0; i < objects->total; i++ )
		{
			CvRect* r = (CvRect*)cvGetSeqElem( objects, i );
			transferLocalRc2SceneRc( r, r, &rgn, 1.0f );
		}
	}

	cvReleaseImage( &imgSubFace );

	return objects;
}
bool CFaceSelect::faceSubfeaturesRate( CvSeq* leyes, CvSeq* reyes, CvSeq* mouths, double &dConf, CvSeq* subFaceFeature )
{
#ifdef SAVE_FACEANA_DEBUG_INFO
	CString strDebugFilename = g_curDebugInfoFilePrefix + _T("_allsubfeas.txt");
	FILE *fp = 0;
#ifdef _UNICODE
	USES_CONVERSION;
	char* pstrFilepath = T2A( strDebugFilename );
	fp = fopen( pstrFilepath, "w+" );
#else
	fp = fopen( strDebugFilename, "w+" );
#endif
	
#endif
	dConf = 0.0f;

	int nleyes = leyes ? leyes->total : 0 ;
	int nreyes = reyes ? reyes->total : 0;
	int nmouths = mouths ? mouths->total : 0;

#ifdef SAVE_FACEANA_DEBUG_INFO
	fprintf( fp, "nleyes = %d; nreyes = %d; nmouths = %d; \n", nleyes, nreyes, nmouths );
#endif

	if( nleyes == 0 && nreyes == 0 && nmouths == 0 )//未找到眼睛和嘴巴
	{
		dConf = 0.0f;
	}
	else if( nleyes == 0 && nreyes == 0 && nmouths )//只找到嘴巴
	{
		dConf = 0.1f;
	}
	else if( ( nleyes == 0 || nreyes == 0 ) && nmouths == 0 )//找到一只眼睛没找到嘴巴
	{
		dConf = 0.2f;
	}
	else if( ( nleyes == 0 || nreyes == 0 ) && nmouths )//找到一只眼睛且找到嘴巴
	{
		dConf = 0.4f;
	}
	else//if( nleyes && nreyes )
	{
		assert( nleyes && nreyes );

		float weight_difInY = 1.0f;
		float weight_difInArea = 0.1f;

		float fMinDif = weight_difInY + weight_difInArea;
		CvRect* leye_best = 0;
		CvRect* reye_best = 0;
		int eyedist = 0;
		int eyeheight = 0;
		int eye_cenx = 0;
		int eyes_disx = 0;
		int eyes_disy = 0;
		int leye_area = 0;
		int reye_area = 0;
		float eyes_difInY = 0.0f;
		float eyes_difInArea = 0.0f;

		int l_id = 0;
		int r_id = 0;
		for( l_id = 0; l_id < nleyes; l_id++ )
		{
			CvRect* leye = (CvRect*)cvGetSeqElem( leyes, l_id );
			int leye_cenx = leye->x + leye->width / 2;
			int leye_ceny = leye->y + leye->height / 2;
			int area1 = leye->width * leye->height;
			for( r_id = 0; r_id < nreyes; r_id++ )
			{
				CvRect* reye = (CvRect*)cvGetSeqElem( reyes, r_id );
				int reye_cenx = reye->x + reye->width / 2;
				int reye_ceny = reye->y + reye->height / 2;

				int disx = abs( reye_cenx - leye_cenx );
				int disy = abs( reye_ceny - leye_ceny );

				int area2 = reye->width * reye->height;

				float difInY = (float)disy / (float)disx;
				float difInArea = (float)abs( area1 - area2 ) / (float)max( area1, area2 );

				float fDifRatio = weight_difInY * difInY + weight_difInArea * difInArea;

				if( fDifRatio < fMinDif )
				{
					fMinDif = fDifRatio;
					leye_best = leye;
					reye_best = reye;

					eyes_disx = disx;
					eyes_disy = disy;
					leye_area = area1;
					reye_area = area2;
					eyes_difInY = difInY;
					eyes_difInArea = difInArea;
					eyedist = disx;
					eyeheight = ( leye_ceny + reye_ceny ) / 2;
					eye_cenx = ( leye_cenx + reye_cenx ) / 2;
				}
			}
		}

#ifdef SAVE_FACEANA_DEBUG_INFO
		fprintf( fp, "eyes_disx = %d; eyes_disy = %d; leye_area = %d; reye_area = %d;\n", eyes_disx, eyes_disy, leye_area, reye_area );
		fprintf( fp, "eyes_difInY = %f;\n", eyes_difInY );
		fprintf( fp, "eyes_difInArea = %f;\n", eyes_difInArea );
		fprintf( fp, "fEyesDif = %f;\n", fMinDif );
		fprintf( fp, "fEyesDist = %d;\n", eyedist );
		fprintf( fp, "fEyesHeight = %d;\n", eyeheight );
		fprintf( fp, "fEyeCenX = %d;\n", eye_cenx );
#endif

		if( leye_best && reye_best )
		{
			cvSeqPush( subFaceFeature, leye_best );
			cvSeqPush( subFaceFeature, reye_best );

			if( nmouths )
			{
				int m_id = 0;
				CvRect* mouth_sel = (CvRect*)cvGetSeqElem( mouths, 0 );
				for( m_id = 1; m_id < nmouths; m_id++ )
				{
					CvRect* mouth = (CvRect*)cvGetSeqElem( mouths, m_id );

					if( mouth->y + mouth->height / 2 > mouth_sel->y + mouth_sel->height / 2 )
					{
						mouth_sel = mouth;
					}
				}

				cvSeqPush( subFaceFeature, mouth_sel );

				int eye_mouth_dist = ( mouth_sel->y + mouth_sel->height / 2 ) - eyeheight ;
				int mouth_cenx = mouth_sel->x + mouth_sel->width / 2;
				int eye_mouth_distInX = abs( mouth_cenx - eye_cenx );

				float fFaceVHRatio = (float)eye_mouth_dist / (float)eyedist;
				float minThre = 1.0f;
				float maxThre = 1.8f;
				if( fFaceVHRatio < minThre )
				{
					fFaceVHRatio = minThre - fFaceVHRatio;
				}
				else if( fFaceVHRatio > maxThre )
				{
					fFaceVHRatio = fFaceVHRatio - maxThre;
				}
				else
				{
					fFaceVHRatio = 0.0f;
				}

				float fMouthDisplaceRatio = (float)eye_mouth_distInX  / (float)eyedist;
				float weight_MouthDisplace = 1.0f;
				float weight_faceVHRatio = 1.0f;

				dConf = 1.0f - ( fMinDif + fFaceVHRatio * weight_faceVHRatio + fMouthDisplaceRatio * weight_MouthDisplace );

#ifdef SAVE_FACEANA_DEBUG_INFO
				fprintf( fp, "eye_mouth_dist = %d;\n", eye_mouth_dist );
				fprintf( fp, "eye_mouth_dif = %f;\n", fFaceVHRatio );
				fprintf( fp, "mouth_cenx = %d;\n", mouth_cenx );
				fprintf( fp, "eye_mouth_distInX = %d;\n", eye_mouth_distInX );
				fprintf( fp, "fMouthDisplaceRatio = %f;\n", fMouthDisplaceRatio );
#endif
			}
			else
			{
				dConf = 1.0f - ( fMinDif + 0.5f );
			}
		}

#ifdef SAVE_FACEANA_DEBUG_INFO
		fprintf( fp, "FinalConf = %f;\n", dConf );
#endif
	}

#ifdef SAVE_FACEANA_DEBUG_INFO
	fclose( fp );
	fp = 0;
#endif
	return true;
}
bool CFaceSelect::rateFace( IplImage* imgFace, double& dConf )
{
	cvClearMemStorage( m_storage_subfacefeature );

	if( CheckOverExpo( imgFace) )
	{
		dConf = 0.0f;
		return false;
	}

	CvSeq* leyes = 0;
	CvSeq* reyes = 0;
	CvSeq* mouths = 0;
	CvSeq* subFaceFeature = 0;

	leyes = cvCreateSeq( 0, sizeof(CvSeq), sizeof(CvRect), m_storage_subfacefeature );
	reyes = cvCreateSeq( 0, sizeof(CvSeq), sizeof(CvRect), m_storage_subfacefeature );
	mouths = cvCreateSeq( 0, sizeof(CvSeq), sizeof(CvRect), m_storage_subfacefeature );
	subFaceFeature = cvCreateSeq( 0, sizeof(CvSeq), sizeof(CvRect), m_storage_subfacefeature );

	if( !getAllSubFeatures( imgFace, leyes, reyes, mouths ) )
	{
		return false;
	}
#ifdef SAVE_FACEANA_DEBUG_INFO
	{
		IplImage* imgFaceShow = 0;
		imgFaceShow = cvCloneImage( imgFace );
		drawFaces( imgFaceShow, leyes );
		drawFaces( imgFaceShow, reyes );
		drawFaces( imgFaceShow, mouths );
		CString strDebugFilename = g_curDebugInfoFilePrefix + _T("_allsubfeas.jpg");
#ifdef _UNICODE
		USES_CONVERSION;
		char* pstrFilepath = T2A( strDebugFilename );
		cvSaveImage( pstrFilepath, imgFaceShow );
#else
		cvSaveImage( strDebugFilename, imgFaceShow );
#endif
		cvReleaseImage( &imgFaceShow );
	}
#endif

	if( !faceSubfeaturesRate( leyes, reyes, mouths, dConf, subFaceFeature ) )
	{
		return false;
	}

#ifdef SAVE_FACEANA_DEBUG_INFO
	{
		IplImage* imgFaceShow = 0;
		imgFaceShow = cvCloneImage( imgFace );
		drawFaces( imgFaceShow, subFaceFeature );
		CString strDebugFilename = g_curDebugInfoFilePrefix + _T("_selsubfeas.jpg");
#ifdef _UNICODE
		USES_CONVERSION;
		char* pstrFilepath = T2A( strDebugFilename );
		cvSaveImage( pstrFilepath, imgFaceShow );
#else
		cvSaveImage( strDebugFilename, imgFaceShow );
#endif
		cvReleaseImage( &imgFaceShow );
	}
#endif

	return true;
}
bool CFaceSelect::transferLocalRc2SceneRc( CvRect* rcInScene, CvRect* rcLocal, CvRect* rcRgn, double dScale )
{
	if( !rcInScene || !rcLocal || !rcRgn ) return false;

	rcInScene->x = int( rcRgn->x + rcLocal->x * dScale );
	rcInScene->y = int( rcRgn->y + rcLocal->y * dScale );
	rcInScene->width = int( rcLocal->width * dScale );
	rcInScene->height = int( rcLocal->height * dScale );

	return true;
}

void CFaceSelect::EyeMouthTest( const char *cFileName )
{
	static int iFaceNum = 0;
	static int iImageNum = 0;
	int iSFactor = 0;
	double dJdgFactor = 0;
	char sFileName[100];
	double dSize = 0;
	CvScalar color = {0,0,0,0};

	IplImage *pImage = cvLoadImage(cFileName, CV_LOAD_IMAGE_UNCHANGED);
	IplImage *pDrawImage = cvLoadImage(cFileName, CV_LOAD_IMAGE_UNCHANGED);

	//找脸并存储
	CvMemStorage *storage = cvCreateMemStorage(0);
	CvSeq* faces = FaceDetect( pImage, storage, cvSize(m_iFaceSize, m_iFaceSize), m_bScaleFaceDetection, m_dScale);

	//Debug 画脸
	for( int ii = 0; ii < (faces?faces->total:0); ii++ )
	{
		CvRect* r = (CvRect*)cvGetSeqElem( faces, ii );
		CvRect rScale = *r;

		//* Michael Test Eye-Mouth Analysis
		IplImage *SubFace = GetSubImage(pImage, rScale);
		double dConf = 0.0f;

#ifdef SAVE_FACEANA_DEBUG_INFO
		CString strPathPrefix = _T("");
		strPathPrefix.Format( _T("../Debug/MidRes/Face%d_"), iFaceNum );
		setCurDebugInfoPath( strPathPrefix );
#endif
		rateFace( SubFace, dConf );

		if( dConf < 0.3 )
		{
			sprintf_s(sFileName, 60, "../Debug/False/Img%d_Face%d_%5f.jpg", iFaceNum, ii, (float)(dConf));

			CvScalar color ={255,0,0,0};//blue color
			cvRectangle( pDrawImage,cvPoint(r->x,r->y),cvPoint( r->x+r->width, r->y+r->height), color, 3, 8, 0 );
		}
		else
		{
			sprintf_s(sFileName, 60, "../Debug/True/Img%d_Face%d_%5f.jpg", iFaceNum,  ii, (float)(dConf));

			CvScalar color ={0,0,255,0};//red color
			cvRectangle( pDrawImage,cvPoint(r->x,r->y),cvPoint( r->x+r->width, r->y+r->height), color, 3, 8, 0 );

			CvRect recUpBody;
			GetUpperbody(r, cvGetSize(pDrawImage), &recUpBody);
			color = cvScalar(0,255,0,0);//green color
			cvRectangle(pDrawImage,cvPoint(recUpBody.x,recUpBody.y),cvPoint( recUpBody.x+recUpBody.width, recUpBody.y+recUpBody.height), color, 2, 8, 0 );
		}

		cvSaveImage(sFileName, SubFace);
		cvReleaseImage(&SubFace);
		//*/
	}

	sprintf_s( sFileName, 60, "../Debug/Img%d.jpg", iFaceNum++ );
	cvSaveImage( sFileName, pDrawImage );

	//char sFileName_test[MAXLEN_PATH];
	//int npathlen = MAXLEN_PATH;
	//sFileName_test[0] = 0;
	//strcpy( sFileName_test, m_outputDir );
	//strcat( sFileName_test, "a.jpg" );
	//cvSaveImage( sFileName_test, pDrawImage );

	cvReleaseImage(&pDrawImage);
	cvReleaseImage(&pImage);
	cvReleaseMemStorage(&storage);
}

void CFaceSelect::CodeDebugTest_Michael(const char *cFileName)
{
	if( !strcmp( cFileName, "BKG") )
	{
		m_bDebugForBkgAna = true;
		RemoveNoiseByBkgAna();
		m_bDebugForBkgAna = false;
		ClearImgSeq();//清空各个链表
	}
	else
	{
		EyeMouthTest( cFileName );
	}
}
//////////////////////////////////End -- Michael Add Eye-Mouth Analysis///////////////////////////////////////

////////////////////////////Yuki Add Upperbody Analysis/////////////////////////////////////////////
bool CFaceSelect::initUpperbodyCascade( )
{
	//InitCascade( cascade_name_upperbody, m_cascade_upperbody );

	m_storage_upperbody = cvCreateMemStorage( 0 );

	return ( m_cascade_upperbody != 0 );
}

void CFaceSelect::releaseUpperbodyCascade( )
{
	//ReleaseCascade( m_cascade_upperbody );

	cvReleaseMemStorage( &m_storage_upperbody );
}

CvSeq* CFaceSelect::UpperbodyDetect( IplImage* pImage,  CvMemStorage* pStorage, CvSize rcMinSize, bool bScale, double dScale)
{
	if (!pImage)
	{
		return 0;
	}

	if( !m_cascade_upperbody )
	{
		return 0;
	}

	if( !pStorage )
	{
		return 0;
	}

	if( pImage->depth != 8 || pImage->nChannels != 3 )
	{
		return 0;
	}

	cvClearMemStorage( pStorage );

	CvSeq *pUpperbody = 0;
	IplImage* img = pImage;
	CvSeq* upperbody = 0;
	CvMemStorage* storage = cvCreateMemStorage(0);

	if (bScale == 1)
	{
		ResizeImg( pImage, img, dScale );//降采样
	}

	IplImage* imgGray = cvCreateImage( cvSize(img->width,img->height), 8, 1 );
	cvCvtColor( img, imgGray, CV_BGR2GRAY );
	cvEqualizeHist( imgGray, imgGray );

	if( m_cascade_upperbody )
	{		
		if (bScale == 1)
		{
			pUpperbody = cvCreateSeq( 0, sizeof(CvSeq), sizeof(CvRect), pStorage );
			GetOrgRectSeq( pUpperbody, upperbody, dScale );
		} 
		else
		{
			pUpperbody = cvCloneSeq(upperbody, pStorage);
		}		
	}


	if (bScale == 1)
	{
		cvReleaseImage(&img);
	}
	cvReleaseImage( &imgGray );
	cvReleaseMemStorage(&storage);

	return pUpperbody;
}

void CFaceSelect::GetUpperbody(  CvRect* pImage, CvSize rcSize,  CvRect* pUpperbody)
{
	pUpperbody->x = pImage->x - (int)(pImage->width * m_ExRatio_l);
	pUpperbody->x = pUpperbody->x < 0? 0:pUpperbody->x;
	pUpperbody->width = (int)(( m_ExRatio_l + m_ExRatio_r + 1.0 ) * pImage->width);
	pUpperbody->width = (pUpperbody->x+pUpperbody->width < rcSize.width)?pUpperbody->width:(rcSize.width - pUpperbody->x);
	
	pUpperbody->y = pImage->y - (int)(pImage->height * m_ExRatio_t);
	pUpperbody->y = pUpperbody->y < 0? 0:pUpperbody->y;
	pUpperbody->height = (int)(( m_ExRatio_t + m_ExRatio_b + 1.0f ) * pImage->height);
	pUpperbody->height = (pUpperbody->y+pUpperbody->height < rcSize.height)?pUpperbody->height:(rcSize.height - pUpperbody->y);

}

int CFaceSelect::Compare2Rect( CvRect* rcFace, CvRect* rcFaceInSeq )
{
	int i;
	int x1,x2,y1,y2,width1,height1;
	int area_rcFace,area_rcFaceInSeq,area_combine;
	int combine_x, combine_y;
	float ratearea1,ratearea2;
	if (rcFace->x <= rcFaceInSeq->x)
	{
		x1 = rcFace->x;
		x2 = rcFaceInSeq->x;
		width1 = rcFace->width;
	}
	else
	{

		x2 = rcFace->x;
		x1 = rcFaceInSeq->x;
		width1 = rcFaceInSeq->width;
	}
	if (rcFace->y <= rcFaceInSeq->y)
	{
		y1 = rcFace->y;
		y2 = rcFaceInSeq->y;
		height1 = rcFace->height;
	}
	else
	{

		y2 = rcFace->y;
		y1 = rcFaceInSeq->y;
		height1 = rcFaceInSeq->height;
	}
	combine_x = x1 + width1 - x2;
	combine_y = y1 + height1 -y2;
	area_combine = combine_x * combine_y;
	area_rcFace = rcFace->width * rcFace->height;
	area_rcFaceInSeq = rcFaceInSeq->width * rcFaceInSeq->height;
	ratearea1 = (float)area_combine/(float)area_rcFace;
	ratearea2 = (float)area_combine/(float)area_rcFaceInSeq;
	if ((rcFace->x <= rcFaceInSeq->x)&&(rcFace->x + rcFace->width >= rcFaceInSeq->x + rcFaceInSeq->width)&&
		(rcFace->y <= rcFaceInSeq->y)&&(rcFace->y + rcFace->height >= rcFaceInSeq->y + rcFaceInSeq->height))
	{
		i = 1;
	}
	else if ((rcFace->x >= rcFaceInSeq->x)&&(rcFace->x + rcFace->width <= rcFaceInSeq->x + rcFaceInSeq->width)&&
		(rcFace->y >= rcFaceInSeq->y)&&(rcFace->y + rcFace->height <= rcFaceInSeq->y + rcFaceInSeq->height))
	{
		i = 2;
	}
	
	else
	{
		i = 0;
	}
	if ((combine_x>0)&&(combine_y>0)&&(ratearea1>0.9)&&(ratearea2>0.9))
	{
		i = 3;
	}
	return i;
}

void CFaceSelect::MergeResultRectSeq( CvSeq* cvResultRectSeq, CvSeq* cvResultRectFactorSeq, CvRect* rcResultFace, double* dFactor )
{
	CvRect* rcFaceInSeq;
	int counter = 0;
	int k;
	double* dFactorInSeq;
	bool replace_flag = 0, merge_flag = 0;
	int iNumberInSeq;

	if ( cvResultRectSeq->total == 0 )
	{	
		cvSeqPush( cvResultRectSeq, rcResultFace );
		cvSeqPush( cvResultRectFactorSeq, dFactor);
	}
	else
	{
		for (int i=0; i<cvResultRectSeq->total; i++)
		{
			rcFaceInSeq = (CvRect*)cvGetSeqElem( cvResultRectSeq, i);
			k = Compare2Rect(rcResultFace, rcFaceInSeq);
			if ( k == 1)
			{
				replace_flag = 1;
				iNumberInSeq = i;
				break;
			}
			if (k == 2)
			{
				break;
			}
			if (k == 3)
			{
				merge_flag = 1;
				iNumberInSeq = i;
				break;
			}
			counter++;
		}
		if (counter == cvResultRectSeq->total)
		{
			cvSeqPush( cvResultRectSeq, rcResultFace );
			cvSeqPush( cvResultRectFactorSeq, dFactor);
		}
		if (replace_flag)
		{
			cvSeqRemove(cvResultRectSeq, iNumberInSeq);

			cvSeqInsert(cvResultRectSeq, iNumberInSeq, rcResultFace);

			dFactorInSeq = (double*)cvGetSeqElem( cvResultRectFactorSeq, iNumberInSeq);
			if (&dFactor < &dFactorInSeq)
			{
				dFactorInSeq = dFactor;
			}
			cvSeqRemove(cvResultRectFactorSeq,iNumberInSeq);
			cvSeqInsert(cvResultRectFactorSeq,iNumberInSeq,dFactorInSeq);
		}
		if (merge_flag)
		{
			dFactorInSeq = (double*)cvGetSeqElem( cvResultRectFactorSeq, iNumberInSeq);
			if (&dFactor< &dFactorInSeq)
			{
				cvSeqRemove(cvResultRectSeq, iNumberInSeq);
				cvSeqInsert(cvResultRectSeq, iNumberInSeq, rcResultFace);
			}
		}

	}
}
////////////////////////////End--Yuki Add Upperbody Analysis//////////////////////////////////////////////


///////////////////////////Michael Add 对外接口相关///////////////////////////////////////////////
void CFaceSelect::SetROI( int x, int y, int width, int height )
{
	m_rcROI = cvRect( x, y, width, height );
}

void CFaceSelect::SetFaceParas( int iMinFace, double dFaceChangeRatio )
{
	m_iFaceSize = iMinFace;
	m_dFaceChangeRatio = dFaceChangeRatio;
}

void CFaceSelect::SetDwSmpRatio( double dRatio )
{
	if( fabs( dRatio - 1.0 ) < 0.1f )
	{
		m_bScaleFaceDetection = 0;
	}
	else
	{
		m_bScaleFaceDetection = 1;
		m_dScale = dRatio;
	}
}

void CFaceSelect::SetOutputDir( const char* dir )
{
	int nlen = (int)strlen(dir);
	if( nlen >= MAXLEN_OUTDIR - 1 ) 
	{
		m_outputDir[0] = 0;
		return;
	}
	strncpy_s( m_outputDir,MAXLEN_OUTDIR, dir, nlen + 1 );
	if( dir[nlen-1] != '/' && dir[nlen-1] != '\\')
	{
		m_outputDir[nlen] = '/';
		m_outputDir[nlen+1] = 0;
	}
	m_outputDir[MAXLEN_OUTDIR-1] = 0;
}

void CFaceSelect::SetExRatio( double topExRatio, double bottomExRatio, double leftExRatio, double rightExRatio )
{
	m_ExRatio_t = topExRatio;
	m_ExRatio_b = bottomExRatio;
	m_ExRatio_l = leftExRatio;
	m_ExRatio_r = rightExRatio;
}
void CFaceSelect::ClearImgSeq()
{
	ClearImageSeq();
	ClearFaceSeq();
	ClearStorageSeq();
}

void CFaceSelect::ClearImageSeq()
{
	if( m_cvImageSeq )
	{
		IplImage **ppImage;
		for (int i=0; i<m_cvImageSeq->total; i++)
		{
			ppImage = CV_GET_SEQ_ELEM(IplImage*, m_cvImageSeq, i);
			cvReleaseImage(ppImage);
		}

		cvClearSeq(m_cvImageSeq);
		cvClearSeq(m_cvFrameSeq);
		cvClearSeq(m_cvRealFaceSeq);
	}

	//if( m_cvSeqStorage )
	//	cvClearMemStorage(m_cvSeqStorage);
}

void CFaceSelect::ClearFaceSeq()
{
	if( m_cvSeqFaceSeq )
	{
		CvSeq **ppSeq;
		for (int i=0; i<m_cvSeqFaceSeq->total; i++)
		{
			ppSeq = CV_GET_SEQ_ELEM(CvSeq*, m_cvSeqFaceSeq, i);
			cvClearSeq(*ppSeq);
		}
		cvClearSeq(m_cvSeqFaceSeq);
	}
	//if( m_cvSeqFaceSeqStorage )
	//	cvClearMemStorage(m_cvSeqFaceSeqStorage);

}

void CFaceSelect::ClearStorageSeq()
{
	if( m_cvSeqFaceStorage )
	{
		CvMemStorage **ppMemSeq;
		for (int i=0; i<m_cvSeqFaceStorage->total; i++)
		{
			ppMemSeq = CV_GET_SEQ_ELEM(CvMemStorage*, m_cvSeqFaceStorage, i);
			cvReleaseMemStorage(ppMemSeq);
		}
		cvClearSeq(m_cvSeqFaceStorage);
	}
}
void CFaceSelect::SetLightMode(int iMode)
{
	if(iMode>=0 && iMode<=2)
		m_iLightCondition = iMode;
	else
		m_iLightCondition = 0;
}
/////////////////////////End -- Michael Add 对外接口相关////////////////////////////////////////

//////////////////Michael Add Rough Background Remove//////////////
void CFaceSelect::DrawFacesInImgSeq( const char* prefix, const char* postfix )
{
	int i = 0;
	int j = 0;
	IplImage **ppImage = 0;
	CvSeq **ppFace = 0;
	for( i = 0; i < (m_cvImageSeq?m_cvImageSeq->total:0); i++ )
	{
		ppImage = CV_GET_SEQ_ELEM(IplImage*, m_cvImageSeq, i);
		IplImage* pImage = cvCloneImage( *ppImage );
		ppFace = CV_GET_SEQ_ELEM(CvSeq*, m_cvSeqFaceSeq, i);
		CvSeq *pFace = *ppFace;

		for( j = 0; j < (pFace?pFace->total:0); j++ )
		{
			CvRect* r = (CvRect*)cvGetSeqElem( pFace, j );
			CvScalar color ={255,0,0,0};//blue color
			cvRectangle( pImage,cvPoint(r->x,r->y),cvPoint( r->x+r->width, r->y+r->height), color, 3, 8, 0 );
		}

		char fname[200];
		sprintf_s( fname, 200, "%s%d%s.jpg", prefix, i, postfix );
		cvSaveImage( fname, pImage );

		cvReleaseImage( &pImage );
	}
}
bool CFaceSelect::RemoveNoiseByBkgAna( )
{
	if( m_bDebugForBkgAna )
	{
		char prefix[10];
		sprintf_s( prefix, 10, "BkgAna_" );
		char postfix[10];
		sprintf_s( postfix, 10, "_before" );
		DrawFacesInImgSeq( prefix, postfix );
	}

	IplImage* imgGray[MAXINIMG];
	IplImage* imgFg[MAXINIMG];
	IplImage* imgBg = 0;
	for( int i = 0; i < MAXINIMG; i++ )
	{
		imgGray[i] = 0;
		imgFg[i] = 0;
	}

	int nInImgs = m_cvImageSeq?m_cvImageSeq->total:0;


	int nw = 0;
	int nh = 0;
	IplImage **ppImage = 0;
	CvSeq **ppFace = 0;
	for( int i = 0; i < nInImgs; i++ )
	{
		ppImage = CV_GET_SEQ_ELEM(IplImage*, m_cvImageSeq, i);
		IplImage* pImage = *ppImage;
		if( i == 0 )
		{
			nw = pImage->width;
			nh = pImage->height;

			imgBg = cvCreateImage( cvSize(pImage->width,pImage->height), 8, 1 );
		}
		else if( pImage->width != nw || pImage->height != nh )
		{
			nInImgs = i;
			break;
		}
		imgGray[i] = cvCreateImage( cvSize(pImage->width,pImage->height), 8, 1 );
		imgFg[i] = cvCreateImage(  cvSize(pImage->width,pImage->height), 8, 1 );
		//cvSmooth(pImage, pImage, CV_MEDIAN, 3, 0, 0, 0);
		cvCvtColor( pImage, imgGray[i], CV_BGR2GRAY );
	}

	CvSeq* seqPixVals = 0;
	CvMemStorage* storage_PixVals = cvCreateMemStorage( 0 );
	seqPixVals = cvCreateSeq( 0, sizeof(CvSeq), sizeof(unsigned char), storage_PixVals );
	for( int i = 0; i < nh; i++ )
	{
		for( int j = 0; j < nw; j++ )
		{
			cvClearSeq( seqPixVals );
			for( int ii = 0; ii < nInImgs; ii++ )
			{
				int nOffset = i * imgGray[ii]->widthStep + j;
				if( imgGray[ii] )
				{
					unsigned char PixVal = (unsigned char)(imgGray[ii]->imageData[nOffset]);
					int nId = 0;
					for( nId = 0; nId < seqPixVals->total; nId++ )
					{
						unsigned char* curPixVal = CV_GET_SEQ_ELEM( unsigned char, seqPixVals, nId );
						if( PixVal < *curPixVal ) break;
					}
					cvSeqInsert( seqPixVals, nId, &PixVal );
				}
			}

			if( seqPixVals->total == nInImgs )
			{
				unsigned char nBgPixVal = 0;
				int nBgPixs = 0;
				int nCurPixs = 0;
				int nTotalVal = 0;
				for( int ii = 0; ii < seqPixVals->total - 1; ii++ )
				{
					unsigned char* curPixVal = CV_GET_SEQ_ELEM( unsigned char, seqPixVals, ii );
					if( ii == 0 )
					{
						nCurPixs = 1;
						nTotalVal = *curPixVal;
					}
					unsigned char* nxtPixVal = CV_GET_SEQ_ELEM( unsigned char, seqPixVals, ii + 1);

					int diff = *nxtPixVal - *curPixVal;
					if( diff < max( 10, *nxtPixVal * 0.15 ) )
					{
						nCurPixs++;
						nTotalVal += *nxtPixVal;
					}
					else
					{
						if( nCurPixs > nBgPixs )
						{
							nBgPixs = nCurPixs;
							nBgPixVal = (unsigned char)( nTotalVal / nBgPixs );

							nCurPixs = 1;
							nTotalVal = *nxtPixVal;
						}
					}
				}

				if( nCurPixs == nInImgs )
				{
					nBgPixs = nCurPixs;
					nBgPixVal = (unsigned char)( nTotalVal / nBgPixs );
				}

				int nOffset = i * imgBg->widthStep + j;
				if( nBgPixs >= nInImgs / 2 )
				{
					imgBg->imageData[nOffset] = nBgPixVal;
					for( int ii = 0; ii < nInImgs; ii++ )
					{
						//int nOffset = i * imgFg[ii]->widthStep + j;
						unsigned char PixVal = imgGray[ii]->imageData[nOffset];
						int diff = abs( (int)PixVal - (int)nBgPixVal );
						if( diff > max( 15, nBgPixVal * 0.15 ) )
							imgFg[ii]->imageData[nOffset] = 255;
						else
							imgFg[ii]->imageData[nOffset] = 0;
					}
				}
				else
				{
					imgBg->imageData[nOffset] = 0;
					for( int ii = 0; ii < nInImgs; ii++ )
					{
						//int nOffset = i * imgGray[ii]->widthStep + j;
						imgFg[ii]->imageData[nOffset] = 255;
					}
				}
			}		


		}
	}

	cvReleaseMemStorage( &storage_PixVals );

	for( int i = 0; i < nInImgs; i++ )
	{
		if( m_bDebugForBkgAna && i == 0 )
			cvSaveImage( "BkgAna_bg.jpg", imgBg );

		if( m_bDebugForBkgAna )
		{
			char fname[200];
			sprintf_s( fname, 200, "BkgAna_%d_fg.jpg", i );
			if( imgFg[i] )
				cvSaveImage( fname, imgFg[i] );
		}

		ppFace = CV_GET_SEQ_ELEM(CvSeq*, m_cvSeqFaceSeq, i);
		CvSeq *pFace = *ppFace;

		if( imgFg[i] )
			for (int j=0; j<(pFace?pFace->total:0); j++)
			{
				bool bRealFace;
				CvRect* FaceRect = (CvRect*)cvGetSeqElem( pFace, j );

				IplImage *imgSubFace = GetSubImage( imgFg[i], *FaceRect );
				int nArea = imgSubFace->width * imgSubFace->height;
				int nValPxs = cvCountNonZero( imgSubFace );

				if( nValPxs < 0.5 * nArea )
				{
					cvSeqRemove( pFace, j );
					j--;
				}

				cvReleaseImage( &imgSubFace );
			}
	}

	for( int i = 0; i < MAXINIMG; i++ )
	{
		cvReleaseImage( &imgGray[i] );
		cvReleaseImage( &imgFg[i] );
	}
	cvReleaseImage( &imgBg );

	if( m_bDebugForBkgAna )
	{
		char prefix[10];
		sprintf_s( prefix, 10, "BkgAna_" );
		char postfix[10];
		sprintf_s( postfix, 10, "_after" );
		DrawFacesInImgSeq( prefix, postfix );
	}

	return true;
}
//////////////////End -- Michael Add Rough Background Remove//////////////

/////////////////Michael Add for OverExposure Ana///////////////////////////
bool CFaceSelect::CheckOverExpo( IplImage *pSrcImg )
{
	IplImage* imgGray = cvCreateImage( cvSize(pSrcImg->width,pSrcImg->height), 8, 1 );
	cvCvtColor( pSrcImg, imgGray, CV_BGR2GRAY );

	float fThre = 0.5f;
	unsigned char nThre = 240;

	int width = imgGray->width;
	int height = imgGray->height;
	int nArea = width * height;

	int iCnt = 0;
	for (int i=0; i<height; i++)
		for (int j=0; j<width; j++)
		{
			int iOffset = i*imgGray->widthStep + j;
			if( (unsigned char)(imgGray->imageData[iOffset]) > nThre )
				iCnt++;
		}

	cvReleaseImage(&imgGray);
	return (iCnt > (int)( nArea * fThre ));
}
////////////////End -- Michael Add for OverExposure Ana////////////////////

//20090716 Defined Interface
void CFaceSelect::AddInFrame(Frame frame)//依次添加一组图片
{
	if( AddInImage( frame.image, frame.searchRect ) )
	{
		cvSeqPush( m_cvFrameSeq, &frame );
	}
}
int CFaceSelect::SearchFaces(Target** targets)//一组添加完后，调用这个函数，返回脸数目
{
	//m_nTotalValidImages = 0;
	Target* targetArray = (Target*)SelectBestImage( 1 );
	*targets = targetArray;
	return m_nTotalValidImages;
}

void CFaceSelect::ReleaseTargets( Target* &targets, int &nCnt )
{
	if( targets == 0 ) return;

	for( int i = 0; i < nCnt; i++ )
	{
		delete[] targets[i].FaceData;
		delete[] targets[i].FaceRects;//20090827 Added for Record Face Positions
		delete[] targets[i].FaceOrgRects;//20090929 Added for Record Face Recognition Purpose
		targets[i].FaceData = 0;
		targets[i].FaceCount = 0;
	}

	delete[] targets;
	targets = 0;
	nCnt = 0;
}
//End -- 20090716 Defined Interface

//20090929 Add for Face Recognition Research
bool CFaceSelect::FaceImagePreprocess( IplImage* imgIn, IplImage* &imgNorm, CvRect roi )
{
	if( imgNorm ) cvReleaseImage( &imgNorm );
	const int nFaceImgWidth = 200;
	const int nFaceImgHeight = 200;
	imgNorm = cvCreateImage( cvSize( nFaceImgWidth, nFaceImgHeight ), 
		imgIn->depth, imgIn->nChannels );

	IplImage* imgSub = NULL;
	if( !CheckImageROI( imgIn, roi )
		|| ( roi.width <= 1 && roi.height <= 1 ) )
	{
		imgSub = cvCloneImage(imgIn);
	}
	else
	{
		imgSub = GetSubImage( imgIn, roi );
	}

	cvClearMemStorage( m_storage_subfacefeature );

	CvSeq* leyes = 0;
	CvSeq* reyes = 0;
	CvSeq* mouths = 0;
	CvSeq* subFaceFeature = 0;

	leyes = cvCreateSeq( 0, sizeof(CvSeq), sizeof(CvRect), m_storage_subfacefeature );
	reyes = cvCreateSeq( 0, sizeof(CvSeq), sizeof(CvRect), m_storage_subfacefeature );
	mouths = cvCreateSeq( 0, sizeof(CvSeq), sizeof(CvRect), m_storage_subfacefeature );
	subFaceFeature = cvCreateSeq( 0, sizeof(CvSeq), sizeof(CvRect), m_storage_subfacefeature );

	bool bFindEyes = false;
	CvRect* leye_best = 0;
	CvRect* reye_best = 0;
	if( getAllSubFeatures( imgSub, leyes, reyes, mouths ) )
	{
		int nleyes = leyes ? leyes->total : 0 ;
		int nreyes = reyes ? reyes->total : 0;
		int nmouths = mouths ? mouths->total : 0;

		if( SearchLREyes( leyes, reyes, leye_best, reye_best ) )
		{
			bFindEyes = true;
		}			
	}

	if( bFindEyes )
	{
//#define _DRAWEYES
#ifdef _DRAWEYES
		//CvRect* r = leye_best;
		//CvScalar color ={255,255,255,0};//BGR
		//cvRectangle( imgSub,cvPoint(r->x,r->y),cvPoint( r->x+r->width, r->y+r->height), color, 3, 8, 0 );
		//r = reye_best;
		//cvRectangle( imgSub,cvPoint(r->x,r->y),cvPoint( r->x+r->width, r->y+r->height), color, 3, 8, 0 );
		CvRect leye_inScene;
		CvRect reye_inScene;
		transferLocalRc2SceneRc( &leye_inScene, leye_best, &roi, 1.0 );
		transferLocalRc2SceneRc( &reye_inScene, reye_best, &roi, 1.0 );
		DrawRectCenter( imgIn, leye_inScene );
		DrawRectCenter( imgIn, reye_inScene );
#endif//End -- _DRAWEYES

		int leye_cenx = leye_best->x + leye_best->width / 2.0f + 0.5f;
		int leye_ceny = leye_best->y + leye_best->height / 2.0f + 0.5f;
		int reye_cenx = reye_best->x + reye_best->width / 2.0f + 0.5f;
		int reye_ceny = reye_best->y + reye_best->height / 2.0f + 0.5f;

		//ReEstimate the Face Rect
		CvRect rcFace;
		int eyedist = abs( reye_cenx - leye_cenx );
		CvPoint ptEyesCenter = cvPoint( ( leye_cenx + reye_cenx ) / 2.0f + 0.5f, 
			( leye_ceny + reye_ceny ) / 2.0f + 0.5f );
		rcFace.x = leye_cenx - eyedist / 2.0f;
		rcFace.width = eyedist * 2.0f;

		rcFace.x += rcFace.width * 0.1f;
		rcFace.width -= rcFace.width * 0.2f;

		rcFace.y = ptEyesCenter.y - eyedist * 0.95f;
		rcFace.height = eyedist * 2.6f;

		rcFace.y += rcFace.height * 0.125f;
		rcFace.height -= rcFace.height * 0.2f;

		CvRect rcFace_inScene = roi;
		transferLocalRc2SceneRc( &rcFace_inScene, &rcFace, &roi, 1.0 );

		/////////////////////////////////////////////Rotate//////////////////////////////////////////////////////////////////
		double angle = atan( (double)(reye_ceny - leye_ceny) / (double)(reye_cenx-leye_cenx) );//设置旋转角度
		IplImage* imgRotate = 0;
		SubImageRotate( imgIn, rcFace_inScene, imgRotate, -angle );
		if( imgRotate )
		{
			cvReleaseImage(&imgSub);
			imgSub = imgRotate;
			imgRotate = 0;
		}
		///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	}
	else
	{
		CvRect rRealFace;
		FaceReDraw(imgSub, &rRealFace);

		CvRect rcFace_inScene = roi;
		transferLocalRc2SceneRc( &rcFace_inScene, &rRealFace, &roi, 1.0 );

		cvReleaseImage(&imgSub);
		imgSub = GetSubImage(imgIn, rcFace_inScene);
	}

	cvResize( imgSub, imgNorm, CV_INTER_LINEAR );
	IplImage* imgGray = cvCreateImage( cvSize(imgNorm->width,imgNorm->height), 8, 1 );
	cvCvtColor( imgNorm, imgGray, CV_BGR2GRAY );
	cvEqualizeHist( imgGray, imgGray );//直方图均衡
	//cvSmooth(imgGray, imgGray, CV_MEDIAN, 3, 0, 0, 0);
	cvSmooth(imgGray, imgGray, CV_GAUSSIAN, 9, 9, 0, 0);
	cvReleaseImage( &imgNorm );
	imgNorm = imgGray;
	cvReleaseImage(&imgSub);

	return true;
}

bool CFaceSelect::FaceImagePreprocess_ForTrain( IplImage* imgIn, ImageArray &normImages, CvRect roi )
{
	int i = 0;
	if( normImages.nImageCount ) ReleaseImageArray( normImages );
	const int nGenImages = 5;
	normImages.nImageCount = nGenImages;
	normImages.imageArr = new IplImage*[nGenImages];
	for( i = 0; i < nGenImages; i++ )
	{
		normImages.imageArr[i] = 0;
	}

	IplImage *imgNorm = 0;
	if( imgNorm ) cvReleaseImage( &imgNorm );
	const int nFaceImgWidth = 200;
	const int nFaceImgHeight = 200;
	imgNorm = cvCreateImage( cvSize( nFaceImgWidth, nFaceImgHeight ), 
		imgIn->depth, imgIn->nChannels );

	IplImage* imgSub = NULL;
	if( !CheckImageROI( imgIn, roi )
		|| ( roi.width <= 1 && roi.height <= 1 ) )
	{
		imgSub = cvCloneImage(imgIn);
	}
	else
	{
		imgSub = GetSubImage( imgIn, roi );
	}

	cvClearMemStorage( m_storage_subfacefeature );

	CvSeq* leyes = 0;
	CvSeq* reyes = 0;
	CvSeq* mouths = 0;
	CvSeq* subFaceFeature = 0;

	leyes = cvCreateSeq( 0, sizeof(CvSeq), sizeof(CvRect), m_storage_subfacefeature );
	reyes = cvCreateSeq( 0, sizeof(CvSeq), sizeof(CvRect), m_storage_subfacefeature );
	mouths = cvCreateSeq( 0, sizeof(CvSeq), sizeof(CvRect), m_storage_subfacefeature );
	subFaceFeature = cvCreateSeq( 0, sizeof(CvSeq), sizeof(CvRect), m_storage_subfacefeature );

	bool bFindEyes = false;
	CvRect* leye_best = 0;
	CvRect* reye_best = 0;
	if( getAllSubFeatures( imgSub, leyes, reyes, mouths ) )
	{
		int nleyes = leyes ? leyes->total : 0 ;
		int nreyes = reyes ? reyes->total : 0;
		int nmouths = mouths ? mouths->total : 0;

		if( SearchLREyes( leyes, reyes, leye_best, reye_best ) )
		{
			bFindEyes = true;
		}			
	}

	IplImage* imgSubbak = cvCloneImage( imgSub );
	for( i = 0; i < nGenImages; i++ )
	{
		double plusangle = (float)( i - nGenImages / 2 ) / (float)( nGenImages / 2 ) * (float)10 * CV_PI / (float)180;
		if( imgSub) cvReleaseImage( &imgSub );
		imgSub = cvCloneImage( imgSubbak );
		if( bFindEyes )
		{
			//#define _DRAWEYES
#ifdef _DRAWEYES
			//CvRect* r = leye_best;
			//CvScalar color ={255,255,255,0};//BGR
			//cvRectangle( imgSub,cvPoint(r->x,r->y),cvPoint( r->x+r->width, r->y+r->height), color, 3, 8, 0 );
			//r = reye_best;
			//cvRectangle( imgSub,cvPoint(r->x,r->y),cvPoint( r->x+r->width, r->y+r->height), color, 3, 8, 0 );
			CvRect leye_inScene;
			CvRect reye_inScene;
			transferLocalRc2SceneRc( &leye_inScene, leye_best, &roi, 1.0 );
			transferLocalRc2SceneRc( &reye_inScene, reye_best, &roi, 1.0 );
			DrawRectCenter( imgIn, leye_inScene );
			DrawRectCenter( imgIn, reye_inScene );
#endif//End -- _DRAWEYES

			int leye_cenx = leye_best->x + leye_best->width / 2.0f + 0.5f;
			int leye_ceny = leye_best->y + leye_best->height / 2.0f + 0.5f;
			int reye_cenx = reye_best->x + reye_best->width / 2.0f + 0.5f;
			int reye_ceny = reye_best->y + reye_best->height / 2.0f + 0.5f;

			//ReEstimate the Face Rect
			CvRect rcFace;
			int eyedist = abs( reye_cenx - leye_cenx );
			CvPoint ptEyesCenter = cvPoint( ( leye_cenx + reye_cenx ) / 2.0f + 0.5f, 
				( leye_ceny + reye_ceny ) / 2.0f + 0.5f );
			rcFace.x = leye_cenx - eyedist / 2.0f;
			rcFace.width = eyedist * 2.0f;

			rcFace.x += rcFace.width * 0.1f;
			rcFace.width -= rcFace.width * 0.2f;

			rcFace.y = ptEyesCenter.y - eyedist * 0.95f;
			rcFace.height = eyedist * 2.6f;

			rcFace.y += rcFace.height * 0.125f;
			rcFace.height -= rcFace.height * 0.2f;

			CvRect rcFace_inScene = roi;
			transferLocalRc2SceneRc( &rcFace_inScene, &rcFace, &roi, 1.0 );

			/////////////////////////////////////////////Rotate//////////////////////////////////////////////////////////////////
			double angle = atan( (double)(reye_ceny - leye_ceny) / (double)(reye_cenx-leye_cenx) );//设置旋转角度

			angle += plusangle;

			IplImage* imgRotate = 0;
			SubImageRotate( imgIn, rcFace_inScene, imgRotate, -angle );
			if( imgRotate )
			{
				cvReleaseImage(&imgSub);
				imgSub = imgRotate;
				imgRotate = 0;
			}
			///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		}
		else
		{
			CvRect rRealFace;
			FaceReDraw(imgSub, &rRealFace);

			CvRect rcFace_inScene = roi;
			transferLocalRc2SceneRc( &rcFace_inScene, &rRealFace, &roi, 1.0 );

			//cvReleaseImage(&imgSub);
			//imgSub = GetSubImage(imgIn, rcFace_inScene);
			/////////////////////////////////////////////Rotate//////////////////////////////////////////////////////////////////
			double angle = 0;//设置旋转角度

			angle += plusangle;

			IplImage* imgRotate = 0;
			SubImageRotate( imgIn, rcFace_inScene, imgRotate, -angle );
			if( imgRotate )
			{
				cvReleaseImage(&imgSub);
				imgSub = imgRotate;
				imgRotate = 0;
			}
			///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		}

		cvResize( imgSub, imgNorm, CV_INTER_LINEAR );
		IplImage* imgGray = cvCreateImage( cvSize(imgNorm->width,imgNorm->height), 8, 1 );
		cvCvtColor( imgNorm, imgGray, CV_BGR2GRAY );
		cvEqualizeHist( imgGray, imgGray );//直方图均衡
		//cvSmooth(imgGray, imgGray, CV_MEDIAN, 3, 0, 0, 0);
		cvSmooth(imgGray, imgGray, CV_GAUSSIAN, 9, 9, 0, 0);
		normImages.imageArr[i] = imgGray;
	}

	cvReleaseImage(&imgSub);
	cvReleaseImage( &imgNorm );
	cvReleaseImage( &imgSubbak );


	return true;
}


void CFaceSelect::DrawRectCenter( IplImage* img, CvRect roi )
{
	CvPoint ptTL = cvPoint( roi.x, roi.y );//TopLeft
	CvPoint ptBR = cvPoint( roi.x + roi.width, roi.y + roi.height );//BottomRight
	CvPoint ptCen = cvPoint( ( ptTL.x + ptBR.x ) / 2.0f + 0.5, ( ptTL.y + ptBR.y ) / 2.0f + 0.5 );//Center
	CvPoint pt1, pt2, pt3, pt4;
	//horizontal line
	pt1.x = ptCen.x - roi.width / 5;
	pt1.y = ptCen.y;
	pt2.x = ptCen.x + roi.width / 5;
	pt2.y = ptCen.y;

	//vertical line
	pt3.x = ptCen.x;
	pt3.y = ptCen.y - roi.height / 5;
	pt4.x = ptCen.x;
	pt4.y = ptCen.y + roi.height / 5;

	cvLine( img, pt1, pt2,CV_RGB(255,255,255), 1);
	cvLine( img, pt3, pt4,CV_RGB(255,255,255), 1);
}

bool CFaceSelect::SubImageRotate_Ver1( IplImage* src, CvRect roi, IplImage* &dst, double angle )//has some problem, may rotate more angle
{
	if( dst )
		cvReleaseImage(&dst);

	bool bRoiInvalid = false;
	if( !CheckImageROI( src, roi )
		|| ( roi.width <= 1 && roi.height <= 1 ) )
	{
		dst = cvCloneImage(src);
	}
	else
	{
		dst = GetSubImage( src, roi );
	}

	int delta = 1;
	int opt = 0;	// 1： 旋转加缩放
	// 0:  仅仅旋转
	double factor;
	if (opt)		// 旋转加缩放
		factor = (cos (angle) + 1.0) * 2;
	else			//  仅仅旋转
		factor = 1;

	//Call : cvGetQuadrangleSubPix
	float m[6];
	// Matrix m looks like:
	// [ m0  m1  m2 ] ===>  [ A11  A12   b1 ]
	// [ m3  m4  m5 ] ===>  [ A21  A22   b2 ]
	CvMat M = cvMat (2, 3, CV_32F, m);
	int w = src->width;
	int h = src->height;
	m[0] = (float) (factor * cos (-angle * 2));
	m[1] = (float) (factor * sin (-angle * 2));
	m[3] = -m[1];
	m[4] = m[0];
	// 将旋转中心移至图像中间
	if( bRoiInvalid )
	{
		m[2] = w * 0.5f;
		m[5] = h * 0.5f;
	}
	else
	{
		m[2] = roi.x + roi.width / 2;
		m[5] = roi.y + roi.height / 2;
	}
	//  dst(x,y) = A * src(x,y) + b
	cvZero (dst);
	cvGetQuadrangleSubPix (src, dst, &M);

	return true;
}

bool CFaceSelect::SubImageRotate( IplImage* src, CvRect roi, IplImage* &dstImage, float radians )//Rotate Clockwisely
{
	if( dstImage )
		cvReleaseImage(&dstImage);

	bool bRoiInvalid = false;
	if( !CheckImageROI( src, roi )
		|| ( roi.width <= 1 && roi.height <= 1 ) )
	{
		dstImage = 0;
		return false;
	}

	int srcWidth,srcHeight;
	LONG dstWidth,dstHeight;
	int srcChannel,dstChannel;
	uchar* srcData, *dstData;
	int srcStep,dstStep;
	int i,j,k;
	float fCosa,fSina;
	fCosa = (float) cos((double)radians);
	fSina = (float) sin((double)radians);

	srcWidth = roi.width;
	srcHeight = roi.height;
	srcChannel = src->nChannels;

	//------------------------Step 1--------------------------------
	//Calculate the dstImage's width and height
	//by calculating the srcImage's 4 vertices' positions in the dstImage 

	CvPoint2D32f srcP1,srcP2,srcP3,srcP4;//4 vertexes
	CvPoint2D32f dstP1,dstP2,dstP3,dstP4;
	srcP1.x = (float) (- (srcWidth  - 1) / 2);
	srcP1.y = (float) (  (srcHeight - 1) / 2);

	srcP2.x = -srcP1.x;
	srcP2.y = srcP1.y;

	srcP3.x = srcP1.x;
	srcP3.y = -srcP1.y;

	srcP4.x = -srcP1.x;
	srcP4.y = -srcP1.y;

	dstP1.x =  fCosa * srcP1.x + fSina * srcP1.y;
	dstP1.y = -fSina * srcP1.x + fCosa * srcP1.y;
	dstP2.x =  fCosa * srcP2.x + fSina * srcP2.y;
	dstP2.y = -fSina * srcP2.x + fCosa * srcP2.y;
	dstP3.x =  fCosa * srcP3.x + fSina * srcP3.y;
	dstP3.y = -fSina * srcP3.x + fCosa * srcP3.y;
	dstP4.x =  fCosa * srcP4.x + fSina * srcP4.y;
	dstP4.y = -fSina * srcP4.x + fCosa * srcP4.y;

	dstWidth = (LONG) ( max( fabs(dstP4.x - dstP1.x), fabs(dstP3.x - dstP2.x) ) + 0.5);
	dstHeight = (LONG) ( max( fabs(dstP4.y - dstP1.y), fabs(dstP3.y - dstP2.y) )  + 0.5);

	//------------------------End -- Step 1--------------------------------

	float f1,f2;//offset pixels in x-axis and y-axis

	f1 = (float) (-(dstWidth-1) / 2 * fCosa - (dstHeight-1) / 2 * fSina + (srcWidth-1) / 2 + roi.x) ;
	f2 = (float) ((dstWidth-1) / 2 * fSina - (dstHeight-1) / 2 * fCosa + (srcHeight-1) / 2 + roi.y) ;

	dstImage = cvCreateImage(cvSize(dstWidth,dstHeight),IPL_DEPTH_8U,3);

	dstChannel = dstImage->nChannels;
	//------------------------Step 2--------------------------------
	//Begin Rotate!
	double srcX; //
	double srcY;
	LONG lx;
	LONG ly;
	double x,y;
	double thresh = 0.4;
	double temp1,temp2;

	srcData = (uchar*)src->imageData;
	srcStep = src->widthStep/sizeof(uchar);

	dstData = (uchar*)dstImage->imageData;
	dstStep = dstImage->widthStep/sizeof(uchar);

	int srcImageWidth = src->width;
	int srcImageHeight = src->height;
	for(i=0 ;i<dstHeight ;i++)
	{
		for (j=0 ;j<dstWidth ;j++)
		{
			srcY = -((float) j) * fSina + ((float) i) * fCosa + f2 + 0.5;
			srcX = ((float) j) * fCosa + ((float) i) * fSina + f1 + 0.5;
			ly = (LONG)srcY;
			lx = (LONG)srcX;
			x = srcX-lx;
			y = srcY-ly;

			if ((lx >= 0) && (lx < srcImageWidth) && (ly >= 0) && (ly < srcImageHeight))
			{
				for (k=0 ;k<dstChannel ;k++)//Interpolation
				{
					temp1 = srcData[ly*srcStep + lx*srcChannel + k] + x*(srcData[ly*srcStep + (lx + 1)*srcChannel + k]-srcData[ly*srcStep + lx*srcChannel + k]);
					temp2 = srcData[(ly + 1)*srcStep + lx*srcChannel + k] + x*(srcData[(ly + 1)*srcStep + (lx + 1)*srcChannel + k]-srcData[(ly + 1)*srcStep + lx*srcChannel + k]);
					dstData[i*dstStep + j*dstChannel + k] = (char)(temp1 + y*(temp2 - temp1));
				}
			}
			else
			{
				for (k=0 ;k<dstChannel ;k++)
				{
					dstData[i*dstStep + j*dstChannel + k] = 0;
				}
			}
		}
	}

	return dstImage;
}

IplImage* CFaceSelect::Rotate( IplImage* src , float radians )
{
	IplImage* dstImage;
	int srcWidth,srcHeight;
	LONG dstWidth,dstHeight;
	int srcChannel,dstChannel;
	uchar* srcData, *dstData;
	int srcStep,dstStep;
	int i,j,k;
	float fCosa,fSina;
	fCosa = (float) cos((double)radians);
	fSina = (float) sin((double)radians);

	srcWidth = src->width;
	srcHeight = src->height;
	srcChannel = src->nChannels;

	//------------------------Step 1--------------------------------
	//Calculate the dstImage's width and height
	//by calculating the srcImage's 4 vertices' positions in the dstImage 

	CvPoint2D32f srcP1,srcP2,srcP3,srcP4;//4 vertexes
	CvPoint2D32f dstP1,dstP2,dstP3,dstP4;
	srcP1.x = (float) (- (srcWidth  - 1) / 2);
	srcP1.y = (float) (  (srcHeight - 1) / 2);

	srcP2.x = -srcP1.x;
	srcP2.y = srcP1.y;

	srcP3.x = srcP1.x;
	srcP3.y = -srcP1.y;

	srcP4.x = -srcP1.x;
	srcP4.y = -srcP1.y;

	dstP1.x =  fCosa * srcP1.x + fSina * srcP1.y;
	dstP1.y = -fSina * srcP1.x + fCosa * srcP1.y;
	dstP2.x =  fCosa * srcP2.x + fSina * srcP2.y;
	dstP2.y = -fSina * srcP2.x + fCosa * srcP2.y;
	dstP3.x =  fCosa * srcP3.x + fSina * srcP3.y;
	dstP3.y = -fSina * srcP3.x + fCosa * srcP3.y;
	dstP4.x =  fCosa * srcP4.x + fSina * srcP4.y;
	dstP4.y = -fSina * srcP4.x + fCosa * srcP4.y;

	dstWidth = (LONG) ( max( fabs(dstP4.x - dstP1.x), fabs(dstP3.x - dstP2.x) ) + 0.5);
	dstHeight = (LONG) ( max( fabs(dstP4.y - dstP1.y), fabs(dstP3.y - dstP2.y) )  + 0.5);

	//------------------------End -- Step 1--------------------------------

	float f1,f2;

	f1 = (float) (-(dstWidth-1) / 2 * fCosa - (dstHeight-1) / 2 * fSina + (srcWidth-1) / 2 );
	f2 = (float) ((dstWidth-1) / 2 * fSina - (dstHeight-1) / 2 * fCosa + (srcHeight-1) / 2 );

	dstImage = cvCreateImage(cvSize(dstWidth,dstHeight),IPL_DEPTH_8U,3);

	dstChannel = dstImage->nChannels;
	//------------------------Step 2--------------------------------
	//Begin Rotate!
	double srcX; //
	double srcY;
	LONG lx;
	LONG ly;
	double x,y;
	double thresh = 0.4;
	double temp1,temp2;

	srcData = (uchar*)src->imageData;
	srcStep = src->widthStep/sizeof(uchar);

	dstData = (uchar*)dstImage->imageData;
	dstStep = dstImage->widthStep/sizeof(uchar);

	for(i=0 ;i<dstHeight ;i++)
	{
		for (j=0 ;j<dstWidth ;j++)
		{
			srcY = -((float) j) * fSina + ((float) i) * fCosa + f2 + 0.5;
			srcX = ((float) j) * fCosa + ((float) i) * fSina + f1 + 0.5;
			ly = (LONG)srcY;
			lx = (LONG)srcX;
			x = srcX-lx;
			y = srcY-ly;

			if ((lx >= 0) && (lx < srcWidth) && (ly >= 0) && (ly < srcHeight))
			{
				for (k=0 ;k<dstChannel ;k++)//Interpolation
				{
					temp1 = srcData[ly*srcStep + lx*srcChannel + k] + x*(srcData[ly*srcStep + (lx + 1)*srcChannel + k]-srcData[ly*srcStep + lx*srcChannel + k]);
					temp2 = srcData[(ly + 1)*srcStep + lx*srcChannel + k] + x*(srcData[(ly + 1)*srcStep + (lx + 1)*srcChannel + k]-srcData[(ly + 1)*srcStep + lx*srcChannel + k]);
					dstData[i*dstStep + j*dstChannel + k] = (char)(temp1 + y*(temp2 - temp1));
				}
			}
			else
			{
				for (k=0 ;k<dstChannel ;k++)
				{
					dstData[i*dstStep + j*dstChannel + k] = 0;
				}
			}
		}
	}
	return dstImage;
}

bool CFaceSelect::SearchLREyes( CvSeq* leyes, CvSeq* reyes, CvRect* &leye_best, CvRect* &reye_best )
{
	leye_best = 0;
	reye_best = 0;

	int nleyes = leyes ? leyes->total : 0 ;
	int nreyes = reyes ? reyes->total : 0;

	if( nleyes == 0 || nreyes == 0 ) return false;

	float weight_difInY = 1.0f;
	float weight_difInArea = 0.1f;

	float fMinDif = weight_difInY + weight_difInArea;
	int eyedist = 0;
	int eyeheight = 0;
	int eye_cenx = 0;
	int eyes_disx = 0;
	int eyes_disy = 0;
	int leye_area = 0;
	int reye_area = 0;
	float eyes_difInY = 0.0f;
	float eyes_difInArea = 0.0f;

	int l_id = 0;
	int r_id = 0;
	for( l_id = 0; l_id < nleyes; l_id++ )
	{
		CvRect* leye = (CvRect*)cvGetSeqElem( leyes, l_id );
		int leye_cenx = leye->x + leye->width / 2;
		int leye_ceny = leye->y + leye->height / 2;
		int area1 = leye->width * leye->height;
		for( r_id = 0; r_id < nreyes; r_id++ )
		{
			CvRect* reye = (CvRect*)cvGetSeqElem( reyes, r_id );
			int reye_cenx = reye->x + reye->width / 2;
			int reye_ceny = reye->y + reye->height / 2;

			int disx = abs( reye_cenx - leye_cenx );
			int disy = abs( reye_ceny - leye_ceny );

			int area2 = reye->width * reye->height;

			float difInY = (float)disy / (float)disx;
			float difInArea = (float)abs( area1 - area2 ) / (float)max( area1, area2 );

			float fDifRatio = weight_difInY * difInY + weight_difInArea * difInArea;

			if( fDifRatio < fMinDif )
			{
				fMinDif = fDifRatio;
				leye_best = leye;
				reye_best = reye;

				eyes_disx = disx;
				eyes_disy = disy;
				leye_area = area1;
				reye_area = area2;
				eyes_difInY = difInY;
				eyes_difInArea = difInArea;
				eyedist = disx;
				eyeheight = ( leye_ceny + reye_ceny ) / 2;
				eye_cenx = ( leye_cenx + reye_cenx ) / 2;
			}
		}
	}

	if( leye_best && reye_best )
		return true;
	else
		return false;
}

bool CFaceSelect::CheckImageROI( IplImage* img, CvRect roi )
{
	if( roi.x < 0 || roi.y < 0 ) return false;

	int imgWidth = img->width;
	int imgHeight = img->height;

	if( roi.x + roi.width >= imgWidth 
		|| roi.y + roi.height >= imgHeight )
	{
		return false;
	}

	return true;
}

void CFaceSelect::FaceReDraw(IplImage *pImage, CvRect *rRealFace)
{
	int iQuantizeStep = 2; //直方图量化步长
	float fChnlRang[2] = {0, 255}; //直方图范围
	int iDimSize = 256/iQuantizeStep;
	float *pfChnlRang =  fChnlRang;

	IplImage *pHSVImage = cvCreateImage(cvGetSize(pImage), pImage->depth, pImage->nChannels);
	IplImage* h_plane = cvCreateImage( cvGetSize(pHSVImage), 8, 1 );
	IplImage* s_plane = cvCreateImage( cvGetSize(pHSVImage), 8, 1 );
	IplImage* v_plane = cvCreateImage( cvGetSize(pHSVImage), 8, 1 );
	IplImage* s_plane_thr = cvCreateImage( cvGetSize(pHSVImage), 8, 1 );

	cvCvtColor(pImage, pHSVImage, CV_BGR2HSV);
	cvCvtPixToPlane( pHSVImage, h_plane, s_plane, v_plane, 0 );

	CvSize head = cvGetSize(pImage);
	CvRect roi;
	//去除脸图两侧可能的背景区域
	roi.x = (int)(head.width*m_dSglBkgProportion);
	roi.y = 0;
	roi.width = (int)(head.width*(1-m_dSglBkgProportion*2));
	roi.height = head.height;

	/*
	//debug 显示画框图像
	cvNamedWindow("Image",1);
	CvScalar color = {255,0,0,0};
	cvRectangle(pImage, cvPoint(roi.x, roi.y), cvPoint(roi.x+roi.width, roi.y+roi.height), color, 1, 8, 0);
	cvShowImage("Image", pImage);
	cvWaitKey(2000);
	// */

	//S变量直方图
	CvHistogram *pHist_s = cvCreateHist(1, &iDimSize, CV_HIST_ARRAY, (float**)(&pfChnlRang), 1);
	cvSetImageROI(s_plane, roi);
	cvCalcHist(&s_plane, pHist_s, 0, 0);
	cvResetImageROI(s_plane);

	///////////////////////////////////////////////////////

	//直方图二值化
	double dMaxPix = roi.width * roi.height;
	int iLowBnd =0 ;
	int iUpBnd = 0;
	int iSFactor = 0;
	iSFactor = GetHistRange(pHist_s, iDimSize-1, dMaxPix, iLowBnd, iUpBnd, m_dFaceProportion);
	int iSRange = iUpBnd - iLowBnd;
	iLowBnd = iLowBnd*iQuantizeStep;
	iUpBnd = (iUpBnd+1)*iQuantizeStep-1;
	cvThreshold(s_plane, s_plane_thr, iUpBnd, 255, CV_THRESH_TOZERO_INV);
	cvThreshold(s_plane_thr, s_plane, iLowBnd, 255, CV_THRESH_BINARY_INV);

	//画直方图
	//DrawHistImage(pHist_s, iDimSize-1, dMaxPix);
	cvReleaseHist(&pHist_s);

	//降噪
	DeNoise(s_plane, 2, 2, 0, 0);
	IplImage* s_plane_not = cvCreateImage( cvGetSize(pHSVImage), 8, 1 );
	cvNot(s_plane, s_plane_not);
	DeNoise(s_plane_not, 2, 2, 0, 0);
	cvNot(s_plane_not, s_plane);
	cvReleaseImage(&s_plane_not);

	//截取有效脸部区域
	int iLeft = 0;
	int iLenth = 0;
	GetEffectiveFaceWidth(s_plane, m_dEarNoise, 0, iLeft, iLenth);
	int iFaceOffset = 0;
#ifdef OBSOLETE_FUNC
	iFaceOffset = GetEffectiveFaceHeight(s_plane, m_dForeheadNoise, 0, iLenth);
#endif
	iFaceOffset = GetEffectiveFaceHeightBKG(s_plane, m_dUpBkgNoise, 0);

	rRealFace->height = s_plane->height - iFaceOffset;
	rRealFace->width = iLenth;
	rRealFace->x = iLeft;
	rRealFace->y = iFaceOffset;

	cvReleaseImage(&pHSVImage);
	cvReleaseImage(&h_plane);
	cvReleaseImage(&s_plane);
	cvReleaseImage(&v_plane);
	cvReleaseImage(&s_plane_thr);
}




extern "C" __declspec(dllexport) void ReleaseImageArray( ImageArray &images )
{
	int i = 0;
	for( i = 0; i < images.nImageCount; i++ )
	{
		cvReleaseImage( &images.imageArr[i] );
	}
	images.nImageCount = 0;
	delete[] images.imageArr;
	images.imageArr = 0;
}

