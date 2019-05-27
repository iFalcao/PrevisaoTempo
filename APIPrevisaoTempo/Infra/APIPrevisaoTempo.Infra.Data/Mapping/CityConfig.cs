using APIPrevisaoTempo.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace APIPrevisaoTempo.Infra.Data.Mapping
{
    public class CityConfig : IEntityTypeConfiguration<City>
    {

        public void Configure(EntityTypeBuilder<City> builder)
        {
            builder.Property(_ => _.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(_ => _.CustomCode)
                .IsRequired()
                .HasMaxLength(30);

            builder.Property(_ => _.Country)
                .HasMaxLength(3);

            builder.Property(_ => _.Latitude)
                .HasColumnType("decimal(9,6)");

            builder.Property(_ => _.Longitude)
                .HasColumnType("decimal(9,6)");

            builder.HasIndex(_ => _.CustomCode).IsUnique();
        }
    }
}
