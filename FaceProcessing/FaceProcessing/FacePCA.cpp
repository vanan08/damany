#include "stdafx.h"
#include "FacePCA.h"

FacePCA::FacePCA(const char* path)
{
	sampleCount = 0;
	imgLen = 0;
	eigenNum = 0;

	this->rootPath = path;

	this->avgVector = NULL;
	this->eigenVector = NULL;
	this->resCoeff = NULL;

	this->sampleFileName = NULL;
}

FacePCA::~FacePCA()
{
	if (this->avgVector != NULL)
	{
		cvReleaseMat(&avgVector);
	}
	if (this->eigenVector != NULL)
	{
		cvReleaseMat(&eigenVector);
	}
	if (this->resCoeff != NULL)
	{
		cvReleaseMat(&resCoeff);
	}
	if (this->sampleFileName != NULL)
	{
		delete[] sampleFileName;
	}
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

int FacePCA::GetTrainSampleCount()
{
	CFileFind imageFile;
	CString imgFileAdd;  

	int sampleCount = 0;//训练样本的图片个数

	bool FileExist = imageFile.FindFile(Combine(GetTrainPath(), _T("*.jpg"))); 
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

void FacePCA::FaceTraining(int imgWidth, int imgHeight, int eigenNum)
{
	DelPCADataFile();

	CFileFind imageFile;
	CString fileName;
	CString imgFileAdd;  

	int sampleCount = GetTrainSampleCount();//训练样本的图片个数
	VERIFY(eigenNum < sampleCount);

	this->imgLen = imgWidth*imgHeight;
	this->sampleCount = sampleCount;
	this->eigenNum = eigenNum;

	WritePCAProfileInt(_T("imgWidth"), imgWidth);
	WritePCAProfileInt(_T("imgHeight"), imgHeight);
	WritePCAProfileInt(_T("eigenNum"), eigenNum);
	WritePCAProfileInt(_T("sampleCount"), sampleCount);

	bool FileExist = false; 

	CvMat *faceDB = cvCreateMat(imgWidth*imgHeight, sampleCount, CV_32FC1);//训练样本数据组成的矩阵，每个样本为一列
	CvMat *selEigenVector = cvCreateMat(eigenNum, sampleCount, CV_32FC1);
	CvMat *EigenVectorFinal = cvCreateMat(imgWidth*imgHeight, eigenNum, CV_32FC1);
	CvMat *reltiveMat = cvCreateMat(sampleCount, sampleCount, CV_32FC1);
	CvMat *AvgVector = cvCreateMat(imgWidth*imgHeight, 1, CV_32FC1);//平均值向量
	CvMat *EigenValue = cvCreateMat(1, sampleCount, CV_32FC1);//协方差矩阵的特征值
	CvMat *EigenVector = cvCreateMat(sampleCount, sampleCount, CV_32FC1);//协方差矩阵的特征向量  
	CvMat *resCoeff = cvCreateMat(sampleCount, eigenNum, CV_32FC1);//取前eigenNum个最大特征值

	int imgCount = 0;

	FileExist = imageFile.FindFile(Combine(GetTrainPath(), _T("*.jpg"))); 
	while (FileExist)
	{
		FileExist = imageFile.FindNextFile();   
		if (!imageFile.IsDots()) 
		{
			fileName = imageFile.GetFileName();
			imgFileAdd = Combine(GetTrainPath(), fileName);

			int strLen = imgFileAdd.GetLength();
			char *fileAddress = new char[strLen+1];

			for (int i=0; i<strLen; i++)
			{
				fileAddress[i] = imgFileAdd[i];
			}
			fileAddress[strLen] = '\0';

			WriteFileName(GetFileNameFilePath(), fileAddress);

			IplImage *bigImg = cvLoadImage(fileAddress, 0);

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

	WriteMatTxtFile(GetAverageValueFilePath(), AvgVector);
	WriteMatTxtFile(GetEigenVectorFilePath(), EigenVectorFinal); 
	WriteMatTxtFile(GetSampleCoefficientFilePath(), resCoeff);

	cvReleaseMat(&faceDB);
	cvReleaseMat(&selEigenVector);
	cvReleaseMat(&EigenVectorFinal);
	cvReleaseMat(&reltiveMat);
	cvReleaseMat(&AvgVector);
	cvReleaseMat(&EigenValue);
	cvReleaseMat(&EigenVector);
	cvReleaseMat(&resCoeff);
}

void FacePCA::Load()
{
	this->sampleCount = GetPCAProfileInt(_T("sampleCount"));
	this->imgLen = GetPCAProfileInt(_T("imgWidth")) * GetPCAProfileInt(_T("imgHeight"));
	this->eigenNum = GetPCAProfileInt(_T("eigenNum"));

	ReleaseMemory();

	this->avgVector = cvCreateMat(imgLen, 1, CV_32FC1);//平均值向量
	this->eigenVector = cvCreateMat(imgLen, eigenNum, CV_32FC1);//协方差矩阵的特征向量  
	this->resCoeff = cvCreateMat(sampleCount, eigenNum, CV_32FC1);//取前eigenNum个最大特征值

	ReadMatTxtFile(GetAverageValueFilePath(), this->avgVector);
	ReadMatTxtFile(GetEigenVectorFilePath(), this->eigenVector);
	ReadMatTxtFile(GetSampleCoefficientFilePath(), this->resCoeff);

	this->sampleFileName = new CString[this->sampleCount];
	ReadFileName(sampleFileName, sampleCount);
}

void FacePCA::FaceRecognition(float currentFace[], similarityMat*& similarity, int& count)
{
	ASSERT(currentFace != NULL);

	CvMat *targetMat = cvCreateMat(imgLen, 1, CV_32FC1);
	CvMat *targetResult = cvCreateMat(1, eigenNum, CV_32FC1);

	for (int i=0; i<imgLen; i++)
	{
		cvmSet(targetMat, i, 0, currentFace[i]);
	}

	cvSub(targetMat, avgVector, targetMat, NULL);
	cvGEMM(targetMat, eigenVector, 1, NULL, 0, targetResult, CV_GEMM_A_T);

	float diff = 0.0;
	float *resCoeffData = resCoeff->data.fl;
	int resCols = resCoeff->cols;
	float *targetResultData = targetResult->data.fl;

	float coeff = 0.0;
	float coeffSum = 0.0;

	count = this->sampleCount;
	similarity = (similarityMat*) ::CoTaskMemAlloc( sizeof(similarityMat) * count );

	for (int i=0; i<sampleCount; i++)//计算得到待识别图片与训练样本之间的差
	{
		diff = 0.0;
		for (int j=0; j<eigenNum; j++)
		{
			diff += abs(abs(resCoeffData[i*resCols+j]) - abs(targetResultData[j]));
			//diff += pow((resCoeffData[i*resCols+j] - targetResultData[j]), 2); 
		}
		similarity[i].similarity = diff;//sqrt(diff);   
	}

	float minNum = similarity[0].similarity;//定义最小值
	float maxNum = similarity[0].similarity;//定义最大值 
	for (int i=1; i<sampleCount; i++)
	{
		if (minNum > similarity[i].similarity)
		{
			minNum = similarity[i].similarity;
		}
		if (maxNum < similarity[i].similarity)
		{
			maxNum = similarity[i].similarity;
		}
	}



	/////////////////////////////////////加入距离判定后的相似度表示方法////////////////////////////////////////////////
	if (minNum < 140000000)
	{
		for (int i=0; i<sampleCount; i++)
		{
			diff = 0.0;
			coeffSum = 0.0;
			for (int j=0; j<eigenNum; j++)
			{
				coeffSum += abs(resCoeffData[i*resCols+j]);
			}
			for (int j=0; j<eigenNum; j++)
			{
				coeff = abs(resCoeffData[i*resCols+j])/coeffSum;//得到权值  
				diff += coeff * (1 - abs(resCoeffData[i*resCols+j] - targetResultData[j])/(abs(resCoeffData[i*resCols+j]) + abs(targetResultData[j])));
			}
			similarity[i].similarity = diff;
			similarity[i].index = i;
			//similarity[i].similarity = 1 - (similarity[i].similarity-minNum)/(maxNum-minNum);    
		}

		minNum = similarity[0].similarity;
		maxNum = similarity[0].similarity;
		for (int i=0; i<sampleCount; i++)
		{
			if (similarity[i].similarity > maxNum)
			{
				maxNum = similarity[i].similarity;
			}
			if (similarity[i].similarity < minNum)
			{
				minNum = similarity[i].similarity;
			}
		}
		if (maxNum > 0.45)
		{
			for (int i=0; i<sampleCount; i++)
			{
				similarity[i].similarity = (similarity[i].similarity-minNum)/(maxNum - minNum); 
			}	
		}
	}
	else
	{
		for (int i=0; i<sampleCount; i++)
		{
			similarity[i].similarity = 0.0; 
			similarity[i].index = i;
		}
	}
	////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

	cvReleaseMat(&targetMat);
	cvReleaseMat(&targetResult);
}

CString FacePCA::GetFileName(int index)
{
	return sampleFileName[index];
}

CString FacePCA::GetConfigFile()
{
	return Combine(this->rootPath, "config.ini");
}

CString FacePCA::GetTrainPath()
{
	CString path = Combine( this->rootPath, GetPCAProfileString(_T("trainingPath") ) );
	VERIFY(path.GetLength() != 0);
	return path;
}

CString FacePCA::GetPCAProfileString(const CString& name)
{
	CString value;
	GetPrivateProfileString(_T("PCA"), name, _T(""), value.GetBuffer(128), 128, GetConfigFile());
	value.ReleaseBuffer();
	VERIFY(value.GetLength() != 0);

	return value;
}
void FacePCA::WritePCAProfileString(const CString& name, const CString& val)
{
	WritePrivateProfileString(_T("PCA"), name, val, GetConfigFile());
}
int FacePCA::GetPCAProfileInt(const CString& name)
{
	int value = GetPrivateProfileInt(_T("PCA"), name, -1, GetConfigFile());
	VERIFY(value != -1);

	return value;
}
void FacePCA::WritePCAProfileInt(const CString& name, int val)
{
	CString value;
	value.Format("%d", val);
	WritePCAProfileString(name, value);
}
CString FacePCA::GetPCAroot()
{
	CString path = Combine( this->rootPath, GetPCAProfileString(_T("PCApath")));
	VERIFY(path.GetLength() != 0);
	return path;
}

inline CString FacePCA::GetSampleCoefficientFilePath()
{
	return Combine(GetPCAroot(), "\\SampleCoefficient.txt");
}

inline CString FacePCA::GetEigenVectorFilePath()
{
	return Combine(GetPCAroot(), "\\EigenVector.txt");
}

inline CString FacePCA::GetAverageValueFilePath()
{
	return Combine(GetPCAroot(), "\\AverageValue.txt");
}

inline CString FacePCA::GetFileNameFilePath()
{
	return Combine(GetPCAroot(), "\\FileName.txt");
}

void FacePCA::DelFile(CString filePath)
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

void FacePCA::DelPCADataFile()
{
	DelFile(GetAverageValueFilePath());
	DelFile(GetEigenVectorFilePath());
	DelFile(GetFileNameFilePath());
	DelFile(GetSampleCoefficientFilePath());
}

void FacePCA::WriteMatTxtFile(CString path, CvMat* mat)
{
	ofstream out(path);//将平均值保存在文件中
	VERIFY(!out.fail());

	for (int i=0; i<mat->rows; i++)
	{
		for (int j=0; j<mat->cols; j++)
		{
			out<<(float)cvGetReal2D(mat, i, j)<<" ";
		}
		out<<endl;
	}

	out.close();
}

void FacePCA::WriteFileName(CString path, char* fileName)
{
	ofstream out(path, ios::app);
	VERIFY(!out.fail());
	out<<fileName<<endl; 
	out.close();
}

void FacePCA::ReadMatTxtFile(CString path, CvMat* mat)
{
	ifstream fileIn(path);
	VERIFY(!fileIn.fail());
	
	float data;
	for (int i=0; i<mat->rows; i++)
	{
		for (int j=0; j<mat->cols; j++)
		{
			fileIn>>data;
			cvmSet(mat, i, j, data);
		}
	}
	
	fileIn.close();
}

void FacePCA::ReleaseMemory()
{
	if (this->avgVector != NULL)
	{
		cvReleaseMat(&avgVector);
	}
	if (this->eigenVector != NULL)
	{
		cvReleaseMat(&eigenVector);
	}
	if (this->resCoeff != NULL)
	{
		cvReleaseMat(&resCoeff);
	}
	if (this->sampleFileName != NULL)
	{
		delete[] sampleFileName;
	}
}

void FacePCA::ReadFileName(CString* str, int count)
{
	ifstream fileIn(GetFileNameFilePath());
	VERIFY(!fileIn.fail());
	char name[500];

	for (int i=0; i<count; i++)
	{
		memset(name, '\0', 500*sizeof(char));
		fileIn.getline(name, 500, '\n');
		str[i] = name; 
	}

	fileIn.close();
}
