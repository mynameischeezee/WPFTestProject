namespace BAMTestProject.BL.Abstract.CrudService
{
    //TODO: Remove crud service completely 
    public interface ICrudService<T> where T : class
    {
        void Insert(T entity);

        T Read(int id);

        T Edit(int id,T entity);

        void Delete(int id);
    }
}
