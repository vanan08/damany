// FacePCAC.cpp : Defines the exported functions for the DLL application.
//
#pragma once

#ifdef DLL_EXPORTS
#define DLL_API _declspec(dllexport)
#else 
#define DLL_API _declspec(dllimport)
#endif

#include "stdafx.h" 
#include "afxwin.h"
#include "cv.h"
#include "highgui.h"
#include "cxcore.h" 
#include "iostream"
#include "fstream"


using namespace std;

struct similarityMat
{
	float similarity;//返回的相似度，为0---1之间的小数,越接近1表示相似度越高
	//char *fileName;//返回的与相似度对应的图片名称
	int index;//文件名索引，指示第几行的文件名
};

class DLL_API FacePCA
{

public:
	FacePCA(const char* path); 
	~FacePCA();
	void FaceTraining(int imgWidth=100, int imgHeight=100, int eigenNum=40);
	void Load();
	void FaceRecognition(float currentFace[], similarityMat*& similarity, int& count);
	CString GetFileName(int index); 


private:
	//float *sampleAvgVal;//指向样本平均值，列向量
	//float *sampleEigenVector;//指向样本的特征向量，imgLen行，imgLen列
	//float *sampleCoeff ;//指向样本的投影系数，sampleCount行，eigenNum列
	//char *sampleFileName;//定义样本的文件名
	int sampleCount;
	int imgLen;
	int eigenNum;
	CString rootPath;

	CvMat* avgVector;//平均值向量
	CvMat* eigenVector;//协方差矩阵的特征向量  
	CvMat* resCoeff;//取前eigenNum个最大特征值

	CString* sampleFileName; 
	//char* sampleFileName;


private:
	int GetTrainSampleCount();
	CString GetConfigFile();
	CString GetPCAProfileString(const CString& name);
	void WritePCAProfileString(const CString& name, const CString& val);
	int GetPCAProfileInt(const CString& name);
	void WritePCAProfileInt(const CString& name, int val);
	CString GetPCAroot();
	inline CString GetTrainPath();
	inline CString GetSampleCoefficientFilePath();
	inline CString GetEigenVectorFilePath();
	inline CString GetAverageValueFilePath();
	inline CString GetFileNameFilePath();
	void DelFile(CString filePath);
	void DelPCADataFile();
	void WriteMatTxtFile(CString path, CvMat* mat);
	void WriteFileName(CString path, char* fileName);
	void ReadMatTxtFile(CString path, CvMat* mat);
	void ReleaseMemory();
	void ReadFileName(CString* str, int count);
};


