#include "stdafx.h"
#include "FaceVarify.h"

#ifndef max
#define max(a,b)            (((a) > (b)) ? (a) : (b))
#endif
#ifndef min
#define min(a,b) (((a) < (b)) ? (a) : (b))
#endif

Damany::Imaging::FaceSearch::FaceVarify::FaceVarify()
{
	leftEye_pt = cvPoint(0, 0);
	rightEye_pt = cvPoint(0, 0);
	mouth_pt = cvPoint(0, 0); 
}

bool Damany::Imaging::FaceSearch::FaceVarify::IsFaceImg(IplImage* smallFaceImg)
{
	/*if (smallFaceImg->nChannels != 3)
	{
	return false;
	}*/
	
	//cvSaveImage("c:\\smallFace.jpg", smallFaceImg);
	IplImage* faceGray = cvCreateImage(cvGetSize(smallFaceImg), 8, 1);
	IplImage* faceResize = cvCreateImage(cvSize(100,100), 8, 1);

	cvCvtColor(smallFaceImg, faceGray, CV_BGR2GRAY);

	//cvSaveImage("c:\\faceGray.jpg", faceGray);

	cvResize(faceGray, faceResize, CV_INTER_CUBIC);
	cvReleaseImage(&faceGray);

	CvScalar *meanVal = new CvScalar;
	CvScalar *stdVal = new CvScalar;

	cvAvgSdv(faceResize, meanVal, stdVal);
	float temp = stdVal->val[0];
	delete meanVal;
	delete stdVal;

	if ((temp < 8/*13*/) || (temp > 64/*60*/))//判断整体的方差太大或太小
	{
		cvReleaseImage(&faceResize); 
		return false;
	}
	else//判断左半个图片与右半个图片的方差的差值
	{
		CvScalar *mVal = new CvScalar;
		CvScalar *sVal = new CvScalar;
		CvRect rect;

		rect.x = 0;
		rect.y = 0;
		rect.width = faceResize->width/2;
		rect.height = faceResize->height;
		cvSetImageROI(faceResize, rect);
		cvAvgSdv(faceResize, mVal, sVal);
		cvResetImageROI(faceResize);
		float leftStd = sVal->val[0];

		rect.x = faceResize->width/2;
		cvSetImageROI(faceResize, rect);
		cvAvgSdv(faceResize, mVal, sVal);
		cvResetImageROI(faceResize);
		float rightStd = sVal->val[0];

		delete mVal;
		delete sVal;

		if ((leftStd < 7) && (rightStd < 7))
		{
			cvReleaseImage(&faceResize);
			return false;
		}

		if (fabs(leftStd - rightStd) > 21.5/*21*/)
		{
			cvReleaseImage(&faceResize); 
			return false;
		}
	}

	int width = 100;
	int height = 100; 

	CvRect faceRect;
	faceRect.x = width/7;
	faceRect.y = height/10;
	faceRect.width = width*6/7 - width/7;
	faceRect.height = height/2 - height/10;  

	IplImage* temp1 = cvCloneImage(faceResize); 
	IplImage* temp2 = cvCloneImage(faceResize);
	cvReleaseImage(&faceResize);

	CvPoint mouth = cvPoint(0, 0);
	CvRect mouthRect;
	mouthRect.x = 20;
	mouthRect.y = 55;
	mouthRect.width = 60;
	mouthRect.height = 40;
	SearchMouth(temp2, mouthRect, mouth);
	cvReleaseImage(&temp2);

	if ((mouth.x == 0) || (mouth.y == 0))//没找到嘴巴
	{
		cvReleaseImage(&temp1);
		return false;
	}

	CvPoint leftEye = cvPoint(0, 0);  
	SearchLeftEye(temp1, faceRect, leftEye);

	CvPoint rightEye = cvPoint(0, 0);  
	SearchRightEye(temp1, faceRect, rightEye); 
	cvReleaseImage(&temp1);

	if (((leftEye.x == 0) || (leftEye.y == 0))
		&& ((rightEye.x == 0) || (rightEye.y == 0)))//左眼、左眉只找到一个，并且，右眼、右眉只找到一个
	{
		return false;
	}
	else if ((leftEye.x == 0) || (leftEye.y == 0))
	{
		bool flag = JudgeFaceByColor(smallFaceImg);
		if (flag)
		{
			return true;
		}
		else
		{
			return false;
		}
	}
	else if ((rightEye.x == 0) || (rightEye.y == 0))
	{
		bool flag = JudgeFaceByColor(smallFaceImg);
		if (flag)
		{
			return true;
		}
		else
		{
			return false;
		}
	}

	float eyeDist  = sqrt(float((leftEye.x-rightEye.x)*(leftEye.x-rightEye.x) + 
		(leftEye.y-rightEye.y)*(leftEye.y-rightEye.y)));
	int mid_x = (leftEye.x + rightEye.x)/2;
	int mid_y = (leftEye.y + rightEye.y)/2;
	float mouthDist = sqrt(float((mid_x-mouth.x)*(mid_x-mouth.x) + (mid_y-mouth.y)*(mid_y-mouth.y)));

	if ((eyeDist < 20) || (eyeDist > 60))
	{
		bool flag = JudgeFaceByColor(smallFaceImg);
		if (flag)
		{
			return true;
		}
		else
		{
			return false;
		}
	}
	if ((mouthDist < 26) || (mouthDist > 61/*59*/))
	{  
		return false;
	} 

	bool sumJudgeFlag = SumJudge(smallFaceImg);
	if (!sumJudgeFlag)
	{
		return false;
	}

	bool rgbJudge = RGBavgDiffJudge(smallFaceImg);
	if (!rgbJudge)
	{
		return false;
	}
	
	return true;  
}

bool Damany::Imaging::FaceSearch::FaceVarify::JudgeFaceByColor( IplImage* pImg )
{
	if( !pImg ) return false;

	bool bFace = true;

	int i = 0;
	int j = 0;
	int h = pImg->height;
	int w = pImg->width;

	int nMRThre = 60;
	float fRcRat = 0.15f;
	float fGcRat = 0.15f;
	float fGBcRat = 0.15f;
	int nOverThr = 240;
	int nMmThre = 5;
	int nAbNorm = 0;
	int nAbNormR = 0;
	int nAbNormG = 0;
	int nAbNormB = 0;
	int nNorm = 0;
	int nOvers = 0;
	for( i = 0; i < h; i++ )
	{
		uchar* ptr = (uchar*) ( pImg->imageData + i * pImg->widthStep );
		for( j = 0; j < w; j++ )
		{
			int B = ptr[ 3 * j + 0 ];
			int G = ptr[ 3 * j + 1 ];
			int R = ptr[ 3 * j + 2 ];

			if( R > G 
				//&& R > B
				)
			{
				nNorm++;
			}

			if( ( R - G ) > nMRThre 
				&& ( R - B ) > nMRThre 
				)
			{
				nAbNorm++;
				nAbNormR++;
			}

			if( ( R - G ) > int( (float)R * fRcRat )
				&& ( R - B ) > int( (float)R * fRcRat )
				&& B > G )
			{
				nAbNorm++;
				nAbNormR++;
			}

			if( ( G - R ) > int( (float)G * fGcRat ) )
			{
				nAbNorm++;
				nAbNormG++;
			}

			if( R > nOverThr && G > nOverThr && B > nOverThr )
			{
				nAbNorm++;
			}

			if( ( B - R ) > int( (float)B * fGBcRat ) 
				&& B > 150 )
			{
				nAbNorm++;
				nAbNormB++;
			}

			int nMinVal = min( R, G );
			nMinVal = min( nMinVal, B );
			int nMaxVal = max( R, G );
			nMaxVal = max( nMaxVal, B );
			if( nMaxVal - nMinVal < nMmThre 
				&& nMinVal > 200 )
			{
				nOvers++;
			}
		}
	}

	if( nAbNorm > int( (float)( w * h ) * 0.5f ) )
	{
		bFace = false;
	}

	if( nOvers > int( (float)( w * h )  * 0.5f ) )
	{
		bFace = false;
	}

	if( ( nAbNorm + nOvers ) > (float)(w * h) * 0.8f )
	{
		bFace = false;
	}

	if( nNorm < (float)(w*h) * 0.6f )
	{
		bFace = false;
	}

	return bFace;
}

void Damany::Imaging::FaceSearch::FaceVarify::SearchLeftEye(IplImage* grayImg, CvRect eyeRect, CvPoint& eyePt)
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

void Damany::Imaging::FaceSearch::FaceVarify::SearchRightEye(IplImage *grayImg, CvRect eyeRect, CvPoint &eyePt)
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

void Damany::Imaging::FaceSearch::FaceVarify::SearchMouth(IplImage *grayImg, CvRect &mouthRect, CvPoint &mouthPt)
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

bool Damany::Imaging::FaceSearch::FaceVarify::SumJudge(IplImage *colorIn)
{
	IplImage *sourGray = cvCreateImage(cvGetSize(colorIn), 8, 1);
	IplImage *gxImg = cvCreateImage(cvGetSize(colorIn), 8, 1);
	IplImage *gyImg = cvCreateImage(cvGetSize(colorIn), 8, 1);

	char Kx[9] = {1,0,-1,2,0,-2,1,0,-1};//X方向掩模，用于得到X方向梯度图
	char Ky[9] = {1,2,1,0,0,0,-1,-2,-1};//Y方向掩模，用于得到Y方向梯度图
	CvMat KX,KY;
	KX = cvMat(3,3,CV_8S,Kx);//构建掩模内核
	KY = cvMat(3,3,CV_8S,Ky);//构建掩模内核

	cvCvtColor(colorIn, sourGray, CV_BGR2GRAY);//将当前帧转化为灰度图
	cvSmooth(sourGray, sourGray, CV_GAUSSIAN, 7, 7);//进行平滑处理
	
	cvFilter2D(sourGray, gxImg, &KX, cvPoint(-1,-1));//得到X方向的梯度图
	cvFilter2D(sourGray, gyImg, &KY, cvPoint(-1,-1));//得到Y方向的梯度图

	cvThreshold(gxImg, gxImg, 50, 255, CV_THRESH_BINARY);
	cvThreshold(gyImg, gyImg, 50, 255, CV_THRESH_BINARY);

	int width = sourGray->width;
	int height = sourGray->height;

	CvScalar sum1 = cvSum(gxImg);
	CvScalar sum2 = cvSum(gyImg);
	float gxSum = sum1.val[0];
	float gySum = sum2.val[0];
	gySum = gySum > 0.00001 ? gySum:0.00001;

	float gyTotalThr = float(width*height*255)*0.01;
	float gxTotalThr = float(width*height*255)*0.001;
	
	CvRect eyeRect;
	eyeRect.x = width/7;
	eyeRect.y = height/10;
	eyeRect.width = width*6/7 - width/7;
	eyeRect.height = height/2 - height/10;
	
	cvSetImageROI(gyImg, eyeRect);
	CvScalar sum3 = cvSum(gyImg);
	cvResetImageROI(gyImg);
	float eyeSum = sum3.val[0];
	
	float eyeThr = float(eyeRect.width*eyeRect.height*255)*0.001;

	CvRect mouthRect;
	mouthRect.x = width/5;
	mouthRect.y = 11*height/20;
	mouthRect.width = 3*width/5;
	mouthRect.height = height/4;

	cvSetImageROI(gyImg, mouthRect);
	CvScalar sum4 = cvSum(gyImg);
	cvResetImageROI(gyImg);
	float mouthSum = sum4.val[0];

	float mouthThr = float(mouthRect.width*mouthRect.height*255)*0.001;

	CvRect noseRect;
	noseRect.x = 43*width/100;
	noseRect.y = height*2/5;
	noseRect.width = 7*width/25;
	noseRect.height = height*2/5;

	//cvSetImageROI(gxImg, noseRect);
	//CvScalar sum5 = cvSum(gxImg);
	//cvResetImageROI(gxImg);
	//float gxNoseSum = sum5.val[0];

	//float gxNoseThr = float(noseRect.width*noseRect.height*255)*0.001;

	cvSetImageROI(gyImg, noseRect);
	CvScalar sum6 = cvSum(gyImg);
	cvResetImageROI(gyImg);
	float gyNoseSum = sum6.val[0];

	float gyNoseThr = float(noseRect.width*noseRect.height*255)*0.01;

	cvReleaseImage(&sourGray);
	cvReleaseImage(&gxImg);
	cvReleaseImage(&gyImg);	

	if (gySum<gyTotalThr)
	{
		return false;
	}
	if (gxSum<gxTotalThr)
	{
		return false;
	}
	if (eyeSum < eyeThr)
	{
		return false;
	}
	if (mouthSum < mouthThr)
	{
		return false;
	}
	/*if (gxNoseSum < gxNoseThr)
	{
		return false;
	}*/
	if (gyNoseSum < gyNoseThr)
	{
		return false;
	}

	return true;
}

bool Damany::Imaging::FaceSearch::FaceVarify::RGBavgDiffJudge(IplImage *sourImg)
{
	IplImage *b_img = cvCreateImage(cvGetSize(sourImg), 8, 1);
	IplImage *g_img = cvCreateImage(cvGetSize(sourImg), 8, 1);
	IplImage *r_img = cvCreateImage(cvGetSize(sourImg), 8, 1);

	cvSplit(sourImg, b_img, g_img, r_img, NULL);

	CvScalar mean;
	CvScalar sdv;
	cvAvgSdv(b_img, &mean, &sdv);
	float b_mean = mean.val[0];
	float b_sdv = sdv.val[0];
	cvAvgSdv(g_img, &mean, &sdv);
	float g_mean = mean.val[0];
	float g_sdv = sdv.val[0];
	cvAvgSdv(r_img, &mean, &sdv);
	float r_mean = mean.val[0];
	float r_sdv = sdv.val[0];

	float minVal = b_mean<g_mean ? b_mean:g_mean;
	minVal = minVal<r_mean ? minVal:r_mean;
	float maxVal = b_mean>g_mean ? b_mean:g_mean;
	maxVal = maxVal>r_mean ? maxVal:r_mean;
	float diff = maxVal - minVal;

	cvReleaseImage(&b_img);
	cvReleaseImage(&g_img);
	cvReleaseImage(&r_img);

	if (diff > 7.5)
	{
		return true;
	}
	return false;
}
