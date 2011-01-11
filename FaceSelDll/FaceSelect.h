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

	//Michael Add -- 设置类变量的接口
	void SetROI( int x, int y, int width, int height );//如果不调用本函数设置检测区，检测区即为全图
	void SetFaceParas( int iMinFace, double dFaceChangeRatio = 5.0f );//设置脸的大小，iMinFace:最小的脸宽，dFaceChangeRatio：最大脸宽与最小脸宽之间的比例
	void SetDwSmpRatio( double dRatio );//设置降采样率, 如果希望将图像降采样处理，dRatio应小于1.0，该参数默认为0.5，即找脸时将图像长宽均变为原来的0.5倍，以节约搜索时间
	void SetOutputDir( const char* dir );//设置图片输出文件夹，调用前请先创建该文件夹

	//SetExRatio : 设置输出的图片应在人脸的四个方向扩展多少比例
	//如果人脸框大小保持不变，4个值都应该为0.0f
	void SetExRatio( double topExRatio, double bottomExRatio, double leftExRatio, double rightExRatio );
	void SetLightMode(int iMode);

private:
	CvSeq* FaceDetect( IplImage* pImage, CvMemStorage* pStorage, CvSize rcMinSize, bool bScale, double dScale );
	//输入img为待检测人脸图像，应包括RGB三分量
	//输出faces为检测到得人脸框，可能为多个
	//获取单个人脸框代码：CvRect* r = (CvRect*)cvGetSeqElem( faces, i );

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

	double m_dSglBkgProportion; //可能出现的单边背景区域
	double m_dFaceProportion;	//脸部区域在直方图中最少所占的比重
	bool m_bScaleFaceDetection; //脸部检测中是否开启缩放
	double m_dScale; //缩放比例
	double m_dForeheadNoise; //平抑不对称的额发引起的噪声Obsolete
	double m_dUpBkgNoise; //平抑不对称的额发引起的噪声
	double m_dSideBkgNoise; //平抑两侧背景引起的噪声Obsolete
	double m_dEarNoise; //平抑耳朵可能引起的噪声
	int m_iFaceSize; //最小脸部大小
	int m_iSFactorValv; //判断真假脸的阈值
	int m_iSizeWeight; //尺寸权重
	int m_iLightCondition; //光线环境 0-顺光 1-逆光 2-强逆光

	//多人寻优
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
	//缩放图像: imgOrg为原始图，imgDes为缩放后的图
	//ATTENTION　:　imgDes在函数内进行内存分配
	//缩放关系：
	//imgDes->width = imgOrg->width * dScale
	//imgDes->height = imgOrg->height * dScale
	//ATTENTION	:	imgDes必须释放！！！

	bool GetOrgRect( CvRect* rcOrg, const CvRect* rcScale, double dScale );
	//与函数resizeImg配套
	//rcDes为目标框在imgDes图像中的位置
	//本函数将imgDes中的位置rcDes，转化为在imgOrg图像中的位置rcOrg

	bool GetOrgRectSeq( CvSeq* orgFaces, const CvSeq* scaleFaces, double dScale );
	//与函数GetOrgRect相对应

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
	IplImage* GetSubImage(IplImage* pOriImage, CvRect roi); /*子图像在函数内创建用完要手动释放*/
	void FaceJudge(IplImage *pImage, double &dEquFactor, int &iSFactor, double &dFaceSize, double &dSVariant, double &dVVariant, double &dHVariant, double &dContrast, double &dVarient); /*脸部图像探测*/
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

	double m_dFaceChangeRatio;//最大脸与最小脸的尺度比例关系
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
	void AddInFrame(Frame frame);//依次添加一组图片
	int SearchFaces(Target** targets);//一组添加完后，调用这个函数，返回脸数目	
	void ReleaseTargets()//显示调用释放内存
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

