namespace BAMTestProject.BL.Implementation.BaseRepositories
{
    public interface IBaseRepository<T> 
        where T : class
    {
        void Delete(int id);
        T Edit(int id, T entity);
        void Insert(T entity);
        T Read(int id);
        T Search(int id);
    }
}


