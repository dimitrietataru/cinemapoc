using Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.Configurations
{
    internal sealed class CinemaMovieConfiguration : IEntityTypeConfiguration<CinemaMovie>
    {
        public void Configure(EntityTypeBuilder<CinemaMovie> builder)
        {
            builder.ToTable("CinemaMovies");

            builder.HasOne(cinemaMovie => cinemaMovie.Cinema);

            builder.HasOne(cinemaMovie => cinemaMovie.Movie);

            builder.HasKey(cinemaMovie => new { cinemaMovie.CinemaId, cinemaMovie.MovieId });

            builder.Property(cm => cm.CinemaId)
                .IsRequired();

            builder.Property(cm => cm.MovieId)
                .IsRequired();
        }
    }
}
