using EF_project.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EF_project.Configuration.EntityConfiguration;

public class CityConfiguration : IEntityTypeConfiguration<City> {

    public void Configure(EntityTypeBuilder<City> builder) {
        builder.ToTable("cities");
        builder
            .HasMany(city => city.Tours)
            .WithOne(tour => tour.City)
            .HasForeignKey(tour=>tour.CityId)
            .OnDelete(DeleteBehavior.Cascade);
    }
    
}