#pragma once

struct FaceSVMParameters 
{
	int svmImgWidth;
	int svmImgHeight;
	int svmEigenNum;
	int svmSampleCount;
	CString options;

	FaceSVMParameters()
	{
		this->svmEigenNum = 0;
		this->svmImgHeight = 0;
		this->svmImgWidth = 0;
		this->options = "";
	}

	int GetSvmImgLen()
	{
		return svmImgHeight * svmImgWidth;
	}

};