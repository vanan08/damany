#pragma once

#ifdef DLL_EXPORTS
#define DLL_API _declspec(dllexport)
#else
#define DLL_API _declspec(dllimport)
#endif

#include "cv.h"
#include <highgui.h>
#include "frame.h"

#define MAXLEN_OUTDIR 128
#define MAXLEN_FNAME 128
#define MAXLEN_PATH ( MAXLEN_OUTDIR + MAXLEN_FNAME )
#define MAXLEN_RESSTR 512
#define MAXINIMG	100

class DLL_API CFaceSelect
{
public:
	CFaceSelect(void);
	virtual ~CFaceSelect(void);

	//Michael Add -- ����������Ľӿ�
	void SetROI( int x, int y, int width, int height );//��������ñ��������ü�������������Ϊȫͼ
	void SetFaceParas( int iMinFace, double dFaceChangeRatio = 5.0f );//�������Ĵ�С��iMinFace:��С������dFaceChangeRatio�������������С����֮��ı���
	void SetDwSmpRatio( double dRatio );//���ý�������, ���ϣ����ͼ�񽵲�������dRatioӦС��1.0���ò���Ĭ��Ϊ0.5��������ʱ��ͼ�񳤿����Ϊԭ����0.5�����Խ�Լ����ʱ��
	void SetOutputDir( const char* dir );//����ͼƬ����ļ��У�����ǰ���ȴ������ļ���

	//SetExRatio : ���������ͼƬӦ���������ĸ�������չ���ٱ���
	//����������С���ֲ��䣬4��ֵ��Ӧ��Ϊ0.0f
	void SetExRatio( double topExRatio, double bottomExRatio, double leftExRatio, double rightExRatio );
	void SetLightMode(int iMode);

private:
	CvSeq* FaceDetect( IplImage* pImage, CvMemStorage* pStorage, CvSize rcMinSize, bool bScale, double dScale );
	//����imgΪ���������ͼ��Ӧ����RGB������
	//���facesΪ��⵽�������򣬿���Ϊ���
	//��ȡ������������룺CvRect* r = (CvRect*)cvGetSeqElem( faces, i );

	CvMemStorage* m_cvSeqStorage;
	CvSeq* m_cvImageSeq;
	CvHaarClassifierCascade* m_cascade_face;
	CvMemStorage *m_casMem_face;
	CvSeq* m_cvSeqFaceSeq;
	CvMemStorage* m_cvSeqFaceSeqStorage;
	CvSeq* m_cvSeqFaceStorage;
	CvMemStorage* m_cvSeqFaceStorageStorage;
	CvSeq* m_cvRealFaceSeq;
	CvMemStorage* m_cvRealFaceSeqStorage;

	double m_dSglBkgProportion; //���ܳ��ֵĵ��߱�������
	double m_dFaceProportion;	//����������ֱ��ͼ��������ռ�ı���
	bool m_bScaleFaceDetection; //����������Ƿ�������
	double m_dScale; //���ű���
	double m_dForeheadNoise; //ƽ�ֲ��ԳƵĶ���������Obsolete
	double m_dUpBkgNoise; //ƽ�ֲ��ԳƵĶ���������
	double m_dSideBkgNoise; //ƽ�����౳�����������Obsolete
	double m_dEarNoise; //ƽ�ֶ���������������
	int m_iFaceSize; //��С������С
	int m_iSFactorValv; //�ж����������ֵ
	int m_iSizeWeight; //�ߴ�Ȩ��
	int m_iLightCondition; //���߻��� 0-˳�� 1-��� 2-ǿ���

	//����Ѱ��
	CvMemStorage *m_pDiffRectStorage;
	CvMemStorage *m_pDiffJudgeStorage;
	CvSeq *m_sqDiffFaceRect;
	CvSeq *m_sqDiffFaceJudge;
	CvMemStorage *m_pHistStorage;
	CvSeq *m_sqHist;
	CvMemStorage *m_pImgNoStorage;
	CvSeq *m_sqImgNo;

	void MultiObjectInitialize();
	void MultiObjectRelease();
	bool HistCompare(CvHistogram *pHistA, CvHistogram *pHistB, CvHistogram *pHist_a, CvHistogram*pHist_b);
	void EnlargeCompareRect(CvRect *rect, CvSize ImgSz, CvRect *RectOut);


	void ResizeImg( IplImage* imgOrg, IplImage* &imgDes, double dScale );
	//����ͼ��: imgOrgΪԭʼͼ��imgDesΪ���ź��ͼ
	//ATTENTION��:��imgDes�ں����ڽ����ڴ����
	//���Ź�ϵ��
	//imgDes->width = imgOrg->width * dScale
	//imgDes->height = imgOrg->height * dScale
	//ATTENTION	:	imgDes�����ͷţ�����

	bool GetOrgRect( CvRect* rcOrg, const CvRect* rcScale, double dScale );
	//�뺯��resizeImg����
	//rcDesΪĿ�����imgDesͼ���е�λ��
	//��������imgDes�е�λ��rcDes��ת��Ϊ��imgOrgͼ���е�λ��rcOrg

	bool GetOrgRectSeq( CvSeq* orgFaces, const CvSeq* scaleFaces, double dScale );
	//�뺯��GetOrgRect���Ӧ

	bool InitCascade( const char* cascade_name, CvHaarClassifierCascade* &cascade, CvMemStorage *casMem );
	void ReleaseCascade( CvHaarClassifierCascade* &cascade, CvMemStorage *casMem );
	void ReleaseImageSeq();
	void ReleaseFaceSeq();
	void ReleaseStorageSeq();
	void AddInFaceSeq(CvSeq **SeqFace, CvMemStorage **MemFace);
	//int GetEffectiveFaceHeight(IplImage *pImage, double dSizeValv, char cValue, int iLenth);
	int GetEffectiveFaceHeightBKG(IplImage *pImage, double dSizeValv, char cValue);
	void GetEffectiveFaceWidth(IplImage *pImage, double dSizeValv, char cValue, int &iOffL, int &iLenth);
	void SpecialEqualizeHist( const CvArr* src, int iMaxIdx, float fMaxVal, CvArr* dst );
	void RyanDebug(const char *cFileName);
	void EyeFaceTest(const char *cFileName);
	void FaceTest(const char *cFileName);
	void CompareTest(const char *cFileName);
	void SkinTest(const char *cFileName);
	void SkinTestShow(const char *cFileName);
	void SkinFilter(IplImage *pImage, IplImage *pGrayImage, int iMaxIdx, bool bRmvBkg, IplImage *pBiImg);
	void MultiObjectJudge(CvSeq *sqFrmRect, CvSeq *sqFrmJudge, int iFrmNo);
	int ContrastJudge(IplImage *imgGray, float &fMaxVal, int &iMaxIdx, double dFold);
	void ColorImageEnhance(IplImage *pImage);
	void StepLengthen(IplImage *pImage, double dFactorL, double dFactorH);
	void EnhanceTest(const char *cFileName);
	bool NeedEnhance(IplImage *pImage);
	void EyeMouthTest( const char *cFileName );
	void BRGtoHSV(IplImage* pBGRImg, IplImage*& pHImg, IplImage*& pSImg, IplImage*& pVImg);
public:
	bool AddInImage( IplImage *pImg, CvRect roi );
	void AddInImage(const char* strFileName);
	void ShowAllImage(int iIntervalTime);
	void TestFaceJudge();
	void DebugTest(const char *cFileName);
	char* SelectBestImage( int outputMode = 0 );
private:
	IplImage* GetSubImage(IplImage* pOriImage, CvRect roi); /*��ͼ���ں����ڴ�������Ҫ�ֶ��ͷ�*/
	void FaceJudge(IplImage *pImage, double &dEquFactor, int &iSFactor, double &dFaceSize, double &dSVariant, double &dVVariant, double &dHVariant, double &dContrast, double &dVarient); /*����ͼ��̽��*/
	int GetHistRange(CvHistogram *pHist, int iMaxRange, double dMaxPix, int &iLower, int &iUpper, double dPortion);
	int GetHistRangeSingle(CvHistogram *pHist, int iMaxRange, double dMaxPix, int iUpper, int &iLower, double dPortion);
	double GetFaceHoriEquality(IplImage* pImage);
	double CountPixels(IplImage *pImage, char cVal);
	void DeNoise(IplImage* pImage, int iW, int iH, int iWO, int iHO);
	void DrawHistImage(CvHistogram *pHist, int iMaxRange, double dMaxPix);
	void DrawHistImage_H(CvHistogram *pHist, int iMaxRange, double dMaxPix);
	void CalcHistVariences(CvHistogram* pHist, int iMaxRange, double &dMean, double &dVarient);
	bool CheckOverExpo( IplImage *pSrcImg );

////////////////Michael Add Eye-Mouth Analysis////////////////////////////////////////////////////

	bool rateFace( IplImage* imgFace, double& dConf );

	CvHaarClassifierCascade* m_cascade_eye;//left eye
	CvHaarClassifierCascade* m_cascade_reye;
	CvHaarClassifierCascade* m_cascade_mouth;
	CvMemStorage *m_casMem_eye;
	CvMemStorage *m_casMem_reye;
	CvMemStorage *m_casMem_mouth;

	CvMemStorage* m_storage_subfacefeature;

	bool initSubFaceFeatureCascade( );
	void releaseSubFaceFeatureCascade( );
	bool getAllSubFeatures( IplImage* imgFace, CvSeq* leyes, CvSeq* reyes, CvSeq* mouths );
	CvSeq* getSubFaceFeature( IplImage* imgFace, const int rgnBound[], CvHaarClassifierCascade* cascade,
		CvMemStorage* storage, CvSize min_size );
	bool faceSubfeaturesRate( CvSeq* leyes, CvSeq* reyes, CvSeq* mouths, double &dConf, CvSeq* subFaceFeature );
	bool transferLocalRc2SceneRc( CvRect* rcInScene, CvRect* rcLocal, CvRect* rcRgn, double dScale );

	void CodeDebugTest_Michael(const char *cFileName);
////////////////End--Michael Add Eye-Mouth Analysis/////////////////////////////////////////////////

////////////////yuki Add Upperbody Analysis//////////////////////////////////////////////////////////////////////////

	CvSeq* UpperbodyDetect( IplImage* pImage,  CvMemStorage* pStorage, CvSize rcMinSize, bool bScale, double dScale);

	CvHaarClassifierCascade* m_cascade_upperbody;

	CvMemStorage* m_storage_upperbody;

	bool initUpperbodyCascade( );
	void releaseUpperbodyCascade( );
	void GetUpperbody( CvRect* pImage, CvSize rcSize, CvRect* pUpperbody);
	void MergeResultRectSeq( CvSeq* cvResultRectSeq, CvSeq* cvResultRectFactorSeq, CvRect* rcResultFace, double* dFactor );
	int Compare2Rect( CvRect* rcFace, CvRect* rcFaceInSeq );

////////////////End--Yuki Add Upperbody Analysis////////////////////////////////////////////

///////////////////////Michael Add Some New Properties/////////////////////////////////
private:
	void InitClass();//Michael Add 20090507

	double m_dFaceChangeRatio;//���������С���ĳ߶ȱ�����ϵ
	CvRect m_rcROI;
	char m_outputDir[MAXLEN_OUTDIR];
	char m_resString[MAXLEN_RESSTR];
	double m_ExRatio_t, m_ExRatio_b, m_ExRatio_l, m_ExRatio_r;

	void ClearImgSeq();
	void ClearImageSeq();
	void ClearFaceSeq();
	void ClearStorageSeq();
	bool RemoveNoiseByBkgAna( );
	void DrawFacesInImgSeq( const char* prefix, const char* postfix );
	bool m_bDebugForBkgAna;

//////////////////End -- Michael Add Some New Properties//////////////////////



//20090716 Defined Interface
public:
	void AddInFrame(Frame frame);//�������һ��ͼƬ
	int SearchFaces(Target** targets);//һ�������󣬵��������������������Ŀ	
	void ReleaseTargets()//��ʾ�����ͷ��ڴ�
	{
		ReleaseTargets( m_targets, m_nTotalValidImages ); 
	}

private:
	CvSeq* m_cvFrameSeq;
	Target* m_targets;
	int m_nTotalValidImages;
	void ReleaseTargets( Target* &targets, int &nCnt );
//End -- 20090716 Defined Interface


//20090929 Add for Face Recognition Research
public:
	bool FaceImagePreprocess( IplImage* imgIn, IplImage* &imgNorm, CvRect roi = cvRect(0,0,0,0) );
	bool FaceImagePreprocess_ForTrain( IplImage* imgIn, ImageArray &normImages, CvRect roi = cvRect(0,0,0,0) );
	void ReleaseImageArray( ImageArray &images )
	{
	}
	void DrawRectCenter( IplImage* img, CvRect roi );
	bool SubImageRotate_Ver1( IplImage* src, CvRect roi, IplImage* &dst, double angle );//has some problem, may rotate more angle
	bool SubImageRotate( IplImage* src, CvRect roi, IplImage* &dstImage, float radians );//Rotate Clockwisely
	IplImage* Rotate( IplImage* src , float radians );
	bool SearchLREyes( CvSeq* leyes, CvSeq* reyes, CvRect* &leye_best, CvRect* &reye_best );
	bool CheckImageROI( IplImage* img, CvRect roi );
	void FaceReDraw(IplImage *pImage, CvRect *rRealFace);
//End -- 20090929 Add for Face Recognition Research

private:
	bool JudgeFaceByColor( IplImage* pImg );
};

