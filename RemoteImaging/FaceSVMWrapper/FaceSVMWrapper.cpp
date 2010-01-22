// This is the main DLL file.

#include "stdafx.h"
#include "FaceSVMWrapper.h"

using namespace System::Runtime::InteropServices;

FaceSVMWrapper::SVM^ FaceSVMWrapper::SVM::LoadFrom(System::String^ directory)
{
	SVM^ svm = gcnew FaceSVMWrapper::SVM(directory);
	svm->Load();

	return svm;
}


FaceSVMWrapper::SVM::SVM(System::String^ imgRepositoryRoot)
{
	IntPtr rootDirPtr = Marshal::StringToHGlobalAnsi(imgRepositoryRoot);
	const char *pRoot = static_cast<const char*>(rootDirPtr.ToPointer());

	this->pSVM = new FaceSvm(pRoot);

	Marshal::FreeHGlobal(rootDirPtr);
}

void FaceSVMWrapper::SVM::Load()
{
	this->pSVM->Load();
}

FaceSVMWrapper::SVM::~SVM(void)
{
	if (this->pSVM != NULL)
	{
		delete this->pSVM;
		this->pSVM = NULL;
	}
}


void FaceSVMWrapper::SVM::Train()
{
	this->pSVM->SvmTrain();
}

double FaceSVMWrapper::SVM::Predict(array<float>^ faceBitMapData)
{
	int floatArraySize = sizeof(float) * faceBitMapData->Length;
	IntPtr floatArrayPtr = Marshal::AllocHGlobal(floatArraySize);
	Marshal::Copy(faceBitMapData, 0, floatArrayPtr,  faceBitMapData->Length);

	double result = this->pSVM->SvmPredict( (float*)  floatArrayPtr.ToPointer() );

	Marshal::FreeHGlobal(floatArrayPtr);

	return result;
}


