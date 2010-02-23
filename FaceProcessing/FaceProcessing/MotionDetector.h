// The following ifdef block is the standard way of creating macros which make exporting 
// from a DLL simpler. All files within this DLL are compiled with the PREPROCESS_EXPORTS
// symbol defined on the command line. this symbol should not be defined on any project
// that uses this DLL. This way any other project whose source files include this file see 
// PREPROCESS_API functions as being imported from a DLL, whereas this DLL sees symbols
// defined with this macro as being exported.
#pragma once

#ifdef DLL_EXPORTS
#define DLL_API _declspec(dllexport)
#else
#define DLL_API _declspec(dllimport)
#endif


#include "highgui.h"
#include "cv.h"
#include "omp.h"

#include "Frame.h"

namespace FaceProcessing
{

	class DLL_API CMotionDetector
	{
	public:
		CMotionDetector()
		{
			this->firstFrmRec = false;
			this->secondFrmRec = false;
			xLeftAlarm = 100; //���岢��ʼ��������������������λ��
			yTopAlarm = 400;
			xRightAlarm = 600;
			yBottomAlarm = 500;  

			minLeftX = 3000;//���岢��ʼ���������������λ��
			minLeftY = 3000; 
			maxRightX = 0;
			maxRightY = 0;

			faceCount = 500; //�������ֵ
			groupCount = 5;//����ͼƬ����
			signelCount = 0;//��¼��ǰ�����е�ͼƬ����

			drawAlarmArea = false;//��־�Ƿ񻭳���������
			drawRect = false; //��־�Ƿ񻭿�
		}

		bool PreProcessFrame(Frame frame, Frame &lastFrame);
		void SetDrawRect(bool draw);
		void SetAlarmArea(const int leftX, const int leftY, const int rightX, const int rightY, bool draw);
		void SetRectThr(const int fCount, const int gCount);


	private:
		void FindRectX(IplImage *img, const int leftY, const int rightY);
		void FindRectY(IplImage *img, const int leftX, const int rightX);



		Frame prevFrame ;//�洢��һ֡��frame    

		bool firstFrmRec;//��һ֡�Ƿ��յ�
		bool secondFrmRec;//�ڶ�֡�Ƿ��յ�

		IplImage *currImg;//��ǰ֡��ͼƬ
		IplImage *lastGrayImg;//��һ֡�Ҷ�ͼ
		IplImage *lastDiffImg;//��һ֡���ͼ�Ķ�ֵ��ͼ 

		int xLeftAlarm; //���岢��ʼ��������������������λ��
		int yTopAlarm;
		int xRightAlarm;
		int yBottomAlarm;  

		int minLeftX;//���岢��ʼ���������������λ��
		int minLeftY; 
		int maxRightX;
		int maxRightY;

		int faceCount; //�������ֵ
		int groupCount;//����ͼƬ����
		int signelCount;//��¼��ǰ�����е�ͼƬ����

		bool drawAlarmArea;//��־�Ƿ񻭳���������
		bool drawRect; //��־�Ƿ񻭿�
	};

}
