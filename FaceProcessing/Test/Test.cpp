// Test.cpp : Defines the entry point for the console application.
#include "stdafx.h"
#include "afxwin.h"
#include "cv.h" 
#include "highgui.h"
#include "iostream"
#include "fstream"
#include "../FaceProcessing/FaceAlignment.h"
#include "../FaceProcessing/LBP.h"
#include <vector>
#include <string>
using namespace std;
typedef std::vector<std::string> file_lists;

bool IsSymmetry(IplImage *grayImg)
{
	IplImage* grayImg2 = cvCreateImage(cvGetSize(grayImg), 8, 1);
	cvFlip(grayImg, grayImg2, 1); 

	int height = grayImg->height;
	int width = grayImg->width;
	int step = grayImg->widthStep;
	uchar* data1 = (uchar*)grayImg->imageData;
	uchar* data2 = (uchar*)grayImg2->imageData;

	int score = 0;
	for (int i=0; i<height; i++)
	{
		for (int j=0; j<width; j++)
		{
			if (abs(data1[i*step+j] - data2[i*step+j]) < 8)
			{
				score++;
			}
		}
	}

	cvReleaseImage(&grayImg2); 

	float percent = float(score)/float(70*70);

	if ( percent < 0.5) 
	{
		return false; 
	}
	return true;
	//return percent;
}

void ReadPts(char* ptsFileName, CvPoint fPt[])
{
	ifstream fileIn;
	fileIn.open(ptsFileName);
	string temp;
	getline(fileIn, temp);
	getline(fileIn, temp);
	getline(fileIn, temp);

	for (int i=0; i<68; i++)
	{
		fileIn>>fPt[i].x>>fPt[i].y;
	}
	getline(fileIn, temp);
	fileIn.close();
}

void BatchTest()
{
	CFileFind findFile;
	CString filePath; 
	char* name;
	
	char beginTime[9];
	_strtime(beginTime);
	
	Damany::Imaging::FaceCompare::FaceAlignment fa("D:\\01\\example\\model.txt", 
			"D:\\01\\example\\haarcascade_frontalface_alt2.xml");
	Damany::Imaging::FaceCompare::LBP faceCmp;

	IplImage *tarImg[3];
	tarImg[0] = cvLoadImage("C:\\faceLib\\0009.jpg");
	tarImg[1] = cvLoadImage("C:\\faceLib\\NO.06.jpg");
	tarImg[2] = cvLoadImage("C:\\faceLib\\NO.05.jpg");

	IplImage *tarLbp[3];
	for (int i=0; i<3; i++)
	{
	tarLbp[i] = cvCreateImage(cvSize(70, 70), 8, 1);
	}

	CvPoint fpt[68];
	ReadPts("C:\\faceLib\\0009.jpg.pts", fpt);
	fa.LibFaceAlignment(tarImg[0], tarLbp[0], fpt);
	ReadPts("C:\\faceLib\\NO.06.jpg.pts", fpt);
	fa.LibFaceAlignment(tarImg[1], tarLbp[1], fpt);
	ReadPts("C:\\faceLib\\NO.05.jpg.pts", fpt);
	fa.LibFaceAlignment(tarImg[2], tarLbp[2], fpt);

	faceCmp.LoadImages(tarLbp, 3);

	/*for (int i=0; i<3; i++)
	{
		static int s_libCount = 0;
		char libAdd[200];
		sprintf(libAdd, "C:\\faceLib\\LBP\\img_%d.jpg", s_libCount++);
		cvSaveImage(libAdd, tarLbp[i]);
	}*/

	for (int i=0; i<3; i++)
	{
		cvReleaseImage(&tarImg[i]);
	}


	bool fileExist = findFile.FindFile(_T("E:\\face\\testPictures\\TestGroup27\\*.jpg")); //遍历文件夹
	float score = 0.0f;
	while (fileExist)
	{
		fileExist = findFile.FindNextFile();  
		if (!findFile.IsDots())
		{
			filePath = findFile.GetFilePath();
			name = filePath.GetBuffer(filePath.GetLength());  
			CvvImage pImg;
			pImg.Load(name); 
			cout<<"**********************************\n";
			cout<<name<<endl;
	
			score = 0.0f;
			IplImage *srcImg  = pImg.GetImage();//加载图片
			IplImage *faceLbp = cvCreateImage(cvSize(70,70), 8, 1);
			cvZero(faceLbp);
			bool alignFlag = fa.RealTimeAlignment(srcImg, faceLbp);
			if (alignFlag)
			{
				/*static int s_count = 0;
				char add[200];
				sprintf(add, "E:\\face\\testPictures\\jiangSuTest\\img_%d.jpg", s_count++);
				cvSaveImage(add, faceLbp);*/

				float score[3];
				float thr = 75.0f;
				faceCmp.CmpFace(faceLbp, score);
				if (score[0] > thr)
				{
					static int s_count1 = 0;
					char add1[200];
					sprintf(add1, "E:\\face\\testPictures\\TestGroup27\\result\\04\\img_%f_%d.jpg", score[0], s_count1++);
					//cvSaveImage(add1, srcImg);
				}
				if (score[1] > thr)
				{
					static int s_count2 = 0;
					char add2[200];
					sprintf(add2, "E:\\face\\testPictures\\TestGroup27\\result\\05\\img_%f_%d.jpg", score[1], s_count2++);
					//cvSaveImage(add2, srcImg);
				}
				if (score[2] > thr)
				{
					static int s_count3 = 0;
					char add3[200];
					sprintf(add3, "E:\\face\\testPictures\\TestGroup27\\result\\02\\img_%f_%d.jpg", score[2], s_count3++);
					//cvSaveImage(add3, srcImg);
				}
			}
	
			cvReleaseImage(&faceLbp);
			pImg.Destroy();
		}
	}
}

static int str_compare(const void *arg1, const void *arg2)
{
	return strcmp((*(std::string*)arg1).c_str(), (*(std::string*)arg2).c_str());//比较字符串arg1 and arg2
}

file_lists ScanNSortDirectory(const std::string &path, const std::string &extension)
{
    WIN32_FIND_DATA wfd;//WIN32_FIND_DATA:Contains information about the file that is found by the 
					   //FindFirstFile, FindFirstFileEx, or FindNextFile function
    HANDLE hHandle;
    string searchPath, searchFile;
    file_lists vFilenames;
	int nbFiles = 0;
    
	searchPath = path + "/*" + extension;
	hHandle = FindFirstFile(searchPath.c_str(), &wfd);//Searches a directory for a file or subdirectory
													 //with a name that matches a specific name
	if (INVALID_HANDLE_VALUE == hHandle)
    {
		fprintf(stderr, "ERROR(%s, %d): Cannot find (*.%s)files in directory %s\n",
			__FILE__, __LINE__, extension.c_str(), path.c_str());
		exit(0);
    }
    do
    {
        //. or ..
        if (wfd.cFileName[0] == '.')
        {
            continue;
        }
        // if exists sub-directory
        if (wfd.dwFileAttributes & FILE_ATTRIBUTE_DIRECTORY)//dwFileAttributes:The file attributes of a file
        {													
//FILE_ATTRIBUTE_DIRECTORY:The handle identifies a directory
            continue;
	    }
        else//if file
        {
			searchFile = path + "/" + wfd.cFileName;
			vFilenames.push_back(searchFile);
			nbFiles++;
		}
    }while (FindNextFile(hHandle, &wfd));//Call this member function to continue a file search begun 
										//with a call to CGopherFileFind::FindFile

    FindClose(hHandle);//Closes a file search handle opened by the FindFirstFile, FindFirstFileEx,
					  //or FindFirstStreamW function

	// sort the filenames
    qsort((void *)&(vFilenames[0]), (size_t)nbFiles, sizeof(string), str_compare);//Performs a quick sort

    return vFilenames;
}

int MaxIndex(float arr[], int count)
{
	float maxVal = arr[0];
	cout<<arr[0]<<" ";
	int maxIndex = 0;
	for (int i=1; i<count; i++)
	{
		cout<<arr[i]<<" ";
		if (maxVal < arr[i])
		{
			maxVal = arr[i];
			maxIndex = i;
		}
	}
	cout<<endl;
	return maxIndex;
}

void GetAdd(char arr[], int index)
{
	switch (index)
	{
	case 0:
		{
			static int s_count0 = 0;
			sprintf(arr, "E:\\face\\testPictures\\jiangSuTest\\result\\00\\img_%d.jpg", s_count0++);
			break;
		}
	case 1:
		{
			static int s_count1 = 0;
			sprintf(arr, "E:\\face\\testPictures\\jiangSuTest\\result\\01\\img_%d.jpg", s_count1++);
			break;
		}
	case 2:
		{
			static int s_count2 = 0;
			sprintf(arr, "E:\\face\\testPictures\\jiangSuTest\\result\\02\\img_%d.jpg", s_count2++);
			break;
		}
	case 3:
		{
			static int s_count3 = 0;
			sprintf(arr, "E:\\face\\testPictures\\result\\result\\03\\img_%d.jpg", s_count3++);
			break;
		}
	case 4:
		{
			static int s_count4 = 0;
			sprintf(arr, "E:\\face\\testPictures\\result\\result\\04\\img_%d.jpg", s_count4++);
			break;
		}
	case 5:
		{
			static int s_count5 = 0;
			sprintf(arr, "E:\\face\\testPictures\\result\\result\\05\\img_%d.jpg", s_count5++);
			break;
		}
	case 6:
		{
			static int s_count6 = 0;
			sprintf(arr, "E:\\face\\testPictures\\result\\result\\06\\img_%d.jpg", s_count6++);
			break;
		}
	case 7:
		{
			static int s_count7 = 0;
			sprintf(arr, "E:\\face\\testPictures\\result\\result\\07\\img_%d.jpg", s_count7++);
			break;
		}
	case 8:
		{
			static int s_count8 = 0;
			sprintf(arr, "E:\\face\\testPictures\\result\\result\\08\\img_%d.jpg", s_count8++);
			break;
		}
	case 9:
		{
			static int s_count9 = 0;
			sprintf(arr, "E:\\face\\testPictures\\result\\result\\09\\img_%d.jpg", s_count9++);
			break;
		}
		
	}
}

void LbpTest()
{
	const int c_faceCount = 3;
	IplImage *tarLbp[c_faceCount];
	tarLbp[0] = cvLoadImage("C:\\faceLib\\LBP\\img_0.jpg", CV_LOAD_IMAGE_GRAYSCALE);
	tarLbp[1] = cvLoadImage("C:\\faceLib\\LBP\\img_1.jpg", CV_LOAD_IMAGE_GRAYSCALE);
	tarLbp[2] = cvLoadImage("C:\\faceLib\\LBP\\img_2.jpg", CV_LOAD_IMAGE_GRAYSCALE);
	/*tarLbp[3] = cvLoadImage("C:\\faceLib\\LBP\\libLbp_3.jpg", CV_LOAD_IMAGE_GRAYSCALE);
	tarLbp[4] = cvLoadImage("C:\\faceLib\\LBP\\libLbp_4.jpg", CV_LOAD_IMAGE_GRAYSCALE);
	tarLbp[5] = cvLoadImage("C:\\faceLib\\LBP\\libLbp_5.jpg", CV_LOAD_IMAGE_GRAYSCALE);
	tarLbp[6] = cvLoadImage("C:\\faceLib\\LBP\\libLbp_6.jpg", CV_LOAD_IMAGE_GRAYSCALE);
	tarLbp[7] = cvLoadImage("C:\\faceLib\\LBP\\libLbp_7.jpg", CV_LOAD_IMAGE_GRAYSCALE);
	tarLbp[8] = cvLoadImage("C:\\faceLib\\LBP\\libLbp_8.jpg", CV_LOAD_IMAGE_GRAYSCALE);
	tarLbp[9] = cvLoadImage("C:\\faceLib\\LBP\\libLbp_9.jpg", CV_LOAD_IMAGE_GRAYSCALE);*/

	Damany::Imaging::FaceCompare::LBP faceCmp; 
	faceCmp.LoadImages(tarLbp, c_faceCount);

	file_lists files = ScanNSortDirectory("E:/face/testPictures/jiangSuTest", ".jpg");
	if (files.empty())
	{
		cout<<"no file find in current directory..."<<endl;
		system("pause");
		exit(-1);
	}

	float score[c_faceCount];
	for (int i=0; i<files.size(); i++)
	{
		//cout<<files[i].c_str()<<endl;
		IplImage *img = cvLoadImage(files[i].c_str(), CV_LOAD_IMAGE_GRAYSCALE);
		faceCmp.CmpFace(img, score);

		int index = MaxIndex(score, c_faceCount);
		char add[200];
		GetAdd(add, index);
		if (score[index] > 85)
		{
			cvSaveImage(add, img);
		}
		//cvSaveImage(add, img);

		cvReleaseImage(&img); 
	}
	files.clear();

	for (int i=0; i<c_faceCount; i++)
	{
		cvReleaseImage(&tarLbp[i]);
	}
}

int _tmain(int argc, TCHAR* argv[], TCHAR* envp[])
{
	BatchTest();
	//LbpTest();

	system("pause");
	return 0;
}
