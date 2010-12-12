/***************************************************************************
                      mpeg_define.h  -  description
                      -----------------------------
    begin                : 04\07\2005
    copyright            : (C) 2004 by stone
    email                : stone_0815@sina.com
 ***************************************************************************/

/***************************************************************************
 *                                                                         *
 *   本文件头定义了解，压器所用的结构和参数值，在引用解压库时必须包含本头  *
 *   文件																   *
 *                                                                         *
 ***************************************************************************/


#ifndef __MPEG_DEFINE_H__
#define __MPEG_DEFINE_H__

//file type ID
typedef enum
{
	FILE_CODEC_ID_AUTOCHECK			= 0x00000000,
	FILE_CODEC_ID_MPEG1				= 0x00010000,
	FILE_CODEC_ID_MPEG2				= 0x00020000,
	FILE_CODEC_ID_AVI				= 0x00030000,
	FILE_CODEC_ID_ASF				= 0x00040000,
	FILE_CODEC_ID_BKH264			= 0x000A0000,
	FILE_CODEC_ID_MPEG4				= 0x000B0000,
	FILE_CODEC_ID_WAVE				= 0x000C0000,
	FILE_CODEC_ID_H264				= 0x000D0000,
	FILE_CODEC_ID_MASK				= 0x00FF0000,
}FILE_CODEC_ID;

typedef enum
{
	FILE_CODEC_TYPE_FILE			= 0x00000000,
	FILE_CODEC_TYPE_STREAM			= 0x00000001,
	FILE_CODEC_TYPE_FILECONVERT		= 0x00000002,
	FILE_CODEC_TYPE_FILEDUMP		= 0x00000003,
	FILE_CODEC_TYPE_AVRENDERER		= 0x00000004,
	FILE_CODEC_TYPE_PICTURE			= 0x00000005,
	FILE_CODEC_TYPE_AVDECODER		= 0x00000006,
	FILE_CODEC_TYPE_MASK			= 0x0000000F,
}FILE_CODEC_TYPE;

//Mpeg CodecID
typedef enum
{
	MPEG_CODEC_ID_NONE				= 0x00000000, 
    MPEG_CODEC_ID_MPEG1VIDEO,
    MPEG_CODEC_ID_MPEG2VIDEO, /* prefered ID for MPEG Video 1 or 2 decoding */
    MPEG_CODEC_ID_MPEG2VIDEO_XVMC,
    MPEG_CODEC_ID_H261,
    MPEG_CODEC_ID_H263,
    MPEG_CODEC_ID_RV10,
    MPEG_CODEC_ID_RV20,
    MPEG_CODEC_ID_MP2,
    MPEG_CODEC_ID_MP3, /* prefered ID for MPEG Audio layer 1, 2 or3 decoding */
    MPEG_CODEC_ID_VORBIS,
    MPEG_CODEC_ID_AC3,
    MPEG_CODEC_ID_MJPEG,
    MPEG_CODEC_ID_MJPEGB,
    MPEG_CODEC_ID_LJPEG,
    MPEG_CODEC_ID_SP5X,
    MPEG_CODEC_ID_MPEG4,
    MPEG_CODEC_ID_RAWVIDEO,
    MPEG_CODEC_ID_MSMPEG4V1,
    MPEG_CODEC_ID_MSMPEG4V2,
    MPEG_CODEC_ID_MSMPEG4V3,
    MPEG_CODEC_ID_WMV1,
    MPEG_CODEC_ID_WMV2,
    MPEG_CODEC_ID_H263P,
    MPEG_CODEC_ID_H263I,
    MPEG_CODEC_ID_FLV1,
    MPEG_CODEC_ID_SVQ1,
    MPEG_CODEC_ID_SVQ3,
    MPEG_CODEC_ID_DVVIDEO,
    MPEG_CODEC_ID_DVAUDIO,
    MPEG_CODEC_ID_WMAV1,
    MPEG_CODEC_ID_WMAV2,
    MPEG_CODEC_ID_MACE3,
    MPEG_CODEC_ID_MACE6,
    MPEG_CODEC_ID_HUFFYUV,
    MPEG_CODEC_ID_CYUV,
    MPEG_CODEC_ID_H264,
    MPEG_CODEC_ID_INDEO3,
    MPEG_CODEC_ID_VP3,
    MPEG_CODEC_ID_THEORA,
    MPEG_CODEC_ID_AAC,
    MPEG_CODEC_ID_MPEG4AAC,
    MPEG_CODEC_ID_ASV1,
    MPEG_CODEC_ID_ASV2,
    MPEG_CODEC_ID_FFV1,
    MPEG_CODEC_ID_4XM,
    MPEG_CODEC_ID_VCR1,
    MPEG_CODEC_ID_CLJR,
    MPEG_CODEC_ID_MDEC,
    MPEG_CODEC_ID_ROQ,
    MPEG_CODEC_ID_INTERPLAY_VIDEO,
    MPEG_CODEC_ID_XAN_WC3,
    MPEG_CODEC_ID_XAN_WC4,
    MPEG_CODEC_ID_RPZA,
    MPEG_CODEC_ID_CINEPAK,
    MPEG_CODEC_ID_WS_VQA,
    MPEG_CODEC_ID_MSRLE,
    MPEG_CODEC_ID_MSVIDEO1,
    MPEG_CODEC_ID_IDCIN,
    MPEG_CODEC_ID_8BPS,
    MPEG_CODEC_ID_SMC,
    MPEG_CODEC_ID_FLIC,
    MPEG_CODEC_ID_TRUEMOTION1,
    MPEG_CODEC_ID_VMDVIDEO,
    MPEG_CODEC_ID_VMDAUDIO,
    MPEG_CODEC_ID_MSZH,
    MPEG_CODEC_ID_ZLIB,
    MPEG_CODEC_ID_QTRLE,

    /* various pcm "codecs" */
    MPEG_CODEC_ID_PCM_S16LE,
    MPEG_CODEC_ID_PCM_S16BE,
    MPEG_CODEC_ID_PCM_U16LE,
    MPEG_CODEC_ID_PCM_U16BE,
    MPEG_CODEC_ID_PCM_S8,
    MPEG_CODEC_ID_PCM_U8,
    MPEG_CODEC_ID_PCM_MULAW,
    MPEG_CODEC_ID_PCM_ALAW,

    /* various adpcm codecs */
    MPEG_CODEC_ID_ADPCM_IMA_QT,
    MPEG_CODEC_ID_ADPCM_IMA_WAV,
    MPEG_CODEC_ID_ADPCM_IMA_DK3,
    MPEG_CODEC_ID_ADPCM_IMA_DK4,
    MPEG_CODEC_ID_ADPCM_IMA_WS,
    MPEG_CODEC_ID_ADPCM_IMA_SMJPEG,
    MPEG_CODEC_ID_ADPCM_MS,
    MPEG_CODEC_ID_ADPCM_4XM,
    MPEG_CODEC_ID_ADPCM_XA,
    MPEG_CODEC_ID_ADPCM_ADX,
    MPEG_CODEC_ID_ADPCM_EA,
    MPEG_CODEC_ID_ADPCM_G726,

	/* AMR */
    MPEG_CODEC_ID_AMR_NB,
    MPEG_CODEC_ID_AMR_WB,

    /* RealAudio codecs*/
    MPEG_CODEC_ID_RA_144,
    MPEG_CODEC_ID_RA_288,

    /* various DPCM codecs */
    MPEG_CODEC_ID_ROQ_DPCM,
    MPEG_CODEC_ID_INTERPLAY_DPCM,
    MPEG_CODEC_ID_XAN_DPCM,
    
    MPEG_CODEC_ID_FLAC,
    
    MPEG_CODEC_ID_MPEG2TS, /* _FAKE_ codec to indicate a raw MPEG2 transport
                         stream (only used by libavformat) */
    MPEG_CODEC_ID_DTS,
	
	MPEG_CODEC_ID_BKMPEG4,
	MPEG_CODEC_ID_MP1,
	MPEG_CODEC_ID_MP123,
	MPEG_CODEC_ID_LIBMPEG2,
	MPEG_CODEC_ID_MPEG2VIRTUALDUB,
	MPEG_CODEC_ID_A52,				//ac3decoder

	MPEG_CODEC_ID_MASK				= 0x00000FFF,
}MPEG_CODEC_ID;

typedef enum
{
	MPEG_CODEC_TYPE_UNKNOWN			= -1,
	MPEG_CODEC_TYPE_VIDEO			= 0x00000000,
	MPEG_CODEC_TYPE_AUDIO			= 0x00001000,
	MPEG_CODEC_TYPE_DATA			= 0x00002000,
	MPEG_CODEC_TYPE_MASK			= 0x0000F000
}MPEG_CODEC_TYPE;

//use file callback
#define	MPEG_LIMIT_VIDEO					0x01
#define	MPEG_LIMIT_AUDIO					0x02

//Display Card Check Flags
#define	DISPLAY_FLAG_VIDEOCARD				0x00000001		//系统中有显卡
#define	DISPLAY_FLAG_OVERLAY				0x00000002		//显卡支持覆盖图面
#define	DISPLAY_FLAG_YUV					0x00000004		//显卡支持YUV图面
#define	DISPLAY_FLAG_RGB16					0x00000008		//支持RGB16色
#define	DISPLAY_FLAG_RGB24					0x00000010		//支持RGB24色
#define	DISPLAY_FLAG_RGB32					0x00000020		//支持RGB32色
#define	DISPLAY_FLAG_OVERLAYARITHSTRETCHY	0x00000040		//显卡Overlay支持Y轴算法任意缩放
#define	DISPLAY_FLAG_OVERLAYARITHSTRETCHYN	0x00000080		//显卡Overlay支持Y轴算法整数倍缩放
#define	DISPLAY_FLAG_OVERLAYSHRINKX			0x00000100		//显卡Overlay支持X轴任意收缩
#define	DISPLAY_FLAG_OVERLAYSHRINKXN		0x00000200		//显卡Overlay支持X轴按整数倍收缩
#define	DISPLAY_FLAG_OVERLAYSHRINKY			0x00000400		//显卡Overlay支持Y轴任意收缩
#define	DISPLAY_FLAG_OVERLAYSHRINKYN		0x00000800		//显卡Overlay支持Y轴按整数倍收缩
#define	DISPLAY_FLAG_OVERLAYSTRETCHX		0x00001000		//显卡Overlay支持X轴任意伸展
#define	DISPLAY_FLAG_OVERLAYSTRETCHXN		0x00002000		//显卡Overlay支持X轴按整数倍伸展
#define	DISPLAY_FLAG_OVERLAYSTRETCHY		0x00004000		//显卡Overlay支持Y轴任意伸展
#define	DISPLAY_FLAG_OVERLAYSTRETCHYN		0x00008000		//显卡Overlay支持Y轴按整数倍伸展

#define	DISPLAY_FLAG_BLTARITHSTRETCHY		0x00010000		//显卡Blt支持Y轴算法任意缩放
#define	DISPLAY_FLAG_BLTSHRINKX				0x00020000		//显卡Blt支持X轴任意收缩
#define	DISPLAY_FLAG_BLTSHRINKXN			0x00040000		//显卡Blt支持X轴按整数倍收缩
#define	DISPLAY_FLAG_BLTSHRINKY				0x00080000		//显卡Blt支持Y轴任意收缩
#define	DISPLAY_FLAG_BLTSHRINKYN			0x00100000		//显卡Blt支持Y轴按整数倍收缩
#define	DISPLAY_FLAG_BLTSTRETCHX			0x00200000		//显卡Blt支持X轴任意伸展
#define	DISPLAY_FLAG_BLTSTRETCHXN			0x00400000		//显卡Blt支持X轴按整数倍伸展
#define	DISPLAY_FLAG_BLTSTRETCHY			0x00800000		//显卡Blt支持Y轴任意伸展
#define	DISPLAY_FLAG_BLTSTRETCHYN			0x01000000		//显卡Blt支持Y轴按整数倍伸展
#define	DISPLAY_FLAG_BLTARITHSTRETCHY		0x02000000		//显卡Blt支持Y轴算法整数倍缩放
#define	DISPLAY_FLAG_DIRECTDRAW				0x10000000		//系统支持DirectDraw


#define	VIDEO_IMAGE_MIRRORLEFTRIGHT			0x0001
#define	VIDEO_IMAGE_MIRRORUPDOWN			0x0002
#define	VIDEO_IMAGE_ROTATES180				0x0003
#define	OTHERPARAM_TURNIMAGE				0x01
#define	OTHERPARAM_GETTURNIMAGE				0x02
#define	OTHERPARAM_MASK						0xFFFF


//Audio Card Flags Check
#define	SOUND_FLAG_AUDIOCARD				0x00000001
#define	SOUND_FLAG_DIRECTSOUND				0x10000000

#define	PLAY_STATE_NONE					-1	
#define	PLAY_STATE_STOP					0
#define	PLAY_STATE_PAUSE				1
#define	PLAY_STATE_PLAY					2
#define	PLAY_STATE_FAST					3
#define	PLAY_STATE_SLOW					4
#define PLAY_STATE_REPLAY				5
#define	PLAY_STATE_LOAD					10
#define	PLAY_STATE_OPEN					11
#define	PLAY_STATE_CLOSE				12
#define	PLAY_STATE_FILEEND				13
#define PLAY_STATE_EXIT					14
#define	PLAY_STATE_ERROR				24
#define PLAY_STATE_STREAMPLAY			30
#define PLAY_STATE_STREAMSTOP			31
#define PLAY_STATE_STREAMOPEN			32
#define PLAY_STATE_STREAMBUFFERRING		33


#define STREAM_TYPE_VIDEO				1
#define STREAM_TYPE_AUDIO				2
#define STREAM_TYPE_AVSYNC				3
#define STREAM_TYPE_ONLYVIDEOKEYFRAME	4

#define	ENC_CIF_FORMAT					0
#define	ENC_QCIF_FORMAT					1
#define	ENC_QQCIF_FORMAT				2
#define	ENC_2CIF_FORMAT					3
#define	ENC_4CIF_FORMAT					4


#define FRAME_TYPE_I					0
#define FRAME_TYPE_P					1
#define FRAME_TYPE_B					2
#define FRAME_TYPE_D					3
#define FRAME_TYPE_S					4
#define FRAME_TYPE_SI					5
#define FRAME_TYPE_SP					6


#define	StandardNone					0x80000000
#define	StandardNTSC					0x00000001
#define	StandardPAL						0x00000002
#define	StandardSECAM					0x00000004

typedef struct FileCallback_t
{
	BOOL	bSetCallBack;
	HANDLE	(CALLBACK* CreateFile)( LPCTSTR lpFileName,
									   DWORD dwDesiredAccess,
									   DWORD dwShareMode,
									   LPSECURITY_ATTRIBUTES lpSecurityAttributes,
									   DWORD dwCreationDisposition,
									   DWORD dwFlagsAndAttributes,
									   HANDLE hTemplateFile,
									   VOID* context);
	BOOL	(CALLBACK* CloseHandle)(HANDLE hObject);
	DWORD	(CALLBACK* SetFilePointer)( HANDLE hFile,
										   LONG lDistanceToMove,
										   PLONG lpDistanceToMoveHigh,
										   DWORD dwMoveMethod );
	BOOL	(CALLBACK* ReadFile)( HANDLE hFile,
									 LPVOID lpBuffer,
									 DWORD nNumberOfcharsToRead,
									 LPDWORD lpNumberOfcharsRead,
									 LPOVERLAPPED lpOverlapped );
	BOOL	(CALLBACK* WriteFile)( HANDLE hFile,
									 LPCVOID lpBuffer, 
									 DWORD nNumberOfcharsToWrite,
									 LPDWORD lpNumberOfcharsWritten,
									 LPOVERLAPPED lpOverlapped );
	DWORD	(CALLBACK* GetFileSize)(HANDLE hFile, LPDWORD lpFileSizeHigh );
}FileCallback_t;

typedef enum
{
	PktError					= -1,
	PktVideoFrames				= 0x00000,
	PktIFrames					= 0x00001,
	PktPFrames					= 0x00002,
	PktBBPFrames				= 0x00004,
	PktAudioFrames				= 0x00008,
	PktMotionDetection			= 0x00010,
	PktDspStatus				= 0x00020,
	PktOrigImage				= 0x00040,
	PktSysHeader				= 0x00080,
	PktBPFrames					= 0x00100,
	PktSFrames					= 0x00200,
	PktSysTrailer				= 0x00400,
	PktStream					= 0x00800,
}FrameType_t;

//surface format
#define VMDDS_NONE				0x00000000	//No use for DCI/DirectDraw.  
#define VMDDS_DCIPS				0x00000001	//Use DCI primary surface.  
#define VMDDS_PS				0x00000002	//Use DirectDraw primary surface.  
#define VMDDS_RGBOVR			0x00000004	//RGB overlay surfaces.  
#define VMDDS_YUVOVR			0x00000008	//YUV overlay surfaces.  
#define VMDDS_RGBOFF			0x00000010	//RGB off-screen surfaces.  
#define VMDDS_YUVOFF			0x00000020	//YUV off-screen surfaces.  
#define VMDDS_RGBFLP			0x00000040	//RGB flipping surfaces.  
#define VMDDS_YUVFLP			0x00000080	//YUV flipping surfaces.  
#define VMDDS_ALL				0x000000FF	//All the previous flags.  
#define VMDDS_DEFAULT			0x00000200	//Use all available surfaces. 
#define VMDDS_YUV				0x000000A8	//(VMDDS_YUVOFF | VMDDS_YUVOVR | VMDDS_YUVFLP).  
#define VMDDS_RGB				0x00000054	//(VMDDS_RGBOFF | VMDDS_RGBOVR | VMDDS_RGBFLP).  
#define VMDDS_PRIMARY			0x00000003	//(VMDDS_DCIPS | VMDDS_PS). 

/*--frame struct--*/
typedef struct AVImage_t
{
    BYTE*	data[4];
	int		size;
    int		linesize[4];
	int		width;
	int		height;
	int		format;
	float	fRate;	
	int		quant;
	int		bVideo;
	int		key_frame;
}AVImage_t;

typedef struct StreamDescription_t
{	
	int		codec_id;
	int		codec_type;
	union
	{
		int	stream_index;
		int frame_rate;		//输入的视频帧率*1000，内部接受到后会除1000，计算帧率
	};
	double	play_time;
	double	buf_time;
	DWORD	flags;	
	void*	data_buf;
	int		data_len;
	void*	motion_buf;
	int		motion_len;
}StreamDescription_t;
//flags作为输出的描述
#define STREAM_DESCRIP_MUX			0x00000001
#define STREAM_DESCRIP_PUTOUT		0x00000002
#define STREAM_DESCRIP_MASK			0x000000FF
//flags作为输入描述
#define STREAM_DESCRIP_IN_RENDER	0x00000001
#define STREAM_DESCRIP_IN_RESET		0x10000000


typedef void (CALLBACK* LOG_CALLBACK)(HANDLE p, DWORD code, const char* info, void* context);
typedef void (CALLBACK* MESSAGE_CALLBACK)(HANDLE hClient, DWORD dwCode, void *context);
typedef int  (CALLBACK* FILEHEAD_CALLBACK)(HANDLE p, BYTE* buf, int size, DWORD *ret, void* context);
/**/
typedef int	 (CALLBACK* AVFRAME_CALLBACK)( HANDLE p, AVImage_t* pImage, void* context );
typedef int	 (CALLBACK* AVDUMP_CALLBACK)( HANDLE p, StreamDescription_t* pDescription, void* context );

/*add by stone 20060329*/
typedef void (CALLBACK *DDRAW_CALLBACK)( HANDLE p, HDC hDC, RECT* lpRect, void *context );

#endif	//__MPEG_DEFINE_H__