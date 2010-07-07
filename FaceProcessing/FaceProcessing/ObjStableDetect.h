#pragma once

#ifdef DLL_EXPORTS
#define DLL_API _declspec(dllexport)
#else
#define DLL_API _declspec(dllimport)
#endif



#include "cv.h"
#include <list>
using namespace std;

typedef struct
{
    time_t *lastRectTime;
    list<CvRect> objRects;
}LastFrameObjRect;

/*********************************************************
检测给定帧的框是否稳定(时间跟重叠面积判断)，如果稳定则放入m_StableOjbRects中
***********************************************************/
class DLL_API CObjStableDetect
{
public:
    CObjStableDetect();
    ~CObjStableDetect();

    void StableDetect(CvSeq* objRects);
    void GetStableObjRects(list<CvRect> &stableObjRects);
private:
    LastFrameObjRect m_LastFrameObjRect;
    list<CvRect> m_StableObjRects;
};




