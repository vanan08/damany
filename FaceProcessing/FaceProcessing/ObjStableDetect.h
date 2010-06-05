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
������֡�Ŀ��Ƿ��ȶ�(ʱ����ص�����ж�)������ȶ������m_StableOjbRects��
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




