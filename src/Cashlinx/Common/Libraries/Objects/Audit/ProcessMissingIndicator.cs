using System;

namespace Common.Libraries.Objects.Audit
{
    [Flags]
    public enum ProcessMissingIndicator
    {
        Found = 0x00000001,
        Reconciled = 0x00000002,
        ChargedOff = 0x00000004,
        Missing = 0x00000008,
    }
}
