#ifndef _FACESEL_IF_DATASTRUCT_H_
#define _FACESEL_IF_DATASTRUCT_H_

#include <windows.h>




#define GUI_LEN 16

struct Frame
{
	IplImage *image;//cv 转换后的图片
	CvRect searchRect;//搜索脸的范围
	BYTE guid[GUI_LEN];
};



struct Target
{
	Frame BaseFrame;//大图片
	int FaceCount;//脸数量
	IplImage** FaceData;//脸数据
	CvRect* FaceRects;//脸对应框的位置,20090827 Added for Record Face Positions
	CvRect* FaceOrgRects;//对脸部矩形框扩展前的位置,20090929 Added for Face Recognition Purpose
};

struct ImageArray
{
	int nImageCount;
	IplImage** imageArr;
};
#endif