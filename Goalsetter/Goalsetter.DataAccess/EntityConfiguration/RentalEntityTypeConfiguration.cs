using Goalsetter.Domains;
using Goalsetter.Domains.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Goalsetter.DataAccess.EntityConfiguration
{
    public class RentalEntityTypeConfiguration : IEntityTypeConfiguration<Rental>
    {
        public void Configure(EntityTypeBuilder<Rental> builder)
        {
            builder.ToTable("Rentals")
                .HasKey(k => k.Id);

            builder.Property(p => p.Id)
                .IsRequired();
            builder.OwnsOne(p => p.DateRange, p =>
            {
                p.Property(pp => pp.StartDate)
                    .HasColumnName("StartDate")
                    .IsRequired();
                p.Property(pp => pp.EndDate)
                    .HasColumnName("EndDate")
                    .IsRequired();
            });
            builder.Property(p => p.CreatedDate)
                .IsRequired()
                .HasDefaultValueSql("GetUtcDate()");
            builder.Property(p => p.UpdatedDate)
                .IsRequired()
                .HasDefaultValueSql("GetUtcDate()");
            builder.Property(p => p.TotalPrice)
                .HasConversion(p => p.Value, p => Price.Create(p).Value)
                .IsRequired();
            
            builder.Navigation("Client");
            builder.Navigation("DateRange");
            builder.Navigation("Vehicle");
        }
    }
}