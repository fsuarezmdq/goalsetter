using Goalsetter.Domains;
using Goalsetter.Domains.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Goalsetter.DataAccess.EntityConfiguration
{
    internal class ClientEntityTypeConfiguration : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            builder.ToTable("Clients").HasKey(k => k.Id);
            builder.Property(p => p.Id).HasColumnName("ClientID").IsRequired();
            builder.Property(p => p.ClientName)
                .HasMaxLength(ClientName.MaxLength)
                .IsRequired()
                .HasConversion(p => p.Value, p => ClientName.Create(p).Value);
            builder.Property(p => p.Email)
                .HasMaxLength(Email.MaxLength)
                .IsRequired()
                .HasConversion(p => p.Value, p => Email.Create(p).Value); ;
            builder.Property(p => p.CreatedDate)
                .IsRequired()
                .HasDefaultValueSql("GetUtcDate()");
            builder.Property(p => p.UpdatedDate)
                .IsRequired()
                .HasDefaultValueSql("GetUtcDate()");
            builder.HasMany(p => p.Rentals)
                .WithOne(p => p.Client)
                .Metadata.PrincipalToDependent.SetPropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}