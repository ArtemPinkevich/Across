namespace DataAccess.BaseImplementation
{
    using Entities;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    public abstract class DatabaseContext : IdentityDbContext<User>
    {
        protected DatabaseContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Record> Records { set; get; }

        public DbSet<Vehicle> Vehicles { set; get; }
        
        public DbSet<CarWash> CarWashes { set; get; }

        public DbSet<WashService> WashServices { set; get; }

        public DbSet<ComplexWashService> ComplexWashServices { set; get; }

        public DbSet<PriceGroup> PriceGroups { set; get; }

        public DbSet<WorkSchedule> WorkSchedules { set; get; }
        
        public DbSet<CarBody> CarBodies { set; get; }
        
        public DbSet<WashServiceSettings> WashServiceSettings { set; get; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasMany(c => c.FavouriteCarWashes)
                .WithMany(c => c.SelectedByUsers)
                .UsingEntity(c => c.ToTable("FavouriteCarWashesSelectedByUsers"));

            modelBuilder.Entity<User>()
                .HasMany(c => c.CarWashes)
                .WithMany(c => c.Users)
                .UsingEntity(c => c.ToTable("CarWasheUser"));

            base.OnModelCreating(modelBuilder);
        }
    }
}
