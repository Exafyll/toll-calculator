using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TollFeeCalculator
{
    internal struct Date
    {
        private int _month;
        private int _day;

        public int Month { get => _month; }
        public int Day { get => _day; }
        public Date( int month, int day)
        {
            _month = month;
            _day = day;
        }
        
    }
    internal struct DateRange(Date start, Date end)
    {
        public bool Contains(DateTime dt)
        {
            int m = dt.Month, d = dt.Day;
            if (start.Month == end.Month)
                return m == start.Month && d >= start.Day && d <= end.Day;

            if (m == start.Month) return d >= start.Day;
            if (m == end.Month) return d <= end.Day;

            return m > start.Month && m < end.Month;
        }
    }
}
