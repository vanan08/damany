#include "stdafx.h"
#include "FaceSVM.h"


FaceSvm::FaceSvm()
{
	svmAvgVector = NULL;
	svmEigenVector = NULL;
	testModel = NULL;		
	svmImgWidth = 100;
	svmImgHeight = 100;
	svmEigenNum = 20;
	svmImgLen = svmImgWidth*svmImgHeight;
}

int FaceSvm::GetGoodGuySampleCount()
{
	AFX_MANAGE_STATE(AfxGetStaticModuleState());

	CFileFind imageFile; 
	int sampleCount = 0;//ѵ��������ͼƬ����

	BOOL FileExist = imageFile.FindFile(_T("C:\\faceRecognition\\SVM\\faceSample\\*.jpg"),0); 
	while (FileExist)
	{
		FileExist = imageFile.FindNextFile();  
		if (!imageFile.IsDots()) 
		{
			sampleCount++;//�����õ�ѵ������ͼƬ������
		}
	}

	return sampleCount;
}

int FaceSvm::GetBadGuySampleCount()
{
	AFX_MANAGE_STATE(AfxGetStaticModuleState());

	CFileFind imageFile; 
	int sampleCount = 0;//ѵ��������ͼƬ����

	BOOL FileExist = imageFile.FindFile(_T("C:\\faceRecognition\\faceSample\\*.jpg"),0); 
	while (FileExist)
	{
		FileExist = imageFile.FindNextFile();  
		if (!imageFile.IsDots()) 
		{
			sampleCount++;//�����õ�ѵ������ͼƬ������
		}
	}

	return sampleCount;
}

void FaceSvm::DelSvmDataFile()
{
	fstream txtFile;
	bool txtExist = false;

	txtFile.open("C:\\faceRecognition\\SVM\\data\\AverageValue.txt");
	if (txtFile)
	{
		txtExist = true;
	}
	txtFile.close();

	if (txtExist)
	{
		remove("C:\\faceRecognition\\SVM\\data\\AverageValue.txt");//����ļ����ڣ���ɾ��
	}

	txtExist = false;
	txtFile.open("C:\\faceRecognition\\SVM\\data\\EigenVector.txt");
	if (txtFile)
	{
		txtExist = true;
	}
	txtFile.close();

	if (txtExist)
	{
		remove("C:\\faceRecognition\\SVM\\data\\EigenVector.txt");//����ļ����ڣ���ɾ��
	}

	txtExist = false;
	txtFile.open("C:\\faceRecognition\\SVM\\data\\SampleCoefficient.txt");
	if (txtFile)
	{
		txtExist = true;
	}
	txtFile.close();

	if (txtExist)
	{
		remove("C:\\faceRecognition\\SVM\\data\\SampleCoefficient.txt");//����ļ����ڣ���ɾ��
	}

	txtExist = false;
	txtFile.open("C:\\faceRecognition\\SVM\\data\\BallNorm.txt");
	if (txtFile)
	{
		txtExist = true;
	}
	txtFile.close();

	if (txtExist)
	{
		remove("C:\\faceRecognition\\SVM\\data\\BallNorm.txt");//����ļ����ڣ���ɾ��
	}

	txtExist = false;
	txtFile.open("C:\\faceRecognition\\SVM\\data\\Label.txt");
	if (txtFile)
	{
		txtExist = true;
	}
	txtFile.close();

	if (txtExist)
	{
		remove("C:\\faceRecognition\\SVM\\data\\Label.txt");//����ļ����ڣ���ɾ��
	}

	txtExist = false;
	txtFile.open("C:\\faceRecognition\\SVM\\data\\Info.txt");
	if (txtFile)
	{
		txtExist = true;
	}
	txtFile.close();

	if (txtExist)
	{
		remove("C:\\faceRecognition\\SVM\\data\\Info.txt");//����ļ����ڣ���ɾ��
	}

	txtExist = false;
	txtFile.open("C:\\faceRecognition\\SVM\\data\\Model.txt");
	if (txtFile)
	{
		txtExist = true;
	}
	txtFile.close();

	if (txtExist)
	{
		remove("C:\\faceRecognition\\SVM\\data\\Model.txt");//����ļ����ڣ���ɾ��
	}
}

void FaceSvm::WriteSvmAvgTxt(CvMat *AvgVector)
{
	ofstream out1("C:\\faceRecognition\\SVM\\data\\AverageValue.txt");//��ƽ��ֵ�������ļ���
	if (out1.fail())
	{
		cvSetErrMode(CV_ErrModeParent);
		cvGuiBoxReport(CV_StsBadArg, __FUNCTION__, "file(AverageValue.txt) write error!!", __FILE__, __LINE__, NULL);
	}
	for (int i=0; i<AvgVector->rows; i++)
	{
		out1<<(float)cvGetReal2D(AvgVector, i, 0)<<endl;
	}
	out1.close();
}

void FaceSvm::WriteSvmEigenVectorTxt(CvMat *EigenVectorFinal)
{
	ofstream out2("C:\\faceRecognition\\SVM\\data\\EigenVector.txt");//�����������������ļ���
	if (out2.fail())
	{
		cvSetErrMode(CV_ErrModeParent);
		cvGuiBoxReport(CV_StsBadArg, __FUNCTION__, "file(EigenVector.txt) write error!!!", __FILE__, __LINE__, NULL);
	}
	for (int i=0; i<EigenVectorFinal->height; i++)
	{
		for (int j=0; j<EigenVectorFinal->width; j++)
		{
			out2<<(float)cvGetReal2D(EigenVectorFinal, i, j)<<" ";
		}
		out2<<endl;
	}
	out2.close(); 
}

void FaceSvm::WriteSvmSamCoeffTxt(CvMat *resCoeff)
{
	ofstream out3("C:\\faceRecognition\\SVM\\data\\SampleCoefficient.txt");//������ͼƬ��ͶӰϵ���������ļ���
	if (out3.fail())
	{
		cvSetErrMode(CV_ErrModeParent);
		cvGuiBoxReport(CV_StsBadArg, __FUNCTION__, "file(SampleCoefficient.txt) write error!!!", __FILE__, __LINE__, NULL);
	}
	for (int i=0; i<resCoeff->height; i++)
	{
		for (int j=0; j<resCoeff->width; j++)
		{
			out3<<(float)cvGetReal2D(resCoeff, i, j)<<" ";
		}
		out3<<endl; 
	}
	out3.close();
}

void FaceSvm::BallNormSvmResCoeff(CvMat *resCoeff)
{
	float refCoeffDis = 0.0;
	for (int i=0; i<resCoeff->height; i++)//���򻯴���
	{
		refCoeffDis = 0.0;
		for (int j=0; j<resCoeff->width; j++)
		{
			refCoeffDis += pow((float)cvGetReal2D(resCoeff, i, j), 2);
		}
		refCoeffDis = sqrt(refCoeffDis);
		refCoeffDis = 1/refCoeffDis;

		for (int j=0; j<resCoeff->width; j++)
		{
			cvmSet(resCoeff, i, j, (refCoeffDis*((float)cvGetReal2D(resCoeff, i, j))));
		}
	}
}

void FaceSvm::WriteBallNormResCoeff(CvMat *resCoeff)
{
	ofstream out4("C:\\faceRecognition\\SVM\\data\\BallNorm.txt");//���򻯺�Ľ�����Ա���
	if (out4.fail())
	{
		cvSetErrMode(CV_ErrModeParent);
		cvGuiBoxReport(CV_StsBadArg, __FUNCTION__, "file(BallNorm.txt) write error!!!", __FILE__, __LINE__, NULL);
	}
	for (int i=0; i<resCoeff->height; i++)
	{
		for (int j=0; j<resCoeff->width; j++)
		{
			out4<<(float)cvGetReal2D(resCoeff, i, j)<<" ";
		}
		out4<<endl; 
	}
	out4.close();
}

void FaceSvm::WriteLabel(int *label, int sampleCount)
{
	ofstream out5("C:\\faceRecognition\\SVM\\data\\Label.txt");
	if (out5.fail())
	{
		cvSetErrMode(CV_ErrModeParent);
		cvGuiBoxReport(CV_StsBadArg, __FUNCTION__, "file(Label.txt) write error!!!", __FILE__, __LINE__, NULL);
	}
	for (int i=0; i<sampleCount; i++)
	{
		out5<<label[i]<<endl; 
	}
	out5.close();
}

void FaceSvm::WriteSvmInfo(int imgWidth, int imgHeight, int eigenNum, int sampleCount)
{
	ofstream out6("C:\\faceRecognition\\SVM\\data\\Info.txt");
	if (out6.fail())
	{
		cvSetErrMode(CV_ErrModeParent);
		cvGuiBoxReport(CV_StsBadArg, __FUNCTION__, "file(Info.txt) write error!!!", __FILE__, __LINE__, NULL);
	}
	out6<<imgWidth<<endl;
	out6<<imgHeight<<endl;
	out6<<eigenNum<<endl;
	out6<<sampleCount<<endl;
	out6.close();
}

void FaceSvm::PCAforSVM(int imgWidth, int imgHeight, int eigenNum)
{
	int sampleCount = GetBadGuySampleCount() + GetGoodGuySampleCount(); 

	if (eigenNum > sampleCount)
	{
		cvSetErrMode(CV_ErrModeParent);
		cvGuiBoxReport(CV_StsBadArg, __FUNCTION__, "sampleCount should greater than(or equal to) eigenNum", __FILE__, __LINE__, NULL);
	}

	CvMat *faceDB = cvCreateMat(imgWidth*imgHeight, sampleCount, CV_32FC1);//ѵ������������ɵľ���ÿ������Ϊһ��
	CvMat *selEigenVector = cvCreateMat(eigenNum, sampleCount, CV_32FC1);
	CvMat *EigenVectorFinal = cvCreateMat(imgWidth*imgHeight, eigenNum, CV_32FC1);
	CvMat *reltiveMat = cvCreateMat(sampleCount, sampleCount, CV_32FC1);
	CvMat *AvgVector = cvCreateMat(imgWidth*imgHeight, 1, CV_32FC1);//ƽ��ֵ����
	CvMat *EigenValue = cvCreateMat(1, sampleCount, CV_32FC1);//Э������������ֵ
	CvMat *EigenVector = cvCreateMat(sampleCount, sampleCount, CV_32FC1);//Э����������������  
	CvMat *resCoeff = cvCreateMat(sampleCount, eigenNum, CV_32FC1);//ȡǰeigenNum���������ֵ

	int *label = new int[sampleCount];
	int imgCount = 0;

	CvvImage pSourImg;

	CFileFind imageFile;
	CString fileName;
	CString imgFileAdd;
	BOOL FileExist = imageFile.FindFile(_T("C:\\faceRecognition\\faceSample\\*.jpg"),0); 
	while (FileExist)
	{
		FileExist = imageFile.FindNextFile();   
		if (!imageFile.IsDots()) 
		{
			fileName = imageFile.GetFileName();
			int fileNameLen = fileName.GetLength();

			imgFileAdd = _T("C:\\faceRecognition\\faceSample\\");

			imgFileAdd += fileName;

			int strLen = imgFileAdd.GetLength();
			TCHAR *fileAddress = new TCHAR[strLen+1];

			for (int i=0; i<strLen; i++)
			{
				fileAddress[i] = imgFileAdd[i];
			}
			fileAddress[strLen] = _T('\0');

			label[imgCount] = 1;

			USES_CONVERSION;
			pSourImg.Load(T2A(fileAddress));
			IplImage *img1 = pSourImg.GetImage();
			IplImage *img2 = cvCreateImage(cvSize(img1->width, img1->height), 8, 3);
			cvCopy(img1, img2);

			IplImage *bigImg = cvCreateImage(cvSize(img1->width, img1->height), 8, 1);
			cvCvtColor(img2, bigImg, CV_BGR2GRAY);

			IplImage *smallImg = cvCreateImage(cvSize(imgWidth, imgHeight), 8, 1);
			uchar *smallImgData = (uchar*)smallImg->imageData;

			cvResize(bigImg, smallImg, CV_INTER_LINEAR);

			int height = smallImg->height;
			int width = smallImg->width;

			for (int i=0; i<height; i++)
			{
				for (int j=0; j<width; j++)
				{
					cvmSet(faceDB, i*width+j, imgCount, smallImgData[i*width+j]);
				}
			}

			delete []fileAddress;
			cvReleaseImage(&img2);
			cvReleaseImage(&bigImg);
			cvReleaseImage(&smallImg);
			imgCount++;
		}
	}

	FileExist = imageFile.FindFile(_T("C:\\faceRecognition\\SVM\\faceSample\\*.jpg"),0); 
	while (FileExist)
	{
		FileExist = imageFile.FindNextFile();   
		if (!imageFile.IsDots()) 
		{
			fileName = imageFile.GetFileName();
			int fileNameLen = fileName.GetLength();

			imgFileAdd = _T("C:\\faceRecognition\\SVM\\faceSample\\");
			imgFileAdd += fileName;

			int strLen = imgFileAdd.GetLength();
			TCHAR *fileAddress = new TCHAR[strLen+1];

			for (int i=0; i<strLen; i++)
			{
				fileAddress[i] = imgFileAdd[i];
			}
			fileAddress[strLen] = _T('\0');

			label[imgCount] = -1;

			USES_CONVERSION;
			pSourImg.Load(T2A(fileAddress));
			IplImage *img1 = pSourImg.GetImage();
			IplImage *img2 = cvCreateImage(cvSize(img1->width, img1->height), 8, 3);
			cvCopy(img1, img2);

			IplImage *bigImg = cvCreateImage(cvSize(img1->width, img1->height), 8, 1);
			cvCvtColor(img2, bigImg, CV_BGR2GRAY);

			IplImage *smallImg = cvCreateImage(cvSize(imgWidth, imgHeight), 8, 1);
			uchar *smallImgData = (uchar*)smallImg->imageData;

			cvResize(bigImg, smallImg, CV_INTER_LINEAR);

			int height = smallImg->height;
			int width = smallImg->width;

			for (int i=0; i<height; i++)
			{
				for (int j=0; j<width; j++) 
				{
					cvmSet(faceDB, i*width+j, imgCount, smallImgData[i*width+j]);
				}
			}

			delete []fileAddress;
			cvReleaseImage(&img2);
			cvReleaseImage(&bigImg);
			cvReleaseImage(&smallImg);
			imgCount++;
		}
	}

	float faceColSum = 0.0;
	for (int i=0; i<imgWidth*imgHeight; i++)
	{
		faceColSum = 0.0;
		for (int j=0; j<sampleCount; j++)
		{
			faceColSum += (float)cvGetReal2D(faceDB, i, j);
		}
		faceColSum = faceColSum/sampleCount;
		cvmSet(AvgVector, i, 0, faceColSum);
	}

	for (int i=0; i<imgWidth*imgHeight; i++)
	{
		for (int j=0; j<sampleCount; j++)
		{
			cvmSet(faceDB, i, j, (double)(cvGetReal2D(faceDB, i, j) - cvGetReal2D(AvgVector, i, 0)));
		}
	}

	cvMulTransposed(faceDB, reltiveMat, 1);//������������ת�õĳ˻�  

	cvEigenVV(reltiveMat, EigenVector, EigenValue, 1.0e-6F);
	for (int i=0; i<eigenNum; i++)
	{
		for (int j=0; j<sampleCount; j++)
		{
			cvmSet(selEigenVector, i, j, (double)cvGetReal2D(EigenVector, i, j));
		}
	}

	cvGEMM(faceDB, selEigenVector, 1, NULL, 0, EigenVectorFinal, CV_GEMM_B_T);
	cvGEMM(faceDB, EigenVectorFinal, 1, NULL, 0, resCoeff, CV_GEMM_A_T); 

	DelSvmDataFile();//ɾ���ϴ�ѵ���õ��������ļ�

	WriteSvmAvgTxt(AvgVector);
	WriteSvmEigenVectorTxt(EigenVectorFinal);
	WriteSvmSamCoeffTxt(resCoeff);

	BallNormSvmResCoeff(resCoeff);
	WriteBallNormResCoeff(resCoeff);

	WriteLabel(label, sampleCount);
	WriteSvmInfo(imgWidth, imgHeight, eigenNum, sampleCount);

	cvReleaseMat(&faceDB);
	cvReleaseMat(&selEigenVector);
	cvReleaseMat(&EigenVectorFinal);
	cvReleaseMat(&reltiveMat);
	cvReleaseMat(&AvgVector);
	cvReleaseMat(&EigenValue);
	cvReleaseMat(&EigenVector);
	cvReleaseMat(&resCoeff);

	pSourImg.Destroy();

	delete[] label; 
}

void FaceSvm::ReadInfoTxt(int &imgWidth, int &imgHeight, int &eigenNum, int &sampleCount)
{
	ifstream fileIn1("C:\\faceRecognition\\SVM\\data\\Info.txt");
	if (fileIn1.fail())
	{
		cvSetErrMode(CV_ErrModeParent);
		cvGuiBoxReport(CV_StsBadArg, __FUNCTION__, "file(Info.txt) read error!!!", __FILE__, __LINE__, NULL);
	}

	fileIn1>>imgWidth;//��ȡͼƬ���
	fileIn1>>imgHeight;//��ȡͼƬ�߶�
	fileIn1>>eigenNum;//��ȡ����������
	fileIn1>>sampleCount;//��ȡѵ����������

	fileIn1.close();
}

void FaceSvm::GetLabel(struct svm_problem *prob, int labelNum)
{
	ifstream fileIn("C:\\faceRecognition\\SVM\\data\\Label.txt");
	if (fileIn.fail())
	{
		cvSetErrMode(CV_ErrModeParent);
		cvGuiBoxReport(CV_StsBadArg, __FUNCTION__, "file(Label.txt) read error!!!", __FILE__, __LINE__, NULL);
	}

	for (int i=0; i<labelNum; i++)
	{
		fileIn>>prob->y[i]; 
	}
	fileIn.close(); 
}

void FaceSvm::GetProbX(struct svm_problem *prob, struct svm_node *x_space, int eigenNum)
{
	ifstream fileIn("C:\\faceRecognition\\SVM\\data\\BallNorm.txt");
	if (fileIn.fail())
	{
		cvSetErrMode(CV_ErrModeParent);
		cvGuiBoxReport(CV_StsBadArg, __FUNCTION__, "file(BallNorm.txt) read error!!!", __FILE__, __LINE__, NULL);
	}

	int index2 = 0;
	for (int i=0; i<prob->l; i++)
	{
		prob->x[i] = &x_space[index2];
		for (int k=0; k<eigenNum; k++)
		{
			x_space[index2].index = k+1;
			fileIn>>x_space[index2].value;
			index2++;
			//x_space[(i+1)*eigenNum+1+k].index = k+1;
			//fileIn>>x_space[(i+1)*eigenNum+1+k].value;
		}
		x_space[index2].index = -1; 
		index2++; 
		//x_space[(i+1)*eigenNum].index = -1;
		//index = (i+1)*eigenNum + 1; 
	}

	fileIn.close();
}

void FaceSvm::GetSvmTrainData(struct svm_problem *prob, struct svm_node *x_space)
{
	int imgWidth = 0;
	int imgHeight = 0;
	int eigenNum = 0;
	int sampleCount = 0;

	ReadInfoTxt(imgWidth, imgHeight, eigenNum, sampleCount);
	GetLabel(prob, sampleCount);//��ȡ����ǩ�������洢��prob.y����
	GetProbX(prob, x_space, eigenNum);//��ȡSVMѵ�����ݣ���������prob.x����
}

void FaceSvm::DefaultSvmParam(struct svm_parameter *param)
{
	param->svm_type = C_SVC;
	param->kernel_type = RBF;
	param->degree = 3;
	param->gamma = 0;	// 1/num_features
	param->coef0 = 0;
	param->nu = 0.5;
	param->cache_size = 100;
	param->C = 1;
	param->eps = 1e-3;
	param->p = 0.1;
	param->shrinking = 1;
	param->probability = 0;
	param->nr_weight = 0;
	param->weight_label = NULL;
	param->weight = NULL;
}

void FaceSvm::SwitchForSvmParma(struct svm_parameter *param, char ch, char *strNum, int nr_fold, int cross_validation)
{
	switch(ch)
	{
	case 's':
		{
			param->svm_type = atoi(strNum);
			break;
		}
	case 't':
		{
			param->kernel_type = atoi(strNum);
			break;
		}
	case 'd':
		{
			param->degree = atoi(strNum);
			break;
		}
	case 'g':
		{
			param->gamma = atof(strNum);
			break;
		}
	case 'r':
		{
			param->coef0 = atof(strNum);
			break;
		}
	case 'n':
		{
			param->nu = atof(strNum);
			break;
		}
	case 'm':
		{
			param->cache_size = atof(strNum);
			break;
		}
	case 'c':
		{
			param->C = atof(strNum);
			break;
		}
	case 'e':
		{
			param->eps = atof(strNum);
			break;
		}
	case 'p':
		{
			param->p = atof(strNum);
			break;
		}
	case 'h':
		{
			param->shrinking = atoi(strNum);
			break;
		}
	case 'b':
		{
			param->probability = atoi(strNum);
			break;
		}
	case 'q':
		{
			break;
		}
	case 'v':
		{
			cross_validation = 1;
			nr_fold = atoi(strNum);
			if (nr_fold < 2)
			{
				cvSetErrMode(CV_ErrModeParent);
				cvGuiBoxReport(CV_StsBadArg, __FUNCTION__, "nr_fold should > 2!!!", __FILE__, __LINE__, NULL);
			}
			break;
		}
	case 'w':
		{
			++param->nr_weight;
			param->weight_label = (int *)realloc(param->weight_label,sizeof(int)*param->nr_weight);
			param->weight = (double *)realloc(param->weight,sizeof(double)*param->nr_weight);

			param->weight_label[param->nr_weight-1] = atoi(strNum);

			param->weight[param->nr_weight-1] = atof(strNum);
			break;
		}
	default:
		{
			break;
		}
	}
}

void FaceSvm::SetSvmParam(struct svm_parameter *param, char *str, int cross_validation, int nr_fold)
{
	DefaultSvmParam(param);
	cross_validation = 0;

	char ch = ' ';
	int strSize = strlen(str);
	for (int i=0; i<strSize; i++)
	{
		if (str[i] == '-')
		{
			ch = str[i+1];
			int length = 0;
			for (int j=i+3; j<strSize; j++)
			{
				if ((isdigit(str[j])) || (str[j]=='.'))
				{
					length++;
				}
				else
				{
					break;
				}
			}
			char *strNum = new char[length+1];
			int index = 0;
			for (int j=i+3; j<i+3+length; j++)
			{
				strNum[index] = str[j];
				index++;
			}
			strNum[length] = '\0';
			SwitchForSvmParma(param, ch, strNum, nr_fold, cross_validation);
			delete strNum;
		}
	}
}

void FaceSvm::ReadAvgTxt(CvMat *avgVector)
{
	ifstream fileIn("C:\\faceRecognition\\SVM\\data\\AverageValue.txt");
	if (fileIn.fail())
	{
		cvSetErrMode(CV_ErrModeParent);
		cvGuiBoxReport(CV_StsBadArg, __FUNCTION__, " file(AverageValue.txt) read error!!!", __FILE__, __LINE__, NULL);
	}

	float val = 0.0;
	for (int i=0; i<avgVector->rows; i++)
	{
		fileIn>>val;
		cvmSet(avgVector, i, 0, (double)val);
	}
	fileIn.close();
}

void FaceSvm::ReadEigVecTxt(CvMat *eigenVector)
{
	ifstream fileIn("C:\\faceRecognition\\SVM\\data\\EigenVector.txt");
	if (fileIn.fail())
	{
		cvSetErrMode(CV_ErrModeParent);
		cvGuiBoxReport(CV_StsBadArg, __FUNCTION__, " file(AverageValue.txt) read error!!!", __FILE__, __LINE__, NULL);
	}

	float val = 0.0;
	for (int i=0; i<eigenVector->rows; i++)
	{
		for (int j=0; j<eigenVector->cols; j++)
		{
			fileIn>>val;
			cvmSet(eigenVector, i, j, val);
		}
	}
	fileIn.close();
}

void FaceSvm::BallNorm(CvMat *targetResult, float *currBallNorm)
{
	float val = 0.0;
	for (int i=0; i<targetResult->cols; i++)
	{
		val += pow((cvGetReal2D(targetResult, 0, i)), 2);
	}

	val = sqrt(val);
	val = 1/val;

	for (int i=0; i<targetResult->cols; i++)
	{
		currBallNorm[i] = val*(cvGetReal2D(targetResult, 0, i));
	}
}

void FaceSvm::PcaProject(float *currentFace, int sampleCount, int imgLen, int eigenNum, float *currBallNorm)
{
	CvMat *targetMat = cvCreateMat(imgLen, 1, CV_32FC1);
	CvMat *targetResult = cvCreateMat(1, eigenNum, CV_32FC1);

	for (int i=0; i<imgLen; i++)
	{
		cvmSet(targetMat, i, 0, currentFace[i]); 
	}

	cvSub(targetMat, svmAvgVector, targetMat, NULL); 

	cvGEMM(targetMat, svmEigenVector, 1, NULL, 0, targetResult, CV_GEMM_A_T); 
	BallNorm(targetResult, currBallNorm); 

	cvReleaseMat(&targetMat);
	cvReleaseMat(&targetResult);
}

void FaceSvm::InitSvmData()
{
	if (svmAvgVector != NULL)
	{
		//cvReleaseMat(&FaceSvm.svmAvgVector);
		cvReleaseMat(&svmAvgVector); 
	}
	if (svmEigenVector != NULL)
	{
		cvReleaseMat(&svmEigenVector);
	}
	if (testModel != NULL)
	{
		svm_destroy_model(testModel);   
	}

	ReadInfoTxt(svmImgWidth, svmImgHeight, svmEigenNum, svmSampleCount);

	svmAvgVector = cvCreateMat(svmImgLen, 1, CV_32FC1);//ƽ��ֵ����
	svmEigenVector = cvCreateMat(svmImgLen, svmEigenNum, CV_32FC1);//Э����������������  

	ReadAvgTxt(svmAvgVector);
	ReadEigVecTxt(svmEigenVector);

	const char* model_file_name = "C:\\faceRecognition\\SVM\\data\\Model.txt";
	testModel = svm_load_model(model_file_name);
}

void FaceSvm::SvmTrain(int imgWidth, int imgHeight, int eigenNum, char *option)
{
	PCAforSVM(imgWidth, imgHeight, eigenNum);

	svm_parameter param;		// set by parse_command_line
	svm_problem prob;		// set by read_problem
	svm_model *model;
	svm_node *x_space;
	int cross_validation = 0;
	int nr_fold = 0;

	int sampleCount = 0; 

	ReadInfoTxt(imgWidth, imgHeight, eigenNum, sampleCount);
	prob.l =  sampleCount;//�õ�ѵ����������
	prob.y = new double[sampleCount];
	prob.x = new struct svm_node*[sampleCount];//ָ������
	x_space = new struct svm_node[(eigenNum+1)*sampleCount];

	SetSvmParam(&param, option, cross_validation, nr_fold);//����SVM����
	GetSvmTrainData(&prob, x_space); //��ȡPCA�򻯺�������ļ�

	model = svm_train(&prob, &param);//����SVMѵ��

	const char* model_file_name = "C:\\faceRecognition\\SVM\\data\\Model.txt";
	svm_save_model(model_file_name, model);

	svm_destroy_model(model);
	svm_destroy_param(&param);
	delete[] prob.y;
	delete[] prob.x;
	delete[] x_space; 
}

double FaceSvm::SvmPredict(float *currentFace)
{
	float *currBallNorm = new float[svmEigenNum];

	PcaProject(currentFace, svmSampleCount, svmImgLen, svmEigenNum, currBallNorm);

	struct svm_node *testX;
	testX = new struct svm_node[svmEigenNum+1];

	double p = 0.0;
	for (int i=0; i<svmEigenNum; i++)
	{
		testX[i].index = i+1;
		testX[i].value = currBallNorm[i]; 
	}
	testX[svmEigenNum].index = -1;

	p = svm_predict(testModel, testX);

	delete[] testX;
	delete[] currBallNorm;

	return p; 
}