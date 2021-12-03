using System.Collections.ObjectModel;

namespace BAMTestProject.BL.Implementation.BaseRepositories
{
    public interface IBaseRepository<T> where T : class
    {
        void Delete(int id);
        void Edit(int id, T entity);
        void Insert(T entity);
        T Read(int id);
        T Search(int id);
        ObservableCollection<T> GetAll();
    }
}


