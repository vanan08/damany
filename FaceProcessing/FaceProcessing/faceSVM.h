#pragma   once 

#ifdef DLL_EXPORTS
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
#include "FaceSVMParameters.h"

using namespace std;


class DLL_API FaceSvm
{
public:
	FaceSvm(const char* path);
	void Load();//SVM数据初始化
	void SvmTrain();
	void SvmTrain(FaceSVMParameters parameters);//SVM训练函数
	double SvmPredict(float *currentFace);//SVM预测函数

private:
	FaceSVMParameters ReadConfigInfo();
	CString GetSVMProfileString(const CString& name);
	void WriteSVMProfileString(const CString& name, const CString& val);
	int GetSVMProfileInt(const CString& name);
	void WriteSVMProfileInt(const CString& name, int val);
	int GetBadGuySampleCount();
	int GetGoodGuySampleCount();
	int GetFileCount(CString path, CString pattern);
	void DelFile(CString filePath);
	void DelSvmDataFile();
	CString GetConfigFile();

	inline CString GetSampleCoefficientFilePath();
	inline CString GetBallNormFilePath();
	inline CString GetLabelFilePath();
	inline CString GetEigenVectorFilePath();
	inline CString GetAverageVauleFilePath();
	inline CString GetModelFilePath(); 

	CString GetSVMroot();
	CString GetBadGuyPath();
	CString GetGoodGuyPath();
	void WriteSvmAvgTxt(CvMat *AvgVector);
	void WriteSvmEigenVectorTxt(CvMat *EigenVectorFinal);
	void WriteSvmSamCoeffTxt(CvMat *resCoeff);
	void BallNormSvmResCoeff(CvMat *resCoeff);
	void WriteBallNormResCoeff(CvMat *resCoeff);
	void WriteLabel(int *label, int sampleCount);
	void WriteSvmInfo(int imgWidth, int imgHeight, int eigenNum, int sampleCount);
	void PCAforSVM(int imgWidth, int imgHeight, int eigenNum);
	int GetSampleCount();

	void GetLabel(struct svm_problem *prob, int labelNum);
	void GetProbX(struct svm_problem *prob, struct svm_node *x_space, int eigenNum);
	void GetSvmTrainData(struct svm_problem *prob, struct svm_node *x_space);
	void DefaultSvmParam(struct svm_parameter *param);
	void SwitchForSvmParma(struct svm_parameter *param, char ch, char *strNum, int nr_fold, int cross_validation);
	void SetSvmParam(struct svm_parameter *param, const char *str, int cross_validation, int nr_fold);
	void ReadAvgTxt(CvMat *avgVector);
	void ReadEigVecTxt(CvMat *eigenVector);
	void BallNorm(CvMat *targetResult, float *currBallNorm);
	void PcaProject(float *currentFace, int sampleCount, int imgLen, int eigenNum, float *currBallNorm); 

	CvMat *svmAvgVector;
	CvMat *svmEigenVector;  
	svm_model* testModel;
	CString rootPath;
	FaceSVMParameters parameters;
};