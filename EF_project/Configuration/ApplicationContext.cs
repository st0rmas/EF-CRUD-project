using EF_project.Configuration.EntityConfiguration;
using EF_project.Entities;
using EF_project.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace EF_project.Configuration;

public class ApplicationContext : DbContext {
    public DbSet<Client> Clients { get; set; }
    public DbSet<Tour> Tours { get; set; }
    public DbSet<City> Cities { get; set; }
    public DbSet<Agency> Agencies { get; set; }
    public DbSet<Passport> Passports { get; set; }
    
    public ApplicationContext() { }
    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
        var config = new ConfigurationBuilder()
            .AddJsonFile("/Users/st0rm/Desktop/programming/c#/console_projects/EF_project/EF_project/appsettings.json")
            .Build();
    
        optionsBuilder.UseNpgsql(config.GetConnectionString("DefaultConnection"));
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
 
        modelBuilder.ApplyConfiguration(new ClientConfiguration());
        modelBuilder.ApplyConfiguration(new AgencyConfiguration());
        modelBuilder.ApplyConfiguration(new TourConfiguration());
        modelBuilder.ApplyConfiguration(new CityConfiguration());
    }

}