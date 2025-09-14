using System;
using System.Globalization;
using TollFeeCalculator;
using static TollFeeCalculator.Vehicle;

namespace TollFeeCalculator {
    public class TollCalculator
    {
        private List<VehicleType> TollFreeVehicles = new List<VehicleType>
        {
            VehicleType.Motorbike,
            VehicleType.Tractor,
            VehicleType.Emergency,
            VehicleType.Diplomat,
            VehicleType.Foreign,
            VehicleType.Military
        };
        private List<Date> TollFreeDates = new()
        { 
            new Date(1, 1),
            new Date(3, 28), new Date(3, 29),
            new Date(4, 1), new Date(4, 30),
            new Date(5, 1), new Date(5, 8), new Date(5, 9),
            new Date(6, 5), new Date(6, 6), new Date(6, 21),
            new Date(11, 1),
            new Date(12, 24), new Date(12, 25), new Date(12, 26), new Date(12, 31),
        };

        private List<DateRange> TollFreeRanges = new()
        {
            new DateRange(new Date(7, 1), new Date(7, 31)),
        };
        private static readonly (TimeOnly From, TimeOnly To, int Fee)[] FeeTimes =
        {
            (new TimeOnly(06,00), new TimeOnly(06,30),  8), 
            (new TimeOnly(06,30), new TimeOnly(07,00), 13),  
            (new TimeOnly(07,00), new TimeOnly(08,00), 18),  
            (new TimeOnly(08,00), new TimeOnly(08,30), 13),  
            (new TimeOnly(08,30), new TimeOnly(15,00),  8),  
            (new TimeOnly(15,00), new TimeOnly(15,30), 13),  
            (new TimeOnly(15,30), new TimeOnly(17,00), 18),  
            (new TimeOnly(17,00), new TimeOnly(18,00), 13),  
            (new TimeOnly(18,00), new TimeOnly(18,30),  8),
        };



        public int GetTollFee(Vehicle vehicle, DateTime[] dates)
        {
            if (dates == null || dates.Length == 0 || vehicle == null) return 0;
            Array.Sort(dates);

            int totalFee = 0;

            DateTime windowStart = dates[0];
            int windowMax = GetTollFee(windowStart, vehicle);

            for (int i = 1; i < dates.Length; i++)
            {
                var t = dates[i];
                int fee = GetTollFee(t, vehicle);

                bool newDay = t.Date != windowStart.Date;
                bool newWindow = (t - windowStart).TotalMinutes > 60;

                if (newDay || newWindow)
                {
                    totalFee += windowMax;
                    windowStart = t;
                    windowMax = fee;
                }
                else
                {
                    if (fee > windowMax) windowMax = fee;
                }
            }

            totalFee += windowMax;
            if (totalFee > 60) totalFee = 60;
            return totalFee;
        }

        private bool IsTollFreeVehicle(Vehicle vehicle)
        {
            if (vehicle == null) return false;

            return TollFreeVehicles.Contains(vehicle.Type);
        }

        public int GetTollFee(DateTime date, Vehicle vehicle)
        {
            if (IsTollFreeDate(date) || IsTollFreeVehicle(vehicle)) return 0;

            var t = TimeOnly.FromDateTime(date);
            foreach (var (from, to, fee) in FeeTimes)
                if (t >= from && t < to)
                    return fee;

            return 0;
        }

        private bool IsTollFreeDate(DateTime date)
        {
            if (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday) return true;

            return (TollFreeRanges.Any(r => r.Contains(date))) || TollFreeDates.Contains(new Date(date.Month, date.Day));
        }
    }
}
