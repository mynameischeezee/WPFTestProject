using System;
using System.Collections.Generic;
using BAMTestProject.BL.Abstract.Services;
using BAMTestProject.BL.Implement.DTO;

namespace BAMTestProject.BL.Implement.Factory
{
    public class BroadcastDtoFactory
    {
        private readonly IBroadcastEndDateCalculator _calculator;
        public BroadcastDtoFactory(IBroadcastEndDateCalculator calculator)
        {
            _calculator = calculator;
        }
        public BroadcastDto Create(DateTime startDate, int viewsCount, List<DayOfWeek> daysOfWeeks)
        {
            return new BroadcastDto { EndDate = _calculator.Calculate(startDate, viewsCount, daysOfWeeks)};
        }
    }
}
