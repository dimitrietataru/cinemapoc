using Core.Models;
using Core.Models.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.Configurations
{
    internal sealed class BookingConfiguration : IEntityTypeConfiguration<Booking>
    {
        public void Configure(EntityTypeBuilder<Booking> builder)
        {
            builder.ToTable("Bookings");

            builder.HasQueryFilter(booking => !booking.IsDeleted);

            builder.HasOne(booking => booking.Event)
                .WithMany(@event => @event.Bookings)
                .HasForeignKey(booking => booking.EventId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasKey(booking => booking.Id)
               .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            builder.Property(booking => booking.EventId)
                .IsRequired();

            builder.Property(booking => booking.Status)
                .HasDefaultValue(BookingStatus.Free)
                .IsRequired();

            builder.Property(booking => booking.Seat)
                .HasMaxLength(10)
                .IsRequired();

            builder.Property(booking => booking.CreatedAt)
                .IsRequired();

            builder.Property(booking => booking.CreatedBy)
                .IsRequired();

            builder.Property(booking => booking.IsDeleted)
                .HasDefaultValue(false)
                .IsRequired();
        }
    }
}
