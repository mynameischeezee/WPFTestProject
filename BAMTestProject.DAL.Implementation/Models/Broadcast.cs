using System;
using System.ComponentModel.DataAnnotations.Schema;
using BAMTestProject.DAL.Abstract.Model;

namespace BAMTestProject.DAL.Implementation.Models
{
    public class Broadcast : AbstractModel
    {
        public int? ShowId { get; set; }
        [ForeignKey(nameof(ShowId))] public Show Show { get; set; }
        public DateTime StartDate { get; set; }
        public DayOfWeek[] Days { get; set; }
        public int ShowsAmount { get; set; }
        public DateTime EndDate { get; set; }
        public int? MarketId { get; set; }
        [ForeignKey(nameof(MarketId))] public Market Market { get; set; }
    }
}
