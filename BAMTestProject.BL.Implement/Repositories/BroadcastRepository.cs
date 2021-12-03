using System.Data.Entity.Migrations;
using System.Linq;
using BAMTestProject.BL.Implementation.BaseRepositories;
using BAMTestProject.DAL.Implementation;
using BAMTestProject.DAL.Implementation.Entities;

namespace BAMTestProject.BL.Implement.Repositories
{
    public class BroadcastRepository : IBaseRepository<BroadcastEntity>
    { 
        private readonly ApplicationDbContext _dbContext;

        public BroadcastRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Delete(int id)
        {
            var broadcast = _dbContext.Broadcasts.FirstOrDefault(x => x.Id == id);
            if (broadcast != null) _dbContext.Broadcasts.Remove(broadcast);
            _dbContext.SaveChanges();
        }
        //TODO: Rework edit system
        public BroadcastEntity Edit(int id, BroadcastEntity entity)
        {
            var broadcast = _dbContext.Broadcasts.FirstOrDefault(x => x.Id == id);
            _dbContext.Broadcasts.AddOrUpdate(entity);
            _dbContext.SaveChanges();
                return broadcast;
        }

        public void Insert(BroadcastEntity entity)
        {
            _dbContext.Broadcasts.Add(entity);
            _dbContext.SaveChanges();
        }

        public BroadcastEntity Read(int id)
        {
            return _dbContext.Broadcasts.FirstOrDefault(x => x.Id == id);
        }

        public BroadcastEntity Search(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}
