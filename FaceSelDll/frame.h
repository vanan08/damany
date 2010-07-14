#ifndef _FACESEL_IF_DATASTRUCT_H_
#define _FACESEL_IF_DATASTRUCT_H_

#include <windows.h>

#define GUI_LEN 16

struct Frame
{
	IplImage *image;//cv ת�����ͼƬ
	CvRect searchRect;//�������ķ�Χ
	BYTE guid[GUI_LEN];
};



struct Target
{
	Frame BaseFrame;//��ͼƬ
	int FaceCount;//������
	IplImage** FaceData;//������
	CvRect* FaceRects;//����Ӧ���λ��,20090827 Added for Record Face Positions
	CvRect* FaceOrgRects;//���������ο���չǰ��λ��,20090929 Added for Face Recognition Purpose
};

struct ImageArray
{
	int nImageCount;
	IplImage** imageArr;
};
#endif