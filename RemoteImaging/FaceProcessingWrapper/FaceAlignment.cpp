#pragma once

#include <stdafx.h>
#include "../../FaceProcessing/FaceProcessing/FaceAlignment.h"

using namespace System;
using namespace System::Runtime::InteropServices;

namespace FaceProcessingWrapper {

	public ref class FaceAlignmentWrapper {
	public:
		FaceAlignmentWrapper(String^ modelPath, String^ classifierPath, int featurePointCount)
		{
			IntPtr modelPtr = Marshal::StringToHGlobalAnsi(modelPath);
			IntPtr classifierPtr = Marshal::StringToHGlobalAnsi(classifierPath);

			try
			{
				m_pAlignment = new Damany::Imaging::FaceCompare::FaceAlignment(
												(char*)modelPtr.ToPointer(),
												(char*)classifierPtr.ToPointer(),
												featurePointCount);
			}
			finally
			{
				Marshal::FreeHGlobal(modelPtr);
				Marshal::FreeHGlobal(classifierPtr);
			}
		}
		~FaceAlignmentWrapper()
		{
			if (m_pAlignment != NULL)
			{
				delete m_pAlignment;
				m_pAlignment = NULL;
			}
		}

		bool LibFaceAlignment(OpenCvSharp::IplImage^ faceImg, OpenCvSharp::IplImage^ faceLbpImg, array<OpenCvSharp::CvPoint>^ featurePt)
		{
			CvPoint* pPoint = new CvPoint[featurePt->Length];
			for (int i=0; i<featurePt->Length; ++i)
			{
				pPoint->x = featurePt[i].X;
				pPoint->y = featurePt[i].Y;
			}

			try
			{
			    return	m_pAlignment->LibFaceAlignment((IplImage*)faceImg->CvPtr.ToPointer(), (IplImage*)faceLbpImg->CvPtr.ToPointer(), pPoint);
			}
			finally
			{
				delete[] pPoint;
			}
			
		}


		bool RealTimeAlignment(OpenCvSharp::IplImage^ faceImg, OpenCvSharp::IplImage^ faceLbpImg)
		{
			return m_pAlignment->RealTimeAlignment(
				                (IplImage*) faceImg->CvPtr.ToPointer(),
								(IplImage*) faceLbpImg->CvPtr.ToPointer()) ;
		}


	private:
		Damany::Imaging::FaceCompare::FaceAlignment *m_pAlignment;
	};
}