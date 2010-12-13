// Test.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include "afxwin.h"
#include "Test.h"
#include "cv.h" 
#include "cxcore.h"
#include "highgui.h"   
#include "iostream"
#include "fstream"
#include "../FaceProcessing/LBP.h" 
#include "../FaceProcessing/FaceVarify.h"
#include "../FaceProcessing/FrontalFaceVarify.h" 
#include "FaceSearch.h"  

//#include "../AAM/AAM/AAM_Basic.h" 
//#include "../AAM/AAM/AAM_IC.h" 
//#include "../AAM/AAM/VJfacedetect.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#endif

CWinApp theApp;

using namespace std;

void BatchProcess()
{
	CFileFind findFile;
	CString filePath; 
	char* name;

	Damany::Imaging::FaceVarify fV;   
	IplImage* tempEye = cvLoadImage("template.jpg", CV_LOAD_IMAGE_GRAYSCALE); 
	Damany::Imaging::FrontalFaceVarify frontalFace(tempEye);  

	bool fileExist = findFile.FindFile(_T("C:\\pic\\*.jpg")); //遍历文件夹
	while (fileExist)
	{
		fileExist = findFile.FindNextFile();  
		if (!findFile.IsDots())
		{
			filePath = findFile.GetFilePath();
			name = filePath.GetBuffer(filePath.GetLength());  
			CvvImage pImg;
			pImg.Load(name); 

			IplImage* sourImg  = pImg.GetImage();//加载图片
			filePath.ReleaseBuffer(); 
			if (sourImg)
			{
				bool isFace = false;
				isFace = fV.IsFaceImg(sourImg);//判断图片中是否有人脸

				/*CvRect faceRect;
				int flag = FaceSearch(sourImg, faceRect);
				if (flag == 1)
				{
				cvSetImageROI(sourImg, faceRect);
				static int count = 0;
				char add[200];
				sprintf(add, "c:\\picResult\\img_%d.jpg", count++);
				cvSaveImage(add, sourImg);
				cvResetImageROI(sourImg);
				}*/

				if (isFace)
				{
					static int faceCount = 0;
					char saveAdd[200];
					sprintf(saveAdd, "C:\\picResult\\face_%d.jpg", faceCount++);
					cvSaveImage(saveAdd, sourImg);
					//bool isFrontal = false; 
					//isFrontal = frontalFace.IsFrontal(sourImg);//判断图片是否是正面人脸  
					//if (isFrontal)
					//{
					//	static int frontalCount = 0; 
					//	char saveAdd2[200];
					//	sprintf(saveAdd2, "C:\\result\\bigPic\\noface\\frontal_%d.jpg", frontalCount++);
					//	cvSaveImage(saveAdd2, sourImg);
					//}
					//else
					//{
					//	static int nonFrontalCount = 0;
					//	char saveAdd3[200];
					//	sprintf(saveAdd3, "C:\\result\\bigPic\\noface\\nonFrontal_%d.jpg", nonFrontalCount++);
					//	cvSaveImage(saveAdd3, sourImg);
					//}
				}
				else
				{
					static int nonFaceCount = 0;
					char saveAdd1[200];
					sprintf(saveAdd1, "C:\\picResult\\noFace_%d.jpg", nonFaceCount++);
					cvSaveImage(saveAdd1, sourImg); 
				}
			}

			pImg.Destroy(); 
		}
	}
}

//////////////////////////////AAM functions //////////////////////////////////////////////////////////
//void ShapeResize(AAM_Shape& shape, int minX, int minY, float xScale=1, float yScale=1)
//{
//	for (int i=0; i<shape.NPoints(); i++)
//	{
//		shape[i].x -= minX;
//		shape[i].y -= minY;
//		shape[i].x *= xScale;
//		shape[i].y *= yScale;
//	}
//}
/////////////////////////////////////////////////////////////////////////////////////////////////////

//void BatchProcess(Damany::Imaging::FaceCompare::LBP& sF)
//{
//	CFileFind findFile;
//	CString filePath; 
//	char* name;
//
//	CvRect faceRect;
//	bool fileExist = findFile.FindFile(_T("C:\\AAMtraining\\test\\*.jpg")); //遍历文件夹
//	while (fileExist)
//	{
//		fileExist = findFile.FindNextFile();   
//		if (!findFile.IsDots())
//		{
//			filePath = findFile.GetFilePath();
//			name = filePath.GetBuffer(filePath.GetLength());  
//			CvvImage pImg;
//			pImg.Load(name); 
//	
//			IplImage* image  = pImg.GetImage();//加载图片
//			filePath.ReleaseBuffer(); 
//			if (image)
//			{
//				faceRect.x = 0;
//				faceRect.y  = 0;
//				faceRect.width = image->width;
//				faceRect.height = image->height;
//
//				IplImage* destGray = cvCreateImage(cvGetSize(image), 8, 1);
//				cvCvtColor(image, destGray, CV_BGR2GRAY);
//
//				float score = 0.0f;
//				sF.CmpFace(destGray, faceRect, &score); 
//				if (1)
//				{
//					char saveAdd[200];
//					static int iCount = 0;  
//					sprintf(saveAdd, "C:\\AAMtraining\\testResult\\img-%5.3f_%d.jpg", score, iCount++);  
//					cvSaveImage(saveAdd, image); 
//				}
//
//				cvReleaseImage(&destGray);
//				pImg.Destroy(); 
//			}
//		}
//	}
//}

int _tmain(int argc, TCHAR* argv[], TCHAR* envp[])
{

	/*IplImage* targetGray = cvLoadImage("C:\\AAMtraining\\test\\target.jpg", CV_LOAD_IMAGE_GRAYSCALE);
	if (!targetGray)
	{
	cout<<"target.jpg load error!!!"<<endl;
	system("pause");
	exit(1);
	}
	cvSaveImage("C:\\AAMtraining\\testResult\\target.jpg", targetGray);

	CvRect targetRect;  
	targetRect.x = 0;
	targetRect.y = 0;
	targetRect.width = targetGray->width;
	targetRect.height = targetGray->height;

	Damany::Imaging::FaceCompare::LBP sF;     
	sF.LoadImages(&targetGray, 1);     

	BatchProcess(sF);   

	cvReleaseImage(&targetGray);*/
	BatchProcess();
	system("pause");
	return 0;
}
