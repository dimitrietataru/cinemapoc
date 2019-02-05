 using AutoMapper;
using Core.Context;
using Core.Interfaces;
using Core.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CMS
{
    public partial class Startup
    {
        public void ConfigureDataServices(IServiceCollection services)
        {
            services.AddAutoMapper();

            string dbConnection = Configuration.GetConnectionString("SqlServer");
            services.AddDbContext<CinemaContext>(
                options => options.UseSqlServer(dbConnection), ServiceLifetime.Scoped);

            services.AddScoped<ICinemaService, CinemaService>();
            services.AddScoped<IMovieService, MovieService>();
            services.AddScoped<ICinemaMovieService, CinemaMovieService>();
        }
    }
}
