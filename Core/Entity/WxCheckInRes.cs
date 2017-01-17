using System;

namespace YUNkefu.Core.Entity
{
    public class WxCheckInRes
    {
        public int TodayRank { get; set; }
        public int MonthRank { get; set; }
        public bool IsSuccess { get; set; }
        public int TotalCount { get; set; }
        public bool IsRepeat { get; set; }
        public DateTime LastCheckInTime { get; set; }
    }
}
