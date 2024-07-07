using MovieLayeredWithoutPictures.DAL.Interfaces;
using MovieLayeredWithoutPictures.DAL.Repositories;

namespace MovieLayeredWithoutPictures.BLL.Infrastructure
{    
    public static class UnitOfWorkServiceExtensions
    {
        public static void AddUnitOfWorkService(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork,EFUnitOfWork> ();
        }
    }
}
