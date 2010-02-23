// The following ifdef block is the standard way of creating macros which make exporting 
// from a DLL simpler. All files within this DLL are compiled with the PREPROCESS_EXPORTS
// symbol defined on the command line. this symbol should not be defined on any project
// that uses this DLL. This way any other project whose source files include this file see 
// PREPROCESS_API functions as being imported from a DLL, whereas this DLL sees symbols
// defined with this macro as being exported.
#ifdef DLL_EXPORTS
#define DLL_API _declspec(dllexport)
#else
#define DLL_API _declspec(dllimport)
#endif


#include "highgui.h"
#include "cv.h"
#include "omp.h"

struct Frame
{
	BYTE cameraID;
	IplImage *image;//转换后需要搜索人脸的大图片,薛晓利给出
	CvRect searchRect;//搜索脸的范围，薛晓利给出
	LONGLONG timeStamp;//沈斌给出

	Frame()
	{
		searchRect = CvRect();
		image = NULL;
	}
};


class DLL_API CMotionDetector
{
public:
	CMotionDetector()
	{
		this->firstFrmRec = false;
		this->secondFrmRec = false;
		xLeftAlarm = 100; //定义并初始化警戒区域的两个坐标点位置
		yTopAlarm = 400;
		xRightAlarm = 600;
		yBottomAlarm = 500;  

		minLeftX = 3000;//定义并初始化框框的两个坐标点位置
		minLeftY = 3000; 
		maxRightX = 0;
		maxRightY = 0;

		faceCount = 500; //画框的阈值
		groupCount = 5;//分组图片个数
		signelCount = 0;//记录当前分组中的图片数量

		drawAlarmArea = false;//标志是否画出警戒区域
		drawRect = false; //标志是否画框
	}

	bool PreProcessFrame(Frame frame, Frame &lastFrame);
	void SetDrawRect(bool draw);
	void SetAlarmArea(const int leftX, const int leftY, const int rightX, const int rightY, bool draw);
	void SetRectThr(const int fCount, const int gCount);


private:
	void FindRectX(IplImage *img, const int leftY, const int rightY);
	void FindRectY(IplImage *img, const int leftX, const int rightX);



	Frame prevFrame ;//存储上一帧的frame    

	bool firstFrmRec;//第一帧是否收到
	bool secondFrmRec;//第二帧是否收到

	IplImage *currImg;//当前帧的图片
	IplImage *lastGrayImg;//上一帧灰度图
	IplImage *lastDiffImg;//上一帧差分图的二值化图 

	int xLeftAlarm; //定义并初始化警戒区域的两个坐标点位置
	int yTopAlarm;
	int xRightAlarm;
	int yBottomAlarm;  

	int minLeftX;//定义并初始化框框的两个坐标点位置
	int minLeftY; 
	int maxRightX;
	int maxRightY;

	int faceCount; //画框的阈值
	int groupCount;//分组图片个数
	int signelCount;//记录当前分组中的图片数量

	bool drawAlarmArea;//标志是否画出警戒区域
	bool drawRect; //标志是否画框
};
