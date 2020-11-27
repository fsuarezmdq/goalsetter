using Goalsetter.Domains;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Goalsetter.DataAccess
{
    internal class VehicleEntityTypeConfiguration : IEntityTypeConfiguration<Vehicle>
    {
        public void Configure(EntityTypeBuilder<Vehicle> builder)
        {
            builder.ToTable("Vehicle").HasKey(k => k.Id);
            builder.Property(p => p.Id).HasColumnName("VehicleID").IsRequired();
            builder.Property(p => p.Makes)
                .HasMaxLength(VehicleMakes.MaxLenth)
                .IsRequired()
                .HasConversion(p => p.Value, p => VehicleMakes.Create(p).Value);

            builder.Property(p => p.Model).HasMaxLength(250).IsRequired();
            builder.Property(p => p.Year).IsRequired();

            //TODO: Setup column types and conversions
        }
    }
}