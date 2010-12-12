#pragma once

#include <stdafx.h>
#include "../../FaceProcessing/FaceProcessing/Illumination.h"

using namespace System;
using namespace System::Runtime::InteropServices;

namespace FaceProcessingWrapper {

	public ref class IlluminationWrapper {
	public:
		IlluminationWrapper(OpenCvSharp::IplImage^ referenceImg)
		{
			m_pIlluminator = new Damany::Imaging::FaceCompare::IlluminationNorm(
											 (IplImage*) referenceImg->CvPtr.ToPointer() );

		}
		~IlluminationWrapper()
		{
			if (m_pIlluminator)
			{
				delete m_pIlluminator;
			}

		}

		void Process(OpenCvSharp::IplImage^ toProcess, OpenCvSharp::IplImage^ processed)
		{
			m_pIlluminator->Norm( (IplImage*) toProcess->CvPtr.ToPointer(), 
									(IplImage*) processed->CvPtr.ToPointer());
		}



	private:
		Damany::Imaging::FaceCompare::IlluminationNorm *m_pIlluminator;
	};
}