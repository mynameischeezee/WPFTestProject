using System;
using System.Linq;
using BAMTestProject.BL.Abstract.CrudService;
using BAMTestProject.DAL.Implementation;
using BAMTestProject.DAL.Implementation.Models;

namespace BAMTestProject.BL.Implement.ModelServices
{
    public class MarketModelService : ICrudService<Market>
    {
        public void Delete(int id)
        {
            using (var dbContext = new ApplicationDbContext())
            {
                var marketToDelete = dbContext.Markets.FirstOrDefault(x => x.Id == id);
                dbContext.Markets.Remove(marketToDelete);
                dbContext.SaveChanges();
            }
        }

        public Market Edit(int id, Market entity)
        {
            using (var dbContext = new ApplicationDbContext())
            {
                var updatedMarket = dbContext.Markets.FirstOrDefault(x => x.Id == id);
                updatedMarket = entity;
                updatedMarket.Id = id;
                dbContext.SaveChanges();
                return updatedMarket;
            }
        }

        public void Insert(Market entity)
        {
            using (var dbContext = new ApplicationDbContext())
            {
                dbContext.Markets.Add(entity);
                dbContext.SaveChanges();
            }
        }

        public Market Read(int id)
        {
            using (var dbContext = new ApplicationDbContext())
            {
                return dbContext.Markets.FirstOrDefault(x => x.Id == id);
            }
        }
    }
}
