#pragma once

#include <stdafx.h>
#include "../../FaceProcessing/FaceProcessing/FaceVarify.h"

using namespace System;
using namespace System::Runtime::InteropServices;

namespace FaceProcessingWrapper
{
	public ref class FaceVerifier
	{
	public:
		FaceVerifier()
		{
			pVerifier = new Damany::Imaging::FaceVarify();
		}


		bool IsFace(OpenCvSharp::IplImage^ img)
		{
			IplImage *pIpl = (IplImage *) img->CvPtr.ToPointer();
			bool result = pVerifier->IsFaceImg(pIpl);
			return result;
		}


	private:
		Damany::Imaging::FaceVarify *pVerifier;


	};
}