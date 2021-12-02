using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using BAMTestProject.DAL.Implementation.BaseEntities;

namespace BAMTestProject.DAL.Implementation.Entities
{
    public class Broadcast : BaseEntity
    {
        public int ShowId { get; set; }
        [ForeignKey(nameof(ShowId))] public Show Show { get; set; }
        public DateTime StartDate { get; set; }
        public string BroadcastDays { get; set; }

        [NotMapped]
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
        [NotMapped] public DateTime EndDate { get; set; }
        public int MarketId { get; set; }
        [ForeignKey(nameof(MarketId))] public Market Market { get; set; }
    }
}
