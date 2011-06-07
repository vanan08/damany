/**********************************************************************
*								
*		本SDK理论最大支持连接路数无限。
*		实际需要根据机器配置、网络速度和服务器设置而定!!
*	
***********************************************************************/

#ifndef __BK_NETCLIENTSDK__
#define __BK_NETCLIENTSDK__

#define OUTFNC	extern "C" __declspec(dllexport) 
#define STDCALL	__cdecl

#ifdef __cplusplus
extern "C" {
#endif

/*
 *初始化的参数定义函数MP4_ClientInit的dwFlags参数，各类型值需要或
 */
//---------------------------------------------------------------

//1.内部传输协议
#define	MPEG_DECODER_ID_BKMP4				0x000A0000		//内部默认传输协议
#define	MPEG_DECODER_ID_RTSP				0x000B0000		//RTSP传输协议
#define	MPEG_DECODER_ID_SONY				0x000C0000		//SONY传输协议
#define	MPEG_DECODER_ID_MASK				0x000FFFFF		//传输协议淹码

//2.数据流处理模式
#define	MPEG_STREAM_MODE_0					0x00000000		//内部网络传输，内部解码显示，默认模式
#define	MPEG_STREAM_MODE_1					0x01000000		//数据流由外部输入，内部解码显示
#define	MPEG_STREAM_MODE_2					0x02000000		//内部网络传输，不解码显示，仅捕获数据流
#define	MPEG_STREAM_MODE_3					0x03000000		//仅启用音视频的外部数据解码，不显示
#define	MPEG_STREAM_MODE_4					0x04000000		//仅启用网络测试
#define	MPEG_STREAM_MODE_MASK				0x0F000000		//数据流处理模式的数据位淹码
//3.窗口处理标志
#define	MPEG_WINDOW_MODE_0					0x00000000		//内部生成显示窗口
#define	MPEG_WINDOW_MODE_1					0x00100000		//使用外部设置窗口
#define	MPEG_WINDOW_MODE_MASK				0x00F00000		//使用窗口的数据为淹码

//---------------------------------------------------------------

/*
 *窗口的鼠标和键盘事件，已经废弃不再使用
 */
#define	PLAY_EVENT_LMOUSEDOWN				0x01
#define	PLAY_EVENT_RMOUSEDOWN				0x02
#define	PLAY_EVENT_LMOUSEDBCLICK			0x03
#define	PLAY_EVENT_RMOUSEDBCLICK			0x04
#define	PLAY_EVENT_LMOUSEUP					0x05
#define	PLAY_EVENT_RMOUSEUP					0x06
#define	PLAY_EVENT_MOUSEMOVE				0x07
#define	PLAY_EVENT_KEYDOWN					0x08
#define	PLAY_EVENT_KEYUP					0x09	

/*
 *扩展参数定义，MP4_ClientSetOtherParam函数的dwFlags参数值
 */
#define	OTHERPARAM_TURNIMAGE				0x01			//显示的视频上下翻转
#define	OTHERPARAM_GETTURNIMAGE				0x02			//读取当前视频显示是否上下翻转
#define	OTHERPARAM_GETREFRESHWINDOWSIZE		0x03			//读取当前视频显示大小是否随窗口大小改变而自动改变
#define	OTHERPARAM_SETREFRESHWINDOWSIZE		0x04			//设置当前视频显示大小随窗口大小改变而自动改变
#define	OTHERPARAM_GETREFRESHWINDOWPOS		0x05			//读取当前视频显示位置是否随窗口位置改变而自动改变
#define	OTHERPARAM_SETREFRESHWINDOWPOS		0x06			//设置当前视频显示位置随窗口位置改变而自动改变

#define	OTHERPARAM_GETIMAGESIZEMODE			0x10			//废弃
#define	OTHERPARAM_SETIMAGESIZEMODE			0x11			//废弃
#define	OTHERPARAM_GETSTREAMHEAD			0x12			//得到数据流头，写本公司能播放的文件头
#define	OTHERPARAM_INITMUTILDISPLAY			0x13			//废弃
#define	OTHERPARAM_GETDISPLAYSIZE			0x14			//得到视频的显示大小，需要用此大小来调整视频的显示宽高比
#define OTHERPARAM_SETSTREAMDATA			0x15			//播放数据流，相当MP4_ClientPlayStreamData
#define OTHERPARAM_GETVIDEOTYPE				0x16			//得到视频类型
#define OTHERPARAM_GETAUDIOTYPE				0x17			//得到音频类型
#define OTHERPARAM_GETFRAMERATE				0x18			//得到视频帧率
#define OTHERPARAM_SETZOOMDISPLAY			0x19			//设置局部放大
#define OTHERPARAM_SETSERIALNUMBER			0x1A			//设置用户唯一识别字符串

#define	OTHERPARAM_MASK						0xFFFF

typedef struct tagStreamType_t
{
	unsigned short	nFactoryId;			//厂家ID
	unsigned short	nSize;				//本结构大小	
	DWORD			dwStreamTag;		//流类型Tag
	DWORD			dwStreamId;			//流ID
	int				iFrameType;			//帧类型
	union
	{
		int			iWidth;				//视频宽
		int			nSamplesPerSec;		//音频采样率
	};
	union
	{
		int			iHeight;			//视频高
		int			wBitsPerSample;		//音频采样位数
	};
	union
	{
		int			iFrameRate;			//帧率*1000
		int			nChannels;			//音频的声道数
	};
	
	//add by 2009-0429
	union
	{
		unsigned short	nDisplayScale;	//显示比例
	};
	
	unsigned short		nTimeType;		//时间戳的类型,0毫秒，1为90HZ
	DWORD				dwTimeStamp;	//时间戳(单位毫秒)
	DWORD				dwPlayTime;		//此帧播放时间(单位毫秒)
	unsigned short		nBlockAlign;	//块对齐
	unsigned short		nSampleSize;	//数据单元大小
	DWORD				dwBitRate;		//此数据流的码流大小	
}StreamType_t;

typedef struct AVImage_t;

typedef void (CALLBACK* MESSAGE_CALLBACK)(HANDLE hClient, DWORD dwCode, void *context);
typedef void (CALLBACK* LOG_CALLBACK)(HANDLE lClient, DWORD code, const char* info, VOID* context);
typedef int  (CALLBACK* MOUSE_CALLBACK)( HANDLE hClient, int EventID, UINT nFlags, POINT point, void *context);
typedef int  (CALLBACK* STREAMREAD_CALLBACK)(HANDLE hClient, void *context);
/*add by stone 20060329*/
typedef void (CALLBACK *DDRAW_CALLBACK)( HANDLE p, HDC hDC, RECT* lpRect, void *context );
/*add by stone 20060523用于获得服务器的消息*/
typedef void (CALLBACK *CTRLDATA_CALLBACK)( HANDLE p, void *context );
typedef int	 (CALLBACK* AVFRAME_CALLBACK)( HANDLE e, AVImage_t* pImage, void* context );

OUTFNC DWORD	STDCALL MP4_ClientGetVersion( DWORD& dwServer, DWORD& dwClient);
OUTFNC DWORD	STDCALL MP4_ClientGetVersionEx( DWORD* pdwBulid );
OUTFNC DWORD	STDCALL MP4_ClientCheckDisplayCard();
OUTFNC DWORD	STDCALL MP4_ClientCheckSoundCard();

OUTFNC HANDLE   STDCALL MP4_ClientInit( HWND hWnd, DWORD dwFlags, DWORD dwDisplayFmt, DWORD dwAudioFmt );
OUTFNC int	    STDCALL MP4_ClientUInit( HANDLE hClient );

OUTFNC int		STDCALL MP4_ClientRegisterLogCallBack( HANDLE hClient, LOG_CALLBACK pCallBack, void* context );
OUTFNC int		STDCALL MP4_ClientRegisterMouseCallBack( HANDLE hClient, MOUSE_CALLBACK pCallBack, void* context );
OUTFNC int		STDCALL MP4_ClientRegisterErrorCallBack( HANDLE hClient, MESSAGE_CALLBACK pCallBack, void* context );
OUTFNC int		STDCALL MP4_ClientSetErrorMessage( HANDLE hClient, HWND hWnd, UINT msgID );
OUTFNC int		STDCALL MP4_ClientRegisterStreamMessage( HANDLE hClient, HWND hWnd, UINT msgID );
OUTFNC int		STDCALL MP4_ClientRegisterStreamCallBack( HANDLE hClient, STREAMREAD_CALLBACK pCallBack, void* context );
//add by stone 20060329
OUTFNC int		STDCALL MP4_ClientRegisterDrawCallBack( HANDLE hClient, DDRAW_CALLBACK pCallBack, void* context );
//注册服务器消息获得回调
OUTFNC int		STDCALL MP4_ClientRegisterCtrlDataCallBack( HANDLE hClient, CTRLDATA_CALLBACK pCallBack, void* context );
OUTFNC int		STDCALL MP4_ClientRegisterCtrlDataMessage( HANDLE hClient, HWND hWnd, UINT msgID );

OUTFNC int		STDCALL MP4_ClientConnect( HANDLE hClient, const char* lpIp, DWORD dwPort, DWORD dwChannel, DWORD dwType );
OUTFNC int		STDCALL MP4_ClientDisConnect( HANDLE hClient );	

OUTFNC int		STDCALL MP4_ClientGetCurrentState(HANDLE hClient);
OUTFNC DWORD	STDCALL MP4_ClientGetConnectTime( HANDLE hClient, DWORD* pdwTotalTime );
OUTFNC int		STDCALL MP4_ClientGetConnectType( HANDLE hClient );
OUTFNC int		STDCALL MP4_ClientSetConnectType( HANDLE hClient, int iType );
OUTFNC float	STDCALL MP4_ClientGetWaitTime( HANDLE hClient );
OUTFNC int		STDCALL MP4_ClientSetWaitTime( HANDLE hClient, float fTime );
OUTFNC int		STDCALL MP4_ClientGetTTL( HANDLE hClient );
OUTFNC int		STDCALL MP4_ClientSetTTL( HANDLE hClient, int iTTL );
OUTFNC int		STDCALL MP4_ClientGetStreamType( HANDLE hClient );
OUTFNC int		STDCALL MP4_ClientSetStreamType( HANDLE hClient, int iType );
OUTFNC int		STDCALL MP4_ClientGetConnectUser( HANDLE hClient, LPSTR lpUser, LPSTR lpPass );
OUTFNC int		STDCALL MP4_ClientSetConnectUser( HANDLE hClient, const char* lpUser, const char* lpPass );
OUTFNC BOOL		STDCALL MP4_ClientGetAutoConnect( HANDLE hClient );
OUTFNC int		STDCALL MP4_ClientSetAutoConnect( HANDLE hClient, BOOL bAuto );
OUTFNC int		STDCALL MP4_ClientGetReConnectNum( HANDLE hClient );
OUTFNC int		STDCALL MP4_ClientSetReConnectNum( HANDLE hClient, int iNum );

OUTFNC long		STDCALL MP4_ClientGetVolume( HANDLE hClient );
OUTFNC int		STDCALL MP4_ClientSetVolume( HANDLE hClient, long lVolume );
OUTFNC long		STDCALL MP4_ClientGetBalance( HANDLE hClient );
OUTFNC int		STDCALL MP4_ClientSetBalance( HANDLE hClient, long lBalance );
OUTFNC BOOL		STDCALL MP4_ClientGetMute( HANDLE hClient );
OUTFNC int		STDCALL MP4_ClientSetMute( HANDLE hClient, BOOL bMute );

OUTFNC BOOL		STDCALL MP4_ClientGetDisplayFormat(HANDLE hClient, DWORD* dwSurfaceFmt);
OUTFNC BOOL		STDCALL MP4_ClientGetDisplayShow( HANDLE hClient );
OUTFNC int		STDCALL MP4_ClientSetDisplayShow( HANDLE hClient, BOOL bShow );
OUTFNC int		STDCALL MP4_ClientGetDisPlayPos( HANDLE hClient, RECT* lpRect );
OUTFNC int		STDCALL MP4_ClientSetDisPlayPos( HANDLE hClient, RECT* lpRect );
OUTFNC int		STDCALL MP4_ClientGetDisPlaySize( HANDLE hClient );
OUTFNC int		STDCALL MP4_ClientSetDisPlaySize( HANDLE hClient, int iSize );
OUTFNC BOOL		STDCALL MP4_ClientGetFullScreen( HANDLE hClient );
OUTFNC BOOL		STDCALL MP4_ClientGetFullScreenEx( HANDLE hClient, RECT *lpRect );
OUTFNC int		STDCALL MP4_ClientSetFullScreen( HANDLE hClient, BOOL bFull );
OUTFNC int		STDCALL MP4_ClientSetFullScreenEx( HANDLE hClient, BOOL bFull, HWND hWnd );
OUTFNC int		STDCALL MP4_ClientSetFullScreenExA( HANDLE hClient, BOOL bFull, HWND hWnd, RECT* lpRect );
OUTFNC COLORREF STDCALL MP4_ClientGetColorKey( HANDLE hClient );
OUTFNC INT		STDCALL MP4_ClientSetColorKey( HANDLE hClient, COLORREF clr );
OUTFNC BOOL		STDCALL MP4_ClientGetZoom( HANDLE hClient, RECT* lpRect );
OUTFNC int		STDCALL MP4_ClientSetZoom( HANDLE hClient, BOOL bZoom, RECT* lpRect );
OUTFNC int		STDCALL MP4_ClientGetImageWidth( HANDLE hClient );
OUTFNC int		STDCALL MP4_ClientGetImageHeight( HANDLE hClient );

//add by stone 20080814得到帧率
OUTFNC int		STDCALL MP4_ClientGetVideoFPS( HANDLE hClient );

OUTFNC int		STDCALL MP4_ClientSetImageQuant( HANDLE hClient, int iQuant );

OUTFNC int		STDCALL MP4_ClientCapturePicturefile( HANDLE hClient, const char* pFileName );
OUTFNC int		STDCALL MP4_ClientSetPicturefile( HANDLE hClient, const char* pFileName );
OUTFNC int		STDCALL MP4_ClientGetPackInfo( HANDLE hClient, LONGLONG *pPackTotal, LONGLONG *pPackLen );

//设置播放数据时的缓冲时间，单位(毫秒)
OUTFNC int		STDCALL MP4_ClientSetBufferTime( HANDLE hClient, DWORD dwTime );

//设置数据流缓冲模式0-不缓冲，有数据直接解码显示，实时但是不流畅；
//1-有缓冲模式，不适时但流畅
OUTFNC int		STDCALL MP4_ClientSetModal( HANDLE hClient, int iModal );

//扩展参数设置，内部使用
OUTFNC int		STDCALL MP4_ClientSetOtherParam( HANDLE hClient, DWORD dwFlags, void* buf, int* iLen );

//设置检测码流的时间(毫秒)
OUTFNC void		STDCALL MP4_ClientSetNetBPSCheckTime( HANDLE hClient, DWORD dwTime );
//读取网络传输的码流大小
OUTFNC DWORD	STDCALL MP4_ClientGetNetBPS( HANDLE hClient );	//得到网络带宽<字节/每秒>

//设置传输类型，默认为TCP/IP传输
OUTFNC void		STDCALL MP4_ClientSetTranstType( HANDLE hClient, int iTranstType );
OUTFNC int		STDCALL MP4_ClientGetTranstType( HANDLE hClient );

//设置传输包大小，只有对DVR硬盘录像机有效
OUTFNC int		STDCALL MP4_ClientSetTranstPackSize( HANDLE hClient, int iTranstPackSize );
OUTFNC int		STDCALL MP4_ClientGetTranstPackSize( HANDLE hClient );

//设置连接转发器的地址，转发器必须是我公司开发的转发器
OUTFNC int		STDCALL MP4_ClientSetConnectTurnIP( HANDLE hClient, const char* lpTurnIP );

//启用数据流捕获，要想外部得到数据流必须调用该函数，
//该函数可以在初始化hClient后的任何时候调用(除回调函数内)
OUTFNC int		STDCALL MP4_ClientStartCapture( HANDLE hClient );
OUTFNC int		STDCALL MP4_ClientStopCapture( HANDLE hClient );

//读取数据流，建议使用MP4_ClientReadStreamDataA函数读取数据流
//在数据流输出回调函数中，每次需要循环调用，直到把数据读完
OUTFNC int		STDCALL MP4_ClientReadStreamData( HANDLE hClient, void* buf, int iLen, int* iFrameType);

//add by stone 20081028，读取帧类型，以及厂家ID
OUTFNC int		STDCALL MP4_ClientReadStreamDataA( HANDLE hClient, void* buf, int iLen, StreamType_t* pStreamType);

//3.5.4.00 add
/*
 * 该函数为应用程序播放流的接口，输入缓冲数据为我公司压缩板卡SDK的输出缓冲数据,
 * 必须要调用MP4_ClientConnect后该函数才能调用成功，且初始化函数MP4_ClientInit参数
 * dwFlags=MPEG_DECODER_ID_BKMP4|MPEG_STREAM_MODE_1(MPEG_STREAM_MODE_0表示启用内部
 * 网络传输模块， MPEG_STREAM_MODE_1表示不启用内部网络传输模块),
 */
OUTFNC int		STDCALL MP4_ClientPlayStreamData( HANDLE hClient, void* buf, int iLen, DWORD dwFlags );

//add by stone 20060707
OUTFNC int		STDCALL MP4_ClientConnectEx( HANDLE hClient, const char* lpIp, DWORD dwPort, DWORD dwChannel, DWORD dwStream, DWORD dwType );

OUTFNC void		STDCALL MP4_RegisterAVFrameCallBack( HANDLE hClient, AVFRAME_CALLBACK pCallBack, void* context );

//add by stone 20061008
OUTFNC int		STDCALL MP4_ClientGetStreamHead( HANDLE hClient, void* buf, int iLen );

//add by stone 20080227
OUTFNC int		STDCALL MP4_ClientGetFactoryID( HANDLE hClient );

OUTFNC int		STDCALL MP4_ClientSetPlayId( HANDLE hClient, int iPlayId );

//添加修复AVI文件的接口，需要录制标准的AVI文件，在应用程序打包后需要调用此函数修复
//注意调用此函数后原文件可能会被修改
//参数说明:
//lpszSrcFile为原文件，lpszDstFile为目标文件,lpszType为文件类型如"AVI", "MP4"等
//返回0表示成功，其它表示错误
OUTFNC int		STDCALL MP4_ConvertFile( const char* lpszSrcFile, const char* lpszDstFile, const char* lpszType, BOOL bForce );

//20090726 add by stone
//设置视频解码输出的格式，及通过AVFRAME_CALLBACK输出的格式, 默认输出YUV420
OUTFNC int		STDCALL MP4_ClientSetImageOutFmt( HANDLE hClient, DWORD dwFmt );
OUTFNC DWORD	STDCALL MP4_ClientGetImageOutFmt( HANDLE hClient );

//强制当前连接数据流下一帧为关键帧
OUTFNC int		STDCALL MP4_ClientMakeKeyFrame( HANDLE hClient );

//抓图指定格式的图片
OUTFNC int		STDCALL MP4_ClientCapturePicturefileA( HANDLE hClient, const char* pFileName, const char* lpFormat );

//抓图到指定内存中，图片大小需要自行分析
OUTFNC int		STDCALL MP4_ClientCapturePicturefileB( HANDLE hClient, const char* pFileName, const char* lpFormat, void* pImageBuf, int* lpImageBufLen );

//设置连接的唯一厂商信息
OUTFNC int		STDCALL MP4_ClientSetSerialNumber( HANDLE hClient, const char* szSerialNumber );

#ifdef __cplusplus
}
#endif

#endif //__BK_NETCLIENTSDK__


