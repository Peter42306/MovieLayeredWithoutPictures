using AutoMapper;
using MovieLayeredWithoutPictures.BLL.DTO;
using MovieLayeredWithoutPictures.BLL.Interfaces;
using MovieLayeredWithoutPictures.DAL.Entities;
using MovieLayeredWithoutPictures.DAL.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace MovieLayeredWithoutPictures.BLL.Services
{
    public class MovieService : IMovieService
    {
        IUnitOfWork UnitOfWork { get; set; }

        public MovieService(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        // Automapper позволяет проецировать одну модель на другую, что позволяет сократить
        public async Task<IEnumerable<MovieDTO>> GetMovies()
        {
            var config=new MapperConfiguration(cfg=>cfg.CreateMap<Movie,MovieDTO>());
            var mapper = new Mapper(config);
            return mapper.Map<IEnumerable<Movie>,IEnumerable<MovieDTO>>(await UnitOfWork.Movies.GetAll());


            //// Без использования AutoMapper
            //// Получаем все фильмы из репозитория
            //var movies = await UnitOfWork.Movies.GetAll();

            //// Преобразуем каждую сущность Movie в MovieDTO
            //return movies.Select(m => new MovieDTO
            //{
            //    Id = m.Id,
            //    Title = m.Title,
            //    Director = m.Director,
            //    Genre = m.Genre,
            //    ReleaseYear = m.ReleaseYear,
            //    Description = m.Description
            //});
        }

        public async Task<MovieDTO> GetMovie(int id)
        {
            var movie=await UnitOfWork.Movies.Get(id);

            if (movie==null)
            {
                throw new ValidationException("Неверное значение");
            }

            return new MovieDTO
            {
                Id = movie.Id,
                Title = movie.Title,
                Director = movie.Director,
                Genre = movie.Genre,
                ReleaseYear = movie.ReleaseYear,
                Description = movie.Description
            };
        }

        public async Task<MovieDTO> GetMovie(string title)
        {
            var movie = await UnitOfWork.Movies.Get(title);

            if (movie == null)
            {
                throw new ValidationException("Неверное значение");
            }

            return new MovieDTO
            {
                Id = movie.Id,
                Title = movie.Title,
                Director = movie.Director,
                Genre = movie.Genre,
                ReleaseYear = movie.ReleaseYear,
                Description = movie.Description
            };
        }        

        public async Task CreateMovie(MovieDTO movieDTO)
        {
            var movie = new Movie
            {
                Id = movieDTO.Id,
                Title = movieDTO.Title,
                Director = movieDTO.Director,
                Genre = movieDTO.Genre,
                ReleaseYear = movieDTO.ReleaseYear,
                Description = movieDTO.Description
            };

            await UnitOfWork.Movies.Create(movie);
            await UnitOfWork.Save();
        }

        public async Task UpdateMovie(MovieDTO movieDTO)
        {
            var movie = new Movie
            {
                Id = movieDTO.Id,
                Title = movieDTO.Title,
                Director = movieDTO.Director,
                Genre = movieDTO.Genre,
                ReleaseYear = movieDTO.ReleaseYear,
                Description = movieDTO.Description
            };

            UnitOfWork.Movies.Update(movie);            
            await UnitOfWork.Save();
        }
        public async Task DeleteMovie(int id)
        {
            await UnitOfWork.Movies.Delete(id);
            await UnitOfWork.Save();
        }
    }
}
