#include "stdafx.h"
#include "FaceVarify.h"

Damany::Imaging::FaceVarify::FaceVarify()
{
	leftEye_pt = cvPoint(0, 0);
	rightEye_pt = cvPoint(0, 0);
	mouth_pt = cvPoint(0, 0); 
}

bool Damany::Imaging::FaceVarify::IsFaceImg(IplImage* smallFaceImg)
{
	IplImage* faceGray = cvCreateImage(cvGetSize(smallFaceImg), 8, 1);
	IplImage* faceResize = cvCreateImage(cvSize(100,100), 8, 1);

	cvCvtColor(smallFaceImg, faceGray, CV_BGR2GRAY);  
	cvResize(faceGray, faceResize, CV_INTER_CUBIC);

	int width = 100;
	int height = 100; 

	CvRect faceRect;
	faceRect.x = width/7;
	faceRect.y = height/10;
	faceRect.width = width*6/7 - width/7;
	faceRect.height = height/2 - height/10;  

	IplImage* temp1 = cvCloneImage(faceResize); 
	IplImage* temp2 = cvCloneImage(faceResize);

	CvPoint leftEye = cvPoint(0, 0);  
	SearchLeftEye(temp1, faceRect, leftEye);

	CvPoint rightEye = cvPoint(0, 0);  
	SearchRightEye(temp1, faceRect, rightEye); 

	CvPoint mouth = cvPoint(0, 0);
	CvRect mouthRect;
	mouthRect.x = 20;
	mouthRect.y = 55;
	mouthRect.width = 60;
	mouthRect.height = 40;
	SearchMouth(temp2, mouthRect, mouth);

	cvReleaseImage(&faceGray);
	cvReleaseImage(&faceResize);   
	cvReleaseImage(&temp1);
	cvReleaseImage(&temp2); 

	float eyeDist  = sqrt(float((leftEye.x-rightEye.x)*(leftEye.x-rightEye.x) + 
		(leftEye.y-rightEye.y)*(leftEye.y-rightEye.y)));
	int mid_x = (leftEye.x + rightEye.x)/2;
	int mid_y = (leftEye.y + rightEye.y)/2;
	float mouthDist = sqrt(float((mid_x-mouth.x)*(mid_x-mouth.x) + (mid_y-mouth.y)*(mid_y-mouth.y)));

	if (((leftEye.x==0) && (leftEye.y==0)) || (rightEye.x==0) && (rightEye.y==0) || ((mouth.x==0) && (mouth.y==0)))
	{
		return false;
	}
	if ((eyeDist < 20) || (eyeDist > 60))
	{
		return false;
	}
	if ((mouthDist < 26) || (mouthDist > 52))
	{  
		return false;
	} 
	return true;  
}

void Damany::Imaging::FaceVarify::SearchLeftEye(IplImage* grayImg, CvRect eyeRect, CvPoint& eyePt)
{
	uchar* grayData = (uchar*)grayImg->imageData;

	cvSetImageROI(grayImg, eyeRect);
	CvScalar avg = cvAvg(grayImg);
	cvResetImageROI(grayImg);
	float avgGray = avg.val[0];
	cvThreshold(grayImg, grayImg, avgGray*0.8, 255, CV_THRESH_BINARY_INV); 
	cvDilate(grayImg, grayImg, NULL, 1);

	int height = grayImg->height;
	int width = grayImg->width;

	CvRect roi;
	CvScalar roiSum;
	int regionSum;
	float dist = sqrt(float(width*width + height*height));
	float currDist = 0.0f;

	for (int i=height/5; i<height/2; i++)
	{
		for (int j=width/7; j<width/2; j++)
		{
			roi.x = (j - 5)>0 ? (j-5):0;
			roi.y = (i - 4)>0 ? (i-4):0;
			roi.width = (roi.x+10)>width ? (width-roi.x):10;
			roi.height = (roi.y+8)>height ? (height-roi.y):8;
			cvSetImageROI(grayImg, roi);
			roiSum = cvSum(grayImg);
			regionSum = int(roiSum.val[0]);
			if (regionSum > 10*255)
			{
				cvResetImageROI(grayImg);
				roi.x = (j - 5)>0 ? (j-5):0;
				roi.y = (i - 12 - 4)>0 ? (i-16):0; 
				roi.width = (roi.x+10)>width ? (width-roi.x):10;
				roi.height = (roi.y+8)>height ? (height-roi.y):8;
				cvSetImageROI(grayImg, roi);
				roiSum = cvSum(grayImg);
				cvResetImageROI(grayImg);
				regionSum = int(roiSum.val[0]); 
				if (regionSum > 10*255)
				{
					currDist = sqrt(float((j-5*width/16)*(j-5*width/16) + (i-25*height/64)*(i-25*height/64)));
					if (currDist < dist)
					{
						dist = currDist;
						eyePt.x = j;
						eyePt.y = i; 
					}
				}
				continue;
			}
			cvResetImageROI(grayImg);
		}
	}
}

void Damany::Imaging::FaceVarify::SearchRightEye(IplImage *grayImg, CvRect eyeRect, CvPoint &eyePt)
{
	int height = grayImg->height;
	int width = grayImg->width;

	CvRect roi;
	CvScalar roiSum;
	int regionSum;
	float dist = sqrt(float(width*width + height*height)); 
	float currDist = 0.0f;

	for (int i=height/5; i<height/2; i++)
	{
		for (int j=width/2; j<width*6/7; j++)
		{
			roi.x = (j - 5)>0 ? (j-5):0;
			roi.y = (i - 4)>0 ? (i-4):0;
			roi.width = (roi.x+10)>width ? (width-roi.x):10;
			roi.height = (roi.y+8)>height ? (height-roi.y):8;
			cvSetImageROI(grayImg, roi);
			roiSum = cvSum(grayImg);
			regionSum = int(roiSum.val[0]);
			if (regionSum > 10*255)
			{
				cvResetImageROI(grayImg); 
				roi.x = (j - 5)>0 ? (j-5):0;
				roi.y = (i - 12 - 4)>0 ? (i-16):0; 
				roi.width = (roi.x+10)>width ? (width-roi.x):10;
				roi.height = (roi.y+8)>height ? (height-roi.y):8; 
				cvSetImageROI(grayImg, roi);
				roiSum = cvSum(grayImg); 
				cvResetImageROI(grayImg);
				regionSum = int(roiSum.val[0]);
				if (regionSum > 10*255)
				{
					currDist = sqrt(float((j-20*width/29)*(j-20*width/29) + (i-height*25/64)*(i-height*25/64)));
					if (currDist < dist)
					{
						dist = currDist; 
						eyePt.x = j; 
						eyePt.y = i;
					}
				}
				continue;
			}
			cvResetImageROI(grayImg);
		}
	} 
}

void Damany::Imaging::FaceVarify::SearchMouth(IplImage *grayImg, CvRect &mouthRect, CvPoint &mouthPt)
{
	cvSetImageROI(grayImg, mouthRect);
	CvScalar avg = cvAvg(grayImg);
	cvResetImageROI(grayImg);

	float avgVal = 0.0f;
	avgVal = avg.val[0];
	cvThreshold(grayImg, grayImg, 0.8*avgVal, 255, CV_THRESH_BINARY_INV);
	cvDilate(grayImg, grayImg, NULL, 1);

	float dist = sqrt(float(grayImg->width*grayImg->width + grayImg->height*grayImg->height)); 
	float currDist = 0.0f;
	int height = grayImg->height;
	int width = grayImg->width;
	int regionSum = 0;
	CvRect roi;
	CvScalar roiSum;

	for (int i=mouthRect.y; i<mouthRect.y+mouthRect.height; i++)
	{
		for (int j=mouthRect.x; j<mouthRect.x+mouthRect.width; j++)
		{
			roi.x = (j - 3)>0 ? (j-3):0;
			roi.y = (i - 3)>0 ? (i-3):0;
			roi.width = (roi.x+7)>width ? (width-roi.x):7;
			roi.height = (roi.y+7)>height ? (height-roi.y):7;
			cvSetImageROI(grayImg, roi);
			roiSum = cvSum(grayImg);
			regionSum = int(roiSum.val[0]);
			cvResetImageROI(grayImg);

			if (regionSum > 20*255)
			{
				currDist = sqrt(float((i-80)*(i-80) + (j-50)*(j-50)));
				if (currDist < dist)
				{
					dist  = currDist; 
					mouthPt.x = j; 
					mouthPt.y = i; 
				}
			}
		}
	}
}