

using System;
using System.Runtime.InteropServices;

namespace PDADemo_CF2._0
{
    public class Win32
    {
        // defines
        public const int IDC_WAIT = 32514;

        // functions
        [DllImport("coredll.dll")]
        public static extern IntPtr LoadCursor(IntPtr hInstance, int lpCursorName);

        [DllImport("coredll.dll")]
        public static extern IntPtr SetCursor(IntPtr hCursor);


        [DllImport("coredll.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SystemParametersInfo(uint uiAction, uint uiParam, int pvParam, uint fWinIni);

        [DllImport("coredll.dll")]
        public static extern IntPtr FindWindow([MarshalAs(UnmanagedType.LPWStr)]string lpClassName, [MarshalAs(UnmanagedType.LPWStr)]string lpWindowName);

        [DllImport("coredll.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);


        public const uint WM_KEYFIRST = 0x0100;
        public const uint WM_KEYDOWN = 0x0100;
        public const uint WM_KEYUP = 0x0101;
        public const uint WM_KEYLAST = 0x0108;

        public const int WM_HOTKEY = 0x0312;

        public const uint MOD_ALT = 1;
        public const uint MOD_CONTROL = 2;
        public const uint MOD_KEYUP = 0x1000;
        public const uint MOD_SHIFT = 4;
        public const uint MOD_WIN = 8;

        public const uint VK_NUMPAD0 = 0x60;
        public const uint VK_NUMPAD1 = 0x61;
        public const uint VK_NUMPAD2 = 0x62;
        public const uint VK_NUMPAD3 = 0x63;
        public const uint VK_NUMPAD4 = 0x64;
        public const uint VK_NUMPAD5 = 0x65;
        public const uint VK_NUMPAD6 = 0x66;
        public const uint VK_NUMPAD7 = 0x67;
        public const uint VK_NUMPAD8 = 0x68;
        public const uint VK_NUMPAD9 = 0x69;

        public const uint VK_F1 = 0x70;
        public const uint VK_F2 = 0x71;
        public const uint VK_F3 = 0x72;
        public const uint VK_F4 = 0x73;
        public const uint VK_F5 = 0x74;
        public const uint VK_F6 = 0x75;
        public const uint VK_F7 = 0x76;
        public const uint VK_F8 = 0x77;
        public const uint VK_F9 = 0x78;

        public const uint VK_F23 = 0x86;
        public const uint VK_F24 = 0x87;

        public const uint VK_ESCAPE = 0x1B;
        public const uint VK_RETURN = 0x0D;
        public const uint VK_CAPITAL = 0x14;
        public const uint VK_NUMBER = 0x0B;

        [DllImport("coredll.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, uint vk);

        [DllImport("coredll.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        [DllImport("coredll.dll")]
        public static extern short GetKeyState(int nVirtKey);


        public const uint WAIT_TIMEOUT = 0x102;
        public const uint WAIT_FAILED = 0xffffffffu;
        public const uint INFINITE = 0xffffffffu;

        [DllImport("coredll.dll")]
        public static extern uint WaitForSingleObject(IntPtr hHandle, uint dwMilliseconds); 

        [DllImport("coredll.dll")]
        public static extern uint WaitForMultipleObjects(uint nCount, IntPtr[] lpHandles, [MarshalAs(UnmanagedType.Bool)]bool fWaitAll, uint dwMilliseconds);

        [DllImport("coredll.dll")]
        public static extern IntPtr CreateEvent(IntPtr lpEventAttributes, [MarshalAs(UnmanagedType.Bool)]bool bManualReset, [MarshalAs(UnmanagedType.Bool)]bool bInitialState, [MarshalAs(UnmanagedType.LPWStr)]string lpName);


        public const uint EVENT_PULSE = 1;
        public const uint EVENT_RESET = 2;
        public const uint EVENT_SET = 3;

        [DllImport("coredll.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool EventModify(IntPtr hEvent, uint func);

        [DllImport("coredll.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool CloseHandle(IntPtr hObject);


        [StructLayout(LayoutKind.Sequential)]
        public class MSGQUEUEOPTIONS
        {
            public uint dwSize;
            public uint dwFlags;
            public uint dwMaxMessages;
            public uint cbMaxMessage;
            public bool bReadAccess;
        }

        [DllImport("coredll.dll")]
        public static extern IntPtr CreateMsgQueue([MarshalAs(UnmanagedType.LPWStr)]string lpszName, MSGQUEUEOPTIONS lpOptions);

        [DllImport("coredll.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool CloseMsgQueue(IntPtr hMsgQ);


        [DllImport("coredll.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool ReadMsgQueue(IntPtr hMsgQ, [MarshalAs(UnmanagedType.AsAny)]out object lpBuffer, uint cbBufferSize, out uint lpNumberOfBytesRead, uint dwTimeout, out uint pdwFlags);

        [StructLayout(LayoutKind.Sequential)]
        public struct POWER_BROADCAST_POWER_INFO
        {
            public uint dwNumLevels;
            public uint dwBatteryLifeTime;
            public uint dwBatteryFullLifeTime;
            public uint dwBackupBatteryLifeTime;
            public uint dwBackupBatteryFullLifeTime;
            public byte bACLineStatus;
            public byte bBatteryFlag;
            public byte bBatteryLifePercent;
            public byte bBackupBatteryFlag;
            public byte bBackupBatteryLifePercent;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct POWER_BROADCAST
        {
            public uint Message;    // one of PBT_Xxx
            public uint Flags;      // one of POWER_STATE_Xxx
            public uint Length;     // byte count of data starting at SystemPowerStateName
            public POWER_BROADCAST_POWER_INFO PI;
        }

        [DllImport("coredll.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool ReadMsgQueue(IntPtr hMsgQ, out POWER_BROADCAST BroadCast, uint cbBufferSize, out uint lpNumberOfBytesRead, uint dwTimeout, out uint pdwFlags);


        public const uint PBT_TRANSITION = 0x00000001;  // broadcast specifying system power state transition
        public const uint PBT_RESUME = 0x00000002;  // broadcast notifying a resume, specifies previous state
        public const uint PBT_POWERSTATUSCHANGE = 0x00000004;  // power supply switched to/from AC/DC
        public const uint PBT_POWERINFOCHANGE = 0x00000008;  // some system power status field has changed


        [DllImport("coredll.dll")]
        public static extern IntPtr RequestPowerNotifications(IntPtr hMsgQ, uint Flags);

        [DllImport("coredll.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool StopPowerNotifications(IntPtr hMsgQ);

        [StructLayout(LayoutKind.Sequential)]
        public class SYSTEM_POWER_STATUS_EX2
        {
            public byte ACLineStatus;
            public byte BatteryFlag;
            public byte BatteryLifePercent;
            public byte Reserved1;
            public uint BatteryLifeTime;
            public uint BatteryFullLifeTime;
            public byte Reserved2;
            public byte BackupBatteryFlag;
            public byte BackupBatteryLifePercent;
            public byte Reserved3;
            public uint BackupBatteryLifeTime;
            public uint BackupBatteryFullLifeTime;
            public uint BatteryVoltage;
            public uint BatteryCurrent;
            public uint BatteryAverageCurrent;
            public uint BatteryAverageInterval;
            public uint BatterymAHourConsumed;
            public uint BatteryTemperature;
            public uint BackupBatteryVoltage;
            public byte BatteryChemistry;
        }

        [DllImport("coredll.dll")]
        public static extern uint GetSystemPowerStatusEx2(SYSTEM_POWER_STATUS_EX2 pSystemPowerStatusEx2, uint dwLen, [MarshalAs(UnmanagedType.Bool)]bool fUpdate);

        public const uint POWER_STATE_ON = 0x00010000u;        // on state
        public const uint POWER_STATE_OFF = 0x00020000u;        // no power, full off
        public const uint POWER_STATE_CRITICAL = 0x00040000u;        // critical off
        public const uint POWER_STATE_BOOT = 0x00080000u;        // boot state
        public const uint POWER_STATE_IDLE = 0x00100000u;        // idle state
        public const uint POWER_STATE_SUSPEND = 0x00200000u;        // suspend state
        public const uint POWER_STATE_RESET = 0x00800000u;        // reset state

        public const uint POWER_FORCE = 0x00001000u;


        [DllImport("coredll.dll")]
        public static extern uint SetSystemPowerState([MarshalAs(UnmanagedType.LPWStr)]string psState, uint StateFlags, uint Options);


        [DllImport("coredll.dll")]
        public static extern IntPtr GetDC(IntPtr hWnd);

        [DllImport("coredll.dll")]
        public static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);

        [DllImport("coredll.dll")]
        public static extern IntPtr CreateCompatibleDC(IntPtr hDC);

        [DllImport("coredll.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool DeleteDC(IntPtr hDC);

        [DllImport("coredll.dll")]
        public static extern IntPtr CreateCompatibleBitmap(IntPtr hDC, int nWidth, int nHeight);

        [DllImport("coredll.dll")]
        public static extern IntPtr SelectObject(IntPtr hDC, IntPtr hgdiobj);

        [DllImport("coredll.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool DeleteObject(IntPtr hgdiobj);


        public const uint SRCCOPY = 0x00CC0020u; /* dest = source                   */
        public const uint SRCPAINT = 0x00EE0086u; /* dest = source OR dest           */
        public const uint SRCAND = 0x008800C6u; /* dest = source AND dest          */
        public const uint SRCINVERT = 0x00660046u; /* dest = source XOR dest          */
        public const uint SRCERASE = 0x00440328u; /* dest = source AND (NOT dest )   */
        public const uint NOTSRCCOPY = 0x00330008u; /* dest = (NOT source)             */
        public const uint NOTSRCERASE = 0x001100A6u; /* dest = (NOT src) AND (NOT dest) */
        public const uint MERGECOPY = 0x00C000CAu; /* dest = (source AND pattern)     */
        public const uint MERGEPAINT = 0x00BB0226u; /* dest = (NOT source) OR dest     */
        public const uint PATCOPY = 0x00F00021u; /* dest = pattern                  */
        public const uint PATPAINT = 0x00FB0A09u; /* dest = DPSnoo                   */
        public const uint PATINVERT = 0x005A0049u; /* dest = pattern XOR dest         */
        public const uint DSTINVERT = 0x00550009u; /* dest = (NOT dest)               */
        public const uint BLACKNESS = 0x00000042u; /* dest = BLACK                    */
        public const uint WHITENESS = 0x00FF0062u; /* dest = WHITE                    */

        [DllImport("coredll.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool BitBlt(IntPtr hdcDest, int nXDest, int nYDest, int nWidth, int nHeight, IntPtr hdcSrc, int nXSrc, int nYSrc, uint dwRop);


        public struct TRIVERTEX
        {
            public int x;
            public int y;
            public ushort Red;
            public ushort Green;
            public ushort Blue;
            public ushort Alpha;

            public TRIVERTEX(int x, int y, ushort red, ushort green, ushort blue, ushort alpha)
            {
                this.x = x;
                this.y = y;
                this.Red = (ushort)(red << 8);
                this.Green = (ushort)(green << 8);
                this.Blue = (ushort)(blue << 8);
                this.Alpha = (ushort)(alpha << 8);
            }
        }

        public struct GRADIENT_RECT
        {
            public uint UpperLeft;
            public uint LowerRight;

            public GRADIENT_RECT(uint ul, uint lr)
            {
                this.UpperLeft = ul;
                this.LowerRight = lr;
            }
        }

        public const int GRADIENT_FILL_RECT_H = 0x00000000;
        public const int GRADIENT_FILL_RECT_V = 0x00000001;

        [DllImport("coredll.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public extern static bool GradientFill(IntPtr hdc, TRIVERTEX[] pVertex, uint dwNumVertex, GRADIENT_RECT[] pMesh, uint dwNumMesh, uint dwMode);

        [DllImport("coredll.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool RoundRect(IntPtr hdc, int nLeftRect, int nTopRect, int nRightRect, int nBottomRect, int nWidth, int nHeight);


        public const int WHITE_BRUSH = 0;
        public const int LTGRAY_BRUSH = 1;
        public const int GRAY_BRUSH = 2;
        public const int DKGRAY_BRUSH = 3;
        public const int BLACK_BRUSH = 4;
        public const int NULL_BRUSH = 5;
        public const int HOLLOW_BRUSH = 5;
        public const int WHITE_PEN = 6;
        public const int BLACK_PEN = 7;
        public const int NULL_PEN = 8;
        public const int SYSTEM_FONT = 13;
        public const int DEFAULT_PALETTE = 15;
        public const int BORDERX_PEN = 32;
        public const int BORDERY_PEN = 33;


        [DllImport("coredll.dll")]
        public static extern IntPtr GetStockObject(int fnObject);

        public const int PS_SOLID = 0;
        public const int PS_DASH = 1;
        public const int PS_NULL = 5;

        public static uint RGB(byte r, byte g, byte b)
        {
            return (uint)r | (uint)((uint)g << 8) | (uint)((uint)b << 16);
        }

        [DllImport("coredll.dll")]
        public static extern IntPtr CreatePen(int fnPenStyle, int nWidth, uint crColor);

        [DllImport("coredll.dll")]
        public static extern IntPtr CreateSolidBrush(uint crColor);


        public class RECT
        {
            int left;
            int top;
            int right;
            int bottom;

            public RECT(int left, int top, int right, int bottom)
            {
                this.left = left;
                this.top = top;
                this.right = right;
                this.bottom = bottom;
            }
        }


        [DllImport("coredll.dll")]
        public static extern int FillRect(IntPtr hDC, RECT lprc, IntPtr hbr);



        [StructLayout(LayoutKind.Sequential)]
        public class KBDLLHOOKSTRUCT
        {
		    public uint vkCode;  // virtual key code 
            public uint scanCode;  // scan code 
            public uint flags;  // flags 
            public uint time;   // time stamp for this message 
            public uint dwExtraInfo; // extra info from the driver or keybd_event 

        }

        public delegate int HOOKPROC(int code, uint wParam, KBDLLHOOKSTRUCT lParam);


        public const int WH_JOURNALRECORD = 0; 
        public const int WH_JOURNALPLAYBACK = 1; 
        public const int WH_KEYBOARD_LL = 20;

        public const int HC_ACTION = 0;

        [DllImport("coredll.dll", EntryPoint="SetWindowsHookExW")]
        public static extern IntPtr SetWindowsHookEx( int idHook, [MarshalAs(UnmanagedType.FunctionPtr)]HOOKPROC lpfn, IntPtr hmod, uint dwThreadId );

        [DllImport("coredll.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("coredll.dll")]
        public static extern int CallNextHookEx(IntPtr hhk, int nCode, uint wParam, KBDLLHOOKSTRUCT lParam);

        public const uint SND_ALIAS     = 0x00010000;   // name is a WIN.INI [sounds] entry
        public const uint SND_FILENAME  = 0x00020000;   // name is a file name

        public const uint SND_SYNC      = 0x00000000;   // play synchronously (default)
        public const uint SND_ASYNC     = 0x00000001;   // play asynchronously
        public const uint SND_NODEFAULT = 0x00000002;   // silence not default, if sound not found
        public const uint SND_MEMORY    = 0x00000004;   // lpszSoundName points to a memory file
        public const uint SND_LOOP      = 0x00000008;   // loop the sound until next sndPlaySound
        public const uint SND_NOSTOP = 0x00000010;   // don't stop any currently playing sound


        [DllImport("coredll.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool sndPlaySound(byte[] lpszSoundName, uint fuSound); 


        [StructLayout(LayoutKind.Sequential)]
        public class SYSTEMTIME
        {
            public ushort wYear; 
            public ushort wMonth; 
            public ushort wDayOfWeek; 
            public ushort wDay; 
            public ushort wHour; 
            public ushort wMinute; 
            public ushort wSecond; 
            public ushort wMilliseconds;

            public SYSTEMTIME(DateTime datetime)
            {
                this.wYear = (ushort)datetime.Year;
                this.wMonth = (ushort)datetime.Month;
                this.wDay = (ushort)datetime.Day;
                this.wHour = (ushort)datetime.Hour;
                this.wMinute = (ushort)datetime.Minute;
                this.wSecond = (ushort)datetime.Second;
                this.wMilliseconds = (ushort)datetime.Millisecond;
            }
        }
        
        [DllImport("coredll.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetLocalTime(SYSTEMTIME lpSystemTime);

    }
}
