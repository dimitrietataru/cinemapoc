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

            services.AddDbContext<CinemaContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("Production")), ServiceLifetime.Scoped);

            services.AddScoped<ICinemaService, CinemaService>();
        }
    }
}
