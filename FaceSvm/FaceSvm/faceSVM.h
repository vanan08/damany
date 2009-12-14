#ifdef FACESVM_EXPORTS
#define FACESVM_API __declspec(dllexport)
#else
#define FACESVM_API __declspec(dllimport)
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

CvMat *svmAvgVector;//SVM����PCAͶӰ���õ�ƽ��ֵ����
CvMat *svmEigenVector;//SVM����PCAͶӰ���õ�Э����������������   
struct svm_model* testModel; //SVMԤ���õĽṹ��

extern "C"
{
	void EXPORT SvmTrain(int imgWidth, int imgHeight, int eigenNum, char *option);//SVM��ѵ����������SVMѵ���������ú���ѵ��SVM�����model
	double EXPORT SvmPredict(float *currentFace);//SVM��Ԥ�⺯�������ڴ�ʶ���ÿһ���������ú������ء�1"��ʾ�����ˡ������ء�-1����ʾ�����ˡ�
	void EXPORT InitSvmData(int imgLen, int eigenNum);//�ú�������SVMԤ�⺯�������������ݣ���SvmTrain()��������֮�󣬸ú���ֻ�����һ�μ���
}