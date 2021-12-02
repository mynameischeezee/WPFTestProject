using System;
using System.Collections.Generic;
using BAMTestProject.BL.Abstract.Services;

namespace BAMTestProject.BL.Implement.Services
{
    public class BroadcastEndDateCalculator : IBroadcastEndDateCalculator
    {
        //TODO: Divide methods in ternary operator
        public DateTime Calculate(DateTime startDate, int viewCount, List<DayOfWeek> weekDays)
        {
            var endDate = startDate;
            var amountOfWeeks = GetAmountOfWeeks(viewCount, weekDays);
            var viewsCount = viewCount - amountOfWeeks * weekDays.Count;
            return !IsHasExtraWeek(viewCount, weekDays) 
                ? endDate.AddDays(amountOfWeeks * 7 + (int)weekDays[viewsCount])
                : endDate.AddDays(amountOfWeeks * 7);
        }

        private int GetAmountOfWeeks(int viewCount, List<DayOfWeek> weekDays)
        {
            var amountOfWeeks = viewCount / weekDays.Count;
            return amountOfWeeks;
        }

        private bool IsHasExtraWeek(int viewCount, List<DayOfWeek> weekDays)
        {
            return (viewCount % weekDays.Count) == 0;
        }
    }
}
