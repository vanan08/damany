#pragma   once 

#ifdef _AFXDLL
#define DLL_API _declspec(dllexport)
#else
#define DLL_API _declspec(dllimport)
#endif


#include "afxwin.h"  
#include "stdafx.h"
#include "cv.h"
#include "cxcore.h"
#include "highgui.h"
#include "svm.h"
#include "iostream" 
#include "fstream"

using namespace std;


class DLL_API FaceSvm
{
public:
	FaceSvm();
	int GetBadGuySampleCount();
	int GetGoodGuySampleCount();
	void DelSvmDataFile();
	void WriteSvmAvgTxt(CvMat *AvgVector);
	void WriteSvmEigenVectorTxt(CvMat *EigenVectorFinal);
	void WriteSvmSamCoeffTxt(CvMat *resCoeff);
	void BallNormSvmResCoeff(CvMat *resCoeff);
	void WriteBallNormResCoeff(CvMat *resCoeff);
	void WriteLabel(int *label, int sampleCount);
	void WriteSvmInfo(int imgWidth, int imgHeight, int eigenNum, int sampleCount);
	void PCAforSVM(int imgWidth, int imgHeight, int eigenNum);
	void ReadInfoTxt(int &imgWidth, int &imgHeight, int &eigenNum, int &sampleCount);
	void GetLabel(struct svm_problem *prob, int labelNum);
	void GetProbX(struct svm_problem *prob, struct svm_node *x_space, int eigenNum);
	void GetSvmTrainData(struct svm_problem *prob, struct svm_node *x_space);
	void DefaultSvmParam(struct svm_parameter *param);
	void SwitchForSvmParma(struct svm_parameter *param, char ch, char *strNum, int nr_fold, int cross_validation);
	void SetSvmParam(struct svm_parameter *param, char *str, int cross_validation, int nr_fold);
	void ReadAvgTxt(CvMat *avgVector);
	void ReadEigVecTxt(CvMat *eigenVector);
	void BallNorm(CvMat *targetResult, float *currBallNorm);
	void PcaProject(float *currentFace, int sampleCount, int imgLen, int eigenNum, float *currBallNorm);
	void InitSvmData();//SVM数据初始化
	void SvmTrain(int imgWidth, int imgHeight, int eigenNum, char *option);//SVM训练函数
	double SvmPredict(float *currentFace);//SVM预测函数

private:
	CvMat *svmAvgVector;
	CvMat *svmEigenVector;  
	svm_model* testModel;
	int svmImgWidth;
	int svmImgHeight;
	int svmImgLen;
	int svmEigenNum;
	int svmSampleCount;
};