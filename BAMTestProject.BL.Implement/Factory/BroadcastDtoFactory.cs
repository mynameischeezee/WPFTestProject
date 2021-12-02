using System;
using System.Collections.Generic;
using BAMTestProject.BL.Abstract.Services;
using BAMTestProject.BL.Implement.DTO;

namespace BAMTestProject.BL.Implement.Factory
{
    public class BroadcastDtoFactory
    {
        //TODO: Rename service
        private readonly IBroadcastEndDateCalculator _calculationService;
        public BroadcastDtoFactory(IBroadcastEndDateCalculator calculationService)
        {
            _calculationService = calculationService;
        }
        public BroadcastDto Create(DateTime startDate, int viewsCount, List<DayOfWeek> daysOfWeeks)
        {
            return new BroadcastDto { EndDate = _calculationService.Calculate(startDate, viewsCount, daysOfWeeks)};
        }
    }
}
