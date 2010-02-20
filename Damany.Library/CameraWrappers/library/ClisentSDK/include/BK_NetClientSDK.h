/**********************************************************************
*								
*		��SDK�������֧������·�����ޡ�
*		ʵ����Ҫ���ݻ������á������ٶȺͷ��������ö���!!
*	
***********************************************************************/

#ifndef _BK_NETCLIENT_
#define _BK_NETCLIENT_

#define OUTFNC extern "C" __declspec(dllexport)
#define STDCALL __stdcall

#define	MPEG_DECODER_ID_BKMP4				0x000A0000
#define	MPEG_STREAM_MODE_0					0x00000000		//�ڲ����紫��
#define	MPEG_STREAM_MODE_1					0x01000000		//���������ⲿ����
#define	MPEG_STREAM_MODE_2					0x02000000		//�ڲ����紫�䣬������������
#define	MPEG_STREAM_MODE_3					0x03000000		//����������Ƶ���ⲿ���ݽ���
#define	MPEG_STREAM_MODE_MASK				0x0F000000		//ʹ���ⲿ����

//���ڱ�־
#define	MPEG_WINDOW_MODE_0					0x00000000		//�ڲ����ɴ���
#define	MPEG_WINDOW_MODE_1					0x00100000		//ʹ���ⲿ����
#define	MPEG_WINDOW_MODE_MASK				0x00F00000		//ʹ���ⲿ����

#define	PLAY_EVENT_LMOUSEDOWN				0x01
#define	PLAY_EVENT_RMOUSEDOWN				0x02
#define	PLAY_EVENT_LMOUSEDBCLICK			0x03
#define	PLAY_EVENT_RMOUSEDBCLICK			0x04
#define	PLAY_EVENT_LMOUSEUP					0x05
#define	PLAY_EVENT_RMOUSEUP					0x06
#define	PLAY_EVENT_MOUSEMOVE				0x07
#define	PLAY_EVENT_KEYDOWN					0x08
#define	PLAY_EVENT_KEYUP					0x09	

#define	VIDEO_IMAGE_MIRRORLEFTRIGHT			0x0001
#define	VIDEO_IMAGE_MIRRORUPDOWN			0x0002
#define	VIDEO_IMAGE_ROTATES180				0x0003
#define	OTHERPARAM_TURNIMAGE				0x01
#define	OTHERPARAM_GETTURNIMAGE				0x02
#define	OTHERPARAM_GETREFRESHWINDOWSIZE		0x03
#define	OTHERPARAM_SETREFRESHWINDOWSIZE		0x04
#define	OTHERPARAM_GETREFRESHWINDOWPOS		0x05
#define	OTHERPARAM_SETREFRESHWINDOWPOS		0x06

#define	OTHERPARAM_GETIMAGESIZEMODE			0x10
#define	OTHERPARAM_SETIMAGESIZEMODE			0x11
#define	OTHERPARAM_GETSTREAMHEAD			0x12

#define	OTHERPARAM_INITMUTILDISPLAY			0x13

#define	OTHERPARAM_MASK						0xFFFF

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

OUTFNC int		STDCALL MP4_ClientConnect( HANDLE hClient, LPCTSTR lpIp, DWORD dwPort, DWORD dwChannel, DWORD dwType );
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
OUTFNC int		STDCALL MP4_ClientSetConnectUser( HANDLE hClient, LPCTSTR lpUser, LPCTSTR lpPass );
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

OUTFNC int		STDCALL MP4_ClientCapturePicturefile( HANDLE hClient, LPCTSTR pFileName );
OUTFNC int		STDCALL MP4_ClientSetPicturefile( HANDLE hClient, LPCTSTR pFileName );
OUTFNC int		STDCALL MP4_ClientGetPackInfo( HANDLE hClient, LONGLONG *pPackTotal, LONGLONG *pPackLen );

OUTFNC int		STDCALL MP4_ClientSetBufferTime( HANDLE hClient, DWORD dwTime );
OUTFNC int		STDCALL MP4_ClientSetModal( HANDLE hClient, int iModal );/*--0 : No Buffer, 1 : Have Buffer--*/

OUTFNC int		STDCALL MP4_ClientSetOtherParam( HANDLE hClient, DWORD dwFlags, void* buf, int* iLen);

OUTFNC void		STDCALL MP4_ClientSetNetBPSCheckTime( HANDLE hClient, DWORD dwTime );
OUTFNC DWORD	STDCALL MP4_ClientGetNetBPS( HANDLE hClient );	//�õ��������<�ֽ�/ÿ��>

OUTFNC void		STDCALL MP4_ClientSetTranstType( HANDLE hClient, int iTranstType );
OUTFNC int		STDCALL MP4_ClientGetTranstType( HANDLE hClient );

OUTFNC int		STDCALL MP4_ClientSetTranstPackSize( HANDLE hClient, int iTranstPackSize );
OUTFNC int		STDCALL MP4_ClientGetTranstPackSize( HANDLE hClient );

OUTFNC int		STDCALL MP4_ClientSetConnectTurnIP( HANDLE hClient, LPCTSTR lpTurnIP );
OUTFNC int		STDCALL MP4_ClientStartCapture( HANDLE hClient );
OUTFNC int		STDCALL MP4_ClientStopCapture( HANDLE hClient );
OUTFNC int		STDCALL MP4_ClientReadStreamData( HANDLE hClient, void* buf, int iLen, int* iFrameType);

//3.5.4.00 add
/*
 * �ú���ΪӦ�ó��򲥷����Ľӿڣ����뻺������Ϊ�ҹ�˾ѹ���忨SDK�������������,
 * ����Ҫ����MP4_ClientConnect��ú������ܵ��óɹ����ҳ�ʼ������MP4_ClientInit����
 * dwFlags=MPEG_DECODER_ID_BKMP4|MPEG_STREAM_MODE_1(MPEG_STREAM_MODE_0��ʾ�����ڲ�
 * ���紫��ģ�飬 MPEG_STREAM_MODE_1��ʾ�������ڲ����紫��ģ��),
 */
OUTFNC int		STDCALL MP4_ClientPlayStreamData( HANDLE hClient, void* buf, int iLen, DWORD dwFlags );

//add by stone 20060707
OUTFNC int		STDCALL MP4_ClientConnectEx( HANDLE hClient, LPCTSTR lpIp, DWORD dwPort, DWORD dwChannel, DWORD dwStream, DWORD dwType );

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

#endif //_BK_NETCLIENT_


