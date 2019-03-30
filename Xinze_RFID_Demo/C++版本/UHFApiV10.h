
typedef struct
{
	unsigned char TagType;
	unsigned char AntNum;
	unsigned char Ids[12];
}TagIds;

#define DllExport _declspec(dllexport)

extern "C" DllExport short __stdcall  NetConnect (HANDLE &hPort,LPCTSTR strReaderIP, int nTcpPort);	
extern "C" DllExport short __stdcall  NetDisconnect(HANDLE &hPort);

extern "C" DllExport short __stdcall  CommOpen (HANDLE &hCom, char *com_port);
extern "C" DllExport short __stdcall  CommClose (HANDLE &hCom);
extern "C" DllExport short __stdcall  SetBaudRate(HANDLE hCom, USHORT BaudRate, unsigned char ReaderAddr = 0xff);
extern "C" DllExport short __stdcall  ResetReader(HANDLE hCom, unsigned char ReaderAddr = 0xff);
extern "C" DllExport short __stdcall  GetFirmwareVersion (HANDLE hCom, unsigned char *major,unsigned char *minor, unsigned char ReaderAddr = 0xff);
extern "C" DllExport short __stdcall  SetRf(HANDLE hCom,unsigned char power,unsigned char freq_type,unsigned char ReaderAddr = 0xff);
extern "C" DllExport short __stdcall  GetRf(HANDLE hCom,unsigned char * power,unsigned char  *freq_type,unsigned char ReaderAddr = 0xff) ;
extern "C" DllExport short __stdcall  GetAnt(HANDLE hCom,unsigned char * ant,unsigned char ReaderAddr) ;
extern "C" DllExport short __stdcall  SetAnt(HANDLE hCom,unsigned char ant,unsigned char ReaderAddr) ;

extern "C" DllExport short __stdcall  IsoMultiTagIdentify(HANDLE hCom, unsigned int * Count,TagIds *value, unsigned char ReaderAddr = 0xff);
extern "C" DllExport short __stdcall  IsoMultiTagRead(HANDLE hCom,  unsigned char iRomAddr,unsigned int * Count,TagIds *value, unsigned char ReaderAddr = 0xff);
extern "C" DllExport short __stdcall  IsoWriteTag(HANDLE hCom ,unsigned char iRomAddr,unsigned char value, unsigned char ReaderAddr = 0xff);
extern "C" DllExport short __stdcall  IsoReadWithID(HANDLE hCom, unsigned char* TagID,unsigned char iRomAddr,unsigned char *AntNum,unsigned char* value, unsigned char ReaderAddr = 0xff);
extern "C" DllExport short __stdcall  IsoWriteWithID(HANDLE hCom, unsigned char* TagID,unsigned char iRomAddr,unsigned char value, unsigned char ReaderAddr = 0xff);
extern "C" DllExport short __stdcall  IsoLockTag(HANDLE hCom ,unsigned char iRomAddr, unsigned char ReaderAddr = 0xff);
extern "C" DllExport short __stdcall  IsoQueryLock(HANDLE hCom, unsigned char iRomAddr,unsigned char *status, unsigned char ReaderAddr = 0xff);
extern "C" DllExport short __stdcall  IsoBlockWrite(HANDLE hCom, unsigned char iRomAddr,unsigned char* value, unsigned char ReaderAddr = 0xff);
extern "C" DllExport short __stdcall  IsoSigleTagRead(HANDLE hCom, unsigned char iRomAddr,unsigned char * value,unsigned char ReaderAddr = 0xff);

extern "C" DllExport short __stdcall  Gen2MultiTagIdentify(HANDLE hCom, unsigned int * Count,TagIds *value,unsigned char Antenna, unsigned char ReaderAddr = 0xff);
extern "C" DllExport short __stdcall  Gen2WriteEPC(HANDLE hCom ,unsigned char WordPtr,unsigned int value, unsigned char ReaderAddr = 0xff);
extern "C" DllExport short __stdcall  Gen2LockTag(HANDLE hCom,unsigned char MemBank = 1, unsigned char Lelvel = 0, unsigned char ReaderAddr = 0xff);
extern "C" DllExport short __stdcall  Gen2KillTag(HANDLE hCom ,unsigned int PassWord, unsigned char ReaderAddr = 0xff);
extern "C" DllExport short __stdcall  Gen2InitEPC(HANDLE hCom, unsigned char WordCount = 6, unsigned char ReaderAddr = 0xff);
extern "C" DllExport short __stdcall  Gen2Read(HANDLE hCom,unsigned char Membank,unsigned char WordPtr,unsigned char WordCnt, unsigned char *value, unsigned char ReaderAddr );
extern "C" DllExport short __stdcall  Gen2Write(HANDLE hCom ,unsigned char Membank,unsigned char WordPtr,unsigned int value, unsigned char ReaderAddr= 0xff );
extern "C" DllExport short __stdcall  Gen2BlockWrite(HANDLE hCom ,unsigned char Membank,unsigned char WordPtr,unsigned char WordCnt,unsigned char *value, unsigned char ReaderAddr = 0xff);

extern "C" DllExport short __stdcall  Gen2ReadSelectEPC(HANDLE hCom,unsigned char *TagEPC,unsigned char Antenna,unsigned char Membank,unsigned char WordPtr,unsigned char WordCnt, unsigned char *value, unsigned char ReaderAddr );
extern "C" DllExport short __stdcall  Gen2WriteSelectEPC(HANDLE hCom,unsigned char *TagEPC,unsigned char Antenna,unsigned char Membank,unsigned char WordPtr,unsigned int value, unsigned char ReaderAddr= 0xff );

extern "C" DllExport short __stdcall  SetOutput(HANDLE hCom,unsigned char value, unsigned char ReaderAddr);

extern "C" DllExport short __stdcall   NXP_Change_EAS(HANDLE hCom ,unsigned int AccPWD,unsigned char EAS_Flag, unsigned char ReaderAddr);
extern "C" DllExport short __stdcall   NXP_EAS_Alarm(HANDLE hCom ,unsigned char *value, unsigned char ReaderAddr);

extern "C" DllExport short __stdcall  GetTagData(HANDLE hCom,int Count,TagIds *value, unsigned char ReaderAddr = 0xff);
extern "C" DllExport short __stdcall  ClearIDBuffer(HANDLE hCom,unsigned char ReaderAddr = 0xff);
extern "C" DllExport short __stdcall  QueryIDCount(HANDLE hCom,unsigned char* Count,unsigned char ReaderAddr = 0xff);

short GetIdWithoutDel(HANDLE hCom,unsigned char *value,unsigned char ReaderAddr = 0xff);
short GetIDACK(HANDLE hCom,unsigned char ReaderAddr = 0xff);
short SetOutPort(HANDLE hCom,unsigned char port_num,unsigned char level,unsigned char ReaderAddr);