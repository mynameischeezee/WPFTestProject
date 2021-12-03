using System.Collections.ObjectModel;
using System.Data.Entity.Migrations;
using System.Linq;
using BAMTestProject.BL.Implementation.BaseRepositories;
using BAMTestProject.DAL.Implementation;
using BAMTestProject.DAL.Implementation.Entities;

namespace BAMTestProject.BL.Implementation.Repositories
{
    public class MarketRepository : IBaseRepository<MarketEntity>
    {
        private readonly ApplicationDbContext _dbContext;

        public MarketRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Delete(int id)
        {
            var market = _dbContext.Markets.FirstOrDefault(x => x.Id == id);
            if (market != null) _dbContext.Markets.Remove(market);
            _dbContext.SaveChanges();
        }

        public void Edit(int id, MarketEntity entity)
        {
            _dbContext.Markets.AddOrUpdate(entity);
            _dbContext.SaveChanges();
        }

        public ObservableCollection<MarketEntity> GetAll()
        {
            return new ObservableCollection<MarketEntity>(_dbContext.Markets);
        }

        public void Insert(MarketEntity entity)
        {
            _dbContext.Markets.Add(entity);
            _dbContext.SaveChanges();
        }

        public MarketEntity Read(int id)
        {
            return _dbContext.Markets.FirstOrDefault(x => x.Id == id);
        }

        public MarketEntity Search(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}
