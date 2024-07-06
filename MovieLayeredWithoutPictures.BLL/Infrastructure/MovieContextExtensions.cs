using Microsoft.EntityFrameworkCore;
using MovieLayeredWithoutPictures.DAL.EF;

namespace MovieLayeredWithoutPictures.BLL.Infrastructure
{
    public static class MovieContextExtensions
    {
        public static void AddMovieContext(this IServiceCollection services, string connection)
        {
            services.AddDbContext<MovieContext>(options =>options.UseSqlServer(connection));
        }
    }
}
