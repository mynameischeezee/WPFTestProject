using System;
using System.Linq;
using BAMTestProject.BL.Abstract.CrudService;
using BAMTestProject.DAL.Implementation;
using BAMTestProject.DAL.Implementation.Models;

namespace BAMTestProject.BL.Implement.ModelServices
{
    public class BroadcastModelService : ICrudService<Broadcast>
    {
        public void Delete(int id)
        {
            using (var dbContext = new ApplicationDbContext())
            {
                var broadcastToDelete = dbContext.Broadcasts.FirstOrDefault(x => x.Id == id);
                dbContext.Broadcasts.Remove(broadcastToDelete);
                dbContext.SaveChanges();
            }
        }

        public Broadcast Edit(int id, Broadcast entity)
        {
            using (var dbContext = new ApplicationDbContext())
            {
                var updatedBroadcast = dbContext.Broadcasts.FirstOrDefault(x => x.Id == id);
                updatedBroadcast = entity;
                updatedBroadcast.Id = id;
                dbContext.SaveChanges();
                return updatedBroadcast;
            }
        }

        public void Insert(Broadcast entity)
        {
            using (var dbContext = new ApplicationDbContext())
            {
                dbContext.Broadcasts.Add(entity);
                dbContext.SaveChanges();
            }
        }

        public Broadcast Read(int id)
        {
            using (var dbContext = new ApplicationDbContext())
            {
                return dbContext.Broadcasts.FirstOrDefault(x => x.Id == id);
            }
        }
    }
}
