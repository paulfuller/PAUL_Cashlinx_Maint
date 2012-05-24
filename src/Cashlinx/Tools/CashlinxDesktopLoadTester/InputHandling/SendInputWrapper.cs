using System;
using System.Runtime.InteropServices;

namespace CashlinxDesktopLoadTester.InputHandling
{
    //Technique for simulating mouse / keyboard inputs found at:
    //http://theoklibrary.org/showthread.php?t=287
    //
    public class SendInputWrapper
    {
        // ReSharper disable InconsistentNaming
        //C# signature for "SendInput()"
        [DllImport("user32.dll", EntryPoint = "SendInput", SetLastError = true)]
        static extern uint SendInput(
            uint nInputs,
            INPUT[] pInputs,
            int cbSize);

        //C# signature for "GetMessageExtraInfo()"
        [DllImport("user32.dll", EntryPoint = "GetMessageExtraInfo", SetLastError = true)]
        static extern IntPtr GetMessageExtraInfo();

        private enum InputType
        {
            INPUT_MOUSE = 0,
            INPUT_KEYBOARD = 1,
            INPUT_HARDWARE = 2,
        }

         public enum VK : 
                ushort 
                {   SHIFT                = 0x10,  
                    CONTROL              = 0x11,  
                    MENU                 = 0x12,  
                    ESCAPE               = 0x1B,  
                    BACK                 = 0x08,  
                    TAB                  = 0x09,  
                    RETURN               = 0x0D,  
                    PRIOR                = 0x21,  
                    NEXT                 = 0x22,  
                    END                  = 0x23,  
                    HOME                 = 0x24,  
                    LEFT                 = 0x25,  
                    UP                   = 0x26,  
                    RIGHT                = 0x27,  
                    DOWN                 = 0x28,  
                    SELECT               = 0x29,  
                    PRINT                = 0x2A,  
                    EXECUTE              = 0x2B,  
                    SNAPSHOT             = 0x2C,  
                    INSERT               = 0x2D,  
                    DELETE               = 0x2E,  
                    HELP                 = 0x2F,  
                    NUMPAD0              = 0x60,  
                    NUMPAD1              = 0x61,  
                    NUMPAD2              = 0x62,  
                    NUMPAD3              = 0x63,  
                    NUMPAD4              = 0x64,  
                    NUMPAD5              = 0x65,  
                    NUMPAD6              = 0x66,  
                    NUMPAD7              = 0x67,  
                    NUMPAD8              = 0x68,  
                    NUMPAD9              = 0x69,  
                    MULTIPLY             = 0x6A,  
                    ADD                  = 0x6B,  
                    SEPARATOR            = 0x6C,  
                    SUBTRACT             = 0x6D,  
                    DECIMAL              = 0x6E,  
                    DIVIDE               = 0x6F,  
                    F1                   = 0x70,  
                    F2                   = 0x71,  
                    F3                   = 0x72,  
                    F4                   = 0x73,  
                    F5                   = 0x74,  
                    F6                   = 0x75,  
                    F7                   = 0x76,  
                    F8                   = 0x77,  
                    F9                   = 0x78,  
                    F10                  = 0x79,  
                    F11                  = 0x7A,  
                    F12                  = 0x7B,  
                    OEM_1                = 0xBA,   
                    // ',:' for US  OEM_PLUS             = 0xBB,   
                    // '+' any country  OEM_COMMA            = 0xBC,   
                    // ',' any country  OEM_MINUS            = 0xBD,   
                    // '-' any country  OEM_PERIOD           = 0xBE,   
                    // '.' any country  OEM_2                = 0xBF,   
                    // '/?' for US  OEM_3                = 0xC0,   
                    // '`~' for US  MEDIA_NEXT_TRACK         = 0xB0,  
                    MEDIA_PREV_TRACK         = 0xB1,  
                    MEDIA_STOP               = 0xB2,  
                    MEDIA_PLAY_PAUSE         = 0xB3,  
                    LWIN                     = 0x5B,  
                    RWIN                     = 0x5C 
         }



        [Flags()]
        private enum MOUSEEVENTF
        {
            MOVE = 0x0001,  // mouse move 
            LEFTDOWN = 0x0002,  // left button down
            LEFTUP = 0x0004,  // left button up
            RIGHTDOWN = 0x0008,  // right button down
            RIGHTUP = 0x0010,  // right button up
            MIDDLEDOWN = 0x0020,  // middle button down
            MIDDLEUP = 0x0040,  // middle button up
            XDOWN = 0x0080,  // x button down 
            XUP = 0x0100,  // x button down
            WHEEL = 0x0800,  // wheel button rolled
            VIRTUALDESK = 0x4000,  // map to entire virtual desktop
            ABSOLUTE = 0x8000,  // absolute move
        }

        [Flags()]
// ReSharper disable UnusedMember.Local
        private enum KEYEVENTF
// ReSharper restore UnusedMember.Local
        {
            EXTENDEDKEY = 0x0001,
            KEYUP = 0x0002,
            UNICODE = 0x0004,
            SCANCODE = 0x0008,
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct MOUSEINPUT
        {
            public int dx;
            public int dy;
            public int mouseData;
            public int dwFlags;
            public int time;
            public IntPtr dwExtraInfo;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct KEYBDINPUT
        {
            public short wVk;
            public short wScan;
            public int dwFlags;
            public int time;
            public IntPtr dwExtraInfo;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct HARDWAREINPUT
        {
            public int uMsg;
            public short wParamL;
            public short wParamH;
        }

        [StructLayout(LayoutKind.Explicit)]
        private struct INPUT
        {
            [FieldOffset(0)]
            public int type;
            [FieldOffset(4)]
            public MOUSEINPUT mi;
            [FieldOffset(4)]
            public KEYBDINPUT ki;
            [FieldOffset(4)]
            public HARDWAREINPUT hi;
        }

        /// <summary>
        /// Single Int KeyCode hex or ascii
        /// </summary>
        /// 
        /// 
        public static uint SendKey(int scanCode, bool press)
        {
            INPUT[] input = new INPUT[1];
            input[0].type = (int)InputType.INPUT_KEYBOARD;
            input[0].ki = new KEYBDINPUT();
            input[0].ki.dwFlags = (int)KEYEVENTF.UNICODE;

            if ((scanCode & 0xFF00) == 0xE000)
            {
                // extended key?
                input[0].ki.dwFlags |= (int)KEYEVENTF.EXTENDEDKEY;
            }

            if (press)
            {
                // press?
                input[0].ki.wScan = (short)(scanCode & 0xFF);
            }
            else
            {
                // release?
                input[0].ki.wScan = (short)scanCode;
                input[0].ki.dwFlags |= (int)KEYEVENTF.KEYUP;
            }

            return(SendInput(1, input, Marshal.SizeOf(input[0])));
        }

        // This function simulates a simple mouseclick at the current cursor position.
        public static uint Click()
        {
            var input_down = new INPUT
            {

                mi =
                {
                    dx = 0,
                    dy = 0,
                    mouseData = 0,
                    dwFlags = (int)MOUSEEVENTF.LEFTDOWN
                }
            };

            INPUT input_up = input_down;
            input_up.mi.dwFlags = (int)MOUSEEVENTF.LEFTUP;

            INPUT[] input = { input_down, input_up };

            return SendInput(2, input, Marshal.SizeOf(input_down));
        }
        // ReSharper restore InconsistentNaming

        //New methods to set x/y and perform keyboard input - GJL 08/31/09
        public static uint Click(int x, int y, bool leftClick)
        {
            var inputDown =
                new INPUT
                {
                    mi =
                        {
                            dx = x,
                            dy = y,
                            mouseData = 0,
                            dwFlags = (int) ((leftClick ? MOUSEEVENTF.LEFTDOWN : MOUSEEVENTF.RIGHTDOWN))
                        }
                };
            INPUT inputUp = inputDown;
            inputUp.mi.dwFlags = (int) ((leftClick ? MOUSEEVENTF.LEFTDOWN : MOUSEEVENTF.RIGHTDOWN));

            INPUT[] input = { inputDown, inputUp };
            return (SendInput(2, input, Marshal.SizeOf(inputDown)));
        }

/*        public static uint TabOut(int x, int y)
        {
            var inputDown =
                new INPUT
                {
                    ki =
                        {
                            wVk = VK_TAB,

                        }
                }
        }*/
    }   
		 


}
