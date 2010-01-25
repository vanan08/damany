#pragma once

#include "..\..\FaceSvm\FaceSvm\FrontFaceChecker.h"

namespace FaceProcessingWrapper
{
	public ref class FrontFaceChecker
	{
	public:
		static FrontFaceChecker^ FromFile(System::String^ templateFile);
		bool IsFront( OpenCvSharp::IplImage^ face );

	private:
		FrontFaceChecker(System::String^ templateFile);
		frontalFaceDetect *pChecker;
	};
}

