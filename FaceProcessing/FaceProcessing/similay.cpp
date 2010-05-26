#include "stdafx.h"
#include "similay.h" 

similayFace::similayFace(IplImage *img, CvRect faceRect)
{
	InitTargetFace(img, faceRect);
	LightCorrect(targetImg);
	CalcTargetHist();
	Gabor(targetResize, targetGabor);

	destFaceResize = cvCreateImage(cvSize(100, 100), 8, 1);
	destResult = cvCreateImage(cvSize(100, 100), 8, 1);
	destLarge = cvCreateImage(cvSize(110, 110), 8, 1);
}

similayFace::~similayFace()
{
	if (targetImg != NULL)
	{
		cvReleaseImage(&targetImg);
	}
	if (targetResize != NULL)
	{
		cvReleaseImage(&targetResize);
	}
	if (targetGabor != NULL)
	{
		cvReleaseImage(&targetGabor);
	}
	if (targetHist != NULL)
	{
		cvReleaseHist(&targetHist);
	}

	cvReleaseImage(&destLarge);
	cvReleaseImage(&destFaceResize); 
	cvReleaseImage(&destResult);
}
void similayFace::InitTargetFace(IplImage* img, CvRect faceRect)
{
	targetImg = cvCreateImage(cvSize(faceRect.width, faceRect.height), 8, 3);
	cvSetImageROI(img, faceRect);
	cvCopy(img, targetImg);
	cvResetImageROI(img); 
	cvSmooth(targetImg, targetImg, CV_BILATERAL, 80, 160);
	IplImage* grayImg = cvCreateImage(cvSize(targetImg->width, targetImg->height), 8, 1);
	cvCvtColor(targetImg, grayImg, CV_BGR2GRAY);
	targetResize = cvCreateImage(cvSize(100, 100), 8, 1);
	cvResize(grayImg, targetResize, CV_INTER_CUBIC);
	cvReleaseImage(&grayImg);

	targetGabor = cvCreateImage(cvSize(100, 100), 8, 1);
}

void similayFace::CalcTargetHist()
{
	IplImage* sourBimg = cvCreateImage(cvSize(targetImg->width, targetImg->height), 8, 1);
	IplImage* sourGimg = cvCreateImage(cvSize(targetImg->width, targetImg->height), 8, 1);
	IplImage* sourRimg = cvCreateImage(cvSize(targetImg->width, targetImg->height), 8, 1);

	IplImage* sourceImg[] = {sourBimg, sourGimg, sourRimg};
	cvSplit(targetImg, sourBimg, sourGimg, sourRimg, NULL);

	int b_bins = 32, g_bins = 32, r_bins = 32;

	int hist_size[] = {b_bins, g_bins, r_bins};
	float b_ranges[] = {0, 255};
	float g_ranges[] = {0, 255};
	float r_ranges[] = {0, 255};
	float* ranges[] = {b_ranges, g_ranges, r_ranges};
	targetHist = cvCreateHist(3, hist_size, CV_HIST_ARRAY, ranges, 1);

	cvCalcHist(sourceImg, targetHist, 0, 0);
	cvNormalizeHist(targetHist, 1.0);

	cvReleaseImage(&sourBimg);
	cvReleaseImage(&sourGimg);
	cvReleaseImage(&sourRimg);
}

void similayFace::GetCode(double m0, double mi, double p0, double pi, char& ch)
{
	if (m0 <= mi)
	{
		if (p0 <= pi)
		{
			ch = '0';
		}
		else
		{
			ch = '1';
		}
	}
	else
	{
		if (p0 <= pi)
		{
			ch = '2';
		}
		else
		{
			ch = '3';
		}
	}
}

int similayFace::GetInt(char res[], int count)
{
	int temp = 0;
	int base = 0;
	for (int i=0; i<count; i++)
	{
		switch(res[i])
		{
		case '0':
			{
				base = 0;
				break;
			}
		case '1':
			{
				base = 1;
				break;
			}
		case '2':
			{
				base = 2;
				break;
			}
		case '3':
			{
				base = 3;
				break;
			}
		}
		temp += (int)base*pow(4.0, i);
	}

	return temp;
}

void similayFace::Gabor(IplImage* img, IplImage* resImg)
{
	double sigma = 2*PI;
	double F = cvSqrt(2.0);
	CvGabor gabor;
	gabor.Init(PI/4, 3, sigma, F);

	IplImage* magImg = cvCreateImage(cvSize(img->width, img->height), IPL_DEPTH_32F, 1);
	IplImage* phaseImg = cvCreateImage(cvSize(img->width, img->height), IPL_DEPTH_32F, 1);

	gabor.conv_img(img, magImg, CV_GABOR_MAG);
	gabor.conv_img(img, phaseImg, CV_GABOR_PHASE);

	IplImage* magImgEx = cvCreateImage(cvSize(img->width+2, img->height+2), IPL_DEPTH_32F, 1);
	IplImage* phaseImgEx = cvCreateImage(cvSize(img->width+2, img->height+2), IPL_DEPTH_32F, 1);

	cvZero(magImgEx);
	cvZero(phaseImgEx);

	double val;
	for (int i=1; i<magImgEx->height-1; i++)
	{
		for (int j=1; j<magImgEx->width-1; j++)
		{
			val = cvGetReal2D(magImg, i-1, j-1);
			cvSetReal2D(magImgEx, i, j, val);
			val = cvGetReal2D(phaseImg, i-1, j-1);
			cvSetReal2D(phaseImgEx, i, j, val);
		}
	}

	double m0, m1, m2, m3, m4;
	double p0, p1, p2, p3, p4;
	char result[4];
	int resultInt;
	for (int i=1; i<magImgEx->height-1; i++)
	{
		for (int j=1; j<magImgEx->width-1; j++)
		{
			m0 = cvGetReal2D(magImgEx, i, j);
			m1 = cvGetReal2D(magImgEx, i-1, j);
			m2 = cvGetReal2D(magImgEx, i, j+1);
			m3 = cvGetReal2D(magImgEx, i+1, j);
			m4 = cvGetReal2D(magImgEx, i-1, j-1);

			p0 = cvGetReal2D(phaseImgEx, i, j);
			p1 = cvGetReal2D(phaseImgEx, i-1, j);
			p2 = cvGetReal2D(phaseImgEx, i, j+1);
			p3 = cvGetReal2D(phaseImgEx, i+1, j);
			p4 = cvGetReal2D(phaseImgEx, i-1, j-1);

			GetCode(m0, m1, p0, p1, result[0]);
			GetCode(m0, m2, p0, p2, result[1]);
			GetCode(m0, m3, p0, p3, result[2]);
			GetCode(m0, m4, p0, p4, result[3]);

			resultInt = GetInt(result, 4);
			cvSetReal2D(resImg, i-1, j-1, (double)resultInt);
		}
	}
	cvReleaseImage(&magImg);
	cvReleaseImage(&phaseImg); 
	cvReleaseImage(&magImgEx);
	cvReleaseImage(&phaseImgEx);

	
}

void similayFace::InitRect(CvRect& sourImgRect)
{
	sourImgRect.x = 0;
	sourImgRect.y = 0;
	sourImgRect.height = 0;
	sourImgRect.width = 0;
}

void similayFace::CmpHistogram(IplImage* destImg, double& result)
{
	IplImage* destBimg = cvCreateImage(cvSize(destImg->width, destImg->height), 8, 1);
	IplImage* destGimg = cvCreateImage(cvSize(destImg->width, destImg->height), 8, 1);
	IplImage* destRimg = cvCreateImage(cvSize(destImg->width, destImg->height), 8, 1);
	
	IplImage* destinImg[] = {destBimg, destGimg, destRimg};
	
	cvSplit(destImg, destBimg, destGimg, destRimg, NULL);

	int b_bins = 32, g_bins = 32, r_bins = 32;
	CvHistogram* destHist;

	int hist_size[] = {b_bins, g_bins, r_bins};
	float b_ranges[] = {0, 255};
	float g_ranges[] = {0, 255};
	float r_ranges[] = {0, 255};
	float* ranges[] = {b_ranges, g_ranges, r_ranges};
	destHist = cvCreateHist(3, hist_size, CV_HIST_ARRAY, ranges, 1);

	cvCalcHist(destinImg, destHist, 0, 0);
	cvNormalizeHist(destHist, 1.0);

	result = cvCompareHist(targetHist, destHist, CV_COMP_CORREL);

	cvReleaseImage(&destBimg);
	cvReleaseImage(&destGimg);
	cvReleaseImage(&destRimg);
	cvReleaseHist(&destHist);
}

void similayFace::ImageRotate(IplImage* img, float faceAngle)
{
	IplImage* destImg = cvCreateImage(cvSize(img->width,img->height), 8, img->nChannels);
	CvMat* warpMat = cvCreateMat(2, 3, CV_32FC1); 

	int width = img->width;
	int height = img->height;

	float alpha = cos(faceAngle);
	float belta = sin(faceAngle);

	cvSetReal2D(warpMat, 0, 0, alpha);
	cvSetReal2D(warpMat, 0, 1, belta);
	cvSetReal2D(warpMat, 0, 2, width*0.5f);
	cvSetReal2D(warpMat,1, 0, -belta);
	cvSetReal2D(warpMat, 1, 1, alpha);
	cvSetReal2D(warpMat, 1, 2, height*0.5f);

	cvGetQuadrangleSubPix(img, destImg, warpMat);
	cvCopy(destImg, img); 

	cvReleaseImage(&destImg);
	cvReleaseMat(&warpMat);
}

bool similayFace::CmpFaceWith_NoRotate(IplImage* img, CvRect& rect, float& cmpResult, bool noRotate)
{
	double histResult = 0.0; 
	sourImg = cvCreateImage(cvSize(rect.width, rect.height), 8, 3);
	cvSetImageROI(img, rect);
	cvCopy(img, sourImg);
	cvResetImageROI(img);

	if (noRotate == true)
	{
		cvSmooth(sourImg, sourImg, CV_BILATERAL, 80, 160);
		LightCorrect(sourImg); 
	}
	
	CmpHistogram(sourImg, histResult); 

	if (histResult > 0.5)
	{
		destFaceGray = cvCreateImage(cvSize(sourImg->width, sourImg->height), 8, 1);
		cvCvtColor(sourImg, destFaceGray, CV_BGR2GRAY);
		cvResize(destFaceGray, destFaceResize, CV_INTER_CUBIC);
		Gabor(destFaceResize, destResult);

		cvZero(destLarge);
		for (int i=0; i<100; i++)
		{
			for (int j=0; j<100; j++)
			{
				int destData = cvGetReal2D(destResult, i, j);
				cvSetReal2D(destLarge, i+5, j+5, destData);
			}
		}

		int match = 0;
		int targetData;
		int destData;
		for (int i=5; i<105; i++)
		{
			for (int j=5; j<105; j++)
			{
				targetData = cvGetReal2D(targetGabor, i-5, j-5);
				for (int m=i-5; m<i+5; m++)
				{
					for (int n=j-5; n<j+5; n++)
					{
						destData = cvGetReal2D(destLarge, m, n);
						if (destData == targetData)
						{
							match++; 
							goto loop;  
						}
					}
				}
loop:
				continue;
			}
		}

		cvReleaseImage(&destFaceGray);

		float resVal = 0.9*histResult + 1.1*match/10000;
		cmpResult = resVal;

		if (match < 6000)
		{
			goto no;
		}
		if (resVal < 1.3)
		{
			goto no;
		}
		goto yes;
	}

	if (noRotate == true)
	{
		cvReleaseImage(&sourImg);
	}
	   
no:
	return false;
yes:
	return true; 
}

bool similayFace::CmpFaceWith_Rotate(IplImage* img, CvRect& rect, float& cmpResult)
{
	int angle = 16;
	float faceAngle = 0.0f; 
	double histResult = 0.0f;
	for (int iterCount=0; iterCount<5; iterCount++)
	{
		if (iterCount == 0)
		{
			sourImg = cvCreateImage(cvSize(rect.width, rect.height), 8, 3);
			cvSetImageROI(img, rect);
			cvCopy(img, sourImg);
			cvResetImageROI(img);

			cvSmooth(sourImg, sourImg, CV_BILATERAL, 80, 160);
			LightCorrect(sourImg); 
			CmpHistogram(sourImg, histResult); 
		}

		faceAngle = (float)(angle - iterCount*8)/180*3.14f;
		if (histResult > 0.5)
		{
			ImageRotate(sourImg, faceAngle); 
			CvRect rect;
			rect.x = 0;
			rect.y = 0;
			rect.width = sourImg->width;
			rect.height = sourImg->height;
			bool flag = CmpFaceWith_NoRotate(sourImg, rect, cmpResult, false);
			if (flag == true)
			{
				cvReleaseImage(&sourImg);
				goto yes;
			}
		}
		else
		{
			cvReleaseImage(&sourImg);
			goto no;
		}

		if (iterCount == 4)
		{
			cvReleaseImage(&sourImg);    
		}
	}
no:
	return false; 
yes:
	return true; 
}

bool CompareFace(IplImage* sourImg, CvRect sourRect, 
			 IplImage* destImg, CvRect destRect, 
			 float *cmpResult, bool noRotate)
{
	similayFace sF(sourImg, sourRect);
	bool findFace = false;
	if (noRotate)
	{
		findFace = sF.CmpFaceWith_NoRotate(destImg, destRect, *cmpResult);
	}
	else
	{
		findFace = sF.CmpFaceWith_Rotate(destImg, destRect, *cmpResult);
	}
	
	return findFace;
}