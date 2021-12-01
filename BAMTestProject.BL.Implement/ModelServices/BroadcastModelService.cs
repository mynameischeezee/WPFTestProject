using System.Linq;
using BAMTestProject.BL.Abstract.CrudService;
using BAMTestProject.DAL.Implementation;
using BAMTestProject.DAL.Implementation.Models;

namespace BAMTestProject.BL.Implement.ModelServices
{
    public class BroadcastModelService : ICrudService<Broadcast>
    {
        private readonly ApplicationDbContext _dbContext;

        public BroadcastModelService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Delete(int id)
        {
            var broadcast = _dbContext.Broadcasts.FirstOrDefault(x => x.Id == id);
            if (broadcast != null) _dbContext.Broadcasts.Remove(broadcast);
            _dbContext.SaveChanges();
        }

        public Broadcast Edit(int id, Broadcast entity)
        {
            var broadcast = _dbContext.Broadcasts.FirstOrDefault(x => x.Id == id);
            _dbContext.Broadcasts.Attach(broadcast);
                broadcast.Days = entity.Days;
                broadcast.MarketId = entity.Id;
                broadcast.ShowId = entity.ShowId;
                broadcast.StartDate = entity.StartDate;
                broadcast.EndDate = entity.EndDate;
                broadcast.BroadcastDays = entity.BroadcastDays;
                broadcast.ShowsAmount = entity.ShowsAmount;
                broadcast.Show = entity.Show;
                broadcast.Market = entity.Market;
                _dbContext.SaveChanges();
                return broadcast;
        }

        public void Insert(Broadcast entity)
        {
            _dbContext.Broadcasts.Add(entity);
            _dbContext.SaveChanges();
        }

        public Broadcast Read(int id)
        {
            return _dbContext.Broadcasts.FirstOrDefault(x => x.Id == id);
        }
    }
}
