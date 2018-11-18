using Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.Configurations
{
    internal sealed class EventConfiguration : IEntityTypeConfiguration<Event>
    {
        public void Configure(EntityTypeBuilder<Event> builder)
        {
            builder.ToTable("Events");

            builder.HasQueryFilter(@event => !@event.IsDeleted);

            builder.HasOne(@event => @event.Auditorium)
                .WithMany(auditorium => auditorium.Events)
                .HasForeignKey(@event => @event.AuditoriumId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasOne(@event => @event.Movie)
                .WithMany(movie => movie.Events)
                .HasForeignKey(@event => @event.MovieId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasKey(@event => @event.Id)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            builder.Property(@event => @event.MovieId)
                .IsRequired();

            builder.Property(@event => @event.AuditoriumId)
                .IsRequired();

            builder.Property(@event => @event.Start)
                .IsRequired();

            builder.Property(@event => @event.End)
                .IsRequired();

            builder.Property(@event => @event.CreatedAt)
                .IsRequired();

            builder.Property(@event => @event.CreatedBy)
                .IsRequired();

            builder.Property(@event => @event.IsDeleted)
                .HasDefaultValue(false)
                .IsRequired();
        }
    }
}
