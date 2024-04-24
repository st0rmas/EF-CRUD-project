using EF_project.Entities;
using EF_project.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EF_project.Configuration.EntityConfiguration;

public class ClientConfiguration : IEntityTypeConfiguration<Client> {

    public void Configure(EntityTypeBuilder<Client> builder) {
        builder.ToTable("clients");
        builder
            .HasOne(client => client.Passport)
            .WithOne(passport => passport.Client)
            .HasForeignKey<Passport>(p => p.ClientId)
            .OnDelete(DeleteBehavior.Cascade);
        builder
            .HasMany(client => client.Tours)
            .WithMany(tours => tours.Clients)
            .UsingEntity(j => j.ToTable("orders"));
        builder.HasKey(client => client.Id);
    }
}