using MovieLayeredWithoutPictures.BLL.DTO;

namespace MovieLayeredWithoutPictures.BLL.Interfaces
{
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
