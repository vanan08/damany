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

CvMat *svmAvgVector;//SVM进行PCA投影所用的平均值向量
CvMat *svmEigenVector;//SVM进行PCA投影所用的协方差矩阵的特征向量   
struct svm_model* testModel; //SVM预测用的结构体

extern "C"
{
	void EXPORT SvmTrain(int imgWidth, int imgHeight, int eigenNum, char *option);//SVM的训练函数，对SVM训练样本，该函数训练SVM所需的model
	double EXPORT SvmPredict(float *currentFace);//SVM的预测函数，对于待识别的每一张人脸，该函数返回“1"表示“坏人”，返回“-1”表示“好人”
	void EXPORT InitSvmData(int imgLen, int eigenNum);//该函数加载SVM预测函数所需的相关数据，在SvmTrain()函数调用之后，该函数只需调用一次即可
}