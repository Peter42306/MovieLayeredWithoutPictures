using Microsoft.EntityFrameworkCore;
using MovieLayeredWithoutPictures.DAL.EF;
using MovieLayeredWithoutPictures.DAL.Entities;
using MovieLayeredWithoutPictures.DAL.Interfaces;

namespace MovieLayeredWithoutPictures.DAL.Repositories
{
    // Репозиторий для управления сущностями Movie в базе данных
    public class MovieRepository : IRepository<Movie>
    {
        private MovieContext _movieContext; // Контекст базы данных для работы с сущностями Movie

        // Конструктор для инициализации контекста через внедрение зависимостей
        public MovieRepository(MovieContext movieContext)
        {
            _movieContext = movieContext;

        }

        // Метод для получения всех фильмов
        public async Task<IEnumerable<Movie>> GetAll()
        {
            return await _movieContext.Movies.ToListAsync(); // Асинхронно возвращает список всех фильмов из базы данных
        }

        // Метод для получения фильма по ID
        public async Task<Movie> Get(int id)
        {

            Movie? movie = await _movieContext.Movies.FindAsync(id); // Асинхронно ищет фильм по ID
            return movie;
        }

        // Метод для получения фильма по названию
        public async Task<Movie> Get(string title)
        {
            var movies = await _movieContext.Movies.Where(x => x.Title == title).ToListAsync(); // Асинхронно ищет фильмы с указанным названием
            Movie? movie = movies?.FirstOrDefault(); // Возвращает первый найденный фильм или null, если таких фильмов нет
            return movie;
        }

        // Метод для создания нового фильма
        public async Task Create(Movie movie)
        {
            await _movieContext.Movies.AddAsync(movie); // Асинхронно добавляет новый фильм в базу данных
            await _movieContext.SaveChangesAsync(); // Сохраняет изменения в базе данных
        }

        // Метод для обновления существующего фильма
        public async void Update(Movie movie)
        {
            _movieContext.Entry(movie).State = EntityState.Modified; // Устанавливает состояние фильма как измененное
            await _movieContext.SaveChangesAsync(); // Асинхронно сохраняет изменения в базе данных
        }

        // Метод для удаления фильма по ID
        public async Task Delete(int id)
        {
            Movie? movie = await _movieContext.Movies.FirstOrDefaultAsync(x => x.Id == id);
            if (movie != null)
            {
                _movieContext.Movies.Remove(movie); // Удаляет найденный фильм из базы данных
                await _movieContext.SaveChangesAsync(); // Асинхронно сохраняет изменения в базе данных
            }
        }
    }
}
