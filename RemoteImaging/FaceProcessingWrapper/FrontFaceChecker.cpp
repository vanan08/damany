#include "StdAfx.h"
#include "FrontFaceChecker.h"

using namespace System;
using namespace System::Runtime::InteropServices;

FaceProcessingWrapper::FrontFaceChecker::FrontFaceChecker(System::String^ templateFile)
{
	IntPtr strPtr = Marshal::StringToHGlobalAnsi(templateFile);
	const char *pTemplate = static_cast<const char*>( strPtr.ToPointer() );

	this->pChecker = new frontalFaceDetect();
	this->pChecker->LoadEyeTemplate( pTemplate );

	Marshal::FreeHGlobal(strPtr);
}


FaceProcessingWrapper::FrontFaceChecker^ FaceProcessingWrapper::FrontFaceChecker::FromFile(System::String^ templateFile)
{
	return gcnew FaceProcessingWrapper::FrontFaceChecker(templateFile);
}


bool FaceProcessingWrapper::FrontFaceChecker::IsFront( OpenCvSharp::IplImage^ face )
{
	return this->pChecker->IsFrontFace( (IplImage*) face->CvPtr.ToPointer() );
}

