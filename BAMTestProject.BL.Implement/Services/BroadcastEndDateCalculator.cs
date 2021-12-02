using System;
using System.Collections.Generic;
using BAMTestProject.BL.Implementation.BaseServices;

namespace BAMTestProject.BL.Implementation.Services
{
    public class BroadcastEndDateCalculator : IBroadcastEndDateCalculator
    {
        public DateTime Calculate(DateTime startDate, int viewCount, List<DayOfWeek> weekDays)
        {
            var endDate = startDate;
            var amountOfWeeks = GetAmountOfWeeks(viewCount, weekDays);
            var viewsCount = viewCount - amountOfWeeks * weekDays.Count;
            return !IsHasExtraWeek(viewCount, weekDays) 
                ? OddWeekCalculator(startDate, amountOfWeeks, weekDays, viewsCount)
                : NonOddWeekCalculator(endDate, amountOfWeeks);
        }

        private int GetAmountOfWeeks(int viewCount, List<DayOfWeek> weekDays)
        {
            var amountOfWeeks = viewCount / weekDays.Count;
            return amountOfWeeks;
        }

        private DateTime OddWeekCalculator(DateTime newDate,int amountOfWeeks, List<DayOfWeek> weekDays, int viewsCount)
        {
            return newDate.AddDays(amountOfWeeks * 7 + (int) weekDays[viewsCount]);
        }

        private DateTime NonOddWeekCalculator(DateTime newDate, int amountOfWeeks)
        {
            return newDate.AddDays(amountOfWeeks * 7);
        }
        private bool IsHasExtraWeek(int viewCount, List<DayOfWeek> weekDays)
        {
            return (viewCount % weekDays.Count) == 0;
        }
    }
}
