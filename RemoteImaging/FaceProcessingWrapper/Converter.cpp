#include "Stdafx.h"
#include "Converter.h"

using namespace System::Runtime::InteropServices;

Frame FaceProcessingWrapper::Converter::ToUnManaged(Damany::Imaging::Common::Frame^ managed)
{
	Frame unmanaged;

	unmanaged.image = (::IplImage*) managed->GetImage()->CvPtr.ToPointer();

	pin_ptr<byte> pByte = & managed->Guid.ToByteArray()[0];
	::memcpy_s(unmanaged.guid, 16, pByte, 16);

	return unmanaged;
}

CvRect FaceProcessingWrapper::Converter::ToNativeRect( OpenCvSharp::CvRect managedRect )
{
	CvRect rect;
	rect.x = managedRect.X;
	rect.y = managedRect.Y;
	rect.width = managedRect.Width;
	rect.height = managedRect.Height;

	return rect;
}

OpenCvSharp::CvRect FaceProcessingWrapper::Converter::ToManagedRect( CvRect nativeRect )
{
	OpenCvSharp::CvRect managed;
	managed.X = nativeRect.x;
	managed.Y = nativeRect.y;
	managed.Width = nativeRect.width;
	managed.Height = nativeRect.height;

	return managed;

}

