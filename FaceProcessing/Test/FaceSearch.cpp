#include "stdafx.h"
#include "FaceSearch.h"

int FaceSearch(IplImage* targetImg, CvRect& rect)//������,�õ���������λ�õĿ��
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

	faceSel.AddInFrame(sampleFrame);//���ش�ʶ���ͼƬ

	Target* targets = 0;
	int nTotal = faceSel.SearchFaces( &targets );//�ڴ�ʶ��ͼƬ��������
	if (nTotal == 0)//�����ǰ��ʶ��ͼƬ�У�û�ҵ���������ֱ���˳�����
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