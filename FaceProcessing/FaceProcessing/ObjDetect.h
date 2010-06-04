#pragma once

#ifdef DLL_EXPORTS
#define DLL_API _declspec(dllexport)
#else
#define DLL_API _declspec(dllimport)
#endif

#include "cv.h"

namespace Kings {
	namespace Imaging {
		class DLL_API CBkg//找目标的类
		{
		public:
			CBkg( void );
			virtual ~CBkg(void);
			void SetBkg( IplImage* pFrame );//设置pFrame进入背景，请确保SetBkg与FrameProcess不会同时被调用
			void FrameProcess( IplImage* pFrame );//处理当前帧
			void GetObjRects( CvSeq* &objRects )//获取目标框，该CvSeq中存放的是CvRect型变量，该指针的内存由类进行管理，请勿在外部释放
			{
				objRects =  m_objRects;
			}
			void SetBkgUpRatio( float fUpRatio )//设置背景更新系数
			{
				m_fBkgUpRatio = fUpRatio;
			}
			void SetROI( CvRect roi )//请在第一次调用FrameProcess前进行设置，否则该设置不生效
			{
				if( m_nFrmNum == 0 )
				{
					m_ROI = roi;
					m_bSetROI = true;
				}
			}
			void SetUpBkgMode( bool bNonUpObj )//设置是否对目标区应用背景更新算法，true：不对目标区进行更新
			{
				m_bOjbMask = bNonUpObj;
			}

			//以下接口函数主要用作显示、调试目的
			IplImage* GetForeground( /*IplImage* &pFrImg*/ )//获取前景信息,该指针指向类成员变量，请勿在外部释放
			{
				//pFrImg = m_pFrImg;
				return m_pFrImg;
				//return m_pDifBetFrmsImg;
			}
			IplImage* GetBackground(   )//获取背景信息
			{
				//kImg = m_pBkImg;
				return m_pBkImg;
			}

		private:
			IplImage* m_pFrImg;//目标（最终结果）
			IplImage* m_pBkImg;//背景
			IplImage* m_pDifImg;//差分结果
			IplImage* m_pShadowMask;//阴影模版
			IplImage* m_pObjectMask;//目标模版
			IplImage* m_pCurImg;

			IplImage* m_pBufPreImg;//前一帧
			CvMat* m_pBufPreMat;
			IplImage* m_pBufDifBetFrmsImg;
			CvMat* m_pBufDifBetFrmsMat;


			CvMat* m_pFrameMat;
			CvMat* m_pFrMat;
			CvMat* m_pBkMat;
			CvMat* m_pBkMat_Pre;//背景备份

			IplImage* m_pPreImg;
			CvMat* m_pPreMat;
			IplImage* m_pDifBetFrmsImg;//帧间差分结果
			CvMat* m_pDifBetFrmsMat;

			int m_nFrmNum;
			CvMat* SerializeMat(IplImage *pSrcImg, IplImage *pMask);
			bool ShadowJudge(CvRect r, IplImage *pDiffImg, IplImage *pBkgImg, IplImage *pCurImg);

		private:
			CvMemStorage* m_storageObjs; // temporary storage
			CvSeq* m_objcontour;
			CvSeq* m_objRects;

		private:
			bool JudgeNonShadow( IplImage* imgDifRes, CvRect roi );
			IplImage* GetSubImage(IplImage* pOriImage, CvRect roi);

		private:
			float m_fBkgUpRatio;

		private:
			CvRect m_ROI;
			bool m_bSetROI;
			void Process( IplImage* pFrame );

		private:
			bool m_bOjbMask;

		private:
			bool transferLocalRc2SceneRc( CvRect* rcInScene, CvRect* rcLocal, CvRect* rcRgn );

		private:
			void RemoveIntersectedRects( CvSeq* pObjRects );

		private:
			int CountPts( IplImage* pSrcImg );
			bool DynThreshold( const CvMat* src, const CvMat* refer, IplImage* dst, float fThRatio, float fThMin, float fThMax );
			void RemoveShadow(IplImage* pDiffImg, CvMat *pBkgMat, CvMat *pCurMat);

		private:
			void InitEnvironment( IplImage* pFrame );
		};

	}
}

