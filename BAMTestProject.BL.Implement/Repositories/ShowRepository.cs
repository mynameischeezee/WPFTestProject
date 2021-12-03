using System.Collections.ObjectModel;
using System.Data.Entity.Migrations;
using System.Linq;
using BAMTestProject.BL.Implementation.BaseRepositories;
using BAMTestProject.DAL.Implementation;
using BAMTestProject.DAL.Implementation.Entities;

namespace BAMTestProject.BL.Implement.Repositories
{
    public class ShowRepository : IBaseRepository<ShowEntity>
    {
        private readonly ApplicationDbContext _dbContext;

        public ShowRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Delete(int id)
        {
            var show = _dbContext.Shows.FirstOrDefault(x => x.Id == id);
            if (show != null) _dbContext.Shows.Remove(show);
            _dbContext.SaveChanges();
        }

        public void Edit(int id, ShowEntity entity)
        {
            _dbContext.Shows.AddOrUpdate(entity);
            _dbContext.SaveChanges();
        }

        public ObservableCollection<ShowEntity> GetAll()
        {
            return new ObservableCollection<ShowEntity>(_dbContext.Shows);
        }

        public void Insert(ShowEntity entity)
        {
            _dbContext.Shows.Add(entity);
            _dbContext.SaveChanges();
        }

        public ShowEntity Read(int id)
        {
            return _dbContext.Shows.FirstOrDefault(x => x.Id == id);
        }

        public ShowEntity Search(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}
