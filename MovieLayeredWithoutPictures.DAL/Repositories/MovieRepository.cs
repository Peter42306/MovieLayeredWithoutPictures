using Microsoft.EntityFrameworkCore;
using MovieLayeredWithoutPictures.DAL.EF;
using MovieLayeredWithoutPictures.DAL.Entities;
using MovieLayeredWithoutPictures.DAL.Interfaces;

namespace MovieLayeredWithoutPictures.DAL.Repositories
{
    public class MovieRepository : IRepository<Movie>
    {
        private MovieContext _movieContext;

        public MovieRepository(MovieContext movieContext)
        {
            _movieContext = movieContext;
        }

        public async Task<IEnumerable<Movie>> GetAll()
        {
            return await _movieContext.Movies.ToListAsync();
        }

        public async Task<Movie> Get(int id)
        {
            Movie? movie=await _movieContext.Movies.FindAsync(id);
            return movie;
        }

        public async Task<Movie> Get(string title)
        {
            var movies=await _movieContext.Movies.Where(x => x.Title == title).ToListAsync();
            Movie? movie=movies?.FirstOrDefault();
            return movie;
        }

        public async Task Create(Movie movie)
        {
            await _movieContext.Movies.AddAsync(movie);
            await _movieContext.SaveChangesAsync();

            //await _movieContext.Movies.AddAsync(movie);
        }

        public async void Update(Movie movie            )
        {
            _movieContext.Entry(movie).State = EntityState.Modified;
            await _movieContext.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            Movie? movie=await _movieContext.Movies.FirstOrDefaultAsync(x => x.Id == id);
            if (movie != null)
            {
                _movieContext.Movies.Remove(movie);
                await _movieContext.SaveChangesAsync();
            }

            //Movie? movie = await _movieContext.Movies.FirstOrDefaultAsync(x => x.Id == id);
            //if (movie != null)
            //{
            //    _movieContext.Movies.Remove(movie);                
            //}
        }        
    }
}
