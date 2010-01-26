#pragma once

#include <stdafx.h>
#include "../../FaceProcessing/FaceProcessing/facePCA.h"

using namespace System;
using namespace System::Runtime::InteropServices;

namespace FaceProcessingWrapper {

	public ref class RecognizeResult
	{
	public:
		property float Similarity
		{
			float get()
			{
				return _similarity;
			}
			void set(float v)
			{
				_similarity = v;
			}
		}

		property int FileIndex
		{
			int get()
			{
				return _fileIndex;
			}
			void set(int v)
			{
				_fileIndex = v;
			}
		}

	private:
		float _similarity;
		int _fileIndex;

	};



	public ref class PCA
	{
	public:
		~PCA()
		{
			if (pPCA != NULL)
			{
				delete pPCA;
				pPCA = NULL;
			}
		}

		static PCA^ LoadFrom(String^ path)
		{
			return gcnew PCA(path);
		}

		void Train(int imgWidth, int imgHeight, int eigenNum)
		{
			this->pPCA->FaceTraining(imgWidth, imgHeight, eigenNum);
		}

		array<RecognizeResult^>^ Recognize(array<float>^ faceBitmap)
		{
			this->pPCA->FaceRecognition()

			return gcnew array<RecognizeResult^>(30);

		}

		String^ GetFileName(int index)
		{
			return "";
		}

	private:
		PCA(String^ path)
		{
			IntPtr pathPtr = Marshal::StringToHGlobalAnsi(path);
			const char* pPath = static_cast<const char*>(pathPtr.ToPointer());
			try
			{
				this->pPCA = new FacePCA(pPath);
				this->pPCA->Load();
			}
			finally
			{
				Marshal::FreeHGlobal(pathPtr);
			}
			
		}


		FacePCA *pPCA;

	};

}