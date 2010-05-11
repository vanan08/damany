#pragma once

#include "StdAfx.h"
#include "..\..\FaceProcessing\FaceProcessing\FrontFaceChecker.h"

using namespace System;
using namespace System::Runtime::InteropServices;


namespace FaceProcessingWrapper
{
	public ref class FrontFaceChecker
	{
	public:
		static FrontFaceChecker^ FromFile(System::String^ templateFile)
		{
			return gcnew FaceProcessingWrapper::FrontFaceChecker(templateFile);
		}

		bool IsFront( OpenCvSharp::IplImage^ face )
		{
			return this->pChecker->IsFrontFace( (IplImage*) face->CvPtr.ToPointer() );
		}

	private:
		FrontFaceChecker(System::String^ templateFile)
		{
			IntPtr strPtr = Marshal::StringToHGlobalAnsi(templateFile);
			const char *pTemplate = static_cast<const char*>( strPtr.ToPointer() );

			this->pChecker = new frontalFaceDetect();
			this->pChecker->LoadEyeTemplate( pTemplate );

			Marshal::FreeHGlobal(strPtr);
		}


		frontalFaceDetect *pChecker;
	};
}



