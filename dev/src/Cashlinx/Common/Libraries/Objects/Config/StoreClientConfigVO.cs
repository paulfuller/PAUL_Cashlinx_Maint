namespace Common.Libraries.Objects.Config
{
    public class StoreClientConfigVO
    {
        public ulong Id
        {
            get; set;
        }
        public string LogLevel { get; set; }
        public bool PrintEnabled { get; set; }
        public int TerminalNumber { get; set; }
        public int TraceLevel { get; set; }
        public string WorkstationId { get; set; }
        public bool CPNHSEnabled { get; set; }

        public StoreClientConfigVO()
        {
            Id = 0L;
            LogLevel = string.Empty;
            PrintEnabled = false;
            TerminalNumber = 0;
            TraceLevel = 0;
            WorkstationId = string.Empty;
            CPNHSEnabled = false;
        }
    }
}
