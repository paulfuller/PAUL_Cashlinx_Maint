using System;

namespace Common.Libraries.Utility.RawTextPrinting
{
    [Flags]
    public enum RawTextFlags
    {
        None = 0x00000000,
        Left = 0x00000001,
        Right = 0x00000002,
        Center = 0x00000004,
        ForceUpper = 0x00000010,
        ForceLower = 0x00000020,
    }
}
