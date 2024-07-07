using MovieLayeredWithoutPictures.DAL.Entities;

namespace MovieLayeredWithoutPictures.DAL.Interfaces
{
    // Интерфейс IUnitOfWork, определяющий контракт для паттерна Unit of Work
    public interface IUnitOfWork
    {
        IRepository<Movie> Movies { get; }
        Task Save();
    }
}
