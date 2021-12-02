using System.Data.Entity.Migrations;
using System.Linq;
using BAMTestProject.DAL.Implementation;
using BAMTestProject.DAL.Implementation.Entities;

namespace BAMTestProject.BL.Implement.Repositories
{
    public class MarketModelService
    {
        private readonly ApplicationDbContext _dbContext;

        public MarketModelService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Delete(int id)
        {
            var market = _dbContext.Markets.FirstOrDefault(x => x.Id == id);
            if (market != null) _dbContext.Markets.Remove(market);
            _dbContext.SaveChanges();
        }

        public Market Edit(int id, Market entity)
        {
            var market = _dbContext.Markets.FirstOrDefault(x => x.Id == id);
            _dbContext.Markets.AddOrUpdate(entity);
            _dbContext.SaveChanges();
            return market;
        }

        public void Insert(Market entity)
        {
            _dbContext.Markets.Add(entity);
            _dbContext.SaveChanges();
        }

        public Market Read(int id)
        {
            return _dbContext.Markets.FirstOrDefault(x => x.Id == id);
        }
    }
}
