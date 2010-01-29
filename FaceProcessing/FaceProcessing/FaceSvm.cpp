/*******************************************************************************************************
Copyright (c) 成都丹玛尼科技有限公司
All right reserved!

作    者：薛晓利
完成日期：2010年1月21日
当前版本：1.2

摘    要：利用SVM根据提供的训练样本进行“好人”“坏人”分类
*******************************************************************************************************/
#include "stdafx.h"
#include "FaceSVM.h"

FaceSvm::FaceSvm(const char* path)
{
	svmAvgVector = NULL;
	svmEigenVector = NULL; 
	testModel = NULL;		
	svmImgWidth = 100;
	svmImgHeight = 100;
	svmEigenNum = 20;
	svmImgLen = svmImgWidth*svmImgHeight;
	this->rootPath = path;
}

static CString Combine(const CString& rootPath, const CString& sub)
{
	CString root = rootPath;
	if (root.Right(1) == _T('\\'))
	{
		root = root.Left(root.GetLength()-1);
	}

	CString subDir = sub;
	if (subDir.Left(1) != _T('\\'))
	{
		subDir.Insert(0, _T('\\'));
	}

	return root + subDir;
}

CString FaceSvm::GetConfigFile()
{
	return Combine(this->rootPath, "config.ini");
}

CString FaceSvm::GetSVMProfileString(const CString& name)
{
	CString value;
	GetPrivateProfileString(_T("SVM"), name, "", value.GetBuffer(128), 128, GetConfigFile());
	value.ReleaseBuffer();
	VERIFY(value.GetLength() != 0);

	return value;
}
void FaceSvm::WriteSVMProfileString(const CString& name, const CString& val)
{
	WritePrivateProfileString(_T("SVM"), name, val, GetConfigFile());
}

int FaceSvm::GetSVMProfileInt(const CString& name)
{
	int value = GetPrivateProfileInt(_T("SVM"), name, -1, Combine(this->rootPath, "config.ini"));
	VERIFY(value != -1);

	return value;
}

void FaceSvm::WriteSVMProfileInt(const CString& name, int val)
{
	CString value;
	value.Format("%d", val);
	WriteSVMProfileString(name, value);
}

CString FaceSvm::GetSVMroot()
{
	CString path = Combine( this->rootPath, GetSVMProfileString(_T("SVMpath")));
	VERIFY(path.GetLength() != 0);
	return path;
}

CString FaceSvm::GetBadGuyPath()
{
	CString path = Combine( this->rootPath, GetSVMProfileString(_T("badGuyPath") ) );
	VERIFY(path.GetLength() != 0);
	return path;
}

CString FaceSvm::GetGoodGuyPath()
{
	CString path = Combine(  this->rootPath, GetSVMProfileString(_T("goodGuyPath")) );
	VERIFY(path.GetLength() != 0);
	return path;
}

inline CString FaceSvm::GetModelFilePath()  
{
	 return Combine(GetSVMroot(), "\\Model.txt");
}

inline CString FaceSvm::GetAverageVauleFilePath() 
{
	return Combine(GetSVMroot(), "\\AverageValue.txt");
}

inline CString FaceSvm::GetEigenVectorFilePath() 
{
	return Combine(GetSVMroot(), "\\EigenVector.txt");
}

inline CString FaceSvm::GetLabelFilePath()
{
	return Combine(GetSVMroot(), "\\Label.txt");
}

inline CString FaceSvm::GetBallNormFilePath()
{
	return Combine(GetSVMroot(), "\\BallNorm.txt");
}

inline CString FaceSvm::GetSampleCoefficientFilePath()
{
	return Combine(GetSVMroot(), "\\SampleCoefficient.txt");
}

int FaceSvm::GetGoodGuySampleCount()
{
	AFX_MANAGE_STATE(AfxGetStaticModuleState());

	int sampleCount = 0;//训练样本的图片个数
	CString fullPath = GetGoodGuyPath();
	sampleCount = GetFileCount(fullPath, _T("*.jpg"));

	return sampleCount;
}

int FaceSvm::GetBadGuySampleCount()
{
	AFX_MANAGE_STATE(AfxGetStaticModuleState());

	int sampleCount = 0;//训练样本的图片个数
	CString fullPath = GetBadGuyPath();
	sampleCount = GetFileCount(fullPath, _T("*.jpg"));

	return sampleCount;
}

int FaceSvm::GetFileCount(CString path, CString pattern)
{
	AFX_MANAGE_STATE(AfxGetStaticModuleState());

	CFileFind imageFile; 
	int sampleCount = 0;//训练样本的图片个数

	CString filePath = Combine( path,  pattern );
	BOOL FileExist = imageFile.FindFile(filePath); 
	while (FileExist)
	{
		FileExist = imageFile.FindNextFile();  
		if (!imageFile.IsDots()) 
		{
			sampleCount++;//遍历得到训练样本图片的总数
		}
	}

	return sampleCount;
}

void FaceSvm::DelFile(CString filePath)
{
	fstream txtFile;
	bool txtExist = false;
	
	txtFile.open(filePath);
	if (txtFile)
	{
		txtExist = true;
	}
	txtFile.close();

	if (txtExist)
	{
		remove(filePath);
	}
}

void FaceSvm::DelSvmDataFile()
{
	DelFile(GetAverageVauleFilePath());
	DelFile(GetEigenVectorFilePath());
	DelFile(GetSampleCoefficientFilePath());
	DelFile(GetBallNormFilePath());
	DelFile(GetLabelFilePath());
	DelFile( GetModelFilePath() );
}

void FaceSvm::WriteSvmAvgTxt(CvMat *AvgVector)
{
	ofstream out1(GetAverageVauleFilePath());//将平均值保存在文件中
	if (out1.fail())
	{
		cvSetErrMode(CV_ErrModeParent);
		cvGuiBoxReport(CV_StsBadArg, __FUNCTION__, "file(AverageValue.txt) write error!!", __FILE__, __LINE__, NULL);
		return;
	}
	for (int i=0; i<AvgVector->rows; i++)
	{
		out1<<(float)cvGetReal2D(AvgVector, i, 0)<<endl;
	}
	out1.close();
}

void FaceSvm::WriteSvmEigenVectorTxt(CvMat *EigenVectorFinal)
{
	ofstream out2(GetEigenVectorFilePath());//将特征向量保存在文件中
	if (out2.fail())
	{
		cvSetErrMode(CV_ErrModeParent);
		cvGuiBoxReport(CV_StsBadArg, __FUNCTION__, "file(EigenVector.txt) write error!!!", __FILE__, __LINE__, NULL);
		return;
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
	ofstream out3(GetSampleCoefficientFilePath());//将样本图片的投影系数保存在文件中
	if (out3.fail())
	{
		cvSetErrMode(CV_ErrModeParent);
		cvGuiBoxReport(CV_StsBadArg, __FUNCTION__, "file(SampleCoefficient.txt) write error!!!", __FILE__, __LINE__, NULL);
		return;
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
	for (int i=0; i<resCoeff->height; i++)//做球化处理
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
	ofstream out4(GetBallNormFilePath());//将球化后的结果予以保存
	if (out4.fail())
	{
		cvSetErrMode(CV_ErrModeParent);
		cvGuiBoxReport(CV_StsBadArg, __FUNCTION__, "file(BallNorm.txt) write error!!!", __FILE__, __LINE__, NULL);
		return;
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
	ofstream out5(GetLabelFilePath());
	if (out5.fail())
	{
		cvSetErrMode(CV_ErrModeParent);
		cvGuiBoxReport(CV_StsBadArg, __FUNCTION__, "file(Label.txt) write error!!!", __FILE__, __LINE__, NULL);
		return;
	}
	for (int i=0; i<sampleCount; i++)
	{
		out5<<label[i]<<endl; 
	}
	out5.close();
}


void FaceSvm::WriteSvmInfo(int imgWidth, int imgHeight, int eigenNum, int sampleCount)
{
	WriteSVMProfileInt(_T("imgWidth"), imgWidth);
	WriteSVMProfileInt(_T("imgHeight"), imgHeight);
	WriteSVMProfileInt(_T("eigenNum"), eigenNum);
	WriteSVMProfileInt(_T("sampleCount"), sampleCount);
}

void FaceSvm::PCAforSVM(int imgWidth, int imgHeight, int eigenNum)
{
	int sampleCount = GetBadGuySampleCount() + GetGoodGuySampleCount(); 

	if (eigenNum > sampleCount)
	{
		cvSetErrMode(CV_ErrModeParent);
		cvGuiBoxReport(CV_StsBadArg, __FUNCTION__, "sampleCount should greater than(or equal to) eigenNum", __FILE__, __LINE__, NULL);
	}

	CvMat *faceDB = cvCreateMat(imgWidth*imgHeight, sampleCount, CV_32FC1);//训练样本数据组成的矩阵，每个样本为一列
	CvMat *selEigenVector = cvCreateMat(eigenNum, sampleCount, CV_32FC1);
	CvMat *EigenVectorFinal = cvCreateMat(imgWidth*imgHeight, eigenNum, CV_32FC1);
	CvMat *reltiveMat = cvCreateMat(sampleCount, sampleCount, CV_32FC1);
	CvMat *AvgVector = cvCreateMat(imgWidth*imgHeight, 1, CV_32FC1);//平均值向量
	CvMat *EigenValue = cvCreateMat(1, sampleCount, CV_32FC1);//协方差矩阵的特征值
	CvMat *EigenVector = cvCreateMat(sampleCount, sampleCount, CV_32FC1);//协方差矩阵的特征向量  
	CvMat *resCoeff = cvCreateMat(sampleCount, eigenNum, CV_32FC1);//取前eigenNum个最大特征值

	int *label = new int[sampleCount];
	int imgCount = 0;

	CvvImage pSourImg;

	CFileFind imageFile;
	CString fileName;
	CString imgFileAdd;
	BOOL FileExist = imageFile.FindFile(Combine(GetBadGuyPath(), _T("*.jpg"))); 
	while (FileExist)
	{
		FileExist = imageFile.FindNextFile();   
		if (!imageFile.IsDots()) 
		{
			fileName = imageFile.GetFileName();
			int fileNameLen = fileName.GetLength();

			imgFileAdd = Combine(GetBadGuyPath(), fileName);

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

	FileExist = imageFile.FindFile(Combine(GetGoodGuyPath(), "*.jpg"),0); 
	while (FileExist)
	{
		FileExist = imageFile.FindNextFile();   
		if (!imageFile.IsDots()) 
		{
			fileName = imageFile.GetFileName();
			int fileNameLen = fileName.GetLength();

			imgFileAdd = Combine(GetGoodGuyPath(), fileName);

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

	cvMulTransposed(faceDB, reltiveMat, 1);//计算数组与其转置的乘积  

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

	DelSvmDataFile();//删除上次训练得到的数据文件

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
	imgWidth = GetSVMProfileInt( _T("imgWidth") );
	imgHeight = GetSVMProfileInt( _T("imgHeight"));
	eigenNum = GetSVMProfileInt( _T("eigenNum"));
	sampleCount = GetBadGuySampleCount() + GetGoodGuySampleCount();
	options = GetSVMProfileString(_T("option"));
}

void FaceSvm::GetLabel(struct svm_problem *prob, int labelNum)
{
	ifstream fileIn(GetLabelFilePath());
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
	ifstream fileIn(GetBallNormFilePath());
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
	GetLabel(prob, sampleCount);//获取“标签”，并存储在prob.y当中
	GetProbX(prob, x_space, eigenNum);//获取SVM训练数据，并保存在prob.x当中
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

void FaceSvm::SetSvmParam(struct svm_parameter *param, const char *str, int cross_validation, int nr_fold)
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
	ifstream fileIn(GetAverageVauleFilePath());
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
	ifstream fileIn(GetEigenVectorFilePath());
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

void FaceSvm::ReadConfigInfo() 
{
	svmImgWidth = GetSVMProfileInt(_T("imgWidth"));
	svmImgHeight = GetSVMProfileInt(_T("imgHeight"));
	svmEigenNum = GetSVMProfileInt(_T("eigenNum"));
	svmSampleCount = GetBadGuySampleCount() + GetGoodGuySampleCount();
	options = GetSVMProfileString(_T("option"));
}

void FaceSvm::Load()
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

	ReadConfigInfo();

	svmAvgVector = cvCreateMat(svmImgLen, 1, CV_32FC1);//平均值向量
	svmEigenVector = cvCreateMat(svmImgLen, svmEigenNum, CV_32FC1);//协方差矩阵的特征向量  

	ReadAvgTxt(svmAvgVector);
	ReadEigVecTxt(svmEigenVector);
	
	//const char* model_file_name = "C:\\faceRecognition\\SVM\\data\\Model.txt";
	testModel = svm_load_model(GetModelFilePath());
}

void FaceSvm::SvmTrain()
{
	ReadConfigInfo();
	
	SvmTrain(svmImgWidth, svmImgHeight, svmEigenNum, options);
}


void FaceSvm::SvmTrain(int imgWidth, int imgHeight, int eigenNum,const char *option)
{
	WriteSVMProfileString(_T("option"), option);

	PCAforSVM(imgWidth, imgHeight, eigenNum);

	svm_parameter param;		// set by parse_command_line
	svm_problem prob;		// set by read_problem
	svm_model *model;
	svm_node *x_space;
	int cross_validation = 0;
	int nr_fold = 0;

	int sampleCount = 0; 

	ReadInfoTxt(imgWidth, imgHeight, eigenNum, sampleCount);
	prob.l =  sampleCount;//得到训练样本个数
	prob.y = new double[sampleCount];
	prob.x = new svm_node*[sampleCount];//指针数组
	x_space = new svm_node[(eigenNum+1)*sampleCount];

	SetSvmParam(&param, option, cross_validation, nr_fold);//设置SVM参数
	GetSvmTrainData(&prob, x_space); //读取PCA球化后的数据文件

	model = svm_train(&prob, &param);//进行SVM训练

	//const char* model_file_name;// = "C:\\faceRecognition\\SVM\\data\\Model.txt";
	//model_file_name = filePath;
	svm_save_model(GetModelFilePath(), model);

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