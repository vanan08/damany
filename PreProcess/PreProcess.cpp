// PreProcess.cpp : Defines the exported functions for the DLL application.
/************************************************************************************************************
Copyright (c) 2009, 成都精识智能技术有限公司
All right reserved!

文件名：PreProcess.cpp
摘  要：该程序，对每一帧输入图片，利用三帧差分法，在指定警戒区域内进行运动目标检测。当警戒区域内检测到运动目标
时，在全图范围内进行运动目标标示，即：画框，并将框的坐标点予以返回。最后，对有运动目标的图片，进行分组,
当分组结束时，返回true,否则，返回false.布控和画框相关的参数通过UI调用函数来实现.

当前版本：3.2
作    者：薛晓利
完成日期：2009年8月31日  
*************************************************************************************************************/
#include "stdafx.h"
#include "PreProcess.h"   

Frame prevFrame ;//存储上一帧的frame    

bool firstFrmRec = false;//第一帧是否收到
bool secondFrmRec = false;//第二帧是否收到

IplImage *currImg;//当前帧的图片
IplImage *lastGrayImg;//上一帧灰度图
IplImage *lastDiffImg;//上一帧差分图的二值化图 

int xLeftAlarm = 100; //定义并初始化警戒区域的两个坐标点位置
int yTopAlarm = 400;
int xRightAlarm = 600;
int yBottomAlarm = 500;  

int minLeftX = 3000;//定义并初始化框框的两个坐标点位置
int minLeftY = 3000; 
int maxRightX = 0;
int maxRightY = 0;

int faceCount = 500; //画框的阈值
int groupCount = 5;//分组图片个数
int signelCount = 0;//记录当前分组中的图片数量

bool drawAlarmArea = false;//标志是否画出警戒区域
bool drawRect = false; //标志是否画框

//标识是否画出红色的框框,通过UI来设定
void SetDrawRect(bool draw)
{
	drawRect = draw;
}

//设置布控的警戒区域，通过UI来调用该函数;
void SetAlarmArea(const int leftX, const int leftY, const int rightX, const int rightY, bool draw)
{
	xLeftAlarm = leftX;
	yTopAlarm = leftY;
	xRightAlarm = rightX; 
	yBottomAlarm = rightY; 
	drawAlarmArea = draw;//设置是否画出布控区域，该区域为蓝色边框
}

//设置运动检测的框框内阈值和分组个数，通过UI来调用该函数
void SetRectThr(const int fCount, const int gCount)
{
	faceCount = fCount;
	groupCount = gCount;
}

//计算选定区域内像素点的数值之和,并将数值返回,该函数由PreProcessFrame函数调用 
int RegionSum(const int left_x, const int left_y, const int right_x, const int right_y, IplImage *img)
{
	uchar *data = (uchar*)img->imageData;
	int step = img->widthStep;
	int sum = 0;
	for (int i=left_y; i<right_y; i++)
	{ 
		for (int j=left_x; j<right_x; j++)
		{
			sum += data[i*step+j];
		}
	}
	return sum;
}

//判断警戒区域内是否有运动目标,有运动目标返回true,否则，返回false,该函数由PreProcessFrame函数调用
bool AlarmArea(const int x_left, const int y_top, const int x_right, const int y_bottom, IplImage *lastDiffImg) 
{
	int step = lastDiffImg->widthStep;
	uchar *lastDiffData = (uchar*)lastDiffImg->imageData;
	int area = (x_right-x_left)*(y_bottom-y_top);
	int lastDiffCount = 0;

	for (int i=y_top; i<y_bottom; i++)
	{
		for (int j=x_left; j<x_right; j++)
		{
			if (lastDiffData[i*step+j] > 10)
			{
				lastDiffCount++;
			}
		}
	}
	if (lastDiffCount > 0.01*area)
	{
		return true;
	}
	else
	{
		return false;
	}
} 

//找到框框的两个横坐标位置,该函数由PreProcessFrame函数调用 
void FindRectX(IplImage *img, const int leftY, const int rightY)
{
	int count = (img->width - 100)/20 + 1;

	int *leftX = new int [count];
	int *rightX = new int [count];

	int sumInRect = 0;

	for (int i=0; i<count; i++)
	{
		if (i==0)
		{
			leftX[i] = 0;
			rightX[i] = 100;
		}
		else
		{
			leftX[i] = leftX[i-1] + 20;
			rightX[i] = rightX[i-1] + 20;
		}
		sumInRect = RegionSum(leftX[i], leftY, rightX[i], rightY, img);
		if (sumInRect > faceCount*255)
		{
			minLeftX = minLeftX<leftX[i] ? minLeftX:leftX[i];
			maxRightX = maxRightX>rightX[i] ? maxRightX:rightX[i];
		}
	}

	delete []leftX;
	delete []rightX;
}

//找到框框的两个纵坐标位置,该函数由PreProcessFrame函数调用  
void FindRectY(IplImage *img, const int leftX, const int rightX)
{
	int count = (img->height - 100)/20 + 1;

	int *leftY = new int[count];
	int *rightY = new int[count];

	int sumInRect = 0;

	for (int i=0; i<count; i++)
	{
		if (i == 0)
		{
			leftY[i] = 0;
			rightY[i] = 100;
		}
		else
		{
			leftY[i] = leftY[i-1] + 20;
			rightY[i] = rightY[i-1] + 20;
		}
		sumInRect = RegionSum(leftX, leftY[i], rightX, rightY[i], img);  
		if (sumInRect > faceCount*255)
		{
			minLeftY = minLeftY<leftY[i] ? minLeftY:leftY[i];
			maxRightY = maxRightY>rightY[i] ? maxRightY:rightY[i]; 
		}
	}

	delete []leftY;
	delete []rightY; 
}

/*每次从摄像头获得一张图片后调用，进行运动目标检测，并画框，
  当完成一个分组后返回true, 分组没结束，则返回false,
  该函数由UI来调用*/
PREPROCESS_API bool PreProcessFrame(Frame frame, Frame &lastFrame)
{
	Frame tempFrame; 
	tempFrame = prevFrame; 

	currImg = frame.image;  

	CvSize imgSize = cvSize(currImg->width,currImg->height);
	IplImage *grayImg = cvCreateImage(imgSize,IPL_DEPTH_8U,1);//当前帧的灰度图
	IplImage *gxImg = cvCreateImage(imgSize,IPL_DEPTH_8U,1);//当前帧的X方向梯度图
	IplImage *gyImg = cvCreateImage(imgSize,IPL_DEPTH_8U,1);//当前帧的Y方向梯度图
	IplImage *diffImg = cvCreateImage(imgSize,IPL_DEPTH_8U,1);//当前帧的差分图
	IplImage *diffImg_2 = cvCreateImage(imgSize,IPL_DEPTH_8U,1);//前一帧差分图 
	IplImage *pyr = cvCreateImage(cvSize((imgSize.width&-2)/2,(imgSize.height&-2)/2),8,1); //进行腐蚀去除噪声的中间临时图片

	int height,width;//定义图像的高，宽，步长 

	char Kx[9] = {1,0,-1,2,0,-2,1,0,-1};//X方向掩模，用于得到X方向梯度图
	char Ky[9] = {1,2,1,0,0,0,-1,-2,-1};//Y方向掩模，用于得到Y方向梯度图
	CvMat KX,KY;
	KX = cvMat(3,3,CV_8S,Kx);//构建掩模内核 
	KY = cvMat(3,3,CV_8S,Ky);//构建掩模内核

	cvCvtColor(currImg,grayImg,CV_BGR2GRAY);
	cvSmooth(grayImg,grayImg,CV_GAUSSIAN,7,7);//进行平滑处理
	cvFilter2D(grayImg,gxImg,&KX,cvPoint(-1,-1));//得到X方向的梯度图
	cvFilter2D(grayImg,gyImg,&KY,cvPoint(-1,-1));//得到Y方向的梯度图
	cvAdd(gxImg,gyImg,grayImg,NULL);//得到梯度图

	cvReleaseImage(&gxImg);
	cvReleaseImage(&gyImg);

	height = grayImg->height;
	width = grayImg->width; 

	bool alarm = false;//警戒区域是否有运动目标   

	CvRect rect;//定义矩形框

	if(!firstFrmRec)//如果是第一帧
	{
		firstFrmRec = true; 
		lastGrayImg = cvCreateImage(imgSize,IPL_DEPTH_8U,1);
		lastDiffImg = cvCreateImage(imgSize,IPL_DEPTH_8U,1);
		cvCopy(grayImg,lastGrayImg,NULL);//如果是第一帧，设置为背景 

		xRightAlarm = currImg->width - 100;
		yBottomAlarm = currImg->height - 100;
		yTopAlarm = currImg->height - 200;
	}
	else
	{
		cvAbsDiff(grayImg,lastGrayImg,diffImg);//得到当前帧的差分图
		cvCopy(grayImg,lastGrayImg,NULL);//将当前帧的梯度图作为下一帧的背景
		cvThreshold(diffImg,diffImg,15,255,CV_THRESH_BINARY);//二值化当前差分图
		if(secondFrmRec)//如果大于等于第三帧
		{
			cvAnd(diffImg,lastDiffImg,diffImg_2);//进行“与”运算，得到前一帧灰度图的“准确”运动目标
			cvPyrDown(diffImg_2,pyr,7);//向下采样
			cvErode(pyr,pyr,0,1);//腐蚀，消除小的噪声
			cvPyrUp(pyr,diffImg_2,7); 
			cvReleaseImage(&pyr);

			if(drawAlarmArea)	cvRectangle(tempFrame.image, cvPoint(xLeftAlarm, yTopAlarm), cvPoint(xRightAlarm, yBottomAlarm), CV_RGB(0, 0, 255), 3, CV_AA, 0);
			alarm = AlarmArea(xLeftAlarm, yTopAlarm, xRightAlarm, yBottomAlarm, diffImg_2);  
		}

		cvCopy(diffImg,lastDiffImg,NULL);//备份当前差分图的二值化图

		cvReleaseImage(&diffImg); 

		minLeftX = 3000;
		minLeftY = 3000; 
		maxRightX = 0;
		maxRightY = 0;

		if (alarm)//若检测出整个图片有运动目标
		{
			FindRectX(diffImg_2, 0, height); 
			FindRectY(diffImg_2, minLeftX, maxRightX);
		}

		if (!secondFrmRec)//设置第二帧已经收到 
		{
			secondFrmRec = true; 
		}
	}

	if(maxRightX*maxRightY)//如果当前帧检测到运动目标，则，画框,分组
	{
		//防止右下角出界
		maxRightX = maxRightX>1 ? maxRightX:2; 
		maxRightY = maxRightY>1 ? maxRightY:2;
		maxRightX = maxRightX<(width+1) ? maxRightX:width;
		maxRightY = maxRightY<(height+1) ? maxRightY:height;

		//防止左上角出界
		minLeftX = minLeftX>0 ? minLeftX:1;
		minLeftY = minLeftY>0 ? minLeftY:1; 
		minLeftX = minLeftX<maxRightX ? minLeftX:(maxRightX-1);
		minLeftY = minLeftY<maxRightY ? minLeftY:(maxRightY-1);

		if (drawRect) cvRectangle(tempFrame.image, cvPoint(minLeftX, minLeftY), cvPoint(maxRightX, maxRightY), CV_RGB(255, 0, 0), 3, CV_AA, 0);
		
		//outobj<<minLeftX<<"	"<<minLeftY<<"	"<<maxRightX-minLeftX<<"	"<<maxRightY-minLeftY<<endl; 

		rect = cvRect(minLeftX, minLeftY, maxRightX-minLeftX, maxRightY-minLeftY); 
		tempFrame.searchRect = rect;

		signelCount++;  
		cvReleaseImage(&grayImg);
		cvReleaseImage(&diffImg_2);   
		prevFrame = frame;
		lastFrame = tempFrame; 

		if(signelCount == groupCount)//如果连续检测到5个单人的情况，分组结束 
		{
			signelCount = 0; 
			return true;
		}
		else
		{
			return false;
		}
		

		//if((minLeftY < 360) && ((maxRightX-minLeftX) < 420))//如果检测到框为单人大小
		//{
		//	signelCount++; 
		//	cvReleaseImage(&grayImg);
		//	cvReleaseImage(&diffImg_2);  
		//	prevFrame = frame;
		//	*lastFrame = tempFrame; 

		//	if(signelCount == groupCount)//如果连续检测到5个单人的情况，分组结束 
		//	{
		//		signelCount = 0;
		//		return true;
		//	}
		//	else
		//	{
		//		return false;
		//	}	
		//}
		//else //如果检测到多人情况，每张图片分为一组
		//{
		//	return false;
		//}
		

		if((minLeftY < 360) && ((maxRightX-minLeftX) < 420))//如果检测到框为单人大小
		{
			signelCount++; 
			cvReleaseImage(&grayImg);
			cvReleaseImage(&diffImg_2);  
			prevFrame = frame;
			lastFrame = tempFrame; 

			if(signelCount == groupCount)//如果连续检测到5个单人的情况，分组结束 
			{
				signelCount = 0;
				return true;
			}
			else
			{
				return false;
			}	
		}
		else //如果检测到多人情况，每张图片分为一组
		{
			signelCount = 0;
			cvReleaseImage(&grayImg);
			cvReleaseImage(&diffImg_2); 
			prevFrame = frame;
			lastFrame = tempFrame;
			return true;
		} 
	}
	else //当前帧没检测到
	{ 
		cvReleaseImage(&grayImg);
		cvReleaseImage(&diffImg_2);
		prevFrame = frame; 
		lastFrame = tempFrame;  

		if (signelCount > 0)//如果前一帧为单人，当前帧没有人
		{
			signelCount = 0; 
			return true;
		}
		else
		{
			return false;
		}
	}

}





