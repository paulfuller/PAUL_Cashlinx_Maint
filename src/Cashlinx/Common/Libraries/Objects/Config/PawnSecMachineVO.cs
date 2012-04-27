namespace Common.Libraries.Objects.Config
{
    public class PawnSecMachineVO
    {
        public ulong ClientId { get; set; }
        public string IPAddress { get; set; }
        public string MachineName { get; set; }
        public string MACAddress { get; set; }
        public bool IsAllowed { get; set; }
        public bool IsConnected { get; set; }
        public string AdobeOverride { get; set; }
        public string GhostOverride { get; set; }
        public string WorkstationName { get; set; }

        public PawnSecMachineVO()
        {
            ClientId = 0L;
            IPAddress = string.Empty;
            MachineName = string.Empty;
            MACAddress = string.Empty;
            IsAllowed = false;
            IsConnected = false;
            AdobeOverride = string.Empty;
            GhostOverride = string.Empty;
            WorkstationName = string.Empty;
        }

    }
}
