using System.Linq;
using BAMTestProject.BL.Abstract.CrudService;
using BAMTestProject.DAL.Implementation;
using BAMTestProject.DAL.Implementation.Models;

namespace BAMTestProject.BL.Implement.ModelServices
{
    public class ShowModelService : ICrudService<Show>
    {
        private readonly ApplicationDbContext _dbContext;

        public ShowModelService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Delete(int id)
        {
            var show = _dbContext.Shows.FirstOrDefault(x => x.Id == id);
            if (show != null) _dbContext.Shows.Remove(show);
            _dbContext.SaveChanges();
        }

        public Show Edit(int id, Show entity)
        {
            var show = _dbContext.Shows.FirstOrDefault(x => x.Id == id);
            _dbContext.Shows.Attach(show);
            show.Name = entity.Name;
            _dbContext.SaveChanges();
            return show;
        }

        public void Insert(Show entity)
        {
            _dbContext.Shows.Add(entity);
            _dbContext.SaveChanges();
        }

        public Show Read(int id)
        {
            return _dbContext.Shows.FirstOrDefault(x => x.Id == id);
        }
    }
}
