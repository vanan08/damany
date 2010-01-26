// The following ifdef block is the standard way of creating macros which make exporting 
// from a DLL simpler. All files within this DLL are compiled with the PREPROCESS_EXPORTS
// symbol defined on the command line. this symbol should not be defined on any project
// that uses this DLL. This way any other project whose source files include this file see 
// PREPROCESS_API functions as being imported from a DLL, whereas this DLL sees symbols
// defined with this macro as being exported.
#ifdef PREPROCESS_EXPORTS
#define PREPROCESS_API __declspec(dllexport)
#else
#define PREPROCESS_API __declspec(dllimport)
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
};

extern "C"
{
	PREPROCESS_API bool PreProcessFrame(Frame frame, Frame &lastFrame);
	PREPROCESS_API void SetDrawRect(bool draw);
	PREPROCESS_API void SetAlarmArea(const int leftX, const int leftY, const int rightX, const int rightY, bool draw);
	PREPROCESS_API void SetRectThr(const int fCount, const int gCount);
}
