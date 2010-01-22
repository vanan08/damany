// FaceSVMWrapper.h

#pragma once
#include "../../FaceSvm/FaceSvm/faceSVM.h"


using namespace System;

namespace FaceSVMWrapper {

	public ref class SVM
	{
		// TODO: Add your methods for this class here.
	public:
		~SVM(void);
		static SVM^ LoadFrom(System::String^ directory);
		void Train();//SVMÑµÁ·º¯Êý
		double Predict(array<float>^ faceBitMapData);//SVMÔ¤²âº¯Êý



	private:
		SVM(System::String^ imgRepositoryRoot);
		void Load();

		FaceSvm *pSVM;
	};
}
