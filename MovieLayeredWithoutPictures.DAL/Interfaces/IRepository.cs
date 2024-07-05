namespace MovieLayeredWithoutPictures.DAL.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> Get(int id);        
        Task<T> Get(string title);
        Task Create(T entity);
        void Update(T entity);
        Task Delete(int id);
    }
}
