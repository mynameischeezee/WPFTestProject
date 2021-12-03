using System;
using System.Collections.Generic;
using System.Linq;
using BAMTestProject.DAL.Implementation.Entities;

namespace BAMTestProject.BL.Implementation.DTO
{
    public class BroadcastDto
    {
        public int ShowId { get; set; }
        public ShowEntity Show { get; set; }
        public DateTime StartDate { get; set; }
        public string BroadcastDays { get; set; }
        public List<DayOfWeek> Days
        {
            get
            {
                var convertedDays = BroadcastDays.Split(',');
                return convertedDays.Select(broadcastDay => (DayOfWeek) Enum.Parse(typeof(DayOfWeek), broadcastDay))
                    .ToList();
            }
            set => BroadcastDays = string.Join(",", value.Select(x => x.ToString()).ToArray());
        }
        public int ShowsAmount { get; set; }
        public int MarketId { get; set; }
        public DateTime EndDate;
    }
}
