using System.Collections.Generic;

namespace Common.Libraries.Utility.Logger
{
    public delegate void AuditLogHandler(IDictionary<string, object> auditData);
    public delegate void AuditLogEnabledChangeHandler(bool prevEnabled, bool newEnabled);

    public interface IAuditLogger
    {
        bool IsEnabled { get; }
        void SetEnabled(bool enabled);
        void SetAuditLogHandler(AuditLogHandler aHandler);
        void SetAuditLogEnabledChangeHandler(AuditLogEnabledChangeHandler aEnHandler);
        void LogAuditMessage(object aType, IDictionary<string, object> auditData);
    }
}
