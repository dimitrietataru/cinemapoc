using Core.Models;
using Core.Models.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.Configurations
{
    internal sealed class MovieConfiguration : IEntityTypeConfiguration<Movie>
    {
        public void Configure(EntityTypeBuilder<Movie> builder)
        {
            builder.ToTable("Movies");

            builder.HasQueryFilter(movie => !movie.IsDeleted);

            builder.HasKey(movie => movie.Id)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            builder.Property(movie => movie.Name)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(movie => movie.Description)
                .HasMaxLength(4001)
                .IsRequired();

            builder.Property(movie => movie.Duration)
                .IsRequired();

            builder.Property(movie => movie.Actors)
                .HasMaxLength(4001)
                .IsRequired();

            builder.Property(movie => movie.Studio)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(movie => movie.ProjectionType)
                .HasDefaultValue(ProjectionType.D2D)
                .IsRequired();

            builder.Property(movie => movie.Rating)
                .HasDefaultValue(MovieRating.G)
                .IsRequired();

            builder.Property(movie => movie.Genre)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(movie => movie.TrailerUrl)
                .HasMaxLength(200);

            builder.Property(movie => movie.PosterUrl)
                .HasMaxLength(200);

            builder.Property(movie => movie.ImbdUrl)
                .HasMaxLength(200);

            builder.Property(movie => movie.StartDate)
                .IsRequired();

            builder.Property(movie => movie.EndDate)
                .IsRequired();

            builder.Property(movie => movie.CreatedAt)
                .IsRequired();

            builder.Property(movie => movie.CreatedBy)
                .IsRequired();

            builder.Property(movie => movie.IsDeleted)
                .HasDefaultValue(false)
                .IsRequired();
        }
    }
}
