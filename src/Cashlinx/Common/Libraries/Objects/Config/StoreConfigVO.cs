namespace Common.Libraries.Objects.Config
{
    public class StoreConfigVO
    {
        public ulong Id { get; set; }
        public string MetalsFile { get; set; }
        public string StonesFile { get; set; }
        public ulong FetchSizeMultiplier { get; set; }
        public string TimeZone { get; set; }
        public string CompanyName { get; set; }
        public string CompanyNumber { get; set; }
        public long MillisecondOffset { get; set; }
        public long SecondOffset { get; set; }
        public long HourOffset { get; set; }
        public long DayOffset { get; set; }
        public long MonthOffset { get; set; }
        public long YearOffset { get; set; }
        public long MinuteOffset { get; set; }
        public string StoreMode { get; set; }

        public StoreConfigVO()
        {
            this.Id = 0L;
            this.MetalsFile = string.Empty;
            this.StonesFile = string.Empty;
            this.FetchSizeMultiplier = 0L;
            this.TimeZone = string.Empty;
            this.MillisecondOffset = 0L;
            this.SecondOffset = 0L;
            this.HourOffset = 0L;
            this.DayOffset = 0L;
            this.MonthOffset = 0L;
            this.YearOffset = 0L;
            this.MinuteOffset = 0L;
            this.CompanyName = string.Empty;
            this.CompanyNumber = string.Empty;
            this.StoreMode = string.Empty;
        }
    }
}
