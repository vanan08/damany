#ifndef ILLUMINATION_H
#define ILLUMINATION_H

#include "stdafx.h"
#include "cv.h"

#ifdef DLL_EXPORTS
#define DLL_API _declspec(dllexport)
#else
#define DLL_API _declspec(dllimport)
#endif

namespace Damany {

	namespace Imaging {

		namespace FaceCompare {
class DLL_API IlluminationNorm
{
public:
	IlluminationNorm(IplImage *refImg);//≤Œøºπ‚’’
	~IlluminationNorm();
	void Norm(IplImage *colorIn, IplImage *colorOut);

private:
	int fSearchingMaxMin(const CvMat * mat, double * min, double * max);
	IplImage *refLight;
};			

		}
	}
}
#endif