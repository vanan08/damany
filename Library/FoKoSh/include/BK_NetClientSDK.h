/**********************************************************************
*								
*		��SDK�������֧������·�����ޡ�
*		ʵ����Ҫ���ݻ������á������ٶȺͷ��������ö���!!
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
 *��ʼ���Ĳ������庯��MP4_ClientInit��dwFlags������������ֵ��Ҫ��
 */
//---------------------------------------------------------------

//1.�ڲ�����Э��
#define	MPEG_DECODER_ID_BKMP4				0x000A0000		//�ڲ�Ĭ�ϴ���Э��
#define	MPEG_DECODER_ID_RTSP				0x000B0000		//RTSP����Э��
#define	MPEG_DECODER_ID_SONY				0x000C0000		//SONY����Э��
#define	MPEG_DECODER_ID_MASK				0x000FFFFF		//����Э������

//2.����������ģʽ
#define	MPEG_STREAM_MODE_0					0x00000000		//�ڲ����紫�䣬�ڲ�������ʾ��Ĭ��ģʽ
#define	MPEG_STREAM_MODE_1					0x01000000		//���������ⲿ���룬�ڲ�������ʾ
#define	MPEG_STREAM_MODE_2					0x02000000		//�ڲ����紫�䣬��������ʾ��������������
#define	MPEG_STREAM_MODE_3					0x03000000		//����������Ƶ���ⲿ���ݽ��룬����ʾ
#define	MPEG_STREAM_MODE_4					0x04000000		//�������������
#define	MPEG_STREAM_MODE_MASK				0x0F000000		//����������ģʽ������λ����
//3.���ڴ����־
#define	MPEG_WINDOW_MODE_0					0x00000000		//�ڲ�������ʾ����
#define	MPEG_WINDOW_MODE_1					0x00100000		//ʹ���ⲿ���ô���
#define	MPEG_WINDOW_MODE_MASK				0x00F00000		//ʹ�ô��ڵ�����Ϊ����

//---------------------------------------------------------------

/*
 *���ڵ����ͼ����¼����Ѿ���������ʹ��
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
 *��չ�������壬MP4_ClientSetOtherParam������dwFlags����ֵ
 */
#define	OTHERPARAM_TURNIMAGE				0x01			//��ʾ����Ƶ���·�ת
#define	OTHERPARAM_GETTURNIMAGE				0x02			//��ȡ��ǰ��Ƶ��ʾ�Ƿ����·�ת
#define	OTHERPARAM_GETREFRESHWINDOWSIZE		0x03			//��ȡ��ǰ��Ƶ��ʾ��С�Ƿ��洰�ڴ�С�ı���Զ��ı�
#define	OTHERPARAM_SETREFRESHWINDOWSIZE		0x04			//���õ�ǰ��Ƶ��ʾ��С�洰�ڴ�С�ı���Զ��ı�
#define	OTHERPARAM_GETREFRESHWINDOWPOS		0x05			//��ȡ��ǰ��Ƶ��ʾλ���Ƿ��洰��λ�øı���Զ��ı�
#define	OTHERPARAM_SETREFRESHWINDOWPOS		0x06			//���õ�ǰ��Ƶ��ʾλ���洰��λ�øı���Զ��ı�

#define	OTHERPARAM_GETIMAGESIZEMODE			0x10			//����
#define	OTHERPARAM_SETIMAGESIZEMODE			0x11			//����
#define	OTHERPARAM_GETSTREAMHEAD			0x12			//�õ�������ͷ��д����˾�ܲ��ŵ��ļ�ͷ
#define	OTHERPARAM_INITMUTILDISPLAY			0x13			//����
#define	OTHERPARAM_GETDISPLAYSIZE			0x14			//�õ���Ƶ����ʾ��С����Ҫ�ô˴�С��������Ƶ����ʾ��߱�
#define OTHERPARAM_SETSTREAMDATA			0x15			//�������������൱MP4_ClientPlayStreamData
#define OTHERPARAM_GETVIDEOTYPE				0x16			//�õ���Ƶ����
#define OTHERPARAM_GETAUDIOTYPE				0x17			//�õ���Ƶ����
#define OTHERPARAM_GETFRAMERATE				0x18			//�õ���Ƶ֡��
#define OTHERPARAM_SETZOOMDISPLAY			0x19			//���þֲ��Ŵ�
#define OTHERPARAM_SETSERIALNUMBER			0x1A			//�����û�Ψһʶ���ַ���

#define	OTHERPARAM_MASK						0xFFFF

typedef struct tagStreamType_t
{
	unsigned short	nFactoryId;			//����ID
	unsigned short	nSize;				//���ṹ��С	
	DWORD			dwStreamTag;		//������Tag
	DWORD			dwStreamId;			//��ID
	int				iFrameType;			//֡����
	union
	{
		int			iWidth;				//��Ƶ��
		int			nSamplesPerSec;		//��Ƶ������
	};
	union
	{
		int			iHeight;			//��Ƶ��
		int			wBitsPerSample;		//��Ƶ����λ��
	};
	union
	{
		int			iFrameRate;			//֡��*1000
		int			nChannels;			//��Ƶ��������
	};
	
	//add by 2009-0429
	union
	{
		unsigned short	nDisplayScale;	//��ʾ����
	};
	
	unsigned short		nTimeType;		//ʱ���������,0���룬1Ϊ90HZ
	DWORD				dwTimeStamp;	//ʱ���(��λ����)
	DWORD				dwPlayTime;		//��֡����ʱ��(��λ����)
	unsigned short		nBlockAlign;	//�����
	unsigned short		nSampleSize;	//���ݵ�Ԫ��С
	DWORD				dwBitRate;		//����������������С	
}StreamType_t;

typedef struct AVImage_t;

typedef void (CALLBACK* MESSAGE_CALLBACK)(HANDLE hClient, DWORD dwCode, void *context);
typedef void (CALLBACK* LOG_CALLBACK)(HANDLE lClient, DWORD code, const char* info, VOID* context);
typedef int  (CALLBACK* MOUSE_CALLBACK)( HANDLE hClient, int EventID, UINT nFlags, POINT point, void *context);
typedef int  (CALLBACK* STREAMREAD_CALLBACK)(HANDLE hClient, void *context);
/*add by stone 20060329*/
typedef void (CALLBACK *DDRAW_CALLBACK)( HANDLE p, HDC hDC, RECT* lpRect, void *context );
/*add by stone 20060523���ڻ�÷���������Ϣ*/
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
//ע���������Ϣ��ûص�
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

//add by stone 20080814�õ�֡��
OUTFNC int		STDCALL MP4_ClientGetVideoFPS( HANDLE hClient );

OUTFNC int		STDCALL MP4_ClientSetImageQuant( HANDLE hClient, int iQuant );

OUTFNC int		STDCALL MP4_ClientCapturePicturefile( HANDLE hClient, const char* pFileName );
OUTFNC int		STDCALL MP4_ClientSetPicturefile( HANDLE hClient, const char* pFileName );
OUTFNC int		STDCALL MP4_ClientGetPackInfo( HANDLE hClient, LONGLONG *pPackTotal, LONGLONG *pPackLen );

//���ò�������ʱ�Ļ���ʱ�䣬��λ(����)
OUTFNC int		STDCALL MP4_ClientSetBufferTime( HANDLE hClient, DWORD dwTime );

//��������������ģʽ0-�����壬������ֱ�ӽ�����ʾ��ʵʱ���ǲ�������
//1-�л���ģʽ������ʱ������
OUTFNC int		STDCALL MP4_ClientSetModal( HANDLE hClient, int iModal );

//��չ�������ã��ڲ�ʹ��
OUTFNC int		STDCALL MP4_ClientSetOtherParam( HANDLE hClient, DWORD dwFlags, void* buf, int* iLen );

//���ü��������ʱ��(����)
OUTFNC void		STDCALL MP4_ClientSetNetBPSCheckTime( HANDLE hClient, DWORD dwTime );
//��ȡ���紫���������С
OUTFNC DWORD	STDCALL MP4_ClientGetNetBPS( HANDLE hClient );	//�õ��������<�ֽ�/ÿ��>

//���ô������ͣ�Ĭ��ΪTCP/IP����
OUTFNC void		STDCALL MP4_ClientSetTranstType( HANDLE hClient, int iTranstType );
OUTFNC int		STDCALL MP4_ClientGetTranstType( HANDLE hClient );

//���ô������С��ֻ�ж�DVRӲ��¼�����Ч
OUTFNC int		STDCALL MP4_ClientSetTranstPackSize( HANDLE hClient, int iTranstPackSize );
OUTFNC int		STDCALL MP4_ClientGetTranstPackSize( HANDLE hClient );

//��������ת�����ĵ�ַ��ת�����������ҹ�˾������ת����
OUTFNC int		STDCALL MP4_ClientSetConnectTurnIP( HANDLE hClient, const char* lpTurnIP );

//��������������Ҫ���ⲿ�õ�������������øú�����
//�ú��������ڳ�ʼ��hClient����κ�ʱ�����(���ص�������)
OUTFNC int		STDCALL MP4_ClientStartCapture( HANDLE hClient );
OUTFNC int		STDCALL MP4_ClientStopCapture( HANDLE hClient );

//��ȡ������������ʹ��MP4_ClientReadStreamDataA������ȡ������
//������������ص������У�ÿ����Ҫѭ�����ã�ֱ�������ݶ���
OUTFNC int		STDCALL MP4_ClientReadStreamData( HANDLE hClient, void* buf, int iLen, int* iFrameType);

//add by stone 20081028����ȡ֡���ͣ��Լ�����ID
OUTFNC int		STDCALL MP4_ClientReadStreamDataA( HANDLE hClient, void* buf, int iLen, StreamType_t* pStreamType);

//3.5.4.00 add
/*
 * �ú���ΪӦ�ó��򲥷����Ľӿڣ����뻺������Ϊ�ҹ�˾ѹ���忨SDK�������������,
 * ����Ҫ����MP4_ClientConnect��ú������ܵ��óɹ����ҳ�ʼ������MP4_ClientInit����
 * dwFlags=MPEG_DECODER_ID_BKMP4|MPEG_STREAM_MODE_1(MPEG_STREAM_MODE_0��ʾ�����ڲ�
 * ���紫��ģ�飬 MPEG_STREAM_MODE_1��ʾ�������ڲ����紫��ģ��),
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

//����޸�AVI�ļ��Ľӿڣ���Ҫ¼�Ʊ�׼��AVI�ļ�����Ӧ�ó���������Ҫ���ô˺����޸�
//ע����ô˺�����ԭ�ļ����ܻᱻ�޸�
//����˵��:
//lpszSrcFileΪԭ�ļ���lpszDstFileΪĿ���ļ�,lpszTypeΪ�ļ�������"AVI", "MP4"��
//����0��ʾ�ɹ���������ʾ����
OUTFNC int		STDCALL MP4_ConvertFile( const char* lpszSrcFile, const char* lpszDstFile, const char* lpszType, BOOL bForce );

//20090726 add by stone
//������Ƶ��������ĸ�ʽ����ͨ��AVFRAME_CALLBACK����ĸ�ʽ, Ĭ�����YUV420
OUTFNC int		STDCALL MP4_ClientSetImageOutFmt( HANDLE hClient, DWORD dwFmt );
OUTFNC DWORD	STDCALL MP4_ClientGetImageOutFmt( HANDLE hClient );

//ǿ�Ƶ�ǰ������������һ֡Ϊ�ؼ�֡
OUTFNC int		STDCALL MP4_ClientMakeKeyFrame( HANDLE hClient );

//ץͼָ����ʽ��ͼƬ
OUTFNC int		STDCALL MP4_ClientCapturePicturefileA( HANDLE hClient, const char* pFileName, const char* lpFormat );

//ץͼ��ָ���ڴ��У�ͼƬ��С��Ҫ���з���
OUTFNC int		STDCALL MP4_ClientCapturePicturefileB( HANDLE hClient, const char* pFileName, const char* lpFormat, void* pImageBuf, int* lpImageBufLen );

//�������ӵ�Ψһ������Ϣ
OUTFNC int		STDCALL MP4_ClientSetSerialNumber( HANDLE hClient, const char* szSerialNumber );

#ifdef __cplusplus
}
#endif

#endif //__BK_NETCLIENTSDK__


