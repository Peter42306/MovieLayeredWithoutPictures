using MovieLayeredWithoutPictures.DAL.EF;
using MovieLayeredWithoutPictures.DAL.Entities;
using MovieLayeredWithoutPictures.DAL.Interfaces;

namespace MovieLayeredWithoutPictures.DAL.Repositories
{
    /*
     * Паттерн Unit of Work позволяет упростить работу с различными репозиториями и дает уверенность, 
     * что все репозитории будут использовать один и тот же контекст данных.
    */

    // Класс, реализующий паттерн Unit of Work для работы с контекстом данных и репозиториями
    public class EFUnitOfWork : IUnitOfWork
    {
        private MovieContext _movieContext; // Контекст базы данных для работы с сущностями Movie
        private MovieRepository _movieRepository; // Репозиторий для управления сущностями Movie

        // Конструктор, принимающий контекст базы данных и инициализирующий его
        public EFUnitOfWork(MovieContext movieContext)
        {
            _movieContext = movieContext;
        }

        // Свойство для доступа к репозиторию фильмов
        public IRepository<Movie> Movies
        {
            get
            {
                // Инициализация репозитория при первом обращении к нему
                if (_movieRepository == null)
                {
                    _movieRepository = new MovieRepository(_movieContext);
                }
                return _movieRepository;
            }
        }

        // Асинхронный метод для сохранения изменений в базе данных
        public async Task Save()
        {
            await _movieContext.SaveChangesAsync();
        }
    }
}
