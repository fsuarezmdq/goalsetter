using Goalsetter.Domains;
using Goalsetter.Domains.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Goalsetter.DataAccess.EntityConfiguration
{
    public class VehiclePriceEntityTypeConfiguration : IEntityTypeConfiguration<VehiclePrice>
    {
        public void Configure(EntityTypeBuilder<VehiclePrice> builder)
        {
            builder.ToTable("VehiclePrice").HasKey(k => k.Id);
            builder.Property(p => p.Id)
                .HasColumnName("VehiclePriceID")
                .IsRequired();
            builder.Property(p => p.Price)
                .IsRequired()
                .HasConversion(p => p.Value, p => Price.Create(p).Value);
            builder.Property(p => p.CreatedDate)
                .IsRequired()
                .HasDefaultValueSql("GetUtcDate()");
            builder.Property(p => p.UpdatedDate)
                .IsRequired()
                .HasDefaultValueSql("GetUtcDate()");
            builder.HasOne(p => p.Vehicle)
                .WithOne(p => p.RentalPrice)
                .OnDelete(DeleteBehavior.Cascade);
            builder.Navigation(p => p.Vehicle);
        }
    }
}