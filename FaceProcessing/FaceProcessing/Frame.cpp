#include "Stdafx.h"

#include "Frame.h"

Frame::Frame()
{
	::ZeroMemory(&searchRect, sizeof(CvRect));
	::ZeroMemory(guid, GUID_LEN);
	image = NULL;
	
}