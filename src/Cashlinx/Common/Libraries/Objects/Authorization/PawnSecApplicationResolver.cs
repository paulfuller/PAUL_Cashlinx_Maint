using System;

namespace Common.Libraries.Objects.Authorization
{
    public static class PawnSecApplicationResolver
    {
        public static int Resolve(PawnSecApplication application)
        {
            switch (application)
            {
                case PawnSecApplication.Cashlinx: return 1;
                case PawnSecApplication.Support: return 2;
                case PawnSecApplication.Audit: return 3;
                case PawnSecApplication.AuditQueries:
                    return 4;
                default:
                    throw new InvalidProgramException("Unable to resolve application: " + application.ToString());
            }
        }
    }
}
