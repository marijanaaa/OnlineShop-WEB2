namespace online_selling.Interfaces.Repository
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> AddAsync (T entity);
        T AddSync (T entity);
        T UpdateSync(T entity);
        Task<List<T>>? GetAllAsync ();
        void UpdateEntitiesSync (List<T> entities);
    }
}
