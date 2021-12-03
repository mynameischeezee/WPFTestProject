using System;
using System.Collections.Generic;
using System.Linq;
using BAMTestProject.DAL.Implementation.Entities;
using Caliburn.Micro;

namespace BAMTestProject.ViewModels
{
    public class BroadcastViewModel : Screen
    {
        public int Id { get; set; }
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

        public BroadcastViewModel Create(DateTime endDate, int id, int showId, ShowEntity show, DateTime startDate, int showsAmount, int marketId, List<DayOfWeek> days)
        {
            return new BroadcastViewModel()
            {
                EndDate = endDate,
                Id = id,
                ShowId = showId,
                Show = show,
                StartDate = startDate,
                Days = days,
                ShowsAmount = showsAmount,
                MarketId = marketId,
            };
        }
    }
}
