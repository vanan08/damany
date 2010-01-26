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
			similarityMat *pResult = NULL;
			int resultCount = 0;
			pin_ptr<float> pFace = &faceBitmap[0];

			try
			{
				this->pPCA->FaceRecognition(pFace, pResult, resultCount);

				array<RecognizeResult^>^ result = gcnew array<RecognizeResult^>(resultCount);

				for (int i=0;i<resultCount;++i)
				{
					result[i] = gcnew RecognizeResult();
					result[i]->FileIndex = pResult[i].index;
					result[i]->Similarity = pResult[i].similarity;
				}

				return result;
			}
			finally
			{
				if (pResult != NULL)
				{
					Marshal::FreeCoTaskMem( (IntPtr) pResult  );
				}
			}

		}

		String^ GetFileName(int index)
		{
			CString str = this->pPCA->GetFileName(index);

			return gcnew String( str );
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