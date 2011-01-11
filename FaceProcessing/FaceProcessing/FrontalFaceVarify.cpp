#include "stdafx.h"
#include "FrontalFaceVarify.h"

Damany::Imaging::FrontalFaceVarify::FrontalFaceVarify(IplImage* templateEye)
{
	//Im1=cvLoadImage(templatePath, 0); 
	Im1 = cvCloneImage(templateEye); 
}

Damany::Imaging::FrontalFaceVarify::~FrontalFaceVarify()
{
	cvReleaseImage(&Im1); 
}

bool Damany::Imaging::FrontalFaceVarify::IsFrontal(IplImage *smallFaceImg)
{
	IplImage* img = cvCreateImage(cvGetSize(smallFaceImg), 8, 1);
	cvCvtColor(smallFaceImg, img, CV_BGR2GRAY);

	IplImage* grayImg = cvCreateImage(cvSize(200, 200), 8, 1);    
	cvResize(img, grayImg, CV_INTER_CUBIC);

	IplImage* im1=0;  //image template 模板图像
	IplImage* im2=0;  //source image   用于检查证明脸的目标图像
	int Front,T;      //Front，是否是正面脸的标志，是为1，T自动检测出的阈值，是中间变量
	float t1,t2,t3;   //分别是左右眼中心横坐标偏移阈值，纵坐标偏移阈值，和眼睛宽长比比阈值，用于判断是否倾斜、侧脸和低抬头
	Front=0;

	//IplImage* Im1=cvLoadImage("template.jpg",0);

	int im2tw=80; 
	int im2th=80; 
	int im1tw=(Im1->width*im2tw)/grayImg->width;
	int im1th=(Im1->height*im2th)/grayImg->height; 

	im2=cvCreateImage(cvSize(im2tw, im2th),IPL_DEPTH_8U,1);
	im1=cvCreateImage(cvSize(im1tw, im1th),IPL_DEPTH_8U,1);

	float kw=float(im2tw)/float(grayImg->width);
	float kh=float(im2th)/float(grayImg->height);

	t1=0.1;
	t2=1.1;
	t3=4.1;

	cvResize(Im1,im1);
	cvResize(grayImg,im2);

	IplImage * Tresult=cvCreateImage(cvSize(im2->width,im1->height),IPL_DEPTH_8U,1);//检查到的眼部区域图像
	IplImage * eye=cvCreateImage(cvSize(im2->width,im1->height),IPL_DEPTH_8U,1);    //检测到的眼部区域二值化后图像，黑色为眼睛

	tmp(im1, im2, Tresult);//模板与目标图像做相关运算的函数
	Threshold(Tresult, &T);//对检测到的眼部区域求出阈值T，为二值化检测眼睛做准备
	eye=cvCloneImage(Tresult);
	Detection(Tresult, eye, &T,kw, kh);//检测出眼睛区域，用eye图像表示
	FrontFace(eye,t1,t2,t3,&Front);//检测出是否正脸，Front等于1为正脸

	bool flag = IsSymmetry(grayImg); 

	cvReleaseImage(&eye);
	cvReleaseImage(&im1);  
	cvReleaseImage(&im2);
	cvReleaseImage(&Tresult);
	cvReleaseImage(&grayImg);
	cvReleaseImage(&img); 

	if (Front == 1)
	{ 
		if (flag)
		{
			return true;
		}
	}
	return false; 	
}

bool Damany::Imaging::FrontalFaceVarify::RectSymmetry(IplImage* colorImg)
{
	IplImage* img = cvCreateImage(cvGetSize(colorImg), 8, 3);
	cvFlip(colorImg, img, 1); 

	IplImage* grayImg1 = cvCreateImage(cvGetSize(img), 8, 1);
	IplImage* grayImg2 = cvCreateImage(cvGetSize(img), 8, 1);
	cvCvtColor(colorImg, grayImg1, CV_BGR2GRAY);
	cvCvtColor(img, grayImg2, CV_BGR2GRAY);

	int height = grayImg2->height;
	int width = grayImg2->width; 
	int step = grayImg2->widthStep;
	uchar* data1 = (uchar*)grayImg1->imageData;
	uchar* data2 = (uchar*)grayImg2->imageData;

	int score = 0;
	for (int i=height/5; i<height*4/5; i++)
	{
		for (int j=width/5; j<width*4/5; j++)
		{
			if (abs(data1[i*step+j] - data2[i*step+j]) < 5)
			{
				score++;
			}
		}
	}

	float ratio = float(score*9)/float(width*height*25);

	cvReleaseImage(&img);
	cvReleaseImage(&grayImg1);
	cvReleaseImage(&grayImg2); 

	if (ratio < 0.02)
	{
		return false;
	}
	return true;
}

int Damany::Imaging::FrontalFaceVarify::plotbox(IplImage* Tresult, IplImage* target, IplImage* templat, CvMat* M)
{
	int r1=target->height;
	int c1=target->width;

	int r2=templat->height;
	int c2=templat->width;

	float fmaxM=0;
	int iRmaxM, iCmaxM;

	//求出M的最大值坐标iRmaxM和iCmaxM，该坐标所对应的区域即为眼部区域
	for (int i=0;i<M->rows;i++)
	{
		for (int j=0;j<M->cols;j++)
		{
			if ((((float*)(M->data.ptr + M->step*i))[j])>fmaxM)
			{
				fmaxM=((float*)(M->data.ptr + M->step*i))[j];
				iRmaxM=i;
				iCmaxM=j;
			}
		}
	}

	for (int ii=0;ii<(Tresult->height);ii++)
	{			
		for (int jj=0;jj<(Tresult->width);jj++)
		{
			((uchar*)(Tresult->imageData + Tresult->widthStep*(ii)))[(jj)]=
				((uchar*)(target->imageData + target->widthStep*(ii+iRmaxM)))[(jj)];

		}
	}
	//对眼部区域Tresult的左右两侧和上下两边的一定宽度区域至于白色，用以消除干扰，提高眼睛定位的准确性
	for (int i=0;i<Tresult->height;i++)
	{
		for (int j=0;j<16;j++)
			((uchar*)(Tresult->imageData+i*Tresult->widthStep))[j]=((uchar)255);
	}
	for (int i=0;i<Tresult->height;i++)
	{
		for (int j=(Tresult->width-16);j<Tresult->width;j++)
			((uchar*)(Tresult->imageData+i*Tresult->widthStep))[j]=((uchar)255);
	}
	for (int i=0;i<3;i++)
	{
		for (int j=0;j<Tresult->width;j++)
			((uchar*)(Tresult->imageData+i*Tresult->widthStep))[j]=((uchar)255);
	}
	for (int i=(Tresult->height-3);i<Tresult->height;i++)
	{
		for (int j=0;j<Tresult->width;j++)
			((uchar*)(Tresult->imageData+i*Tresult->widthStep))[j]=((uchar)255);
	}

	return 0;
}

int Damany::Imaging::FrontalFaceVarify::tmp(IplImage* templat, IplImage* target, IplImage* Tresult)
{
	IplImage* image22=cvCreateImage(cvSize(templat->width,templat->height),IPL_DEPTH_8U,1);
	int r1=target->height;
	int c1=target->width;

	int r2=templat->height;
	int c2=templat->width;

	CvScalar Scalar1;
	Scalar1 = cvAvg(templat);

	for (int ii=0;ii<(r2);ii++)
	{			
		for (int jj=0;jj<(c2);jj++)
		{
			if (((((uchar*)(templat->imageData + templat->widthStep*(ii)))[(jj)])-(uchar)(Scalar1.val[0]))<0)
				((uchar*)(image22->imageData + image22->widthStep*(ii)))[(jj)] = 0;
			else
				((uchar*)(image22->imageData + image22->widthStep*(ii)))[(jj)] = 
				((uchar*)(templat->imageData + templat->widthStep*(ii)))[(jj)]-(uchar)(Scalar1.val[0]);
		}
	}

	CvMat* M=cvCreateMat((r1-r2+1),(c1-c2+1),CV_32FC1);//模板与目标图像的相关矩阵，最大值处开始的区域为眼睛区域

	CvScalar Scalar2;
	float t,a1,a2,corr;
	for(int i=0;i<(r1-r2+1);i++)
	{
		for (int j=0;j<(c1-c2+1);j++)
		{
			CvMat* Nimage=cvCreateMat(r2,c2,CV_8UC1);
			for (int ii=0;ii<(r2);ii++)
			{			
				for (int jj=0;jj<(c2);jj++)
				{
					((uchar*)(Nimage->data.ptr + Nimage->step*ii))[jj] = 
						((uchar*)(target->imageData + target->widthStep*(ii+i)))[(jj+j)];
				}
			}

			Scalar2 = cvAvg(Nimage);
			for (int ii=0;ii<(r2);ii++)
			{			
				for (int jj=0;jj<(c2);jj++)
				{
					if ((((uchar*)(Nimage->data.ptr + Nimage->step*ii))[jj]-(uchar)(Scalar2.val[0]))<0)
						((uchar*)(Nimage->data.ptr + Nimage->step*ii))[jj] = 0;
					else
						((uchar*)(Nimage->data.ptr + Nimage->step*ii))[jj] = 
						((uchar*)(Nimage->data.ptr + Nimage->step*ii))[jj]-(uchar)(Scalar2.val[0]);
				}
			}

			//求点（i，j）的相关系数t
			corr=0;
			for (int ii=0;ii<(r2);ii++)
			{			
				for (int jj=0;jj<(c2);jj++)
				{
					corr = float((((uchar*)(Nimage->data.ptr + Nimage->step*ii))[jj])*
						(((uchar*)(image22->imageData + image22->widthStep*(ii)))[(jj)]))+corr;
				}
			}

			a1=a2=0.0;
			for (int ii=0;ii<(r2);ii++)
			{			
				for (int jj=0;jj<(c2);jj++)
				{
					a1=a1+float((((uchar*)(Nimage->data.ptr + Nimage->step*ii))[jj])*
						(((uchar*)(Nimage->data.ptr + Nimage->step*ii))[jj]));
					a2=a2+float((((uchar*)(image22->imageData + image22->widthStep*(ii)))[(jj)])*
						(((uchar*)(image22->imageData + image22->widthStep*(ii)))[(jj)]));
				}
			}
			t=corr/(sqrt(a1)*sqrt(a2));
			((float*)(M->data.ptr + M->step*i))[j] = (float)(t);

			cvReleaseMat(&Nimage);
		}
	}

	plotbox( Tresult,  target,  templat, M);//由相关矩阵M，求出目标图像的眼睛区域Tresult

	cvReleaseImage(&image22);
	cvReleaseMat(&M);
	return 0;
}

int Damany::Imaging::FrontalFaceVarify::Threshold(IplImage *Tresult, int *T)
{
	int iHalfWidth = Tresult->width/2;
	(*T)=255;

	//先把眼部区域分为左右眼区域，分别求出每一个区域的最小值，当作左右眼，再取最小值的最大值的1.02倍作为整个眼部区域的阈值，低于该阈值置为0，高于置为1，求出眼睛的二值化图
	for (int i = 0; i <Tresult->height ;i++)
	{
		for (int j = 0; j< iHalfWidth; j++) 
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

int Damany::Imaging::FrontalFaceVarify::Detection(IplImage *Tresult, IplImage *eye, int *T, float kw, float kh)
{
	//眼睛的二值化图eye
	for (int i=0;i<Tresult->height;i++)
	{
		for (int j=0; j<Tresult->width;j++)
		{
			if (((uchar*)(Tresult->imageData+i*Tresult->widthStep))[j]<=((uchar)(*T)))
				((uchar*)(eye->imageData+i*eye->widthStep))[j]=((uchar)0);
			else
				((uchar*)(eye->imageData+i*eye->widthStep))[j]=((uchar)255);
		}
	}
	//为了消除鼻梁侧阴影的影响，鼻梁两侧一定宽度的区域在二值化图中置为255
	for (int i=0;i<eye->height;i++)
	{
		for (int j=(eye->width/2-int(13*kw));j<(eye->width/2+int(13*kw));j++)
			((uchar*)(eye->imageData+i*eye->widthStep))[j]=((uchar)255);
	}

	return 0;
}

int Damany::Imaging::FrontalFaceVarify::FrontFaceCenter(IplImage *P, int *center_x, int *center_y, float *column_row_k)
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

int Damany::Imaging::FrontalFaceVarify::FrontFace(IplImage *eye, float t1, float t2, float t3, int *Front)
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
			((uchar*)(eyeright->imageData+eyeleft->widthStep*i))[j]=
				((uchar*)(eye->imageData+eye->widthStep*i))[eye->width-j-1];
		}
	}
	//分别求出左右侧眼睛图像中的左右眼区域坐标和宽长比
	FrontFaceCenter(eyeleft,&center_x_left,&center_y_left,&column_row_k_left);
	FrontFaceCenter(eyeright,&center_x_right,&center_y_right,&column_row_k_right);
	//判断是否歪斜，否为1
	if ((abs(center_x_left-center_x_right)<t1)&&(center_x_left!=1)&&(center_x_right!=1))
	{
		front1=1;
	}
	//判断是否侧脸，否为1
	if ((abs(center_y_left-center_y_right)<t2)&&(center_y_left!=1)&&(center_y_right!=1))
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

bool Damany::Imaging::FrontalFaceVarify::IsSymmetry(IplImage *grayImg)
{
	IplImage* grayImg2 = cvCreateImage(cvGetSize(grayImg), 8, 1);
	cvFlip(grayImg, grayImg2, 1); 

	int height = grayImg->height;
	int width = grayImg->width;
	int step = grayImg->widthStep;
	uchar* data1 = (uchar*)grayImg->imageData;
	uchar* data2 = (uchar*)grayImg2->imageData;

	int score = 0;
	for (int i=0; i<height; i++)
	{
		for (int j=0; j<width; j++)
		{
			if (abs(data1[i*step+j] - data2[i*step+j]) < 8)
			{
				score++;
			}
		}
	}

	cvReleaseImage(&grayImg2); 

	if (score < height*width*9/40) 
	{
		return false; 
	}
	return true;
}