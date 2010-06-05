#pragma once

#include <stdafx.h>
#include <cxcore.h>
#include "../../FaceProcessing/FaceProcessing/ObjStableDetect.h"
#include "Converter.h"

namespace FaceProcessingWrapper
{

	public ref class StableObjectDetector
	{

	public:
		StableObjectDetector()
		{
			pDetector = new CObjStableDetect();
		}
		~StableObjectDetector()
		{
			if (pDetector != NULL)
			{
				delete pDetector;
			}
		}

		array<OpenCvSharp::CvRect>^ DetectStableObjects(array<OpenCvSharp::CvRect>^ objects)
		{
			CvSeq seq;

			for (int i=0;i<objects->Length; ++i)
			{
				CvRect *pNative = new CvRect();
				*pNative =  Converter::ToNativeRect(objects[i]);

				cvSeqPush(&seq, pNative);
			}

			pDetector->StableDetect(&seq);

			list<CvRect> stable;
			pDetector->GetStableObjRects(stable);

			size_t length = stable.size();
			array<OpenCvSharp::CvRect>^ returnRects = gcnew array<OpenCvSharp::CvRect>(length);
			int i=0; 
			for (list<CvRect>::iterator it = stable.begin(); it != stable.end(); ++it)
			{
				OpenCvSharp::CvRect mr = Converter::ToManagedRect(*it);
				returnRects[i] = mr;
				++i;
			}
			
			return returnRects;
		}

	private:
		CObjStableDetect *pDetector;

	};


}

