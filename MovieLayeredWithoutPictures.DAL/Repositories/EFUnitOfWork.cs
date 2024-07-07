using MovieLayeredWithoutPictures.DAL.EF;
using MovieLayeredWithoutPictures.DAL.Entities;
using MovieLayeredWithoutPictures.DAL.Interfaces;

namespace MovieLayeredWithoutPictures.DAL.Repositories
{
    /*
     * Паттерн Unit of Work позволяет упростить работу с различными репозиториями и дает уверенность, 
     * что все репозитории будут использовать один и тот же контекст данных.
    */

    public class EFUnitOfWork : IUnitOfWork
    {
        private MovieContext _movieContext;
        private MovieRepository _movieRepository;

        public EFUnitOfWork(MovieContext movieContext)
        {
            _movieContext = movieContext;
        }

        public IRepository<Movie> Movies
        {
            get
            {
                if (_movieRepository == null)
                {
                    _movieRepository = new MovieRepository(_movieContext);
                }
                return _movieRepository;
            }
        }

        public async Task Save()
        {
            await _movieContext.SaveChangesAsync();
        }
    }
}
