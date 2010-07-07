#include "stdafx.h"
#include "FaceSearch.h"

int FaceSearch(IplImage* targetImg, CvRect& rect)//找人脸,得到人脸所在位置的框框
{
	static CFaceSelect faceSel; 
	Frame sampleFrame;
	sampleFrame.cameraID = 1; 
	sampleFrame.image = targetImg;//cvCreateImage(cvSize(targetImg->width, targetImg->height), 8, 3);
	//cvCopy(targetImg, sampleFrame.image);

	sampleFrame.searchRect = cvRect(0, 0, 0, 0);  
	/*sampleFrame.searchRect.x = 0;
	sampleFrame.searchRect.y = 0;
	sampleFrame.searchRect.width = targetImg->width;
	sampleFrame.searchRect.height = targetImg->height; */

	faceSel.AddInFrame(sampleFrame);//加载待识别的图片

	Target* targets = 0;
	int nTotal = faceSel.SearchFaces( &targets );//在待识别图片中找人脸
	if (nTotal == 0)//如果当前待识别图片中，没找到人脸，则直接退出程序
	{
		return 0;
	}

	for( int i = 0; i < nTotal; i++ )
	{
		for( int j = 0; j < targets[i].FaceCount; j++ )
		{
			rect = targets[i].FaceOrgRects[j]; 
			cvReleaseImage(&targets->FaceData[j]); 
		}
	}
	//cvReleaseImage(&sampleFrame.image);
	faceSel.ReleaseTargets();
	return 1;
}