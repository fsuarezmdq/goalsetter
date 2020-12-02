using Goalsetter.Domains;
using Goalsetter.Domains.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Goalsetter.DataAccess.EntityConfiguration
{
    public class VehicleEntityTypeConfiguration : IEntityTypeConfiguration<Vehicle>
    {
        public void Configure(EntityTypeBuilder<Vehicle> builder)
        {
            builder.ToTable("Vehicles")
                .HasKey(k => k.Id);
            builder.Property(p => p.Id)
                .IsRequired();
            builder.Property(p => p.Makes)
                .HasMaxLength(VehicleMakes.MaxLength)
                .IsRequired()
                .HasConversion(p => p.Value, p => VehicleMakes.Create(p).Value);
            builder.Property(p => p.Model)
                .HasMaxLength(VehicleModel.MaxLength)
                .IsRequired()
                .HasConversion(p => p.Value, p => VehicleModel.Create(p).Value); ;
            builder.Property(p => p.Year)
                .IsRequired();
            builder.Property(p => p.CreatedDate)
                .IsRequired()
                .HasDefaultValueSql("GetUtcDate()");
            builder.Property(p => p.UpdatedDate)
                .IsRequired()
                .HasDefaultValueSql("GetUtcDate()");
            builder.HasOne(p =>p.RentalPrice)
                .WithOne(p => p.Vehicle)
                .HasForeignKey<VehiclePrice>()
                .IsRequired();
            builder.HasMany(p => p.Rentals)
                .WithOne(p=>p.Vehicle)
                .Metadata.PrincipalToDependent.SetPropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}