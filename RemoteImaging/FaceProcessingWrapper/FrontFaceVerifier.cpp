#pragma once

#include <stdafx.h>
#include "../../FaceProcessing/FaceProcessing/FrontalFaceVarify.h"

using namespace System;
using namespace System::Runtime::InteropServices;

namespace FaceProcessingWrapper
{
	public ref class FrontFaceVerifier
	{
	public:
		FrontFaceVerifier(OpenCvSharp::IplImage^ faceTemplate)
		{
			IplImage *pIpl = (IplImage *) faceTemplate->CvPtr.ToPointer();

			pVerifier = new Damany::Imaging::FrontalFaceVarify(pIpl);
		}

		bool IsFrontFace(OpenCvSharp::IplImage^ face)
		{
			return true;

		}

	private:
		Damany::Imaging::FrontalFaceVarify *pVerifier;

	};
 
}