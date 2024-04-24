using EF_project.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EF_project.Configuration.EntityConfiguration;

public class AgencyConfiguration : IEntityTypeConfiguration<Agency> {

    public void Configure(EntityTypeBuilder<Agency> builder) {
        builder.ToTable("agencies");
        builder
            .HasMany(agency => agency.Tours)
            .WithOne(tour => tour.Agency)
            .HasForeignKey(tour => tour.AgencyId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}