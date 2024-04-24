using EF_project.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EF_project.Configuration.EntityConfiguration;

public class TourConfiguration : IEntityTypeConfiguration<Tour> {

    public void Configure(EntityTypeBuilder<Tour> builder) {
        builder.ToTable("tours");
    }
}