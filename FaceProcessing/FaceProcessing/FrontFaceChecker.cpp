// frontalFaceDetect.cpp : Defines the exported functions for the DLL application.
/*****************************************************************************************************
Copyritht (c) 成都丹玛尼科技有限公司
All right reserved！

作    者：杜欣宇 薛晓利
完成日期：2010年1月19日
当前版本:1.0

摘    要：本程序，利用加载的人眼图片，对归一化后的200*200像素人脸图片进行正面人脸检测。若当前图片为
          正面人脸，则返回true,否则，返回false
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

	//求出M的最大值坐标iRmaxM和iCmaxM，该坐标所对应的区域即为眼部区域
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
	//对眼部区域Tresult的左右两侧和上下两边的一定宽度区域至于白色，用以消除干扰，提高眼睛定位的准确性
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

	CvMat* M=cvCreateMat((targetHeight-templateHeight+1),(targetWidth-templateWidth+1),CV_32FC1);//模板与目标图像的相关矩阵，最大值处开始的区域为眼睛区域

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

			//求点（i，j）的相关系数t
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

	PlotBox(Tresult, target, templat, M);//由相关矩阵M，求出目标图像的眼睛区域Tresult

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
	IplImage* eyeleft= cvCreateImage(cvSize(iHalfWidth, eye->height), IPL_DEPTH_8U,1);//左侧眼睛区域图像
	IplImage* eyeright=cvCreateImage(cvSize(iHalfWidth, eye->height), IPL_DEPTH_8U,1);//镜像的右侧眼睛区域图像
	for(int i=0;i<eye->height;i++)
	{
		for (int j=0;j<iHalfWidth;j++)
		{
			((uchar*)(eyeleft->imageData+eyeleft->widthStep*i))[j]=((uchar*)(eye->imageData+eye->widthStep*i))[j];
			((uchar*)(eyeright->imageData+eyeleft->widthStep*i))[j]=((uchar*)(eye->imageData+eye->widthStep*i))[eye->width-j-1];
		}
	}
	//分别求出左右侧眼睛图像中的左右眼区域坐标和宽长比
	FrontFaceCenter(eyeleft,&center_x_left,&center_y_left,&column_row_k_left);
	FrontFaceCenter(eyeright,&center_x_right,&center_y_right,&column_row_k_right);
	//判断是否歪斜，否为1
	if (abs(center_x_left-center_x_right)<t1)
	{
		front1=1;
	}
	//判断是否侧脸，否为1
	if (abs(center_y_left-center_y_right)<t2)
	{
		front2=1;
	}
	//判断是否低抬头，否为1
	if (((column_row_k_left<t3)&&(column_row_k_right<t3))&&((column_row_k_left>0)&&(column_row_k_right>0)))
	{	
		front3=1;
	}
	//以上三个条件的综合，正脸为1
	(*Front)=front1*front2*front3;
	cvReleaseImage(&eyeleft);
	cvReleaseImage(&eyeright);

	return 0;
}

int frontalFaceDetect::FrontFaceCenter(IplImage* P,int* center_x,int* center_y, float* column_row_k)
{
	int iRow1,iRow2,iCol1,iCol2;
	iRow1=iRow2=iCol1=iCol2=1;

	//对求出的二值化图中的眼睛区域精确定位
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
	(*center_x)=(iRow1+iRow2)/2;  //眼睛区域中心的横坐标
	(*center_y)=(iCol1+iCol2)/2;  //眼睛区域中心的纵坐标
	(*column_row_k)=abs(((float)(iCol1-iCol2))/((float)(iRow1-iRow2))); //眼睛区域的宽长比
	if ((iCol1==iCol2)||(iRow1==iRow2))
		(*column_row_k)=1.0;

	return 0;
}

int frontalFaceDetect::Detection(IplImage* Tresult, IplImage* eye, int* T)
{
	//眼睛的二值化图eye
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
	//为了消除鼻梁侧阴影的影响，鼻梁两侧一定宽度的区域在二值化图中置为255
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


	//先把眼部区域分为左右眼区域，分别求出每一个区域的最小值，当作左右眼，再取最小值的最大值的1.02倍作为整个眼部区域的阈值，低于该阈值置为0，高于置为1，求出眼睛的二值化图
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
	Tresult=cvCreateImage(cvSize(targetNormFace->width, targetNormFace->height),IPL_DEPTH_8U,1);//检查到的眼部区域图像
	eye=cvCreateImage(cvSize(targetNormFace->width, targetNormFace->height),IPL_DEPTH_8U,1); 

	tmp(eyeTemplate, img, Tresult);//模板与目标图像做相关运算的函数
	Threshold(Tresult, &T);//对检测到的眼部区域求出阈值T，为二值化检测眼睛做准备
	eye=cvCloneImage(Tresult);
	Detection(Tresult, eye, &T);//检测出眼睛区域，用eye图像表示
	FrontFace(eye,t1,t2,t3,&Front);//检测出是否正脸，Front等于1为正脸  

	if (Front == 1)//如果判断为正面人脸 
	{
		return true;
	}
	else
	{
		return false; 
	}
}