// faceSVM.cpp : Defines the exported functions for the DLL application.
/************************************************************************************************
Copyritht (c) �ɶ�������Ƽ����޹�˾
All right reserved!

������ڣ�2009��12��12��
��    �ߣ�Ѧ����
��ǰ�汾��1.2

ժҪ���ṩSVM�㷨ѵ��������SVMԤ�⺯�������У�SVMѵ���������ڵõ�SVMԤ�������model��
	  SVMԤ�⺯�������ڶԴ�ʶ�����������Ԥ�б��ж��ǡ����ˡ����ǡ����ˡ���
************************************************************************************************/
#include "stdafx.h"
#include "faceSVM.h"

//�ú������ڻ������SVMѵ����ͼƬ����
int GetSvmSampleCount()
{
	AFX_MANAGE_STATE(AfxGetStaticModuleState());

	CFileFind imageFile; 
	int sampleCount = 0;//ѵ��������ͼƬ����

	bool FileExist = imageFile.FindFile(_T("C:\\faceRecognition\\SVM\\faceSample\\*.jpg"),0); 
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

void DelSvmDataFile()
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

void WriteSvmAvgTxt(CvMat *AvgVector)
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

void WriteSvmEigenVectorTxt(CvMat *EigenVectorFinal)
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

void WriteSvmSamCoeffTxt(CvMat *resCoeff)
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

void BallNormSvmResCoeff(CvMat *resCoeff)
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

void WriteBallNormResCoeff(CvMat *resCoeff)
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

void WriteLabel(int *label, int sampleCount)
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

void WriteSvmInfo(int imgWidth, int imgHeight, int eigenNum, int sampleCount)
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

void PCAforSVM(int imgWidth, int imgHeight, int eigenNum)
{
	int sampleCount = GetSvmSampleCount(); 

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

	CFileFind imageFile;
	CString fileName;
	CString imgFileAdd;
	bool FileExist; 
	FileExist = imageFile.FindFile(_T("C:\\faceRecognition\\SVM\\faceSample\\*.jpg"),0); 
	while (FileExist)
	{
		FileExist = imageFile.FindNextFile();   
		if (!imageFile.IsDots()) 
		{
			fileName = imageFile.GetFileName();
			int fileNameLen = fileName.GetLength();

			imgFileAdd = "C:\\faceRecognition\\SVM\\faceSample\\";
			imgFileAdd += fileName;

			int strLen = imgFileAdd.GetLength();
			char *fileAddress = new char[strLen+1];

			for (int i=0; i<strLen; i++)
			{
				fileAddress[i] = imgFileAdd[i];
			}
			fileAddress[strLen] = '\0';

			if (fileAddress[strLen-11] == '+')
			{
				label[imgCount] = 1;
			}
			else
			{
				label[imgCount] = -1;
			}

			IplImage *bigImg = cvLoadImage(fileAddress, 0);
			if (!bigImg)
			{
				break;
			}

			delete []fileAddress;

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

			cvReleaseImage(&bigImg);
			cvReleaseImage(&smallImg);

			imgCount++;

			if (imgCount == sampleCount)
			{

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
			}
		}
	}

	cvReleaseMat(&faceDB);
	cvReleaseMat(&selEigenVector);
	cvReleaseMat(&EigenVectorFinal);
	cvReleaseMat(&reltiveMat);
	cvReleaseMat(&AvgVector);
	cvReleaseMat(&EigenValue);
	cvReleaseMat(&EigenVector);
	cvReleaseMat(&resCoeff);

	delete[] label; 
}

void ReadInfoTxt(int &imgWidth, int &imgHeight, int &eigenNum, int &sampleCount)
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

void GetLabel(struct svm_problem *prob, int labelNum)
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

void GetProbX(struct svm_problem *prob, struct svm_node *x_space, int eigenNum)
{
	ifstream fileIn("C:\\faceRecognition\\SVM\\data\\BallNorm.txt");
	if (fileIn.fail())
	{
		cvSetErrMode(CV_ErrModeParent);
		cvGuiBoxReport(CV_StsBadArg, __FUNCTION__, "file(BallNorm.txt) read error!!!", __FILE__, __LINE__, NULL);
	}

	int j = 0;
	int index = 0;
	for (int i=0; i<prob->l; i++)
	{
		j = index;
		prob->x[i] = &x_space[j];
		for (int k=0; k<eigenNum; k++)
		{
			x_space[(i+1)*eigenNum+1+k].index = k+1;
			fileIn>>x_space[(i+1)*eigenNum+1+k].value;
		}
		x_space[(i+1)*eigenNum].index = -1;
		index = (i+1)*eigenNum + 1; 
	}

	fileIn.close();
}

void GetSvmTrainData(struct svm_problem *prob, struct svm_node *x_space)
{
	int imgWidth = 0;
	int imgHeight = 0;
	int eigenNum = 0;
	int sampleCount = 0;

	ReadInfoTxt(imgWidth, imgHeight, eigenNum, sampleCount);
	GetLabel(prob, sampleCount);//��ȡ����ǩ�������洢��prob.y����
	GetProbX(prob, x_space, eigenNum);//��ȡSVMѵ�����ݣ���������prob.x����
}

void DefaultSvmParam(struct svm_parameter *param)
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

void SwitchForSvmParma(struct svm_parameter *param, char ch, char num, int nr_fold, int cross_validation)
{
	char val[2];
	switch(ch)
	{
	case 's':
		{
			val[0] = num;
			param->svm_type = atoi(val);
			break;
		}
	case 't':
		{
			val[0] = num;
			param->kernel_type = atoi(val);
			break;
		}
	case 'd':
		{
			val[0] = num;
			param->degree = atoi(val);
			break;
		}
	case 'g':
		{
			val[0] = num;
			param->gamma = atof(val);
			break;
		}
	case 'r':
		{
			val[0] = num;
			param->coef0 = atof(val);
			break;
		}
	case 'n':
		{
			val[0] = num;
			param->nu = atof(val);
			break;
		}
	case 'm':
		{
			val[0] = num;
			param->cache_size = atof(val);
			break;
		}
	case 'c':
		{
			val[0] = num;
			param->C = atof(val);
			break;
		}
	case 'e':
		{
			val[0] = num;
			param->eps = atof(val);
			break;
		}
	case 'p':
		{
			val[0] = num;
			param->p = atof(val);
			break;
		}
	case 'h':
		{
			val[0] = num;
			param->shrinking = atoi(val);
			break;
		}
	case 'b':
		{
			val[0] = num;
			param->probability = atoi(val);
			break;
		}
	case 'q':
		{
			break;
		}
	case 'v':
		{
			val[0] = num;
			cross_validation = 1;
			nr_fold = atoi(val);
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

			val[0] = num;
			param->weight_label[param->nr_weight-1] = atoi(val);

			val[0] = num;
			param->weight[param->nr_weight-1] = atof(val);
			break;
		}
	default:
		{
			break;
		}
	}
}

void SetSvmParam(struct svm_parameter *param, char *str, int cross_validation, int nr_fold)
{
	DefaultSvmParam(param);
	cross_validation = 0;

	int strSize = strlen(str);
	if ((str[0] != '-') || (str[5] != '-'))
	{
		cvSetErrMode(CV_ErrModeParent);
		cvGuiBoxReport(CV_StsBadArg, __FUNCTION__, "parament error!!!", __FILE__, __LINE__, NULL);
	}

	SwitchForSvmParma(param, str[1], str[3], nr_fold, cross_validation);
	SwitchForSvmParma(param, str[6], str[8], nr_fold, cross_validation);

}

void ReadAvgTxt(CvMat *avgVector)
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

void ReadEigVecTxt(CvMat *eigenVector)
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

void ReadResCoeffTxt(CvMat *resCoeff)
{
	ifstream fileIn("C:\\faceRecognition\\SVM\\data\\SampleCoefficient.txt");
	if (fileIn.fail())
	{
		cvSetErrMode(CV_ErrModeParent);
		cvGuiBoxReport(CV_StsBadArg, __FUNCTION__, " file(AverageValue.txt) read error!!!", __FILE__, __LINE__, NULL);
	}

	float val;
	for (int i=0; i<resCoeff->rows; i++)
	{
		for (int j=0; j<resCoeff->cols; j++)
		{
			fileIn>>val;
			cvmSet(resCoeff, i, j, val);
		}
	}
	fileIn.close();
}

void BallNorm(CvMat *targetResult, float *currBallNorm)
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

void PcaProject(float *currentFace, int sampleCount, int imgLen, int eigenNum, float *currBallNorm)
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

//�ú������ڼ���SVMԤ�⺯�������������ݣ�ע���ú�����SVMѵ����ɺ�ֻ����һ�μ��ɣ�
extern "C" void EXPORT InitSvmData(int imgLen, int eigenNum)
{
	if (svmAvgVector != NULL)
	{
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

	svmAvgVector = cvCreateMat(imgLen, 1, CV_32FC1);//ƽ��ֵ����
	svmEigenVector = cvCreateMat(imgLen, eigenNum, CV_32FC1);//Э����������������  

	ReadAvgTxt(svmAvgVector);
	ReadEigVecTxt(svmEigenVector);

	const char* model_file_name = "C:\\faceRecognition\\SVM\\data\\Model.txt";
	testModel = svm_load_model(model_file_name);
}

//�ú�������SVMѵ������SVM��ѵ��������������Ӧ�Ĳ���ѡ�����ѵ��
extern "C" void EXPORT SvmTrain(int imgWidth, int imgHeight, int eigenNum, char *option)
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

//SVM��Ԥ�⺯��,�Լ��ص�ÿ������ͼƬ���ú�������+1��ʾ�����ˡ������ء�-1����ʾ����
extern "C" double EXPORT SvmPredict(float *currentFace)
{
	int imgWidth = 0;
	int imgHeight = 0;
	int eigenNum = 0;
	int sampleCount = 0;
	int imgLen = 0;

	ReadInfoTxt(imgWidth, imgHeight, eigenNum, sampleCount);
	imgLen = imgWidth*imgHeight;

	float *currBallNorm = new float[eigenNum];

	PcaProject(currentFace, sampleCount, imgLen, eigenNum, currBallNorm);

	struct svm_node *testX;
	testX = new struct svm_node[eigenNum+1];
	
	for (int i=0; i<eigenNum; i++)
	{
		testX[i].index = i+1;
		testX[i].value = currBallNorm[i]; 
	}
	testX[eigenNum].index = -1;

	double p = svm_predict(testModel, testX); 
	
	delete[] testX;
	delete[] currBallNorm;

	return p; 
}


