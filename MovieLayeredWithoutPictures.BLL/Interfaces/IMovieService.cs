using MovieLayeredWithoutPictures.BLL.DTO;

namespace MovieLayeredWithoutPictures.BLL.Interfaces
{
    // Определяет контракт для работы с фильмами в приложении через слой бизнес-логики
    public interface IMovieService
    {
        Task<IEnumerable<MovieDTO>> GetMovies();
        Task<MovieDTO> GetMovie(int id);
        Task<MovieDTO> GetMovie(string title);
        Task CreateMovie(MovieDTO movieDTO);
        Task UpdateMovie(MovieDTO movieDTO);
        Task DeleteMovie(int id);
    }
}
