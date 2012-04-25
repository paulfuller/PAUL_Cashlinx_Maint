using System;
using System.Text;

namespace Common.Libraries.Objects.Config
{
    public class ShopCalendarVO
    {
        public static readonly DateTime NULLTIME = DateTime.MinValue;
        public static readonly string NAMEOFDAY = "NameOfDay:";
        public static readonly string OPENTIME = ",OpenTime:";
        public static readonly string CLOSETIME = ",CloseTime:";
        public static readonly string CALDATE = ",CalendarDate:";
        public static readonly string WORKDAY = ",IsWorkday:";
        public static readonly string DAYOFWEEK = ",IsDayOfWeek:";
        public static readonly string HOLIDAY = ",IsHoliday:";

        public string NameOfDay { get; private set; }
        public DateTime OpenTime { get; private set; }
        public DateTime CloseTime { get; private set; }
        public DateTime CalendarDate { get; private set; }
        public bool WorkDay { get; private set; }
        public bool IsDayOfWeek { get; private set; }
        public bool IsHoliday { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public ShopCalendarVO()
        {
            this.NameOfDay = string.Empty;
            this.OpenTime = NULLTIME;
            this.CloseTime = NULLTIME;
            this.CalendarDate = NULLTIME;
            this.WorkDay = false;
            this.IsDayOfWeek = false;
            this.IsHoliday = false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nmOfDay"></param>
        public void setNameOfDay(string nmOfDay)
        {
            if (this.NameOfDay == string.Empty &&
                !string.IsNullOrEmpty(nmOfDay))
            {
                this.NameOfDay = nmOfDay;
                this.IsDayOfWeek = true;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="oTime"></param>
        public void setOpenTime(DateTime oTime)
        {
            if (this.OpenTime == NULLTIME)
            {
                this.OpenTime = oTime;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cTime"></param>
        public void setCloseTime(DateTime cTime)
        {
            if (this.CloseTime == NULLTIME)
            {
                this.CloseTime = cTime;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cDate"></param>
        public void setCalendarDate(DateTime cDate)
        {
            if (this.CalendarDate == NULLTIME)
            {
                this.CalendarDate = cDate;
            }
        }

        public void setWorkdayFlag(bool wkDayFlag)
        {
            this.WorkDay = wkDayFlag;
        }

        public void setHolidayFlag(bool holFlag)
        {
            this.IsHoliday = holFlag;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine();
            sb.Append(NAMEOFDAY);
            sb.Append(this.NameOfDay);
            sb.Append(OPENTIME);
            sb.Append(this.OpenTime);
            sb.Append(CLOSETIME);
            sb.Append(this.CloseTime);
            sb.Append(CALDATE);
            sb.Append(this.CalendarDate);
            sb.Append(WORKDAY);
            sb.Append(this.WorkDay);
            sb.Append(DAYOFWEEK);
            sb.Append(this.IsDayOfWeek);
            sb.Append(HOLIDAY);
            sb.Append(this.IsHoliday);
            return (sb.ToString());
        }
    }
}
