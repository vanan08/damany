#include "stdafx.h"
#include "FaceAlignment.h"

Damany::Imaging::FaceCompare::FaceAlignment::FaceAlignment(char* modelPath, char* classifierPath, int featurePointCount)
{
	model.ReadModel(modelPath);
	facedet.LoadCascade(classifierPath);
	fptCount = featurePointCount;
}

bool Damany::Imaging::FaceCompare::FaceAlignment::LibFaceAlignment(IplImage *faceImg, 
	IplImage *faceLbpImg, CvPoint featurePt[])
{
	bool flag = Alignment(faceImg, faceLbpImg, featurePt);
	return flag;
}

bool Damany::Imaging::FaceCompare::FaceAlignment::RealTimeAlignment(IplImage *faceImg, 
	IplImage *faceLbpImg)
{
	bool flag = Alignment(faceImg, faceLbpImg);
	return flag;
}

int fSearchingMaxMin(const CvMat * mat, double * min, double * max)
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

void LightNorm(IplImage *sourImg, IplImage *refImg, IplImage *destImg)
{
	IplImage * dstImg1 =cvCreateImage(cvGetSize(refImg ), IPL_DEPTH_8U,3);
	IplImage * dstImg2 =cvCreateImage(cvGetSize(sourImg ), IPL_DEPTH_8U,3);

	CvMat * dstM1 = cvCreateMat(dstImg1->height,dstImg1->width,CV_32FC3);
	CvMat * dstM2 = cvCreateMat(dstImg2->height,dstImg2->width,CV_32FC3);

	IplImage * scrMImg1 =cvCreateImage(cvGetSize(refImg ), IPL_DEPTH_32F,3);
	IplImage * scrMImg2 =cvCreateImage(cvGetSize(sourImg ), IPL_DEPTH_32F,3);

	IplImage * grayImg1 =cvCreateImage(cvGetSize(refImg ), IPL_DEPTH_32F,1);
	IplImage * grayImg2 =cvCreateImage(cvGetSize(sourImg ), IPL_DEPTH_32F,1);

	cvConvert(refImg,dstM1);
	cvConvert(sourImg,dstM2);

	double min1=0;
	double min2=0; 
	double max1=0;
	double max2=0;

	fSearchingMaxMin(dstM1,&min1,&max1);
	fSearchingMaxMin(dstM2,&min2,&max2);

	double m1,m2;

	m1=max1;
	m2=max2;

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

	cvCopy(dstImg2, destImg);

	cvReleaseImage(&scrMImg1);
	cvReleaseImage(&dstImg1);
	cvReleaseImage(&grayImg1);
	cvReleaseMat(&dstM1);
	cvReleaseImage(&scrMImg2);
	cvReleaseImage(&dstImg2);
	cvReleaseImage(&grayImg2);
	cvReleaseMat(&dstM2);
}

void Draw(IplImage *sourImg, CvPoint fpt[])
{
	for (int i=0; i<68; i++)
	{
		cvCircle(sourImg, fpt[i], 2, CV_RGB(255,0,0)); 
	}
}

bool Damany::Imaging::FaceCompare::FaceAlignment::fptCheck(CvPoint fPt[], 
	int fptCount, int imgWidth, int imgHeight)
{
	CvRect rect;
	int minX, minY, maxX, maxY;
	minX = MinX(fPt, fptCount);
	minY = MinY(fPt, fptCount);
	maxX = MaxX(fPt, fptCount);
	maxY = MaxY(fPt, fptCount);
	minX = minX<(imgWidth-1) ? minX:(imgWidth-2);
	minY = minY<(imgHeight-1) ? minY:(imgHeight-2);
	maxX = maxX>minX ? maxX:(minX+1);
	maxY = maxY>minY ? maxY:(minY+1);

	rect.x = minX;
	rect.y = minY;
	rect.width = maxX - minX;
	rect.height = maxY - minY;  

	if ((rect.width<50) || (rect.height<50))
	{
		return false;
	}

	int xLimit = rect.x + rect.width/2;
	/*for (int i=0; i<fptCount; i++)
	{
	cout<<i<<"	"<<fPt[i].x<<" "<<fPt[i].y<<endl;
	}*/
/////////////////////////左边脸////////////////////////////////////////////
	if (fPt[0].x > xLimit)
	{
		return false;
	}
	if (fPt[1].x > xLimit)
	{
		return false;
	}
	if (fPt[2].x > xLimit)
	{
		return false;
	}
	if (fPt[3].x > xLimit)
	{
		return false;
	}
///////////////////////////右边脸//////////////////////////////////////////
	if (fPt[11].x < xLimit)
	{
		return false;
	}
	if (fPt[12].x < xLimit)
	{
		return false;
	}
	if (fPt[13].x < xLimit)
	{
		return false;
	}
	if (fPt[14].x < xLimit)
	{
		return false;
	}
////////////////////////眼睛、眉毛、鼻子根部//////////////////////////////////////////////////	
	int yLimit = rect.y + rect.height/2;
	for (int i=15; i<38; i++)
	{
		if (fPt[i].y > yLimit)
		{
			return false;
		}
	}
	if (fPt[45].y > yLimit)
	{
		return false;
	}
/////////////////////////////////////////////////////////////////////////////////
	for (int i=5; i<9; i++)
	{
		if (fPt[i].y < yLimit)
		{
			return false;
		}
	}
////////////////////////////////////////////////////////////////////////////////
	if (fPt[0].y <= rect.x)
	{
		return false;
	}
	for (int i=1; i<7; i++)
	{
		if (fPt[i].y <= fPt[i-1].y)
		{
			return false;
		}
	}

	if (fPt[14].y <= rect.x)
	{
		return false;
	}
	for (int i=8; i<15; i++)
	{
		if (fPt[i].y <= fPt[i+1].y)
		{
			return false;
		}
	}
	

	return true;
}

bool Damany::Imaging::FaceCompare::FaceAlignment::Alignment(IplImage* faceImg, 
	IplImage* faceLbpImg, CvPoint featurePt[])

{
	IplImage* sourImg = cvCloneImage(faceImg);
	CvPoint *fPt = new CvPoint[fptCount];
	/////////////////////////////////////////////////////////////
	/*IplROI* roi = new IplROI;
	roi->coi = 0;
	roi->height = faceRect.height;
	roi->width = faceRect.width;
	roi->xOffset = faceRect.x;
	roi->yOffset = faceRect.y;
	sourImg->roi = roi;*/
	/////////////////////////////////////////////////////////////

	AAM_Shape shape;
	bool flag = model.InitShapeFromDetBox(shape, facedet, sourImg);//get the initial target face rect
	if (flag == false)
	{
		cout<<"no face find in current target face image!!!!"<<endl;
		cvReleaseImage(&sourImg);
		delete[] fPt;
		return false;
	}

	//sourImg->roi = 0; ////////////////////////////////////
	//delete roi;//////////////////////////////////////

	if (featurePt != NULL)
	{
		for (int i=0; i<fptCount; i++)
		{
			fPt[i] = featurePt[i];
		}
	}
	else
	{
		model.Fit(sourImg, shape, 30, false);
		for (int i=0; i<fptCount; i++)
		{
			fPt[i] = cvPointFrom32f(shape[i]);
			//fPt[i].x += faceRect.x;//////////////////////////
			//fPt[i].y += faceRect.y;///////////////////////////
		}
	}
	
	float angle = GetAngle(fPt);
	if (fabs(angle) > 0.87)//如果大于50度
	{
		cvReleaseImage(&sourImg);
		delete[] fPt;
		return false;
	}
	ImageRotate(sourImg, fPt);

	int originX = sourImg->width;
	originX /= 2;
	int originY = sourImg->height;
	originY /= 2;
	ShapeRotate(fPt, fptCount, angle, originX, originY);
	CheckRange(fPt, fptCount, 0, 0, sourImg->width, sourImg->height);

	bool checkFlag = fptCheck(fPt, fptCount, sourImg->width, sourImg->height);
	if (!checkFlag)
	{
		cvReleaseImage(&sourImg);
		delete[] fPt;
		return false;
	}


	CvRect rect;
	int minX, minY, maxX, maxY;
	minX = MinX(fPt, fptCount);
	minY = MinY(fPt, fptCount);
	maxX = MaxX(fPt, fptCount);
	maxY = MaxY(fPt, fptCount);
	minX = minX<(sourImg->width-1) ? minX:(sourImg->width-2);
	minY = minY<(sourImg->height-1) ? minY:(sourImg->height-2);
	maxX = maxX>minX ? maxX:(minX+1);
	maxY = maxY>minY ? maxY:(minY+1);

	rect.x = minX;
	rect.y = minY;
	rect.width = maxX - minX;
	rect.height = maxY - minY;  

	CvPoint leftEye = cvPoint(0, 0);
	CvPoint rightEye = cvPoint(0, 0);
	CvPoint mouth = cvPoint(0, 0);
	GetEyeMouthPos(fPt, leftEye, rightEye, mouth);
	if (leftEye.x == rightEye.x)//in case of error
	{
		leftEye.x = rect.width*2/7;
		rightEye.x = rect.width*5/7;
	}

	float eyeDist = CalcEyeDist(leftEye, rightEye);
	float xScale = float(rect.width)/eyeDist;
	int iWidth = int(float(30)*xScale);
	int iHeight = int(float(iWidth)*float(rect.height)/float(rect.width));
	iWidth = iWidth>0 ? iWidth:1;
	iHeight = iHeight>0 ? iHeight:1;

	leftEye.x -= rect.x;
	leftEye.y -= rect.y;
	rightEye.x -= rect.x;
	rightEye.y -= rect.y;
	mouth.x -= rect.x;
	mouth.y -= rect.y;

	float namba1 = float(iWidth)/float(rect.width);
	float namba2 = float(iHeight)/float(rect.height); 
	leftEye.x = int(float(leftEye.x)*namba1);
	leftEye.y = int(float(leftEye.y)*namba2);

	cvSetImageROI(sourImg, rect);

	/*IplImage *refImg = cvLoadImage("c:\\handmade\\ref.jpg");
	IplImage *destImg = cvCreateImage(cvGetSize(sourImg), 8, 3);
	LightNorm(sourImg, refImg, destImg);
	cvCopy(destImg, sourImg);
	cvReleaseImage(&refImg);
	cvReleaseImage(&destImg);*/

	IplImage* sourGray = cvCreateImage(cvSize(rect.width, rect.height), 8, 1);
	cvCvtColor(sourImg, sourGray, CV_BGR2GRAY);
	cvResetImageROI(sourImg);

	ShapeNormalize(fPt, fptCount, rect.x, rect.y); 
	RemoveNonFaceArea(fPt, sourGray, fptCount);

	IplImage* scaleImg = cvCreateImage(cvSize(iWidth, iHeight), 8, 1);
	cvResize(sourGray, scaleImg, CV_INTER_CUBIC);
	GetLBPimg(scaleImg, faceLbpImg, leftEye);
	//cvResize(sourGray, lbpImg, CV_INTER_CUBIC);
	cvReleaseImage(&sourImg);
	cvReleaseImage(&sourGray);
	cvReleaseImage(&scaleImg);	
	delete[] fPt;

	return true;
}

int Damany::Imaging::FaceCompare::FaceAlignment::MinX(CvPoint fPt[], int fCount)
{
	int temp = fPt[0].x;
	for (int i=1; i<fCount; i++)
	{
		if (fPt[i].x < temp)
		{
			temp = fPt[i].x;
		}
	}

	return temp;
}

int Damany::Imaging::FaceCompare::FaceAlignment::MinY(CvPoint fPt[], int fCount)
{
	int temp = fPt[0].y;
	for (int i=1; i<fCount; i++)
	{
		if (fPt[i].y < temp)
		{
			temp = fPt[i].y;
		}
	}

	return temp;
}

int Damany::Imaging::FaceCompare::FaceAlignment::MaxX(CvPoint fPt[], int fCount)
{
	int temp = fPt[0].x;
	for (int i=1; i<fCount; i++)
	{
		if (fPt[i].x > temp)
		{
			temp = fPt[i].x;
		}
	}

	return temp;
}

int Damany::Imaging::FaceCompare::FaceAlignment::MaxY(CvPoint fPt[], int fCount)
{
	int temp = fPt[0].y;
	for (int i=1; i<fCount; i++)
	{
		if (fPt[i].y > temp)
		{
			temp = fPt[i].y;
		}
	}

	return temp;
}

void Damany::Imaging::FaceCompare::FaceAlignment::ShapeNormalize(CvPoint fpt[], int fCount, int minX, int minY)
{
	for (int i=0; i<fCount; i++)
	{
		fpt[i].x -= minX;
		fpt[i].y -= minY;
	}
}

void Damany::Imaging::FaceCompare::FaceAlignment::ShapeResize(CvPoint fpt[], int fCount, float xScale, float yScale)
{
	for (int i=0; i<fCount; i++)
	{
		fpt[i].x = int(float(fpt[i].x)*xScale);
		fpt[i].y = int(float(fpt[i].y)*yScale);
	}
}

void Damany::Imaging::FaceCompare::FaceAlignment::RemoveArea(IplImage* img, CvRect& faceRect, 
	CvPoint& pt1, CvPoint& pt2, bool LEFT)
{
	uchar* data = (uchar*)img->imageData;
	int step = img->widthStep;
	int channels = img->nChannels;

	float k = 0.0f;
	if ((pt1.x-pt2.x) != 0)
	{
		k = float(pt1.y - pt2.y)/float(pt1.x - pt2.x);
	}

	float b = pt1.y - float(pt1.x)*k;

	int startY = pt1.y>pt2.y ? pt2.y:pt1.y;//smaller one
	int endY = pt1.y>pt2.y ? pt1.y:pt2.y;//bigger one
	endY = endY>=(faceRect.y+faceRect.height) ? (faceRect.y+faceRect.height-1):endY;
	int startX = faceRect.x;
	int endX = faceRect.x + faceRect.width;
	endX = endX>=(faceRect.x+faceRect.width) ? (faceRect.x+faceRect.width-1):endX;
	int x = 0;
	if (LEFT)
	{
		for (int i=startY; i<endY+1; i++)
		{
			if (abs(k) < 0.0000000001)
			{
				x = (pt1.x + pt2.x)/2;
			}
			else
			{
				x = int(float(i - b)/k);
			}

			for (int j=startX; j<x; j++)
			{
				if (channels == 1)
				{
					data[i*step+j] = 0;
				}
				else if (channels == 3)
				{
					data[i*step+j*channels] = 0;
					data[i*step+j*channels+1] = 0;
					data[i*step+j*channels+2] = 0;
				}
			}
		}
	}
	else
	{
		endX = faceRect.width + faceRect.x;
		for (int i=startY; i<endY+1; i++)
		{
			if (fabs(k) < 0.0000000001)
			{
				x = (pt1.x + pt2.x)/2;
			}
			else
			{
				x = int(float(i - b)/k);
			}

			for (int j=endX; j>x; j--)
			{
				if (channels == 1)
				{
					data[i*step+j] = 0;
				}
				else if (channels == 3)
				{
					data[i*step+j*channels] = 0;
					data[i*step+j*channels+1] = 0;
					data[i*step+j*channels+2] = 0;
				}
			}
		}
	}
}

void Damany::Imaging::FaceCompare::FaceAlignment::RemoveBackground(IplImage* img, CvRect& faceRect, CvPoint fpt[])
{
	RemoveArea(img, faceRect, fpt[0], fpt[1]);
	RemoveArea(img, faceRect, fpt[1], fpt[2]);
	RemoveArea(img, faceRect, fpt[2], fpt[3]);
	RemoveArea(img, faceRect, fpt[3], fpt[4]);
	RemoveArea(img, faceRect, fpt[4], fpt[5]);
	RemoveArea(img, faceRect, fpt[5], fpt[6]);
	RemoveArea(img, faceRect, fpt[6], fpt[7]);

	RemoveArea(img, faceRect, fpt[7], fpt[8], false);
	RemoveArea(img, faceRect, fpt[8], fpt[9], false);
	RemoveArea(img, faceRect, fpt[9], fpt[10], false);
	RemoveArea(img, faceRect, fpt[10], fpt[11], false);
	RemoveArea(img, faceRect, fpt[11], fpt[12], false); 
	RemoveArea(img, faceRect, fpt[12], fpt[13], false);
	RemoveArea(img, faceRect, fpt[13], fpt[14], false);
}

void Damany::Imaging::FaceCompare::FaceAlignment::RemoveNonFaceArea(CvPoint fPt[], IplImage* img, int fCount)
{
	int min_X = MinX(fPt, fCount);
	int min_Y = MinY(fPt, fCount);
	int max_X = MaxX(fPt, fCount);
	int max_Y = MaxY(fPt, fCount);
	min_X = min_X>0 ? min_X:0;
	min_Y = min_Y>0 ? min_Y:0;
	max_X = max_X>0 ? max_X:0;
	max_Y = max_Y>0 ? max_Y:0;

	CvRect faceRect;
	faceRect.x = min_X;
	faceRect.y = min_Y;
	faceRect.width = max_X - min_X;
	faceRect.height = max_Y - min_Y;

	faceRect.x = faceRect.x>0 ? faceRect.x:0;
	faceRect.x = faceRect.x<img->width ? faceRect.x:(img->width-1);
	faceRect.y = faceRect.y>0 ? faceRect.y:0;
	faceRect.y = faceRect.y<img->height ? faceRect.y:(img->height-1);
	faceRect.width = (faceRect.x+faceRect.width)<img->width ? faceRect.width:(img->width-faceRect.x);
	faceRect.height = (faceRect.y+faceRect.height)<img->height ? faceRect.height:(img->height-faceRect.y);

	RemoveBackground(img, faceRect, fPt); 
}

void Damany::Imaging::FaceCompare::FaceAlignment::GetEyeMouthPos(CvPoint fPt[], CvPoint& leftEye, CvPoint& rightEye, CvPoint& mouth)
{
	leftEye.x = (fPt[27].x + fPt[28].x + fPt[29].x + fPt[30].x + fPt[31].x)/5;//left eye center
	leftEye.y = (fPt[27].y + fPt[28].y + fPt[29].y + fPt[30].y + fPt[31].y)/5;
	rightEye.x = (fPt[32].x + fPt[33].x + fPt[34].x + fPt[35].x + fPt[36].x)/5;//right eye center
	rightEye.y = (fPt[32].y + fPt[33].y + fPt[34].y + fPt[35].y + fPt[36].y)/5;

	int sumX = 0;
	int sumY = 0;
	for (int i=48; i<67; i++)//mouth center
	{
		sumX += fPt[i].x;
		sumY += fPt[i].y;
	}
	mouth.x = sumX/19;
	mouth.y = sumY/19;
}

float Damany::Imaging::FaceCompare::FaceAlignment::GetAngle(CvPoint fPt[])
{
	float faceAngle = -1.0f;
	CvPoint2D32f leftEyeCenter, rightEyeCenter;
	leftEyeCenter.x = (fPt[27].x + fPt[28].x + fPt[29].x + fPt[30].x + fPt[31].x)/5;//left eye center
	leftEyeCenter.y = (fPt[27].y + fPt[28].y + fPt[29].y + fPt[30].y + fPt[31].y)/5;
	rightEyeCenter.x = (fPt[32].x + fPt[33].x + fPt[34].x + fPt[35].x + fPt[36].x)/5;//right eye center
	rightEyeCenter.y = (fPt[32].y + fPt[33].y + fPt[34].y + fPt[35].y + fPt[36].y)/5;

	faceAngle = (rightEyeCenter.y - leftEyeCenter.y)/(rightEyeCenter.x - leftEyeCenter.x);
	faceAngle *= -1.0f;
	return faceAngle;
}

void Damany::Imaging::FaceCompare::FaceAlignment::ImageRotate(IplImage* img, CvPoint fPt[])
{
	IplImage* sourImg = cvCreateImage(cvSize(img->width, img->height), 8, img->nChannels);
	IplImage* destImg = cvCreateImage(cvSize(img->width,img->height), 8, img->nChannels);

	cvCopy(img, sourImg);

	int width = sourImg->width;
	int height = sourImg->height;

	CvMat* warpMat = cvCreateMat(2, 3, CV_32FC1); 

	float faceAngle = 0.0f;
	faceAngle = GetAngle(fPt);

	float alpha = cos(faceAngle);
	float belta = sin(faceAngle);

	cvSetReal2D(warpMat, 0, 0, alpha);
	cvSetReal2D(warpMat, 0, 1, belta);
	cvSetReal2D(warpMat, 0, 2, width*0.5f);
	cvSetReal2D(warpMat,1, 0, -belta);
	cvSetReal2D(warpMat, 1, 1, alpha);
	cvSetReal2D(warpMat, 1, 2, height*0.5f);

	cvGetQuadrangleSubPix(sourImg, destImg, warpMat); 
	cvZero(img);
	cvCopy(destImg, img); 

loop:
	cvReleaseImage(&sourImg);
	cvReleaseImage(&destImg);
	cvReleaseMat(&warpMat);
}

void Damany::Imaging::FaceCompare::FaceAlignment::ShapeRotate(CvPoint fPt[], int fCount, float angle, int originX, int originY)
{
	float alpha = cos(angle);
	float belta = sin(angle);

	for (int i=0; i<fCount; i++)
	{
		float x = float(fPt[i].x);
		float y = float(fPt[i].y);
		float x0 = float(originX);
		float y0 = float(originY);

		fPt[i].x = int(x*alpha - y*belta - x0*alpha + y0*belta + x0);
		fPt[i].y = int(x*belta + y*alpha - x0*belta - y0*alpha + y0);
	}
}

float Damany::Imaging::FaceCompare::FaceAlignment::CalcEyeDist(CvPoint leftEye, CvPoint rightEye)
{
	float dist = sqrt(float((rightEye.x-leftEye.x)*(rightEye.x-leftEye.x)) 
		+ float((rightEye.y-leftEye.y)*(rightEye.y-leftEye.y)));
	return dist;
}

float Damany::Imaging::FaceCompare::FaceAlignment::CalcEyeMouthDist(CvPoint leftEye, CvPoint rightEye, CvPoint mouth)
{
	int x = (leftEye.x + rightEye.x)/2;
	int y = (leftEye.y + rightEye.y)/2;
	float dist = sqrt(float((x-mouth.x)*(x-mouth.x)) + float((y-mouth.y)*(y-mouth.y)));
	return dist;
}

void Damany::Imaging::FaceCompare::FaceAlignment::GetLBPimg(IplImage* scaleImg, IplImage* lbpImg, CvPoint& leftEye)
{
	int lbpHeight = lbpImg->height;
	int lbpWidth = lbpImg->width;
	int lbpStep = lbpImg->widthStep;
	uchar* lbpData = (uchar*)lbpImg->imageData;

	int scaleHeight = scaleImg->height;
	int scaleWidth = scaleImg->width;
	int scaleStep = scaleImg->widthStep;
	uchar* scaleData = (uchar*)scaleImg->imageData;

	int xOffset = leftEye.x - 19;
	int yOffset = leftEye.y - 14;
	IplImage* yImg = cvCreateImage(cvSize(scaleWidth, scaleHeight-yOffset), 8, 1);
	if (yOffset > 0)
	{
		CvRect rect;
		rect.x = 0;
		rect.y = yOffset;
		rect.width = scaleWidth;
		rect.height = scaleHeight - yOffset;
		cvSetImageROI(scaleImg, rect);
		cvCopy(scaleImg, yImg);
		cvResetImageROI(scaleImg);
	}
	else if (yOffset < 0)
	{	
		CvRect rect;
		rect.x = 0;
		rect.y = -1*yOffset;
		rect.width = scaleWidth;
		rect.height = scaleHeight;
		cvSetImageROI(yImg, rect);
		cvCopy(scaleImg, yImg);
		cvResetImageROI(yImg);

		uchar* yImgData = (uchar*)yImg->imageData;
		int yOff = -1*yOffset;
		for (int i=0; i<yOff; i++)
		{
			for (int j=0; j<scaleWidth; j++)
			{
				yImgData[i*scaleStep+j] = 0;//yImgData[yOff*scaleStep+j];
			}
		}
	}
	else
	{
		cvCopy(scaleImg, yImg);
	}

	IplImage* xImg = cvCreateImage(cvSize(yImg->width-xOffset, yImg->height), 8, 1);
	if (xOffset > 0)
	{
		CvRect rect;
		rect.x = xOffset;
		rect.y = 0;
		rect.width = yImg->width - xOffset;
		rect.height = yImg->height;
		cvSetImageROI(yImg, rect);
		cvCopy(yImg, xImg);
		cvResetImageROI(yImg);
	}
	else if (xOffset < 0)
	{	
		CvRect rect;
		rect.x = -1*xOffset;
		rect.y = 0;
		rect.width = yImg->width;
		rect.height = yImg->height;
		cvSetImageROI(xImg, rect);
		cvCopy(yImg, xImg);
		cvResetImageROI(xImg);

		uchar* xImgData = (uchar*)xImg->imageData;
		int xOff = -1*xOffset;
		for (int i=0; i<xImg->height; i++)
		{
			for (int j=0; j<xOff; j++)
			{
				xImgData[i*xImg->widthStep+j] = 0;//xImgData[i*xImg->widthStep+xOff];
			}
		}
	}
	else
	{
		cvCopy(yImg, xImg);
	}

	int widthOffset = lbpWidth - xImg->width;
	int heightOffset = lbpHeight - xImg->height;
	IplImage* wImg = cvCreateImage(cvSize(lbpWidth, xImg->height), 8, 1);
	if (widthOffset > 0)
	{
		CvRect rect;
		rect.x = 0;
		rect.width = xImg->width;
		rect.y = 0;
		rect.height = xImg->height;
		cvSetImageROI(wImg, rect);
		cvCopy(xImg, wImg);
		cvResetImageROI(wImg);
		uchar* wImgData = (uchar*)wImg->imageData;
		for (int i=0; i<wImg->height; i++)
		{
			for (int j=xImg->width; j<wImg->width; j++)
			{
				wImgData[i*wImg->widthStep+j] = 0;//wImgData[i*wImg->widthStep+(xImg->width-1)];
			}
		}
	}
	else if (widthOffset < 0)
	{	
		CvRect rect;
		rect.x = 0;
		rect.width = lbpWidth;
		rect.y = 0;
		rect.height = xImg->height;
		cvSetImageROI(xImg, rect);
		cvCopy(xImg, wImg);
		cvResetImageROI(xImg);
	}
	else
	{
		cvCopy(xImg, wImg);
	}

	if (heightOffset < 0)
	{
		CvRect rect;
		rect.x = 0;
		rect.width = wImg->width;
		rect.y = 0;
		rect.height = lbpHeight;
		cvSetImageROI(wImg, rect);
		cvCopy(wImg, lbpImg);
		cvResetImageROI(wImg);
	}
	else if (heightOffset > 0)
	{
		CvRect rect;
		rect.x = 0;
		rect.width = wImg->width;
		rect.y = 0;
		rect.height = wImg->height;
		cvSetImageROI(lbpImg, rect);
		cvCopy(wImg, lbpImg);
		cvResetImageROI(lbpImg);

		for (int i=wImg->height; i<lbpHeight; i++)
		{
			for (int j=0; j<wImg->width; j++)
			{
				lbpData[i*lbpStep+j] = 0;//lbpData[(wImg->height-1)*lbpStep+j];
			}
		}
	}
	else
	{
		cvCopy(wImg, lbpImg); 
	} 

	cvReleaseImage(&yImg);
	cvReleaseImage(&xImg);
	cvReleaseImage(&wImg);
}

void Damany::Imaging::FaceCompare::FaceAlignment::CheckRange(CvPoint fpt[], int count, int min_x, int min_y, int max_x, int max_y)
{
	for (int i=0; i<count; i++)
	{
		if (fpt[i].x < min_x)
		{
			fpt[i].x = min_x;
		}
		if (fpt[i].y < min_y)
		{
			fpt[i].y = min_y;
		}
		if (fpt[i].x > max_x)
		{
			fpt[i].x = max_x;
		}
		if (fpt[i].y > max_y)
		{
			fpt[i].y = max_y;
		}
	}
}



