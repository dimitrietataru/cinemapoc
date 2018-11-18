using Core.Models;
using Core.Models.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.Configurations
{
    internal sealed class AuditoriumConfiguration : IEntityTypeConfiguration<Auditorium>
    {
        public void Configure(EntityTypeBuilder<Auditorium> builder)
        {
            builder.ToTable("Auditoriums");

            builder.HasQueryFilter(auditorium => !auditorium.IsDeleted);

            builder.HasOne(auditorium => auditorium.Cinema)
               .WithMany(cinema => cinema.Auditoriums)
               .HasForeignKey(auditorium => auditorium.CinemaId)
               .OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasKey(auditorium => auditorium.Id)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            builder.Property(auditorium => auditorium.CinemaId)
               .IsRequired();

            builder.Property(auditorium => auditorium.Name)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(auditorium => auditorium.Capacity)
                .IsRequired();

            builder.Property(auditorium => auditorium.Seats)
                .HasMaxLength(4001)
                .IsRequired();

            builder.Property(auditorium => auditorium.Status)
                .HasDefaultValue(AuditoriumStatus.Operational)
                .IsRequired();

            builder.Property(auditorium => auditorium.SeatType)
                .HasDefaultValue(AuditoriumSeatType.Normal)
                .IsRequired();

            builder.Property(auditorium => auditorium.ScreenType)
                .HasDefaultValue(AuditoriumScreenType.Basic)
                .IsRequired();

            builder.Property(auditorium => auditorium.CreatedAt)
                .IsRequired();

            builder.Property(auditorium => auditorium.CreatedBy)
                .IsRequired();

            builder.Property(auditorium => auditorium.IsDeleted)
                .HasDefaultValue(false)
                .IsRequired();
        }
    }
}
