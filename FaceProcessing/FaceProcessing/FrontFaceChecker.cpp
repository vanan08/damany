// frontalFaceDetect.cpp : Defines the exported functions for the DLL application.
/*****************************************************************************************************
Copyritht (c) �ɶ�������Ƽ����޹�˾
All right reserved��

��    �ߣ������� Ѧ����
������ڣ�2010��1��19��
��ǰ�汾:1.0

ժ    Ҫ�����������ü��ص�����ͼƬ���Թ�һ�����200*200��������ͼƬ��������������⡣����ǰͼƬΪ
          �����������򷵻�true,���򣬷���false
******************************************************************************************************/
#include "stdafx.h"
#include "FrontFaceChecker.h" 



frontalFaceDetect::frontalFaceDetect()
{
	eyeTemplate = NULL;
	img = cvCreateImage(cvSize(200, 200), 8, 1);
	Front=0; 
	t1=4.0;
	t2=5.0;
	t3=4.1;
	Tresult = NULL;
	eye = NULL;
}

frontalFaceDetect::~frontalFaceDetect()
{
	if (eyeTemplate != NULL)
	{
		cvReleaseImage(&eyeTemplate);
	}
	if (img != NULL)
	{
		cvReleaseImage(&img);
	}
	if (Tresult != NULL)
	{
		cvReleaseImage(&Tresult);
	}
	if (eye != NULL)
	{
		cvReleaseImage(&eye);
	}
}

int frontalFaceDetect::LoadEyeTemplate(const char* eyeImgAdd)
{
	eyeTemplate = cvLoadImage(eyeImgAdd, CV_LOAD_IMAGE_GRAYSCALE);
	VERIFY(eyeTemplate != NULL);
	if (eyeTemplate)
	{
		return 1;
	}
	else
	{
		return 0;
	}
}

void frontalFaceDetect::PlotBox(IplImage* Tresult, IplImage* target, IplImage* templat, CvMat* M)
{
	float fmaxM=0.0;
	int iRmaxM, iCmaxM;

	//���M�����ֵ����iRmaxM��iCmaxM������������Ӧ������Ϊ�۲�����
	double min_val = 0.0;
	double max_val = 0.0;
	CvPoint min_pt;
	CvPoint max_pt;
	cvMinMaxLoc(M, &min_val, &max_val, &min_pt, &max_pt, NULL);
	iRmaxM = max_pt.x;
	iCmaxM = max_pt.y;

	for (int ii=0;ii<(Tresult->height);ii++)
	{			
		for (int jj=0;jj<(Tresult->width);jj++)
		{
			((uchar*)(Tresult->imageData + Tresult->widthStep*(ii)))[(jj)]=((uchar*)(target->imageData + target->widthStep*(ii+iRmaxM)))[(jj)];

		}
	}
	//���۲�����Tresult������������������ߵ�һ������������ڰ�ɫ�������������ţ�����۾���λ��׼ȷ��
	for (int i=0;i<Tresult->height;i++)
	{
		for (int j=0;j<16;j++)
			((uchar*)(Tresult->imageData+i*Tresult->widthStep))[j] = 255;
	}
	for (int i=0;i<Tresult->height;i++)
	{
		for (int j=(Tresult->width-16);j<Tresult->width;j++)
			((uchar*)(Tresult->imageData+i*Tresult->widthStep))[j] = 255;
	}
	for (int i=0;i<3;i++)
	{
		for (int j=0;j<Tresult->width;j++)
			((uchar*)(Tresult->imageData+i*Tresult->widthStep))[j] = 255;
	}
	for (int i=(Tresult->height-3);i<Tresult->height;i++)
	{
		for (int j=0;j<Tresult->width;j++)
			((uchar*)(Tresult->imageData+i*Tresult->widthStep))[j] = 255;
	}
}

int frontalFaceDetect::tmp(IplImage* templat, IplImage* target, IplImage* Tresult)
{
	IplImage* image22=cvCreateImage(cvSize(templat->width,templat->height),IPL_DEPTH_8U,1);
	int targetHeight = target->height;
	int targetWidth = target->width;

	int templateHeight = templat->height; 
	int templateWidth = templat->width;

	//mean value of templat
	CvScalar Scalar1;
	Scalar1 = cvAvg(templat);

	//image22=templat-mean(templat)
	for (int ii=0;ii<(templateHeight);ii++)
	{			
		for (int jj=0;jj<(templateWidth);jj++)
		{
			if (((((uchar*)(templat->imageData + templat->widthStep*(ii)))[(jj)])-(uchar)(Scalar1.val[0]))<0)
				((uchar*)(image22->imageData + image22->widthStep*(ii)))[(jj)] = 0;
			else
				((uchar*)(image22->imageData + image22->widthStep*(ii)))[(jj)] = ((uchar*)(templat->imageData + templat->widthStep*(ii)))[(jj)]-(uchar)(Scalar1.val[0]);
		}
	}

	CvMat* M=cvCreateMat((targetHeight-templateHeight+1),(targetWidth-templateWidth+1),CV_32FC1);//ģ����Ŀ��ͼ�����ؾ������ֵ����ʼ������Ϊ�۾�����

	CvScalar Scalar2;
	float t,a1,a2,corr;
	for(int i=0;i<(targetHeight-templateHeight+1);i++)
	{
		for (int j=0;j<(targetWidth-templateWidth+1);j++)
		{
			CvMat* Nimage=cvCreateMat(templateHeight,templateWidth,CV_8UC1);
			//Nimage=target;
			for (int ii=0;ii<(templateHeight);ii++)
			{			
				for (int jj=0;jj<(templateWidth);jj++)
				{
					((uchar*)(Nimage->data.ptr + Nimage->step*ii))[jj] = ((uchar*)(target->imageData + target->widthStep*(ii+i)))[(jj+j)];
				}
			}

			Scalar2 = cvAvg(Nimage);
			//Nimage=target-mean(target);
			for (int ii=0;ii<(templateHeight);ii++)
			{			
				for (int jj=0;jj<(templateWidth);jj++)
				{
					if ((((uchar*)(Nimage->data.ptr + Nimage->step*ii))[jj]-(uchar)(Scalar2.val[0]))<0)
						((uchar*)(Nimage->data.ptr + Nimage->step*ii))[jj] = 0;
					else
						((uchar*)(Nimage->data.ptr + Nimage->step*ii))[jj] = ((uchar*)(Nimage->data.ptr + Nimage->step*ii))[jj]-(uchar)(Scalar2.val[0]);
				}
			}

			//��㣨i��j�������ϵ��t
			corr=0;
			for (int ii=0;ii<(templateHeight);ii++)
			{			
				for (int jj=0;jj<(templateWidth);jj++)
				{
					corr = float((((uchar*)(Nimage->data.ptr + Nimage->step*ii))[jj])*(((uchar*)(image22->imageData + image22->widthStep*(ii)))[(jj)]))+corr;
				}
			}

			a1=a2=0.0;
			for (int ii=0;ii<(templateHeight);ii++)
			{			
				for (int jj=0;jj<(templateWidth);jj++)
				{
					a1=a1+float((((uchar*)(Nimage->data.ptr + Nimage->step*ii))[jj])*(((uchar*)(Nimage->data.ptr + Nimage->step*ii))[jj]));
					a2=a2+float((((uchar*)(image22->imageData + image22->widthStep*(ii)))[(jj)])*(((uchar*)(image22->imageData + image22->widthStep*(ii)))[(jj)]));
				}
			}
			t=corr/(sqrt(a1)*sqrt(a2));

			((float*)(M->data.ptr + M->step*i))[j] = (float)(t);

			cvReleaseMat(&Nimage);
		}

	}

	PlotBox(Tresult, target, templat, M);//����ؾ���M�����Ŀ��ͼ����۾�����Tresult

	cvReleaseImage(&image22); 
	cvReleaseMat(&M);
	return 0;
}

int frontalFaceDetect::FrontFace(IplImage* eye, float t1, float t2, float t3, int* Front)
{
	int iHalfWidth, center_x_left,center_y_left,center_x_right,center_y_right;
	float column_row_k_left,column_row_k_right;
	int front1,front2,front3;
	front1=front2=front3=0;
	iHalfWidth = eye->width/2;
	IplImage* eyeleft= cvCreateImage(cvSize(iHalfWidth, eye->height), IPL_DEPTH_8U,1);//����۾�����ͼ��
	IplImage* eyeright=cvCreateImage(cvSize(iHalfWidth, eye->height), IPL_DEPTH_8U,1);//������Ҳ��۾�����ͼ��
	for(int i=0;i<eye->height;i++)
	{
		for (int j=0;j<iHalfWidth;j++)
		{
			((uchar*)(eyeleft->imageData+eyeleft->widthStep*i))[j]=((uchar*)(eye->imageData+eye->widthStep*i))[j];
			((uchar*)(eyeright->imageData+eyeleft->widthStep*i))[j]=((uchar*)(eye->imageData+eye->widthStep*i))[eye->width-j-1];
		}
	}
	//�ֱ�������Ҳ��۾�ͼ���е���������������Ϳ���
	FrontFaceCenter(eyeleft,&center_x_left,&center_y_left,&column_row_k_left);
	FrontFaceCenter(eyeright,&center_x_right,&center_y_right,&column_row_k_right);
	//�ж��Ƿ���б����Ϊ1
	if (abs(center_x_left-center_x_right)<t1)
	{
		front1=1;
	}
	//�ж��Ƿ��������Ϊ1
	if (abs(center_y_left-center_y_right)<t2)
	{
		front2=1;
	}
	//�ж��Ƿ��̧ͷ����Ϊ1
	if (((column_row_k_left<t3)&&(column_row_k_right<t3))&&((column_row_k_left>0)&&(column_row_k_right>0)))
	{	
		front3=1;
	}
	//���������������ۺϣ�����Ϊ1
	(*Front)=front1*front2*front3;
	cvReleaseImage(&eyeleft);
	cvReleaseImage(&eyeright);

	return 0;
}

int frontalFaceDetect::FrontFaceCenter(IplImage* P,int* center_x,int* center_y, float* column_row_k)
{
	int iRow1,iRow2,iCol1,iCol2;
	iRow1=iRow2=iCol1=iCol2=1;

	//������Ķ�ֵ��ͼ�е��۾�����ȷ��λ
	for (int i=0;i<P->height;i++)
	{
		for (int j=0;j<P->width;j++)
		{
			if (((uchar*)(P->imageData+P->widthStep*i))[j]==0)
			{
				iRow1=i;
				break;
			}
		}
	}
	for (int i=P->height-1;i>=0;i--)
	{
		for (int j=0;j<P->width;j++)
		{
			if (((uchar*)(P->imageData+P->widthStep*i))[j]==0)
			{
				iRow2=i;
				break;
			}
		}
	}
	for (int j=0;j<P->width;j++)
	{
		for (int i=0;i<P->height;i++)
		{
			if (((uchar*)(P->imageData+sizeof(uchar)*(P->height)*j))[i]==0)
			{
				iCol1=j;
				break;
			}
		}
	}
	for (int j=P->width-1;j>=0;j--)
	{
		for (int i=0;i<P->height;i++)
		{
			if (((uchar*)(P->imageData+sizeof(uchar)*(P->height)*j))[i]==0)
			{
				iCol2=j;
				break;
			}
		}
	}
	(*center_x)=(iRow1+iRow2)/2;  //�۾��������ĵĺ�����
	(*center_y)=(iCol1+iCol2)/2;  //�۾��������ĵ�������
	(*column_row_k)=abs(((float)(iCol1-iCol2))/((float)(iRow1-iRow2))); //�۾�����Ŀ���
	if ((iCol1==iCol2)||(iRow1==iRow2))
		(*column_row_k)=1.0;

	return 0;
}

int frontalFaceDetect::Detection(IplImage* Tresult, IplImage* eye, int* T)
{
	//�۾��Ķ�ֵ��ͼeye
	for (int i=0;i<Tresult->height;i++)
	{
		for (int j=0; j<Tresult->width;j++)
		{
			if (((uchar*)(Tresult->imageData+i*Tresult->widthStep))[j]<((uchar)(*T)))
				((uchar*)(eye->imageData+i*eye->widthStep))[j] = 0;
			else
				((uchar*)(eye->imageData+i*eye->widthStep))[j] = 255;
		}
	}
	//Ϊ��������������Ӱ��Ӱ�죬��������һ����ȵ������ڶ�ֵ��ͼ����Ϊ255
	for (int i=0;i<eye->height;i++)
	{
		for (int j=(eye->width/2-13);j<(eye->width/2+13);j++)
			((uchar*)(eye->imageData+i*eye->widthStep))[j] = 255;
	}

	return 0;
}

int frontalFaceDetect::Threshold(IplImage* Tresult, int* T)
{
	int iHalfWidth = Tresult->width/2;

	(*T)=255;


	//�Ȱ��۲������Ϊ���������򣬷ֱ����ÿһ���������Сֵ�����������ۣ���ȡ��Сֵ�����ֵ��1.02����Ϊ�����۲��������ֵ�����ڸ���ֵ��Ϊ0��������Ϊ1������۾��Ķ�ֵ��ͼ
	for (int i = 0; i < iHalfWidth;i++)
	{
		for (int j = 0; j< Tresult->height; j++)
		{


			uchar tleft, tright;
			tleft=((uchar*)(Tresult->imageData+Tresult->widthStep*i))[j];
			tright=((uchar*)(Tresult->imageData+Tresult->widthStep*i))[(Tresult->width-1-j)];
			uchar t;
			if (tleft<tright)
				t=tright;
			else
				t=tleft;
			if (t<(*T))
				(*T)=t;
		}
	}
	(*T) = (int)((*T)* 1.2);

	return 0;
}

bool frontalFaceDetect::IsFrontFace(IplImage* targetNormFace)
{
	cvCopy(targetNormFace, img);
	Tresult=cvCreateImage(cvSize(targetNormFace->width, targetNormFace->height),IPL_DEPTH_8U,1);//��鵽���۲�����ͼ��
	eye=cvCreateImage(cvSize(targetNormFace->width, targetNormFace->height),IPL_DEPTH_8U,1); 

	tmp(eyeTemplate, img, Tresult);//ģ����Ŀ��ͼ�����������ĺ���
	Threshold(Tresult, &T);//�Լ�⵽���۲����������ֵT��Ϊ��ֵ������۾���׼��
	eye=cvCloneImage(Tresult);
	Detection(Tresult, eye, &T);//�����۾�������eyeͼ���ʾ
	FrontFace(eye,t1,t2,t3,&Front);//�����Ƿ�������Front����1Ϊ����  

	if (Front == 1)//����ж�Ϊ�������� 
	{
		return true;
	}
	else
	{
		return false; 
	}
}