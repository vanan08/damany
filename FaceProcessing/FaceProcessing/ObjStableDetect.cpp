#include "Stdafx.h"
#include "ObjStableDetect.h"

CObjStableDetect::CObjStableDetect()
{
    m_LastFrameObjRect.lastRectTime = NULL;
    m_LastFrameObjRect.objRects.clear();

    m_StableObjRects.clear();
}

CObjStableDetect::~CObjStableDetect()
{
    if ( m_LastFrameObjRect.lastRectTime != NULL)
    {
       
        delete[]  m_LastFrameObjRect.lastRectTime;
        m_LastFrameObjRect.lastRectTime = NULL;

    }   
    m_LastFrameObjRect.objRects.clear();

    m_StableObjRects.clear();
}

void CObjStableDetect::StableDetect(CvSeq* objRects)
{
    //清除上一帧的稳定框
    m_StableObjRects.clear();

    //无框
    if (objRects == NULL)
    {
        if (m_LastFrameObjRect.lastRectTime != NULL)
        {
            delete[] m_LastFrameObjRect.lastRectTime;
            m_LastFrameObjRect.lastRectTime = NULL;
        }
        m_LastFrameObjRect.objRects.clear();
        return;
    }
    if (objRects->total <= 0)
    {
        if (m_LastFrameObjRect.lastRectTime != NULL)
        {
            delete[] m_LastFrameObjRect.lastRectTime;
            m_LastFrameObjRect.lastRectTime = NULL;
        }
        m_LastFrameObjRect.objRects.clear();
        return;
    }


    //有框
    float fThreRatio = 0.8f;

    time_t *nowRectTime = new time_t[objRects->total];

    //printf("\n");
    //本帧与上次帧矩形重叠超过90%，则记录以前的时
    for (int i=0; i<objRects->total; i++)
    {
        
        nowRectTime[i] = time(0);
        //printf("current frame time=%ld\n", nowRectTime[i]);
        CvRect *r1 = (CvRect*) cvGetSeqElem(objRects, i);
        int Area1 = r1->width * r1->height;

        //printf("Area1=%d\n", Area1);

        int j = 0;
        list<CvRect>::iterator r2;
        for (r2=m_LastFrameObjRect.objRects.begin(); 
            r2!=m_LastFrameObjRect.objRects.end(); 
            ++r2) 
        {
            int Area2 = r2->width * r2->height;
            //printf("Area2=%d\n", Area2);
            int x1 = max( r1->x, r2->x );
            int y1 = max( r1->y, r2->y );
            int x2 = min( r1->x + r1->width, r2->x + r2->width);
            int y2 = min( r1->y + r1->height, r2->y + r2->height);

            if( x1 < x2 && y1 < y2 )
            {
                int intersectArea = ( x2 - x1 ) * ( y2 - y1 );
                //printf("Area1=%d, Area2=%d, intersectArea=%d\n", Area1, Area2, intersectArea);
                if( intersectArea > fThreRatio * Area1 
                    && intersectArea > fThreRatio * Area2 )
                {
                    nowRectTime[i] = m_LastFrameObjRect.lastRectTime[j];
                    //printf("intersectArea>N, record last frame time, rect[%d]=%d\n", j, m_LastFrameObjRect.lastRectTime[j]);
                }
            }
            else
            {
                //printf("Area1=%d, Area2=%d, intersectArea=0\n", Area1, Area2);
            }

            j++;
        }
    }
    //printf("\n");

    //清空上次的框与时
    m_LastFrameObjRect.objRects.clear();
    if (m_LastFrameObjRect.lastRectTime != NULL)
    {
        delete[] m_LastFrameObjRect.lastRectTime;
        m_LastFrameObjRect.lastRectTime = NULL;
    }

    //记录本次的框与时
    m_LastFrameObjRect.lastRectTime = nowRectTime;

    for (int i=0; i<objRects->total; i++)
    {
        CvRect *rect = (CvRect*) cvGetSeqElem(objRects, i);
        m_LastFrameObjRect.objRects.push_back(*rect);

        time_t t = time(0);
        if (t - nowRectTime[i] > 3)
        {
            //printf(">3 now=%ld, last frame time=%ld\n", t, nowRectTime[i]);
            m_StableObjRects.push_back(*rect);
        }
    }

    return;
}

void CObjStableDetect::GetStableObjRects(list<CvRect> &stableObjRects)
{
    stableObjRects = m_StableObjRects;
}

