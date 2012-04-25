using System;

namespace Common.Libraries.Objects.Authorization
{
    [Flags]
    public enum PawnSecApplication
    {
        None         = 0x00000000,
        Cashlinx     = 0x00000001,
        Support      = 0x00000002,
        Audit        = 0x00000004,
        AuditQueries = 0x00000008
    }
}
