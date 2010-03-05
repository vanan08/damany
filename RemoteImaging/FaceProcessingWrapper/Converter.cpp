#include "Stdafx.h"
#include "Converter.h"

using namespace System::Runtime::InteropServices;

Frame FaceProcessingWrapper::FrameConverter::ToUnManaged(Damany::ImageProcessing::Contracts::Frame^ managed)
{
	Frame unmanaged;

	unmanaged.image = (::IplImage*) managed->Ipl->CvPtr.ToPointer();

	pin_ptr<byte> pByte = & managed->Guid.ToByteArray()[0];
	::memcpy_s(unmanaged.guid, 16, pByte, 16);

	return unmanaged;
}



