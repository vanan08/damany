#include "stdafx.h"
#include "Illumination.h"

Damany::Imaging::FaceCompare::IlluminationNorm::IlluminationNorm(IplImage *refImg)
{
	refLight = cvCreateImage(cvGetSize(refImg), 8, refImg->nChannels);
	cvCopy(refImg, refLight);
}

Damany::Imaging::FaceCompare::IlluminationNorm::~IlluminationNorm()
{
	if (!refLight)
	{
		cvReleaseImage(&refLight);
	}
}

int Damany::Imaging::FaceCompare::IlluminationNorm::fSearchingMaxMin(const CvMat * mat, double * min, double * max)
{
	double temp = 0;
	double Min;
	double Max;
	(Min)=0;
	(Max)=0;

	float *p;
	int row,col;

	for(row = 0; row < mat->rows; row++)
	{
		p = mat->data.fl + row * (mat->step/4);
		for(col = 0; col < mat->cols; col++)
		{
			temp=(*p);
			if (temp>(Max))
				(Max)=temp;
			if (temp<(Min))
				(Min)=temp;
			temp=(*(p+1));
			if (temp>(Max))
				(Max)=temp;
			if (temp<(Min))
				(Min)=temp;
			temp=(*(p+2));
			if (temp>(Max))
				(Max)=temp;
			if (temp<(Min))
				(Min)=temp;

			p+=3;
		}
	}

	(*min)=(Min);
	(*max)=(Max);
	return 0;
}

void Damany::Imaging::FaceCompare::IlluminationNorm::
	Norm(IplImage *colorIn, IplImage *colorOut)
{
	IplImage * dstImg1 =cvCreateImage(cvGetSize(refLight ), IPL_DEPTH_8U,3);
	IplImage * dstImg2 =cvCreateImage(cvGetSize(colorIn ), IPL_DEPTH_8U,3);

	CvMat * dstM1 = cvCreateMat(dstImg1->height,dstImg1->width,CV_32FC3);
	CvMat * dstM2 = cvCreateMat(dstImg2->height,dstImg2->width,CV_32FC3);

	IplImage * scrMImg1 =cvCreateImage(cvGetSize(refLight ), IPL_DEPTH_32F,3);
	IplImage * scrMImg2 =cvCreateImage(cvGetSize(colorIn ), IPL_DEPTH_32F,3);

	IplImage * grayImg1 =cvCreateImage(cvGetSize(refLight ), IPL_DEPTH_32F,1);
	IplImage * grayImg2 =cvCreateImage(cvGetSize(colorIn ), IPL_DEPTH_32F,1);

	cvConvert(refLight,dstM1);
	cvConvert(colorIn,dstM2);

	double min1=0;
	double min2=0; 
	double max1=0;
	double max2=0;

	//fSearchingMaxMin(dstM1,&min1,&max1);
	//fSearchingMaxMin(dstM2,&min2,&max2);

	double m1,m2;

	m1 = 255;//max1;
	m2 = 255;//max2;

	cvConvertScale(dstM1,dstM1,1/m1,0);
	cvConvertScale(dstM2,dstM2,1/m2,0);

	cvConvert(dstM1,scrMImg1);
	cvConvert(dstM2,scrMImg2);

	cvCvtColor(scrMImg1,grayImg1,CV_BGR2GRAY);
	cvCvtColor(scrMImg2,grayImg2,CV_BGR2GRAY);

	CvScalar avg1,avg2;

	avg1 = cvAvg(grayImg1);
	avg2 = cvAvg(grayImg2);

	double gammar = log(avg1.val[0])/log(avg2.val[0]);

	cvPow(scrMImg2,scrMImg2,gammar);
	cvConvertScale(scrMImg2,dstImg2,255);
	cvCopy(dstImg2, colorOut);

	cvReleaseImage(&scrMImg1);
	cvReleaseImage(&dstImg1);
	cvReleaseImage(&grayImg1);
	cvReleaseMat(&dstM1);
	cvReleaseImage(&scrMImg2);
	cvReleaseImage(&dstImg2);
	cvReleaseImage(&grayImg2);
	cvReleaseMat(&dstM2);
}