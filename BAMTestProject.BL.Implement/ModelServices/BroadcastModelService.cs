using System.Data.Entity.Migrations;
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
        //TODO: Rework edit system
        public Broadcast Edit(int id, Broadcast entity)
        {
            var broadcast = _dbContext.Broadcasts.FirstOrDefault(x => x.Id == id);
            _dbContext.Broadcasts.AddOrUpdate(entity);
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
