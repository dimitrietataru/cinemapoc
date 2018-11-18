using Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.Configurations
{
    internal sealed class CinemaConfiguration : IEntityTypeConfiguration<Cinema>
    {
        public void Configure(EntityTypeBuilder<Cinema> builder)
        {
            builder.ToTable("Cinemas");

            builder.HasQueryFilter(cinema => !cinema.IsDeleted);

            builder.HasKey(cinema => cinema.Id)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            builder.Property(cinema => cinema.Name)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(cinema => cinema.Description)
                .HasMaxLength(1000)
                .IsRequired();

            builder.Property(cinema => cinema.Location)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(cinema => cinema.Address)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(cinema => cinema.Contact)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(cinema => cinema.Schedule)
                .HasMaxLength(4001)
                .IsRequired();

            builder.Property(cinema => cinema.CreatedAt)
                .IsRequired();

            builder.Property(cinema => cinema.CreatedBy)
                .IsRequired();

            builder.Property(cinema => cinema.IsDeleted)
                .HasDefaultValue(false)
                .IsRequired();
        }
    }
}
