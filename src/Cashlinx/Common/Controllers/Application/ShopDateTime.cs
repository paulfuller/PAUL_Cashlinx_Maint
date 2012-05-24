using System;
using Common.Libraries.Objects.Config;

namespace Common.Controllers.Application
{
    public sealed class ShopDateTime : MarshalByRefObject
    {
        /// <summary>
        /// Internal class lock object
        /// </summary>
        static readonly object mutexIntObj = new object();
        /// <summary>
        /// Singleton instance variable
        /// </summary>
#if __MULTI__
        // ReSharper disable InconsistentNaming
        static readonly object mutexObj = new object();
        static readonly Dictionary<int, ShopDateTime> multiInstance =
            new Dictionary<int, ShopDateTime>();
        // ReSharper restore InconsistentNaming
#else
        static readonly ShopDateTime instance = new ShopDateTime();
#endif
        /// <summary>
        /// Static constructor - forces compiler to initialize the object prior to any code access
        /// </summary>
        static ShopDateTime()
        {
        }

        public override object InitializeLifetimeService()
        {
            return null;
        }


        /// <summary>
        /// Static instance property accessor
        /// </summary>
        public static ShopDateTime Instance
        {
            get
            {
#if (!__MULTI__)
                return (instance);
#else
                lock (mutexObj)
                {
                    int tId = Thread.CurrentThread.ManagedThreadId;
                    if (multiInstance.ContainsKey(tId))
                    {
                        return (multiInstance[tId]);
                    }
                    var sdT = new ShopDateTime();
                    multiInstance.Add(tId, sdT);
                    return (sdT);
                }
#endif
            }
        }

        #region Private Fields
        private int offsetYears;
        private int offsetMonths;
        private int offsetDays;
        private int offsetHours;
        private int offsetMinutes;
        private int offsetSeconds;
        private int offsetMilliseconds;
        private bool useDefaultTimeZone;
        private string overrideTimeZone;        
        private DateTime shopDate;
        private TimeSpan shopTime;
        private DateTime fullShopDateTime;
        private TimeZoneInfo shopTimeZone;
        private string shortShopTime;
        private bool databaseShopTimeSet;
        private bool shopTimeOffsetSet;
        private DateTime databaseShopTime;
        private TimeSpan databaseShopTimeDifference;
        private DateTime shopDateTimeInGMT;
        private string shopTransactionTime;
        private DateTime shopDateCurTime;
        private bool pawnSecTimeOffsetSet;
        private long pawnSecOffsetSpanYears;
        private long pawnSecOffsetSpanMonths;
        private long pawnSecOffsetSpanDays;
        private long pawnSecOffsetSpanHours;
        private long pawnSecOffsetSpanMinutes;
        private long pawnSecOffsetSpanSeconds;
        private long pawnSecOffsetSpanMilliseconds;
        #endregion

        #region Accessors
        public DateTime ShopDate
        {
            get
            {
                this.computeCurrentShopTime();
                return (this.shopDate);
            }
        }

        public DateTime FullShopDateTime
        {
            get
            {
                this.computeCurrentShopTime();
                return (this.fullShopDateTime);
            }
        }

        public DateTime ShopDateCurTime
        {
            get
            {
                this.computeCurrentShopTime();
                return (this.shopDateCurTime);
            }
        }

        public DateTime ShopDateInGMT
        {
            get
            {
                this.computeCurrentShopTime();
                return (this.shopDateTimeInGMT);
            }
        }

        public string ShopTransactionTime
        {
            get
            {
                this.computeCurrentShopTime();
                return (this.shopTransactionTime);
            }
        }

        public TimeSpan ShopTime
        {
            get
            {
                this.computeCurrentShopTime();
                return (this.shopTime);
            }
        }

        public string ShopShortTime
        {
            get
            {
                this.computeCurrentShopTime();
                return (this.shortShopTime);
            }
        }

        public TimeZoneInfo ShopTimeZone
        {
            get
            {
                return (this.shopTimeZone);
            }
        }

        public int YearOffset
        {
            get
            {
                return (this.offsetYears);
            }
        }

        public int MonthOffset
        {
            get
            {
                return (this.offsetMonths);
            }
        }

        public int DayOffset
        {
            get
            {
                return (this.offsetDays);
            }
        }

        public int HourOffset
        {
            get
            {
                return (this.offsetHours);
            }
        }

        public int MinuteOffset
        {
            get
            {
                return (this.offsetMinutes);
            }
        }

        public int SecondOffset
        {
            get
            {
                return (this.offsetSeconds);
            }
        }

        public int MillisecondOffset
        {
            get
            {
                return (this.offsetMilliseconds);
            }
        }

        #endregion

        private void computeCurrentShopTime()
        {
            DateTime curDateTime = DateTime.Now;
            if (this.databaseShopTimeSet)
            {
                curDateTime += this.databaseShopTimeDifference;
            }
            if (this.pawnSecTimeOffsetSet)
            {
                if (this.pawnSecOffsetSpanYears != 0)
                {
                    curDateTime = curDateTime.AddYears((int)this.pawnSecOffsetSpanYears);
                }
                if (this.pawnSecOffsetSpanMonths != 0)
                {
                    curDateTime = curDateTime.AddMonths((int)this.pawnSecOffsetSpanMonths);
                }
                if (this.pawnSecOffsetSpanDays != 0)
                {
                    curDateTime = curDateTime.AddDays((int)this.pawnSecOffsetSpanDays);
                }
                if (this.pawnSecOffsetSpanHours != 0)
                {
                    curDateTime = curDateTime.AddHours(this.pawnSecOffsetSpanMinutes);
                }
                if (this.pawnSecOffsetSpanMinutes != 0)
                {
                    curDateTime = curDateTime.AddMinutes(this.pawnSecOffsetSpanMinutes);
                }
                if (this.pawnSecOffsetSpanSeconds != 0)
                {
                    curDateTime = curDateTime.AddSeconds(this.pawnSecOffsetSpanSeconds);
                }
                if (this.pawnSecOffsetSpanMilliseconds != 0)
                {
                    curDateTime = curDateTime.AddMilliseconds(this.pawnSecOffsetSpanMilliseconds);
                }
            }
            if (this.shopTimeOffsetSet)
            {
                if (this.offsetYears != 0)
                {
                    curDateTime = curDateTime.AddYears(this.offsetYears);
                }
                if (this.offsetMonths != 0)
                {
                    curDateTime = curDateTime.AddMonths(this.offsetMonths);
                }
                if (this.offsetDays != 0)
                {
                    curDateTime = curDateTime.AddDays(this.offsetDays);
                }
                if (this.offsetHours != 0)
                {
                    curDateTime = curDateTime.AddHours(this.offsetHours);
                }
                if (this.offsetMinutes != 0)
                {
                    curDateTime = curDateTime.AddMinutes(this.offsetMinutes);
                }
                if (this.offsetSeconds != 0)
                {
                    curDateTime = curDateTime.AddSeconds(this.offsetSeconds);
                }
                if (this.offsetMilliseconds != 0)
                {
                    curDateTime = curDateTime.AddMilliseconds(this.offsetMilliseconds);
                }
            }
            lock (mutexIntObj)
            {
                this.shopDate = curDateTime.Date;
                this.shopTime = curDateTime.TimeOfDay;
                this.shortShopTime = curDateTime.ToShortTimeString();
                this.shopDateTimeInGMT = curDateTime.ToUniversalTime();
                this.shopTransactionTime = shopDate.ToShortDateString() + " " + this.shopTime.ToString();
                this.shopDateCurTime = curDateTime.ToLocalTime();
                this.fullShopDateTime = curDateTime;
            }
        }

        /// <summary>
        /// Method performs the offset time adjustment against a passed in date
        /// </summary>
        /// <param name="dateToAdjust"></param>
        /// <param name="adjustedDate"></param>
        /// <returns></returns>
        public void AdjustDateTime(DateTime dateToAdjust, out DateTime adjustedDate)
        {
            adjustedDate = dateToAdjust;
            if (this.databaseShopTimeSet)
            {
                adjustedDate += this.databaseShopTimeDifference;
            }
            if (this.pawnSecTimeOffsetSet)
            {
                if (this.pawnSecOffsetSpanYears != 0)
                {
                    adjustedDate = adjustedDate.AddYears((int)this.pawnSecOffsetSpanYears);
                }
                if (this.pawnSecOffsetSpanMonths != 0)
                {
                    adjustedDate = adjustedDate.AddMonths((int)this.pawnSecOffsetSpanMonths);
                }
                if (this.pawnSecOffsetSpanDays != 0)
                {
                    adjustedDate = adjustedDate.AddDays((int)this.pawnSecOffsetSpanDays);
                }
                if (this.pawnSecOffsetSpanHours != 0)
                {
                    adjustedDate = adjustedDate.AddHours(this.pawnSecOffsetSpanMinutes);
                }
                if (this.pawnSecOffsetSpanMinutes != 0)
                {
                    adjustedDate = adjustedDate.AddMinutes(this.pawnSecOffsetSpanMinutes);
                }
                if (this.pawnSecOffsetSpanSeconds != 0)
                {
                    adjustedDate = adjustedDate.AddSeconds(this.pawnSecOffsetSpanSeconds);
                }
                if (this.pawnSecOffsetSpanMilliseconds != 0)
                {
                    adjustedDate = adjustedDate.AddMilliseconds(this.pawnSecOffsetSpanMilliseconds);
                }
            }
            if (this.shopTimeOffsetSet)
            {
                if (this.offsetYears != 0)
                {
                    adjustedDate = adjustedDate.AddYears(this.offsetYears);
                }
                if (this.offsetMonths != 0)
                {
                    adjustedDate = adjustedDate.AddMonths(this.offsetMonths);
                }
                if (this.offsetDays != 0)
                {
                    adjustedDate = adjustedDate.AddDays(this.offsetDays);
                }
                if (this.offsetHours != 0)
                {
                    adjustedDate = adjustedDate.AddHours(this.offsetHours);
                }
                if (this.offsetMinutes != 0)
                {
                    adjustedDate = adjustedDate.AddMinutes(this.offsetMinutes);
                }
                if (this.offsetSeconds != 0)
                {
                    adjustedDate = adjustedDate.AddSeconds(this.offsetSeconds);
                }
                if (this.offsetMilliseconds != 0)
                {
                    adjustedDate = adjustedDate.AddMilliseconds(this.offsetMilliseconds);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="yearOffset"></param>
        /// <param name="monthOffset"></param>
        /// <param name="dayOffset"></param>
        /// <param name="hourOffset"></param>
        /// <param name="minuteOffset"></param>
        /// <param name="secondsOffset"></param>
        /// <param name="millisecondsOffset"></param>
        public void setOffsets(
            int yearOffset, 
            int monthOffset, 
            int dayOffset, 
            int hourOffset,
            int minuteOffset,
            int secondsOffset,
            int millisecondsOffset)
        {
            lock (mutexIntObj)
            {
                this.offsetYears = yearOffset;
                this.offsetMonths = monthOffset;
                this.offsetDays = dayOffset;
                this.offsetHours = hourOffset;
                this.offsetMinutes = minuteOffset;
                this.offsetSeconds = secondsOffset;
                this.offsetMilliseconds = millisecondsOffset;
                this.shopTimeOffsetSet = true;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="useDefaultTZone"></param>
        /// <param name="overrideTZoneValue"></param>
        public void setTimezoneOverrides(
            bool useDefaultTZone,
            string overrideTZoneValue)
        {
            lock (mutexIntObj)
            {
                this.useDefaultTimeZone = useDefaultTZone;
                if (!this.useDefaultTimeZone)
                {
                    this.overrideTimeZone = overrideTZoneValue;
                    this.shopTimeZone = TimeZoneInfo.FindSystemTimeZoneById(this.overrideTimeZone);
                }
                else
                {
                    this.shopTimeZone = TimeZoneInfo.Local;
                }
               
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public ShopDateTime()
        {
            lock (mutexIntObj)
            {
                this.databaseShopTimeSet = false;
                var now = DateTime.Now;
                this.shopDate = now.Date;
                this.shopTime = now.TimeOfDay;
                this.shortShopTime = now.ToShortTimeString();
                this.shopTimeZone = TimeZoneInfo.Local;
                this.shopDateCurTime = now.Date.ToLocalTime();
                this.offsetDays = 0;
                this.offsetHours = 0;
                this.offsetMilliseconds = 0;
                this.offsetMinutes = 0;
                this.offsetMonths = 0;
                this.offsetSeconds = 0;
                this.offsetYears = 0;
                this.overrideTimeZone = shopTimeZone.DisplayName;
                this.useDefaultTimeZone = true;
                this.shopTimeOffsetSet = false;
                this.pawnSecTimeOffsetSet = false;
                this.fullShopDateTime = now;
            }
        }

        public void SetDatabaseTime(DateTime databaseTime)
        {
            lock (mutexIntObj)
            {
                if (this.databaseShopTimeSet == false)
                {
                    this.databaseShopTimeSet = true;
                    this.databaseShopTime = databaseTime;
                    this.databaseShopTimeDifference = DateTime.Now - this.databaseShopTime;
                }
            }
        }

        public void SetPawnSecOffsetTime(StoreConfigVO pawnSecOffset)
        {
            lock(mutexIntObj)
            {
                if (this.pawnSecTimeOffsetSet == false)
                {
                    this.pawnSecTimeOffsetSet = true;
                    this.pawnSecOffsetSpanYears = pawnSecOffset.YearOffset;
                    this.pawnSecOffsetSpanMonths = pawnSecOffset.MonthOffset;
                    this.pawnSecOffsetSpanDays = pawnSecOffset.DayOffset;
                    this.pawnSecOffsetSpanHours = pawnSecOffset.HourOffset;
                    this.pawnSecOffsetSpanMinutes = pawnSecOffset.MinuteOffset;
                    this.pawnSecOffsetSpanSeconds = pawnSecOffset.SecondOffset;
                    this.pawnSecOffsetSpanMilliseconds = pawnSecOffset.MillisecondOffset;
                }
            }
        }
    }
}
