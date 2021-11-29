using System;
using System.Linq;
using BAMTestProject.BL.Abstract.CrudService;
using BAMTestProject.DAL.Implementation;
using BAMTestProject.DAL.Implementation.Models;

namespace BAMTestProject.BL.Implement.ModelServices
{
    public class ShowModelService : ICrudService<Show>
    {
        public void Insert(Show entity)
        {
            using (var dbContext = new ApplicationDbContext())
            {
                dbContext.Shows.Add(entity);
                dbContext.SaveChanges();
            }
        }

        public Show Read(int id)
        {
            using (var dbContext = new ApplicationDbContext())
            {
                return dbContext.Shows.FirstOrDefault(x => x.Id == id);
            }
        }
        //TODO: rework edit system
        public Show Edit(int id,Show entity)
        {
            using (var dbContext = new ApplicationDbContext())
            {
                var updatedShow = dbContext.Shows.FirstOrDefault(x => x.Id == id);
                updatedShow.Name = entity.Name;
                dbContext.SaveChanges();
                return updatedShow;
            }
        }

        public void Delete(int id)
        {
            using (var dbContext = new ApplicationDbContext())
            {
                var showToDelete = dbContext.Shows.FirstOrDefault(x => x.Id == id);
                dbContext.Shows.Remove(showToDelete);
                dbContext.SaveChanges();
            }
        }
    }
}
