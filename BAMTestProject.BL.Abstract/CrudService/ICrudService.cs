namespace BAMTestProject.BL.Abstract.CrudService
{
    public interface ICrudService<T> where T : class
    {
        void Insert(T entity);

        T Read(int id);

        T Edit(int id,T entity);

        void Delete(int id);
    }
}
