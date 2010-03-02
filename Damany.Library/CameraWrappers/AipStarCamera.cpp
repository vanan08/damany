#include "Stdafx.h"
#include <windows.h>
#include "library/ClisentSDK/include/BK_NetClientSDK.h"
#include "library/ClisentSDK/include/mpeg_define.h"

using namespace System;
using namespace System::Runtime::InteropServices;
using namespace System::Drawing;
using namespace Damany::Component;


namespace Damany
{
	namespace Component
	{
		namespace CameraWrappers
		{
			public ref class  AipStarCamera : ICamera
			{

			public:
				AipStarCamera(String^ ip, int port, String^ userName, String^ password)
				{
					this->ip = ip;
					this->port = port;
					this->userName = userName;
					this->password = password;
					this->hClient = NULL;

					this->tmpFile = System::IO::Path::GetTempFileName();

				}

				~AipStarCamera(void)
				{
					if (this->hClient != NULL)
					{
						MP4_ClientDisConnect(this->hClient);
						MP4_ClientUInit(this->hClient);
						this->hClient = NULL;
					}
				}

				virtual array<Byte>^ CaptureImageBytes(void)
				{
					System::IntPtr ptrFile = Marshal::StringToHGlobalAnsi(this->tmpFile);
					LPCTSTR pSzTmpFile = static_cast<LPCTSTR>(ptrFile.ToPointer());
					MP4_ClientCapturePicturefile(this->hClient, pSzTmpFile);
					Marshal::FreeHGlobal(ptrFile);

					return System::IO::File::ReadAllBytes(this->tmpFile);
				}

				virtual property bool Record 
				{
					bool get()
					{
						return false;
					}
					void set(bool value)
					{

					}
				}

				virtual void Connect(void)
				{
					DWORD flag = MPEG_CODEC_ID_BKMPEG4 | FILE_CODEC_TYPE_STREAM;
					DWORD flag1 = VMDDS_YUVOFF|VMDDS_RGBOFF;

					HANDLE hClient = NULL;
					hClient = MP4_ClientInit(0, flag, flag1, 0);
					if (hClient == NULL)
					{
						throw gcnew System::Exception();
					}

					this->hClient = hClient;

					int result = 0;
					result = MP4_ClientSetStreamType(this->hClient, STREAM_TYPE_AVSYNC);
					
					result = MP4_ClientSetWaitTime(this->hClient, 3000);

					System::IntPtr ptrUser = Marshal::StringToHGlobalAnsi(this->userName);
					LPCTSTR pUser = static_cast<LPCTSTR>(ptrUser.ToPointer());
					System::IntPtr ptrPwd = Marshal::StringToHGlobalAnsi(this->password);
					LPCTSTR pPwd = static_cast<LPCTSTR>(ptrPwd.ToPointer());
					result = MP4_ClientSetConnectUser(this->hClient, pUser, pPwd);
					Marshal::FreeHGlobal(ptrUser);
					Marshal::FreeHGlobal(ptrPwd);

					result = MP4_ClientStartCapture(this->hClient);

					IntPtr ptrIp = Marshal::StringToHGlobalAnsi(this->ip);
					LPCTSTR pIp = static_cast<LPCTSTR>(ptrIp.ToPointer());
					result = MP4_ClientConnectEx(this->hClient, pIp, this->port, 0, 0, 0);
					Marshal::FreeHGlobal(ptrIp);

					MP4_ClientSetTranstType(this->hClient, 1);
					result = MP4_ClientSetTranstPackSize(this->hClient, 4096);

					System::Threading::Thread::Sleep(3000);
				}

				virtual void Start(void)
				{

				}

				virtual Image^ CaptureImage()
				{
					throw gcnew System::NotImplementedException();
				}

				virtual void SetAGCMode(bool enableAGC, bool enableDigitalGain)
				{

				}

				virtual void SetShutter(ShutterMode mode, int level)
				{

				}

				virtual void SetIris(IrisMode mode, int level)
				{

				}

				

			private:
				String^ ip;
				DWORD port;
				String^ userName;
				String^ password;
				HANDLE hClient;
				String^ tmpFile;
			};





		}
	}
}