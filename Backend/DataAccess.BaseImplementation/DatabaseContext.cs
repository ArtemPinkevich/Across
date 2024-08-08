using Entities.Document;

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

        public DbSet<CarWash> CarWashes { set; get; }
        
        public DbSet<Truck> Trucks { set; get; }
        
        public DbSet<Cargo> Cargos { set; get; }
        
        public DbSet<TransportationOrder> TransportationOrders { set; get; }
        
        public DbSet<TruckRequirements> TruckRequirements { set; get; }
        
        public DbSet<TransferChangeStatusRecord> TransferChangeHistoryRecords { set; get; }

        public DbSet<TransferAssignedDriverRecord> TransferAssignedDriverRecords { set; get; }
        
        public DbSet<Document> Documents { set; get; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<User>()
            //    .HasMany(c => c.FavouriteCarWashes)
            //    .WithMany(c => c.SelectedByUsers)
            //    .UsingEntity(c => c.ToTable("FavouriteCarWashesSelectedByUsers"));

            //modelBuilder.Entity<User>()
            //    .HasMany(c => c.CarWashes)
            //    .WithMany(c => c.Users)
            //    .UsingEntity(c => c.ToTable("CarWasheUser"));

            modelBuilder.Entity<User>()
                .HasMany(user => user.TransportationOrders)
                .WithOne(transportationOrder => transportationOrder.User)
                .HasForeignKey(transportationOrder => transportationOrder.UserId)
                .IsRequired();
            
            modelBuilder.Entity<User>()
                .HasMany(user => user.OrdersOfferedByDriver)
                .WithMany(transportationOrder => transportationOrder.PossibleDrivers)
                .UsingEntity(x => x.ToTable("DriverAndOrderWishes"));
            
            base.OnModelCreating(modelBuilder);
        }
    }
}
