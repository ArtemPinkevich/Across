using GeoService.Entities;
using Microsoft.EntityFrameworkCore;

namespace GeoService.DataAccess;

public class GeoDbContext:DbContext
{
    public DbSet<City> Cities { get; set; }
    
    public DbSet<Region> Regions { get; set; }

    public DbSet<Country> Countries { get; set; }
    
    public GeoDbContext(DbContextOptions<GeoDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
    }
}