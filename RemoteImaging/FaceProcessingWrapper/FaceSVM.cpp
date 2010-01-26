// This is the main DLL file.

#include "stdafx.h"
#include "FaceSVM.h"

using namespace System::Runtime::InteropServices;

FaceProcessingWrapper::SVM^ FaceProcessingWrapper::SVM::LoadFrom(System::String^ directory)
{
	SVM^ svm = gcnew FaceProcessingWrapper::SVM(directory);
	svm->Load();

	return svm;
}


FaceProcessingWrapper::SVM::SVM(System::String^ imgRepositoryRoot)
{
	IntPtr rootDirPtr = Marshal::StringToHGlobalAnsi(imgRepositoryRoot);
	const char *pRoot = static_cast<const char*>(rootDirPtr.ToPointer());

	this->pSVM = new FaceSvm(pRoot);

	Marshal::FreeHGlobal(rootDirPtr);
}

void FaceProcessingWrapper::SVM::Load()
{
	this->pSVM->Load();
}

FaceProcessingWrapper::SVM::~SVM(void)
{
	if (this->pSVM != NULL)
	{
		delete this->pSVM;
		this->pSVM = NULL;
	}
}


void FaceProcessingWrapper::SVM::Train()
{
	this->pSVM->SvmTrain();
}

double FaceProcessingWrapper::SVM::Predict(array<float>^ faceBitMapData)
{
	int floatArraySize = sizeof(float) * faceBitMapData->Length;
	IntPtr floatArrayPtr = Marshal::AllocHGlobal(floatArraySize);
	Marshal::Copy(faceBitMapData, 0, floatArrayPtr,  faceBitMapData->Length);

	double result = this->pSVM->SvmPredict( (float*)  floatArrayPtr.ToPointer() );

	Marshal::FreeHGlobal(floatArrayPtr);

	return result;
}


