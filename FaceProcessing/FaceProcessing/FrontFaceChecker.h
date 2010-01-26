#pragma once

#ifdef DLL_EXPORTS
#define DLL_API _declspec(dllexport)
#else
#define DLL_API _declspec(dllimport)
#endif

#include "stdafx.h"
#include "cv.h"
#include "cxcore.h"
#include "highgui.h" 


class DLL_API frontalFaceDetect
{
public:
	frontalFaceDetect();
	~frontalFaceDetect();
	int LoadEyeTemplate(const char* eyeImgAdd);//�����۾�ģ��ͼƬ
	bool IsFrontFace(IplImage* targetNormFace);//���ش�ʶ�������ͼƬ������������⣬����


private: 
	void PlotBox(IplImage* Tresult, IplImage* target, IplImage* templat, CvMat* M);
	int tmp(IplImage* templat, IplImage* target, IplImage* Tresult);
	int Threshold(IplImage* Tresult, int* T);
	int FrontFace(IplImage* eye, float t1, float t2, float t3, int* Front);
	int FrontFaceCenter(IplImage* P,int* center_x,int* center_y, float* column_row_k);
	int Detection(IplImage* Tresult, IplImage* eye, int* T);

private:
	IplImage* eyeTemplate;//�۾�ģ��ͼ��
	IplImage* img;//��ǰ��Ҫ�ж�����������ͼƬ
	int Front,T;      //Front���Ƿ����������ı�־����Ϊ1��T�Զ���������ֵ�����м����
	float t1,t2,t3;   //�ֱ������������ĺ�����ƫ����ֵ��������ƫ����ֵ�����۾����ȱ���ֵ��
	//�����ж��Ƿ���б�������͵�̧ͷ
	IplImage* Tresult;
	IplImage* eye;



};

