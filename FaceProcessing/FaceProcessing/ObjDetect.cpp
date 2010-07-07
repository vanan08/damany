#include "stdafx.h"
#include "ObjDetect.h"
#include "stdio.h" 


Kings::Imaging::CObjectDetector::CObjectDetector( void )
{
	m_pFrImg = 0;
	m_pBkImg = 0;

	m_pFrameMat = 0;
	m_pFrMat = 0;
	m_pBkMat = 0;
	m_pBkMat_Pre = 0;
	m_pDifImg = 0;
	m_pCurImg = 0;

	//隔帧
	m_pBufPreImg = 0;
	m_pBufPreMat = 0;
	m_pBufDifBetFrmsImg = 0;
	m_pBufDifBetFrmsMat = 0;

	m_pShadowMask = 0;
	m_pObjectMask = 0;

	m_nFrmNum = 0;

	m_storageObjs = cvCreateMemStorage( 0 );
	m_objcontour = 0;
	m_objRects = 0;

	m_fBkgUpRatio = 0.05f;

	m_ROI = cvRect( 0, 0, 0, 0 );
	m_bSetROI = false;

	m_bOjbMask = false;

	m_pPreImg = 0;
	m_pPreMat = 0;
	m_pDifBetFrmsImg = 0;//帧间差分结果
	m_pDifBetFrmsMat = 0;
}

Kings::Imaging::CObjectDetector::~CObjectDetector(void)
{
	cvReleaseImage(&m_pFrImg);
	cvReleaseImage(&m_pBkImg);
	cvReleaseImage(&m_pDifImg);
	cvReleaseImage(&m_pShadowMask);
	cvReleaseImage(&m_pObjectMask);
	cvReleaseImage(&m_pCurImg);
	
	//隔帧
	if (m_pBufPreImg)
		cvReleaseImage(&m_pBufPreImg);
	if (m_pBufDifBetFrmsImg)
		cvReleaseImage(&m_pBufDifBetFrmsImg);
	if (m_pBufPreMat)
		cvReleaseMat(&m_pBufPreMat);
	if (m_pBufDifBetFrmsMat)
		cvReleaseMat(&m_pBufDifBetFrmsMat);

	cvReleaseMat(&m_pFrameMat);
	cvReleaseMat(&m_pFrMat);
	cvReleaseMat(&m_pBkMat);
	cvReleaseMat(&m_pBkMat_Pre);

	m_nFrmNum = 0;

	if( m_objcontour ) cvClearSeq( m_objcontour );
	if( m_objRects ) cvClearSeq( m_objRects );
	cvReleaseMemStorage( &m_storageObjs );

	cvReleaseImage( &m_pPreImg );
	cvReleaseMat( &m_pPreMat );
	cvReleaseImage( &m_pDifBetFrmsImg );//帧间差分结果
	cvReleaseMat( &m_pDifBetFrmsMat );
}

CvMat* Kings::Imaging::CObjectDetector::SerializeMat(IplImage *pSrcImg, IplImage *pMask)
{
	int iCnt = 0;
	int iNum = CountPts(pMask);
	CvMat *pSrlMat = cvCreateMat(1, iNum, CV_32FC1);
	for (int i=0; i<pSrcImg->height; i++)
		for (int j=0; j<pSrcImg->width; j++)
		{
			int iOffset = i*pSrcImg->widthStep + j;
			if (pMask->imageData[iOffset])
			{
				*(pSrlMat->data.fl + iCnt) = (unsigned char)(pSrcImg->imageData[iOffset]);
				iCnt++;
			}
		}
		return pSrlMat;
}

bool Kings::Imaging::CObjectDetector::ShadowJudge(CvRect r, IplImage *pDiffImg, IplImage *pBkgImg, IplImage *pCurImg)
{
	bool bResult;
	float fResulta, fResultb;
	//float fResult;
	IplImage *pSubDiffImg = GetSubImage(pDiffImg, r);
	//cvSaveImage("mask.bmp", pSubDiffImg);
	IplImage *pSubBkgImg = GetSubImage(pBkgImg, r);
	//cvSaveImage("Bkg.bmp", pSubBkgImg);
	IplImage *pSubCurImg = GetSubImage(pCurImg, r);
	//cvSaveImage("Frg.bmp", pSubCurImg);

	IplImage *pSubPosDiff = cvCreateImage(cvGetSize(pSubDiffImg), 8, 1);
	IplImage *pSubNegDiff = cvCreateImage(cvGetSize(pSubDiffImg), 8, 1);
	pSubNegDiff->origin = pSubPosDiff->origin = pSubDiffImg->origin;
	cvCmp(pSubCurImg, pSubBkgImg, pSubPosDiff, CV_CMP_GT);
	cvAnd(pSubDiffImg, pSubPosDiff, pSubPosDiff);
	//cvSaveImage("maskPos.bmp", pSubPosDiff);
	cvCmp(pSubCurImg, pSubBkgImg, pSubNegDiff, CV_CMP_LT);
	cvAnd(pSubDiffImg, pSubNegDiff, pSubNegDiff);
	//cvSaveImage("maskNeg.bmp", pSubNegDiff);

	fResulta=0;
	fResultb=0;
	int iNonZeroPos = CountPts(pSubPosDiff);
	if (iNonZeroPos>9)
	{
		CvMat *pSubBkgMat = SerializeMat(pSubBkgImg, pSubPosDiff);
		CvMat *pSubCurMat = SerializeMat(pSubCurImg, pSubPosDiff);
		CvMat *pResult = cvCreateMat(1,1,CV_32FC1);

		cvMatchTemplate(pSubCurMat, pSubBkgMat, pResult,CV_TM_CCOEFF_NORMED);
		fResulta = *(pResult->data.fl);

		cvReleaseMat(&pResult);
		cvReleaseMat(&pSubBkgMat);
		cvReleaseMat(&pSubCurMat);

		if (fResulta<0.65)
		{
			bResult = false;
			cvReleaseImage(&pSubNegDiff);
			cvReleaseImage(&pSubPosDiff);
			cvReleaseImage(&pSubDiffImg);
			cvReleaseImage(&pSubBkgImg);
			cvReleaseImage(&pSubCurImg);

			return bResult;
		}
	}

	int iNonZeroNeg = CountPts(pSubNegDiff);
	if (iNonZeroNeg>9)
	{
		CvMat *pSubBkgMat = SerializeMat(pSubBkgImg, pSubNegDiff);
		CvMat *pSubCurMat = SerializeMat(pSubCurImg, pSubNegDiff);
		CvMat *pResult = cvCreateMat(1,1,CV_32FC1);

		cvMatchTemplate(pSubCurMat, pSubBkgMat, pResult,CV_TM_CCOEFF_NORMED);
		fResultb = *(pResult->data.fl);

		cvReleaseMat(&pResult);
		cvReleaseMat(&pSubBkgMat);
		cvReleaseMat(&pSubCurMat);

		if (fResultb<0.65)
		{
			bResult = false;
			cvReleaseImage(&pSubNegDiff);
			cvReleaseImage(&pSubPosDiff);
			cvReleaseImage(&pSubDiffImg);
			cvReleaseImage(&pSubBkgImg);
			cvReleaseImage(&pSubCurImg);

			return bResult;
		}
	}

	//fResult = ((float)(iNonZeroPos)*fResulta + (float)(iNonZeroNeg)*fResultb) / (float)(iNonZeroNeg+iNonZeroPos);

	cvReleaseImage(&pSubNegDiff);
	cvReleaseImage(&pSubPosDiff);
	cvReleaseImage(&pSubDiffImg);
	cvReleaseImage(&pSubBkgImg);
	cvReleaseImage(&pSubCurImg);

	return true;
}

void Kings::Imaging::CObjectDetector::FrameProcess( IplImage* pFrame )
{
	IplImage* pProcFrame = NULL;
	
	if( m_nFrmNum == 0 && m_bSetROI  )//对第一帧图像进行判断
	{
		if( m_ROI.width > 0 && m_ROI.height > 0 
			&& m_ROI.x >= 0 && m_ROI.y >= 0
			&& m_ROI.x + m_ROI.width <= pFrame->width
			&& m_ROI.y + m_ROI.height <= pFrame->height
			)
		{
			m_bSetROI = true;
		}
		else//设置区域无效
		{
			m_bSetROI = false;
			m_ROI = cvRect( 0, 0, 0, 0 );
		}
	}
	
	if( m_bSetROI )
	{
		pProcFrame = GetSubImage( pFrame, m_ROI );
	}
	else
	{
		pProcFrame = cvCloneImage( pFrame );
	}
	
	Process( pProcFrame );

	if( m_bSetROI )
	{
		for( int ii = 0; ii < (m_objRects? m_objRects ->total:0); ii++ )
		{
			CvRect* r = (CvRect*)cvGetSeqElem(m_objRects, ii );
			transferLocalRc2SceneRc( r, r, &m_ROI );
		}
	}

	cvReleaseImage( &pProcFrame );
}

void Kings::Imaging::CObjectDetector::InitEnvironment( IplImage* pFrame )
{
	m_pBkImg = cvCloneImage(pFrame);//Color Background Image
	m_pDifImg = cvCreateImage( cvSize(pFrame->width, pFrame->height), IPL_DEPTH_8U,1 );
	m_pObjectMask = cvCreateImage( cvSize(pFrame->width, pFrame->height), IPL_DEPTH_8U,1 );
	m_pFrImg = cvCreateImage(cvSize(pFrame->width, pFrame->height), IPL_DEPTH_8U,1);
	m_pDifImg->origin = m_pFrImg->origin = pFrame->origin;

	m_pFrameMat = cvCreateMat(pFrame->height, pFrame->width, CV_32FC3);

	m_pBkMat = cvCreateMat(pFrame->height, pFrame->width, CV_32FC3);
	m_pBkMat_Pre = cvCreateMat( pFrame->height, pFrame->width, CV_32FC3 );

	m_pFrMat = cvCreateMat(pFrame->height, pFrame->width, CV_32FC1);

	cvConvert(m_pBkImg, m_pBkMat);
}

void Kings::Imaging::CObjectDetector::SetBkg( IplImage* pFrame )//设置pFrame进入背景，请确保SetBkg与FrameProcess不会同时被调用
{
	if( m_nFrmNum == 0 )
	{
		m_nFrmNum++;

		InitEnvironment(pFrame);
	}
	else
	{
		cvReleaseImage(&m_pBkImg);

		m_pBkImg = cvCloneImage(pFrame);//Color Background Image

		cvConvert( m_pBkImg, m_pBkMat );
	}
}

void Kings::Imaging::CObjectDetector::Process(IplImage* pFrame)
{
	if( m_nFrmNum == 0)
	{
		m_nFrmNum++;

		InitEnvironment(pFrame);

		//m_pBkImg = cvCloneImage(pFrame);//Color Background Image
		//m_pDifImg = cvCreateImage( cvSize(pFrame->width, pFrame->height), IPL_DEPTH_8U,1 );
		//m_pObjectMask = cvCreateImage( cvSize(pFrame->width, pFrame->height), IPL_DEPTH_8U,1 );
		//m_pFrImg = cvCreateImage(cvSize(pFrame->width, pFrame->height), IPL_DEPTH_8U,1);
		//m_pDifImg->origin = m_pFrImg->origin = pFrame->origin;

		//m_pFrameMat = cvCreateMat(pFrame->height, pFrame->width, CV_32FC3);

		//m_pBkMat = cvCreateMat(pFrame->height, pFrame->width, CV_32FC3);
		//m_pBkMat_Pre = cvCreateMat( pFrame->height, pFrame->width, CV_32FC3 );

		//m_pFrMat = cvCreateMat(pFrame->height, pFrame->width, CV_32FC1);
		//
		//cvConvert(m_pBkImg, m_pBkMat);
	}
	else
	{
 	   	CvMat* pGrayBkgMat = cvCreateMat( pFrame->height, pFrame->width, CV_32FC1 );
		CvMat* pGrayCurMat = cvCreateMat( pFrame->height, pFrame->width, CV_32FC1 );
		CvMat* pSubMat = cvCreateMat( pFrame->height, pFrame->width, CV_32FC3 );

		cvConvert(pFrame, m_pFrameMat);

		cvAbsDiff(m_pBkMat, m_pFrameMat, pSubMat);
		cvCvtColor(pSubMat, m_pFrMat, CV_BGR2GRAY);

		//二值化前景图(这里采用特定阈值进行二值化)
		//cvThreshold(m_pFrMat, m_pDifImg, 20, 255.0, CV_THRESH_BINARY);
		cvCvtColor( m_pBkMat, pGrayBkgMat, CV_BGR2GRAY );
		DynThreshold( m_pFrMat, pGrayBkgMat, m_pDifImg, 0.09, 10, 20 );

		//去除阴影
		RemoveShadow(m_pDifImg, m_pBkMat, m_pFrameMat);
		//进行形态学滤波，去掉噪音
		cvErode(m_pDifImg, m_pDifImg, 0, 1);
		cvDilate(m_pDifImg, m_pDifImg, 0, 1);
		int ith = 0;
		int nTimes = 1;
		for( ith = 0; ith < nTimes; ith++ )
		{
			cvDilate(m_pDifImg, m_pDifImg, 0, 1);
		}
		for( ith = 0; ith < nTimes; ith++ )
		{
			cvErode(m_pDifImg, m_pDifImg, 0, 1);
		}

		if( m_objcontour )
			cvClearSeq( m_objcontour);
		if( m_objRects )
			cvClearSeq( m_objRects );
		if( m_storageObjs )
			cvClearMemStorage( m_storageObjs );
		cvFindContours( m_pDifImg, m_storageObjs, &m_objcontour, sizeof(CvContour), CV_RETR_CCOMP, CV_CHAIN_APPROX_SIMPLE );
		//cvZero( m_pDifImg );
		cvZero(m_pFrImg);
		cvCopy(m_pDifImg, m_pFrImg);
		cvZero( m_pObjectMask );
		m_objRects = cvCreateSeq( 0, sizeof(CvSeq), sizeof(CvRect), m_storageObjs);
		CvSeq* pCurContour = m_objcontour;
		for( ; pCurContour; pCurContour = pCurContour->h_next )
		{
			CvRect r = ((CvContour*)pCurContour)->rect;
			if(r.height * r.width > 200 ) // 面积小的方形抛弃掉
			{
				bool bNonShadow = true;
				//////////////////////////////////////////////////////////////////////////Add Shadow Analyze
				bNonShadow = JudgeNonShadow( m_pDifImg, r );//Michael's shadow detect function
				//////////////////////////////////////////////////////////////////////////End -- Add Shadow Analyze	
				CvScalar color = CV_RGB( 255, 255, 255 );
				if( bNonShadow )
				{
					cvDrawContours( m_pFrImg, pCurContour, color, color, -1, CV_FILLED, 8 );
					cvSeqPush( m_objRects, &r );
					cvDrawContours( m_pObjectMask, pCurContour, color, color, -1, CV_FILLED, 8 );
				}
			}
		}
		RemoveIntersectedRects( m_objRects );

		cvCopy( m_pBkMat, m_pBkMat_Pre );
		cvRunningAvg(m_pFrameMat, m_pBkMat, m_fBkgUpRatio, 0);
		if( m_bOjbMask )
			cvCopy( m_pBkMat_Pre, m_pBkMat, m_pObjectMask );//目标区域不进入背景
		cvConvert(m_pBkMat, m_pBkImg);

		cvReleaseMat(&pGrayBkgMat);
		cvReleaseMat(&pGrayCurMat);
		cvReleaseMat(&pSubMat);
	}
}

IplImage* Kings::Imaging::CObjectDetector::GetSubImage(IplImage* pOriImage, CvRect roi)
{
	IplImage * pSubImage = NULL;   

	cvSetImageROI(pOriImage, roi);
	pSubImage = cvCreateImage( cvSize(pOriImage->roi->width, pOriImage->roi->height), pOriImage->depth, pOriImage->nChannels );   
	pSubImage->origin = pOriImage->origin;   
	cvCopy(pOriImage, pSubImage);   
	cvResetImageROI(pOriImage);  

	return pSubImage;   
}

bool Kings::Imaging::CObjectDetector::JudgeNonShadow( IplImage* imgDifRes, CvRect roi )
{
	bool bNonShadow = true;

	double thre = 7.0f;

	IplImage* imgSub = GetSubImage( imgDifRes, roi );

	int nValPts = CountPts( imgSub );

	double diag_length = (double)imgSub->width * (double)imgSub->width
								+ (double)imgSub->height * (double)imgSub->height;
	diag_length = sqrt(  diag_length );//对角线长度

	double averObjWidth = (double)nValPts / diag_length;

	if( averObjWidth < thre )//判断目标是否成一条长线
	{
		bNonShadow = false;
	}

	cvReleaseImage( &imgSub );

	return bNonShadow;
}

bool Kings::Imaging::CObjectDetector::transferLocalRc2SceneRc( CvRect* rcInScene, CvRect* rcLocal, CvRect* rcRgn )
{
	if( !rcInScene || !rcLocal || !rcRgn ) return false;

	rcInScene->x = rcRgn->x + rcLocal->x;
	rcInScene->y = rcRgn->y + rcLocal->y;
	rcInScene->width = rcLocal->width;
	rcInScene->height = rcLocal->height;

	return true;
}

void Kings::Imaging::CObjectDetector::RemoveIntersectedRects( CvSeq* pObjRects )
{
	float fThreRatio = 0.6f;
	for( int ii = 0; ii < (pObjRects?pObjRects->total:0); ii++ )
	{
		CvRect* r1 = (CvRect*)cvGetSeqElem( pObjRects, ii );
		int Area1 = r1->width * r1->height;
		for( int jj = ii + 1; jj < (pObjRects?pObjRects->total:0); jj++ )
		{
			CvRect* r2 = (CvRect*)cvGetSeqElem( pObjRects, jj );
			int Area2 = r2->width * r2->height;
			int x1 = max( r1->x, r2->x );
			int y1 = max( r1->y, r2->y );
			int x2 = min( r1->x + r1->width, r2->x + r2->width);
			int y2 = min( r1->y + r1->height, r2->y + r2->height);

			if( x1 < x2 && y1 < y2 )
			{
				int intersectArea = ( x2 - x1 ) * ( y2 - y1 );
				if( intersectArea > fThreRatio * Area1 
					|| intersectArea > fThreRatio * Area2 )//需要合并
				{
					CvRect rcMerge = cvRect( 0, 0, 0, 0 );
					rcMerge.x = min( r1->x, r2->x );
					rcMerge.y = min( r1->y, r2->y );
					rcMerge.width = max( r1->x + r1->width, r2->x + r2->width ) - rcMerge.x;
					rcMerge.height = max( r1->y + r1->height, r2->y + r2->height) - rcMerge.y;

					cvSeqRemove( pObjRects, jj );
					cvSeqRemove( pObjRects, ii );
					cvSeqInsert( pObjRects, ii, &rcMerge );
					ii--;

					break;
				}
			}
		}
	}
}

int Kings::Imaging::CObjectDetector::CountPts( IplImage* pSrcImg )
{
	int iCnt = 0;
	for (int i=0; i<pSrcImg->height; i++)
		for (int j=0; j<pSrcImg->width; j++)
		{
			int iOffset = i*pSrcImg->widthStep + j;
			if (pSrcImg->imageData[iOffset])
			{
				iCnt++;
			}
		}
	return iCnt;
}

bool Kings::Imaging::CObjectDetector::DynThreshold( const CvMat* src, const CvMat* refer, IplImage* dst, float fThRatio, float fThMin, float fThMax )
{
	int w = dst->width;
	int h = dst->height;

	int w1 = src->cols;
	int h1 = src->rows;

	int w2 = refer->cols;
	int h2 = refer->rows;

	if( w1 != w || h1 != h 
		|| w2 != w || h2 != h )
	{
		return false;
	}

	for( int i = 0; i < h; i++ )
	{
		for( int j = 0; j < w; j++ )
		{
			float val = cvmGet(src, i, j);//src->data.fl[ i * src->cols + j ];
			float refval = cvmGet( refer, i, j) * fThRatio;//refer->data.fl[ i * src->cols + j ] * fThRatio;
			refval = refval > fThMin ? refval : fThMin;
			refval = refval < fThMax ? refval : fThMax;
			if( val > refval )
			{
				dst->imageData[dst->widthStep * i + j ] = 255;
			}
			else
			{
				dst->imageData[dst->widthStep * i + j ] = 0;
			}
		}
	}
	
	return true;
}



void Kings::Imaging::CObjectDetector::RemoveShadow(IplImage* pDiffImg, CvMat *pBkgMat, CvMat *pCurMat)
{
	double dRB, dGB, dBB, dRF, dGF, dBF, dMax, dLF, dLB;
	double dCF1, dCF2, dCF3, dCB1, dCB2, dCB3;
	double dDif1, dDif2, dDif3, dDif4, dVar;
	double dDifCoef = 0.03;

	for (int i=0; i<pDiffImg->height; i++)
		for (int j=0; j<pDiffImg->width; j++)
		{
			int iDifOffset = i*pDiffImg->widthStep + j;
			if (pDiffImg->imageData[iDifOffset])
			{
				int iMatOffset = i*3*pBkgMat->cols + j*3;
				dBB = pBkgMat->data.fl[iMatOffset];
				dGB = pBkgMat->data.fl[iMatOffset+1];
				dRB = pBkgMat->data.fl[iMatOffset+2];
				dLB = (dRB+dBB+dGB)/3;


				dBF = pCurMat->data.fl[iMatOffset];
				dGF = pCurMat->data.fl[iMatOffset+1];
				dRF = pCurMat->data.fl[iMatOffset+2];
				dLF = (dRF+dBF+dGF)/3;

				dDif4 = (abs(dRF-dRB)+abs(dGF-dGB)+abs(dBF-dBB))/3;
				dVar = ((abs(dRF-dRB)-dDif4)*(abs(dRF-dRB)-dDif4)+(abs(dGF-dGB)-dDif4)*(abs(dGF-dGB)-dDif4)+(abs(dBF-dBB)-dDif4)*(abs(dBF-dBB)-dDif4))/3;

				if (dDif4<80 && dVar<40)
				{

					if (dLF<90 || dLB<90)
					{
						//pDiffImg->imageData[iDifOffset] = 0;
						//*
						dDifCoef = 0.04;

						dMax = max(dGB, dBB);
						dCB1 = atan(dRB/dMax);
						dMax = max(dBB, dRB);
						dCB2 = atan(dGB/dMax);
						dMax = max(dRB, dGB);
						dCB3 = atan(dBB/dMax);

						dMax = max(dGF, dBF);
						dCF1 = atan(dRF/dMax);
						dMax = max(dBF, dRF);
						dCF2 = atan(dGF/dMax);
						dMax = max(dRF, dGF);
						dCF3 = atan(dBF/dMax);

						dDif1 = abs(dCB1-dCF1);
						dDif2 = abs(dCB2-dCF2);
						dDif3 = abs(dCB3-dCF3);

						if (dDif1<(dDifCoef+0.02) && dDif2<dDifCoef && dDif3<dDifCoef)
						{
							pDiffImg->imageData[iDifOffset] = 0;
						}
						//*/
					}
					else
					{
						//pDiffImg->imageData[iDifOffset] = 0;
						dDifCoef = 0.02;

						dMax = max(dGB, dBB);
						dCB1 = atan(dRB/dMax);
						dMax = max(dBB, dRB);
						dCB2 = atan(dGB/dMax);
						dMax = max(dRB, dGB);
						dCB3 = atan(dBB/dMax);

						dMax = max(dGF, dBF);
						dCF1 = atan(dRF/dMax);
						dMax = max(dBF, dRF);
						dCF2 = atan(dGF/dMax);
						dMax = max(dRF, dGF);
						dCF3 = atan(dBF/dMax);

						dDif1 = abs(dCB1-dCF1);
						dDif2 = abs(dCB2-dCF2);
						dDif3 = abs(dCB3-dCF3);

						if (dDif1<(dDifCoef+0.01) && dDif2<dDifCoef && dDif3<dDifCoef)
						{
							pDiffImg->imageData[iDifOffset] = 0;
						}
					}
				}
			}
		}
}



#ifdef CODEREF_OLDPROC
void Kings::Imaging::CObjectDetector::Process( IplImage* pFrame )
{
	bool bDetShadow = false;
	bool bProcWholeIllumChange = false;
	if( m_nFrmNum == 0)
	{
		m_nFrmNum++;

		m_pBkImg = cvCreateImage(cvSize(pFrame->width, pFrame->height), IPL_DEPTH_8U,1); // 存放背景图像(灰度)
		m_pFrImg = cvCreateImage(cvSize(pFrame->width, pFrame->height), IPL_DEPTH_8U,1); // 存放结果图像(灰度)
		m_pDifImg = cvCreateImage( cvSize(pFrame->width, pFrame->height), IPL_DEPTH_8U,1 );//存放差分结果
		m_pShadowMask = cvCreateImage( cvSize(pFrame->width, pFrame->height), IPL_DEPTH_8U,1 );//存放阴影模版
		m_pObjectMask = cvCreateImage( cvSize(pFrame->width, pFrame->height), IPL_DEPTH_8U,1 );//存放目标模版
		m_pCurImg = cvCreateImage(cvSize(pFrame->width, pFrame->height), IPL_DEPTH_8U,1);

		//Attention!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!注意//
		m_pBkImg->origin = m_pFrImg->origin = m_pCurImg->origin = m_pDifImg->origin = m_pShadowMask->origin = pFrame->origin;
		m_pObjectMask->origin = pFrame->origin;

		m_pBkMat = cvCreateMat(pFrame->height, pFrame->width, CV_32FC1);
		m_pBkMat_Pre = cvCreateMat( pFrame->height, pFrame->width, CV_32FC1 );
		m_pFrMat = cvCreateMat(pFrame->height, pFrame->width, CV_32FC1);
		m_pFrameMat = cvCreateMat(pFrame->height, pFrame->width, CV_32FC1);


		/////////////////////////////////////前一帧的信息///////////////////////////////
		m_pPreImg = cvCreateImage( cvSize(pFrame->width, pFrame->height), IPL_DEPTH_8U,1 );
		m_pPreMat = cvCreateMat( pFrame->height, pFrame->width, CV_32FC1 );
		m_pDifBetFrmsImg = cvCreateImage( cvSize(pFrame->width, pFrame->height), IPL_DEPTH_8U,1 );
		m_pDifBetFrmsMat = cvCreateMat( pFrame->height, pFrame->width, CV_32FC1 );
		m_pPreImg->origin = pFrame->origin;
		m_pDifBetFrmsImg->origin = pFrame->origin;


		//转化成单通道图像再处理(灰度)
		cvCvtColor(pFrame, m_pBkImg, CV_BGR2GRAY);
		cvCvtColor(pFrame, m_pFrImg, CV_BGR2GRAY);

		cvConvert(m_pFrImg, m_pFrameMat);
		cvConvert(m_pFrImg, m_pFrMat);
		cvConvert(m_pFrImg, m_pBkMat);
		cvCopy(m_pBkMat, m_pBkMat_Pre);



		cvCopy( m_pFrameMat, m_pPreMat );
		cvConvert( m_pPreMat, m_pPreImg );
	}
	else
	{
		// 隔帧处理
		if (m_pBufPreImg==0)
		{
			m_pBufPreImg = cvCreateImage( cvSize(pFrame->width, pFrame->height), IPL_DEPTH_8U,1 );
			m_pBufDifBetFrmsImg = cvCreateImage( cvSize(pFrame->width, pFrame->height), IPL_DEPTH_8U,1 );
			m_pBufPreMat = cvCreateMat(pFrame->height, pFrame->width, CV_32FC1);
			m_pBufDifBetFrmsMat = cvCreateMat(pFrame->height, pFrame->width, CV_32FC1);

			cvCvtColor(pFrame, m_pBufPreImg, CV_BGR2GRAY);
			cvConvert(m_pBufPreImg, m_pBufPreMat);

			return;
		}

		cvCvtColor(pFrame, m_pFrImg, CV_BGR2GRAY); //转化成单通道图像再处理(灰度)
		cvCvtColor(pFrame, m_pCurImg, CV_BGR2GRAY);
		cvConvert(m_pFrImg, m_pFrameMat);
		//高斯滤波先，以平滑图像
		//cvSmooth(m_pFrameMat, m_pFrameMat, CV_GAUSSIAN, 3, 0, 0);

		//当前帧跟背景图相减(求背景差并取绝对值)
		cvAbsDiff(m_pFrameMat, m_pBkMat, m_pFrMat);

		//二值化前景图(这里采用特定阈值进行二值化)
		//cvThreshold(m_pFrMat, m_pFrImg, 10, 255.0, CV_THRESH_BINARY);
		DynThreshold( m_pFrMat, m_pBkMat, m_pFrImg, 0.09, 10, 20 );

		//隔帧相减
		cvAbsDiff( m_pFrameMat, m_pPreMat, m_pDifBetFrmsMat );
		cvThreshold( m_pDifBetFrmsMat, m_pDifBetFrmsImg, 20, 255.0, CV_THRESH_BINARY);

		//隔帧差分
		cvAbsDiff( m_pFrameMat, m_pBufPreMat, m_pBufDifBetFrmsMat);
		cvThreshold( m_pBufDifBetFrmsMat, m_pBufDifBetFrmsImg, 20, 255.0, CV_THRESH_BINARY);
		CvMat* tmpMat= cvCreateMat( pFrame->height, pFrame->width, CV_32FC1);
		IplImage* tmpImg = cvCreateImage(cvSize(pFrame->width, pFrame->height), IPL_DEPTH_8U,1);
		cvAbsDiff(m_pBufPreMat, m_pBkMat, tmpMat);
		cvThreshold(tmpMat, tmpImg, 20, 255.0, CV_THRESH_BINARY);
		int bufdif = CountPts(tmpImg);
		cvReleaseImage(&tmpImg);
		cvReleaseMat(&tmpMat);

		cvCopy( m_pFrImg, m_pDifImg );
		m_pDifImg->origin = m_pFrImg->origin;
		int dif = CountPts(m_pDifImg);
		printf("current dif = %d\n", dif);

		float fThreRatio = 0.2;
		if( !bProcWholeIllumChange ) fThreRatio = 1.1;
		if(dif > m_pDifImg->width * m_pDifImg->height * fThreRatio )//出现过多的目标点0.3表示有30%的像素变化了。暂时写死
		{
			if(bufdif < m_pDifImg->width * m_pDifImg->height * 0.2)
			{
				cvCopy(m_pBufPreImg, m_pPreImg);
				cvConvert(m_pPreImg, m_pPreMat);
				cvCopy( m_pCurImg, m_pBufPreImg);
				cvConvert( m_pBufPreImg, m_pBufPreMat);
				return;
			}

			if( CountPts(m_pDifBetFrmsImg) < m_pDifBetFrmsImg->width * m_pDifBetFrmsImg->height * 0.01 )//帧间变化很小
			{
				cvRunningAvg(m_pFrameMat, m_pBkMat, 0.2, 0);//更新下背景取当前帧的90%更新进背景，暂时写死
			}
			else
			{
				float fIdRatio_x = 0.1;//Indent Ratio in x
				float fIdRatio_y = 0.05;//Indent Ratio in y
				CvRect rcImg = cvRect( (int)fIdRatio_x * pFrame->width, (int)fIdRatio_y * pFrame->height, 
					(int)( 1 - 2 * fIdRatio_x ) * pFrame->width,  (int)( 1 - 2 * fIdRatio_y ) * pFrame->height );
				//bool bObject = ShadowJudge(rcImg, m_pFrImg, m_pBkImg, m_pCurImg);//判断是否全局光照变化,true:判断为光照变化&&false:判断为火车经过
				bool bObject = false;//加入帧间和隔帧分析后，不再需要进行阴影判断，直接认为是火车经过导致的变化
				//if( m_bOjbMask )
				if( !bObject && m_bOjbMask )//火车经过引起变化
				{
					cvCopy( m_pBkMat, m_pFrameMat );//以背景帧作为当前帧，相当于不更新背景
				}
				else
				{
					//cvCopy( m_pFrameMat, m_pBkMat );//以当前帧作为背景
					cvRunningAvg(m_pFrameMat, m_pBkMat, m_fBkgUpRatio, 0);
				}
			}

			cvZero( m_pFrImg );
		}

		//进行形态学滤波，去掉噪音
		cvErode(m_pFrImg, m_pFrImg, 0, 1);
		cvDilate(m_pFrImg, m_pFrImg, 0, 1);
		int ith = 0;
		int nTimes = 0;
		for( ith = 0; ith < nTimes; ith++ )
		{
			cvDilate(m_pFrImg, m_pFrImg, 0, 1);
		}
		for( ith = 0; ith < nTimes; ith++ )
		{
			cvErode(m_pFrImg, m_pFrImg, 0, 1);
		}

		if( m_objcontour )
			cvClearSeq( m_objcontour);
		if( m_objRects )
			cvClearSeq( m_objRects );
		if( m_storageObjs )
			cvClearMemStorage( m_storageObjs );
		cvFindContours( m_pFrImg, m_storageObjs, &m_objcontour, sizeof(CvContour), CV_RETR_CCOMP, CV_CHAIN_APPROX_SIMPLE );
		cvZero( m_pFrImg );

		cvZero( m_pShadowMask );
		cvZero( m_pObjectMask );
		m_objRects = cvCreateSeq( 0, sizeof(CvSeq), sizeof(CvRect), m_storageObjs);
		CvSeq* pCurContour = m_objcontour;
		for( ; pCurContour; pCurContour = pCurContour->h_next )
		{
			CvRect r = ((CvContour*)pCurContour)->rect;
			if(r.height * r.width > 200) // 面积小的方形抛弃掉
			{
				bool bNonShadow = true;
				//////////////////////////////////////////////////////////////////////////Add Shadow Analyze
				bNonShadow = JudgeNonShadow( m_pDifImg, r );//Michael's shadow detect function
				//////////////////////////////////////////////////////////////////////////End -- Add Shadow Analyze	
				CvScalar color = CV_RGB( 255, 255, 255 );
				if( bNonShadow )
				{

					/* replace CV_FILLED with 1 to see the outlines */
					cvDrawContours( m_pFrImg, pCurContour, color, color, -1, CV_FILLED, 8 );

					bool bresult = ShadowJudge(r, m_pFrImg, m_pBkImg, m_pCurImg);
					if( !bDetShadow )
						bresult = false;
					if (bresult==false)//确认是目标
					{
						cvSeqPush( m_objRects, &r );
						cvDrawContours( m_pObjectMask, pCurContour, color, color, -1, CV_FILLED, 8 );
					}
					else//判断是阴影
					{
						cvDrawContours( m_pShadowMask, pCurContour, color, color, -1, CV_FILLED, 8 );
					}
				}


			}
		}

		RemoveIntersectedRects( m_objRects );//合并相交的框

		//cvCopy( m_pFrameMat, m_pBkMat, m_pShadowMask );//阴影进入背景

		//滑动平均更新背景(求平均)
		cvCopy( m_pBkMat, m_pBkMat_Pre );//背景更新前备份
		cvRunningAvg(m_pFrameMat, m_pBkMat, m_fBkgUpRatio, 0);
		if( m_bOjbMask )
			cvCopy( m_pBkMat_Pre, m_pBkMat, m_pObjectMask );//目标区域不进入背景
		//将背景转化为图像格式，用以显示
		cvConvert(m_pBkMat, m_pBkImg);

		// 保持原图像的旋转方向
		//m_pBkImg->origin = m_pFrImg->origin = pFrame->origin;

		//cvCopy( m_pFrameMat, m_pPreMat );//当前帧进前一帧
		//cvConvert( m_pPreMat, m_pPreImg );

		//隔帧
		cvCopy(m_pBufPreImg, m_pPreImg);
		cvConvert(m_pPreImg, m_pPreMat);
		cvCopy( m_pCurImg, m_pBufPreImg);
		cvConvert( m_pBufPreImg, m_pBufPreMat);

		//隔帧
		//cvCopy( m_pCurImg, m_pPreImg);
		//cvConvert( m_pPreImg, m_pPreMat);
	}
}
#endif
