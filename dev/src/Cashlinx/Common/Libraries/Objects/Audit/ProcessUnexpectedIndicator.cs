using System;

namespace Common.Libraries.Objects.Audit
{
    [Flags]
    public enum ProcessUnexpectedIndicator
    {
        ChargedOn = 0x00000001,
        Reactivated = 0x00000002,
        Unexpected = 0x00000004,
        Unscanned = 0x00000008,
    }
}
