using System;
using System.Collections.Generic;

namespace BAMTestProject.BL.Abstract.Services
{
    public interface IBroadcastEndDateCalculator
    {
        DateTime Calculate(DateTime startDate, int viewCount, List<DayOfWeek> weekDays);
    }
}
