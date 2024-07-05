using MovieLayeredWithoutPictures.DAL.Entities;

namespace MovieLayeredWithoutPictures.DAL.Interfaces
{
    public interface IUnitOfWork
    {
        IRepository<Movie> Movies { get; }
        Task Save();
    }
}
