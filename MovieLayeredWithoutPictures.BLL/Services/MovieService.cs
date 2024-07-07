using AutoMapper;
using MovieLayeredWithoutPictures.BLL.DTO;
using MovieLayeredWithoutPictures.BLL.Interfaces;
using MovieLayeredWithoutPictures.DAL.Entities;
using MovieLayeredWithoutPictures.DAL.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace MovieLayeredWithoutPictures.BLL.Services
{
    // Реализация сервиса для работы с фильмами
    public class MovieService : IMovieService
    {
        IUnitOfWork UnitOfWork { get; set; }

        // Конструктор, принимающий IUnitOfWork как параметр
        public MovieService(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        // Automapper позволяет проецировать одну модель на другую, что позволяет сократить
        // Метод для получения всех фильмов и преобразования их в DTO с помощью AutoMapper
        public async Task<IEnumerable<MovieDTO>> GetMovies()
        {
            // Конфигурация AutoMapper для отображения сущности Movie в DTO MovieDTO
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Movie, MovieDTO>());
            var mapper = new Mapper(config);

            // Получаем все фильмы из репозитория и маппим их в MovieDTO
            return mapper.Map<IEnumerable<Movie>, IEnumerable<MovieDTO>>(await UnitOfWork.Movies.GetAll());
        }

        // Метод для получения фильма по id
        public async Task<MovieDTO> GetMovie(int id)
        {
            var movie = await UnitOfWork.Movies.Get(id);

            if (movie == null)
            {
                throw new ValidationException("Неверное значение");
            }

            // Возвращаем DTO фильма
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

        // Метод для получения фильма по названию
        public async Task<MovieDTO> GetMovie(string title)
        {
            var movie = await UnitOfWork.Movies.Get(title);

            if (movie == null)
            {
                throw new ValidationException("Неверное значение");
            }

            // Возвращаем DTO фильма
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

        // Метод для создания нового фильма
        public async Task CreateMovie(MovieDTO movieDTO)
        {
            // Создаем объект Movie из DTO
            var movie = new Movie
            {
                Id = movieDTO.Id,
                Title = movieDTO.Title,
                Director = movieDTO.Director,
                Genre = movieDTO.Genre,
                ReleaseYear = movieDTO.ReleaseYear,
                Description = movieDTO.Description
            };

            // Добавляем фильм в репозиторий и сохраняем изменения
            await UnitOfWork.Movies.Create(movie);
            await UnitOfWork.Save();
        }

        // Метод для обновления информации о фильме
        public async Task UpdateMovie(MovieDTO movieDTO)
        {
            // Получаем фильм по id
            var movie = await UnitOfWork.Movies.Get(movieDTO.Id);

            if (movie != null)
            {
                // Обновляем свойства фильма из DTO
                movie.Id = movieDTO.Id;
                movie.Title = movieDTO.Title;
                movie.Director = movieDTO.Director;
                movie.Genre = movieDTO.Genre;
                movie.ReleaseYear = movieDTO.ReleaseYear;
                movie.Description = movieDTO.Description;

                // Сохраняем изменения в репозитории                
                await UnitOfWork.Save();
            }
            else
            {
                throw new ValidationException("Фильм не найден");
            }
        }

        // Метод для удаления фильма по id
        public async Task DeleteMovie(int id)
        {
            // Удаляем фильм из репозитория и сохраняем изменения
            await UnitOfWork.Movies.Delete(id);
            await UnitOfWork.Save();
        }
    }
}
