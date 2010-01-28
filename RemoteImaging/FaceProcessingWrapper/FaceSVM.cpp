#pragma once

#include "stdafx.h"
#include "..\..\FaceProcessing\FaceProcessing\faceSVM.h"

using namespace System;
using namespace System::Runtime::InteropServices;


namespace FaceProcessingWrapper {

	public ref class SVM
	{
		// TODO: Add your methods for this class here.
	public:
		~SVM(void)
		{
			if (this->pSVM != NULL)
			{
				delete this->pSVM;
				this->pSVM = NULL;
			}
		}

		static SVM^ LoadFrom(System::String^ directory)
		{
			SVM^ svm = gcnew SVM(directory);
			svm->Load();

			return svm;
		}

		void Train()
		{
			this->pSVM->SvmTrain();
		}

		double Predict(array<float>^ faceBitMapData)
		{
			int floatArraySize = sizeof(float) * faceBitMapData->Length;
			IntPtr floatArrayPtr = Marshal::AllocHGlobal(floatArraySize);
			Marshal::Copy(faceBitMapData, 0, floatArrayPtr,  faceBitMapData->Length);

			double result = this->pSVM->SvmPredict( (float*)  floatArrayPtr.ToPointer() );

			Marshal::FreeHGlobal(floatArrayPtr);

			return result;
		}



	private:
		SVM(System::String^ imgRepositoryRoot)
		{
			IntPtr rootDirPtr = Marshal::StringToHGlobalAnsi(imgRepositoryRoot);
			const char *pRoot = static_cast<const char*>(rootDirPtr.ToPointer());

			try
			{
				this->pSVM = new FaceSvm(pRoot);
			}
			finally
			{
				if (rootDirPtr != IntPtr.Zero)
				{
					Marshal::FreeHGlobal(rootDirPtr);
				}
			}
			
		}

		void Load()
		{
			this->pSVM->Load();
		}

		FaceSvm *pSVM;
	};

}