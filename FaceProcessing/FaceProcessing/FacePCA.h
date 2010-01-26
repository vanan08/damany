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
	float similarity;//���ص����ƶȣ�Ϊ0---1֮���С��,Խ�ӽ�1��ʾ���ƶ�Խ��
	//char *fileName;//���ص������ƶȶ�Ӧ��ͼƬ����
	int index;//�ļ���������ָʾ�ڼ��е��ļ���
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
	//float *sampleAvgVal;//ָ������ƽ��ֵ��������
	//float *sampleEigenVector;//ָ������������������imgLen�У�imgLen��
	//float *sampleCoeff ;//ָ��������ͶӰϵ����sampleCount�У�eigenNum��
	//char *sampleFileName;//�����������ļ���
	int sampleCount;
	int imgLen;
	int eigenNum;
	CString rootPath;

	CvMat* avgVector;//ƽ��ֵ����
	CvMat* eigenVector;//Э����������������  
	CvMat* resCoeff;//ȡǰeigenNum���������ֵ

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


