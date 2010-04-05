/***************************************************************************
                      transt_define.h  -  description
                      -----------------------------
    begin                : 04\07\2005
    copyright            : (C) 2004 by stone
    email                : stone_0815@sina.com
 ***************************************************************************/

/***************************************************************************
 *                                                                         *
 *   ���ļ�ͷ�����˴��������õĽṹ�Ͳ���ֵ�������ô����ʱ���������ͷ    *
 *   �ļ�																   *
 *                                                                         *
 ***************************************************************************/

#ifndef __TRANST_DEFINE_H__
#define __TRANST_DEFINE_H__

//
#define MPEG_STREAM_PACKINFO_FRAMERATE4  0x00000001
#define MPEG_STREAM_PACKINFO_DISPLAY	 0x00000002
#define MPEG_STREAM_PACKINFO_MASK		 0x000000FF

#define MPEG_STREAM_TYPE_NONE		     0x00000000
#define MPEG_STREAM_TYPE_MPEG1		     0x00010000
#define MPEG_STREAM_TYPE_MPEG1VIDEO		 0x00020000	
#define MPEG_STREAM_TYPE_MPEG2		     0x00030000
#define MPEG_STREAM_TYPE_MPEG4		     0x00040000
#define MPEG_STREAM_TYPE_MP2		     0x00050000
#define MPEG_STREAM_TYPE_MP3		     0x00060000
#define MPEG_STREAM_TYPE_AVI		     0x00070000
#define MPEG_STREAM_TYPE_ASF		     0x00080000
#define MPEG_STREAM_TYPE_H263		     0x00090000
#define MPEG_STREAM_TYPE_BKH263			 0x000A0000
#define MPEG_STREAM_TYPE_BKMP4			 0x000B0000
#define MPEG_STREAM_TYPE_WAV			 0x000C0000
#define MPEG_STREAM_TYPE_H264			 0x000D0000
#define MPEG_STREAM_TYPE_AAC			 0x000E0000
#define MPEG_STREAM_TYPE_PCM_S16LE		 0x000F0000
#define MPEG_STREAM_TYPE_OTHER			 0x00F00000
#define MPEG_STREAM_TYPE_MASK			 0x00FF0000

#define	MPEG_TRANSTPACK_TYPE_FRAME		0x01000000
#define	MPEG_TRANSTPACK_TYPE_STREAM		0x02000000
#define	MPEG_TRANSTPACK_TYPE_FORCEWRITE	0x03000000
#define	MPEG_TRANSTPACK_TYPE_MAXWRITE	0x04000000
#define	MPEG_TRANSTPACK_TYPE_MASK		0x0F000000

#define	MPEG_TRANSTPACK_CONTROL_ERROR	0x10000000
#define	MPEG_TRANSTPACK_CONTROL_RESET	0x20000000
#define	MPEG_TRANSTPACK_CONTROL_MASK	0xF0000000
//Trnast Frame Type
#define	MPEG_FRAME_TYPE_SYSHEADER		0x01
#define	MPEG_FRAME_TYPE_IFRAME			0x02
#define	MPEG_FRAME_TYPE_PFRAME			0x03
#define	MPEG_FRAME_TYPE_BFRAME			0x04
#define	MPEG_FRAME_TYPE_DFRAME			0x05
#define	MPEG_FRAME_TYPE_IPBDFRAME		0x06
#define	MPEG_FRAME_TYPE_AIPBDFRAME		0x07
#define	MPEG_FRAME_TYPE_AUDIOFRAME		0x08
#define	MPEG_FRAME_TYPE_MASK			0x000000FF


//������������
#define ListenTcpIp			0x00			//TCP/IP����
#define ListenUdp			0x01			//UDP����
#define ListenDialing		0x02			//�绰�ߴ���
#define ListenTcpMulti		0x03			//�ಥ��ʽ1
#define ListenUdpMulti		0x04			//�ಥ��ʽ2
#define ListenAuto			0x05			//�Զ�����

//��������Ͷ���
typedef enum
{
	TRANST_TYPE_BASE	= 0x00,
	TRANST_TYPE_PACKET	= 0x00,
	TRANST_TYPE_STREAM	= 0x01,
	TRANST_TYPE_TAIL	= 0x01,
}TranstType_t;

//���������ýṹ
typedef struct CHANNELINFO
{
	BYTE	m_channelid;		//ͨ��ID
	BYTE    m_allowchannum;		//ϵͳ�����ͨ������
	BYTE    m_totalchannum;		//ϵͳ�ܹ���ͨ������
	BYTE	m_bufnum;			//������Ŀ
	DWORD	m_buflen;			//�����С
	DWORD   m_waittime;			//��ʱʱ��(��λ��1����)
	DWORD	m_listentype;		//��������
	BOOL	m_bTransVideoFrame;	//������Ƶ
	BOOL	m_bTransAudioFrame;	//������Ƶ
	short	m_iDisplayWidth;	//�ͻ�����ʾ��
	short	m_iDisplayHeight;	//�ͻ�����ʾ��
}CHANNELINFO, *PCHANNELINFO;

//д�����������
typedef struct DATATYPEINFO
{
	DWORD	m_dwSize;			//���ṹ��С
	DWORD	m_dwFactoryID;		//�������ݵĳ���ID
	int		m_iFrameType;		//֡����
	int		m_iKeyFrame;		//�ؼ�֡
	int		m_iFps;				//֡��
	int		m_iWidth;			//��Ƶ��
	int		m_iHeight;			//��Ƶ��
	int		m_iDisplayWidth;
	int		m_iDisplayHeight;
	int		m_iSampleRate;
	int		m_iBitsPersample;
	int		m_iChannels;
	DWORD	m_dwTimeStamp;
	DWORD	m_dwPlayTime;
	DWORD	m_dwStreamType;
}DATATYPEINFO;


//����ص���������
typedef void (CALLBACK* LOG_CALLBACK)(HANDLE hTranst, DWORD dwCode, const char* lpMess, void *context);
typedef void (CALLBACK* MESSAGE_CALLBACK)(HANDLE hTranst, DWORD dwCode, void *context);


//�������
#define ERROR_CODE_NOERROR					0x00000000
#define ERROR_CODE_PLAYBUFFER				0x00000030
#define ERROR_CODE_STOPBUFFER				0x00000031
#define ERROR_CODE_OPENBUFFER				0x00000032
#define ERROR_CODE_RECVBUFFER				0x00000033

#define ERROR_CODE_ENCFMTCHANGE				0x00000040

#define ERROR_CODE_HANDLE					0xC0000010
#define ERROR_CODE_INVALID_HANDLE			0xc0000010
#define ERROR_CODE_PARAM					0xC0000011
#define ERROR_CODE_CREATETHREAD				0xC0000012
#define ERROR_CODE_CREATESOCKET				0xC0000013
#define ERROR_CODE_OUTMEMORY				0xC0000014
#define ERROR_CODE_MORECHANNEL				0xC0000015
#define ERROR_CODE_CHANNELMAGIC_MISMATCH	0xc0000016
#define ERROR_CODE_CALLBACK_REGISTERED		0xc0000017
#define ERROR_CODE_QUEUE_OVERFLOW			0xc0000018
#define ERROR_CODE_STREAM_THREAD_FAILURE	0xc0000019
#define ERROR_CODE_THREAD_STOP_ERROR		0xc000001A
#define ERROR_CODE_NOT_SUPPORT				0xc000001B
#define ERROR_CODE_WAIT_TIMEOUT				0xc000001C
#define ERROR_CODE_INVALID_ARGUMENT			0xc000001D
#define ERROR_CODE_INVALID_INTERFACE		0xc000001E

#define ERROR_CODE_BEGINCONNECT				0xC0001000
#define ERROR_CODE_CONNECTSUCCESS			0xC0001001
#define ERROR_CODE_NETWORK					0xC0001002
#define ERROR_CODE_CONNECTERROR				0xC0001003
#define ERROR_CODE_CONNECTERROR_1000		0xC0001004
#define ERROR_CODE_SERVERSTOP				0xC0001005
#define ERROR_CODE_SERVERSTOP_1000			0xC0001006
#define ERROR_CODE_TIMEOUT					0xC0001007
#define ERROR_CODE_TIMEOUT_1000				0xC0001008
#define ERROR_CODE_SENDDATA					0xC0001009
#define ERROR_CODE_SENDDATA_1000			0xC000100A
#define ERROR_CODE_RECVDATA					0xC000100B
#define ERROR_CODE_RECVDATA_1000			0xC000100C

#define ERROR_CODE_CLOSECONNECT				0xC0010000
#define ERROR_CODE_SERVERNOSTART			0xC0010001
#define ERROR_CODE_SERVERERROR				0xC0010002
#define ERROR_CODE_CHANNELLIMIT 			0xC0010003
#define ERROR_CODE_SERVERLIMIT				0xC0010004
#define ERROR_CODE_SERVERREFUSE				0xC0010005
#define ERROR_CODE_IPLIMIT					0xC0010006
#define ERROR_CODE_PORTLIMIT				0xC0010007
#define ERROR_CODE_TYPEERROR				0xC0010008
#define ERROR_CODE_USERERROR				0xC0010009
#define ERROR_CODE_PASSWORDERROR			0xC001000A
#define ERROR_CODE_DONTTURN					0xC001000B
#define ERROR_CODE_VERSION					0xC0100000


#endif	//__TRANST_DEFINE_H__
